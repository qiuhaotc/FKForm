using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FKForm.FKFormBase;
using FKForm.IFKFormBase;

namespace TestMVC.CustomAjaxAttribute
{
    public class CustomTestAttr: FKForm.FKFormControl.ControlCustom
    {

        public CustomTestAttr(string controlID, string validateJavaScript, string displayName = "", string custumError = "", FKForm.FKFormBase.FormControlPublicValue.FormControlType controlType = FormControlPublicValue.FormControlType.Text)
        {
            this.ControlID = controlID;
            this.CustomErrorStr = custumError;
            this.ControlType = controlType;
            this.ValidateJavaScript = validateJavaScript;
            if (displayName == "")
            {
                this.DisplayName = controlID;
            }
            else
            {
                this.DisplayName = displayName;
            }
            RenderErrorString();
        }

        public override void SetValidateMethod()
        {
            IValidateMethod method = new ValidateTest2();
            this.ValidateMethod = method;
        }

        public override void RenderErrorString()
        {
            if (CustomErrorStr != "")
            {
                ErrorStr = CustomErrorStr;
            }
            else
            {
                ErrorStr = "自定义错误,长度不能小于10";
            }
        }
    }

    public class ValidateTest2 : IValidateMethod
    {

        public bool IsValid(string value)
        {
            if (value==null||value.Length < 10)
            {
                return false;
            }
            return true;
        }
    }
}