using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Models
{
    public class CustomDataException : Exception
    {
        public CustomDataException(object data)
        {
            Data = data;
        }


        public object Data { get; private set; }
    }
}