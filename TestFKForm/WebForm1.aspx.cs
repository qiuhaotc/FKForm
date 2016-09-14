using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestFCForm
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string ValidateScript { get; set; }

        public string ServerValidateScript { get; set; }

        FKForm.IFKFormBase.IValidateBase validateBase = FKForm.FKFormBase.ValidateHelper.GetValidateItems<ModelTest>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Name"] != null)
            {
                if (Request["Ajax"] == "true")
                {
                    ModelTest test = new ModelTest();
                    FKForm.FKFormBase.ValidateHelper.ValidateItem(test, validateBase, Context.Request, true);
                    Response.Write(validateBase.RenderBackJavaScriptAjaxValidateString());
                    Response.End();
                }
                else
                {
                    CheckValue();
                }
            }
            
        }

        protected void CheckValue()
        {
            ModelTest test = new ModelTest( );

            if (FKForm.FKFormBase.ValidateHelper.ValidateItem(test, validateBase, Context.Request, true))
            {
                Response.Write("正确");
                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(test));
            }
            else
            {
                ServerValidateScript = validateBase.RenderBackJavaScriptValidateString();
            }
        }

        protected string ValidateFor()
        {
            return validateBase.RenderJavaScriptValidateString();
        }
    }
}