﻿using Cherimoya.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Language
{
    public class MultipleVariableExpression : Expression
    {
        public MultipleVariableExpression(string[] variables, int fromPosition, int toPosition)
            : base(fromPosition, toPosition)
        {
            Variables = variables;
        }

        public string[] Variables { get; private set; }

        public override bool IsConstantExpression()
        {
            return false;
        }

        public override IEnumerable<Expression> GetChildren()
        {
            return new Expression[] { };
        }
    }
}