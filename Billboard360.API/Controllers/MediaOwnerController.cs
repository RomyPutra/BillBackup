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
using Billboard360.API.Models.Enums;

namespace Billboard360.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaOwnerController : ControllerBase
    {
        protected readonly DatabaseContext DbContext;


        public MediaOwnerController(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult<RegisterResponseModel> Register([FromBody] RegisterInputModel data)
        {
            try
            {
                RegisterBL userBL = new RegisterBL(DbContext);
                return userBL.Register(data, RoleEnum.MediaOwner);
            }
            catch (Exception ex)
            {
                RegisterResponseModel logres = new RegisterResponseModel();
                logres.Message = ex.Message;
                logres.Response = false;

                return logres;
            }

        }






    }
}