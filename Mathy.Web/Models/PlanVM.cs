using Mathy.Planning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Models
{
    public class PlanVM
    {
        public PlanVM(EvaluationContext context)
        {
            Title = context.Plan.Title;
            Description = context.Plan.Description;
            Author = context.Plan.Author;
            Steps = context.Steps.Select((i, index) => new PlanStepListCellVM(i, index)).ToArray();
        }


        public string Title { get; private set; }

        public string Description { get; private set; }

        public string Author { get; private set; }

        public PlanStepListCellVM[] Steps { get; private set; }
    }
}