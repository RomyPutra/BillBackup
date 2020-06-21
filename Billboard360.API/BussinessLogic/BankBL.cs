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
    public class BankBL
    {
        protected readonly DatabaseContext DbContext;

        public BankBL(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public BankResponseModel Save(AddBankInputModel data)
        {
            Bank temp = new Bank();

            temp.Kode = data.Kode;
            temp.BankName = data.Nama;
            temp.IsActive = data.IsActive;
            temp.CreateDate = DateTime.Now;
            temp.CreateByUserID = data.CreateByUserID;

            BankRepository bankRepo = new BankRepository(DbContext);

            var res = bankRepo.Insert(temp);

            BankResponseModel result = new BankResponseModel();

            result.Message = res.Message;
            result.Response = res.Result;

            BankOutputModel output = new BankOutputModel();

            output.Kode = data.Kode;
            output.Nama = data.Nama;

            result.data = output;

            return result;
        }

        public BankResponseModel Edit(EditBankInputModel data)
        {
            Bank temp = new Bank();

            temp.ID = data.BankID;
            temp.Kode = data.Kode;
            temp.BankName = data.Nama;
            temp.IsActive = data.IsActive;
            temp.LastUpdateDate = DateTime.Now;
            temp.LastUpdateByUserID = data.LastUpdateByUserID;

            BankRepository bankRepo = new BankRepository(DbContext);

            var res = bankRepo.Update(temp);

            BankResponseModel result = new BankResponseModel();

            result.Message = res.Message;
            result.Response = res.Result;

            BankOutputModel output = new BankOutputModel();

            output.Kode = data.Kode;
            output.Nama = data.Nama;

            result.data = output;

            return result;
        }

        public List<InfoBankDetailModel> GetListBank(FilterBank data)
        {
            BankRepository repo = new BankRepository(DbContext);
            var res = repo.GetAllBank();

            if (data.BankName != string.Empty)
            {
                res = res.Where(x => x.BankName.Contains(data.BankName));
            }

            var filter = (from x in res
                          where x.DeletedDate == null
                          select new InfoBankDetailModel()
                          {
                              BankID = x.ID,
                              Kode = x.Kode,
                              Bank = x.BankName,
                              LogoBank = x.LogoBank,
                              IsActive = x.IsActive
                          });
            int pageNumber = data.PageNumber - 1;

            filter = filter.Skip(pageNumber * data.PageSize).Take(data.PageSize);

            return filter.ToList();


        }

        public InfoBankDetailModel GetDetailBank(Guid BankID)
        {
            BankRepository repo = new BankRepository(DbContext);
            var res = repo.GetBankByID(BankID);

            return (from x in res
                    select new InfoBankDetailModel()
                    {
                        BankID = x.ID,
                        Kode = x.Kode,
                        Bank = x.BankName,
                        LogoBank = x.LogoBank,
                        IsActive = x.IsActive
                    }).FirstOrDefault();
        }

        public InActiveBankOutputModel ChangeStatusBank(InActiveBankInputModel data)
        {
            Bank temp = new Bank();
            temp.ID = data.BankID;
            temp.LastUpdateByUserID = data.UserID;
            temp.IsActive = data.Value;

            BankRepository repo = new BankRepository(DbContext);
            var res = repo.ChangeStatus(temp);

            InActiveBankOutputModel output = new InActiveBankOutputModel();
            output.BankID = res.ID;

            return output;
        }

        public DeleteBankOutputModel DeleteBank(DeleteBankInputModel data)
        {
            Bank temp = new Bank();
            temp.ID = data.BankID;
            temp.DeletedByUserID = data.UserID;

            BankRepository repo = new BankRepository(DbContext);
            var res = repo.DeleteBank(temp);

            DeleteBankOutputModel output = new DeleteBankOutputModel();
            output.BankID = res.ID;

            return output;
        }

        public List<GetUserBankOutputModel> GetUserBankAccount(GetUserBankInputModel data)
        {
            var query = (from b in DbContext.Bank
                         join ub in DbContext.UserBank on b.ID equals ub.BankID
                         join u in DbContext.User on ub.UserID equals u.ID
                         where ub.UserID == data.UserID && ub.DeletedDate == null
                         select new GetUserBankOutputModel()
                         {
                             AccountNumber = ub.AccountNumber,
                             BankID = b.ID,
                             BankName = b.BankName,
                             UserBankID = ub.ID,
                             AccountName = ub.AccountName
                         });

            return query.ToList();
        }


        public ListBankAccountResultModel GetListUserBankAccount(FilterBank data)
        {
            var query = (from b in DbContext.Bank
                         join ub in DbContext.UserBank on b.ID equals ub.BankID
                         join u in DbContext.User on ub.UserID equals u.ID
                         join ur in DbContext.UserRole on u.ID equals ur.UserID
                         join r in DbContext.Role on ur.RoleID equals r.ID
                         where ub.DeletedDate == null
                         select new ListBankUserAccountOutputModel()
                         {
                             BankAccount = ub.AccountNumber,
                             Role = r.Name,
                             BankName = b.BankName,
                             UserName = u.UserName,
                             AccountBankName = ub.AccountName
                         });

            if (data.BankAccountName != string.Empty)
            {
                query = query.Where(x => x.AccountBankName == data.BankAccountName);
            }

            if (data.BankName != string.Empty)
            {
                query = query.Where(x => x.BankName == data.BankName);
            }

            if (data.UserName != string.Empty)
            {
                query = query.Where(x => x.UserName == data.UserName);
            }

            int pageNumber = data.PageNumber - 1;

            var requery = query.Skip(pageNumber * data.PageSize).Take(data.PageSize);

            ListBankAccountResultModel result = new ListBankAccountResultModel();
            result.data = requery.ToList();

            result.TotalData = query.Count();
            result.TotalPage = (query.Count() / data.PageSize) + 1;

            return result;
        }


    }
}
