using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iklan.Models
{
	#region Custom Book
	public class CustomViewBook
	{
		public List<ViewResultBookModel> Data { get; set; }
		public double TotalBayar { get; set; }
	}
	#endregion

	#region Custom Dashboard
	public class CustomDashboard
	{
		public RekapKotakEmpatOutputModel KotakEmpat { get; set; }
		public List<ReportPurchaseAndBookOutputModel> PurchaseAndBook { get; set; }
		public ChardModel Chard { get; set; }
		public List<ReportProblemListOutputModel> SiteProblem { get; set; }
	}
	#endregion

	#region Custom Site
	public class SelectListItem
	{
		public string Text { get; set; }
		public string Value { get; set; }
		public string Selected { get; set; }
	}
	public class EditSite
	{
		public SiteDetailResultOutputModel val { get; set; }
		public EditSiteDetailInputModel input { get; set; }
		public List<SelectListItem> Options { get; set; }
	}
	public class EditItem
	{
		public SiteDetailItemModel val { get; set; }
		public EditSiteItemInputModel input { get; set; }
	}
	public class EditPrice
	{
		public SiteDetailPriceModel val { get; set; }
		public EditSitePriceInputModel input { get; set; }
	}
	public class EditImage
	{
		public SiteDetailImageModel val { get; set; }
		public EditSiteImageInputModel input { get; set; }
	}
	public class ForDetail
	{
		public List<City> Cities { get; set; }
		public SiteDetailResultOutputModel Site { get; set; }
	}

	public class EditDetail
	{
		public CustomSiteDetailItemModel val { get; set; }
		public CustomSiteDetailItemModel input { get; set; }
	}
	public class CustomSiteDetailItemModel
	{
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public Guid WishlistID { get; set; }
		public Guid SitePriceID { get; set; }
		public Guid SiteImageID { get; set; }
		public string ItemName { get; set; }
		public string ArahLokasi { get; set; }
		public string KodeLokasi { get; set; }
		public double Panjang { get; set; }
		public double Lebar { get; set; }
		public bool isWishlist { get; set; }
		public string Dasar { get; set; }
		public double Kelipatan { get; set; }
		public double HargaAwal { get; set; }
		public double HargaAkhir { get; set; }
		public string MinimDasar { get; set; }
		public int MinimOrder { get; set; }
		public string Image { get; set; }
	}
	#endregion

	public class JsonModels
	{
		public string sEcho { get; set; }
		public string sSearch { get; set; }
		public int iDisplayLength { get; set; }
		public int iDisplayStart { get; set; }
		public int iColumns { get; set; }
		public int iSortCol_0 { get; set; }
		public string sSortDir_0 { get; set; }
		public int iSortingCols { get; set; }
		public string sColumns { get; set; }
	}

	public class JsonModel
	{
		public string HTMLString { get; set; }
		public bool NoMoreData { get; set; }
	}
}
