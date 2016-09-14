

using FKForm.FKFormBase;

namespace FKForm.FKFormControl
{
    public abstract class ControlCustom : FKFormBase.FormControlBaseAttribute
    {
        public IFKFormBase.IValidateMethod ValidateMethod { get; set; }

        public string ValidateJavaScript { get; set; }

        public ControlCustom()
        {
            SetValidateMethod();
        }

        public abstract void SetValidateMethod();

        public override bool CheckValue(string value)
        {
            return ValidateMethod.IsValid(value);
        }

        public override string RenderCheckScript()
        {
            string customScriptStr = "";

            customScriptStr = "ValidateJavaScript:'" + ValidateJavaScript + "'";

            string scriptStr = string.Format(FormControlPublicValue.JavsScriptValidateItemTmp, ControlID, FormControlPublicValue.ValidateType.Other.ToString(), ErrorStr, customScriptStr);

            return scriptStr;
        }
    }
}