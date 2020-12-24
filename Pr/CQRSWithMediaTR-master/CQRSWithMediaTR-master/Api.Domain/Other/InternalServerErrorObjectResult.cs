
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using ContentResult = Microsoft.AspNetCore.Mvc.ContentResult;

namespace Customer.Domain.Other
{
    public class InternalServerErrorObjectResult : JsonResult
    {
        public int StatusCode { get; set; }
        public dynamic Message { get; set; }
        public string Desciption { get; set; }
        public InternalServerErrorObjectResult() {
            StatusCode = StatusCodes.Status200OK;
            Message = "";
        }
        public InternalServerErrorObjectResult(int value, string message) : base()
        {
            this.StatusCode = value;
            this.Message = message;
        }
        public InternalServerErrorObjectResult(int value, string message, string desciption) : base()
        {
            this.StatusCode = value;
            this.Message = message;
            this.Desciption = desciption;
        }
        public (ContentResult content, int responce) CreateResult()
        {
            dynamic resultObject = new ExpandoObject();
            resultObject.code= StatusCode;
            resultObject.responce = Message;

            if (Desciption != "")
            {
                resultObject.desciption= Desciption;
            }
            try
            {
                if (resultObject.desciption == null)
                {
                    (resultObject as IDictionary<string, object>).Remove(nameof(resultObject.desciption));
                }
            }
            catch (Exception exception)
            {
                //throw new Exception(exception.Message);
            }
            string json = JsonConvert.SerializeObject(resultObject);
            return (content: new ContentResult 
            { 
               Content = json, ContentType = "application/json" 
            }, responce: StatusCode);
        }

    }
}
