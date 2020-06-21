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
    public class RateBL
    {
        protected readonly DatabaseContext db;

        public RateBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public AddRateOutputModel Save(AddRateInputModel data)
        {
            RateRepository repo = new RateRepository(db);
            AddRateOutputModel output = new AddRateOutputModel();

            LogRateSite temp = new LogRateSite();
            temp.SiteID = data.SiteID;
            temp.RateScore = data.RateScore;
            temp.UserID = data.UserID;

            var res = repo.Insert(temp);

            if(res.ID != Guid.Empty)
            {
                SiteRepository siteRepo = new SiteRepository(db);
                var updateRes = siteRepo.UpdateRateScore(temp);

                output.ID = updateRes.ID;
            }

            return output;
        }
    }
}
