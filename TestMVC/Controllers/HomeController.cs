using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FKForm.FKFormBase;
using FKForm.FKValidateModel;
using TestMVC.CustomAjaxAttribute;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var validate = ValidateHelper.GetValidateItems<ModelTest>();

            ValidateHelper.RenderJavaScriptValidateString(validate, ViewBag);

            return View();
        }

        public JsonResult Test(ModelTest model)
        {
            var validate = ValidateHelper.GetValidateItems<ModelTest>();
            
            ValidateHelper.ValidateItem(model, validate, null, false);

            validate.AddErrorItem(new ErrorItems() {
                ControlID = "Name",
                ErrorStr = "新增自定义错误",
                ValidateType = "custom",
            });

            return Json(validate.GetBackJavaScriptValidateItems());

        }

        public JsonResult TestAjax()
        {
            string controlID = Request["controlID"];

            string value = Request["value"];

            ValidateTestMethod test = new ValidateTestMethod();
            ValidateJsonResult model = new ValidateJsonResult();

            model.ControlID = controlID;

            model.IsValid = test.IsValid(value);

            if (!model.IsValid)
            {
                model.ErrorStr = "请为[简介]输入字数少于10的值";
            }
            
            return Json(model);
        }
    }
}