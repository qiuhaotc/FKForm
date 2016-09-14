namespace FKForm.IFKFormBase
{
    public interface IFormControl
    {
        #region 校验数据

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <returns></returns>
        bool CheckValue(string value);

        #endregion 校验数据

        #region 获取控件ID

        /// <summary>
        /// 获取控件ID
        /// </summary>
        /// <returns></returns>
        string GetControlID();

        #endregion 获取控件ID

        #region 获取错误提示信息

        /// <summary>
        /// 获取错误提示信息
        /// </summary>
        /// <returns></returns>
        string GetErrorStr();

        #endregion 获取错误提示信息


        #region 获取校验类型
        /// <summary>
        /// 获取校验类型
        /// </summary>
        /// <returns></returns>
        string GetControlType(); 
        #endregion

        #region 生成校验JavaScript

        /// <summary>
        /// 生成校验JavaScript
        /// </summary>
        /// <returns></returns>
        string RenderCheckScript();

        /// <summary>
        /// 生成错误提示
        /// </summary>
        /// <returns></returns>
        void RenderErrorString();

        #endregion 生成校验JavaScript
    }
}