using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.Web.Models
{
    public class AddBankAccountInputModel
    {
        public Guid UserID { get; set; }
        public Guid BankID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public bool isSelected { get; set; }
    }

    public class EditBankAccountInputModel
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid BankID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public bool isSelected { get; set; }
    }

    public class AddBankAccountOutputModel
    {
        public Guid UserID { get; set; }
        public Guid BankID { get; set; }
    }

    public class EditBankAccountOutputModel
    {
        public Guid UserID { get; set; }
        public Guid BankID { get; set; }
    }

    public class AddBankAccountResponseMoodel: BaseResponseModel
    {
        public AddBankAccountOutputModel data { get; set; }
    }

    public class EditBankAccountResponseMoodel : BaseResponseModel
    {
        public EditBankAccountOutputModel data { get; set; }
    }

	public class ListBankAccountOutputModel
	{
		public Guid UserBankID { get; set; }
		public string BankName { get; set; }
		public string AccountNumber { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CompanyName { get; set; }
	}

	public class ListBankAccountResponseModel : BaseResponseModel
	{
		public List<ListBankAccountOutputModel> data { get; set; }
	}

	public class DeleteBankAccountInputModel
	{
		public Guid UserID { get; set; }
		public Guid UserBankID { get; set; }
	}

	public class DeleteBankAccountOutputModel
	{
		public string Message { get; set; }
	}

	public class DeleteBankAccountResponseModel : BaseResponseModel
	{
		public DeleteBankAccountOutputModel data { get; set; }
	}

    public class ListBankUserAccountOutputModel
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string AccountBankName { get; set; }

    }

    public class ListBankAccountResultModel
    {
        public List<ListBankUserAccountOutputModel> data { get; set; }
        public int TotalData { get; set; }
        public int TotalPage { get; set; }
    }

    public class ListBankUserAccountResponseModel : BaseResponseModel
    {
        public List<ListBankUserAccountOutputModel> Data { get; set; }
    }
}
