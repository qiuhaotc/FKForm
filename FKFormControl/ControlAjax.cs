

using FKForm.FKFormBase;

namespace FKForm.FKFormControl
{
    /// <summary>
    /// 抽象Ajax校验方法 用户自定义校验方法继承改类就可以了 为ValidateMethod提供自定义的校验方法
    /// </summary>
    public abstract class ControlAjax : FKFormBase.FormControlBaseAttribute
    {
        /// <summary>
        /// 校验地址
        /// </summary>
        public string ValidateURL { get; set; }

        /// <summary>
        /// 校验方法 和 校验地址里面校验方法保持一致
        /// </summary>
        public IFKFormBase.IValidateMethod ValidateMethod { get; set; }

        public override bool CheckValue(string value)
        {
            return ValidateMethod.IsValid(value);
        }

        /// <summary>
        /// 设置校验方法
        /// </summary>
        public virtual void SetValidateMethod(string controlID, string validateURL, IFKFormBase.IValidateMethod validateMethod, string displayName = "", string custumError = "", bool IsTrim = true, FKFormBase.FormControlPublicValue.FormControlType controlType = FormControlPublicValue.FormControlType.Text)
        {
            this.ControlID = controlID;
            this.ControlType = controlType;
            this.CustomErrorStr = custumError;
            this.ValidateURL = validateURL;
            this.ValidateMethod = validateMethod;
            this.DisplayName = displayName;
            RenderErrorString();
        }

        public override string RenderCheckScript()
        {
            string ajaxStr = "";

            ajaxStr = "ValidateURL:'" + ValidateURL + "'";

            string scriptStr = string.Format(FormControlPublicValue.JavsScriptValidateItemTmp, ControlID, FormControlPublicValue.ValidateType.Ajax.ToString(), ErrorStr, ajaxStr);

            return scriptStr;
        }

        public override void RenderErrorString()
        {
            if (CustomErrorStr == "")
            {
                ErrorStr = string.Format(FormControlPublicValue.GetErrorStringTemp(FormControlPublicValue.ValidateType.Ajax), DisplayName);
            }
            else
            {
                ErrorStr = CustomErrorStr;
            }
        }
    }
}