using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;


namespace Billboard360.API.BussinessLogic
{
    public class BankAccountBL
    {
        protected readonly DatabaseContext db;

        public BankAccountBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public AddBankAccountResponseMoodel Save(AddBankAccountInputModel data)
        {
            UserBank temp = new UserBank();

            temp.AccountName = data.AccountName;
            temp.AccountNumber = data.AccountNumber;
            temp.BankID = data.BankID;
            temp.ID = Guid.NewGuid();
            temp.isSelected = data.isSelected;
            temp.UserID = data.UserID;
            temp.CreateByUserID = data.UserID;
            temp.CreateDate = DateTime.Now;

            UserBankRepository repo = new UserBankRepository(db);

            var res = repo.Insert(temp);

            AddBankAccountOutputModel output = new AddBankAccountOutputModel();

            output.BankID = data.BankID;
            output.UserID = data.UserID;

            AddBankAccountResponseMoodel response = new AddBankAccountResponseMoodel();

            response.data = output;
            response.Message = res.Message;
            response.Response = res.Result;

            return response;
        }

        public EditBankAccountResponseMoodel Edit(EditBankAccountInputModel data)
        {
            UserBank temp = new UserBank();

            temp.AccountName = data.AccountName;
            temp.AccountNumber = data.AccountNumber;
            temp.BankID = data.BankID;
            temp.ID = data.ID;
            temp.isSelected = data.isSelected;
            temp.UserID = data.UserID;
            temp.CreateByUserID = data.UserID;
            temp.CreateDate = DateTime.Now;

            UserBankRepository repo = new UserBankRepository(db);

            var res = repo.Edit(temp);

            EditBankAccountOutputModel output = new EditBankAccountOutputModel();

            if(res.Result)
            {
                output.BankID = data.BankID;
                output.UserID = data.UserID;
            } else
            {
                output.BankID = Guid.Empty;
                output.UserID = Guid.Empty;
            }
            

            EditBankAccountResponseMoodel response = new EditBankAccountResponseMoodel();

            response.data = output;
            response.Message = res.Message;
            response.Response = res.Result;

            return response;
        }

        public List<ListBankAccountOutputModel> GetBankAccount()
        {
            UserBankRepository repo = new UserBankRepository(db);

            

            var res = (from ub in db.UserBank
                       join b in db.Bank on ub.BankID equals b.ID
                       join u in db.User on ub.UserID equals u.ID
                       join c in db.Company on u.ID equals c.UserID
                       select new ListBankAccountOutputModel
                       {
                           AccountNumber = ub.AccountNumber,
                           BankName = b.BankName,
                           CompanyName = c.CompanyName,
                           FirstName = u.FirstName,
                           LastName=u.LastName,
                           UserBankID = ub.ID,
                       }).ToList();

            return res;

        }

        public DeleteBankAccountOutputModel Delete(DeleteBankAccountInputModel data)
        {
            UserBankRepository repo = new UserBankRepository(db);

            var res = repo.Delete(data.UserBankID, data.UserID);

            DeleteBankAccountOutputModel output = new DeleteBankAccountOutputModel();

            output.Message = res.Message;
            return output;
        }
    }
}
