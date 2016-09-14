using System;
using FKForm.IFKFormBase;

namespace FKForm.FKFormBase
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class FormControlBaseAttribute : Attribute, IFormControl
    {
        #region 公共变量

        /// <summary>
        /// 控件的ID信息
        /// </summary>
        public string ControlID { get; set; }

        /// <summary>
        /// 控件的错误提示信息
        /// </summary>
        public string ErrorStr { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 用户自定义错误信息
        /// </summary>
        public string CustomErrorStr { get; set; }

        /// <summary>
        /// 网页错误提示string
        /// </summary>
        public string ErrorStrForWeb { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public FormControlPublicValue.FormControlType ControlType { get; set; }

        #endregion 公共变量

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckValue(string value);

        /// <summary>
        /// 生成校验用JavaScript
        /// </summary>
        public abstract string RenderCheckScript();

        /// <summary>
        /// 生成错误提示数据
        /// </summary>
        public abstract void RenderErrorString();

        public string GetControlID()
        {
            return ControlID;
        }

        public string GetControlType()
        {
            return ControlType.ToString();
        }

        public string GetErrorStr()
        {
            RenderErrorString();
            return ErrorStr;
        }
    }
}