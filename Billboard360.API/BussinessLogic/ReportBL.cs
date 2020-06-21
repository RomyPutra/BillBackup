using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;
using System.Net.Mail;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Cryptography.X509Certificates;

namespace Billboard360.API.BussinessLogic
{
    public class ReportBL
    {
        protected readonly DatabaseContext db;

        public ReportBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }


        public AddReportProblemOutputModel Save(AddReportProblemInputModel data)
        {
            ReportRepository repo = new ReportRepository(db);

            Report temp = new Report();

            temp.isToSPV = data.isToSPV;
            temp.SiteID = data.SiteID;
            temp.UserID = data.UserID;
            temp.Desc = data.Desc;
            temp.ReportNo = GenerateReportNo();

            var res = repo.Insert(temp);

            AddReportProblemOutputModel outputModel = new AddReportProblemOutputModel();

            outputModel.ReportID = res.ID;
            outputModel.ReportNo = temp.ReportNo;

            return outputModel;

        }

        private string GenerateReportNo()
        {
            string result = "";

            DateTime today = DateTime.Now;
            var dbCount = db.Payment.Where(x => x.CreateDate == today).ToList().Count();

            string headInv = "RPT";
            string tahun = DateTime.Now.Year.ToString();
            string bulan = DateTime.Now.Month.ToString();
            string tglJam = DateTime.Now.Date.ToString();
            int count = dbCount + 1;
            result = headInv + "/" + tahun + "/" + bulan + "/" + tglJam + "/" + count;

            return result;
        }

        public ReportProblemListResultModel GetProblem(ReportProblemListInputModel data)
        {
            var res = (from r in db.Report
                       join s in db.TitikLokasi on r.SiteID equals s.ID
                       join u in db.User on r.UserID equals u.ID
                       select new ReportProblemListOutputModel
                       {
                           ReportNo = r.ReportNo,
                           Desc = r.Desc,
                           From = u.UserName,
                           NoBillboard = s.NoBillboard,
                           ReportProblemID = r.ID,
                           IsToSPV = r.isToSPV,
                       });

            if(data.IsToSPV)
            {
                res = res.Where(x => x.IsToSPV);
            }

            int pageNumber = data.PageNumber - 1;

            res = res.Skip(pageNumber * data.PageSize).Take(data.PageSize);

            ReportProblemListResultModel result = new ReportProblemListResultModel();

            result.data = res.ToList();
            result.TotalPages = (res.Count() / data.PageSize) + 1;
            result.TotalData = res.Count();


            return result;
        }




        public ReportPurchaseAndBookResultModel GetReportPurchaseAndBook(ReportPurchaseAndBookInputModel data)
        {
            string roleName = "";
            UserBL userBL = new UserBL(db);

            if (data.UserName != null && data.UserName != string.Empty)
            {
                roleName = userBL.CheckRole(data.UserName);
            }



            var query = (from b in db.BookDetail
                         join s in db.TitikLokasi on b.SiteID equals s.ID
                         join sp in db.TitikLokasiPrice on b.PriceID equals sp.ID
                         select new ReportPurchaseAndBookOutputModel()
                         {
                             BookID = b.ID,
                             Harga = b.Price,
                             HargaTotal = b.Qty * b.Price,
                             OwnerSiteID = s.OwnerByUserID,
                             Qty = b.Qty,
                             NoBillBoard = s.NoBillboard,
                             Status = b.StatusApprovalPerBillboard == 1 ? "Approve" : b.StatusApprovalPerBillboard == 2 ? "Reject" : "On Progress",
                             Unit = sp.Dasar,
                             StartDate = b.StartDate,
                             EndDate = b.EndDate,
                             BookNo = db.Book.Where(x => x.ID == b.BookID).FirstOrDefault().BookNo,
                             UserName = db.User
                                            .Where(z => z.ID == db.Book
                                                                    .Where(x => x.ID == b.BookID).Select(x => x.UserID).FirstOrDefault()).Select(z => z.UserName).FirstOrDefault(),
                             TglBook = b.CreateDate,
                             TglTrans = db.Book.Where(x => x.ID == b.BookID).Select(x => x.PaymentID).FirstOrDefault() != null ?
                                            db.Payment.Where(x => x.ID == db.Book
                                                        .Where(z => z.ID == b.BookID).Select(z => z.PaymentID).FirstOrDefault()).FirstOrDefault().CreateDate.ToString() : ""
                         });

            if (data.UserName != null && data.UserName != string.Empty)
            {
                query = (from q in query
                         where q.UserName.Contains(data.UserName)
                         select new ReportPurchaseAndBookOutputModel()
                         {
                             BookID = q.BookID,
                             TglBook = q.TglBook,
                             TglTrans = q.TglTrans,
                             Harga = q.Harga,
                             HargaTotal = q.HargaTotal,
                             Qty = q.Qty,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status,
                             Unit = q.Unit,
                             UserName = q.UserName
                         });
            }

            if (data.NoBillboard != null && data.NoBillboard != string.Empty)
            {
                query = (from q in query
                         where q.NoBillBoard.Contains(data.NoBillboard)
                         select new ReportPurchaseAndBookOutputModel()
                         {
                             BookID = q.BookID,
                             TglBook = q.TglBook,
                             TglTrans = q.TglTrans,
                             Harga = q.Harga,
                             HargaTotal = q.HargaTotal,
                             Qty = q.Qty,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status,
                             Unit = q.Unit,
                             UserName = q.UserName
                         });
            }

            if (data.IsBook != false)
            {
                query = (from q in query
                         where q.Status == "On Progress" || q.Status == "Reject"
                         select new ReportPurchaseAndBookOutputModel()
                         {
                             BookID = q.BookID,
                             TglBook = q.TglBook,
                             TglTrans = q.TglTrans,
                             Harga = q.Harga,
                             HargaTotal = q.HargaTotal,
                             Qty = q.Qty,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status,
                             Unit = q.Unit,
                             UserName = q.UserName
                         });
            }

            if (data.IsPurchased != false)
            {
                query = (from q in query
                         where q.Status == "Approve"
                         select new ReportPurchaseAndBookOutputModel()
                         {
                             BookID = q.BookID,
                             TglBook = q.TglBook,
                             TglTrans = q.TglTrans,
                             Harga = q.Harga,
                             HargaTotal = q.HargaTotal,
                             Qty = q.Qty,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status,
                             Unit = q.Unit,
                             UserName = q.UserName
                         });
            }

            if (data.TanggalPurchaseAwal != null)
            {
                query = (from q in query
                         where q.TglBook >= data.TanggalPurchaseAwal
                         select new ReportPurchaseAndBookOutputModel()
                         {
                             BookID = q.BookID,
                             TglBook = q.TglBook,
                             TglTrans = q.TglTrans,
                             Harga = q.Harga,
                             HargaTotal = q.HargaTotal,
                             Qty = q.Qty,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status,
                             Unit = q.Unit,
                             UserName = q.UserName
                         });
            }

            if (data.TanggalPurchaseAkhir != null)
            {
                query = (from q in query
                         where q.TglBook <= data.TanggalPurchaseAwal
                         select new ReportPurchaseAndBookOutputModel()
                         {
                             BookID = q.BookID,
                             TglBook = q.TglBook,
                             TglTrans = q.TglTrans,
                             Harga = q.Harga,
                             HargaTotal = q.HargaTotal,
                             Qty = q.Qty,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status,
                             Unit = q.Unit,
                             UserName = q.UserName
                         });
            }

            if (roleName == "MDO")
            {

                UserRepository userRepo = new UserRepository(db);
                var dataUser = userRepo.FindByUserName(data.UserName);

                query = query.Where(x => x.OwnerSiteID == dataUser.ID);
            }

            int pageNumber = data.PageNumber - 1;

            query = query.Skip(pageNumber * data.PageSize).Take(data.PageSize);

            ReportPurchaseAndBookResultModel result = new ReportPurchaseAndBookResultModel();

            result.result = query.ToList();
            result.TotalPages = (query.Count() / data.PageSize) + 1;
            result.TotalData = query.Count();

            return result;
        }

        public ReportSiteResultModel GetReportSite(ReportSiteInputModel data)
        {
            var today = DateTime.Now;

            var query = (from b in db.BookDetail
                         join s in db.TitikLokasi on b.SiteID equals s.ID
                         join u in db.User on s.OwnerByUserID equals u.ID
                         select new ReportSiteOutputModel()
                         {
                             MediaOwnerGuid = u.ID,
                             MediaOwnerName = u.FirstName + " " + u.LastName,
                             Kota = s.Kota,
                             Provinsi = s.Provinsi,
                             NoBillBoard = s.NoBillboard + db.TitikLokasiDetail.Where(x => x.TitikLokasiID == s.ID && x.ID == b.SiteItemID).Select(x => x.ArahLokasi).FirstOrDefault() != "" ? db.TitikLokasiDetail.Where(x => x.TitikLokasiID == s.ID && x.ID == b.SiteItemID).Select(x => x.ArahLokasi).FirstOrDefault() : "",
                             Status = db.Book.Where(z => z.ID == b.BookID).Select(x => x.PaymentID).FirstOrDefault() != Guid.Empty ? "Purchased" : db.BookDetail.Where(x => today > x.EndDate && x.SiteID == s.ID && x.SiteItemID == b.SiteItemID).Count() > 0 ? "Available" : "Booked"
                         });

            if (data.MediaOwnerID != null && data.MediaOwnerID != Guid.Empty)
            {


                query = (from q in query
                         where q.MediaOwnerGuid == data.MediaOwnerID
                         select new ReportSiteOutputModel()
                         {
                             MediaOwnerGuid = q.MediaOwnerGuid,
                             MediaOwnerName = q.MediaOwnerName,
                             Kota = q.Kota,
                             Provinsi = q.Provinsi,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status
                         });
            }

            if (data.NoBillboard != null && data.NoBillboard != string.Empty)
            {
                query = (from q in query
                         where q.NoBillBoard.Contains(data.NoBillboard)
                         select new ReportSiteOutputModel()
                         {
                             MediaOwnerGuid = q.MediaOwnerGuid,
                             MediaOwnerName = q.MediaOwnerName,
                             Kota = q.Kota,
                             Provinsi = q.Provinsi,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status
                         });
            }

            if (data.IsBooked != false)
            {
                query = (from q in query
                         where q.Status == "Booked"
                         select new ReportSiteOutputModel()
                         {
                             MediaOwnerGuid = q.MediaOwnerGuid,
                             MediaOwnerName = q.MediaOwnerName,
                             Kota = q.Kota,
                             Provinsi = q.Provinsi,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status
                         });
            }

            if (data.IsPurchased != false)
            {
                query = (from q in query
                         where q.Status == "Purchased"
                         select new ReportSiteOutputModel()
                         {
                             MediaOwnerGuid = q.MediaOwnerGuid,
                             MediaOwnerName = q.MediaOwnerName,
                             Kota = q.Kota,
                             Provinsi = q.Provinsi,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status
                         });
            }

            if (data.IsPurchased != false)
            {
                query = (from q in query
                         where q.Status == "Available"
                         select new ReportSiteOutputModel()
                         {
                             MediaOwnerGuid = q.MediaOwnerGuid,
                             MediaOwnerName = q.MediaOwnerName,
                             Kota = q.Kota,
                             Provinsi = q.Provinsi,
                             NoBillBoard = q.NoBillBoard,
                             Status = q.Status
                         });
            }

            int pageNumber = data.PageNumber - 1;

            query = query.Skip(pageNumber * data.PageSize).Take(data.PageSize);

            ReportSiteResultModel result = new ReportSiteResultModel();

            result.data = query.ToList();
            result.TotalPages = (query.Count() / data.PageSize) + 1;
            result.TotalData = query.Count();

            return result;
        }

        public string BuildInvoice(PurchaseOutputModel data)
        {
            string headLiner = "Billboard Indo";
            string address = "Jl. Pluto Bima Sakti <br/>  No 123  <br/>  Galaxy Alam Semesta";
            string greetingMsg = "Hai Enggar,";
            string thxMessage = "Pembelianmu Telah Selesai";
            string mediaOwnerName = "Warna Warni";
            string noBillboard = "7061BTJW";
            string priceItem = "Rp 30.000.000";
            string subThxMessage = "Pembelian <b>" + noBillboard + "</b> telah selesai. Dana pembelianmu akan diteruskan ke Media Owner";

            string itemHtml = "";
            itemHtml += "<div style='padding-left: 10px; padding-bottom: 10px; padding-top: 10px;'>";
            itemHtml += "<div style='width: 300px; float: left;'>" + noBillboard + "</div>";
            itemHtml += "<div style='text-align: right; padding-right: 10px;'>" + priceItem + "</div>";

            string total = "";
            total = "Rp 30.000.000";

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
            content += "<div style='width:200px; float: left;'>Media Owner</div>";
            content += "<div style='text-align: left; padding-right: 10px;'>: " + mediaOwnerName + "</div>";
            content += "</div><br/>";
            content += "<div style='background-color:#e0e0e0; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; border-radius: 5px ;'>";
            content += "<div>";
            content += "<div style='width: 300px; float: left;'><b>Invoice No</b></div>";
            content += "<div style='text-align: right; padding-right: 10px;'><b>INV/20200121/012</b></div>";
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

        public RekapKotakEmpatOutputModel GetKotakEmpat(ReportKotaEmpatInputModel data)
        {
            RekapKotakEmpatOutputModel outputModel = new RekapKotakEmpatOutputModel();

            DateTime today = DateTime.Now;

            int totalBooking = 0;

            if (data.UserID != null && data.UserID != Guid.Empty)
            {
                totalBooking = db.Book.Count(x => x.UserID == data.UserID);
            }else
            {
                totalBooking = db.Book.Count();
            }

            int totalOrder = 0;

            if (data.UserID != null && data.UserID != Guid.Empty)
            {
                totalOrder = db.Book.Count(x => x.PaymentID != Guid.Empty && x.UserID == data.UserID);
            }
            else
            {
                totalOrder = db.Book.Count(x => x.PaymentID != Guid.Empty);
            }

            int totalUseSite = 0;


            if (data.UserID != null && data.UserID != Guid.Empty)
            {
                //totalUseSite = db.BookDetail.Where(x => x.EndDate <= DateTime.Now && x.CreateDate.Month == today.Month).GroupBy(x => x.SiteItemID).Count();
                totalUseSite = (from bd in db.BookDetail
                                join s in db.TitikLokasi on bd.SiteID equals s.ID
                                where bd.EndDate <= DateTime.Now && bd.CreateDate.Month == today.Month && s.OwnerByUserID == data.UserID
                                select new { 
                                    SiteID = bd.SiteID
                                }).Distinct().Count();
            }
            else
            {
                totalUseSite = (from bd in db.BookDetail
                                join s in db.TitikLokasi on bd.SiteID equals s.ID
                                where bd.EndDate <= DateTime.Now && bd.CreateDate.Month == today.Month
                                select new
                                {
                                    SiteID = bd.SiteID
                                }).Distinct().Count();
            }

            int totalSite = 0;

            if (data.UserID != null && data.UserID != Guid.Empty)
            {
                totalSite = db.TitikLokasi.Where(x => x.DeletedDate == null && x.OwnerByUserID == data.UserID).Count();

            }
            else
            {
                totalSite = db.TitikLokasi.Where(x => x.DeletedDate == null).Count();
            }

            outputModel.TotalBooking = totalBooking;
            outputModel.TotalOrder = totalOrder;
            outputModel.TotalUseSite = totalUseSite;
            outputModel.TotalSite = totalSite;

            var dataRevenue = (from b in db.Book
                               from bd in db.BookDetail.Where(d => d.BookID == b.ID).DefaultIfEmpty()
                               join p in db.Payment on b.PaymentID equals p.ID
                               join s in db.TitikLokasi on bd.SiteID equals s.ID
                               where b.CreateDate.Month == today.Month
                               select new Revenue()
                               {
                                   //TotalRevenue = db.BookDetail.Where(d => d.BookID == b.ID).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                                   TotalRevenue = bd.FinalPrice != 0 ? bd.FinalPrice : bd.Price,
                                   PartRevenue = p.TotalPaid,
                                   UserID = s.OwnerByUserID
                               });

            if(data.UserID != null && data.UserID != Guid.Empty)
            {
                dataRevenue = dataRevenue.Where(x => x.UserID == data.UserID);
            }

            outputModel.TotalRevenue = dataRevenue.Sum(x => x.TotalRevenue);
            outputModel.Revenue = dataRevenue.Sum(x => x.PartRevenue);



            return outputModel;
        }


        public ChartModel GetTransactionPerMonth()
        {
            DateTime today = DateTime.Now;

            DateTime jan = new DateTime(2020, 1, 1);
            DateTime feb = new DateTime(2020, 2, 1);
            DateTime mar = new DateTime(2020, 3, 1);
            DateTime apr = new DateTime(2020, 4, 1);
            DateTime mei = new DateTime(2020, 5, 1);
            DateTime juni = new DateTime(2020, 6, 1);
            DateTime juli = new DateTime(2020, 7, 1);
            DateTime agu = new DateTime(2020, 8, 1);
            DateTime sept = new DateTime(2020, 9, 1);
            DateTime oct = new DateTime(2020, 10, 1);
            DateTime nov = new DateTime(2020, 11, 1);
            DateTime des = new DateTime(2020, 12, 1);


            var data = from x in db.Book
                       where x.PaymentID != Guid.Empty && x.DeletedDate == null
                       select new ChartModel()
                       {
                           Januari = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == jan.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           Februari = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == feb.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           Maret = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == mar.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           April = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == apr.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           Mei = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == mei.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           Juni = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == juni.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           Juli = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == juli.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           Agustus = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == agu.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           September = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == sept.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           Oktober = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == oct.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           November = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == nov.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                           Desember = db.BookDetail.Where(z => z.StatusApprovalPerBillboard == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == des.Month).Sum(o => o.FinalPrice != 0 ? o.FinalPrice : o.Price),
                       };

            return data.FirstOrDefault();
        }


        public ChartModel GetBookPerMonth()
        {
            DateTime today = DateTime.Now;

            DateTime jan = new DateTime(2020, 1, 1);
            DateTime feb = new DateTime(2020, 2, 1);
            DateTime mar = new DateTime(2020, 3, 1);
            DateTime apr = new DateTime(2020, 4, 1);
            DateTime mei = new DateTime(2020, 5, 1);
            DateTime juni = new DateTime(2020, 6, 1);
            DateTime juli = new DateTime(2020, 7, 1);
            DateTime agu = new DateTime(2020, 8, 1);
            DateTime sept = new DateTime(2020, 9, 1);
            DateTime oct = new DateTime(2020, 10, 1);
            DateTime nov = new DateTime(2020, 11, 1);
            DateTime des = new DateTime(2020, 12, 1);


            var data = from x in db.Book
                       where x.PaymentID != Guid.Empty && x.DeletedDate == null
                       select new ChartModel()
                       {
                           Januari = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == jan.Month).Count(),
                           Februari = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == feb.Month).Count(),
                           Maret = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == mar.Month).Count(),
                           April = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == apr.Month).Count(),
                           Mei = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == mei.Month).Count(),
                           Juni = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == juni.Month).Count(),
                           Juli = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == juli.Month).Count(),
                           Agustus = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == agu.Month).Count(),
                           September = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == sept.Month).Count(),
                           Oktober = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == oct.Month).Count(),
                           November = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == nov.Month).Count(),
                           Desember = db.Book.Where(z => z.StatusApproval == 1 && z.CreateDate.Year == today.Year && z.CreateDate.Month == des.Month).Count()
                       };

            return data.FirstOrDefault();
        }


    }
}
