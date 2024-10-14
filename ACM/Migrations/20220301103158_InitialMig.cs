using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class InitialMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsoAlpha2Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsoAlpha3Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallingCodePrefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumberValidationLength = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    DisciplineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IconClassName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.DisciplineID);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    EmailTemplateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateBodyTags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSTemplateBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.EmailTemplateID);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    FAQID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Catergory = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.FAQID);
                });

            migrationBuilder.CreateTable(
                name: "LanguageCultures",
                columns: table => new
                {
                    LanguageCultureID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CultureNameCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageCultures", x => x.LanguageCultureID);
                });

            migrationBuilder.CreateTable(
                name: "LinkUserRoleAudit",
                columns: table => new
                {
                    LinkUserRoleAuditID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkUserRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidFromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidToDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkUserRoleAudit", x => x.LinkUserRoleAuditID);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfiguration",
                columns: table => new
                {
                    SystemConfigurationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfigValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfiguration", x => x.SystemConfigurationID);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryTokensType",
                columns: table => new
                {
                    TemporaryTokensTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryTokensType", x => x.TemporaryTokensTypeID);
                });

            migrationBuilder.CreateTable(
                name: "UserAudit",
                columns: table => new
                {
                    UserAuditID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellphoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginTries = table.Column<int>(type: "int", nullable: false),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    AcceptTermsAndConditions = table.Column<bool>(type: "bit", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCultureID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValidFromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidToDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAudit", x => x.UserAuditID);
                });

            migrationBuilder.CreateTable(
                name: "UserPaymentTransactions",
                columns: table => new
                {
                    UserPaymentTransactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountGross = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountNet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PFPaymentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PFReferenceID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PFPaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentRefID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPaymentTransactions", x => x.UserPaymentTransactionID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleID);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvIsoCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceID);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID");
                });

            migrationBuilder.CreateTable(
                name: "DisciplineSpecialities",
                columns: table => new
                {
                    DisciplineSpecialityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisciplineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IconClassName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineSpecialities", x => x.DisciplineSpecialityID);
                    table.ForeignKey(
                        name: "FK_DisciplineSpecialities_Disciplines_DisciplineID",
                        column: x => x.DisciplineID,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalizationValues",
                columns: table => new
                {
                    LocalizationValueID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageCultureID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KeyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationValues", x => x.LocalizationValueID);
                    table.ForeignKey(
                        name: "FK_LocalizationValues_LanguageCultures_LanguageCultureID",
                        column: x => x.LanguageCultureID,
                        principalTable: "LanguageCultures",
                        principalColumn: "LanguageCultureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageCultureID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProvinceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellphoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginTries = table.Column<int>(type: "int", nullable: false),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    AcceptTermsAndConditions = table.Column<bool>(type: "bit", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiveEmailNotification = table.Column<bool>(type: "bit", nullable: false),
                    IsAdminApproved = table.Column<bool>(type: "bit", nullable: false),
                    ProfileImageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID");
                    table.ForeignKey(
                        name: "FK_Users_LanguageCultures_LanguageCultureID",
                        column: x => x.LanguageCultureID,
                        principalTable: "LanguageCultures",
                        principalColumn: "LanguageCultureID");
                    table.ForeignKey(
                        name: "FK_Users_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceID");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationLog",
                columns: table => new
                {
                    ApplicationLogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogOriginator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationLog", x => x.ApplicationLogID);
                    table.ForeignKey(
                        name: "FK_ApplicationLog_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "CalendarEventTypes",
                columns: table => new
                {
                    CalendarEventTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEventTypes", x => x.CalendarEventTypeID);
                    table.ForeignKey(
                        name: "FK_CalendarEventTypes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkUserRole",
                columns: table => new
                {
                    LinkUserRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkUserRole", x => x.LinkUserRoleID);
                    table.ForeignKey(
                        name: "FK_LinkUserRole_UserRoles_UserRoleID",
                        column: x => x.UserRoleID,
                        principalTable: "UserRoles",
                        principalColumn: "UserRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkUserRole_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInAppNotifications",
                columns: table => new
                {
                    UserInAppNotificationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInAppNotifications", x => x.UserInAppNotificationID);
                    table.ForeignKey(
                        name: "FK_UserInAppNotifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTemporaryToken",
                columns: table => new
                {
                    UserTemporaryTokenID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemporaryTokensTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTemporaryToken", x => x.UserTemporaryTokenID);
                    table.ForeignKey(
                        name: "FK_UserTemporaryToken_TemporaryTokensType_TemporaryTokensTypeID",
                        column: x => x.TemporaryTokensTypeID,
                        principalTable: "TemporaryTokensType",
                        principalColumn: "TemporaryTokensTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTemporaryToken_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                columns: table => new
                {
                    CalendarEventID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarEventTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAllDay = table.Column<bool>(type: "bit", nullable: false),
                    EnableReminder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.CalendarEventID);
                    table.ForeignKey(
                        name: "FK_CalendarEvents_CalendarEventTypes_CalendarEventTypeID",
                        column: x => x.CalendarEventTypeID,
                        principalTable: "CalendarEventTypes",
                        principalColumn: "CalendarEventTypeID");
                    table.ForeignKey(
                        name: "FK_CalendarEvents_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEventTypeMetaField",
                columns: table => new
                {
                    CalendarEventTypeMetaFieldID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarEventTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEventTypeMetaField", x => x.CalendarEventTypeMetaFieldID);
                    table.ForeignKey(
                        name: "FK_CalendarEventTypeMetaField_CalendarEventTypes_CalendarEventTypeID",
                        column: x => x.CalendarEventTypeID,
                        principalTable: "CalendarEventTypes",
                        principalColumn: "CalendarEventTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEventMetaFieldValues",
                columns: table => new
                {
                    CalendarEventMetaFieldValueID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarEventID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarEventTypeMetaFieldID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetaValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEventMetaFieldValues", x => x.CalendarEventMetaFieldValueID);
                    table.ForeignKey(
                        name: "FK_CalendarEventMetaFieldValues_CalendarEvents_CalendarEventID",
                        column: x => x.CalendarEventID,
                        principalTable: "CalendarEvents",
                        principalColumn: "CalendarEventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarEventMetaFieldValues_CalendarEventTypeMetaField_CalendarEventTypeMetaFieldID",
                        column: x => x.CalendarEventTypeMetaFieldID,
                        principalTable: "CalendarEventTypeMetaField",
                        principalColumn: "CalendarEventTypeMetaFieldID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationLog_UserID",
                table: "ApplicationLog",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEventMetaFieldValues_CalendarEventID",
                table: "CalendarEventMetaFieldValues",
                column: "CalendarEventID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEventMetaFieldValues_CalendarEventTypeMetaFieldID",
                table: "CalendarEventMetaFieldValues",
                column: "CalendarEventTypeMetaFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_CalendarEventTypeID",
                table: "CalendarEvents",
                column: "CalendarEventTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_UserID",
                table: "CalendarEvents",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEventTypeMetaField_CalendarEventTypeID",
                table: "CalendarEventTypeMetaField",
                column: "CalendarEventTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEventTypes_UserID",
                table: "CalendarEventTypes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineSpecialities_DisciplineID",
                table: "DisciplineSpecialities",
                column: "DisciplineID");

            migrationBuilder.CreateIndex(
                name: "IX_LinkUserRole_UserID",
                table: "LinkUserRole",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LinkUserRole_UserRoleID",
                table: "LinkUserRole",
                column: "UserRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationValues_LanguageCultureID",
                table: "LocalizationValues",
                column: "LanguageCultureID");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryID",
                table: "Provinces",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_UserInAppNotifications_UserID",
                table: "UserInAppNotifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryID",
                table: "Users",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LanguageCultureID",
                table: "Users",
                column: "LanguageCultureID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProvinceID",
                table: "Users",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTemporaryToken_TemporaryTokensTypeID",
                table: "UserTemporaryToken",
                column: "TemporaryTokensTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTemporaryToken_UserID",
                table: "UserTemporaryToken",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationLog");

            migrationBuilder.DropTable(
                name: "CalendarEventMetaFieldValues");

            migrationBuilder.DropTable(
                name: "DisciplineSpecialities");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "LinkUserRole");

            migrationBuilder.DropTable(
                name: "LinkUserRoleAudit");

            migrationBuilder.DropTable(
                name: "LocalizationValues");

            migrationBuilder.DropTable(
                name: "SystemConfiguration");

            migrationBuilder.DropTable(
                name: "UserAudit");

            migrationBuilder.DropTable(
                name: "UserInAppNotifications");

            migrationBuilder.DropTable(
                name: "UserPaymentTransactions");

            migrationBuilder.DropTable(
                name: "UserTemporaryToken");

            migrationBuilder.DropTable(
                name: "CalendarEvents");

            migrationBuilder.DropTable(
                name: "CalendarEventTypeMetaField");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "TemporaryTokensType");

            migrationBuilder.DropTable(
                name: "CalendarEventTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LanguageCultures");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
