using Cherimoya;
using Dandelion.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Planning
{
    class PlanParser
    {
        public static Plan Parse(string s)
        {
            Plan instance = new JsonDeserializer().DeserializeString(s, typeof(Plan)) as Plan;

            foreach (SourceVariable variable in instance.Variables)
            {
                if (!IsVariable(variable.Name))
                {
                    throw new Exception(string.Format("{0} is not a valid variable name.", variable.Name));
                }
            }


            if (instance.Styles == null)
            {
                instance.Styles = new Style[] { };
            }


            return instance;
        }

        private static bool IsVariable(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            else
            {
                char c = s[0];
                return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '_' || c >= 256;
            }
        }
    }
}
