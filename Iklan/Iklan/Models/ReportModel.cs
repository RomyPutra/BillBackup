using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Iklan.Models
{

	#region Report Problem
	public class AddReportProblemInputModel
	{
		public Guid SiteID { get; set; }
		public Guid UserID { get; set; }
		public string Desc { get; set; }
		public bool isToSPV { get; set; }
	}

	public class AddReportProblemOutputModel
	{
		public Guid ReportID { get; set; }
		public string ReportNo { get; set; }
	}

	public class AddReportProblemResponseModel : BaseResponseModel
	{
		public AddReportProblemOutputModel data { get; set; }
	}

	public class ReportProblemListInputModel : BaseModel
	{
		public bool isToSPV { get; set; }
	}
	public class ReportProblemListOutputModel
	{
		public Guid ReportProblemID { get; set; }
		public string ReportNo { get; set; }
		public string NoBillboard { get; set; }
		public string Desc { get; set; }
		public string From { get; set; }
	}

	public class ReportProblemListResponseModel : BaseResponseModel
	{
		public List<ReportProblemListOutputModel> data { get; set; }
	}
	#endregion

	#region View Report Site
	public class ReportSiteInputModel : BaseModel
	{
		public string NoBillboard { get; set; }
		public string Kota { get; set; }
		public string Provinsi { get; set; }
		public Guid MediaOwnerID { get; set; }
		public bool IsBooked { get; set; }
		public bool IsPurchased { get; set; }
	}

	public class ReportSiteOutputModel
	{
		public string NoBillBoard { get; set; }
		public string Kota { get; set; }
		public string Provinsi { get; set; }
		public string MediaOwnerName { get; set; }
		public Guid MediaOwnerGuid { get; set; }
		public string Status { get; set; }
	}

	public class ReportSiteResponseModel : BaseResponseModel
	{
		public List<ReportSiteOutputModel> data { get; set; }
	}

	public class ViewReportPurchaseBookInputModel : BaseModel
	{
		public bool IsPurchased { get; set; }
		public bool IsBook { get; set; }
		public string UserName { get; set; }
		public string NoBillboard { get; set; }
	}

	public class ViewReportPurchaseBookOutputModel
	{
		public string UserName { get; set; }
		public string Status { get; set; }
		public string NoBillBoard { get; set; }
		public int Qty { get; set; }
		public string Unit { get; set; }
		public double Harga { get; set; }
		public double HargaTotal { get; set; }
	}

	public class ViewReportPurchaseBookResponseModel : BaseResponseModel
	{
		public List<ViewReportPurchaseBookOutputModel> data { get; set; }
	}

	#endregion

	#region View Dashboard
	public class ReportPurchaseAndBookInputModel : BaseModel
	{
		public bool IsPurchased { get; set; }
		public bool IsBook { get; set; }
		public string UserName { get; set; }
		public string NoBillboard { get; set; }
		public DateTime? TanggalPurchaseAwal { get; set; }
		public DateTime? TanggalPurchaseAkhir { get; set; }
	}

	public class ReportPurchaseAndBookOutputModel
	{
		public Guid BookID { get; set; }
		public string UserName { get; set; }
		public string Status { get; set; }
		public string NoBillBoard { get; set; }
		public int Qty { get; set; }
		public string Unit { get; set; }
		public double Harga { get; set; }
		public double HargaTotal { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime TglBook { get; set; }
		public string TglTrans { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime StartDate { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime EndDate { get; set; }
		public string BookNo { get; set; }
		public Guid OwnerSiteID { get; set; }
	}

	public class ReportPurchaseAndBookResultModel
	{
		public List<ReportPurchaseAndBookOutputModel> result { get; set; }
		public int TotalPages { get; set; }
		public int TotalData { get; set; }
	}

	public class ReportPurchaseAndModelResponseModel : BaseResponseModel
	{
		public List<ReportPurchaseAndBookOutputModel> data { get; set; }
	}

	public class ReportKotaEmpatInputModel
	{
		public Guid? UserID { get; set; }
	}

	public class RekapKotakEmpatOutputModel
	{
		public double TotalRevenue { get; set; }
		public double Revenue { get; set; }
		public int TotalOrder { get; set; }
		public int TotalBooking { get; set; }
		public int TotalUseSite { get; set; }
		public int TotalSite { get; set; }
	}

	public class RekapKotakEmpatResponseModel : BaseResponseModel
	{
		public RekapKotakEmpatOutputModel data { get; set; }
	}

	public class ChardResponseModel : BaseResponseModel
	{
		public ChardModel data { get; set; }
	}

	public class ChardModel
	{
		public double januari { get; set; }
		public double februari { get; set; }
		public double maret { get; set; }
		public double april { get; set; }
		public double mei { get; set; }
		public double juni { get; set; }
		public double juli { get; set; }
		public double agustus { get; set; }
		public double september { get; set; }
		public double oktober { get; set; }
		public double november { get; set; }
		public double desember { get; set; }
	}
	#endregion
}
