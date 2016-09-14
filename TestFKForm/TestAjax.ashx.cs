using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using FKForm.FKValidateModel;
using TestFCForm.CustomAjaxAttribute;

namespace TestFCForm
{
    /// <summary>
    /// TestAjax 的摘要说明
    /// </summary>
    public class TestAjax : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            string controlID = context.Request["controlID"];

            if (string.IsNullOrEmpty(controlID))
            {
                return;
            }

            string value = context.Request["value"];

            ValidateTestMethod test = new ValidateTestMethod();
            ValidateJsonResult model = new ValidateJsonResult();

            model.ControlID = controlID;
            
            model.IsValid = test.IsValid(value);

            if (!model.IsValid)
            {
                model.ErrorStr = "请为[简介]输入字数少于10的值";
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}