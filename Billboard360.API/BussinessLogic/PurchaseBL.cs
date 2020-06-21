using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;
using System.Runtime.InteropServices.ComTypes;
using Billboard360.API.Models.Enums;

namespace Billboard360.API.BussinessLogic
{
    public class PurchaseBL
    {
        protected readonly DatabaseContext db;

        public PurchaseBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public PurchaseOutputModel Process(PurchaseInputModel data)
        {
            PurchaseRepository repo = new PurchaseRepository(db);

            Payment temp = new Payment();

            DateTime today = DateTime.Now;

            temp.Diskon = data.Diskon;
            temp.InvoiceNo = GenerateInvoice();
            temp.UserID = data.UserID;
            temp.TotalPrice = data.GrandTotal;
            temp.SubTotalPrice = data.SubTotal;
            temp.CreateByUserID = data.UserID;
            temp.CreateDate = today;
            temp.TotalPaid = data.UangMuka;
            temp.PPNProsen = data.PPNProsen;
            temp.isLunas = data.GrandTotal - data.UangMuka == 0 ? true : false;

            if (data.BookID.Count() > 0)
            {
                foreach (var x in data.BookID)
                {
                    Guid bookID = new Guid();
                    bookID = Guid.Parse(x);
                    temp.BookID = bookID;
                    //keterangan user bayar full payment atau DP

                    var paymentTypeQuery = (from p in db.Payment
                                            join m in db.MidTransLog on p.ID equals m.PaymentID
                                            where p.BookID == bookID && m.MidTransStatus != 0
                                            select new
                                            {
                                                m
                                            });

                    var countPaymentType = paymentTypeQuery.Count();

                    temp.PaymentType = countPaymentType == 0 && data.GrandTotal - data.UangMuka == 0 ? (int)TransactionTypeEnum.FullPayment :
                                        countPaymentType < 1 && data.GrandTotal - data.UangMuka != 0 ? (int)TransactionTypeEnum.DP : (int)TransactionTypeEnum.FullPayment;
                }
            }



            var res = repo.Insert(temp);

            PurchaseOutputModel output = new PurchaseOutputModel();

            if (res.ID != Guid.Empty)
            {
                output.IDTransaction = res.ID;
                output.InvoiceNo = temp.InvoiceNo;

                List<Guid> listBookID = new List<Guid>();

                foreach (var x in data.BookID)
                {
                    listBookID.Add(Guid.Parse(x));
                }

                BookRepository bookRepo = new BookRepository(db);

                bookRepo.UpdatePurchase(listBookID, res.ID);

            }

            return output;
        }


        private string GenerateInvoice()
        {
            string result = "";

            DateTime today = DateTime.Now;
            var dbCount = db.Payment.Where(x => x.CreateDate.Date == today.Date).ToList().Count();

            string headInv = "INV";
            string tahun = DateTime.Now.Year.ToString();
            string bulan = DateTime.Now.Month.ToString();
            string tglJam = DateTime.Now.Date.ToString();
            int count = dbCount + 1;
            result = headInv + "/" + tahun + "/" + bulan + "/" + tglJam + "/" + count;

            return result;
        }
    }
}
