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

namespace Billboard360.API.Controllers
{
    public class LoggerController : ControllerBase
    {
        protected readonly DatabaseContext DbContext;


        public LoggerController(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

    }
}