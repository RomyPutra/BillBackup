using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class BankRepository
    {
        DatabaseContext db = new DatabaseContext();
        public BankRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<Bank> GetAllBank()
        {
            return db.Bank.Where(x => x.DeletedDate == null);
        }

        public IQueryable<Bank> GetBankByID(Guid BankID)
        {
            return db.Bank.Where(x => x.ID == BankID);
        }

        public Bank GetBank(Guid BankID)
        {
            return db.Bank.Where(x => x.ID == BankID).FirstOrDefault();
        }

        public Response Insert(Bank data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID == Guid.Empty)
                {
                    data.ID = Guid.NewGuid();

                    db.Add(data);

                    db.SaveChanges();

                    message = "Save data bank success";
                    result = true;
                }

                res.ID = data.ID;
                res.Message = message;
                res.Result = result;

                return res;

            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;

                return res;
            }

        }

        public Response Update(Bank data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {

                    var find = (from ds in db.Bank
                                where ds.ID == data.ID
                                select ds).FirstOrDefault();

                    find.IsActive = data.IsActive;
                    find.BankName = data.BankName;
                    find.LastUpdateByUserID = data.LastUpdateByUserID;
                    find.LastUpdateDate = DateTime.Now;
                    find.LogoBank = data.LogoBank;
                    find.Kode = data.Kode;
                    

                    db.SaveChanges();

                    message = "Update data master Bank success";
                    result = true;
                }

                res.ID = data.ID;
                res.Message = message;
                res.Result = result;

                return res;

            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;

                return res;
            }

        }

        public Response ChangeStatus (Bank data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {

                    var find = (from ds in db.Bank
                                where ds.ID == data.ID
                                select ds).FirstOrDefault();

                    find.IsActive = data.IsActive;
                    find.LastUpdateByUserID = data.LastUpdateByUserID;
                    find.LastUpdateDate = DateTime.Now;
                    
                    db.SaveChanges();

                    message = "Update data master Bank success";
                    result = true;
                }

                res.ID = data.ID;
                res.Message = message;
                res.Result = result;

                return res;

            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;

                return res;
            }
        }

        public Response DeleteBank(Bank data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {

                    var find = (from ds in db.Bank
                                where ds.ID == data.ID
                                select ds).FirstOrDefault();

                    find.DeletedByUserID = data.LastUpdateByUserID;
                    find.DeletedDate = DateTime.Now;

                    db.SaveChanges();

                    message = "Delete data master Bank success";
                    result = true;
                }

                res.ID = data.ID;
                res.Message = message;
                res.Result = result;

                return res;

            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;

                return res;
            }
        }
    }
}
