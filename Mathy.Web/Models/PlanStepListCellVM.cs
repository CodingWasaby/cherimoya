using Mathy.Planning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Models
{
    public class PlanStepListCellVM
    {
        public PlanStepListCellVM(Step step, int index)
        {
            Title = step.SourceExpression.Title;
            Description = step.SourceExpression.Description;
            Index = index;
        }


        public string Title { get; private set; }

        public string Description { get; private set; }

        public int Index { get; private set; }
    }
}