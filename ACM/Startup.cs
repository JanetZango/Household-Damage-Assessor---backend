using Hangfire;
using Hangfire.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using ACM.Services.ClickatellServiceFactory;
using ACM.SignalRHubs;
using ACM.ViewModels.Services.SystemConfigServiceFactory;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace ACM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowAnyOrigin();
                    });
            });

            services.AddCors();


            services.AddControllersWithViews();

            services.AddHttpClient();

            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<SecurityOptions>(Configuration.GetSection("Security"));
            services.Configure<FileStorageOptions>(Configuration.GetSection("FileStorage"));

            services.Configure<SingoNetworkAPI>(Configuration.GetSection("SingoNetworkAPI"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.Configure<EmailOptions>(Configuration.GetSection("SystemEmailSMTP"));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISystemConfigService, SystemConfigService>();

            services.AddScoped<BackgroundJobHelper>();

            services.AddHttpClient<IClickatellService, ClickatellService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddSessionLocalization();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = GetJwtSigningCredentials();
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(12);
                    options.Cookie.Name = "ACMAuthenticationCookie";
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtIssuerOptions:Issuer"],
                        ValidAudience = Configuration["JwtIssuerOptions:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Security:SecretKey"]))
                    };
                });

            //Add Swagger Generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Integration", Version = "v1" });
                c.IncludeXmlComments(GetXmlCommentsPath(PlatformServices.Default.Application));
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString =
                    Configuration.GetConnectionString("CacheConnection");
                options.SchemaName = Configuration["CacheSettings:SchemaName"];
                options.TableName = Configuration["CacheSettings:TableName"];
                options.DefaultSlidingExpiration = new TimeSpan(12, 0, 0);
            });
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddDetection();

            services.AddControllersWithViews();

            services.AddSignalR(hubOptions =>
            {
                hubOptions.HandshakeTimeout = TimeSpan.FromSeconds(30);
                hubOptions.ClientTimeoutInterval = TimeSpan.FromSeconds(1200);
                hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(15);
                hubOptions.EnableDetailedErrors = true;
            });
            services.AddSingleton<IUserIdProvider, SignalRCustomUserIdProvider>();

            services.AddProgressiveWebApp("/manifest/manifest.webmanifest");
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(10, retryAttempt)));

        }
        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(6, TimeSpan.FromSeconds(30));
        }

        private string GetXmlCommentsPath(ApplicationEnvironment appEnvironment)
        {
            return Path.Combine(appEnvironment.ApplicationBasePath, "App.xml");
        }

        private SigningCredentials GetJwtSigningCredentials()
        {
            var secSettings = Configuration.GetSection("Security");

            string secretKey = secSettings["SecretKey"];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            return new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDBContext dbcontext, IOptions<SecurityOptions> securityOptions,
            IRecurringJobManager recurringJobs)
        {
            dbcontext.Database.Migrate();

            app.UseOptions();
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                }
            });

            app.UseDetection();
            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizeFilter() }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<UIUpdateHub>("/uiUpdateHub");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(dbcontext, securityOptions.Value);

            //Setup Background Jobs
            SetupBackgroundJobs(recurringJobs);
        }

        private void SetupBackgroundJobs(IRecurringJobManager recurringJobs)
        {
            recurringJobs.AddOrUpdate("CleanApplicationLog", Job.FromExpression<BackgroundJobHelper>(x => x.CleanApplicationLog()), "0 8 * * SAT");

            recurringJobs.AddOrUpdate("UpcomingEventReminder", Job.FromExpression<BackgroundJobHelper>(x => x.SendUpcomingEventReminder(CancellationToken.None)), "*/30 * * * *");

        }
    }
}
