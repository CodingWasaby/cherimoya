using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cherimoya.Expressions
{
    public class Variable
    {
        Variable()
        {
        }

        public static Variable Of(String name, Object value)
        {
            Variable instance = new Variable();
            instance.Name = name;
            instance.Value = value;
            return instance;
        }

        public string Name { get; private set; }

        public object Value { get; private set; }
    }
}
