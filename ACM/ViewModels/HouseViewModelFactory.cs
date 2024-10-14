using ACM.Helpers.EmailServiceFactory;
using ACM.Models.SystemModelFactory;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Models.HouseModelFactory;
using System.ComponentModel.DataAnnotations;
namespace ACM.ViewModels.HouseViewModelFactory
{
    public class HousesViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;
        internal JwtIssuerOptions _jwtOptions;

        internal string errorMessage = "";

        public Guid HouseID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
		public string HouseImage { get; set; }
		public string Address { get; set; }
		public string Location { get; set; }
        public Guid? UserID { get; set; }
        public List<HouseImageListData> Images { get; set; }

		internal async Task PopulateDetails()
        {
            if (HouseID != Guid.Empty)
            {
                var item = _context.Houses.Where(x => x.HouseID == HouseID).FirstOrDefault();
                if (item != null)
                {
                    Description = item.Description;
                    Address = item.Address;
                    Location = item.Location;
                    HouseImage = item.HouseImage;
                }

                var list = (from t in _context.HouseImages
                            where t.HouseID == HouseID
                            select new HouseImageListData
                            {
                                HouseImage = t.Image
                            });
                Images = list.ToList();
			}
        }

        internal async Task<Guid> Save()
        {
            
            //UserHelperFunctions userHelper = new UserHelperFunctions()
            //{
            //    _context = _context,
            //    _securityOptions = _securityOptions,
            //    _user = _user
            //};
            //userHelper.Populate();
            bool isNew = false;
            bool isValid = true;
            errorMessage = "";
            //Validate inputs
            if (isValid)
            {
                var house = _context.Houses.Where(x => x.HouseID == HouseID).FirstOrDefault();
                if (house == null)
                {
					house = new House();
                    isNew = true;
					house.HouseID = Guid.NewGuid();
					house.CreatedDateTime = DateTime.Now;
				}
				house.Description = Description;
                house.HouseImage = HouseImage;
                house.UserID = UserID;
                house.Location = Location;
                house.Address = Address;
                house.EditDateTime = DateTime.Now;


                if (isNew)
                {
                    _context.Add(house);
                }
                else
                {
                    _context.Update(house);
                }

                
                foreach (var img in Images)
                {
					var image = new HouseImage();
                    image.HouseImageID = Guid.NewGuid();
                    image.HouseID = house.HouseID;
                    image.Image = img.ToString();

                    _context.HouseImages.Add(image);
				}

                await _context.SaveChangesAsync();
                HouseID = house.HouseID;
            }
            return HouseID;
        }
        internal async Task<bool> Remove()
		{
			bool returnValue = false;
			//UserHelperFunctions userHelper = new UserHelperFunctions()
   //         {
   //             _context = _context,
   //             _securityOptions = _securityOptions,
   //             _user = _user
   //         };
   //         userHelper.Populate();
            var Houses = _context.Houses.FirstOrDefault(x => x.HouseID == HouseID);
            if (Houses != null)
            {
                _context.Houses.Remove(Houses);
                await _context.SaveChangesAsync();
				returnValue = true;
			}
			return returnValue;
		}
    }
    public class HousesListViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
		internal ClaimsPrincipal _user;
        public string SearchValue { get; set; }
        public PaginationViewModel Pagination { get; set; }
        public List<HouseListViewModelData> HousesList { get; set; }
        internal async Task PopulateLists()
        {
            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }
            var list = (from t in _context.Houses
                        where (!string.IsNullOrEmpty(SearchValue) && (t.Description.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                        orderby t.Description
                        select new HouseListViewModelData
                        {
                            HouseID = t.HouseID,
                            Description = t.Description,
                            Address = t.Address,
                            Location = t.Location,
                            HouseImage = t.HouseImage
                        });

            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            HousesList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }
    }
    public class HouseListViewModelData
    {
        public Guid HouseID { get; set; }
        public string Description { get; set; }
		public string HouseImage { get; set; }
		public string Address { get; set; }
		public string Location { get; set; }
		public List<HouseImageListData> Images { get; set; }
	}
	public class HouseImageListData
	{
		public string HouseImage { get; set; }
	}
}
