namespace FKForm.IFKFormBase
{
    /// <summary>
    /// 自动生成Form表单接口
    /// </summary>
    public interface IValidateModelInfo
    {
        /// <summary>
        /// 获取校验方法名称
        /// </summary>
        /// <returns></returns>
        string GetValidateMethodName();

        /// <summary>
        /// 获取发送到的地址
        /// </summary>
        /// <returns></returns>
        string GetAction();

        /// <summary>
        /// 获取post/get方法
        /// </summary>
        /// <returns></returns>
        string GetMethod();
    }
}