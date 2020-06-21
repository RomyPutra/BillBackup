using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Billboard360.DataAccess.Repositories
{
    public class BookDetailRepository
    {
        DatabaseContext db = new DatabaseContext();
        public BookDetailRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(BookDetail data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID == Guid.Empty)
                {
                    data.ID = Guid.NewGuid();

                    db.BookDetail.Add(data);

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

        public Response UpdateApproval(Guid bookDetailID, Guid userID, int value, double hargaFinal, string note)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                var find = db.BookDetail.Where(x => x.ID == bookDetailID && x.DeletedDate == null).FirstOrDefault();

                if (find != null)
                {
                    find.StatusApprovalPerBillboard = value;
                    find.LastUpdateByUserID = userID;
                    find.LastUpdateDate = DateTime.Now;
                    find.FinalPrice = hargaFinal;
                    find.Note = note;

                    db.SaveChanges();

                    string resultResponse = value == 1 ? "Approve" : value == 2 ? "Reject" : "on Progress";

                    message = "Booking telah di " + resultResponse;
                    result = true;
                }

                res.ID = bookDetailID;
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

        public IQueryable<BookDetail> GetBookDetailByID(Guid bookDetailID)
        {
            return db.BookDetail.Where(x => x.ID == bookDetailID);
        }

        public Response DeleteItemBook(List<Guid> data, Guid userID)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                DateTime today = DateTime.Now;
                var find = db.BookDetail.Where(x => data.Contains(x.ID)).ToList();

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

        public IQueryable<BookDetail> CheckAvailableSite(DateTime? startDate, DateTime? endDate)
        {

            DateTime conStartDate = (DateTime)startDate;
            DateTime conEnDate = (DateTime)endDate;

            var result = db.BookDetail.Where(x => (x.StartDate.Date > conStartDate.Date && x.EndDate.Date > x.StartDate.Date) && (x.StartDate.Date > conEnDate.Date && x.EndDate.Date > conEnDate.Date));

            return result;
        }
    }
}
