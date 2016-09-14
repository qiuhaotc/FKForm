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
        public static bool ValidateItem(object model, IValidateBase valiBase, System.Web.HttpContext context, bool autoSetValue = true)
        {
            valiBase.SetContext(context);
            valiBase.SetModel(model, autoSetValue);
            return valiBase.ValidForm(autoSetValue);
        }
    }
}