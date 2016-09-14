using System;

namespace FKForm.FKFormControl
{
    /// <summary>
    /// 下拉框校验
    /// </summary>
    public class ControlSelect : FKFormBase.FormControlBaseAttribute
    {
        public override bool CheckValue(string value)
        {
            throw new NotImplementedException();
        }

        public override string RenderCheckScript()
        {
            throw new NotImplementedException();
        }

        public override void RenderErrorString()
        {
            throw new NotImplementedException();
        }
    }
}