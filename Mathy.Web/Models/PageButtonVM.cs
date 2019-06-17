using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Models
{
    public class PageButtonVM
    {
        public string Text { get; set; }

        public string ClassName { get; set; }

        public bool IsGap { get; set; }

        public string OnClick { get; set; }
    }
}