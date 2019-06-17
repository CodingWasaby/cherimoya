using Dandelion.Text;
using Mathy.Maths;
using Mathy.Planning;
using Mathy.Visualization.Computation;
using Roselle;
using Roselle.Building;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petunia
{
    class ReportBuilder
    {
        private DocumentBuilder db;

        private EvaluationContext context;


        public Report Build(EvaluationContext context)
        {
            db = new DocumentBuilder();
            this.context = context;

            db.Center(context.Plan.Title, FontSize.ExtraLarge, TextStyle.Bold);
            db.Br();
            db.Center("实验日期：" + DateTime.Now.ToLongDateString(), FontSize.Normal);
            db.Pb();
            BuildChaptor1();
            db.Pb();
            BuildChaptor2();
            db.Pb();
            BuildChaptor3();
            db.Pb();
            BuildChaptor4();
            db.Pb();
            BuildChaptor5();

            return new Report(db.Document);
        }

        private void BuildChaptor1()
        {
            db.T0("一 简述")
              .T1("1.1 摘要")
              .P(context.Plan.Description)
              .T1("1.2 实验准备")
              .StartTable()
              .Header("名称", "标记", "类型")
              .Rows(context.Plan.Variables.Select(i => new string[] { i.Description, i.Name, i.Type.ToString() }))
              .EndTable()
              .T1("1.3 实验数据")
              .StartTable()
              .Header("标记", "数据")
              .Rows(context.InVariables.Select(i => new string[] { i, context.GetValue(i).ToString() }))
              .EndTable();
        }

        private void BuildChaptor2()
        {
            db.Title("二 步骤", 0);

            int seq = 0;

            foreach (Step step in context.Steps)
            {
                seq++;

                db.Br();
                db.T1(string.Format("2.{0} {1}", seq, step.SourceExpression.Title));
                db.P(step.SourceExpression.Description);
                db.Image(step.Image, "Graph" + seq + ".png");

                foreach (string variable in step.OutVariables)
                {
                    object value = context.GetValue(variable);

                    if (value is Matrix)
                    {
                        db.P(variable);
                        db.P((value as Matrix).GetString(context.Settings.DecimalDigitCount));
                    }
                    else if (value is Bitmap)
                    {
                        db.P(variable);
                        db.Image(value as Bitmap, variable + ".png");
                    }
                    else
                    {
                        db.P(string.Format("{0}: {1}", variable, 
                            value is double ? 
                            NumberFormatter.ForDecimalDigits(context.Settings.DecimalDigitCount).ToText((double)value) :
                            value.ToString())
                        );
                    }
                }
            }
        }

        private void BuildChaptor3()
        {
            db.T0("三 实验过程图示")
              .Image(ComputePlanVisualizer.Visualize(context), "ComputationTree.png");
        }

        private void BuildChaptor4()
        {
            db.T0("四 实验结论")
              .T1("4.1 结论");

            for (int i = 1; i <= 10; i++)
            {
                db.Br();
            }


            db.T1("4.2 签字");

            for (int i = 1; i <= 10; i++)
            {
                db.Br();
            }
        }

        private void BuildChaptor5()
        {
            db.T0("五 附录");
        }
    }
}
