using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;

namespace Billboard360.API.BussinessLogic
{
    public class PositionBL
    {
        protected readonly DatabaseContext DbContext;

        public PositionBL(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        //public PositionResponse Save(PositionInputModel data)
        //{
        //    Position temp = new Position();

        //    temp.Name = data.Name;
        //    temp.CreateDate = DateTime.Now;

        //    if(data.CurrUserID != Guid.Empty)
        //    {
        //        temp.CreateByUserID = data.CurrUserID;
        //    } else
        //    {
        //        temp.CreateByUserID = Guid.Empty;
        //    }
            

        //    PositionRepository posRepo = new PositionRepository(DbContext);
        //    var response = posRepo.Insert(temp);

        //    PositionResponse posResponse = new PositionResponse();

        //    posResponse.ID = response.ID;
        //    posResponse.Message = response.Message;

        //    return posResponse;


        //}
    }
}
