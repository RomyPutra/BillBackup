using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class PurchaseRepository
    {
        protected readonly DatabaseContext db;

        public PurchaseRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response Insert(Payment data)
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

        public IQueryable<Payment> GetPaymentByInvoiceNumber(string invoiceNumber)
        {
            return db.Payment.Where(x => x.InvoiceNo == invoiceNumber && x.DeletedDate == null);
        }
    }
}
