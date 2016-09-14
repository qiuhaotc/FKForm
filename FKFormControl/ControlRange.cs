using System;
using System.Text.RegularExpressions;
using FKForm.FKFormBase;

namespace FKForm.FKFormControl
{
    public class ControlRange : FKFormBase.FormControlBaseAttribute
    {
        public double? MaxValue { get; set; }
        public double? MinValue { get; set; }
        public bool? IsTrim { get; set; }
        public FKFormBase.FormControlPublicValue.RangeType RangeType { get; set; }
        public string RegexStr { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controlID">待校验控件ID</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="custumError">自定义错误信息</param>
        /// <param name="IsTrim">是否去除首尾空白字符</param>
        public ControlRange(string controlID, double maxValue, double minValue = 0.0, string displayName = "", string custumError = "", FKFormBase.FormControlPublicValue.RangeType rangeType = FKFormBase.FormControlPublicValue.RangeType.Int, FKFormBase.FormControlPublicValue.FormControlType controlType = FormControlPublicValue.FormControlType.Text, bool IsTrim = true)
        {
            this.ControlID = controlID;
            this.CustomErrorStr = custumError;
            this.MaxValue = maxValue;
            this.MinValue = minValue;
            this.IsTrim = IsTrim;
            this.RangeType = rangeType;
            this.ControlType = controlType;
            if (displayName == "")
            {
                this.DisplayName = controlID;
            }
            else
            {
                this.DisplayName = displayName;
            }
            RegexStr = FormControlPublicValue.GetRangeRegex(RangeType).Replace("'", "\\'");
            RenderErrorString();
        }

        public override bool CheckValue(string value)
        {
            if (value == null)/*&& (MaxValue < 0 || MinValue > 0)*/
            {
                return false;
            }
            else
            {
                Regex reg = new Regex(RegexStr);

                if (string.IsNullOrEmpty(value) || !reg.IsMatch(value))
                {
                    return false;
                }
                else
                {
                    if (MaxValue.HasValue && MinValue.HasValue)
                    {
                        double nowValue;

                        if (!double.TryParse(value, out nowValue))
                        {
                            return false;
                        }
                        else
                        {
                            if (nowValue > MaxValue || nowValue < MinValue)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public override string RenderCheckScript()
        {
            string rangerStr = "";

            if (MaxValue.HasValue && MinValue.HasValue)
            {
                rangerStr = "MaxValue:" + MaxValue + ",MinValue:" + MinValue + ",RegexStr:'" + RegexStr + "'";
            }
            else
            {
                throw new ArgumentNullException();
            }

            string scriptStr = string.Format(FormControlPublicValue.JavsScriptValidateItemTmp, ControlID, FormControlPublicValue.ValidateType.Range.ToString(), ErrorStr, rangerStr);

            return scriptStr;
        }

        public override void RenderErrorString()
        {
            if (CustomErrorStr == "")
            {
                ErrorStr = string.Format(FormControlPublicValue.GetErrorStringTemp(FormControlPublicValue.ValidateType.Range), DisplayName, "介于" + MinValue + "和" + MaxValue + "之间", (RangeType == FormControlPublicValue.RangeType.Int ? "整数" : ""));
            }
            else
            {
                ErrorStr = CustomErrorStr;
            }
        }
    }
}