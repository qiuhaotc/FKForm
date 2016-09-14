using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKForm.IFKFormBase
{
   public interface IErrorItems
    {
        string ControlID { get; set; }
        string ValidateType { get; set; }
        string ErrorStr { get; set; }
    }
}
