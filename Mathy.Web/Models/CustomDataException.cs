using System;

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