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
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        protected readonly DatabaseContext DbContext;


        public BankController(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        [Route("AddMasterBank")]
        [HttpPost]
        public ActionResult<BankResponseModel> AddMasterBank([FromBody] AddBankInputModel data)
        {
            BankResponseModel response = new BankResponseModel();
            try
            {
                BankBL posBL = new BankBL(DbContext);
                return posBL.Save(data);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Response = false;
                return response;
            }


        }

        [Route("EditMasterBank")]
        [HttpPost]
        public ActionResult<BankResponseModel> EditMasterBank([FromBody] EditBankInputModel data)
        {
            BankResponseModel response = new BankResponseModel();
            try
            {
                BankBL posBL = new BankBL(DbContext);
                return posBL.Edit(data);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Response = false;
                return response;
            }


        }

        [Route("GetListBank")]
        [HttpPost]
        public ActionResult<ListBankResponseModel> GetListBank([FromBody] FilterBank filter)
        {
            ListBankResponseModel res = new ListBankResponseModel();
            try
            {
                BankBL posBL = new BankBL(DbContext);
                var data = posBL.GetListBank(filter);

                res.data = data;

                res.Message = "Success";
                res.Response = true;

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }


        }

        [Route("GetListBankUserAccount")]
        [HttpPost]
        public ActionResult<ListBankUserAccountResponseModel> GetListBankUserAccount([FromBody] FilterBank filter)
        {
            ListBankUserAccountResponseModel res = new ListBankUserAccountResponseModel();
            try
            {
                BankBL posBL = new BankBL(DbContext);
                var data = posBL.GetListUserBankAccount(filter);

                res.Data =data.data;
                res.TotalData = data.TotalData;
                res.TotalPages = data.TotalPage;

                res.Message = "Success";
                res.Response = true;

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }


        }

        [Route("GetBank")]
        [HttpGet]
        public ActionResult<InfoBankResponseModel> GetBank(Guid BankID)
        {
            InfoBankResponseModel res = new InfoBankResponseModel();
            try
            {
                BankBL posBL = new BankBL(DbContext);
                var data = posBL.GetDetailBank(BankID);

                res.data = data;

                res.Message = "Success";
                res.Response = true;

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;
                return res;
            }


        }

        [Route("ChangeStatusBank")]
        [HttpPost]
        public ActionResult<InActiveBankResponseModel> ChangeStatusBank([FromBody] InActiveBankInputModel data)
        {
            InActiveBankResponseModel response = new InActiveBankResponseModel();
            try
            {
                BankBL bl = new BankBL(DbContext);
                var res = bl.ChangeStatusBank(data);

                response.data = res;
                response.Message = "Success Change status";
                response.Response = true;

                return response;                
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Response = false;
                return response;
            }


        }

        [Route("DeleteBank")]
        [HttpPost]
        public ActionResult<DeleteBankResponseModel> DeleteBank([FromBody] DeleteBankInputModel data)
        {
            DeleteBankResponseModel response = new DeleteBankResponseModel();
            try
            {
                BankBL bl = new BankBL(DbContext);
                var res = bl.DeleteBank(data);

                response.data = res;
                response.Message = "Success Change status";
                response.Response = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Response = false;
                return response;
            }


        }



    }
}