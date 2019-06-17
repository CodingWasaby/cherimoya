﻿using Mathy.Planning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Models
{
    public class JobStepListCellVM
    {
        public JobStepListCellVM(Step step, int index)
        {
            Title = step.SourceExpression.Title;
            Description = step.SourceExpression.Description;
            
            if (step.State == StepState.Unready)
            {
                ImageFileName = "error.jpg";
            }
            else if (step.State == StepState.Ready)
            {
                ImageFileName = "ok.jpg";
            }
            else
            {
                ImageFileName = "skipped.png";
            }

            Index = index;
        }


        public string Title { get; private set; }

        public string Description { get; private set; }

        public string ImageFileName { get; private set; }

        public int Index { get; private set; }
    }
}