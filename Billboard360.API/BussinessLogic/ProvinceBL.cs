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
    public class ProvinceBL
    {
        protected readonly DatabaseContext db;

        public ProvinceBL(DatabaseContext dbContext)
        {
            db = dbContext;
            
        }

        public AddProvinceOutputModel Save(AddProviceInputModel data)
        {
            ProvinceRepository repo = new ProvinceRepository(db);

            Province temp = new Province();

            temp.Kode = data.Kode;
            temp.Provinsi = data.Provinsi;
            temp.CreateByUserID = data.UserID;
            temp.CreateDate = DateTime.Now;

            var res = repo.Insert(temp);

            AddProvinceOutputModel xres = new AddProvinceOutputModel();
            xres.ProvinsiID = res.ID;

            return xres;

        }

        public EditProviceOutputModel Edit(EditProviceInputModel data)
        {
            ProvinceRepository repo = new ProvinceRepository(db);

            Province temp = new Province();

            temp.ID = data.ProvinsiID;
            temp.Kode = data.Kode;
            temp.Provinsi = data.Provinsi;
            temp.LastUpdateByUserID = data.UserID;
            temp.LastUpdateDate = DateTime.Now;

            var res = repo.Update(temp);

            EditProviceOutputModel xres = new EditProviceOutputModel();


            xres.ProvinsiID = data.ProvinsiID;
            xres.Kode = data.Kode;
            xres.Provinsi = data.Provinsi;


            return xres;

        }

        public DeleteProviceOutputModel Delete(DeleteProvinceInputModel data)
        {
            ProvinceRepository repo = new ProvinceRepository(db);

            Province temp = new Province();

            temp.ID = data.ProvinsiID;
            temp.DeletedByUserID = data.UserID;
            temp.DeletedDate = DateTime.Now;

            var res = repo.DeleteProvince(temp);

            DeleteProviceOutputModel xres = new DeleteProviceOutputModel();

            xres.Message = "Success";


            return xres;

        }

        public SyncProvinceAPIResponseModel SyncProviceFromAPI(string baseURL)
        {

            RestClient rest = new RestClient(baseURL);

            RestRequest req = new RestRequest("provinsi", Method.GET);

            req.AddHeader("Accept", "application/json");

            req.AddHeader("Content-Type", "application/json");
            

            var res = rest.Execute(req);

            var result = JsonConvert.DeserializeObject<SyncProvinceAPIResponseModel>(res.Content);



            List<Province> listTemp = new List<Province>();

            if(!result.error)
            {
                var today = DateTime.Now;
                foreach(var x in result.provinsi)
                {
                    Province temp = new Province();
                    temp.ID = Guid.NewGuid();
                    temp.Kode = Convert.ToInt32(x.id);
                    temp.Provinsi = x.nama;
                    temp.CreateByUserID = Guid.Empty;
                    temp.CreateDate = today;

                    listTemp.Add(temp);
                }

                ProvinceRepository repo = new ProvinceRepository(db);

                repo.InsertList(listTemp);
            }
            
            return result;
        }
    }
}
