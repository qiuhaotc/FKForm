
using FKForm.FKFormBase;

namespace FKForm.FKFormControl
{
    public class ControlSame : FKFormBase.FormControlBaseAttribute
    {
        /// <summary>
        /// 待比较控件ID
        /// </summary>
        public string CompareControlID { get; set; }

        public ControlSame(string controlID, string compareControlID, string displayName = "", string custumError = "", bool IsTrim = true, FKFormBase.FormControlPublicValue.FormControlType controlType = FormControlPublicValue.FormControlType.Text)
        {
            this.ControlID = controlID;
            this.CustomErrorStr = custumError;
            this.CompareControlID = compareControlID;
            this.ControlType = controlType;
            if (displayName == "")
            {
                this.DisplayName = controlID;
            }
            else
            {
                this.DisplayName = displayName;
            }

            RenderErrorString();
        }

        /// <summary>
        /// 服务器端无需比较,直接返回true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool CheckValue(string value)
        {
            return true;
        }

        public override string RenderCheckScript()
        {
            string checkSaveStr = "";

            checkSaveStr = "CompareControlID:'" + CompareControlID + "'";
            //checkSaveStr2 = "CompareControlID:'" + ControlID + "'";

            string scriptStr = string.Format(FormControlPublicValue.JavsScriptValidateItemTmp, ControlID, FormControlPublicValue.ValidateType.Compare.ToString(), ErrorStr, checkSaveStr);
            //string scriptStr2 = string.Format(FormControlPublicValue.JavsScriptValidateItemTmp, CompareControlID, FormControlPublicValue.ValidateType.Compare.ToString(), ErrorStr, checkSaveStr2);

            return scriptStr;// +"," + scriptStr2;
        }

        public override void RenderErrorString()
        {
            if (CustomErrorStr == "")
            {
                ErrorStr = string.Format(FormControlPublicValue.GetErrorStringTemp(FormControlPublicValue.ValidateType.Compare), DisplayName);
            }
            else
            {
                ErrorStr = CustomErrorStr;
            }
        }
    }
}