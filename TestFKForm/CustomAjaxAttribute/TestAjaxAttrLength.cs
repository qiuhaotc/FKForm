
using FKForm.FKFormBase;
using FKForm.IFKFormBase;

namespace TestFCForm.CustomAjaxAttribute
{
    public class TestAjaxAttrLength : FKForm.FKFormControl.ControlAjax
    {
        public TestAjaxAttrLength(string controlID, string validateURL, string displayName = "", string custumError = "", bool IsTrim = true, FormControlPublicValue.FormControlType controlType = FormControlPublicValue.FormControlType.Text)
        {
            IValidateMethod validateMethod = new ValidateTestMethod();

            SetValidateMethod(controlID, validateURL, validateMethod, displayName, custumError, IsTrim, controlType);
        }
    }
}