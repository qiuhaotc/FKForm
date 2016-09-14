using System;
using System.Text.RegularExpressions;
using FKForm.FKFormBase;

namespace FKForm.FKFormControl
{
    public class ControlRegex : FKFormBase.FormControlBaseAttribute
    {
        public Regex FormRegex { get; set; }
        public bool? IsTrim { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controlID">待校验控件ID</param>
        /// <param name="controlRegex">校验正则表达式</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="custumError">自定义错误信息</param>
        /// <param name="IsTrim">是否去除首尾空白字符</param>
        public ControlRegex(string controlID, string controlRegex, string displayName = "", string custumError = "", bool IsTrim = true, FKFormBase.FormControlPublicValue.FormControlType controlType = FormControlPublicValue.FormControlType.Text)
        {
            this.ControlID = controlID;
            this.CustomErrorStr = custumError;
            this.FormRegex = new Regex(controlRegex);
            this.ControlType = ControlType;
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

        public override bool CheckValue(string value)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                if (FormRegex != null)
                {
                    if (!FormRegex.IsMatch(value))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public override void RenderErrorString()
        {
            if (CustomErrorStr == "")
            {
                ErrorStr = string.Format(FormControlPublicValue.GetErrorStringTemp(FKFormBase.FormControlPublicValue.ValidateType.Regex), DisplayName);
            }
            else
            {
                ErrorStr = CustomErrorStr;
            }
        }

        public override string RenderCheckScript()
        {
            string regexStr = "";

            if (FormRegex != null)
            {
                regexStr += "RegexStr:'" + FormRegex.ToString().Replace("'", "\\'") + "'";
            }
            else
            {
                throw new ArgumentNullException();
            }

            string scriptStr = string.Format(FormControlPublicValue.JavsScriptValidateItemTmp, ControlID, FormControlPublicValue.ValidateType.Regex.ToString(), ErrorStr, regexStr);

            return scriptStr;
        }
    }
}