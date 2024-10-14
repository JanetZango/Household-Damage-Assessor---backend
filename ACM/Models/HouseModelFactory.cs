using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models.HouseModelFactory
{
	public class House
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid HouseID { get; set; }
		public string Description { get; set; }
		public string HouseImage { get; set; }
		public string Address { get; set; }
		public string Location { get; set; }
		public Guid? UserID { get; set; }
		public DateTime CreatedDateTime { get; set; }
		public DateTime EditDateTime { get; set; }
	}

	public class HouseImage
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid HouseImageID { get; set; }
		public Guid? HouseID { get; set; }
		public string Image { get; set; }
	}
}
