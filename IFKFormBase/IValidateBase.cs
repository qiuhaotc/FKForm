namespace FKForm.IFKFormBase
{
    /// <summary>
    /// 数据校验接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidateBase
    {
        /// <summary>
        /// 校验后的错误信息提示
        /// </summary>
        string ErrorStr { get; set; }

        /// <summary>
        /// 请求上下文
        /// </summary>
        System.Web.HttpContext Context { get; set; }

        /// <summary>
        /// 设置待校验模型
        /// </summary>
        /// <param name="mode">模型</param>
        /// <param name="autoSetValuel">自动赋值(已赋值模型可以设置为false)</param>
        void SetModel(object mode, bool autoSetValuel);

        /// <summary>
        /// 设置待校验数据值
        /// </summary>
        void SetValidateValues();

        /// <summary>
        /// 获取格式化后的值
        /// </summary>
        /// <param name="value">待格式化值</param>
        /// <returns></returns>
        string GetFormatValueString(string value);

        /// <summary>
        /// 校验表单
        /// </summary>
        /// <param name="autoSetValue">自动设置模型值</param>
        /// <returns></returns>
        bool ValidForm(bool autoSetValue = true);

        /// <summary>
        /// 生成前台校验JavaScript
        /// </summary>
        /// <returns></returns>
        string RenderJavaScriptValidateString();

        /// <summary>
        /// 生成前台校验被屏蔽后的错误提示JavaScript
        /// </summary>
        /// <returns></returns>
        string RenderBackJavaScriptValidateString();

        /// <summary>
        ///  生成前台校验被屏蔽后的Ajax错误提示JavaScript
        /// </summary>
        /// <returns></returns>
        string RenderBackJavaScriptAjaxValidateString();

        /// <summary>
        /// 设置当前的数据上下文
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        void SetContext(System.Web.HttpContext context);

        /// <summary>
        /// 增加自定义错误项
        /// </summary>
        /// <param name="errorItem"></param>
        void AddErrorItem(IErrorItems errorItem);

        /// <summary>
        /// 去除自定义错误项
        /// </summary>
        /// <param name="errorItem"></param>
        void RemoveErrorItem(IErrorItems errorItem);
    }
}