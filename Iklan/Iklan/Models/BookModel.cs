using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Iklan.Models
{
	public class AddBookInputModel
	{
		public Guid SitePriceID { get; set; }
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public Guid UserID { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int Qty { get; set; }
		public double Price { get; set; }
		public double TotalPerItem { get; set; }
		public Guid? CartID { get; set; }
	}

	public class AddToBookListInputModel
	{
		public List<AddBookInputModel> Data { get; set; }
		public bool isFromCart { get; set; }
	}

	public class AddBookOutputModel
	{
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public Guid BookID { get; set; }
		public string BookNo { get; set; }
	}

	public class AddBookResponseModel : BaseResponseModel
	{
		public AddBookOutputModel data { get; set; }
	}

	public class ViewBookInputModel : BaseModel
	{
		public Guid UserID { get; set; }
		public string Status { get; set; }
		public Guid? BookID { get; set; }
	}

	public class ViewCartInputModel
	{
		public Guid UserID { get; set; }
	}

	public class ViewResultCartModel
	{
		public Guid CartID { get; set; }
		public Guid CompanyID { get; set; }
		public Guid UserID { get; set; }
		public Guid PaymentID { get; set; }
		public ViewCartSiteSectionkResultModel Site { get; set; }
		public string StatusApproval { get; set; }
		public Guid BookDetailID { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime StartDate { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime EndDate { get; set; }
		public int Qty { get; set; }
		public double Price { get; set; }
		public double TotalHargaPerItem { get; set; }
	}


	public class ViewArrayCartResponse : BaseResponseModel
	{
		public List<ViewResultCartModel> Data { get; set; }
	}

	public class ViewCartResponseModel : BaseResponseModel
	{
		public List<ViewCartOutputModel> data { get; set; }
	}

	public class ViewCartSiteSectionkResultModel
	{
		public SiteDetailBookOutputModel Detail { get; set; }
	}

	public class ViewCartOutputModel
	{
		public Guid SiteID { get; set; }
		public Guid CompanyID { get; set; }
		public Guid UserID { get; set; }
		public Guid? PurchaseID { get; set; }
		public int Qty { get; set; }
		public double Price { get; set; }
		public double TotalPerItem { get; set; }
	}

	public class ViewListBook
	{
		public Guid BookID { get; set; }
		public Guid UserID { get; set; }
		public Guid? PaymentID { get; set; }
		public string StatusApproval { get; set; }
		public int Status { get; set; }
		public List<ViewListDetailBook> DetailBook { get; set; }
	}

	public class ViewListDetailBook
	{
		public Guid BookDetailID { get; set; }
		public Guid BookID { get; set; }
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime StartDate { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime EndDate { get; set; }
		public int Qty { get; set; }
		public double Price { get; set; }
		public double TotalPerItem { get; set; }
		public int StatusApprovalPerBillboard { get; set; }
	}

	public class SiteDetailBookOutputModel
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
		public double RateScoreAverage { get; set; }
		public SiteDetailItemBookOutputModel Item { get; set; }
	}

	public class SiteDetailItemBookOutputModel
	{
		public string ArahLokasi { get; set; }
		public double Panjang { get; set; }
		public double Lebar { get; set; }
		public string Ukuran { get; set; }
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public SitePriceBookOutputModel Price { get; set; }
		public List<SiteImageBookOutputModel> Image { get; set; }
	}

	public class SiteImageBookOutputModel
	{
		public string Image { get; set; }
	}

	public class SitePriceBookOutputModel
	{
		public Guid PriceID { get; set; }
		public double Kelipatan { get; set; }
		public double HargaAwal { get; set; }
		public double HargaAkhir { get; set; }
		public string Satuan { get; set; }
	}

	public class ViewBookOutputModel
	{
		public Guid SiteID { get; set; }
		public Guid CompanyID { get; set; }
		public Guid UserID { get; set; }
		public Guid? PurchaseID { get; set; }
		public int Qty { get; set; }
		public double Price { get; set; }
		public double TotalPerItem { get; set; }
	}

	public class ViewBookSiteSectionkResultModel
	{
		public SiteDetailBookOutputModel Detail { get; set; }
	}

	public class ViewResultBookModel
	{
		public Guid BookID { get; set; }
		public Guid CompanyID { get; set; }
		public Guid UserID { get; set; }
		public Guid? PaymentID { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime CreateDate { get; set; }
		public string StatusApproval { get; set; }
		//public string StatusMidtrans { get; set; }
		public int StatusInt { get; set; }
		public double TotalHargaBook { get; set; }
		public string BookNo { get; set; }
		public string TransactionType { get; set; }
		public int TransactionTypeInt { get; set; }
		public List<ViewBookDetailSection> ListBookDetail { get; set; }
		public List<ViewPaymentMidtransLogBookOuputModel> ListMidtransLog { get; set; }
	}

	public class ViewPaymentMidtransLogBookOuputModel
	{
		public Guid MidtransLogID { get; set; }
		public string VANumber { get; set; }
		public string BankName { get; set; }
		public double PaidAmount { get; set; }
		public int StatusMidTransInt { get; set; }
		public string StatusMidTrans { get; set; }
		public string PaymentType { get; set; }
		public int PaymentTypeInt { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime TransactionDate { get; set; }
	}

	public class ViewBookDetailSection
	{
		public Guid BookDetailID { get; set; }
		public SiteDetailBookOutputModel Site { get; set; }
		public string StatusPerItem { get; set; }
		public string Message { get; set; }
		public double Price { get; set; }
		public int Qty { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime StartDate { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime EndDate { get; set; }
		public double TotalHargaPerItem { get; set; }
	}

	public class ViewArrayBookResponse : BaseResponseModel
	{
		public List<ViewResultBookModel> Data { get; set; }
		public double TotalBayar { get; set; }
	}

	public class ViewBookResponseModel : BaseResponseModel
	{
		public List<ViewBookOutputModel> data { get; set; }
	}

	public class NotificationBookInputModel
	{
		public DateTime ExpiredDate { get; set; }
	}

	public class NotificationBookOutputModel
	{
		public string NoBillboard { get; set; }
		public string Pulau { get; set; }
		public string Kota { get; set; }
		public string Provinsi { get; set; }
		public string Cabang { get; set; }
		public string Ukuran { get; set; }
		public string HorV { get; set; }
		public string BillboardType { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime StartDate { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime EndDate { get; set; }
	}

	public class NotificationBookResponseModel : BaseResponseModel
	{
		public List<NotificationBookOutputModel> data { get; set; }
	}

	public class DeleteBookInputModel
	{
		public Guid UserID { get; set; }
		public List<Guid> BookID { get; set; }
	}

	public class DeleteBookOutputModel
	{
		public string Message { get; set; }
	}

	public class DeleteBookResponseModel : BaseResponseModel
	{
		public DeleteBookOutputModel data { get; set; }
	}

	public class DeleteItemBookInputModel
	{
		public Guid UserID { get; set; }
		public List<Guid> BookID { get; set; }
	}

	public class DeleteItemBookOutputModel
	{
		public string Message { get; set; }
	}

	public class DeleteItemBookResponseModel : BaseResponseModel
	{
		public DeleteItemBookOutputModel data { get; set; }
	}

	public class ApprovalBookInputModel
	{
		public int Value { get; set; }
		public double HargaFinal { get; set; }
		public String Reason { get; set; }
		public Guid UserID { get; set; }
		public Guid BookDetailID { get; set; }
	}

	public class ApprovalBookOutputModel
	{
		public Guid BookID { get; set; }
		public string Message { get; set; }
	}

	public class ApprovalBookResponseModel : BaseResponseModel
	{
		public ApprovalBookOutputModel data { get; set; }
	}

	public class AddItemToCurrentBookInputModel
	{
		public Guid SitePriceID { get; set; }
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public Guid UserID { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int Qty { get; set; }
		public double Price { get; set; }
		public double TotalPerItem { get; set; }
	}

	public class AddItemToCurrentBookListInputModel
	{
		public List<AddItemToCurrentBookInputModel> Data { get; set; }
		public Guid BookID { get; set; }
		public Guid BookDetailIDForDeleted { get; set; }

	}

	public class AddItemToCurrentBookOutputModel
	{
		public Guid SiteID { get; set; }
		public Guid SiteItemID { get; set; }
		public Guid BookID { get; set; }
		public string BookNo { get; set; }

	}

	public class AddItemToCurrentBookResponseModel : BaseResponseModel
	{
		public AddItemToCurrentBookOutputModel data { get; set; }
	}
}
