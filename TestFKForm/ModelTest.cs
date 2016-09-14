using FKForm.FKFormBase;
using FKForm.FKFormControl;

namespace TestFCForm
{
    public class ModelTest
    {

        [ControlRegex("Name", @"^[\u4E00-\u9FA5]+$", "姓名", "请为[姓名]输入正确的中文")]
        [ControlLength("Name", 10, 1, "姓名")]
        public string Name { get; set; }

        [ControlRegex("Ages", @"^[0-9]{1,3}$", "年龄")]
        [ControlLength("Ages", 3, 1, "年龄")]
        public int Ages { get; set; }

        
        [ControlRange("Money", 10000, 0, "金钱")]
        public int Money { get; set; }

        [CustomAjaxAttribute.TestAjaxAttrLength("Memo", "/TestAjax.ashx", "简介", "请为[简介]输入字数少于10的值")]
        public string Memo { get; set; }

        [ControlLength("Password", 10, 6, "密码")]
        [ControlSame("Password","PasswordRepeat","密码","两次密码输入不一致")]
        public string Password { get; set; }

        [ControlLength("PasswordRepeat", 10, 6, "密码")]
        [ControlSame("PasswordRepeat", "Password", "密码", "两次密码输入不一致")]
        public string PasswordRepeat { get; set; }

        [ControlRange("Area", 10, 1, "地区", "请为[地区]选择正确的值")]
        public int Area { get; set; }

        [CustomAjaxAttribute.CustomTestAttr("PhoneNum", "customValidateMethod", "电话", "请为[电话]长度提供长度大于10的值", FormControlPublicValue.FormControlType.Select)]
        public string PhoneNum { get; set; }
    }
}