using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Billboard360.API.Models;
using Billboard360.API.BussinessLogic;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;
using Billboard360.Helper.Engine;
using Newtonsoft.Json;
using Billboard360.API.Core;
using Newtonsoft;
using Billboard360.API.Models.Enums;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using RestSharp.Deserializers;
using System.IO;
using RestSharp.Extensions;
using RestSharp.Authenticators;
using Billboard360.Helper.Encrypt;
using System.Net;
using System.Text;
using System.Collections.Specialized;


namespace Billboard360.API.BussinessLogic
{
    public class CityBL
    {
        protected readonly DatabaseContext db;

        public CityBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public AddCityOutputModel Save(AddCityInputModel data)
        {
            CityRepository repo = new CityRepository(db);

            City temp = new City();

            var tempKodeProvinsi = Convert.ToInt32(data.KodeProvinsi);

            temp.CityName = data.CityName;
            temp.Kode = Convert.ToInt32(data.Kode);
            temp.KodeProvinsi = Convert.ToInt32(data.KodeProvinsi);
            temp.ProvinceID = db.Province.Where(x => x.Kode == tempKodeProvinsi).FirstOrDefault().ID;
            temp.CreateByUserID = data.UserID;
            temp.CreateDate = DateTime.Now;

            var res = repo.Insert(temp);

            AddCityOutputModel output = new AddCityOutputModel();
            output.CityID = res.ID;
            output.CityName = data.CityName;

            return output;
        }

        public EditCityOutputModel Update(EditCityInputModel data)
        {
            CityRepository repo = new CityRepository(db);

            City temp = new City();

            var tempKodeProvinsi = Convert.ToInt32(data.KodeProvinsi);

            temp.ID = data.CityID;
            temp.CityName = data.CityName;
            temp.Kode = Convert.ToInt32(data.Kode);
            temp.KodeProvinsi = Convert.ToInt32(data.KodeProvinsi);
            temp.ProvinceID = db.Province.Where(x => x.Kode == tempKodeProvinsi).FirstOrDefault().ID;
            temp.LastUpdateByUserID = data.UserID;
            temp.LastUpdateDate = DateTime.Now;

            var res = repo.Update(temp);

            EditCityOutputModel output = new EditCityOutputModel();
            output.CityID = res.ID;
            output.CityName = data.CityName;

            return output;
        }

        public SyncCityMessageResponseModel SyncCity(string baseURL)
        {
            var listProvinceInfo = db.Province.Select(x => x).ToList();

            bool error = false;
            string message = string.Empty;

            List<City> listCityAll = new List<City>();
            var today = DateTime.Now;

            foreach(var prov in listProvinceInfo)
            {
                List<City> listCurrCity = new List<City>();

                var currUrl = baseURL + "provinsi/" + prov.Kode + "/";

                RestClient rest = new RestClient(currUrl);

                RestRequest req = new RestRequest("kabupaten", Method.GET);

                req.AddHeader("Accept", "application/json");

                req.AddHeader("Content-Type", "application/json");

                var res = rest.Execute(req);

                var result = JsonConvert.DeserializeObject<SyncCityResponseModel>(res.Content);

                if(!result.error && result.kabupatens != null)
                {
                    foreach(var pos in result.kabupatens)
                    {
                        City temp = new City();

                        temp.ID = Guid.NewGuid();
                        temp.ProvinceID = prov.ID;
                        temp.KodeProvinsi = Convert.ToInt32(pos.id_prov);
                        temp.Kode = Convert.ToInt32(pos.id);
                        temp.CityName = pos.nama.StartsWith("Kab. ") ? pos.nama.Replace("Kab. ", "") :
                                        pos.nama.StartsWith("Kota ") ? pos.nama.Replace("Kota ", "") : pos.nama;
                        temp.CreateByUserID = Guid.Empty;
                        temp.CreateDate = today;

                        listCurrCity.Add(temp);
                    }

                    listCityAll.AddRange(listCurrCity);
                }
            }

            CityRepository repo = new CityRepository(db);
           var lex= repo.InsertList(listCityAll);

            SyncCityMessageResponseModel response = new SyncCityMessageResponseModel();

            response.error = lex.Result;
            response.message = lex.Message;

            return response;

        }


    }
}
