using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class CompareRepository
    {
        DatabaseContext db = new DatabaseContext();
        public CompareRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(Compare data)
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

                    message = "Save Data success";
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

        public Response Delete(Guid compareID, Guid userID)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                List<Compare> listCom = new List<Compare>();
                if (compareID != Guid.Empty)
                {
                    listCom = db.Compare.Where(x => x.ID == compareID).ToList();
                }
                else
                {
                    listCom = db.Compare.Where(x => x.UserID == userID).ToList();
                }


                if (listCom != null && listCom.Count > 0)
                {

                    listCom.ForEach(x =>
                    {
                        x.DeletedDate = DateTime.Now;
                        x.DeletedByUserID = userID;
                    });

                    db.SaveChanges();

                    message = "Delete data success";
                    result = true;
                }

                res.ID = compareID;
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
