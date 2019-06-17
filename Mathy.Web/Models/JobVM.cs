using Mathy.Planning;
using Petunia.LogicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Models
{
    public class JobVM
    {
        public JobVM(EvaluationContext context, JobLM job)
        {
            AutoID = job.AutoID;
            EditID = Guid.NewGuid().ToString();
            Settings = new SettingsVM(context.Settings);
            PlanTitle = context.Plan.Title;
            Name = job.Name;
            Description = context.Plan.Description;
            Author = context.Plan.Author;
            Steps = context.Steps.Select((i, index) => new JobStepListCellVM(i, index)).ToArray();
            IsCompleted = context.Steps.All(i => i.State != StepState.Unready);

            ExportButtonClass = IsCompleted ? "button" : "button-disabled";
            ExportButtonCode = IsCompleted ? "javascript:exportAsWordDocument()" : null;

            ViewGraphButtonClass = IsCompleted ? "button" : "button-disabled";
            ViewGraphButtonCode = IsCompleted ? "javascript:viewGraph()" : null;
        }


        public int AutoID { get; set; }

        public string EditID { get; set; }

        public SettingsVM Settings { get; set; }

        public string PlanTitle { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Author { get; private set; }

        public JobStepListCellVM[] Steps { get; private set; }

        public string ExportButtonClass { get; private set; }

        public string ExportButtonCode { get; private set; }

        public string ViewGraphButtonClass { get; private set; }

        public string ViewGraphButtonCode { get; private set; }

        public bool IsCompleted { get; private set; }
    }
}