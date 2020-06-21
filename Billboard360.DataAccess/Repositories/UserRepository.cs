using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class UserRepository
    {
        DatabaseContext db = new DatabaseContext();
        public UserRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(User data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            db.Set<User>().Add(data);

            if(isCommit)
            {
                db.SaveChanges();

                message = "Save data success";
                result = true;
                res.ID = data.ID;
            }
           
            res.Message = message;
            res.Result = result;

            return res;

        }

        public Response Update(User data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {
                    var temp = (from x in db.User
                                where x.ID == data.ID
                                select x).FirstOrDefault();

                    temp.AccessFailedCount = data.AccessFailedCount;
                    temp.ConcurrencyStamp = data.ConcurrencyStamp;
                    temp.EmailConfirmed = data.EmailConfirmed;
                    temp.FirstName = data.FirstName;
                    temp.LastName = data.LastName;
                    temp.LastUpdateByUserID = data.LastUpdateByUserID;
                    temp.LastUpdateDate = data.LastUpdateDate;
                    temp.LockOutEnabled = data.LockOutEnabled;
                    temp.NormalizedUserName = data.NormalizedUserName;
                    //temp.PasswordHash = data.PasswordHash;
                    temp.PhoneNumber = data.PhoneNumber;
                    temp.PhoneNumberConfirmed = data.PhoneNumberConfirmed;
                    temp.PhotoUrl = data.PhotoUrl;
                    temp.SecurityStamp = data.SecurityStamp;
                    temp.SignInToMobile = data.SignInToMobile;
                    temp.TwoFactorEnabled = data.TwoFactorEnabled;
                    temp.UserName = data.UserName;

                    message = "Update data success";
                    result = true;

                    if(isCommit)
                    {
                        db.SaveChanges();
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

        public Response Delete(Guid id, Guid userID)
        {
            Response response = new Response();
            string message = "Failed";
            bool result = false;

            try
            {
                var res = (from x in db.User
                           where x.ID == id
                           select x).FirstOrDefault();

                if (res != null)
                {
                    res.DeletedDate = DateTime.Now;
                    res.DeletedByUserID = userID;

                    db.SaveChanges();

                    message = "Success deleted data";
                    result = true;
                }

                response.Message = message;
                response.Result = result;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }

        public Response EnableDisabled(Guid currentUserID, Guid userID, bool isActive)
        {
            Response response = new Response();
            string message = "Failed";
            bool result = false;

            try
            {
                var res = (from x in db.User
                           where x.ID == userID
                           select x).FirstOrDefault();

                if (res != null)
                {
                    res.LastUpdateByUserID = currentUserID;
                    res.LastUpdateDate = DateTime.Now;
                    res.IsActive = isActive;

                    db.SaveChanges();

                    message = "Success deleted data";
                    result = true;
                }

                response.Message = message;
                response.Result = result;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }

        public Response Login(string userName, string password)
        {
            Response response = new Response();

            try
            {
                var data = (from x in db.User
                            where x.UserName == userName && x.PasswordHash == password && x.DeletedDate == null
                            select x).FirstOrDefault();

                
                if (data != null)
                {
                    response.ID = data.ID;
                    response.Message = "Success login";
                    response.Result = true;
                }
                else
                {
                    response.Message = "Failed login";
                    response.Result = false;
                }
               


                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public User FindByUserName(string userName)
        {
            return db.User.Where(x => x.UserName == userName).FirstOrDefault();
            
        }

        public IQueryable<User> FindByID(Guid userID)
        {
            return db.User.Where(x => x.ID == userID);

        }

        public Response ChangePasswordWithOldPassword(string userName, string newPassword, string oldPassword = "", bool useOldPassword = false)
        {
            Response response = new Response();
            bool result = false;
            string message = "Failed Change password";

            try
            {
         
                var res = (from x in db.User
                            where x.UserName == userName && x.DeletedDate == null
                            select x);

                if(useOldPassword)
                {
                    res = res.Where(x => x.PasswordHash == oldPassword);
                }

                var data = res.FirstOrDefault();

                if (data != null)
                {
                    if (useOldPassword)
                    {
                        if(data.PasswordHash == oldPassword)
                        {
                            data.PasswordHash = newPassword;
                        } else
                        {
                            response.ID = Guid.Empty;
                            response.Message = "Old password not match";
                            response.Result = false;

                            return response;
                        }
                    }
                    else
                    {
                        data.PasswordHash = newPassword;
                    }
                    db.SaveChanges();

                    response.ID = data.ID;
                    result = true;
                    message = "Success change password";

                }
                else
                {
                    result = false;
                }
                response.Message = message;
                response.Result = result;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }

    }
}
