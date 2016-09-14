namespace FKForm.IFKFormBase
{
    internal interface IRenderScript
    {
        /// <summary>
        /// 生成的校验JS代码
        /// </summary>
        string ScriptString { get; set; }

        /// <summary>
        /// 设置校验JS代码
        /// </summary>
        void SetScript();

        /// <summary>
        /// 获取校验JS代码
        /// </summary>
        string GetScript();
    }
}