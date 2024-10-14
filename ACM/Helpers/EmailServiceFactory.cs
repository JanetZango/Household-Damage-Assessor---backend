using App.TemplateParser;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using MailKit;

namespace ACM.Helpers.EmailServiceFactory
{
    public interface IEmailService
    {
        string EmailBody { get; set; }
        string SMSBody { get; set; }
        string Response { get; set; }
        MimeMessage Message { get; set; }
        Exception? ExceptionResponse { get; set; }

        Task SendEmailAsync(List<string> toEmailAddresses, String subject,
            PublicEnums.EmailTemplateList emailTemplate, Dictionary<string, PropertyMetaData> variableValues,
            ClaimsPrincipal user, List<string> ccEmailAddresses = null, List<EmailAttachment> attachments = null, bool receiveEmail = true, 
            bool requestDeliveryReceipt = false, bool requestReadReceipt = false, string replyToEmailAddress = "", string replyToName = "");

        Task<string> ProcessEmailTemplate(PublicEnums.EmailTemplateList emailTemplate, Dictionary<string, PropertyMetaData> variableValues);
        Task<string> ProcessSMSTemplate(PublicEnums.EmailTemplateList emailTemplate, Dictionary<string, PropertyMetaData> variableValues);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly AppDBContext _context;

        public string EmailBody { get; set; }
        public string SMSBody { get; set; }
        public string Response { get; set; }
        public MimeMessage Message { get; set; }
        public Exception? ExceptionResponse { get; set; }

        public EmailService(AppDBContext context, IOptions<EmailOptions> emailOptions)
        {
            _context = context;
            this._emailOptions = emailOptions.Value;
        }

        public async Task SendEmailAsync(List<string> toEmailAddresses, String subject,
            PublicEnums.EmailTemplateList emailTemplate, Dictionary<string, PropertyMetaData> variableValues,
            ClaimsPrincipal user, List<string> ccEmailAddresses = null, List<EmailAttachment> attachments = null,
            bool receiveEmail = true, bool requestDeliveryReceipt = false, bool requestReadReceipt = false, string replyToEmailAddress = "",
            string replyToName = "")
        {
            try
            {
                if (_emailOptions.EmailEnabled)
                {
                    var emailMessage = new MimeMessage();

                    emailMessage.From.Add(new MailboxAddress(_emailOptions.FromName, _emailOptions.FromAddress));

                    foreach (var toEmailAddress in toEmailAddresses)
                    {
                        emailMessage.To.Add(new MailboxAddress("", toEmailAddress));
                    }
                    if (ccEmailAddresses != null)
                    {
                        foreach (var ccEmailAddress in ccEmailAddresses)
                        {
                            emailMessage.Cc.Add(new MailboxAddress("", ccEmailAddress));
                        }
                    }
                    if(!string.IsNullOrEmpty(replyToEmailAddress) && !string.IsNullOrEmpty(replyToName))
                    {
                        emailMessage.ReplyTo.Add(new MailboxAddress(replyToName, replyToEmailAddress));
                    }

                    //Process email template
                    string htmlMessage = await ProcessEmailTemplate(emailTemplate, variableValues);
                    string smsMessage = await ProcessSMSTemplate(emailTemplate, variableValues);

                    emailMessage.Subject = subject;

                    //Build body
                    var builder = new BodyBuilder();
                    builder.HtmlBody = htmlMessage;
                    EmailBody = htmlMessage;
                    SMSBody = smsMessage;

                    //Add attachments
                    if (attachments != null && attachments.Count > 0)
                    {
                        foreach (var item in attachments)
                        {
                            builder.Attachments.Add(item.AttachmentName, item.AttachmentData, ContentType.Parse(item.ContentType));
                        }
                    }

                    emailMessage.Body = builder.ToMessageBody();

                    if(requestReadReceipt)
                    {
                        if (!string.IsNullOrEmpty(replyToEmailAddress) && !string.IsNullOrEmpty(replyToName))
                        {
                            emailMessage.Headers[HeaderId.DispositionNotificationTo] = new MailboxAddress(replyToName, replyToEmailAddress).ToString(true);
                        }
                        else
                        {
                            emailMessage.Headers[HeaderId.DispositionNotificationTo] = new MailboxAddress(_emailOptions.FromName, _emailOptions.FromAddress).ToString(true);
                        }
                    }

                    Message = emailMessage;

                    if (receiveEmail)
                    {
                        using (var client = new DSNSmtpClient())
                        {
                            client.RequestDeliveryReceipt = requestDeliveryReceipt;
                            client.LocalDomain = _emailOptions.LocalDomain;

                            await client.ConnectAsync(_emailOptions.MailServerAddress, Convert.ToInt32(_emailOptions.MailServerPort), SecureSocketOptions.Auto);

                            if (_emailOptions.RequireLogin)
                            {
                                await client.AuthenticateAsync(new NetworkCredential(_emailOptions.Username, _emailOptions.UserPassword));
                            }
                            client.DeliveryStatusNotificationType = DeliveryStatusNotificationType.Full;

                            client.MessageSent += (object sender, MessageSentEventArgs e) => {
                                Response = e.Response;
                            };
                            

                            await client.SendAsync(emailMessage);
                            await client.DisconnectAsync(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionResponse = ex;
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Helpers.EmailServiceFactory.EmailService.SendEmailAsync", ex.Message, user, ex);
            }
        }

        public async Task<string> ProcessEmailTemplate(PublicEnums.EmailTemplateList emailTemplate, Dictionary<string, PropertyMetaData> variableValues)
        {
            //Get email template
            string templateText = _context.EmailTemplates.Where(x => x.EventCode == emailTemplate.ToString()).First().TemplateBody;

            if (!string.IsNullOrEmpty(templateText))
            {
                return TemplateParser.Render(templateText, variableValues, Placeholder.Bracket);
            }
            else
            {
                return "";
            }
        }

        public async Task<string> ProcessSMSTemplate(PublicEnums.EmailTemplateList emailTemplate, Dictionary<string, PropertyMetaData> variableValues)
        {
            //Get email template
            string templateText = _context.EmailTemplates.Where(x => x.EventCode == emailTemplate.ToString()).First().SMSTemplateBody;

            if (!string.IsNullOrEmpty(templateText))
            {
                return TemplateParser.Render(templateText, variableValues, Placeholder.Bracket);
            }
            else
            {
                return "";
            }
        }
    }

    public class EmailAttachment
    {
        public string AttachmentName { get; set; }
        public byte[] AttachmentData { get; set; }
        public string ContentType { get; set; }
    }

    public class DSNSmtpClient : SmtpClient
    {
        public bool RequestDeliveryReceipt { get; set; }

        protected override DeliveryStatusNotification? GetDeliveryStatusNotifications(MimeMessage message, MailboxAddress mailbox)
        {
            if (RequestDeliveryReceipt)
            {
                return DeliveryStatusNotification.Failure |
                       DeliveryStatusNotification.Success;
            }
            return null;
        }
    }
}
