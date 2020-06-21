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

namespace Billboard360.API.BussinessLogic
{
    public class BillboardTypeBL
    {
        protected readonly DatabaseContext db;

        public BillboardTypeBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public AddBillboardTypeOutputModel Save(AddBillboardTypeInputModel data)
        {
            BillboardType temp = new BillboardType();

            temp.Kode = data.Kode;
            temp.Type = data.Type;

            BillboardTypeRepository repo = new BillboardTypeRepository(db);
            var res = repo.Insert(temp);

            AddBillboardTypeOutputModel output = new AddBillboardTypeOutputModel();
            output.ID = res.ID;

            return output;
        }
    }
}
