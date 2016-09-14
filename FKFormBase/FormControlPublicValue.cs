namespace FKForm.FKFormBase
{
    /// <summary>
    /// 公共数据
    /// </summary>
    public class FormControlPublicValue
    {
        public static string JavaScriptValidateObjTmp = "validateItemArray=[{0}];";
        public static string JavaScriptServerValidateErrorArray = "serverValidateErrorArray={0};$(function(){{showServerAlertInfo();}});";
        public static string JavaScriptServerValidateAjaxScript = "{0}";
        public static string JavsScriptValidateItemTmp = "{{ControlID:'{0}',ValidateType:'{1}',ErrorStr:'{2}',{3}}}";

        public enum FormControlType
        {
            Text,
            Check,
            Select,
            File
        }

        public enum ValidateType
        {
            Regex,
            Length,
            Range,
            Other,
            Ajax,
            Compare
        }

        /// <summary>
        /// 获取错误提示字符串
        /// </summary>
        /// <param name="errorType"></param>
        /// <returns></returns>
        public static string GetErrorStringTemp(ValidateType errorType)
        {
            switch (errorType)
            {
                case ValidateType.Regex:
                    return "请为[{0}]输入正确的值";

                case ValidateType.Length:
                    return "请为[{0}]输入长度{1}的值";

                case ValidateType.Range:
                    return "请为[{0}]输入大小{1}的{2}值";

                case ValidateType.Ajax:
                    return "请为[{0}]输入正确的值";

                case ValidateType.Compare:
                    return "请为[{0}]输入相同的值";

                default:
                    return "错误的数值";
            }
        }

        /// <summary>
        /// 范围校验类型
        /// </summary>
        public enum RangeType
        {
            Int,
            Number
        }

        /// <summary>
        /// 获取数字正则校验字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetRangeRegex(RangeType type)
        {
            switch (type)
            {
                case RangeType.Int:
                    return @"^-?(0|[1-9][0-9]*)$";

                case RangeType.Number:
                default:
                    return @"^[+-]?([0-9]*\.?[0-9]+|[0-9]+\.?[0-9]*)$";/*([eE][+-]?[0-9]+)?*/
            }
        }
    }
}