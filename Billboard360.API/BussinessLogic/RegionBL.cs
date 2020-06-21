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

namespace Billboard360.API.BussinessLogic
{
    public class RegionBL
    {
        protected readonly DatabaseContext db;

        public RegionBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        
    }
}
