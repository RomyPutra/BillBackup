using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.API.Models.Enums;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;

namespace Billboard360.API.BussinessLogic
{
    public class LoginBL
    {
        protected readonly DatabaseContext db;

        public LoginBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public LoginResponseModel Login(string userName, string password)
        {
            UserRepository repo = new UserRepository(db);

            string message = "Gagal Login!! Username atau password tidak cocok";
            bool result = false;

            string hashPassword = password.ConvertToMD5();

            var response = (from u in db.User
                            join ur in db.UserRole on u.ID equals ur.UserID
                            join r in db.Role on ur.RoleID equals r.ID
                            where u.UserName == userName && u.PasswordHash == hashPassword && u.DeletedDate == null
                            select new LoginOutputModel
                            {
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                RoleID = r.ID,
                                UserID = u.ID,
                                RoleName = r.Name,
                                UserName = u.UserName,
                                IsActive = u.IsActive
                            }).FirstOrDefault();

            LoginResponseModel res = new LoginResponseModel();


            if (response != null)
            {
                if(response.IsActive)
                {
                    res.data = response;
                    message = "Selamat datang " + response.FirstName + " " + response.LastName;
                    result = true;
                } else
                {
                    //res.data = response;
                    res.data = response;
                    message = response.FirstName + " " + response.LastName + " tidak aktif";
                    result = false;
                }                
            }

            res.Message = message;
            res.Response = result;

            return res;
        }




    }
}
