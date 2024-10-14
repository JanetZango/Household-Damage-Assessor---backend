using ACM.Models.AccountDataModelFactory;
using ACM.Models.Auditing.AuditAccountDataModelFactory;
using static ACM.Helpers.PublicEnums;

namespace ACM.Helpers
{
    public static class DataAuditExtentions
    {
        public static void Audit(this User auditItems, AppDBContext _context, DataAuditAction auditAction)
        {
            var nowDate = DateTime.UtcNow;

            if (auditAction == DataAuditAction.ACTION_UPDATE || auditAction == DataAuditAction.ACTION_DELETE)
            {
                //Get prev audit record
                var prevAuditList = _context.UserAudit.Where(x => x.UserID == auditItems.UserID && DateTime.UtcNow >= x.ValidFromDate && DateTime.UtcNow <= x.ValidToDate);

                //End audit record
                if (prevAuditList != null)
                {
                    foreach (var prevAudit in prevAuditList)
                    {
                        prevAudit.ValidToDate = nowDate;
                        _context.Update(prevAudit);
                    }
                }
            }

            if (auditAction == DataAuditAction.ACTION_ADD || auditAction == DataAuditAction.ACTION_UPDATE)
            {
                //Add initial audit record
                UserAudit audit = new UserAudit()
                {
                    UserAuditID = Guid.NewGuid(),
                    CreatedUserID = auditItems.CreatedUserID,
                    EditUserID = auditItems.EditUserID,
                    ValidFromDate = nowDate,
                    ValidToDate = DateTime.MaxValue,
                    CreatedDateTime = auditItems.CreatedDateTime,
                    EditDateTime = auditItems.EditDateTime,

                    DisplayName = auditItems.DisplayName,
                    EmailAddress = auditItems.EmailAddress,
                    FirstName = auditItems.FirstName,
                    IsRemoved = auditItems.IsRemoved,
                    IsSuspended = auditItems.IsSuspended,
                    LoginTries = auditItems.LoginTries,
                    Password = auditItems.Password,
                    Surname = auditItems.Surname,
                    Timezone = auditItems.Timezone,
                    UserID = auditItems.UserID,
                    AcceptTermsAndConditions = auditItems.AcceptTermsAndConditions,
                    CellphoneNumber = auditItems.CellphoneNumber,
                    CountryID = auditItems.CountryID,
                    IDNumber = auditItems.IDNumber,
                    LanguageCultureID = auditItems.LanguageCultureID,
                    Title = auditItems.Title,
                    IsEmailVerified = auditItems.IsEmailVerified
                };
                _context.Add(audit);
            }
        }

        public static void Audit(this LinkUserRole auditItems, AppDBContext _context, DataAuditAction auditAction)
        {
            var nowDate = DateTime.UtcNow;

            if (auditAction == DataAuditAction.ACTION_UPDATE || auditAction == DataAuditAction.ACTION_DELETE)
            {
                //Get prev audit record
                var prevAuditList = _context.LinkUserRoleAudit.Where(x => x.LinkUserRoleID == auditItems.LinkUserRoleID && DateTime.UtcNow >= x.ValidFromDate && DateTime.UtcNow <= x.ValidToDate);

                //End audit record
                if (prevAuditList != null)
                {
                    foreach (var prevAudit in prevAuditList)
                    {
                        prevAudit.ValidToDate = nowDate;
                        _context.Update(prevAudit);
                    }
                }
            }

            if (auditAction == DataAuditAction.ACTION_ADD || auditAction == DataAuditAction.ACTION_UPDATE)
            {
                //Add initial audit record
                LinkUserRoleAudit audit = new LinkUserRoleAudit()
                {
                    LinkUserRoleAuditID = Guid.NewGuid(),
                    CreatedUserID = auditItems.CreatedUserID,
                    EditUserID = auditItems.EditUserID,
                    ValidFromDate = nowDate,
                    ValidToDate = DateTime.MaxValue,
                    CreatedDateTime = auditItems.CreatedDateTime,
                    EditDateTime = auditItems.EditDateTime,

                    LinkUserRoleID = auditItems.LinkUserRoleID,
                    UserID = auditItems.UserID,
                    UserRoleID = auditItems.UserRoleID,
                };
                _context.Add(audit);
            }
        }
    }
}
