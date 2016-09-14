namespace TestFCForm.CustomAjaxAttribute
{
    /// <summary>
    /// 自定义Method
    /// </summary>
    public class ValidateTestMethod : FKForm.IFKFormBase.IValidateMethod
    {
        public bool IsValid(string value)
        {
            if (value != null && value.Length > 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}