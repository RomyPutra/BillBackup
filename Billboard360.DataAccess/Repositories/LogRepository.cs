using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class LogRepository
    {
        DatabaseContext db = new DatabaseContext();
        public LogRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(LogActivity data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {
                    db.Set<LogActivity>().Add(data);

                    if (isCommit)
                    {
                        db.SaveChanges();

                        message = "Save Data log success";
                        result = true;

                        res.ID = data.ID;
                    }

                }

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
