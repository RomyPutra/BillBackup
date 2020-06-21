using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;
using Billboard360.Helper.Common;
using Billboard360.API.Models.Enums;

namespace Billboard360.API.BussinessLogic
{
    public class BookBL
    {
        protected readonly DatabaseContext db;

        public BookBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        private string GenerateBookNo()
        {
            var prefix = "BK";
            var curMonth = DateTime.Now.Month;
            var curYear = DateTime.Now.Year;
            var curDate = DateTime.Now.Day;

            Random rnd = new Random();

            var randomNo = rnd.Next(1000);

            return prefix + "/" + curYear + "/" + curMonth + "/" + curDate + "/" + randomNo;
        }

        public AddBookOutputModel Save(AddToBookListInputModel data)
        {
            BookRepository repo = new BookRepository(db);

            BookDetailRepository detailRepo = new BookDetailRepository(db);

            Book temp = new Book();


            DateTime today = DateTime.Now;

            temp.CompanyID = db.Company.Where(x => x.UserID == data.Data[0].UserID).Select(x => x.ID).FirstOrDefault();
            temp.PaymentID = Guid.Empty;

            temp.UserID = data.Data[0].UserID;

            temp.CreateDate = today;
            temp.CreateByUserID = data.Data[0].UserID;
            temp.StatusApproval = 0;
            string bookNo = GenerateBookNo();
            temp.BookNo = bookNo;

            var res = repo.Insert(temp);

            foreach (var cur in data.Data)
            {
                //Ngakali biar mobile tidak usah insert companyID
                BookDetail tempDetail = new BookDetail();

                tempDetail.PriceID = cur.SitePriceID;
                tempDetail.Price = cur.Price;
                tempDetail.Qty = cur.Qty;
                tempDetail.TotalPerItem = cur.TotalPerItem;
                tempDetail.StartDate = cur.StartDate;
                tempDetail.EndDate = cur.EndDate;
                tempDetail.SiteID = cur.SiteID;
                tempDetail.SiteItemID = cur.SiteItemID;
                tempDetail.CreateDate = today;
                tempDetail.CreateByUserID = cur.UserID;
                tempDetail.StatusApprovalPerBillboard = 0;
                tempDetail.BookID = res.ID;

                detailRepo.Insert(tempDetail);

                if (data.isFromCart)
                {
                    CartRepository cartRepo = new CartRepository(db);
                    Guid cartID = (Guid)cur.CartID;
                    cartRepo.UpdateCartAfterBook(cartID, res.ID, data.Data[0].UserID);
                }
            }


            AddBookOutputModel output = new AddBookOutputModel();
            output.SiteID = data.Data[0].SiteID;
            output.BookID = Guid.Empty; //IKI GA EROH MBOH NGELU NDAS KAKEAN REVISI LAST MINUTE :( //.ID;
            output.BookNo = bookNo;

            return output;

        }


        public AddItemToCurrentBookOutputModel ChangeItemCurrentBook(AddItemToCurrentBookListInputModel data)
        {
            BookRepository repo = new BookRepository(db);

            BookDetailRepository detailRepo = new BookDetailRepository(db);

            Book temp = new Book();

            //delete dulu
            //Delete Start
            List<Guid> bookDetailIDCollection = new List<Guid>();
            bookDetailIDCollection.Add(data.BookDetailIDForDeleted);
            detailRepo.DeleteItemBook(bookDetailIDCollection, data.Data[0].UserID);
            //Delete End

            //Masukkan Data baru
            //Change Start
            DateTime today = DateTime.Now;

            foreach (var cur in data.Data)
            {
                //Ngakali biar mobile tidak usah insert companyID
                BookDetail tempDetail = new BookDetail();

                tempDetail.PriceID = cur.SitePriceID;
                tempDetail.Price = cur.Price;
                tempDetail.Qty = cur.Qty;
                tempDetail.TotalPerItem = cur.TotalPerItem;
                tempDetail.StartDate = cur.StartDate;
                tempDetail.EndDate = cur.EndDate;
                tempDetail.SiteID = cur.SiteID;
                tempDetail.SiteItemID = cur.SiteItemID;
                tempDetail.CreateDate = today;
                tempDetail.CreateByUserID = cur.UserID;
                tempDetail.StatusApprovalPerBillboard = 0;
                tempDetail.BookID = data.BookID;

                detailRepo.Insert(tempDetail);

            }
            //Change End

            AddItemToCurrentBookOutputModel output = new AddItemToCurrentBookOutputModel();
            output.SiteID = data.Data[0].SiteID;
            output.BookID = data.BookID;
            output.BookNo = db.Book.Where(x => x.ID == data.BookID).FirstOrDefault().BookNo;

            return output;

        }

        public AddToCartOutputModel SaveCart(AddBookInputModel data)
        {
            AddToCartOutputModel output = new AddToCartOutputModel();




            CartRepository repo = new CartRepository(db);

            var checkCart = repo.CheckCartItemExist(data.UserID, data.StartDate, data.EndDate, data.SiteID, data.SiteItemID).FirstOrDefault();

            if (checkCart != null)
            {
                output.BookID = Guid.Empty;
                output.SiteID = data.SiteID;
            }
            else
            {
                Cart temp = new Cart();

                DateTime today = DateTime.Now;

                temp.PriceID = data.SitePriceID;
                //Ngakali biar mobile tidak usah insert companyID
                temp.CompanyID = db.Company.Where(x => x.UserID == data.UserID).Select(x => x.ID).FirstOrDefault();
                temp.BookID = Guid.Empty;
                temp.SiteID = data.SiteID;
                temp.SiteItemID = data.SiteItemID;
                temp.UserID = data.UserID;
                temp.Price = data.Price;
                temp.Qty = data.Qty;
                temp.TotalPerItem = data.TotalPerItem;
                temp.StartDate = data.StartDate;
                temp.EndDate = data.EndDate;
                temp.CreateDate = today;
                temp.CreateByUserID = data.UserID;
                temp.StatusApproval = 0;

                var res = repo.Insert(temp);

                output.SiteID = data.SiteID;
                output.BookID = res.ID;
            }


            return output;

        }

        public ViewArrayCartResponse ViewCart(ViewCartInputModel data)
        {
            ViewArrayBookResponse res = new ViewArrayBookResponse();

            var temp = db.Cart.Where(x => x.UserID == data.UserID && x.DeletedDate == null && x.BookID == Guid.Empty).ToList();

            List<ViewResultCartModel> listViewResult = new List<ViewResultCartModel>();

            if (temp.Count > 0)
            {
                //Insert data
                foreach (var sel in temp)
                {
                    var siteQuery = (from s in db.TitikLokasi
                                     where s.ID == sel.SiteID && s.DeletedDate == null
                                     select new SiteDetailBookOutputModel
                                     {
                                         NoBillboard = s.NoBillboard,
                                         Pulau = s.Pulau,
                                         Kota = s.Kota,
                                         Provinsi = s.Provinsi,
                                         Cabang = s.Cabang,
                                         HorV = s.HorV,
                                         Tipe = s.Type,
                                         Longitude = s.Longitude,
                                         Latitude = s.Latitude,
                                         RateScoreAverage = s.RateScoreAverage,
                                         Item = db.TitikLokasiDetail.Where(x => x.ID == sel.SiteItemID)
                                                            .Select(x => new SiteDetailItemBookOutputModel()
                                                            {
                                                                SiteID = x.TitikLokasiID,
                                                                SiteItemID = x.ID,
                                                                ArahLokasi = x.ArahLokasi,
                                                                Lebar = x.Lebar,
                                                                Panjang = x.Panjang,
                                                                Ukuran = x.Panjang + "X" + x.Lebar,
                                                                Image = db.TitikLokasiImage.Where(z => z.TitikLokasiDetailID == sel.SiteItemID)
                                                                                    .Select(z => new SiteImageBookOutputModel()
                                                                                    {
                                                                                        Image = z.PathImage
                                                                                    }).ToList(),
                                                                Price = db.TitikLokasiPrice.Where(z => z.TitikLokasiDetailID == sel.SiteItemID)
                                                                                    .Select(z => new SitePriceBookOutputModel()
                                                                                    {
                                                                                        HargaAkhir = z.HargaAkhir,
                                                                                        HargaAwal = z.HargaAwal,
                                                                                        Kelipatan = z.Kelipatan,
                                                                                        Satuan = z.Dasar,
                                                                                        PriceID = z.ID
                                                                                    }).FirstOrDefault()

                                                            }).FirstOrDefault()
                                     });

                    ViewCartSiteSectionkResultModel siteSection = new ViewCartSiteSectionkResultModel();
                    siteSection.Detail = siteQuery.FirstOrDefault();

                    var bookQuery = (from x in db.Cart
                                     where x.ID == sel.ID
                                     select new ViewResultCartModel
                                     {
                                         CartID = x.ID,
                                         CompanyID = x.CompanyID,
                                         UserID = x.UserID,
                                         StatusApproval = x.StatusApproval == 1 ? "Approve" : x.StatusApproval == 2 ? "Reject" : "On Progress",
                                         StartDate = x.StartDate,
                                         EndDate = x.EndDate,
                                         Price = x.Price,
                                         Qty = x.Qty,
                                         TotalHargaPerItem = x.TotalPerItem
                                     }).FirstOrDefault();

                    bookQuery.Site = siteSection;

                    listViewResult.Add(bookQuery);
                }
            }

            ViewArrayCartResponse last = new ViewArrayCartResponse();
            last.Data = listViewResult;

            return last;

        }

        public DeleteBookOutputModel DeleteCart(DeleteBookInputModel data)
        {
            CartRepository repo = new CartRepository(db);
            var res = repo.DeleteCart(data.BookID, data.UserID);

            DeleteBookOutputModel output = new DeleteBookOutputModel();
            if (res.Result)
            {
                output.Message = "Data telah di hapus";
            }
            else
            {
                output.Message = "Data gagal di hapus";
            }

            return output;
        }

        public ViewArrayBookResponse GetBook(ViewBookInputModel data)
        {
            ViewArrayBookResponse res = new ViewArrayBookResponse();

            var resData = (from b in db.Book
                           where b.UserID == data.UserID && b.DeletedDate == null
                           orderby b.CreateDate descending
                           select new ViewResultBookModel()
                           {
                               CreateDate = b.CreateDate,
                               CompanyID = b.CompanyID,
                               BookNo = b.BookNo,
                               BookID = b.ID,
                               PaymentID = b.PaymentID,
                               //StatusMidtrans = b.PaymentID == Guid.Empty ? string.Empty : db.MidTransLog.Where(x => x.PaymentID == b.PaymentID).Select(x => x.MidTransStatus).FirstOrDefault() == 0 ? "Failed" :
                               //                 db.MidTransLog.Where(x => x.PaymentID == b.PaymentID).Select(x => x.MidTransStatus).FirstOrDefault() == 1 ? "Success" : "Pending",
                               StatusApproval = db.BookDetail.Count(z => z.StatusApprovalPerBillboard == 1 && z.BookID == b.ID && z.DeletedDate == null) == db.BookDetail.Count(z => z.BookID == b.ID && z.DeletedDate == null) ? "Approve" :
                                                db.BookDetail.Count(z => z.StatusApprovalPerBillboard == 2 && z.BookID == b.ID && z.DeletedDate == null) > 0 ? "Reject" : "On Progress",
                               StatusInt = db.BookDetail.Count(z => z.StatusApprovalPerBillboard == 1 && z.BookID == b.ID && z.DeletedDate == null) == db.BookDetail.Count(z => z.BookID == b.ID && z.DeletedDate == null) ? 1 :
                                                db.BookDetail.Count(z => z.StatusApprovalPerBillboard == 2 && z.BookID == b.ID && z.DeletedDate == null) > 0 ? 2 : 0,
                               UserID = b.UserID,

                               ListBookDetail = db.BookDetail.Where(z => z.BookID == b.ID && z.DeletedDate == null).Select(z => new ViewBookDetailSection()
                               {
                                   BookDetailID = z.ID,
                                   Site = db.TitikLokasi.Where(s => s.ID == z.SiteID && s.DeletedDate == null).Select(s => new SiteDetailBookOutputModel()
                                   {

                                       NoBillboard = s.NoBillboard,
                                       Pulau = s.Pulau,
                                       Kota = s.Kota,
                                       Provinsi = s.Provinsi,
                                       Cabang = s.Cabang,
                                       HorV = s.HorV,
                                       Tipe = s.Type,
                                       Longitude = s.Longitude,
                                       Latitude = s.Latitude,
                                       RateScoreAverage = s.RateScoreAverage,
                                       Item = db.TitikLokasiDetail.Where(x => x.ID == z.SiteItemID)
                                                                .Select(x => new SiteDetailItemBookOutputModel()
                                                                {
                                                                    SiteID = x.TitikLokasiID,
                                                                    SiteItemID = x.ID,
                                                                    ArahLokasi = x.ArahLokasi,
                                                                    Lebar = x.Lebar,
                                                                    Panjang = x.Panjang,
                                                                    Ukuran = x.Panjang + "X" + x.Lebar,
                                                                    Image = db.TitikLokasiImage.Where(y => y.TitikLokasiDetailID == z.SiteItemID)
                                                                                        .Select(i => new SiteImageBookOutputModel()
                                                                                        {
                                                                                            Image = i.PathImage
                                                                                        }).ToList(),
                                                                    Price = db.TitikLokasiPrice.Where(y => y.TitikLokasiDetailID == z.SiteItemID)
                                                                                        .Select(i => new SitePriceBookOutputModel()
                                                                                        {
                                                                                            HargaAkhir = i.HargaAkhir,
                                                                                            HargaAwal = i.HargaAwal,
                                                                                            Kelipatan = i.Kelipatan,
                                                                                            Satuan = i.Dasar,
                                                                                            PriceID = i.ID
                                                                                        }).FirstOrDefault()

                                                                }).FirstOrDefault()

                                   }).FirstOrDefault(),
                                   StatusPerItem = z.StatusApprovalPerBillboard == 1 ? "Approve" : z.StatusApprovalPerBillboard == 2 ? "Reject" : "On Progress",
                                   Message = z.StatusApprovalPerBillboard != 1 ? "Booking ini ditolak admin, hubungi admin!" : "Deal",
                                   Price = z.Price,
                                   Qty = z.Qty,
                                   TotalHargaPerItem = z.TotalPerItem,
                                   StartDate = z.StartDate,
                                   EndDate = z.EndDate,
                               }).ToList(),
                           });




            if (data.Status != "")
            {
                int status = Convert.ToInt32(data.Status);

                resData = (from b in resData
                           where b.StatusInt == status
                           select new ViewResultBookModel
                           {
                               CreateDate = b.CreateDate,
                               BookNo = b.BookNo,
                               BookID = b.BookID,
                               UserID = b.UserID,
                               CompanyID = b.CompanyID,
                               PaymentID = b.PaymentID,
                               StatusApproval = b.StatusApproval,
                               StatusInt = b.StatusInt,
                               ListBookDetail = b.ListBookDetail
                           });
            }
            else if (data.BookID != null && data.BookID != Guid.Empty)
            {
                resData = (from b in resData
                           where b.BookID == data.BookID
                           select new ViewResultBookModel
                           {
                               CreateDate = b.CreateDate,
                               BookNo = b.BookNo,
                               BookID = b.BookID,
                               UserID = b.UserID,
                               CompanyID = b.CompanyID,
                               PaymentID = b.PaymentID,
                               StatusApproval = b.StatusApproval,
                               StatusInt = b.StatusInt,
                               ListBookDetail = b.ListBookDetail
                           });
            }

            var finalRes = resData.ToList();

            //check sudah pembayaran atau belum

            foreach (var x in finalRes)
            {
                if (x.PaymentID != Guid.Empty)
                {
                    var listPaymentID = db.Payment.Where(z => z.BookID == x.BookID).Select(z => z.ID).ToList();

                    var listMidTransLog = (from m in db.MidTransLog
                                           join p in db.Payment on m.PaymentID equals p.ID
                                           where p.BookID == x.BookID
                                           orderby p.CreateDate
                                           select new ViewPaymentMidtransLogBookOuputModel()
                                           {
                                               BankName = m.BankName,
                                               MidtransLogID = m.ID,
                                               VANumber = m.VANumber,
                                               PaidAmount = db.Payment.Where(o => o.ID == m.PaymentID).Select(z => z.TotalPaid).FirstOrDefault(),
                                               //Di ubah jadi per transaksinya karena setiap per booking punya lebih dari 1x transaksi midtrans
                                               //StatusMidTrans = db.MidTransLog.Where(o => o.PaymentID == x.PaymentID).Select(z => z.MidTransStatus).FirstOrDefault() == 0 ? "Failed" :
                                               //                 db.MidTransLog.Where(o => o.PaymentID == x.PaymentID).Select(z => z.MidTransStatus).FirstOrDefault() == 1 ? "Success" : "Pending",
                                               //StatusMidTransInt = db.MidTransLog.Where(o => o.PaymentID == x.PaymentID).Select(z => z.MidTransStatus).FirstOrDefault() == 0 ? 0 :
                                               //                 db.MidTransLog.Where(o => o.PaymentID == x.PaymentID).Select(z => z.MidTransStatus).FirstOrDefault() == 1 ? 1 : 2,
                                               StatusMidTrans = m.MidTransStatus == 0 ? "Failed" : m.MidTransStatus == 1 ? "Success" : "Pending",
                                               StatusMidTransInt = m.MidTransStatus,
                                               PaymentType = p.PaymentType == 1 ? "Full Payment" : "DP",
                                               PaymentTypeInt = p.PaymentType,
                                               TransactionDate = p.CreateDate
                                           }).ToList();
                    x.ListMidtransLog = listMidTransLog;

                    var isLunas = db.Payment.Where(z => z.ID == x.PaymentID).Select(z => z.isLunas).FirstOrDefault() == true ? true : false;
                    x.TransactionType = db.Payment.Where(z => z.ID == x.PaymentID).Select(z => z.isLunas).FirstOrDefault() == true ? "Full Payment" : "Down Payment";
                    x.TransactionTypeInt = listMidTransLog.Count > 1 && isLunas ? (int)TransactionTypeEnum.DP : listMidTransLog.Count > 1 && isLunas == false ? (int)TransactionTypeEnum.DP : (int)TransactionTypeEnum.FullPayment;
                }
                else
                {
                    x.TransactionType = "";
                    x.ListMidtransLog = null;
                    x.TransactionType = null;
                }

            }

            double TotalHarga = 0;

            foreach (var x in finalRes)
            {
                foreach (var y in x.ListBookDetail)
                {
                    x.TotalHargaBook += y.TotalHargaPerItem;
                    TotalHarga = x.TotalHargaBook;
                }
            }

            res.Data = finalRes;

            //TAMBAHKAN DENGAN PAJAK 10%
            var tax = TotalHarga * 10 / 100;
            res.TotalBayar = TotalHarga + tax;

            return res;
        }

        public ViewArrayBookResponse GetHistoryNew(ViewBookInputModel data)
        {
            ViewArrayBookResponse res = new ViewArrayBookResponse();

            ViewArrayBookResponse last = new ViewArrayBookResponse();

            //DIGANTI MANEH SAMPE BONGKOH
            //LEK BUTUH PENYESUAIAN CODE ENGKOK SEK BOS
            var temp = (from b in db.Book
                        where b.UserID == data.UserID && b.DeletedDate == null && b.PaymentID == Guid.Empty
                        select new ViewListBook
                        {
                            BookID = b.ID,
                            UserID = b.UserID,
                            PaymentID = b.PaymentID,
                            Status = b.StatusApproval,
                            DetailBook = db.BookDetail.Where(x => x.BookID == b.ID).Select(x => new ViewListDetailBook()
                            {
                                BookID = x.BookID,
                                BookDetailID = x.ID,
                                StatusApprovalPerBillboard = x.StatusApprovalPerBillboard,
                                EndDate = x.EndDate,
                                StartDate = x.StartDate,
                                Price = x.Price,
                                Qty = x.Qty,
                                SiteItemID = x.SiteItemID,
                                SiteID = x.SiteID,
                                TotalPerItem = x.Price * x.Qty,
                            }).ToList(),
                        });

            if (data.Status != "")
            {
                int status = Convert.ToInt32(data.Status);

                temp = (from b in temp
                        where b.Status == status
                        select new ViewListBook
                        {
                            BookID = b.BookID,
                            UserID = b.UserID,
                            PaymentID = b.PaymentID,
                            Status = b.Status,
                            DetailBook = db.BookDetail.Where(x => x.BookID == b.BookID).Select(x => new ViewListDetailBook()
                            {
                                BookID = x.BookID,
                                BookDetailID = x.ID,
                                StatusApprovalPerBillboard = x.StatusApprovalPerBillboard,
                                EndDate = x.EndDate,
                                StartDate = x.StartDate,
                                Price = x.Price,
                                Qty = x.Qty,
                                SiteItemID = x.SiteItemID,
                                SiteID = x.SiteID,
                                TotalPerItem = x.Price * x.Qty,
                            }).ToList(),
                        });
            }
            else if (data.BookID != null && data.BookID != Guid.Empty)
            {
                temp = (from b in temp
                        where b.BookID == data.BookID
                        select new ViewListBook
                        {
                            BookID = b.BookID,
                            UserID = b.UserID,
                            PaymentID = b.PaymentID,
                            Status = b.Status,
                            DetailBook = db.BookDetail.Where(x => x.BookID == b.BookID).Select(x => new ViewListDetailBook()
                            {
                                BookID = x.BookID,
                                BookDetailID = x.ID,
                                StatusApprovalPerBillboard = x.StatusApprovalPerBillboard,
                                EndDate = x.EndDate,
                                StartDate = x.StartDate,
                                Price = x.Price,
                                Qty = x.Qty,
                                SiteItemID = x.SiteItemID,
                                SiteID = x.SiteID,
                                TotalPerItem = x.Price * x.Qty,
                            }).ToList(),
                        });
            }


            int pageNumber = data.PageNumber - 1;
            temp = temp.Skip(pageNumber * data.PageSize).Take(data.PageSize);

            var reTemp = temp.ToList();

            List<ViewResultBookModel> listViewResult = new List<ViewResultBookModel>();

            foreach (var lop1 in reTemp)
            {
                if (reTemp != null && lop1.DetailBook.Count > 0)
                {
                    //Insert data
                    foreach (var sel in lop1.DetailBook)
                    {
                        var siteQuery = (from s in db.TitikLokasi
                                         where s.ID == sel.SiteID && s.DeletedDate == null
                                         select new SiteDetailBookOutputModel
                                         {
                                             NoBillboard = s.NoBillboard,
                                             Pulau = s.Pulau,
                                             Kota = s.Kota,
                                             Provinsi = s.Provinsi,
                                             Cabang = s.Cabang,
                                             HorV = s.HorV,
                                             Tipe = s.Type,
                                             Longitude = s.Longitude,
                                             Latitude = s.Latitude,
                                             RateScoreAverage = s.RateScoreAverage,
                                             Item = db.TitikLokasiDetail.Where(x => x.ID == sel.SiteItemID)
                                                                .Select(x => new SiteDetailItemBookOutputModel()
                                                                {
                                                                    ArahLokasi = x.ArahLokasi,
                                                                    Lebar = x.Lebar,
                                                                    Panjang = x.Panjang,
                                                                    Ukuran = x.Panjang + "X" + x.Lebar,
                                                                    Image = db.TitikLokasiImage.Where(z => z.TitikLokasiDetailID == sel.SiteItemID)
                                                                                        .Select(z => new SiteImageBookOutputModel()
                                                                                        {
                                                                                            Image = z.PathImage
                                                                                        }).ToList(),
                                                                    Price = db.TitikLokasiPrice.Where(z => z.TitikLokasiDetailID == sel.SiteItemID)
                                                                                        .Select(z => new SitePriceBookOutputModel()
                                                                                        {
                                                                                            HargaAkhir = z.HargaAkhir,
                                                                                            HargaAwal = z.HargaAwal,
                                                                                            Kelipatan = z.Kelipatan,
                                                                                            Satuan = z.Dasar,
                                                                                            PriceID = z.ID
                                                                                        }).FirstOrDefault()

                                                                }).FirstOrDefault()
                                         });

                        SiteDetailBookOutputModel siteSection = new SiteDetailBookOutputModel();
                        siteSection = siteQuery.FirstOrDefault();

                        var bookQuery = (from x in db.Book
                                         where x.ID == sel.BookID
                                         select new ViewResultBookModel
                                         {
                                             BookID = x.ID,
                                             CompanyID = x.CompanyID,
                                             UserID = x.UserID,
                                             CreateDate = x.CreateDate,
                                             StatusApproval = db.BookDetail.Count(z => z.StatusApprovalPerBillboard == 1) >= db.BookDetail.Count(z => z.BookID == x.ID) ? "Approve" :
                                                             db.BookDetail.Count(z => z.StatusApprovalPerBillboard == 2) > 0 ? "Reject" : "On Progress",
                                             ListBookDetail = db.BookDetail.Where(z => z.BookID == x.ID).Select(z => new ViewBookDetailSection()
                                             {
                                                 BookDetailID = z.ID,
                                                 Site = siteSection,
                                                 StatusPerItem = z.StatusApprovalPerBillboard == 1 ? "Approve" : z.StatusApprovalPerBillboard == 2 ? "Reject" : "On Progress",
                                                 Message = z.StatusApprovalPerBillboard != 1 ? "Booking ini ditolak admin, hubungi admin!" : "Deal",
                                                 Price = z.Price,
                                                 Qty = z.Qty,
                                                 TotalHargaPerItem = z.TotalPerItem,
                                                 StartDate = z.StartDate,
                                                 EndDate = z.EndDate
                                             }).ToList()
                                         }).FirstOrDefault();

                        //bookQuery.Site = siteSection;
                        double totalHarga = 0;

                        foreach (var x in bookQuery.ListBookDetail)
                        {
                            totalHarga += x.TotalHargaPerItem;
                        }

                        bookQuery.TotalHargaBook = totalHarga;

                        listViewResult.Add(bookQuery);
                    }
                }


                last.Data = listViewResult;

                //Calculate total bayar
                foreach (var i in lop1.DetailBook)
                {
                    last.TotalBayar += i.Price;
                }
            }


            var tax = last.TotalBayar * 10 / 100;
            last.TotalBayar = last.TotalBayar + tax;
            return last;

        }


        public List<NotificationBookOutputModel> GetExpiredBook(NotificationBookInputModel data)
        {
            var res = (from x in db.BookDetail
                       join s in db.TitikLokasi on x.SiteID equals s.ID
                       where x.EndDate < data.ExpiredDate
                       select new NotificationBookOutputModel()
                       {
                           BillboardType = s.Type,
                           Cabang = s.Cabang,
                           NoBillboard = s.NoBillboard,
                           HorV = s.HorV,
                           Kota = s.Kota,
                           Provinsi = s.Provinsi,
                           Pulau = s.Pulau,
                           StartDate = x.StartDate,
                           EndDate = x.EndDate,
                       }).ToList();

            return res;
        }

        public DeleteBookOutputModel Delete(DeleteBookInputModel data)
        {
            BookRepository repo = new BookRepository(db);
            var res = repo.DeleteBook(data.BookID, data.UserID);

            DeleteBookOutputModel output = new DeleteBookOutputModel();
            if (res.Result)
            {
                output.Message = "Data telah di hapus";
            }
            else
            {
                output.Message = "Data gagal di hapus";
            }

            return output;
        }

        public DeleteItemBookOutputModel DeleteItem(DeleteItemBookInputModel data)
        {
            BookDetailRepository repo = new BookDetailRepository(db);
            var res = repo.DeleteItemBook(data.BookID, data.UserID);

            DeleteItemBookOutputModel output = new DeleteItemBookOutputModel();
            if (res.Result)
            {
                output.Message = "Data telah di hapus";
            }
            else
            {
                output.Message = "Data gagal di hapus";
            }

            return output;
        }

        public ApprovalBookOutputModel ApprovalBook(ApprovalBookInputModel data)
        {
            BookDetailRepository bookDetailRepo = new BookDetailRepository(db);

            var res = bookDetailRepo.UpdateApproval(data.BookDetailID, data.UserID, data.Value, data.HargaFinal, data.Reason);


            ApprovalBookOutputModel output = new ApprovalBookOutputModel();

            var bookID = bookDetailRepo.GetBookDetailByID(data.BookDetailID).FirstOrDefault().BookID;

            BookRepository bookRepo = new BookRepository(db);

            bookRepo.UpdateApproval(bookID, data.UserID, data.Value);

            output.Message = res.Message;
            output.BookID = data.BookDetailID;

            return output;
        }

        public string BuilBookReceiptToAdmin(AddToBookListInputModel data)
        {
            UserRepository userRepo = new UserRepository(db);
            var userInfo = userRepo.FindByID(data.Data[0].UserID).FirstOrDefault();

            SiteRepository siteRepo = new SiteRepository(db);
            var siteInfo = siteRepo.GetSiteByID(data.Data[0].SiteID).FirstOrDefault();

            string headLiner = "Billboardindo";
            string address = "Gedung Graha Media <br/>  Jl.Blora No. 8-10  <br/>  Jakarta";
            string greetingMsg = "Terima kasih " + userInfo.FirstName + " " + userInfo.LastName + " telah melakukan booking,";
            string thxMessage = "admin kami akan segera mengkonfirmasi untuk ketersediannya.";
            string mediaOwnerName = userInfo.FirstName + " " + userInfo.LastName;
            string noBillboard = siteInfo.NoBillboard;
            string priceItem = data.Data[0].Price.ToString();
            string subThxMessage = "Booking telah dilakukan"; //"Booking <b>" + noBillboard + "</b> telah dilakukan.";

            string itemHtml = "";
            itemHtml += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px;'>";

            foreach (var x in data.Data)
            {
                var tempData = (from t in db.TitikLokasi
                                where t.ID == x.SiteID
                                select new
                                {
                                    NoBillboard = t.NoBillboard,
                                    Price = x.Price,
                                }).FirstOrDefault();



                itemHtml += "<div style='width: 100px; float: left;'>" + tempData.NoBillboard + "</div>";
                itemHtml += "<div style='text-align: right; padding-right: 10px;'>" + tempData.Price.ToRupiah() + "</div> <br/>";
                itemHtml += "<div>" + x.StartDate + " - " + x.EndDate + "</div><br/>";
            }

            double grandTotal = 0;

            foreach (var x in data.Data)
            {
                grandTotal += x.Price;
            }

            string total = "";
            total = grandTotal.ToRupiah();

            string content = "";
            content += "<html>";
            content += "<head></head>";
            content += "<body style='font-family:Trebuchet MS, Lucida Sans Unicode, Lucida Grande, Lucida Sans, Arial, sans-serif; padding: 10px;'>";
            content += "<div style='width: 450px;'>";
            content += "<div style='background-color:#0E62AC;'>";
            content += "<h3 style='padding: 10px; color: #ffffff;'>" + headLiner + "</h3>";
            content += "</div>";
            content += "<div style='padding: 10px;'>";
            content += "<div id='content'>";
            content += "<p>" + greetingMsg + "</p>";
            content += "<h2>" + thxMessage + "<h2/>";
            content += "<p style='color:#616161;'>" + subThxMessage + "</p>";
            content += "<div>";
            content += "<div style='width:200px; float: left;'>Media Buyer</div>";
            content += "<div style='text-align: left; padding-right: 10px;'>: " + mediaOwnerName + "</div>";
            content += "</div><br/>";
            content += "<div style='background-color:#e0e0e0; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-radius: 5px ;'>";
            content += "<div>";
            content += "<div style='width: 300px; float: left;'><b>Nomor Booking No</b></div>";
            content += "<div style='text-align: right; padding-right: 10px;'><b>BK/20200121/012</b></div>";
            content += "</div>";
            content += "</div>";
            content += itemHtml;
            content += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-top-color: #e0e0e0 ; border-bottom-color: #e0e0e0; border-style: solid; border-right-style:hidden; border-left-style:hidden;'>";
            content += "<div style='width: 300px; float: left; '><b>Total</b>";
            content += "</div>";
            content += "<div style='text-align: right; padding-right: 3px;'><b>" + total + "</b>";
            content += "</div>";
            content += "</div>";
            content += "<br/><br/><br/>";
            content += "</div>";
            content += "<div style='background-color:#0E62AC; color: #ffffff; padding: 10px; text-align: right;'>";
            content += "<b>" + headLiner + "</b><br/>";
            content += address + "</div>";
            content += "</body></html>";

            return content;

        }

        public string BuilBookChangeReceiptToAdmin(AddItemToCurrentBookListInputModel data)
        {
            UserRepository userRepo = new UserRepository(db);
            var userInfo = userRepo.FindByID(data.Data[0].UserID).FirstOrDefault();

            SiteRepository siteRepo = new SiteRepository(db);
            var siteInfo = siteRepo.GetSiteByID(data.Data[0].SiteID).FirstOrDefault();

            string headLiner = "Billboardindo";
            string address = "Gedung Graha Media <br/>  Jl.Blora No. 8-10  <br/>  Jakarta";
            string greetingMsg = "Terima kasih " + userInfo.FirstName + " " + userInfo.LastName + " telah melakukan booking,";
            string thxMessage = "admin kami akan segera mengkonfirmasi untuk ketersediannya.";
            string mediaOwnerName = userInfo.FirstName + " " + userInfo.LastName;
            string noBillboard = siteInfo.NoBillboard;
            string priceItem = data.Data[0].Price.ToString();
            string subThxMessage = "Booking telah dilakukan"; //"Booking <b>" + noBillboard + "</b> telah dilakukan.";

            string itemHtml = "";
            itemHtml += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px;'>";

            foreach (var x in data.Data)
            {
                var tempData = (from t in db.TitikLokasi
                                where t.ID == x.SiteID
                                select new
                                {
                                    NoBillboard = t.NoBillboard,
                                    Price = x.Price,
                                }).FirstOrDefault();



                itemHtml += "<div style='width: 100px; float: left;'>" + tempData.NoBillboard + "</div>";
                itemHtml += "<div style='text-align: right; padding-right: 10px;'>" + tempData.Price.ToRupiah() + "</div> <br/>";
                itemHtml += "<div>" + x.StartDate + " - " + x.EndDate + "</div><br/>";
            }

            double grandTotal = 0;

            foreach (var x in data.Data)
            {
                grandTotal += x.Price;
            }

            string total = "";
            total = grandTotal.ToRupiah();

            string content = "";
            content += "<html>";
            content += "<head></head>";
            content += "<body style='font-family:Trebuchet MS, Lucida Sans Unicode, Lucida Grande, Lucida Sans, Arial, sans-serif; padding: 10px;'>";
            content += "<div style='width: 450px;'>";
            content += "<div style='background-color:#0E62AC;'>";
            content += "<h3 style='padding: 10px; color: #ffffff;'>" + headLiner + "</h3>";
            content += "</div>";
            content += "<div style='padding: 10px;'>";
            content += "<div id='content'>";
            content += "<p>" + greetingMsg + "</p>";
            content += "<h2>" + thxMessage + "<h2/>";
            content += "<p style='color:#616161;'>" + subThxMessage + "</p>";
            content += "<div>";
            content += "<div style='width:200px; float: left;'>Media Buyer</div>";
            content += "<div style='text-align: left; padding-right: 10px;'>: " + mediaOwnerName + "</div>";
            content += "</div><br/>";
            content += "<div style='background-color:#e0e0e0; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-radius: 5px ;'>";
            content += "<div>";
            content += "<div style='width: 300px; float: left;'><b>Nomor Booking No</b></div>";
            content += "<div style='text-align: right; padding-right: 10px;'><b>BK/20200121/012</b></div>";
            content += "</div>";
            content += "</div>";
            content += itemHtml;
            content += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-top-color: #e0e0e0 ; border-bottom-color: #e0e0e0; border-style: solid; border-right-style:hidden; border-left-style:hidden;'>";
            content += "<div style='width: 300px; float: left; '><b>Total</b>";
            content += "</div>";
            content += "<div style='text-align: right; padding-right: 3px;'><b>" + total + "</b>";
            content += "</div>";
            content += "</div>";
            content += "<br/><br/><br/>";
            content += "</div>";
            content += "<div style='background-color:#0E62AC; color: #ffffff; padding: 10px; text-align: right;'>";
            content += "<b>" + headLiner + "</b><br/>";
            content += address + "</div>";
            content += "</body></html>";

            return content;

        }

        public string BuilBookReceiptToMediaBuyer(AddToBookListInputModel data)
        {
            UserRepository userRepo = new UserRepository(db);
            var userInfo = userRepo.FindByID(data.Data[0].UserID).FirstOrDefault();

            SiteRepository siteRepo = new SiteRepository(db);
            var siteInfo = siteRepo.GetSiteByID(data.Data[0].SiteID).FirstOrDefault();

            string headLiner = "Billboardindo";
            string address = "Gedung Graha Media <br/>  Jl.Blora No. 8-10  <br/>  Jakarta";
            string greetingMsg = "Terima kasih " + userInfo.FirstName + " " + userInfo.LastName + " telah melakukan booking,";
            string thxMessage = "admin kami akan segera mengkonfirmasi untuk ketersediannya.";
            string mediaOwnerName = userInfo.FirstName + " " + userInfo.LastName;
            string noBillboard = siteInfo.NoBillboard;
            string priceItem = data.Data[0].Price.ToString();
            string subThxMessage = "Booking telah dilakukan"; //"Booking <b>" + noBillboard + "</b> telah dilakukan.";

            string itemHtml = "";
            itemHtml += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px;'>";

            foreach (var x in data.Data)
            {
                var tempData = (from t in db.TitikLokasi
                                where t.ID == x.SiteID
                                select new
                                {
                                    NoBillboard = t.NoBillboard,
                                    Price = x.Price,
                                }).FirstOrDefault();



                itemHtml += "<div style='width: 100px; float: left;'>" + tempData.NoBillboard + "</div>";
                itemHtml += "<div style='text-align: right; padding-right: 10px;'>" + tempData.Price.ToRupiah() + "</div><br/>";
                itemHtml += "<div>" + x.StartDate + " - " + x.EndDate + "</div><br/>";
            }

            double grandTotal = 0;

            foreach (var x in data.Data)
            {
                grandTotal += x.Price;
            }

            string total = "";
            total = grandTotal.ToRupiah();

            string content = "";
            content += "<html>";
            content += "<head></head>";
            content += "<body style='font-family:Trebuchet MS, Lucida Sans Unicode, Lucida Grande, Lucida Sans, Arial, sans-serif; padding: 10px;'>";
            content += "<div style='width: 450px;'>";
            content += "<div style='background-color:#0E62AC;'>";
            content += "<h3 style='padding: 10px; color: #ffffff;'>" + headLiner + "</h3>";
            content += "</div>";
            content += "<div style='padding: 10px;'>";
            content += "<div id='content'>";
            content += "<p>" + greetingMsg + "</p>";
            content += "<h2>" + thxMessage + "<h2/>";
            content += "<p style='color:#616161;'>" + subThxMessage + "</p>";
            content += "<div>";
            content += "<div style='width:200px; float: left;'>Media Buyer</div>";
            content += "<div style='text-align: left; padding-right: 10px;'>: " + mediaOwnerName + "</div>";
            content += "</div><br/>";
            content += "<div style='background-color:#e0e0e0; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-radius: 5px ;'>";
            content += "<div>";
            content += "<div style='width: 300px; float: left;'><b>Nomor Booking No</b></div>";
            content += "<div style='text-align: right; padding-right: 10px;'><b>BK/20200121/012</b></div>";
            content += "</div>";
            content += "</div>";
            content += itemHtml;
            content += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-top-color: #e0e0e0 ; border-bottom-color: #e0e0e0; border-style: solid; border-right-style:hidden; border-left-style:hidden;'>";
            content += "<div style='width: 300px; float: left; '><b>Total</b>";
            content += "</div>";
            content += "<div style='text-align: right; padding-right: 10px;'><b>" + total + "</b>";
            content += "</div>";
            content += "</div>";
            content += "<br/><br/><br/>";
            content += "</div>";
            content += "<div style='background-color:#0E62AC; color: #ffffff; padding: 10px; text-align: right;'>";
            content += "<b>" + headLiner + "</b><br/>";
            content += address + "</div>";
            content += "</body></html>";

            return content;

        }

        public string BuilBookChangeReceiptToMediaBuyer(AddItemToCurrentBookListInputModel data)
        {
            UserRepository userRepo = new UserRepository(db);
            var userInfo = userRepo.FindByID(data.Data[0].UserID).FirstOrDefault();

            SiteRepository siteRepo = new SiteRepository(db);
            var siteInfo = siteRepo.GetSiteByID(data.Data[0].SiteID).FirstOrDefault();

            string headLiner = "Billboardindo";
            string address = "Gedung Graha Media <br/>  Jl.Blora No. 8-10  <br/>  Jakarta";
            string greetingMsg = "Terima kasih " + userInfo.FirstName + " " + userInfo.LastName + " telah melakukan booking,";
            string thxMessage = "admin kami akan segera mengkonfirmasi untuk ketersediannya.";
            string mediaOwnerName = userInfo.FirstName + " " + userInfo.LastName;
            string noBillboard = siteInfo.NoBillboard;
            string priceItem = data.Data[0].Price.ToString();
            string subThxMessage = "Booking telah dilakukan"; //"Booking <b>" + noBillboard + "</b> telah dilakukan.";

            string itemHtml = "";
            itemHtml += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px;'>";

            foreach (var x in data.Data)
            {
                var tempData = (from t in db.TitikLokasi
                                where t.ID == x.SiteID
                                select new
                                {
                                    NoBillboard = t.NoBillboard,
                                    Price = x.Price,
                                }).FirstOrDefault();



                itemHtml += "<div style='width: 100px; float: left;'>" + tempData.NoBillboard + "</div>";
                itemHtml += "<div style='text-align: right; padding-right: 10px;'>" + tempData.Price.ToRupiah() + "</div><br/>";
                itemHtml += "<div>" + x.StartDate + " - " + x.EndDate + "</div><br/>";
            }

            double grandTotal = 0;

            foreach (var x in data.Data)
            {
                grandTotal += x.Price;
            }

            string total = "";
            total = grandTotal.ToRupiah();

            string content = "";
            content += "<html>";
            content += "<head></head>";
            content += "<body style='font-family:Trebuchet MS, Lucida Sans Unicode, Lucida Grande, Lucida Sans, Arial, sans-serif; padding: 10px;'>";
            content += "<div style='width: 450px;'>";
            content += "<div style='background-color:#0E62AC;'>";
            content += "<h3 style='padding: 10px; color: #ffffff;'>" + headLiner + "</h3>";
            content += "</div>";
            content += "<div style='padding: 10px;'>";
            content += "<div id='content'>";
            content += "<p>" + greetingMsg + "</p>";
            content += "<h2>" + thxMessage + "<h2/>";
            content += "<p style='color:#616161;'>" + subThxMessage + "</p>";
            content += "<div>";
            content += "<div style='width:200px; float: left;'>Media Buyer</div>";
            content += "<div style='text-align: left; padding-right: 10px;'>: " + mediaOwnerName + "</div>";
            content += "</div><br/>";
            content += "<div style='background-color:#e0e0e0; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-radius: 5px ;'>";
            content += "<div>";
            content += "<div style='width: 300px; float: left;'><b>Nomor Booking No</b></div>";
            content += "<div style='text-align: right; padding-right: 10px;'><b>BK/20200121/012</b></div>";
            content += "</div>";
            content += "</div>";
            content += itemHtml;
            content += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-top-color: #e0e0e0 ; border-bottom-color: #e0e0e0; border-style: solid; border-right-style:hidden; border-left-style:hidden;'>";
            content += "<div style='width: 300px; float: left; '><b>Total</b>";
            content += "</div>";
            content += "<div style='text-align: right; padding-right: 10px;'><b>" + total + "</b>";
            content += "</div>";
            content += "</div>";
            content += "<br/><br/><br/>";
            content += "</div>";
            content += "<div style='background-color:#0E62AC; color: #ffffff; padding: 10px; text-align: right;'>";
            content += "<b>" + headLiner + "</b><br/>";
            content += address + "</div>";
            content += "</body></html>";

            return content;

        }


        public string BuilBookEmailApprovalToMediaBuyer(ApprovalBookInputModel data, string message)
        {
            UserRepository userRepo = new UserRepository(db);
            var userInfo = userRepo.FindByID(data.UserID).FirstOrDefault();

            var siteInfo = (from b in db.BookDetail
                            join t in db.TitikLokasi on b.SiteID equals t.ID
                            join tp in db.TitikLokasiPrice on b.SiteItemID equals tp.TitikLokasiDetailID
                            where b.ID == data.BookDetailID
                            select new
                            {
                                b.Price,
                                t.NoBillboard,
                            }).FirstOrDefault();


            string headLiner = "Billboard Indo";
            string address = "Gedung Graha Media <br/>  Jl.Blora No. 8-10  <br/>  Jakarta";
            string greetingMsg = "Hai " + userInfo.FirstName + " " + userInfo.LastName + " kami telah melakukan konfirmasi pesanan kammu,";
            string thxMessage = message;
            string mediaOwnerName = userInfo.FirstName + " " + userInfo.LastName;
            string noBillboard = siteInfo.NoBillboard;
            string priceItem = siteInfo.Price.ToString();
            string subThxMessage = "Booking <b>" + noBillboard + "</b> telah dilakukan.";

            string itemHtml = "";
            itemHtml += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px;'>";
            itemHtml += "<div style='width: 300px; float: left;'>" + noBillboard + "</div>";
            itemHtml += "<div style='text-align: right; padding-right: 10px;'>" + priceItem + "</div>";

            string total = "";
            total = siteInfo.Price.ToString();

            string content = "";
            content += "<html>";
            content += "<head></head>";
            content += "<body style='font-family:Trebuchet MS, Lucida Sans Unicode, Lucida Grande, Lucida Sans, Arial, sans-serif; padding: 10px;'>";
            content += "<div style='width: 450px;'>";
            content += "<div style='background-color:#0E62AC;'>";
            content += "<h3 style='padding: 10px; color: #ffffff;'>" + headLiner + "</h3>";
            content += "</div>";
            content += "<div style='padding: 10px;'>";
            content += "<div id='content'>";
            content += "<p>" + greetingMsg + "</p>";
            content += "<h2>" + thxMessage + "<h2/>";
            content += "<p style='color:#616161;'>" + subThxMessage + "</p>";
            content += "<div>";
            content += "<div style='width:200px; float: left;'>Media Buyer</div>";
            content += "<div style='text-align: left; padding-right: 10px;'>: " + mediaOwnerName + "</div>";
            content += "</div><br/>";
            content += "<div style='background-color:#e0e0e0; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-radius: 5px ;'>";
            content += "<div>";
            content += "<div style='width: 300px; float: left;'><b>Nomor Booking No</b></div>";
            content += "<div style='text-align: right; padding-right: 10px;'><b>BK/20200121/012</b></div>";
            content += "</div>";
            content += "</div>";
            content += itemHtml;
            content += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-top-color: #e0e0e0 ; border-bottom-color: #e0e0e0; border-style: solid; border-right-style:hidden; border-left-style:hidden;'>";
            content += "<div style='width: 300px; float: left; '><b>Total</b>";
            content += "</div>";
            content += "<div style='text-align: right; padding-right: 10px;'><b>" + total + "</b>";
            content += "</div>";
            content += "</div>";
            content += "<br/><br/><br/>";
            content += "</div>";
            content += "<div style='background-color:#0E62AC; color: #ffffff; padding: 10px; text-align: right;'>";
            content += "<b>" + headLiner + "</b><br/>";
            content += address + "</div>";
            content += "</body></html>";

            return content;

        }
    }
}
