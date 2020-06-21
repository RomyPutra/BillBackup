using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class UserRoleRepository
    {
        DatabaseContext db = new DatabaseContext();
        public UserRoleRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(UserRole data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            db.Set<UserRole>().Add(data);

            if (isCommit)
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
    }
}
