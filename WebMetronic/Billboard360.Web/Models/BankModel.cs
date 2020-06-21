using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.Web.Models
{
    public class AddBankInputModel
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string LogoBank { get; set; }
        public bool IsActive { get; set; }
        public Guid CreateByUserID { get; set; }
    }

    public class EditBankInputModel
    {
        public Guid BankID { get; set; }
        public string Kode { get; set; }
        public string LogoBank { get; set; }
        public string Nama { get; set; }
        public bool IsActive { get; set; }
        public Guid LastUpdateByUserID { get; set; }
    }

    public class BankOutputModel
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
    }

    public class BankResponseModel: BaseResponseModel
    {
        public BankOutputModel data { get; set; }
    }

    public class InfoBankDetailModel
    {
        public Guid BankID { get; set; }
        public string Kode { get; set; }
        public string Bank { get; set; }
        public string LogoBank { get; set; }
        public bool IsActive { get; set; }
    }

    public class InfoBankResponseModel:BaseResponseModel
    {
        public InfoBankDetailModel data { get; set; }
    }

    public class FilterBank:BaseModel
    {
        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string UserName { get; set; }
    }

    public class ListBankResponseModel:BaseResponseModel
    {
        public List<InfoBankDetailModel> data { get; set; }
    }

    public class InActiveBankInputModel
    {
        public Guid BankID { get; set; }
        public bool Value { get; set; }

        public Guid UserID { get; set; }
    }

    public class InActiveBankOutputModel
    {
        public Guid BankID { get; set; }
    }

    public class InActiveBankResponseModel: BaseResponseModel
    {
        public InActiveBankOutputModel data { get; set; }
    }

    public class DeleteBankInputModel
    {
        public Guid BankID { get; set; }
        public Guid UserID { get; set; }
    }

    public class DeleteBankOutputModel
    {
        public Guid BankID { get; set; }
    }

    public class DeleteBankResponseModel:BaseResponseModel
    {
        public DeleteBankOutputModel data { get; set; }
    }

	public class GetUserBankInputModel
	{
		public Guid UserID { get; set; }
	}

	public class GetUserBankOutputModel
	{
		public Guid UserBankID { get; set; }
		public Guid BankID { get; set; }
		public string BankName { get; set; }
		public string AccountNumber { get; set; }
		public string AccountName { get; set; }
	}

	public class GetUserBankResponseModel : BaseResponseModel
	{
		public List<GetUserBankOutputModel> data { get; set; }
	}

	public class EditBankAccount
	{
		public EditBankAccountInputModel input { get; set; }
		public GetUserBankOutputModel item { get; set; }
	}
}
