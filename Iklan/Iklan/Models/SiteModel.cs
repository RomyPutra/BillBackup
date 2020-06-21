using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iklan.Models
{
	public class AddtoCartSite
	{
		public Guid SiteID { get; set; }
		public Guid SitePriceID { get; set; }
		public Guid SiteItemID { get; set; }
		public double Price { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}

	#region View list Site Mobile
	public class FilterBillboard : BaseModel
	{
		public string TypeBillboard { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string Alamat { get; set; }
		public double? MinimumPrice { get; set; }
		public double? MaximumPrice { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public Guid? UserID { get; set; }
		public bool showWithDisabledSite { get; set; }
		public bool isFilterDataTable { get; set; }
		public string FilterDataTableValue { get; set; }
		public DateTime? startDate { get; set; }
		public DateTime? endDate { get; set; }
		public int? panjang { get; set; }
		public int? lebar { get; set; }
		public sort? sorting { get; set; }

	}

	public enum sort
	{
		AtoZ=1,
		ZtoA=2,
		Termurah=3,
		Termahal=4
	}

	#endregion

	#region Add Site

	public class AddSiteDetailInputModel
	{
		public string NoBillboard { get; set; }
		public string Pulau { get; set; }
		public string Kota { get; set; }
		public string Provinsi { get; set; }
		public string Cabang { get; set; }
		public string HorV { get; set; }
		public string Tipe { get; set; }
		public string Longitude { get; set; }
		public string Latitude { get; set; }
		public string Address { get; set; }
	}

	public class AddSitePriceInputModel
	{
		public string Dasar { get; set; }
		public double Kelipatan { get; set; }
		public double HargaAwal { get; set; }
		public double HargaAkhir { get; set; }

	}

	public class AddSiteImageInputModel
	{
		public string Image { get; set; }
	}

	public class AddSiteResultDetailInputModel
	{
		public Guid UserID { get; set; }
		public AddSiteDetailInputModel Detail { get; set; }
	}

	public class AddSiteResultPriceInputModel
	{
		public Guid UserID { get; set; }
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public List<AddSitePriceInputModel> Price { get; set; }
		public string MinimDasar { get; set; }
		public int MinimOrder { get; set; }
	}

	public class AddSiteResultImageInputModel
	{
		public Guid UserID { get; set; }
		public Guid SiteID { get; set; }
		public Guid SiteitemID { get; set; }
		public List<AddSiteImageInputModel> Image { get; set; }
	}

	public class AddSiteOutputModel
	{
		public Guid SiteID { get; set; }
	}

	public class AddSiteResponseModel : BaseResponseModel
	{
		public AddSiteOutputModel data { get; set; }
	}

	public class AddSiteItemDetailInputModel
	{
		public Guid SiteID { get; set; }
		public string KodeArahLokasi { get; set; }
		public string ArahLokasi { get; set; }
		public double Panjang { get; set; }
		public double Lebar { get; set; }
	}

	public class AddSiteItemDetailOutputModel
	{
		public Guid SiteItemDetailID { get; set; }
	}

	public class AddSiteItemDetailResponseModel : BaseResponseModel
	{
		public AddSiteItemDetailOutputModel data { get; set; }
	}
	#endregion

	#region Get Site detail admin

	public class SiteDetailInputModel
	{
		public Guid SiteID { get; set; }
		public Guid? UserID { get; set; }
	}

	public class SiteDetailOutputModel
	{
		public Guid SiteID { get; set; }
		public string NoBillboard { get; set; }
		public string Pulau { get; set; }
		public string Kota { get; set; }
		public string Provinsi { get; set; }
		public string Cabang { get; set; }
		public string Ukuran { get; set; }
		public string HorV { get; set; }
		public string Tipe { get; set; }
		public string Longitude { get; set; }
		public string Latitude { get; set; }
		public double RateScoreAverage { get; set; }
		public string Alamat { get; set; }
		public string ImageHeader { get; set; }
		public int TotalView { get; set; }
	}

	public class SiteDetailPriceModel
	{

		public Guid SitePriceID { get; set; }
		public Guid SiteItemID { get; set; }
		public string Dasar { get; set; }
		public double Kelipatan { get; set; }
		public double HargaAwal { get; set; }
		public double HargaAkhir { get; set; }
		public string MinimDasar { get; set; }
		public int MinimOrder { get; set; }
	}

	public class SiteDetailImageModel
	{
		public Guid SiteImageID { get; set; }
		public Guid SiteItemID { get; set; }
		public string Image { get; set; }
	}

	public class SiteDetailItemModel
	{
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public string ArahLokasi { get; set; }
		public string KodeLokasi { get; set; }
		public double Panjang { get; set; }
		public double Lebar { get; set; }
		public SiteWishlist Wishlist { get; set; }
		public List<SiteDetailPriceModel> Price { get; set; }
		public List<SiteDetailImageModel> Image { get; set; }
	}

	public class SiteDetailResultOutputModel
	{
		public SiteDetailOutputModel Detail { get; set; }
		public List<SiteDetailItemModel> Item { get; set; }

	}

	public class SiteDetailResponseModel : BaseResponseModel
	{
		public bool isLogin { get; set; }
		public string act { get; set; }
		public SiteDetailResultOutputModel data { get; set; }
	}
	#endregion

	#region Edit Site admin
	public class EditSiteDetailInputModel
	{
		public Guid SiteID { get; set; }
		public string NoBillboard { get; set; }
		public string Pulau { get; set; }
		public string Kota { get; set; }
		public string Provinsi { get; set; }
		public string Cabang { get; set; }
		public double Panjang { get; set; }
		public double Lebar { get; set; }
		public string HorV { get; set; }
		public string Tipe { get; set; }
		public string Longitude { get; set; }
		public string Latitude { get; set; }
		public double Harga { get; set; }
		public string Address { get; set; }
	}

	public class EditSiteItemInputModel
	{
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public string KodeArahLokasi { get; set; }
		public string ArahLokasi { get; set; }
		public double Panjang { get; set; }
		public double Lebar { get; set; }
	}

	public class EditSitePriceInputModel
	{
		public Guid SitePriceID { get; set; }
		public string Dasar { get; set; }
		public double Kelipatan { get; set; }
		public double HargaAwal { get; set; }
		public double HargaAkhir { get; set; }
		public string MinimDasar { get; set; }
		public int MinimOrder { get; set; }
		public double HargaPerhari { get; set; }
	}

	public class EditSiteImageInputModel
	{
		public Guid SiteImageID { get; set; }
		public string Image { get; set; }
		public string IsThumbnail { get; set; }
	}

	public class EditSiteResultInputModel
	{
		public Guid UserID { get; set; }

		public EditSiteDetailInputModel Detail { get; set; }
		public List<EditSiteItemInputModel> Item { get; set; }
		public List<EditSitePriceInputModel> Price { get; set; }
		public List<EditSiteImageInputModel> Image { get; set; }
	}

	public class EditSiteOutputModel
	{
		public EditSiteDetailInputModel Detail { get; set; }
		public List<EditSiteItemInputModel> Item { get; set; }
		public List<EditSitePriceInputModel> Price { get; set; }
		public List<EditSiteImageInputModel> Image { get; set; }
	}

	public class EditSiteResponseModel : BaseResponseModel
	{
		public EditSiteOutputModel data { get; set; }
	}
	#endregion

	#region View List Admin
	public class SiteListOutputModel
	{
		public Guid SiteID { get; set; }
		public string Alamat { get; set; }
		public string NoBillboard { get; set; }
		public string Pulau { get; set; }
		public string Kota { get; set; }
		public string Provinsi { get; set; }
		public string Cabang { get; set; }
		public string HorV { get; set; }
		public string Tipe { get; set; }
		public string Longitude { get; set; }
		public string Latitude { get; set; }
		public double RateScoreAverage { get; set; }
		public double HargaPerhari { get; set; }
		public string MinimDasar { get; set; }
		public int MinimOrder { get; set; }
		public int TotalView { get; set; }
		public double Distance { get; set; }
		public string Status { get; set; }
		public string ImageHeader { get; set; }
		//public SiteWishlist Wishlist { get; set; }
		public List<SiteListItemModel> SiteItem { get; set; }

	}

	public class SiteListResultModel
	{
		public List<SiteListOutputModel> Result { get; set; }
		public int TotalPage { get; set; }
		public int TotalData { get; set; }
	}

	public class SiteWishlist
	{
		public bool isWishlist { get; set; }
		public Guid WishlistID { get; set; }
	}

	public class SiteListItemModel
	{
		public Guid SiteItemID { get; set; }
		public string ArahLokasi { get; set; }
		public string KodeLokasi { get; set; }
		public double Panjang { get; set; }
		public double Lebar { get; set; }
		public string Catatan { get; set; }
		public SiteWishlist Wishlist { get; set; }
		public List<EditSitePriceInputModel> Price { get; set; }
		public List<EditSiteImageInputModel> Image { get; set; }
	}

	public class SiteListResponseModel : BaseResponseModel
	{
		public string type { get; set; }
		public string location { get; set; }
		public string city { get; set; }
		public string provinsi { get; set; }
		public DateTime? startDate { get; set; }
		public DateTime? endDate { get; set; }
		public int? panjang { get; set; }
		public int? lebar { get; set; }
		public bool isLogin { get; set; }
		public double? MinimumPrice { get; set; }
		public double? MaximumPrice { get; set; }
		public sort? sorting { get; set; }

		public List<SiteListOutputModel> data { get; set; }
	}

	public class DeleteSiteInputModel
	{
		public Guid SiteID { get; set; }
		public Guid UserID { get; set; }
	}

	public class DeleteSiteOutputModel
	{
		public string message { get; set; }
	}

	public class DeleteSiteResponseModel : BaseResponseModel
	{
		public DeleteSiteOutputModel data { get; set; }
	}

	public class ChangeStatusInputModel
	{
		public Guid SiteID { get; set; }
		public Guid UserID { get; set; }
		public int Value { get; set; }
	}

	public class ChangeStatusOutputModel
	{
		public string message { get; set; }
	}

	public class ChangeStatusResponseModel : BaseResponseModel
	{
		public ChangeStatusOutputModel data { get; set; }
	}

	public class CheckDateAvailableInputModel
	{
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

	}

	public class CheckDateAvailableOutputModel
	{
		public Guid BookDetailID { get; set; }
	}

	public class CheckAvailableResultModel
	{
		public List<CheckDateAvailableOutputModel> Result { get; set; }
		public bool CanBook { get; set; }
	}

	public class CheckDateAvailableResponseModel : BaseResponseModel
	{
		public CheckAvailableResultModel data { get; set; }
	}
	#endregion
}
