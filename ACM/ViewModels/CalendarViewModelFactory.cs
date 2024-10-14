using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using ACM.ViewModels.Services.SystemConfigServiceFactory;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace ACM.ViewModels.CalendarViewModelFactory
{
    public class CalendarViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;
        internal string _errorMessage;
        internal ISystemConfigService _systemConfig;
        internal IStringLocalizer<SessionStringLocalizer> _localizer;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? CalendarDateFrom { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? CalendarDateTo { get; set; }

        public List<CalendarViewModelEventType> EventTypeList { get; set; }
        public List<CalendarViewModelEvent> CalEventList { get; set; }

        public async Task PopulateModel(bool loadEvents = true)
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            if (CalendarDateFrom != null && CalendarDateFrom > DateTime.MinValue)
            {
                CalendarDateFrom = Convert.ToDateTime(CalendarDateFrom.Value.ToString("yyyy/MM/dd") + " 00:00");
            }
            else
            {
                CalendarDateFrom = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            }

            if (CalendarDateTo != null && CalendarDateTo > DateTime.MinValue)
            {
                CalendarDateTo = Convert.ToDateTime(CalendarDateTo.Value.ToString("yyyy/MM/dd") + " 23:59");
            }
            else
            {
                CalendarDateTo = CalendarDateFrom.Value.AddMonths(1).AddDays(-1);
            }

            EventTypeList = (from e in _context.CalendarEventTypes
                             where e.UserID == userHelper.loggedInUserID
                             orderby e.Description
                             select new CalendarViewModelEventType
                             {
                                 CalendarEventTypeID = e.CalendarEventTypeID,
                                 ColorCode = e.ColorCode,
                                 Description = e.Description,
                                 MetaFields = (from m in _context.CalendarEventTypeMetaField
                                               where m.CalendarEventTypeID == e.CalendarEventTypeID
                                               orderby m.Description
                                               select new CalendarViewModelEventTypeMeta
                                               {
                                                   CalendarEventTypeMetaFieldID = m.CalendarEventTypeMetaFieldID,
                                                   Description = m.Description,
                                                   IsRemoved = false
                                               }).ToList()
                             }).ToList();

            if (loadEvents)
            {
                CalEventList = (from c in _context.CalendarEvents.Include(x => x.CalendarEventType)
                                where c.UserID == userHelper.loggedInUserID
                                && c.StartTime >= CalendarDateFrom && c.StartTime <= CalendarDateTo
                                orderby c.StartTime, c.EndTime
                                select new CalendarViewModelEvent
                                {
                                    Url = c.Url,
                                    StartTime = c.StartTime.ToTimezoneFromUtc(_user).ToOffsetTimezone(_user),
                                    CalendarEventID = c.CalendarEventID,
                                    CalendarEventTypeID = c.CalendarEventTypeID,
                                    ColorCode = (c.CalendarEventType != null) ? c.CalendarEventType.ColorCode : c.ColorCode,
                                    Description = c.Description,
                                    EndTime = c.EndTime.ToTimezoneFromUtc(_user).ToOffsetTimezone(_user),
                                    IsAllDay = c.IsAllDay,
                                    AllowEdit = true,
                                    EnableReminder = c.EnableReminder,
                                    EditDateTime = c.EditDateTime.ToTimezoneFromUtc(_user),
                                    EditUserDisplayName = (c.EditUserID != null) ? _context.Users.First(x => x.UserID == c.EditUserID).DisplayName : "",
                                    TypeDescription = (c.CalendarEventType != null) ? c.CalendarEventType.Description : "Custom",
                                    MetaValues = (from t in _context.CalendarEventTypeMetaField
                                                  join m in _context.CalendarEventMetaFieldValues on new { c.CalendarEventID, t.CalendarEventTypeMetaFieldID } equals new { m.CalendarEventID, m.CalendarEventTypeMetaFieldID } into mI
                                                  from m in mI.DefaultIfEmpty()
                                                  where t.CalendarEventTypeID == c.CalendarEventTypeID
                                                  orderby t.Description
                                                  select new CalendarViewModelEventMetaValue
                                                  {
                                                      CalendarEventMetaFieldValueID = (m != null) ? m.CalendarEventMetaFieldValueID : Guid.Empty,
                                                      CalendarEventTypeMetaFieldID = t.CalendarEventTypeMetaFieldID,
                                                      MetaDescription = t.Description,
                                                      MetaValue = (m != null) ? m.MetaValue : ""
                                                  }).ToList()
                                }).ToList();
            }
            else
            {
                CalEventList = new List<CalendarViewModelEvent>();
            }
        }

        internal async Task<Guid> UpdateCalendarEventType(CalendarViewModelEventType EventTypeItem)
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            bool isAdd = false;

            var item = _context.CalendarEventTypes.FirstOrDefault(x => x.UserID == userHelper.loggedInUserID && x.CalendarEventTypeID == EventTypeItem.CalendarEventTypeID);
            if (item == null)
            {
                item = new Models.UserModelFactory.CalendarEventType()
                {
                    CalendarEventTypeID = Guid.NewGuid(),
                    UserID = userHelper.loggedInUserID,
                };
                isAdd = true;

                _context.CalendarEventTypes.Add(item);
            }

            item.ColorCode = EventTypeItem.ColorCode;
            item.Description = EventTypeItem.Description;

            if (EventTypeItem.MetaFields != null)
            {
                foreach (var remItem in EventTypeItem.MetaFields.Where(x => x.IsRemoved == true))
                {
                    if (_context.CalendarEventMetaFieldValues.Any(x => x.CalendarEventTypeMetaFieldID == remItem.CalendarEventTypeMetaFieldID) == false)
                    {
                        var metaItem = _context.CalendarEventTypeMetaField.FirstOrDefault(x => x.CalendarEventTypeMetaFieldID == remItem.CalendarEventTypeMetaFieldID);
                        if (metaItem != null)
                        {
                            _context.Remove(metaItem);
                        }
                    }
                }

                foreach (var meta in EventTypeItem.MetaFields.Where(x => x.IsRemoved == false))
                {
                    var metaItem = _context.CalendarEventTypeMetaField.FirstOrDefault(x => x.CalendarEventTypeMetaFieldID == meta.CalendarEventTypeMetaFieldID);
                    if (metaItem == null)
                    {
                        _context.CalendarEventTypeMetaField.Add(new Models.UserModelFactory.CalendarEventTypeMetaField()
                        {
                            CalendarEventTypeMetaFieldID = Guid.NewGuid(),
                            CalendarEventTypeID = item.CalendarEventTypeID,
                            Description = meta.Description
                        });
                    }
                    else
                    {
                        metaItem.Description = meta.Description;
                    }
                }
            }

            await _context.SaveChangesAsync();

            return item.CalendarEventTypeID;
        }

        internal async Task RemoveCalendarEventType(Guid calendarEventTypeID)
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            var item = _context.CalendarEventTypes.FirstOrDefault(x => x.UserID == userHelper.loggedInUserID && x.CalendarEventTypeID == calendarEventTypeID);
            if (item != null)
            {
                var metaFiledsRem = _context.CalendarEventTypeMetaField.Where(x => x.CalendarEventTypeID == item.CalendarEventTypeID).ToList();
                _context.RemoveRange(metaFiledsRem);

                _context.Remove(item);

                await _context.SaveChangesAsync();
            }
            else if (calendarEventTypeID != Guid.Empty)
            {
                _errorMessage = "Calendar event type not found";
            }
        }

        internal async Task<Guid> UpdateCalendarEvent(CalendarViewModelEvent calEvent)
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            bool isAdd = false;

            var item = _context.CalendarEvents.FirstOrDefault(x => x.UserID == userHelper.loggedInUserID && x.CalendarEventID == calEvent.CalendarEventID);
            if (item == null)
            {
                item = new Models.UserModelFactory.CalendarEvent()
                {
                    CalendarEventID = Guid.NewGuid(),
                    UserID = userHelper.loggedInUserID,
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedUserID = userHelper.loggedInUserID
                };
                isAdd = true;

                _context.CalendarEvents.Add(item);
            }

            item.CalendarEventTypeID = calEvent.CalendarEventTypeID;
            item.ColorCode = calEvent.ColorCode;
            item.Description = calEvent.Description;
            item.EndTime = (calEvent.IsAllDay == false && calEvent.EndTime != null) ? calEvent.EndTime.Value.DateTime.ToUTCTimezone(_user) : (DateTime?)null;
            item.IsAllDay = calEvent.IsAllDay;
            item.StartTime = calEvent.StartTime.DateTime.ToUTCTimezone(_user);
            item.Url = calEvent.Url;
            item.EditDateTime = DateTime.UtcNow;
            item.EditUserID = userHelper.loggedInUserID;
            item.EnableReminder = calEvent.EnableReminder;

            //Remove old meta fields
            var metaFiledsRem = _context.CalendarEventMetaFieldValues.Where(x => x.CalendarEventID == item.CalendarEventID).ToList();
            _context.RemoveRange(metaFiledsRem);

            if (calEvent.MetaValues != null && calEvent.MetaValues.Count > 0)
            {
                foreach (var meta in calEvent.MetaValues)
                {
                    _context.CalendarEventMetaFieldValues.Add(new Models.UserModelFactory.CalendarEventMetaFieldValue()
                    {
                        CalendarEventMetaFieldValueID = Guid.NewGuid(),
                        CalendarEventID = item.CalendarEventID,
                        CalendarEventTypeMetaFieldID = meta.CalendarEventTypeMetaFieldID,
                        MetaValue = meta.MetaValue
                    });
                }
            }

            await _context.SaveChangesAsync();

            return item.CalendarEventID;
        }

        internal async Task RemoveCalendarEvent(Guid calendarEventID)
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            var item = _context.CalendarEvents.FirstOrDefault(x => x.UserID == userHelper.loggedInUserID && x.CalendarEventID == calendarEventID);
            if (item != null)
            {
                var metaFiledsRem = _context.CalendarEventMetaFieldValues.Where(x => x.CalendarEventID == item.CalendarEventID).ToList();
                _context.RemoveRange(metaFiledsRem);

                _context.Remove(item);

                await _context.SaveChangesAsync();
            }
        }
    }

    public class CalendarViewModelEvent
    {
        public Guid CalendarEventID { get; set; }
        public Guid? CalendarEventTypeID { get; set; }
        public string Description { get; set; }
        public string ColorCode { get; set; }
        public string Url { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public bool IsAllDay { get; set; }
        public DateTime? EditDateTime { get; set; }
        public string EditUserDisplayName { get; set; }
        public bool EnableReminder { get; set; }

        public List<CalendarViewModelEventMetaValue> MetaValues { get; set; }
        public string TypeDescription { get; set; }
        public bool AllowEdit { get; set; }
    }

    public class CalendarViewModelEventMetaValue
    {
        public Guid CalendarEventMetaFieldValueID { get; set; }
        public Guid CalendarEventTypeMetaFieldID { get; set; }
        public string MetaDescription { get; set; }
        public string MetaValue { get; set; }
    }

    public class CalendarViewModelEventType
    {
        public Guid CalendarEventTypeID { get; set; }
        public string Description { get; set; }
        public string ColorCode { get; set; }

        public List<CalendarViewModelEventTypeMeta> MetaFields { get; set; }
    }

    public class CalendarViewModelEventTypeMeta
    {
        public Guid CalendarEventTypeMetaFieldID { get; set; }
        public string Description { get; set; }
        public bool IsRemoved { get; set; }
    }
}
