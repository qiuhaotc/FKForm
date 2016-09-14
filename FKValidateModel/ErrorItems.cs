using FKForm.IFKFormBase;

namespace FKForm.FKValidateModel
{
    public class ErrorItems: IErrorItems
    {
        public string ControlID { get; set; }
        public string ValidateType { get; set; }
        public string ErrorStr { get; set; }
    }
}