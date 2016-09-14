using System;
using FKForm.IFKFormBase;

namespace FKForm.FKFormBase
{
    public class ValidateHelper
    {
        /// <summary>
        /// 获取校验类
        /// </summary>
        /// <typeparam name="V">待校验模型</typeparam>
        /// <returns></returns>
        public static IValidateBase GetValidateItems<V>() where V : class
        {
            ValidateBase<V> valiBase = new ValidateBase<V>();
            return valiBase;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="model">待赋值校验类</param>
        /// <param name="valiBase"></param>
        /// <param name="context"></param>
        /// <param name="autoSetValue">自动赋值</param>
        /// <returns></returns>
        public static bool ValidateItem(object model, IValidateBase valiBase, System.Web.HttpRequest request, bool autoSetValue = true)
        {
            valiBase.SetContext(request);
            valiBase.SetModel(model, autoSetValue);
            return valiBase.ValidForm(autoSetValue);
        }

        /// <summary>
        /// 生成后台校验完成的错误Javascript代码
        /// </summary>
        /// <param name="valiBase"></param>
        /// <param name="data"></param>
        public static void RenderBackJavaScriptAjaxValidateString(IValidateBase valiBase, dynamic data)
        {
            data.BackValidateScript = valiBase.RenderJavaScriptValidateString();
        }

        /// <summary>
        /// 生成前端校验Scripts 保存在 ValidateScript 中
        /// </summary>
        /// <param name="valiBase"></param>
        /// <param name="data"></param>
        public static void RenderJavaScriptValidateString(IValidateBase valiBase,dynamic data)
        {
            data.ValidateScript = valiBase.RenderJavaScriptValidateString();
        }

    }
}