using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class WishListRepository
    {
        protected readonly DatabaseContext db;

        public WishListRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(WishList data)
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

                    message = "Save data success";
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

        public Response DeleteByID(Guid id)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            var data = db.WishList.Where(x => x.ID == id).FirstOrDefault();

            if(data != null)
            {
                data.DeletedDate = DateTime.Now;

                db.SaveChanges();

                message = "Success delete";
                result = true;
                res.ID = id;
            }

            res.Message = message;
            res.Result = result;

            return res;

        }


    }
}
