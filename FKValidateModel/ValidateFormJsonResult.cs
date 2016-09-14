using System.Collections.Generic;

namespace FKForm.FKValidateModel
{
    public class ValidateFormJsonResult
    {
        public bool IsValid { get; set; }
        public List<ValidateJsonResult> ValidateResultList { get; set; }
        public string NextURL { get; set; }
    }
}