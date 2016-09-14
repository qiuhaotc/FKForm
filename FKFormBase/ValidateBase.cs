using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FKForm.FKValidateModel;
using FKForm.IFKFormBase;

namespace FKForm.FKFormBase
{
    /// <summary>
    /// 数据校验基类
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public class ValidateBase<V> : IValidateBase where V : class
    {
       static object objLock = new object();

        public ValidateBase()
        {
        }

        public ValidateBase(System.Web.HttpContext context)
        {
            this.Context = context;
        }

        static ValidateBase()
        {
            SetValidateInfoStaticList();
        }

        public string ErrorStr
        {
            get;
            set;
        }

        public bool AutoSetValue { get; set; }

        public List<IErrorItems> ErrorItemsList { get; set; }

        public System.Web.HttpContext Context
        {
            get;
            set;
        }

        public V ModelValidate
        {
            get;
            set;
        }

        public List<string> ValueStrList
        {
            get;
            set;
        }

        public static List<FKValidateModel.PropInfoWithValidte> ValidateInfoListStatic
        {
            get;
            set;
        }

        public static string JavaScriptForWeb
        {
            get;
            set;
        }

        public void SetModel(object model, bool autoSetValue)
        {
            this.ModelValidate = model as V;

            this.AutoSetValue = autoSetValue;

            SetValidateValues();
        }

        /// <summary>
        /// 设置数值
        /// </summary>
        public void SetValidateValues()
        {
            if (AutoSetValue)
            {
                if (Context == null)
                {
                    throw new ArgumentNullException("Context");
                }
                else
                {
                    ValueStrList = new List<string>();
                    foreach (var validateItem in ValidateInfoListStatic)
                    {
                        string value = Context.Request[validateItem.PropInfo.Name];

                        string valueStr = GetFormatValueString(value);

                        ValueStrList.Add(valueStr);
                    }
                }
            }
            else
            {
                ValueStrList = new List<string>();
                foreach (var validateItem in ValidateInfoListStatic)
                {
                    object value = validateItem.PropInfo.GetValue(ModelValidate);

                    string valueStr = GetFormatValueString(value == null ? null : value.ToString());

                    ValueStrList.Add(valueStr);
                }
            }
        }

        public virtual string GetFormatValueString(string value)
        {
            if (value != null)
            {
                return value.Trim();
            }
            else
            {
                return value;
            }
        }

        public bool ValidForm(bool autoSetValue = true)
        {
            ErrorItemsList = new List<IErrorItems>();

            bool valid = true;

            foreach (var item in ValidateInfoListStatic)
            {
                foreach (var itemCheck in item.ControlAttrList)
                {
                    ErrorItems errorItem = new ErrorItems();

                    if (!itemCheck.CheckValue(ValueStrList[item.Index]))
                    {
                        errorItem.ControlID = itemCheck.GetControlID();
                        errorItem.ErrorStr = itemCheck.GetErrorStr();
                        errorItem.ValidateType = itemCheck.GetControlType();
                        ErrorItemsList.Add(errorItem);
                        valid = false;
                    }
                }
            }

            if (valid == true && autoSetValue)
            {
                SetModelValues(ModelValidate, ValidateInfoListStatic);
            }

            return valid;
        }

        /// <summary>
        /// 赋值模型
        /// </summary>
        /// <param name="ModelValidate"></param>
        /// <param name="validateInfoList"></param>
        private void SetModelValues(V ModelValidate, List<FKValidateModel.PropInfoWithValidte> validateInfoList)
        {
            foreach (var item in validateInfoList)
            {
                string valueThis = ValueStrList[item.Index];
                if (!string.IsNullOrEmpty(valueThis))
                {
                    object value = ChangeTypeHelper.ChangeType(valueThis, item.PropInfo.PropertyType);
                    item.PropInfo.SetValue(ModelValidate, value);
                }
            }
        }

        /// <summary>
        /// 生成JS校验代码
        /// </summary>
        /// <returns></returns>
        public string RenderJavaScriptValidateString()
        {
            if (JavaScriptForWeb == null)
            {
                StringBuilder sb = new StringBuilder(ValidateInfoListStatic.Sum(u => u.ControlAttrList.Count) * 20);

                foreach (var item in ValidateInfoListStatic)
                {
                    StringBuilder sbOne = new StringBuilder(item.ControlAttrList.Count * 20);
                    sbOne.Append("[");
                    foreach (var itemCheck in item.ControlAttrList)
                    {
                        if (sbOne.Length == 1)
                        {
                            sbOne.Append(itemCheck.RenderCheckScript());
                        }
                        else
                        {
                            sbOne.Append("," + itemCheck.RenderCheckScript());
                        }
                    }
                    sbOne.Append("]");

                    if (sb.Length == 0)
                    {
                        sb.Append(sbOne.ToString());
                    }
                    else
                    {
                        sb.Append("," + sbOne.ToString());
                    }
                }

                JavaScriptForWeb = string.Format(FormControlPublicValue.JavaScriptValidateObjTmp, sb.ToString());
            }
            return JavaScriptForWeb;
        }

        /// <summary>
        /// 设置静态待校验列表
        /// </summary>
        private static void SetValidateInfoStaticList()
        {
            if (ValidateInfoListStatic == null)
            {
                lock (objLock)
                {
                    if (ValidateInfoListStatic == null)
                    {
                        int index = 0;

                        ValidateInfoListStatic = new List<FKValidateModel.PropInfoWithValidte>();

                        Type t = typeof(V);

                        PropertyInfo[] propList = t.GetProperties();

                        foreach (var prop in propList)
                        {
                            List<IFormControl> iAttributeList = new List<IFormControl>();
                            object[] attrs = prop.GetCustomAttributes(typeof(IFormControl), true);

                            foreach (var attr in attrs)
                            {
                                IFormControl control = attr as IFormControl;
                                iAttributeList.Add(control);
                            }

                            FKValidateModel.PropInfoWithValidte items = new FKValidateModel.PropInfoWithValidte();
                            items.ControlAttrList = iAttributeList;
                            items.Index = index;
                            items.PropInfo = prop;
                            ValidateInfoListStatic.Add(items);
                            index++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 生成后台错误提示
        /// </summary>
        /// <returns></returns>
        public string RenderBackJavaScriptValidateString()
        {
            string strJsonStr=RenderJsonItem();
            return string.Format(FormControlPublicValue.JavaScriptServerValidateErrorArray, strJsonStr);
        }

        /// <summary>
        /// 生成Ajax校验后Script
        /// </summary>
        /// <returns></returns>
        public string RenderBackJavaScriptAjaxValidateString()
        {
            string strJsonStr = RenderJsonItem();
            return string.Format(FormControlPublicValue.JavaScriptServerValidateAjaxScript, strJsonStr);
        }

        public void SetContext(System.Web.HttpContext context)
        {
            this.Context = context;
        }

        private string RenderJsonItem()
        {
            StringBuilder sb = new StringBuilder(ErrorItemsList.Count * 20);

            sb.Append("[");

            foreach (var errorItem in ErrorItemsList)
            {
                if (sb.Length > 1)
                {
                    sb.Append(",{\"ControlID\":\"" + errorItem.ControlID + "\",\"ValidateType\":\"" + errorItem.ValidateType + "\",\"ErrorStr\":\"" + errorItem.ErrorStr + "\"}");
                }
                else
                {
                    sb.Append("{\"ControlID\":\"" + errorItem.ControlID + "\",\"ValidateType\":\"" + errorItem.ValidateType + "\",\"ErrorStr\":\"" + errorItem.ErrorStr + "\"}");
                }
            }

            sb.Append("]");

            return sb.ToString();
        }

        public void AddErrorItem(IErrorItems errorItem)
        {
            ErrorItemsList.Add(errorItem);
        }

        public void RemoveErrorItem(IErrorItems errorItem)
        {
            ErrorItemsList.Remove(errorItem);
        }
    }
}