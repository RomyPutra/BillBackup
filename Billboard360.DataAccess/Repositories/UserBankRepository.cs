using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class UserBankRepository
    {
        DatabaseContext db = new DatabaseContext();
        public UserBankRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(UserBank data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                db.Add(data);

                db.SaveChanges();

                message = "Save data bank success";
                result = true;

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

        public Response Edit(UserBank data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {
                    var find = db.UserBank.Where(x => x.ID == data.ID && x.DeletedDate == null).FirstOrDefault();

                    if (find != null)
                    {
                        find.BankID = data.BankID;
                        find.UserID = data.UserID;
                        find.AccountName = data.AccountName;
                        find.AccountNumber = data.AccountNumber;
                        find.isSelected = data.isSelected;

                        db.SaveChanges();

                        message = "Update data Bank success";
                        result = true;
                    } else
                    {
                        message = "Data Bank User tidak di temukan hubungi admin!";
                    }
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

        public Response Delete(Guid userBankID, Guid userID)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (userBankID != Guid.Empty)
                {
                    var find = db.UserBank.Where(x => x.ID == userBankID && x.DeletedDate == null).FirstOrDefault();

                    if (find != null)
                    {
                        find.DeletedByUserID = userID;
                        find.DeletedDate = DateTime.Now;

                        db.SaveChanges();

                        message = "Delete data Bank success";
                        result = true;
                    }
                }

                res.ID = userBankID;
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

        public IQueryable<UserBank> GetUserBankAll()
        {
            return db.UserBank.Where(x => x.DeletedDate == null);
        }
    }
}
