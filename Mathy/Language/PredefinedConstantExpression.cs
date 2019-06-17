using Cherimoya.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Language
{
    public class PredefinedConstantExpression : Expression
    {
        internal PredefinedConstantExpression(string name, object value, int fromPosition, int toPosition) :
            base(fromPosition, toPosition)
        {
            Name = name;
            Value = value;
        }


        public string Name { get; private set; }

        public object Value { get; private set; }


        public override bool IsConstantExpression()
        {
            return true;
        }

        public override IEnumerable<Expression> GetChildren()
        {
            return new Expression[] { };
        }
    }
}
