﻿using Cherimoya.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Language
{
    public class ArrayExpression : Expression
    {
        public ArrayExpression(Expression[] items, int fromPosition, int toPosition)
            : base(fromPosition, toPosition)
        {
            Items = items;
        }

        public Expression[] Items { get; private set; }

        public override bool IsConstantExpression()
        {
            return Items.All(i => i.IsConstantExpression());
        }

        public override IEnumerable<Expression> GetChildren()
        {
            return Items;
        }
    }
}