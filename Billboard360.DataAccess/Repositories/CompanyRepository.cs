using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class CompanyRepository
    {
        DatabaseContext db = new DatabaseContext();
        public CompanyRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(Company data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if(data.ID != Guid.Empty)
                {
                    db.Set<Company>().Add(data);

                    if(isCommit)
                    {
                        db.SaveChanges();

                        message = "Save Data Company success";
                        result = true;

                        res.ID = data.ID;
                    }
                    
                }

                res.Message = message;
                res.Result = result;

                return res;

            }catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;

                return res;
            }
        }

        public Response Update(Company data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            var findData = db.Company.Where(x => x.UserID == data.UserID && x.DeletedDate == null).FirstOrDefault();

            if(findData != null)
            {
                findData.Email = data.Email;
                findData.Note = data.Note;
                findData.NPWP = data.NPWP;
                findData.Website = data.Website;
                findData.Alamat = data.Alamat;
                findData.CompanyName = data.CompanyName;
                findData.Kategory = data.Kategory;

                if(isCommit)
                {
                    db.SaveChanges();

                    message = "Save Data Company success";
                    result = true;
                }
            }

            res.Message = message;
            res.Result = result;

            return res;
        }
    }
}
