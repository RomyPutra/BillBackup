using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class CartRepository
    {
        DatabaseContext db = new DatabaseContext();
        public CartRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(Cart data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID == Guid.Empty)
                {
                    data.ID = Guid.NewGuid();

                    //db.Add(data);
                    db.Cart.Add(data);

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

        public IQueryable<Cart> GetCartByUserID(Guid userID)
        {
            return db.Cart.Where(x => x.UserID == userID && x.DeletedDate == null);

        }

        public IQueryable<Cart> GetCartID(Guid id)
        {
            return db.Cart.Where(x => x.ID == id && x.DeletedDate == null);
        }


        public Response DeleteCart(List<Guid> data, Guid userID)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                DateTime today = DateTime.Now;
                var find = db.Cart.Where(x => data.Contains(x.ID)).ToList();

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


        public Response UpdateCartAfterBook(Guid cartID, Guid bookID, Guid userID)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                DateTime today = DateTime.Now;
                var find = db.Cart.Where(x => x.ID == cartID).ToList();

                if (find != null)
                {
                    foreach (var x in find)
                    {
                        x.BookID = bookID;
                        x.LastUpdateDate = today;
                        x.LastUpdateByUserID = userID;
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


        public IQueryable<Cart> CheckCartItemExist(Guid userID, DateTime startDate, DateTime endDate, Guid siteID, Guid siteItemID)
        {

            DateTime conStartDate = (DateTime)startDate;
            DateTime conEnDate = (DateTime)endDate;

            //var result = db.Cart.Where(x => (conStartDate == x.StartDate && conEnDate == x.EndDate) || (conStartDate < x.EndDate && x.StartDate < conEnDate) && 
            //                                (x.UserID == userID && x.DeletedDate == null && x.SiteID == siteID && x.SiteItemID == siteItemID));

            var result = db.Cart.Where(x => ((conStartDate == x.StartDate && conEnDate == x.EndDate) || (conStartDate < x.EndDate && x.StartDate < conEnDate)) &&
                                               x.UserID == userID && x.DeletedDate == null && x.SiteID == siteID && x.SiteItemID == siteItemID
                                                );

            var tes = result.ToList();

            return result;
                
        }
    }


}
