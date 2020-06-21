using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.API.Models.Enums;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;

namespace Billboard360.API.BussinessLogic
{
    public class LogAppBL
    {
        protected readonly DatabaseContext db;

        public LogAppBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public void SaveLog(LogAppEnum actionType, Guid userID, string message)
        {
            LogRepository repo = new LogRepository(db);

            LogActivity data = new LogActivity();

            int kodeLogger = (int)actionType;

            data.CreateByUserID = userID;
            data.CreateDate = DateTime.Now;
            data.KodeLogger = actionType.ToString();
            data.Message = message;

            repo.Insert(data, true);


        }
    }
}
