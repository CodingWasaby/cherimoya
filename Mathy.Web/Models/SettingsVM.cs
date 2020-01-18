using Mathy.Planning;

namespace Mathy.Web.Models
{
    public class SettingsVM
    {
        public SettingsVM(Settings settings)
        {
            DecimalDigitCount = settings.DecimalDigitCount;
        }


        public int DecimalDigitCount { get; set; }
    }
}