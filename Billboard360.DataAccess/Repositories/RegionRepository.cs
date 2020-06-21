using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace Billboard360.DataAccess.Repositories
{
    public class RegionRepository
    {
        DatabaseContext db = new DatabaseContext();
        public RegionRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<Region> GetListRegion()
        {
            return db.Region.Where(x => x.DeletedDate == null);
        }

        public IQueryable<Region> GetRegionByID(Guid id)
        {
            return db.Region.Where(x => x.ID == id && x.DeletedDate == null);
        }

        public IQueryable<Region> GetCityByKodeProv(string kodeProv)
        {
            return db.Region.Where(x => x.KodeProvinsi == kodeProv && x.DeletedDate == null);
        }

        public IQueryable<Region> GetProvince()
        {
            return db.Region.FromSql("SELECT DISTINCT KodeProvinsi, Provinsi FROM Region");
        }

        
    }
}
