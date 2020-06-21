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
    public class ChangePasswordBL
    {
        protected readonly DatabaseContext DbContext;

        public ChangePasswordBL(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public ChangePasswordResponseModel ChangePasswordWithOldPassword(ChangePasswordInputModel data)
        {

            bool result = false;
            string message = "Failed Change Password";
            UserRepository repo = new UserRepository(DbContext);

            var userInfo = repo.FindByID(data.UserID).FirstOrDefault();

            ChangePasswordResponseModel res = new ChangePasswordResponseModel();
            

            if (userInfo != null)
            {
                
                var response = repo.ChangePasswordWithOldPassword(userInfo.UserName, data.NewPassword.ConvertToMD5(), data.OldPassword.ConvertToMD5(), true);

                if (response.Result)
                {
                    ChangePasswordOutputModel resOut = new ChangePasswordOutputModel();
                    resOut.UserID = response.ID;

                    res.data = resOut;
                }

                result = response.Result;
                message = response.Message;

            }
            res.Response = result;
            res.Message = message;

            return res;
        }

        public ChangePasswordResponseModel ChangePasswordWithoutOldPassword(ChangePasswordForgotInputModel data)
        {

            bool result = false;
            string message = "Failed Change Password";
            UserRepository repo = new UserRepository(DbContext);

            var userInfo = repo.FindByID(data.UserID).FirstOrDefault();

            ChangePasswordResponseModel res = new ChangePasswordResponseModel();


            if (userInfo != null)
            {

                var response = repo.ChangePasswordWithOldPassword(userInfo.UserName, data.NewPassword.ConvertToMD5(), string.Empty, false);

                if (response.Result)
                {
                    ChangePasswordOutputModel resOut = new ChangePasswordOutputModel();
                    resOut.UserID = response.ID;

                    res.data = resOut;
                }

                result = response.Result;
                message = response.Message;

            }
            res.Response = result;
            res.Message = message;

            return res;
        }
    }
}
