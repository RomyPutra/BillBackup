using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Billboard360.API.Models;
using Billboard360.API.BussinessLogic;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;
using Billboard360.API.Core;

namespace Billboard360.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly DatabaseContext db;
        AppSettings AppSettings { get; }


        public BaseController(DatabaseContext dbContext, AppSettings appSettings)
        {
            db = dbContext;
            AppSettings = appSettings;
        }


        [Route("GetBillboardType")]
        [HttpGet]
        public ActionResult<BillboardTypeListResponseModel> GetBillboardType()
        {
            BillboardTypeListResponseModel res = new BillboardTypeListResponseModel();
            try
            {
                BillboardTypeRepository repo = new BillboardTypeRepository(db);

                var query = repo.GetListAll();

                var data = (from x in query
                            select new BillboardTypeListOutputModel
                            {
                                ID = x.ID,
                                Kode = x.Kode,
                                Type = x.Type
                            }).ToList();

                res.data = data;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = true;
                return res;
            }


        }


        [Route("AddBillboardType")]
        [HttpPost]
        public ActionResult<AddBillboardTypeResponseModel> AddBillboardType([FromBody]AddBillboardTypeInputModel data)
        {
            AddBillboardTypeResponseModel res = new AddBillboardTypeResponseModel();
            try
            {
                BillboardTypeBL bl = new BillboardTypeBL(db);
                var x = bl.Save(data);

                res.data = x;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }



        [Route("GetProvince")]
        [HttpPost]
        public ActionResult<ProvinceResponseModel> GetProvince()
        {
            ProvinceResponseModel res = new ProvinceResponseModel();
            try
            {

                ProvinceRepository repo = new ProvinceRepository(db);

                var query = repo.GetAll();

                var res2 = (from y in query
                            select new ProviceOutputModel
                            {
                                ProvinceID = y.ID,
                                Kode = y.Kode,
                                Provinsi = y.Provinsi
                            }).OrderBy(x=> x.Provinsi).ToList();

                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }

        [Route("GetProvinceDetail")]
        [HttpGet]
        public ActionResult<ProvinceDetailResponseModel> GetProvinceDetail(Guid provinceID)
        {
            ProvinceDetailResponseModel res = new ProvinceDetailResponseModel();
            try
            {

                ProvinceRepository repo = new ProvinceRepository(db);

                var query = repo.GetAll();

                var res2 = (from y in query
                            where y.ID == provinceID
                            select new ProviceOutputModel
                            {
                                ProvinceID = y.ID,
                                Kode = y.Kode,
                                Provinsi = y.Provinsi
                            }).FirstOrDefault();

                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }

        [Route("AddProvince")]
        [HttpPost]
        public ActionResult<AddProvinceResponseModel> AddProvince([FromBody]AddProviceInputModel data)
        {
            AddProvinceResponseModel res = new AddProvinceResponseModel();
            try
            {
                ProvinceBL bl = new ProvinceBL(db);
                var res2 = bl.Save(data);

                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }

        [Route("EditProvince")]
        [HttpPost]
        public ActionResult<EditProvinceResponseModel> EditProvince([FromBody]EditProviceInputModel data)
        {
            EditProvinceResponseModel res = new EditProvinceResponseModel();
            try
            {
                ProvinceBL bl = new ProvinceBL(db);
                var res2 = bl.Edit(data);

                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }

        [Route("DeleteProvince")]
        [HttpPost]
        public ActionResult<DeleteProvinceResponseModel> DeleteProvince([FromBody]DeleteProvinceInputModel data)
        {
            DeleteProvinceResponseModel res = new DeleteProvinceResponseModel();
            try
            {
                ProvinceBL bl = new ProvinceBL(db);
                var res2 = bl.Delete(data);

                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }


        [Route("GetCity")]
        [HttpPost]
        public ActionResult<CityResponseModel> GetCity([FromBody] CityInputModel data)
        {
            CityResponseModel res = new CityResponseModel();
            try
            {

                CityRepository repo = new CityRepository(db);

                var query = repo.GetCities(data.KodeProv);

                var res2 = (from y in query
                            select new CityOuputModel
                            {
                                ProvinceID = y.ProvinceID,
                                CityID = y.ID,
                                Kode = y.Kode,
                                Kota = y.CityName
                            }).OrderBy(x => x.Kota).ToList();

                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }

        [Route("GetCityDetail")]
        [HttpGet]
        public ActionResult<CityDetailResponseModel> GetCityDetail(Guid cityID)
        {
            CityDetailResponseModel res = new CityDetailResponseModel();
            try
            {

                CityRepository repo = new CityRepository(db);

                var query = repo.GetCitiesDetail(cityID);

                var res2 = (from y in query
                            select new CityOuputModel
                            {
                                ProvinceID = y.ProvinceID,
                                CityID = y.ID,
                                Kode = y.Kode,
                                Kota = y.CityName
                            }).FirstOrDefault();

                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }

        [Route("AddCity")]
        [HttpPost]
        public ActionResult<AddCityResponseModel> AddCity([FromBody]AddCityInputModel data)
        {
            AddCityResponseModel res = new AddCityResponseModel();
            try
            {
                CityBL bl = new CityBL(db);
                var res2 = bl.Save(data);

                res.data = res2;
                res.Message = res.Message;
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }

        [Route("EditCity")]
        [HttpPost]
        public ActionResult<EditCityResponseModel> EditCity([FromBody]EditCityInputModel data)
        {
            EditCityResponseModel res = new EditCityResponseModel();
            try
            {
                CityBL bl = new CityBL(db);
                var res2 = bl.Update(data);

                res.data = res2;
                res.Message = res.Message;
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }



        [Route("GetAllCity")]
        [HttpPost]
        public ActionResult<CityResponseModel> GetAllCity()
        {
            CityResponseModel res = new CityResponseModel();
            try
            {

                CityRepository repo = new CityRepository(db);

                var query = repo.GetAllCities();

                var res2 = (from y in query
                            select new CityOuputModel
                            {
                                ProvinceID = y.ProvinceID,
                                CityID = y.ID,
                                Kode = y.Kode,
                                Kota = y.CityName
                            }).ToList();

                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }


        [Route("GetRoleDropDown")]
        [HttpPost]
        public ActionResult<RoleResponseModel> GetRole([FromBody] IsRoleInputModel data)
        {
            RoleResponseModel res = new RoleResponseModel();
            try
            {

                RoleRepository repo = new RoleRepository(db);

                List<RoleOutputModel> res2 = new List<RoleOutputModel>();

                var query = repo.GetRoleDropDown();

                if (data.IsSupervisor)
                {
                    res2 = (from y in query
                            where y.Name == "SPV"
                            select new RoleOutputModel()
                            {
                                RoleCode = y.Name,
                                RoleID = y.ID,
                                RoleName = y.NormalizedName
                            }).ToList();
                }
                else
                {
                    res2 = (from y in query
                            where y.Name != "SPV"
                            select new RoleOutputModel()
                            {
                                RoleCode = y.Name,
                                RoleID = y.ID,
                                RoleName = y.NormalizedName
                            }).ToList();
                }


                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }


        //Mas Adhit dan Mas bayu minta filter provinsi ambil dari provinsi yang ada di table TitikLokasi, bukan yang dari table province
        [Route("GetProvinceNew")]
        [HttpPost]
        public ActionResult<ProvinceResponseModel> GetProvinceNew()
        {
            ProvinceResponseModel res = new ProvinceResponseModel();
            try
            {

                ProvinceRepository repo = new ProvinceRepository(db);

                var query = repo.GetAllFromTitikLokasiTable();

                var res2 = (from y in query
                            select new ProviceOutputModel
                            {
                                ProvinceID = Guid.Empty,
                                Kode = 0,
                                Provinsi = y.Provinsi
                            }).Distinct().OrderBy(x => x.Provinsi).ToList();

                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }

        //Mas Adhit dan Mas bayu minta filter city ambil dari provinsi yang ada di table TitikLokasi, bukan yang dari table city
        [Route("GetCityNew")]
        [HttpPost]
        public ActionResult<CityResponseModel> GetCityNew()
        {
            CityResponseModel res = new CityResponseModel();
            try
            {

                CityRepository repo = new CityRepository(db);

                var query = repo.GetAllCitiesFromTitikLokasi();

                var res2 = (from y in query
                            select new CityOuputModel
                            {
                                ProvinceID = Guid.Empty,
                                CityID = Guid.Empty,
                                Kode = 0,
                                Kota = y.CityName
                            }).Distinct().ToList();

                res.data = res2;
                res.Message = "Success get data";
                res.Response = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }
        }


        [Route("SyncProvinceFromAPI")]
        [HttpPost]
        public ActionResult<SyncProvinceAPIResponseModel> GetProvinceFromAPI()
        {
            SyncProvinceAPIResponseModel res = new SyncProvinceAPIResponseModel();
            try
            {

                ProvinceBL bl = new ProvinceBL(db);

                var query = bl.SyncProviceFromAPI(AppSettings.URLSyncProvinceCity);

                return query;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
                res.error = false;
                return res;
            }
        }

        [Route("SyncCityFromAPI")]
        [HttpPost]
        public ActionResult<SyncCityMessageResponseModel> GetCityFromAPI()
        {
            SyncCityMessageResponseModel res = new SyncCityMessageResponseModel();
            try
            {

                CityBL bl = new CityBL(db);

                var query = bl.SyncCity(AppSettings.URLSyncProvinceCity);

                return query;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
                res.error = false;
                return res;
            }
        }




    }

}