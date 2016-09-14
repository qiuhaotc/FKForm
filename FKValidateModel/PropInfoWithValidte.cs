using System.Collections.Generic;
using System.Reflection;

namespace FKForm.FKValidateModel
{
    public class PropInfoWithValidte
    {
        public PropertyInfo PropInfo { get; set; }
        public int Index { get; set; }
        public List<IFKFormBase.IFormControl> ControlAttrList { get; set; }
    }
}