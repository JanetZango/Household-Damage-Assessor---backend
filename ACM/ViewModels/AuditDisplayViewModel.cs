using System.ComponentModel.DataAnnotations;

namespace ACM.ViewModels
{
    public class AuditDisplayViewModel
    {
        public Guid? CreatedUserID { get; set; }
        public string CreatedUserDisplayName { get; set; }
        public Guid? EditUserID { get; set; }
        public string EditUserDisplayName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime? CreatedDateTime { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime? EditDateTime { get; set; }
    }

    public class GPSLocationViewModel
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public double Accuracy { get; set; }
        public double Altitude { get; set; }
    }
}
