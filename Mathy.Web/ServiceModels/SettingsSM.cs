using Mathy.Planning;
using Mathy.Web.Models;
using System.Collections.Generic;

namespace Mathy.Web.ServiceModels
{
    public class SettingsSM
    {
        public int JobAutoID { get; set; }

        public string DecimalDigitCount { get; set; }

        public Settings ToLM()
        {
            Dictionary<string, string> messages = new Dictionary<string, string>();


            int value = 0;

            if (DecimalDigitCount == null)
            {
                messages.Add("decimalDigitCount", "不能为空");
            }
            else if (!int.TryParse(DecimalDigitCount.Trim(), out value))
            {
                messages.Add("decimalDigitCount", "数字格式错误");
            }
            else if (!(value >= 1 && value <= 9))
            {
                messages.Add("decimalDigitCount", "必须在1到9之间");
            }


            if (messages.Count > 0)
            {
                throw new CustomDataException(messages);
            }


            return new Settings() { DecimalDigitCount = value };
        }
    }
}