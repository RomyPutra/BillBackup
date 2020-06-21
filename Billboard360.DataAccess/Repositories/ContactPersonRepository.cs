using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class ContactPersonRepository
    {
        DatabaseContext db = new DatabaseContext();
        public ContactPersonRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(ContactPerson data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {
                    data.ID = Guid.NewGuid();

                    db.Set<ContactPerson>().Add(data);

                    if (isCommit)
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

            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;

                return res;
            }
        }

        public Response Update(ContactPerson data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            var findData = db.ContactPerson.Where(x => x.UserID == data.UserID && x.DeletedDate == null).FirstOrDefault();

            if(findData != null)
            {
                findData.Email = data.Email;
                findData.Name = data.Name;

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
