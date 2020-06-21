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
    public class WishListBL
    {
        protected readonly DatabaseContext db;

        public WishListBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<WishListOutputModel> GetWishList(WishListInputModel data)
        {
            var temp = (from w in db.WishList
                        join s in db.TitikLokasi on w.SiteID equals s.ID
                        join si in db.TitikLokasiDetail on w.SiteItemID equals si.ID
                        where w.UserID == data.UserID && w.DeletedDate == null
                        select new WishListOutputModel()
                        {
                            WishlistID = w.ID,
                            BillboardType = s.Type,
                            Cabang = s.Cabang,
                            HorV = s.HorV,
                            Kota = s.Kota,
                            NoBillboard = s.NoBillboard,
                            Provinsi = s.Provinsi,
                            Latitude = s.Latitude,
                            Longitude = s.Longitude,
                            Pulau = s.Pulau,
                            RateScoreAverage = s.RateScoreAverage,
                            SiteID = s.ID,
                            Image = db.TitikLokasiImage.Where(x => x.TitikLokasiID == s.ID).Select(x => x.PathImage).ToList(),
                            SiteItem = db.TitikLokasiDetail.Where(x => x.TitikLokasiID == si.TitikLokasiID)
                                                                .Select(x => new SiteDetailItemModel()
                                                                {
                                                                    SiteID = x.TitikLokasiID,
                                                                    SiteItemID = x.ID,
                                                                    ArahLokasi = x.ArahLokasi,
                                                                    Lebar = x.Lebar,
                                                                    Panjang = x.Panjang,
                                                                    Catatan = x.Keterangan,
                                                                    Image = db.TitikLokasiImage.Where(y => y.TitikLokasiDetailID == si.ID)
                                                                                        .Select(i => new SiteDetailImageModel()
                                                                                        {
                                                                                            SiteImageID = i.ID,
                                                                                            isThumbnail = i.IsThumbnail,
                                                                                            Image = i.PathImage
                                                                                        }).ToList(),
                                                                    Price = db.TitikLokasiPrice.Where(y => y.TitikLokasiDetailID == si.ID)
                                                                                        .Select(i => new SiteDetailPriceModel()
                                                                                        {
                                                                                            HargaAkhir = i.HargaAkhir,
                                                                                            HargaAwal = i.HargaAwal,
                                                                                            Kelipatan = i.Kelipatan,
                                                                                            Dasar = i.Dasar, 
                                                                                            SitePriceID = i.ID,
                                                                                            MinimDasar = i.MinimDasar,
                                                                                            MinimOrder = i.MinimOrder,
                                                                                        }).ToList()

                                                                }).ToList()
                        });




            int pageNumber = data.PageNumber - 1;

            temp = temp.Skip(pageNumber * data.PageSize).Take(data.PageSize);

            return temp;
        }

        public AddToWishListOutputModel Save(AddToWishListInputModel data)
        {
            WishListRepository repo = new WishListRepository(db);

            WishList temp = new WishList();

            temp.SiteID = data.SiteID;
            temp.UserID = data.UserID;
            temp.SiteItemID = data.SiteItemID;

            var res = repo.Insert(temp);

            AddToWishListOutputModel output = new AddToWishListOutputModel();
            output.SiteID = data.SiteID;


            return output;

        }

        public DeleteFromWishListOutputModel DeleteFromWishList(Guid wishListID)
        {
            WishListRepository repo = new WishListRepository(db);

            var res = repo.DeleteByID(wishListID);

            DeleteFromWishListOutputModel output = new DeleteFromWishListOutputModel();

            output.WishListID = res.ID;

            return output;
        } 

        public List<NotificationWishListOutput> FindWishListAvailable(Guid userID)
        {
            var res = (from w in db.WishList
                       join s in db.TitikLokasi on w.SiteID equals s.ID
                       where w.UserID == userID && w.DeletedDate == null
                       select new NotificationWishListOutput()
                       {
                           BillboardType = s.Type,
                           Cabang = s.Cabang,
                           HorV = s.HorV,
                           Kota = s.Kota,
                           NoBillboard = s.NoBillboard,
                           Provinsi = s.Provinsi,
                           Pulau = s.Pulau
                       }).ToList();

            return res;
        }
    }
}
