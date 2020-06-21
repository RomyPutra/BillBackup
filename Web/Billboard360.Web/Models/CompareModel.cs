using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.Web.Models
{
	public class AddCompareInputModel
	{
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public Guid UserID { get; set; }

	}

	public class AddCompareOutputModel
	{
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public string Message { get; set; }
	}

	public class CompareResponseModel : BaseResponseModel
	{
		public AddCompareOutputModel data { get; set; }
	}

	public class CompareListInputModel
	{
		public Guid UserID { get; set; }
	}

	public class CompareListSiteItemModel
	{
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public string ArahLokasi { get; set; }
		public string KodeLokasi { get; set; }
		public double Panjang { get; set; }
		public double Lebar { get; set; }

		public List<CompareListPriceModel> Price { get; set; }
		public List<CompareListImageModel> Image { get; set; }
	}

	public class CompareListResultModel
	{
		public CompareListDetailModel Detail { get; set; }

		public List<CompareListSiteItemModel> Item { get; set; }
	}

	public class CompareListDetailModel
	{
		public Guid SiteID { get; set; }
		public Guid CompareID { get; set; }
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
	}

	public class CompareListPriceModel
	{
		public Guid SitePriceID { get; set; }
		public Guid SiteItemID { get; set; }
		public string Dasar { get; set; }
		public double Kelipatan { get; set; }
		public double HargaAwal { get; set; }
		public double HargaAkhir { get; set; }
	}

	public class CompareListImageModel
	{
		public Guid SiteImageID { get; set; }
		public Guid SiteItemID { get; set; }
		public string Image { get; set; }
	}

	public class CompareListResponseModel : BaseResponseModel
	{
		public List<CompareListResultModel> data { get; set; }
	}

	public class DeleteCompareInputModel
	{
		public Guid CompareID { get; set; }
		public Guid UserID { get; set; }
	}

	public class DeleteCompareOutputModel
	{
		public Guid CompareID { get; set; }
	}

	public class DeleteCompareResponseModel : BaseResponseModel
	{
		public DeleteCompareOutputModel data { get; set; }
	}

	public class ClearCompareInputModel
	{
		public Guid UserID { get; set; }
	}

	public class ClearCompareOutputModel
	{
		public string Message { get; set; }
	}

	public class ClearCompareResponseModel : BaseResponseModel
	{
		public ClearCompareOutputModel data { get; set; }
	}
}
