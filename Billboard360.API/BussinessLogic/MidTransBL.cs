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
    public class MidTransBL
    {
        protected readonly DatabaseContext DbContext;
        AppSettings AppSettings { get; }


        public MidTransBL(DatabaseContext dbContext, AppSettings appSettings)
        {
            DbContext = dbContext;
            AppSettings = appSettings;
        }

        public MidTransTransactionResponseModel TransactionMidTrans(MidTransChargeInputModel data)
        {
            //MidTransRequestModel reqModel = new MidTransRequestModel();
            //reqModel = data;



            RestClient rest = new RestClient(AppSettings.MidtransConfig.URL);

            RestRequest req = new RestRequest("transactions", Method.POST);            

            req.AddHeader("Accept", "application/json");

            string auth = AppSettings.MidtransConfig.ServerKey.ToString();

            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Basic " + auth.Base64Encode());

            string jsonObject = JsonConvert.SerializeObject(data);

            req.AddParameter("application/json", jsonObject, ParameterType.RequestBody);

            var res = rest.Execute(req);

            return JsonConvert.DeserializeObject<MidTransTransactionResponseModel>(res.Content);
        }


        
        public UpdatePaymentStatusResponseModel SaveMidtransLog(UpdatePaymentStatusInputModel data, ModeMidTransEnum mode)
        {
            MidTransLog temp = new MidTransLog();

            PurchaseRepository purchaseRepo = new PurchaseRepository(DbContext);
            var purchaseInfo = purchaseRepo.GetPaymentByInvoiceNumber(data.InvoiceNumber).FirstOrDefault();

            temp.CreateByUserID = data.UserID;
            temp.CreateDate = DateTime.Now;
            temp.MidTransStatus = (int)data.Status;
            temp.PaymentID = purchaseInfo.ID;
            temp.BankName = data.BankName;
            temp.VANumber = data.VANumber;
            temp.MidTransTransactionType = data.MidTransPaymentType;

            MidtransLogRepository repo = new MidtransLogRepository(DbContext);

            var res = repo.Insert(temp);

            UpdatePaymentStatusOutputModel output = new UpdatePaymentStatusOutputModel();

            output.Message = res.Message;

            UpdatePaymentStatusResponseModel response = new UpdatePaymentStatusResponseModel();

            response.Message = res.Message;
            response.Response = true;
            

            return response;

        }

        public UpdatePaymentStatusResponseModel SaveMidtransLog(NotificationHandlingModel data, ModeMidTransEnum mode)
        {
            MidTransLog temp = new MidTransLog();


            temp.CreateByUserID = Guid.NewGuid();
            temp.CreateDate = DateTime.Now;
            temp.MidTransStatus = 3;
            temp.PaymentID = Guid.NewGuid();

            var iString = data.transaction_time;
            DateTime oDate = DateTime.ParseExact(iString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            temp.Transaction_Time = oDate;
            temp.Transaction_Status = data.transaction_status;
            temp.Transaction_ID = data.transaction_id;
            temp.Status_Message = data.status_message;
            temp.Status_Code = data.status_code;
            temp.Signature_Key = data.signature_key;
            temp.Payment_Type = data.payment_type;
            temp.Order_ID = data.order_id;
            temp.Merchant_ID = data.merchant_id;
            temp.Gross_Amount = data.gross_amount;
            temp.Currency = data.currency;
            temp.Approval_Code = data.approval_code;
            temp.ModeTransaction = (int)mode;

            MidtransLogRepository repo = new MidtransLogRepository(DbContext);

            var res = repo.Insert(temp);

            UpdatePaymentStatusOutputModel output = new UpdatePaymentStatusOutputModel();

            output.Message = res.Message;

            UpdatePaymentStatusResponseModel response = new UpdatePaymentStatusResponseModel();

            response.Message = res.Message;
            response.Response = true;


            return response;

        }



    }
}
