using ACM.Models.AccountDataModelFactory;
using ACM.Models.ACMDataModelFactory;
using ACM.Models.FormDataModelFactory;
using ACM.Models.SystemModelFactory;

namespace ACM.Models
{
    public static class DbInitializer
    {
        public static void Initialize(AppDBContext context, SecurityOptions securityOptions)
        {
            AddUserRoles(context, securityOptions);
            AddInitialUserAccounts(context, securityOptions);
            AddSystemConfiguration(context, securityOptions);
            AddEmailTemplates(context, securityOptions);
            AddTemporaryTokensType(context, securityOptions);

        }

        private static void AddEmailTemplates(AppDBContext context, SecurityOptions securityOptions)
        {
            string emailStyleSheet = @"<style>
/* -------------------------------------
    GLOBAL
    A very basic CSS reset
------------------------------------- */
* {
    margin: 0;
    font-family: ""Helvetica Neue"", Helvetica, Arial, sans-serif;
    box-sizing: border-box;
    font-size: 14px;
}

img {
    max-width: 100%;
}

body {
    -webkit-font-smoothing: antialiased;
    -webkit-text-size-adjust: none;
    width: 100% !important;
    height: 100%;
    line-height: 1.6em;
    /* 1.6em * 14px = 22.4px, use px to get airier line-height also in Thunderbird, and Yahoo!, Outlook.com, AOL webmail clients */
    /*line-height: 22px;*/
}

/* Let's make sure all tables have defaults */
table td {
    vertical-align: top;
}

/* -------------------------------------
    BODY & CONTAINER
------------------------------------- */
body {
    background-color: #fff;
    color: #6c7b88
}

.body-wrap {
    background-color: #fff;
    width: 100%;
}

.container {
    display: block !important;
    max-width: 600px !important;
    margin: 0 auto !important;
    /* makes it centered */
    clear: both !important;
}

.content {
    max-width: 600px;
    margin: 0 auto;
    display: block;
    padding: 20px;
}

/* -------------------------------------
    HEADER, FOOTER, MAIN
------------------------------------- */
.main {
    background-color: #F5F5F5;
    border-bottom: 2px solid #d7d7d7;
}

.content-wrap {
    padding: 20px;
}

.content-block {
    padding: 0 0 20px;
}

.header {
    width: 100%;
    margin-bottom: 20px;
}

.footer {
    width: 100%;
    clear: both;
    color: #999;
}

    .footer p, .footer a, .footer td {
        color: #999;
        font-size: 12px;
    }

/* -------------------------------------
    TYPOGRAPHY
------------------------------------- */
h1, h2, h3 {
    font-family: ""Helvetica Neue"", Helvetica, Arial, ""Lucida Grande"", sans-serif;
    color: #1a2c3f;
    margin: 30px 0 0;
    line-height: 1.2em;
    font-weight: 400;
}

h1 {
    font-size: 32px;
    font-weight: 500;
    /* 1.2em * 32px = 38.4px, use px to get airier line-height also in Thunderbird, and Yahoo!, Outlook.com, AOL webmail clients */
    /*line-height: 38px;*/
}

h2 {
    font-size: 24px;
    font-weight: 600;
    /* 1.2em * 24px = 28.8px, use px to get airier line-height also in Thunderbird, and Yahoo!, Outlook.com, AOL webmail clients */
    /*line-height: 29px;*/
}

h3 {
    font-size: 18px;
    /* 1.2em * 18px = 21.6px, use px to get airier line-height also in Thunderbird, and Yahoo!, Outlook.com, AOL webmail clients */
    /*line-height: 22px;*/
}

h4 {
    font-size: 14px;
    font-weight: 600;
}

p, ul, ol {
    margin-bottom: 10px;
    font-weight: normal;
}

    p li, ul li, ol li {
        margin-left: 5px;
        list-style-position: inside;
    }

/* -------------------------------------
    LINKS & BUTTONS
------------------------------------- */
a {
    color: #2E8B80;
    text-decoration: underline;
}

.btn-primary {
    text-decoration: none;
    color: #FFF;
    background-color: #155787;
    border: #104368;
    border-width: 10px 20px;
    line-height: 2em;
    /* 2em * 14px = 28px, use px to get airier line-height also in Thunderbird, and Yahoo!, Outlook.com, AOL webmail clients */
    /*line-height: 28px;*/
    font-weight: bold;
    text-align: center;
    cursor: pointer;
    display: inline-block;
    text-transform: capitalize;
    border-radius: 4px;
    padding: 8px 8px 8px 8px;
}



/* -------------------------------------
    OTHER STYLES THAT MIGHT BE USEFUL
------------------------------------- */
.last {
    margin-bottom: 0;
}

.first {
    margin-top: 0;
}

.aligncenter {
    text-align: center;
}

.alignright {
    text-align: right;
}

.alignleft {
    text-align: left;
}

.clear {
    clear: both;
}

/* -------------------------------------
    ALERTS
    Change the class depending on warning email, good email or bad email
------------------------------------- */
.alert {
    font-size: 16px;
    color: #fff;
    font-weight: 500;
    padding: 20px;
    text-align: center;
}

    .alert a {
        color: #fff;
        text-decoration: none;
        font-weight: 500;
        font-size: 16px;
    }

    .alert.alert-warning {
        background-color: #FFA726;
    }

    .alert.alert-bad {
        background-color: #ef5350;
    }

    .alert.alert-good {
        background-color: #8BC34A;
    }

/* -------------------------------------
    INVOICE
    Styles for the billing table
------------------------------------- */
.invoice {
    margin: 25px auto;
    text-align: left;
    width: 100%;
}

    .invoice td {
        padding: 5px 0;
    }

    .invoice .invoice-items {
        width: 100%;
    }

        .invoice .invoice-items td {
            border-top: #eee 1px solid;
        }

        .invoice .invoice-items .total td {
            border-top: 2px solid #6c7b88;
            font-size: 18px;
        }

.aligncenter {
    text-align: center;
}
/* -------------------------------------
    RESPONSIVE AND MOBILE FRIENDLY STYLES
------------------------------------- */
@media only screen and (max-width: 640px) {
    body {
        padding: 0 !important;
    }

    h1, h2, h3, h4 {
        font-weight: 800 !important;
        margin: 20px 0 5px !important;
    }

    h1 {
        font-size: 22px !important;
    }

    h2 {
        font-size: 18px !important;
    }

    h3 {
        font-size: 16px !important;
    }

    .container {
        padding: 0 !important;
        width: 100% !important;
    }

    .content {
        padding: 0 !important;
    }

    .content-wrap {
        padding: 10px !important;
    }

    .invoice {
        width: 100% !important;
    }
}

/*# sourceMappingURL=styles.css.map */
</style>";
            string headerLogo = @"<td><img src=""[HostUrl]/EmailTemplate/networkLogo.jpeg"" style=""width:200px;""/></td>";
            string footer = @"<div class=""footer"">
                        <table width=""100%"">
                            <tr>
                                <td class=""aligncenter"">
                                    <h2>We’d love to hear from you!</h2>
                                </td>
                            </tr>
                            <tr>
                                <td class=""aligncenter"">Copyright © 2022 ACM. All Rights Reserved.</td>
                            </tr>
                            <tr>
                                <td class=""aligncenter""><a href=""https://ACM.africa/"">ACM.co.za</a> | <a href=""mailto:ACM@technodezi.co.za"">ACM@tirisan.co.za</a></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>";
            if (context.EmailTemplates.Any(t => t.EventCode == PublicEnums.EmailTemplateList.NTF_REGISTRATION_WELCOME_CUSTOM.ToString()) == false)
            {
                EmailTemplate template = new EmailTemplate()
                {
                    Description = "Registration welcome email",
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    EventCode = PublicEnums.EmailTemplateList.NTF_REGISTRATION_WELCOME_CUSTOM.ToString(),
                    EmailTemplateID = Guid.NewGuid(),
                    TemplateBodyTags = "[DisplayName],[Username],[Password],[HostUrl]",
                    TemplateBody = @"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Welcome to ACM</title>
</head>

<body itemscope=itemscope itemtype=""http://schema.org/EmailMessage"">
" + emailStyleSheet + @"
    <table class=""body-wrap"">
        <tr>
            <td></td>
            <td class=""container"" width=""600"">
                <div class=""content"">
                    <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope=itemscope itemtype=""http://schema.org/ConfirmAction"">
                        <tr>
                            <td class=""content-wrap"">
                                <meta itemprop=""name"" content=""Confirm Email"" />
                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        " + headerLogo + @"
                                    </tr>
                                     <tr>
                                        <td class=""content-block"">
                                            <h2>Welcome to ACM</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Welcome to ACM [DisplayName]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            This email serves as an indication that you have been granted access to / registered on the ACM system.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            You can now log on to the ACM system using your username <b>[Username]</b> and your temporary password as indicated below.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Your temporary password is: <b>[Password]</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Please log in then navigate to Profile > Change Password, to change your temporary password.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block aligncenter"" itemprop=""handler"" itemscope=itemscope itemtype=""http://schema.org/HttpActionHandler"">
                                            <a href=""[HostUrl]"" class=""btn-primary"" itemprop=""url"">Open ACM system</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                     " + footer + @"
                </div>
            </td>
            <td></td>
        </tr>
    </table>

</body>
</html>
                    "
                };

                context.EmailTemplates.Add(template);
                context.SaveChanges();
            }

            if (context.EmailTemplates.Any(t => t.EventCode == PublicEnums.EmailTemplateList.NTF_PASSWORD_RESET_LINK.ToString()) == false)
            {
                EmailTemplate template = new EmailTemplate()
                {
                    Description = "Password reset link email",
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    EventCode = PublicEnums.EmailTemplateList.NTF_PASSWORD_RESET_LINK.ToString(),
                    EmailTemplateID = Guid.NewGuid(),
                    TemplateBodyTags = "[DisplayName],[HostUrl],[Link]",
                    TemplateBody = @"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Password reset link</title>
</head>

<body itemscope=itemscope itemtype=""http://schema.org/EmailMessage"">
" + emailStyleSheet + @"
    <table class=""body-wrap"">
        <tr>
            <td></td>
            <td class=""container"" width=""600"">
                <div class=""content"">
                    <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope=itemscope itemtype=""http://schema.org/ConfirmAction"">
                        <tr>
                            <td class=""content-wrap"">
                                <meta itemprop=""name"" content=""Password reset"" />
                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        " + headerLogo + @"
                                    </tr>
                                     <tr>
                                        <td class=""content-block"">
                                            <h2>Reset Your Password</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Dear [DisplayName]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            You have requested a password reset for ACM. Please click the Reset Password button to reset your password.<br />
                                            If you did not request a password reset, please ignore this email.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block aligncenter"" itemprop=""handler"" itemscope=itemscope itemtype=""http://schema.org/HttpActionHandler"">
                                            <a href=""[Link]"" class=""btn-primary"" itemprop=""url"">Reset Password</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    " + footer + @"
                </div>
            </td>
            <td></td>
        </tr>
    </table>

</body>
</html>
                    "
                };

                context.EmailTemplates.Add(template);
                context.SaveChanges();
            }

            if (context.EmailTemplates.Any(t => t.EventCode == PublicEnums.EmailTemplateList.NTF_PASSWORD_CHANGED.ToString()) == false)
            {
                EmailTemplate template = new EmailTemplate()
                {
                    Description = "Password Changed email",
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    EventCode = PublicEnums.EmailTemplateList.NTF_PASSWORD_CHANGED.ToString(),
                    EmailTemplateID = Guid.NewGuid(),
                    TemplateBodyTags = "[DisplayName],[HostUrl]",
                    TemplateBody = @"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Password Changed</title>
</head>

<body itemscope=itemscope itemtype=""http://schema.org/EmailMessage"">
" + emailStyleSheet + @"
    <table class=""body-wrap"">
        <tr>
            <td></td>
            <td class=""container"" width=""600"">
                <div class=""content"">
                    <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope=itemscope itemtype=""http://schema.org/ConfirmAction"">
                        <tr>
                            <td class=""content-wrap"">
                                <meta itemprop=""name"" content=""Password reset"" />
                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        " + headerLogo + @"
                                    </tr>
                                     <tr>
                                        <td class=""content-block"">
                                            <h2>Password Changed</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Dear [DisplayName]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            This emails is to notify you that your password on the ACM system have been changed. Should this be in error please contact your Administrator as soon as possible.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                   " + footer + @"
                </div>
            </td>
            <td></td>
        </tr>
    </table>

</body>
</html>
                    "
                };

                context.EmailTemplates.Add(template);
                context.SaveChanges();
            }

            if (context.EmailTemplates.Any(t => t.EventCode == PublicEnums.EmailTemplateList.NTF_CONTACT_US.ToString()) == false)
            {
                EmailTemplate template = new EmailTemplate()
                {
                    Description = "Contact Us",
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    EventCode = PublicEnums.EmailTemplateList.NTF_CONTACT_US.ToString(),
                    EmailTemplateID = Guid.NewGuid(),
                    TemplateBodyTags = "[Name],[EmailAddress],[ContactNumber],[Message],[HostUrl]",
                    TemplateBody = @"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Contact Us</title>
</head>

<body itemscope=itemscope itemtype=""http://schema.org/EmailMessage"">
" + emailStyleSheet + @"
    <table class=""body-wrap"">
        <tr>
            <td></td>
            <td class=""container"" width=""600"">
                <div class=""content"">
                    <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope=itemscope itemtype=""http://schema.org/ConfirmAction"">
                        <tr>
                            <td class=""content-wrap"">
                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        " + headerLogo + @"
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            <h2>Contact Us form</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Dear Administrator
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                        Someone submitted a contact us request 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            <b>Name</b>: [Name]<br />
                                            <b>Email Address</b>: [EmailAddress]<br />
                                            <b>Contact Number</b>: [ContactNumber]<br />
                                            <b>Message</b>: <pre>[Message]</pre><br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    " + footer + @"
                </div>
            </td>
            <td></td>
        </tr>
    </table>

</body>
</html>
                    "
                };

                context.EmailTemplates.Add(template);
                context.SaveChanges();
            }

            if (context.EmailTemplates.Any(t => t.EventCode == PublicEnums.EmailTemplateList.NTF_EMAIL_VERIFICATION_LINK.ToString()) == false)
            {
                EmailTemplate template = new EmailTemplate()
                {
                    Description = "Email verification link email",
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    EventCode = PublicEnums.EmailTemplateList.NTF_EMAIL_VERIFICATION_LINK.ToString(),
                    EmailTemplateID = Guid.NewGuid(),
                    TemplateBodyTags = "[DisplayName],[HostUrl],[Link]",
                    TemplateBody = @"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Email verification link</title>
</head>

<body itemscope=itemscope itemtype=""http://schema.org/EmailMessage"">
" + emailStyleSheet + @"
    <table class=""body-wrap"">
        <tr>
            <td></td>
            <td class=""container"" width=""600"">
                <div class=""content"">
                    <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope=itemscope itemtype=""http://schema.org/ConfirmAction"">
                        <tr>
                            <td class=""content-wrap"">
                                <meta itemprop=""name"" content=""Password reset"" />
                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        " + headerLogo + @"
                                    </tr>
                                     <tr>
                                        <td class=""content-block"">
                                            <h2>Verify your email address</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Dear [DisplayName]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            In order to ensure that you registered with the correct email address we need to verify that you received this email.<br />
                                            Please click Verify Email below to verify your email address.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block aligncenter"" itemprop=""handler"" itemscope=itemscope itemtype=""http://schema.org/HttpActionHandler"">
                                            <a href=""[Link]"" class=""btn-primary"" itemprop=""url"">Verify Email</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    " + footer + @"
                </div>
            </td>
            <td></td>
        </tr>
    </table>

</body>
</html>
                    "
                };

                context.EmailTemplates.Add(template);
                context.SaveChanges();
            }

            if (context.EmailTemplates.Any(t => t.EventCode == PublicEnums.EmailTemplateList.NTF_UPCOMING_EVENT_REMINDER.ToString()) == false)
            {
                EmailTemplate template = new EmailTemplate()
                {
                    Description = "Upcoming Event Reminder",
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    EventCode = PublicEnums.EmailTemplateList.NTF_UPCOMING_EVENT_REMINDER.ToString(),
                    EmailTemplateID = Guid.NewGuid(),
                    TemplateBodyTags = "[Name],[Description],[IsAllDay],[StartTime],[HostUrl]",
                    TemplateBody = @"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Upcoming Event Reminder</title>
</head>

<body itemscope=itemscope itemtype=""http://schema.org/EmailMessage"">
" + emailStyleSheet + @"
    <table class=""body-wrap"">
        <tr>
            <td></td>
            <td class=""container"" width=""600"">
                <div class=""content"">
                    <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope=itemscope itemtype=""http://schema.org/ConfirmAction"">
                        <tr>
                            <td class=""content-wrap"">
                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        " + headerLogo + @"
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            <h2>Upcoming event reminder</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Dear [Name]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                        <b>[Description]</b> is starting soon. Please see your online calendar for details.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            <b>Description</b>: [Description]<br />
                                            <b>Start Time</b>: [StartTime]<br />
                                            <b>All Day Event</b>: [IsAllDay]<br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    " + footer + @"
                </div>
            </td>
            <td></td>
        </tr>
    </table>

</body>
</html>
                    "
                };

                context.EmailTemplates.Add(template);
                context.SaveChanges();
            }

            if (context.EmailTemplates.Any(t => t.EventCode == PublicEnums.EmailTemplateList.NTF_REGISTRATION_APPROVAL_REQUIRED.ToString()) == false)
            {
                EmailTemplate template = new EmailTemplate()
                {
                    Description = "Registration approval required",
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    EventCode = PublicEnums.EmailTemplateList.NTF_REGISTRATION_APPROVAL_REQUIRED.ToString(),
                    EmailTemplateID = Guid.NewGuid(),
                    TemplateBodyTags = "[DisplayName],[Username],[HostUrl]",
                    TemplateBody = @"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Registration approval required</title>
</head>

<body itemscope=itemscope itemtype=""http://schema.org/EmailMessage"">
" + emailStyleSheet + @"
    <table class=""body-wrap"">
        <tr>
            <td></td>
            <td class=""container"" width=""600"">
                <div class=""content"">
                    <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope=itemscope itemtype=""http://schema.org/ConfirmAction"">
                        <tr>
                            <td class=""content-wrap"">
                                <meta itemprop=""name"" content=""Confirm Email"" />
                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        " + headerLogo + @"
                                    </tr>
                                     <tr>
                                        <td class=""content-block"">
                                            <h2>Registration approval required</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Dear [DisplayName]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            A new user with username [Username] has registered on the ACM system that requires approval.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Please log on to the system to approve the registration.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block aligncenter"" itemprop=""handler"" itemscope=itemscope itemtype=""http://schema.org/HttpActionHandler"">
                                            <a href=""[HostUrl]"" class=""btn-primary"" itemprop=""url"">Open ACM system</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                     " + footer + @"
                </div>
            </td>
            <td></td>
        </tr>
    </table>

</body>
</html>
                    "
                };

                context.EmailTemplates.Add(template);
                context.SaveChanges();
            }

            if (context.EmailTemplates.Any(t => t.EventCode == PublicEnums.EmailTemplateList.NTF_ACCOUNT_APPROVED.ToString()) == false)
            {
                EmailTemplate template = new EmailTemplate()
                {
                    Description = "Account Approved",
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    EventCode = PublicEnums.EmailTemplateList.NTF_ACCOUNT_APPROVED.ToString(),
                    EmailTemplateID = Guid.NewGuid(),
                    TemplateBodyTags = "[DisplayName],[HostUrl]",
                    TemplateBody = @"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Account Approved</title>
</head>

<body itemscope=itemscope itemtype=""http://schema.org/EmailMessage"">
" + emailStyleSheet + @"
    <table class=""body-wrap"">
        <tr>
            <td></td>
            <td class=""container"" width=""600"">
                <div class=""content"">
                    <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope=itemscope itemtype=""http://schema.org/ConfirmAction"">
                        <tr>
                            <td class=""content-wrap"">
                                <meta itemprop=""name"" content=""Password reset"" />
                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        " + headerLogo + @"
                                    </tr>
                                     <tr>
                                        <td class=""content-block"">
                                            <h2>Account Approved</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Dear [DisplayName]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Your ACM account have been approved and you may now start using it.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block aligncenter"" itemprop=""handler"" itemscope=itemscope itemtype=""http://schema.org/HttpActionHandler"">
                                            <a href=""[HostUrl]"" class=""btn-primary"" itemprop=""url"">Open ACM system</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                   " + footer + @"
                </div>
            </td>
            <td></td>
        </tr>
    </table>

</body>
</html>
                    "
                };

                context.EmailTemplates.Add(template);
                context.SaveChanges();
            }

            if (context.EmailTemplates.Any(t => t.EventCode == PublicEnums.EmailTemplateList.NTF_FORM_SUBMISSION_QUESTION_NOTIFICATION.ToString()) == false)
            {
                EmailTemplate template = new EmailTemplate()
                {
                    Description = "Form Submission - Question notification",
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    EventCode = PublicEnums.EmailTemplateList.NTF_FORM_SUBMISSION_QUESTION_NOTIFICATION.ToString(),
                    EmailTemplateID = Guid.NewGuid(),
                    TemplateBodyTags = "[FormDefinitionName],[QuestionName],[AnswerName],[HostUrl],[FormLink],[CompletionDate],[Remarks]",
                    TemplateBody = @"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Form Submission - Question notification</title>
</head>

<body itemscope=itemscope itemtype=""http://schema.org/EmailMessage"">
" + emailStyleSheet + @"
    <table class=""body-wrap"">
        <tr>
            <td></td>
            <td class=""container"" width=""600"">
                <div class=""content"">
                    <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope=itemscope itemtype=""http://schema.org/ConfirmAction"">
                        <tr>
                            <td class=""content-wrap"">
                                <meta itemprop=""name"" content=""Confirm Email"" />
                                <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        " + headerLogo + @"
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            <b>Dear User</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            A user have answered '[AnswerName]' to the question '[QuestionName]' on Form '[FormDefinitionName]'.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            Date of completion: [CompletionDate]<br />
                                            Remarks: <pre>[Remarks]</pre>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""content-block"">
                                            <a href=""[FormLink]"">Please click here to view the Form and the answers
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                     " + footer + @"
                </div>
            </td>
            <td></td>
        </tr>
    </table>

</body>
</html>
                    "
                };

                context.EmailTemplates.Add(template);
                context.SaveChanges();
            }
        }

        private static void AddUserRoles(AppDBContext context, SecurityOptions securityOptions)
        {
            bool itemAdded = false;

            if (context.UserRoles.Any(x => x.EventCode == PublicEnums.UserRoleList.ROLE_ADMINISTRATOR) == false)
            {
                context.UserRoles.Add(new UserRole { Description = "Administrator", EventCode = PublicEnums.UserRoleList.ROLE_ADMINISTRATOR.ToString(), UserRoleID = Guid.NewGuid() });
                itemAdded = true;
            }

            if (context.UserRoles.Any(x => x.EventCode == PublicEnums.UserRoleList.ROLE_USER) == false)
            {
                context.UserRoles.Add(new UserRole { Description = "User", EventCode = PublicEnums.UserRoleList.ROLE_USER.ToString(), UserRoleID = Guid.NewGuid() });
                itemAdded = true;
            }

            if (context.UserRoles.Any(x => x.EventCode == PublicEnums.UserRoleList.ROLE_TECHNICIAN_USER) == false)
            {
                context.UserRoles.Add(new UserRole { Description = "Technician", EventCode = PublicEnums.UserRoleList.ROLE_TECHNICIAN_USER.ToString(), UserRoleID = Guid.NewGuid() });
                itemAdded = true;
            }

            if (context.UserRoles.Any(x => x.EventCode == PublicEnums.UserRoleList.ROLE_SALES_USER) == false)
            {
                context.UserRoles.Add(new UserRole { Description = "Sales User", EventCode = PublicEnums.UserRoleList.ROLE_SALES_USER.ToString(), UserRoleID = Guid.NewGuid() });
                itemAdded = true;
            }

            if (context.UserRoles.Any(x => x.EventCode == PublicEnums.UserRoleList.ROLE_VENDOR) == false)
            {
                context.UserRoles.Add(new UserRole { Description = "Vendor", EventCode = PublicEnums.UserRoleList.ROLE_VENDOR.ToString(), UserRoleID = Guid.NewGuid() });
                itemAdded = true;
            }

            if (itemAdded)
            {
                context.SaveChanges();
            }
        }

        private static void AddInitialUserAccounts(AppDBContext context, SecurityOptions securityOptions)
        {
            if (context.Users.Any() == false)
            {
                var password = "password";
                string hashedPassword = HashProvider.ComputeHash(password, HashProvider.HashAlgorithmList.SHA256, securityOptions.PasswordSalt);

                var users = new User[]
                {
                    new User
                    {
                        DisplayName = "Dezi Van Vuuren",
                        FirstName = "Dezi",
                        Surname = "Van Vuuren",
                        EmailAddress = "dezi@tirisan.co.za",
                        Password = hashedPassword,
                        UserID = Guid.NewGuid(),
                        CreatedDateTime = DateTime.UtcNow,
                        CreatedUserID = Guid.Empty,
                        EditDateTime = DateTime.UtcNow,
                        EditUserID = Guid.Empty,
                        IsEmailVerified = true,
                    }
                    
                };

                foreach (User s in users)
                {
                    context.Users.Add(s);

                    var adminRole = context.UserRoles.Where(x => x.EventCode == PublicEnums.UserRoleList.ROLE_ADMINISTRATOR).First();
                    LinkUserRole roleLink = new LinkUserRole()
                    {
                        LinkUserRoleID = Guid.NewGuid(),
                        UserID = s.UserID,
                        UserRoleID = adminRole.UserRoleID
                    };
                    context.LinkUserRole.Add(roleLink);
                }
                context.SaveChanges();
            }
        }

        private static void AddTemporaryTokensType(AppDBContext context, SecurityOptions securityOptions)
        {
            bool itemAdded = false;

            if (context.TemporaryTokensType.Any(x => x.EventCode == PublicEnums.TemporaryTokensTypeList.TYPE_FORGOT_PASSWORD.ToString()) == false)
            {
                context.TemporaryTokensType.Add(new TemporaryTokensType { Description = "Forgot Password", EventCode = PublicEnums.TemporaryTokensTypeList.TYPE_FORGOT_PASSWORD.ToString(), TemporaryTokensTypeID = Guid.NewGuid() });
                itemAdded = true;
            }
            if (context.TemporaryTokensType.Any(x => x.EventCode == PublicEnums.TemporaryTokensTypeList.TYPE_EMAIL_VERIFICATION.ToString()) == false)
            {
                context.TemporaryTokensType.Add(new TemporaryTokensType { Description = "Email Verification", EventCode = PublicEnums.TemporaryTokensTypeList.TYPE_EMAIL_VERIFICATION.ToString(), TemporaryTokensTypeID = Guid.NewGuid() });
                itemAdded = true;
            }

            if (itemAdded)
            {
                context.SaveChanges();
            }
        }

        private static void AddSystemConfiguration(AppDBContext context, SecurityOptions securityOptions)
        {
            bool itemAdded = false;

            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_LOGIN_TOKEN_VALID_MIN.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_LOGIN_TOKEN_VALID_MIN.ToString(),
                    Description = "Login token valid for days",
                    ConfigValue = "7",
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PASSEORD_RESETLINK_VALIDFOR_MIN.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_PASSEORD_RESETLINK_VALIDFOR_MIN.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "Password reset link valid for minutes",
                    ConfigValue = "30",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_LOGIN_RETRYLIMIT.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_LOGIN_RETRYLIMIT.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "Login retry limit",
                    ConfigValue = "3",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_CLEAN_APP_LOG_DAYS.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_CLEAN_APP_LOG_DAYS.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "Clean application log older than days",
                    ConfigValue = "90",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_DEFAULT_TIME_ZONE.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_DEFAULT_TIME_ZONE.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "Default Time Zone",
                    ConfigValue = "South Africa Standard Time",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }

            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_CONTACT_US.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_CONTACT_US.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "Contact US",
                    ConfigValue = "",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_TERMS_CONDITIONS.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_TERMS_CONDITIONS.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "Terms and Conditions",
                    ConfigValue = "",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_MERCHANTID.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_PAYFAST_MERCHANTID.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "PayFast Merchant ID",
                    ConfigValue = "",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_MERCHANTKEY.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_PAYFAST_MERCHANTKEY.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "PayFast Merchant Key",
                    ConfigValue = "",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_PASSPHRASE.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_PAYFAST_PASSPHRASE.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "PayFast Pass phrase",
                    ConfigValue = "",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_IS_TESTING.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_PAYFAST_IS_TESTING.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "PayFast Api Testing mode (Sandbox)",
                    ConfigValue = "true",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_PROCESSURL.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_PAYFAST_PROCESSURL.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "PayFast Process URL",
                    ConfigValue = "",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_VALIDATEURL.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_PAYFAST_VALIDATEURL.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "PayFast Validate URL",
                    ConfigValue = "",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }
            if (context.SystemConfiguration.Any(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_USE_PAYFAST_GATEWAY.ToString()) == false)
            {
                context.SystemConfiguration.Add(new SystemConfiguration()
                {
                    EventCode = PublicEnums.SystemConfigurationList.KEY_USE_PAYFAST_GATEWAY.ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = null,
                    EditDateTime = DateTime.UtcNow,
                    EditUserID = null,
                    Description = "Use PayFast payment gateway",
                    ConfigValue = "true",
                    SystemConfigurationID = Guid.NewGuid()
                });
                itemAdded = true;
            }

            if (itemAdded)
            {
                context.SaveChanges();
            }
        }



   }
}
