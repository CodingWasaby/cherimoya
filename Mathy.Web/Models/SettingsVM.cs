using Mathy.Planning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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