using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;


namespace Billboard360.DataAccess.Repositories
{
    public class BillboardTypeRepository
    {
        DatabaseContext db = new DatabaseContext();
        public BillboardTypeRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<BillboardType> GetListAll(bool includeDeleted = false)
        {
            bool dataDeleted = includeDeleted;

            if (dataDeleted)
            {
                var dataQuery = from x in db.BillboardType
                                select x;

                return dataQuery;
            }
            else
            {
                var dataQuery = from x in db.BillboardType
                                where x.DeletedDate == null
                                select x;

                return dataQuery;
            }
        }

        public Response Insert(BillboardType data)
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

        public Response Update(BillboardType data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data != null)
                {
                    var find = db.BillboardType.Where(x => x.ID == data.ID).FirstOrDefault();

                    if (find != null)
                    {
                        find.Kode = data.Kode;
                        find.Type = data.Type;
                        find.LastUpdateDate = DateTime.Now;
                        find.LastUpdateByUserID = data.LastUpdateByUserID;

                        message = "Save data success";
                        result = true;
                    }
                    else
                    {
                        message = "Billboard type not found";
                        result = true;
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

        public Response DeletedByID(Guid id)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            var data = db.BillboardType.Where(x => x.ID == id).FirstOrDefault();

            if (data != null)
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
