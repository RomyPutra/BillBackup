using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;

namespace Billboard360.API.BussinessLogic
{
    public class CompareBL
    {
        protected readonly DatabaseContext db;

        public CompareBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public AddCompareOutputModel Save(AddCompareInputModel data)
        {
            CompareRepository repo = new CompareRepository(db);

            Compare temp = new Compare();

            string message = "";

            temp.CreateByUserID = data.UserID;
            temp.SiteID = data.SiteID;
            temp.UserID = data.UserID;
            temp.SiteItemID = data.SiteItemID;

            var checkCompare = db.Compare.Where(x => x.UserID == data.UserID && x.DeletedDate == null).ToList();

            AddCompareOutputModel output = new AddCompareOutputModel();

            if (checkCompare.Count <= 3)
            {
                var res = repo.Insert(temp);
                output.SiteID = data.SiteID;
                message = res.Message;
            }
            else
            {
                message = "Maksimal hanya 3 site";
            }

            output.Message = message;

            return output;
        }

        public List<CompareListResultModel> GetCompareList (CompareListInputModel data)
        {
            var temp = db.Compare.Where(x => x.UserID == data.UserID && x.DeletedDate == null).ToList();

            List<CompareListResultModel> listCompare = new List<CompareListResultModel>();

            if (temp != null && temp.Count > 0)
            {
                
                foreach (var select in temp)
                {

                    var headQuery = (from head in db.TitikLokasi
                                     where head.ID == @select.SiteID
                                     select new CompareListDetailModel()
                                     {
                                         SiteID = head.ID,
                                         CompareID = @select.ID,
                                         Cabang = head.Cabang,
                                         HorV = head.HorV,
                                         Kota = head.Kota,
                                         Pulau = head.Pulau,
                                         Latitude = head.Latitude,
                                         Longitude = head.Longitude,
                                         NoBillboard = head.NoBillboard,
                                         Tipe = head.Type,
                                         Provinsi = head.Provinsi,
                                         RateScoreAverage = head.RateScoreAverage,
                                     }).FirstOrDefault();

                    var ItemQuery = (from item in db.TitikLokasiDetail
                                     where item.TitikLokasiID == @select.SiteID
                                     select new CompareListSiteItemModel()
                                     {
                                         ArahLokasi = item.ArahLokasi,
                                         SiteID = item.TitikLokasiID,
                                         KodeLokasi = item.KodeArahLokasi,
                                         SiteItemID = item.ID,
                                         Panjang = item.Panjang,
                                         Lebar = item.Lebar,
                                         Price = db.TitikLokasiPrice.Where(x => x.TitikLokasiDetailID == item.ID).Select(x => new CompareListPriceModel
                                         {
                                             SitePriceID = x.ID,
                                             SiteItemID = x.TitikLokasiDetailID,
                                             Dasar = x.Dasar,
                                             HargaAkhir = x.HargaAkhir,
                                             HargaAwal = x.HargaAwal,
                                             Kelipatan = x.Kelipatan
                                         }).ToList(),
                                         Image = db.TitikLokasiImage.Where(x => x.TitikLokasiDetailID == item.ID).Select(x => new CompareListImageModel
                                         {
                                             SiteImageID = x.ID,
                                             SiteItemID = x.TitikLokasiDetailID,
                                             Image = x.PathImage
                                         }).ToList()
                                     }).ToList();


                    CompareListResultModel resData = new CompareListResultModel();

                    resData.Detail = headQuery;
                    resData.Item = ItemQuery;

                    listCompare.Add(resData);

                }
            }

            return listCompare;
        }

   

        public DeleteCompareOutputModel DeleteCompare(DeleteCompareInputModel data)
        {
            CompareRepository repo = new CompareRepository(db);

            var res = repo.Delete(data.CompareID, data.UserID);

            DeleteCompareOutputModel output = new DeleteCompareOutputModel();

            output.CompareID = res.ID;

            return output;
        }

        public ClearCompareOutputModel DeleteCompare(ClearCompareInputModel data)
        {
            CompareRepository repo = new CompareRepository(db);

            var res = repo.Delete(Guid.Empty, data.UserID);

            ClearCompareOutputModel output = new ClearCompareOutputModel();

            output.Message = "Compare sudah dihapus semua!";

            return output;
        }
    }
}
