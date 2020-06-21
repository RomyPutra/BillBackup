using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class LogForgotPasswordRepository
    {
        DatabaseContext db = new DatabaseContext();
        public LogForgotPasswordRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(LogForgotPassword data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            db.Add(data);
            db.SaveChanges();

            message = "Save data success";
            result = true;

            res.ID = data.ID;
            res.Message = message;
            res.Result = result;

            return res;
        }

        public bool IsValid(Guid id)
        {

            var today = DateTime.Now;
            var query = db.LogForgotPassword.Where(x => x.ID == id && x.DateExpired > today && x.IsUsed == false).FirstOrDefault();

            if(query != null)
            {
                return true;
            } else
            {
                return false;
            }

        }

        public IQueryable<LogForgotPassword> GetLogForgotPasswordByID(Guid id) 
        {
            return db.LogForgotPassword.Where(x => x.ID == id);
        }
    }
}
