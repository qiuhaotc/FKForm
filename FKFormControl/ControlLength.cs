using System;
using FKForm.FKFormBase;

namespace FKForm.FKFormControl
{
    public class ControlLength : FKFormBase.FormControlBaseAttribute
    {
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public bool? IsTrim { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controlID">待校验控件ID</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="custumError">自定义错误信息</param>
        /// <param name="IsTrim">是否去除首尾空白字符</param>
        public ControlLength(string controlID, int maxLength, int minLength = 0, string displayName = "", string custumError = "", bool IsTrim = true, FKFormBase.FormControlPublicValue.FormControlType controlType = FormControlPublicValue.FormControlType.Text)
        {
            this.ControlID = controlID;
            this.CustomErrorStr = custumError;
            this.MaxLength = maxLength;
            this.MinLength = minLength;
            this.IsTrim = IsTrim;
            this.ControlType = controlType;
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
                if (MinLength == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (MaxLength.HasValue && MinLength.HasValue)
                {
                    if (value.Length > MaxLength || value.Length < MinLength)
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
                if (MinLength == 0)
                {
                    ErrorStr = string.Format(FormControlPublicValue.GetErrorStringTemp(FormControlPublicValue.ValidateType.Length), DisplayName, "不大于" + MaxLength);
                }
                else
                {
                    ErrorStr = string.Format(FormControlPublicValue.GetErrorStringTemp(FormControlPublicValue.ValidateType.Length), DisplayName, "介于" + MinLength + "和" + MaxLength + "之间");
                }
            }
            else
            {
                ErrorStr = CustomErrorStr;
            }
        }

        public override string RenderCheckScript()
        {
            string lengthStr = "";

            if (MaxLength.HasValue && MinLength.HasValue)
            {
                lengthStr = "MaxLength:" + MaxLength + ",MinLength:" + MinLength;
            }
            else
            {
                throw new ArgumentNullException();
            }

            string scriptStr = string.Format(FormControlPublicValue.JavsScriptValidateItemTmp, ControlID, FormControlPublicValue.ValidateType.Length.ToString(), ErrorStr, lengthStr);

            return scriptStr;
        }
    }
}