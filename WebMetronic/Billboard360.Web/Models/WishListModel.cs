using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.Web.Models
{
	public class DeleteFromWishListInputModel
	{
		public Guid WishListID { get; set; }
	}

	public class DeleteFromWishListOutputModel
	{
		public Guid WishListID { get; set; }
	}

	public class DeleteFromWishListResponseModel : BaseResponseModel
	{
		public DeleteFromWishListOutputModel data { get; set; }
	}

	public class AddToWishListInputModel
	{
		public Guid UserID { get; set; }
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
	}

	public class AddToWishListOutputModel
	{
		public Guid SiteID { get; set; }
	}

	public class AddToWishListResponseModel : BaseResponseModel
	{
		public AddToWishListOutputModel data { get; set; }
	}

	public class WishListInputModel : BaseModel
	{
		public Guid UserID { get; set; }
	}

	public class WishListOutputModel
	{
		public Guid WishlistID { get; set; }
		public Guid SiteID { get; set; }
		public string NoBillboard { get; set; }
		public string Pulau { get; set; }
		public string Kota { get; set; }
		public string Provinsi { get; set; }
		public string Cabang { get; set; }
		public string Ukuran { get; set; }
		public string HorV { get; set; }
		public string BillboardType { get; set; }
		public string Longitude { get; set; }
		public string Latitude { get; set; }
		public double Harga { get; set; }
		public double RateScoreAverage { get; set; }
		public List<string> Image { get; set; }
	}

	public class WishListResponseModel : BaseResponseModel
	{
		public List<WishListOutputModel> data { get; set; }
	}

	public class NotificationWishListInput
	{
		public Guid UserID { get; set; }
	}

	public class NotificationWishListOutput : WishListOutputModel
	{

	}

	public class NotificationWishListResponseModel : BaseResponseModel
	{
		public List<NotificationWishListOutput> data { get; set; }
	}
}
