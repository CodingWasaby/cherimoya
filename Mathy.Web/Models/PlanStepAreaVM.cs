﻿using Dandelion.Text;
using Mathy.Maths;
using Mathy.Planning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace Mathy.Web.Models
{
    public class PlanStepAreaVM
    {
        public PlanStepAreaVM(EvaluationContext context, Step step, int stepIndex, int updateCount)
        {
            Index = stepIndex;
            Title = step.SourceExpression.Title;
            Description = step.SourceExpression.Description;
            ImageUrl = "/Home/GetStepImage?stepIndex=" + stepIndex + "&updateCount=" + updateCount;
        }


        public int Index { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}