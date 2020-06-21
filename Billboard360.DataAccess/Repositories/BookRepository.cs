using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;


namespace Billboard360.DataAccess.Repositories
{
    public class BookRepository
    {
        DatabaseContext db = new DatabaseContext();
        public BookRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(Book data)
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

        public IQueryable<Book> GetBookByUserID(Guid userID)
        {
            return db.Book.Where(x => x.UserID == userID && x.DeletedDate == null);

        }

        public IQueryable<Book> GetBookByID(Guid id)
        {
            return db.Book.Where(x => x.ID == id && x.DeletedDate == null);
        } 

        public Response UpdatePurchase(List<Guid> listBookID, Guid purchaseID)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                var find = db.Book.Where(x => listBookID.Contains(x.ID)).ToList();

                if(find != null && find.Count > 0)
                {
                    foreach (var x in find)
                    {
                        x.PaymentID = purchaseID;
                    }

                    db.SaveChanges();

                    message = "Save data success";
                    result = true;
                }

                res.ID = purchaseID;
                res.Message = message;
                res.Result = result;

                return res;

            }
            catch(Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;

                return res;
            }            
        }

        public Response UpdateApproval(Guid bookID, Guid userID, int value)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                var find = db.Book.Where(x => x.ID == bookID && x.DeletedDate == null).FirstOrDefault();

                if (find != null)
                {
                    find.StatusApproval = value;
                    find.LastUpdateByUserID = userID;
                    find.LastUpdateDate = DateTime.Now;

                    db.SaveChanges();

                    string resultResponse = value == 1 ? "Approve" : value == 2 ? "Reject" : "on Progress";

                    message = "Booking telah di " + resultResponse;
                    result = true;
                }

                res.ID = bookID;
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

        public Response DeleteBook(List<Guid> data, Guid userID)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                DateTime today = DateTime.Now;
                var find = db.Book.Where(x => data.Contains(x.ID)).ToList();

                if (find != null && find.Count > 0)
                {
                    foreach (var x in find)
                    {
                        x.DeletedByUserID = userID;
                        x.DeletedDate = today;
                    }

                    db.SaveChanges();

                    message = "Delete data success";
                    result = true;
                }

                res.ID = Guid.Empty;
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
