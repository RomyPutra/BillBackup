using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess.Models.Enums;
using Billboard360.Helper;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using NetTopologySuite;
using System.Data.SqlClient;
using Billboard360.API.Models.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Billboard360.API.BussinessLogic
{
    public class SiteBL
    {
        protected readonly DatabaseContext db;

        public SiteBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public SiteListResultModel GetListSite(FilterBillboard filter)
        {
            SiteRepository repo = new SiteRepository(db);
            SiteListResultModel result = new SiteListResultModel();
            List<SiteListOutputModel> reQLat = new List<SiteListOutputModel>();

            string roleName = "";

            //CHECK ROLE USER IS MEDIA OWNER OR MEDIA BUYER OR ADMIN

            UserBL userBL = new UserBL(db);

            if (filter.UserID != null && filter.UserID != Guid.Empty)
            {
                roleName = userBL.CheckRole((Guid)filter.UserID);
            }


            //LIHAT DARI MANA FILTERNYA DARI SEARCH NYA DATATABLE ATAU BUKAN
            //KARENA SIFATNYA ADALAH OR BUKAN AND

            if (filter.isFilterDataTable)
            {
                var query = repo.GetAll();



                if (filter.StartDate != null && filter.EndDate != null)
                {
                    BookDetailRepository bookDetailRepo = new BookDetailRepository(db);
                    var siteIdQuery = bookDetailRepo.CheckAvailableSite(filter.StartDate, filter.EndDate);

                    var siteIDCollection = siteIdQuery.Select(x => x.SiteID).Distinct().ToList();

                    query = query.Where(x => !siteIDCollection.Contains(x.ID));
                }

                if (roleName == "MDO")
                {
                    query = query.Where(x => x.OwnerByUserID == filter.UserID);
                }

                if (!filter.showWithDisabledSite)
                {
                    query = query.Where(x => x.Status == 1);
                }

                var reQuery = (from x in query
                               select new SiteListOutputModel()
                               {
                                   SiteID = x.ID,
                                   NoBillboard = x.NoBillboard,
                                   Cabang = x.Cabang,
                                   HorV = x.HorV == "H" ? "Horizontal" : "Vertical",
                                   Kota = x.Kota,
                                   Provinsi = x.Provinsi,
                                   Alamat = x.Address,
                                   Pulau = x.Pulau,
                                   Tipe = x.Type,
                                   RateScoreAverage = x.RateScoreAverage,
                                   Latitude = x.Latitude,
                                   Longitude = x.Longitude,
                                   TotalView = x.TotalView,
                                   Status = x.Status == 1 ? "Active" : "InActive",
                                   HargaPerhari = db.TitikLokasiPrice.Where(z => z.TitikLokasiID == x.ID).OrderBy(z => z.HargaAwal).Select(z => z.HargaAwal).FirstOrDefault(), // diambil harga paling murah karena ga mungkin untuk filter yang ada di detail
                                   MinimDasar = db.TitikLokasiPrice.Where(z => z.TitikLokasiID == x.ID).OrderBy(z => z.HargaAwal).Select(z => z.MinimDasar).FirstOrDefault(),
                                   MinimOrder = db.TitikLokasiPrice.Where(z => z.TitikLokasiID == x.ID).OrderBy(z => z.HargaAwal).Select(z => z.MinimOrder).FirstOrDefault(),
                                   ImageHeader = db.TitikLokasiImage.Where(y => y.TitikLokasiID == x.ID && y.IsThumbnail == "1").Select(z => z.PathImage).FirstOrDefault(),
                                   SiteItem = db.TitikLokasiDetail.Where(z => z.TitikLokasiID == x.ID).Select(z => new SiteListItemModel()
                                   {
                                       ArahLokasi = z.ArahLokasi,
                                       KodeLokasi = z.KodeArahLokasi,
                                       Panjang = z.Panjang,
                                       Lebar = z.Lebar,
                                       SiteItemID = z.ID,
                                       Catatan = z.Keterangan,
                                       Image = db.TitikLokasiImage.Where(y => y.TitikLokasiDetailID == z.ID).Select(y => new EditSiteImageInputModel()
                                       {
                                           Image = y.PathImage,
                                           SiteImageID = y.ID,
                                           IsThumbnail = y.IsThumbnail
                                       }).ToList(),
                                       Price = db.TitikLokasiPrice.Where(p => p.TitikLokasiDetailID == z.ID).Select(p => new EditSitePriceInputModel()
                                       {
                                           MinimDasar = p.MinimDasar,
                                           MinimOrder = p.MinimOrder,
                                           Dasar = p.Dasar,
                                           HargaAkhir = p.HargaAkhir,
                                           HargaAwal = p.HargaAwal,
                                           Kelipatan = p.Kelipatan,
                                           SitePriceID = p.ID,
                                           HargaPerhari = p.HargaAwal
                                       }).ToList(),
                                       Wishlist = filter.UserID == null ?
                                                                    new SiteWishlist() { isWishlist = false, WishlistID = Guid.Empty } :
                                                                    new SiteWishlist()
                                                                    {
                                                                        isWishlist = db.WishList.Count(w => w.SiteID == z.TitikLokasiID && w.SiteItemID == z.ID && w.UserID == filter.UserID && w.DeletedDate == null) > 0 ? true : false,
                                                                        WishlistID = db.WishList.Count(w => w.SiteID == z.TitikLokasiID && w.SiteItemID == z.ID && w.UserID == filter.UserID && w.DeletedDate == null) > 0 ? db.WishList.Where(w => w.SiteID == z.TitikLokasiID && w.SiteItemID == z.ID && w.UserID == filter.UserID && w.DeletedDate == null).FirstOrDefault().ID : Guid.Empty
                                                                    }
                                   }).ToList()
                               });

                reQuery = reQuery.Where(x => x.Tipe.Contains(filter.FilterDataTableValue) || x.Alamat.Contains(filter.FilterDataTableValue) || x.Provinsi.Contains(filter.FilterDataTableValue) || x.NoBillboard.Contains(filter.FilterDataTableValue));

                int pageNumber = filter.PageNumber - 1;

                if (filter.Sorting == Sort.None || filter.Sorting == Sort.AtoZ)
                {
                    reQuery = reQuery.OrderBy(z => z.NoBillboard);
                } else if (filter.Sorting == Sort.ZtoA)
                {
                    reQuery = reQuery.OrderByDescending(z => z.NoBillboard);
                } else if(filter.Sorting == Sort.Termahal)
                {
                    reQuery = reQuery.OrderByDescending(z => z.HargaPerhari);
                } else if(filter.Sorting == Sort.Termurah)
                {
                    reQuery = reQuery.OrderBy(z => z.HargaPerhari);
                }


                reQuery = reQuery.Skip(pageNumber * filter.PageSize).Take(filter.PageSize);

                result.Result = reQuery.ToList();
                result.TotalPage = (reQuery.Count() / filter.PageSize) + 1;
                result.TotalData = reQuery.Count();
            }
            else
            {
                if ((filter.Latitude != "" && filter.Longitude != "") && (filter.Latitude != "0" && filter.Longitude != "0"))
                {

                    var query = db.SP_GetTitikLokasiWithDistance.FromSql("GetTitikLokasiFilterLocation @p0, @p1", filter.Latitude, filter.Longitude).ToList();

                    if (filter.StartDate != null && filter.EndDate != null)
                    {
                        BookDetailRepository bookDetailRepo = new BookDetailRepository(db);
                        var siteIdQuery = bookDetailRepo.CheckAvailableSite(filter.StartDate, filter.EndDate);

                        var siteIDCollection = siteIdQuery.Select(x => x.SiteID).Distinct().ToList();

                        query = query.Where(x => !siteIDCollection.Contains(x.ID)).ToList();
                    }

                    if (roleName == "MDO")
                    {
                        query = query.Where(x => x.OwnerByUserID == filter.UserID).ToList();
                    }

                    if (!filter.showWithDisabledSite)
                    {
                        query = query.Where(x => x.Status == 1).ToList();
                    }

                    var reQuery = (from x in query
                                   select new SiteListOutputModel()
                                   {
                                       SiteID = x.ID,
                                       NoBillboard = x.NoBillboard,
                                       Cabang = x.Cabang,
                                       HorV = x.HorV == "H" ? "Horizontal" : "Vertical",
                                       Kota = x.Kota,
                                       Provinsi = x.Provinsi,
                                       Alamat = x.Address,
                                       Pulau = x.Pulau,
                                       Tipe = x.Type,
                                       RateScoreAverage = x.RateScoreAverage,
                                       Latitude = x.Latitude,
                                       Longitude = x.Longitude,
                                       TotalView = x.TotalView,
                                       Distance = x.Distance,
                                       Status = x.Status == 1 ? "Active" : "InActive",
                                       HargaPerhari = db.TitikLokasiPrice.Where(z => z.TitikLokasiID == x.ID).OrderBy(z => z.HargaAwal).Select(z => z.HargaAwal).FirstOrDefault(), // diambil harga paling murah karena ga mungkin untuk filter yang ada di detail
                                       MinimDasar = db.TitikLokasiPrice.Where(z => z.TitikLokasiID == x.ID).OrderBy(z => z.HargaAwal).Select(z => z.MinimDasar).FirstOrDefault(),
                                       MinimOrder = db.TitikLokasiPrice.Where(z => z.TitikLokasiID == x.ID).OrderBy(z => z.HargaAwal).Select(z => z.MinimOrder).FirstOrDefault(),
                                       ImageHeader = db.TitikLokasiImage.Where(y => y.TitikLokasiID == x.ID && y.IsThumbnail == "1").Select(z => z.PathImage).FirstOrDefault(),
                                       SiteItem = db.TitikLokasiDetail.Where(z => z.TitikLokasiID == x.ID).Select(z => new SiteListItemModel()
                                       {
                                           ArahLokasi = z.ArahLokasi,
                                           KodeLokasi = z.KodeArahLokasi,
                                           Panjang = z.Panjang,
                                           Lebar = z.Lebar,
                                           SiteItemID = z.ID,
                                           Catatan = z.Keterangan,
                                           Image = db.TitikLokasiImage.Where(y => y.TitikLokasiDetailID == z.ID).Select(y => new EditSiteImageInputModel()
                                           {
                                               Image = y.PathImage,
                                               SiteImageID = y.ID,
                                               IsThumbnail = y.IsThumbnail
                                           }).ToList(),
                                           Price = db.TitikLokasiPrice.Where(p => p.TitikLokasiDetailID == z.ID).Select(p => new EditSitePriceInputModel()
                                           {
                                               MinimDasar = p.MinimDasar,
                                               MinimOrder = p.MinimOrder,
                                               Dasar = p.Dasar,
                                               HargaAkhir = p.HargaAkhir,
                                               HargaAwal = p.HargaAwal,
                                               Kelipatan = p.Kelipatan,
                                               SitePriceID = p.ID,
                                               HargaPerhari = p.HargaAwal
                                           }).ToList(),
                                           Wishlist = filter.UserID == null ?
                                                                        new SiteWishlist() { isWishlist = false, WishlistID = Guid.Empty } :
                                                                        new SiteWishlist()
                                                                        {
                                                                            isWishlist = db.WishList.Count(w => w.SiteID == z.TitikLokasiID && w.SiteItemID == z.ID && w.UserID == filter.UserID && w.DeletedDate == null) > 0 ? true : false,
                                                                            WishlistID = db.WishList.Count(w => w.SiteID == z.TitikLokasiID && w.SiteItemID == z.ID && w.UserID == filter.UserID && w.DeletedDate == null) > 0 ? db.WishList.Where(w => w.SiteID == z.TitikLokasiID && w.SiteItemID == z.ID && w.UserID == filter.UserID && w.DeletedDate == null).FirstOrDefault().ID : Guid.Empty
                                                                        }
                                       }).ToList()
                                   });


                    if (filter.TypeBillboard != null && filter.TypeBillboard != string.Empty)
                    {
                        reQuery = reQuery.Where(x => x.Tipe == filter.TypeBillboard);
                    }


                    if (filter.MinimumPrice != 0)
                    {
                        reQuery = reQuery.Where(x => x.HargaPerhari >= filter.MinimumPrice);
                    }

                    if (filter.MaximumPrice != 0)
                    {
                        reQuery = reQuery.Where(x => x.HargaPerhari <= filter.MaximumPrice);
                    }

                    // Search by GeoLocation
                    if ((filter.Latitude != "" && filter.Longitude != "") && (filter.Latitude != "0" && filter.Longitude != "0"))
                    {

                    }
                    else
                    {
                        if (filter.Province != null && filter.Province != string.Empty)
                        {
                            reQuery = reQuery.Where(x => x.Provinsi.Contains(filter.Province));
                        }

                        if (filter.City != null && filter.City != string.Empty)
                        {
                            reQuery = reQuery.Where(x => x.Kota.Contains(filter.City));
                        }

                        if (filter.Alamat != null && filter.Alamat != string.Empty)
                        {
                            reQuery = reQuery.Where(x => x.Alamat.Contains(filter.Alamat));
                        }
                    }



                    int pageNumber = filter.PageNumber - 1;

                    reQuery = reQuery.Skip(pageNumber * filter.PageSize).Take(filter.PageSize);

                    result.Result = reQuery.ToList();
                    result.TotalPage = (query.Count() / filter.PageSize) + 1;
                    result.TotalData = query.Count();
                }
                else
                {

                    var query = repo.GetAll();

                    if (filter.Panjang != 0 && filter.Lebar == 0)
                    {
                        query = (from q in query
                                 join d in db.TitikLokasiDetail on q.ID equals d.TitikLokasiID
                                 where d.Panjang == filter.Panjang
                                 select new TitikLokasi
                                 {
                                     ID = q.ID,
                                     AdaKontruksi = q.AdaKontruksi,
                                     AddressReal = q.AddressReal,
                                     Address = q.Address,
                                     AdvTypeID = q.AdvTypeID,
                                     RateScoreAverage = q.RateScoreAverage,
                                     Cabang = q.Cabang,
                                     CreateByUserID = q.CreateByUserID,
                                     CreateDate = q.CreateDate,
                                     DeletedDate = q.DeletedDate,
                                     DeletedByUserID = q.DeletedByUserID,
                                     HorV = q.HorV,
                                     KelasJalan = q.KelasJalan,
                                     KodeFile = q.KodeFile,
                                     Kota = q.Kota,
                                     Lampu = q.Lampu,
                                     Latitude = q.Latitude,
                                     LastUpdateByUserID = q.LastUpdateByUserID,
                                     LastUpdateDate = q.LastUpdateDate,
                                     Longitude = q.Longitude,
                                     NoBillboard = q.NoBillboard,
                                     OwnerByUserID = q.OwnerByUserID,
                                     PIC = q.PIC,
                                     Provinsi = q.Pulau,
                                     RateScoreTotal = q.RateScoreTotal,
                                     Pulau = q.Pulau,
                                     Status = q.Status,
                                     Tinggi = q.Tinggi,
                                     TotalView = q.TotalView,
                                     TransaksiCount = q.TransaksiCount,
                                     Type = q.Type
                                 });



                    }

                    if (filter.Lebar != 0 && filter.Panjang == 0)
                    {
                        query = (from q in query
                                 join d in db.TitikLokasiDetail on q.ID equals d.TitikLokasiID
                                 where d.Lebar == filter.Lebar
                                 select new TitikLokasi
                                 {
                                     ID = q.ID,
                                     AdaKontruksi = q.AdaKontruksi,
                                     AddressReal = q.AddressReal,
                                     Address = q.Address,
                                     AdvTypeID = q.AdvTypeID,
                                     RateScoreAverage = q.RateScoreAverage,
                                     Cabang = q.Cabang,
                                     CreateByUserID = q.CreateByUserID,
                                     CreateDate = q.CreateDate,
                                     DeletedDate = q.DeletedDate,
                                     DeletedByUserID = q.DeletedByUserID,
                                     HorV = q.HorV,
                                     KelasJalan = q.KelasJalan,
                                     KodeFile = q.KodeFile,
                                     Kota = q.Kota,
                                     Lampu = q.Lampu,
                                     Latitude = q.Latitude,
                                     LastUpdateByUserID = q.LastUpdateByUserID,
                                     LastUpdateDate = q.LastUpdateDate,
                                     Longitude = q.Longitude,
                                     NoBillboard = q.NoBillboard,
                                     OwnerByUserID = q.OwnerByUserID,
                                     PIC = q.PIC,
                                     Provinsi = q.Pulau,
                                     RateScoreTotal = q.RateScoreTotal,
                                     Pulau = q.Pulau,
                                     Status = q.Status,
                                     Tinggi = q.Tinggi,
                                     TotalView = q.TotalView,
                                     TransaksiCount = q.TransaksiCount,
                                     Type = q.Type
                                 });


                    }

                    if (filter.Panjang != 0 && filter.Lebar != 0)
                    {
                        query = (from q in query
                                 join d in db.TitikLokasiDetail on q.ID equals d.TitikLokasiID
                                 where d.Lebar == filter.Lebar && d.Panjang == filter.Panjang
                                 select new TitikLokasi
                                 {
                                     ID = q.ID,
                                     AdaKontruksi = q.AdaKontruksi,
                                     AddressReal = q.AddressReal,
                                     Address = q.Address,
                                     AdvTypeID = q.AdvTypeID,
                                     RateScoreAverage = q.RateScoreAverage,
                                     Cabang = q.Cabang,
                                     CreateByUserID = q.CreateByUserID,
                                     CreateDate = q.CreateDate,
                                     DeletedDate = q.DeletedDate,
                                     DeletedByUserID = q.DeletedByUserID,
                                     HorV = q.HorV,
                                     KelasJalan = q.KelasJalan,
                                     KodeFile = q.KodeFile,
                                     Kota = q.Kota,
                                     Lampu = q.Lampu,
                                     Latitude = q.Latitude,
                                     LastUpdateByUserID = q.LastUpdateByUserID,
                                     LastUpdateDate = q.LastUpdateDate,
                                     Longitude = q.Longitude,
                                     NoBillboard = q.NoBillboard,
                                     OwnerByUserID = q.OwnerByUserID,
                                     PIC = q.PIC,
                                     Provinsi = q.Pulau,
                                     RateScoreTotal = q.RateScoreTotal,
                                     Pulau = q.Pulau,
                                     Status = q.Status,
                                     Tinggi = q.Tinggi,
                                     TotalView = q.TotalView,
                                     TransaksiCount = q.TransaksiCount,
                                     Type = q.Type
                                 });


                    }

                    if (filter.StartDate != null && filter.EndDate != null)
                    {
                        BookDetailRepository bookDetailRepo = new BookDetailRepository(db);
                        var siteIdQuery = bookDetailRepo.CheckAvailableSite(filter.StartDate, filter.EndDate);

                        var siteIDCollection = siteIdQuery.Select(x => x.SiteID).Distinct().ToList();

                        query = query.Where(x => !siteIDCollection.Contains(x.ID));
                    }

                    if (roleName == "MDO")
                    {
                        query = query.Where(x => x.OwnerByUserID == filter.UserID);
                    }

                    if (!filter.showWithDisabledSite)
                    {
                        query = query.Where(x => x.Status == 1);
                    }

                    var reQuery = (from x in query
                                   select new SiteListOutputModel()
                                   {
                                       SiteID = x.ID,
                                       NoBillboard = x.NoBillboard,
                                       Cabang = x.Cabang,
                                       HorV = x.HorV == "H" ? "Horizontal" : "Vertical",
                                       Kota = x.Kota,
                                       Provinsi = x.Provinsi,
                                       Alamat = x.Address,
                                       Pulau = x.Pulau,
                                       Tipe = x.Type,
                                       RateScoreAverage = x.RateScoreAverage,
                                       Latitude = x.Latitude,
                                       Longitude = x.Longitude,
                                       TotalView = x.TotalView,
                                       Status = x.Status == 1 ? "Active" : "InActive",
                                       HargaPerhari = db.TitikLokasiPrice.Where(z => z.TitikLokasiID == x.ID).OrderBy(z => z.HargaAwal).Select(z => z.HargaAwal).FirstOrDefault(), // diambil harga paling murah karena ga mungkin untuk filter yang ada di detail
                                       MinimDasar = db.TitikLokasiPrice.Where(z => z.TitikLokasiID == x.ID).OrderBy(z => z.HargaAwal).Select(z => z.MinimDasar).FirstOrDefault(),
                                       MinimOrder = db.TitikLokasiPrice.Where(z => z.TitikLokasiID == x.ID).OrderBy(z => z.HargaAwal).Select(z => z.MinimOrder).FirstOrDefault(),
                                       ImageHeader = db.TitikLokasiImage.Where(y => y.TitikLokasiID == x.ID && y.IsThumbnail == "1").Select(z => z.PathImage).FirstOrDefault(),
                                       SiteItem = db.TitikLokasiDetail.Where(z => z.TitikLokasiID == x.ID).Select(z => new SiteListItemModel()
                                       {
                                           ArahLokasi = z.ArahLokasi,
                                           KodeLokasi = z.KodeArahLokasi,
                                           Panjang = z.Panjang,
                                           Lebar = z.Lebar,
                                           SiteItemID = z.ID,
                                           Catatan = z.Keterangan,
                                           Image = db.TitikLokasiImage.Where(y => y.TitikLokasiDetailID == z.ID).Select(y => new EditSiteImageInputModel()
                                           {
                                               Image = y.PathImage,
                                               SiteImageID = y.ID,
                                               IsThumbnail = y.IsThumbnail
                                           }).ToList(),
                                           Price = db.TitikLokasiPrice.Where(p => p.TitikLokasiDetailID == z.ID).Select(p => new EditSitePriceInputModel()
                                           {
                                               MinimDasar = p.MinimDasar,
                                               MinimOrder = p.MinimOrder,
                                               Dasar = p.Dasar,
                                               HargaAkhir = p.HargaAkhir,
                                               HargaAwal = p.HargaAwal,
                                               Kelipatan = p.Kelipatan,
                                               SitePriceID = p.ID,
                                               HargaPerhari = p.HargaAwal
                                           }).ToList(),
                                           Wishlist = filter.UserID == null ?
                                                                        new SiteWishlist() { isWishlist = false, WishlistID = Guid.Empty } :
                                                                        new SiteWishlist()
                                                                        {
                                                                            isWishlist = db.WishList.Count(w => w.SiteID == z.TitikLokasiID && w.SiteItemID == z.ID && w.UserID == filter.UserID && w.DeletedDate == null) > 0 ? true : false,
                                                                            WishlistID = db.WishList.Count(w => w.SiteID == z.TitikLokasiID && w.SiteItemID == z.ID && w.UserID == filter.UserID && w.DeletedDate == null) > 0 ? db.WishList.Where(w => w.SiteID == z.TitikLokasiID && w.SiteItemID == z.ID && w.UserID == filter.UserID && w.DeletedDate == null).FirstOrDefault().ID : Guid.Empty
                                                                        }
                                       }).ToList()
                                   });




                    if (filter.TypeBillboard != null && filter.TypeBillboard != string.Empty)
                    {
                        reQuery = reQuery.Where(x => x.Tipe == filter.TypeBillboard);
                    }

                    if (filter.MinimumPrice != 0)
                    {
                        reQuery = reQuery.Where(x => x.HargaPerhari >= filter.MinimumPrice);
                    }

                    if (filter.MaximumPrice != 0)
                    {
                        reQuery = reQuery.Where(x => x.HargaPerhari <= filter.MaximumPrice);
                    }


                    // Search by GeoLocation
                    if ((filter.Latitude != "" && filter.Longitude != "") && (filter.Latitude != "0" && filter.Longitude != "0"))
                    {




                    }
                    else
                    {
                        if (filter.Province != null && filter.Province != string.Empty)
                        {
                            reQuery = reQuery.Where(x => x.Provinsi.Contains(filter.Province));
                        }

                        if (filter.City != null && filter.City != string.Empty)
                        {
                            reQuery = reQuery.Where(x => x.Kota.Contains(filter.City));
                        }

                        if (filter.Alamat != null && filter.Alamat != string.Empty)
                        {
                            reQuery = reQuery.Where(x => x.Alamat.Contains(filter.Alamat));
                        }
                    }

                    if (filter.Sorting == Sort.None || filter.Sorting == Sort.AtoZ)
                    {
                        reQuery = reQuery.OrderBy(z => z.NoBillboard);
                    }
                    else if (filter.Sorting == Sort.ZtoA)
                    {
                        reQuery = reQuery.OrderByDescending(z => z.NoBillboard);
                    }
                    else if (filter.Sorting == Sort.Termahal)
                    {
                        reQuery = reQuery.OrderByDescending(z => z.HargaPerhari);
                    }
                    else if (filter.Sorting == Sort.Termurah)
                    {
                        reQuery = reQuery.OrderBy(z => z.HargaPerhari);
                    }


                    int pageNumber = filter.PageNumber - 1;



                    reQuery = reQuery.Skip(pageNumber * filter.PageSize).Take(filter.PageSize);

                    result.Result = reQuery.ToList();
                    result.TotalPage = (query.Count() / filter.PageSize) + 1;
                    result.TotalData = query.Count();
                }
            }



            return result;
        }

        public SiteDetailResultOutputModel GetSiteDetail(SiteDetailInputModel data)
        {
            SiteRepository repo = new SiteRepository(db);

            var headQuery = (from head in db.TitikLokasi
                             where head.ID == data.SiteID
                             select new SiteDetailOutputModel()
                             {
                                 SiteID = head.ID,
                                 Cabang = head.Cabang,
                                 HorV = head.HorV == "H" ? "Horizontal" : "Vertical",
                                 Kota = head.Kota,
                                 Pulau = head.Pulau,
                                 Latitude = head.Latitude,
                                 Longitude = head.Longitude,
                                 NoBillboard = head.NoBillboard,
                                 Tipe = head.Type,
                                 Provinsi = head.Provinsi,
                                 RateScoreAverage = head.RateScoreAverage,
                                 Alamat = head.Address,
                                 ImageHeader = db.TitikLokasiImage.Where(y => y.TitikLokasiID == head.ID && y.IsThumbnail == "1").Select(z => z.PathImage).FirstOrDefault(),
                                 TotalView = head.TotalView
                             }).FirstOrDefault();

            var ItemQuery = (from item in db.TitikLokasiDetail
                             where item.TitikLokasiID == data.SiteID && item.DeletedDate == null
                             select new SiteDetailItemModel()
                             {
                                 ArahLokasi = item.ArahLokasi,
                                 SiteID = item.TitikLokasiID,
                                 KodeLokasi = item.KodeArahLokasi,
                                 SiteItemID = item.ID,
                                 Panjang = item.Panjang,
                                 Lebar = item.Lebar,
                                 Price = db.TitikLokasiPrice.Where(x => x.TitikLokasiDetailID == item.ID).Select(x => new SiteDetailPriceModel
                                 {
                                     SitePriceID = x.ID,
                                     SiteItemID = x.TitikLokasiDetailID,
                                     Dasar = x.Dasar,
                                     HargaAkhir = x.HargaAkhir,
                                     HargaAwal = x.HargaAwal,
                                     Kelipatan = x.Kelipatan,
                                     MinimOrder = x.MinimOrder,
                                     MinimDasar = x.MinimDasar

                                 }).ToList(),
                                 Image = db.TitikLokasiImage.Where(x => x.TitikLokasiDetailID == item.ID).Select(x => new SiteDetailImageModel
                                 {
                                     SiteImageID = x.ID,
                                     SiteItemID = x.TitikLokasiDetailID,
                                     Image = x.PathImage
                                 }).ToList(),
                                 Wishlist = data.UserID == null ?
                                                                new SiteWishlist() { isWishlist = false, WishlistID = Guid.Empty } :
                                                                new SiteWishlist()
                                                                {
                                                                    isWishlist = db.WishList.Count(w => w.SiteID == data.SiteID && w.SiteItemID == item.ID && w.UserID == data.UserID && w.DeletedDate == null) > 0 ? true : false,
                                                                    WishlistID = db.WishList.Count(w => w.SiteID == data.SiteID && w.SiteItemID == item.ID && w.UserID == data.UserID && w.DeletedDate == null) > 0 ? db.WishList.Where(w => w.SiteID == data.SiteID && w.SiteItemID == item.ID && w.UserID == data.UserID && w.DeletedDate == null).FirstOrDefault().ID : Guid.Empty
                                                                }
                             }).ToList();

            SiteDetailResultOutputModel resData = new SiteDetailResultOutputModel();


            repo.CountViewSite(data.SiteID);

            resData.Detail = headQuery;
            resData.Item = ItemQuery;

            return resData;
        }

        public AddSiteOutputModel SavePriceSite(AddSiteResultPriceInputModel data)
        {
            SitePriceRepository priceRepo = new SitePriceRepository(db);

            List<TitikLokasiPrice> listSitePrice = new List<TitikLokasiPrice>();

            var today = DateTime.Now;

            foreach (var x in data.Price)
            {
                TitikLokasiPrice sp = new TitikLokasiPrice();
                sp.ID = Guid.NewGuid();
                sp.TitikLokasiID = data.SiteID;
                sp.Dasar = x.Dasar;
                sp.HargaAwal = x.HargaAwal;
                sp.HargaAkhir = x.HargaAkhir;
                sp.Kelipatan = x.Kelipatan;
                sp.CreateDate = today;
                sp.CreateByUserID = data.UserID;
                sp.TitikLokasiDetailID = data.SiteItemID;
                sp.MinimOrder = data.MinimOrder;
                sp.MinimDasar = data.MinimDasar;

                listSitePrice.Add(sp);
            }

            priceRepo.Insert(listSitePrice, true);

            AddSiteOutputModel output = new AddSiteOutputModel();
            output.SiteID = data.SiteID;

            return output;
        }

        public AddSiteOutputModel SaveImageSite(AddSiteResultImageInputModel data)
        {
            SiteImageRepository imageRepo = new SiteImageRepository(db);

            var today = DateTime.Now;
            List<TitikLokasiImage> listSiteImage = new List<TitikLokasiImage>();

            foreach (var y in data.Image)
            {
                TitikLokasiImage si = new TitikLokasiImage();
                si.TitikLokasiID = data.SiteID;
                si.PathImage = y.Image;
                si.ID = Guid.NewGuid();
                si.CreateByUserID = data.UserID;
                si.CreateDate = today;
                si.TitikLokasiDetailID = data.SiteitemID;

                listSiteImage.Add(si);
            }

            var res = imageRepo.Insert(listSiteImage, true);

            AddSiteOutputModel output = new AddSiteOutputModel();
            output.SiteID = data.SiteID;

            return output;


        }

        public AddSiteOutputModel SaveDetailSite(AddSiteResultDetailInputModel data)
        {


            SiteRepository siteRepo = new SiteRepository(db);

            Guid siteID = Guid.NewGuid();
            DateTime today = DateTime.Now;

            TitikLokasi site = new TitikLokasi();
            site.ID = siteID;
            site.NoBillboard = data.Detail.NoBillboard;
            site.Pulau = data.Detail.Pulau;
            site.Kota = data.Detail.Kota;
            site.Provinsi = data.Detail.Provinsi;
            site.Cabang = data.Detail.Cabang;
            site.HorV = data.Detail.HorV;
            site.Type = data.Detail.Tipe;
            site.Longitude = data.Detail.Longitude;
            site.Latitude = data.Detail.Latitude;
            site.CreateDate = today;
            site.CreateByUserID = data.UserID;
            site.OwnerByUserID = data.UserID;
            site.Status = 1;
            site.Address = data.Detail.Address;

            siteRepo.Insert(site, true);

            AddSiteOutputModel outputModel = new AddSiteOutputModel();

            outputModel.SiteID = siteID;

            return outputModel;
        }

        public AddSiteItemDetailOutputModel SaveDetailSiteItem(AddSiteItemDetailInputModel data)
        {
            Guid siteItemID = Guid.NewGuid();
            DateTime today = DateTime.Now;


            TitikLokasiDetail site = new TitikLokasiDetail();

            site.ID = siteItemID;
            site.TitikLokasiID = data.SiteID;
            site.KodeArahLokasi = data.KodeArahLokasi;
            site.ArahLokasi = data.ArahLokasi;
            site.Panjang = data.Panjang;
            site.Lebar = data.Lebar;

            SiteItemRepository repo = new SiteItemRepository(db);
            var res = repo.Insert(site, true);


            AddSiteItemDetailOutputModel ret = new AddSiteItemDetailOutputModel();
            ret.SiteItemDetailID = res.ID;
            return ret;


        }

        public void ImportSite(ImportSiteInputModel data)
        {
            if (data.Input.Count > 0)
            {

                SiteRepository siteRepo = new SiteRepository(db);
                SiteImageRepository imageRepo = new SiteImageRepository(db);
                SitePriceRepository priceRepo = new SitePriceRepository(db);
                SiteItemRepository itemRepo = new SiteItemRepository(db);

                DateTime today = DateTime.Now;


                List<TitikLokasiPrice> listSitePrice = new List<TitikLokasiPrice>();

                List<TitikLokasiDetail> listSiteItem = new List<TitikLokasiDetail>();

                foreach (var i in data.Input)
                {
                    //Check data ada atau tidak
                    //kalau ada maka tidak boleh masuk
                    //kalau belum ada maka harus di import
                    var check = siteRepo.GetSiteByNoBillboard(i.No).FirstOrDefault();

                    Guid siteID = Guid.NewGuid();

                    List<ImportSiteModel> currentData = new List<ImportSiteModel>();

                    currentData = (from x in data.Input
                                   where x.No == i.No
                                   select x).ToList();

                    //check hanya null yg tidak ada boleh masuk
                    if (check == null)
                    {

                        //Master Section

                        TitikLokasi site = new TitikLokasi();
                        site.ID = siteID;
                        site.NoBillboard = currentData[0].No;
                        site.Pulau = currentData[0].Pulau;
                        site.Kota = currentData[0].Kota;
                        site.Provinsi = currentData[0].Provinsi;
                        site.Cabang = currentData[0].NamaCabang;
                        site.HorV = currentData[0].HorV;
                        site.Type = currentData[0].Type;
                        site.Longitude = currentData[0].Longitude;
                        site.Latitude = currentData[0].Latitude;
                        site.CreateByUserID = data.UserID;
                        site.CreateDate = today;
                        site.Address = currentData[0].Alamat;
                        site.AddressReal = currentData[0].Alamat;
                        site.KelasJalan = currentData[0].KelasJalan;
                        site.Status = 1;

                        siteRepo.Insert(site, false);
                    }


                    foreach (var selected in currentData)
                    {

                        //check kalau check != null siteID di isi dari check.ID
                        //kalau check == null ga usah di isi daari check.ID
                        if (check != null)
                        {
                            siteID = check.ID;
                        }

                        Guid siteItemID = Guid.NewGuid();

                        TitikLokasiDetail siteItem = new TitikLokasiDetail();

                        siteItem.ID = siteItemID;
                        siteItem.TitikLokasiID = siteID;
                        siteItem.ArahLokasi = selected.ArahLokasi;
                        siteItem.KodeArahLokasi = selected.KodeArahLokasi;
                        siteItem.Keterangan = selected.Keterangan;
                        siteItem.Lebar = selected.Lebar;
                        siteItem.Panjang = selected.Panjang;
                        siteItem.CreateByUserID = data.UserID;
                        siteItem.CreateDate = today;

                        if (check != null && check.ID != null && check.ID != Guid.Empty)
                        {
                            itemRepo.DeleteBySiteID(check.ID, data.UserID, false);

                        }


                        itemRepo.Insert(siteItem, false);

                        //Price Section
                        listSitePrice = new List<TitikLokasiPrice>();
                        foreach (var cur in currentData)
                        {

                            //check apa ada data dengan siteItemID itu dan harga awal itu
                            var sitePrice = priceRepo.FindPrice(siteItemID, cur.HargaAwal).FirstOrDefault();

                            TitikLokasiPrice sp = new TitikLokasiPrice();

                            //kalau sitePrice == null sp
                            if (sitePrice == null)
                            {
                                sp.ID = Guid.NewGuid();
                                sp.TitikLokasiDetailID = siteItemID;
                                sp.TitikLokasiID = siteID;
                                sp.Dasar = cur.Satuan;
                                sp.HargaAwal = cur.HargaAwal;
                                sp.HargaAkhir = cur.HargaAkhir;
                                sp.Kelipatan = cur.Qty;
                                sp.CreateByUserID = data.UserID;
                                sp.CreateDate = today;
                                sp.MinimOrder = cur.MinimOrder;
                                sp.MinimDasar = cur.MinimDasar;

                                listSitePrice.Add(sp);
                            }
                        }
                    }

                    priceRepo.DeleteRangeBySiteID(siteID, data.UserID, false);

                    priceRepo.Insert(listSitePrice, true);

                }
            }
        }

        public EditSiteOutputModel Edit(EditSiteResultInputModel data)
        {
            SiteRepository siteRepo = new SiteRepository(db);
            SiteImageRepository imageRepo = new SiteImageRepository(db);
            SitePriceRepository priceRepo = new SitePriceRepository(db);
            SiteItemRepository itemRepo = new SiteItemRepository(db);

            var oldData = siteRepo.GetSiteByID(data.Detail.SiteID).FirstOrDefault();

            DateTime today = DateTime.Now;

            List<TitikLokasiPrice> listSitePrice = new List<TitikLokasiPrice>();

            List<TitikLokasiDetail> listSiteItem = new List<TitikLokasiDetail>();

            List<TitikLokasiImage> listSiteImage = new List<TitikLokasiImage>();

            if (data.Detail != null)
            {
                TitikLokasi site = new TitikLokasi();
                site.ID = data.Detail.SiteID;
                site.NoBillboard = data.Detail.NoBillboard;
                site.Pulau = data.Detail.Pulau;
                site.Kota = data.Detail.Kota;
                site.Provinsi = data.Detail.Provinsi;
                site.Cabang = data.Detail.Cabang;
                site.HorV = data.Detail.HorV;
                site.Type = data.Detail.Tipe;
                site.Status = (int)StatusTitikLokasiEnum.Active;
                site.Longitude = data.Detail.Longitude;
                site.Address = data.Detail.Address;
                site.AddressReal = data.Detail.Address;
                site.Latitude = data.Detail.Latitude;
                site.LastUpdateByUserID = data.UserID;
                site.LastUpdateDate = today;
                site.CreateByUserID = oldData.CreateByUserID;
                site.CreateDate = oldData.CreateDate;

                siteRepo.Update(site, true);
            }

            if (data.Item != null && data.Item.Count > 0)
            {
                // Edit
                foreach (var z in data.Item)
                {
                    TitikLokasiDetail item = new TitikLokasiDetail();

                    item.ArahLokasi = z.ArahLokasi;
                    item.KodeArahLokasi = z.ArahLokasi;
                    item.ID = z.SiteItemID;
                    item.TitikLokasiID = z.SiteID;
                    item.LastUpdateByUserID = data.UserID;
                    item.LastUpdateDate = today;
                    item.Panjang = z.Panjang;
                    item.Lebar = z.Lebar;

                    listSiteItem.Add(item);

                    if (data.Price != null && data.Price.Count > 0)
                    {
                        foreach (var x in data.Price)
                        {
                            TitikLokasiPrice sp = new TitikLokasiPrice();
                            sp.ID = x.SitePriceID;
                            sp.TitikLokasiID = data.Detail.SiteID;
                            sp.Dasar = x.Dasar;
                            sp.HargaAwal = x.HargaAwal;
                            sp.HargaAkhir = 0;
                            sp.Kelipatan = x.Kelipatan;
                            sp.LastUpdateByUserID = data.UserID;
                            sp.LastUpdateDate = today;
                            sp.CreateDate = oldData.CreateDate;
                            sp.CreateByUserID = oldData.CreateByUserID;
                            sp.TitikLokasiDetailID = z.SiteItemID;
                            sp.MinimOrder = x.MinimOrder;
                            sp.MinimDasar = x.MinimDasar;

                            listSitePrice.Add(sp);
                        }

                        priceRepo.Update(data.Detail.SiteID, listSitePrice, false);
                    }

                    if (data.Image != null && data.Image.Count > 0)
                    {

                        foreach (var y in data.Image)
                        {
                            TitikLokasiImage si = new TitikLokasiImage();
                            si.TitikLokasiID = data.Detail.SiteID;
                            si.PathImage = y.Image;
                            si.ID = y.SiteImageID;
                            si.LastUpdateByUserID = data.UserID;
                            si.LastUpdateDate = today;
                            si.CreateByUserID = oldData.CreateByUserID;
                            si.CreateDate = oldData.CreateDate;
                            si.TitikLokasiDetailID = z.SiteItemID;

                            listSiteImage.Add(si);
                        }

                        imageRepo.Update(data.Detail.SiteID, listSiteImage, false);

                    }

                }

                itemRepo.Update(data.Detail.SiteID, listSiteItem, true, ModeEnum.Edit);
            }

            EditSiteOutputModel outputModel = new EditSiteOutputModel();

            //Untuk mempercepat proses, output diambil bukan dari grid tapi dari data yg di Inputkan :p
            outputModel.Detail = data.Detail;
            outputModel.Image = data.Image;
            outputModel.Price = data.Price;
            outputModel.Item = data.Item;


            return outputModel;
        }

        public CheckAvailableResultModel CheckSiteAvailable(CheckDateAvailableInputModel data)
        {

            var res = (from b in db.BookDetail
                       where b.SiteID == data.SiteID && b.SiteItemID == data.SiteItemID && b.StartDate >= data.StartDate && b.EndDate <= data.EndDate && b.DeletedDate == null
                       select new CheckDateAvailableOutputModel()
                       {
                           BookDetailID = b.ID
                       }).ToList();

            CheckAvailableResultModel result = new CheckAvailableResultModel();
            result.Result = res;
            result.CanBook = res.Count() > 0 ? false : true; //KALAU LEBIH DARI 0 BERARTI ADA YANG BOOKING. JADI GA BISA DI BOOKING

            return result;

        }

    }
}
