using Cherimoya.Expressions;
using Mathy;
using Mathy.Planning;
using Mathy.Visualization.Computation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cherimoya.Test
{
    class TestDiff
    {
        public static void CalcDiff()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["x"] = 0;

            Expression e = MathyShell.Compile("2*pow(x, 3)+2*pow(x, 2)+5", dict)[0];

            Expression e1 = MathyShell.Diff(e, "x", dict);

            Console.WriteLine("Nodes: " + e1.Flatten().Length);

            for (int i = 0; i <= 10; i++)
            {
                dict["x"] = i;
                Console.WriteLine(i + ", " + MathyShell.Evaluate(new Expression[] { e1 }, dict));
            }
        }

        public static void TestDiffPlan()
        {
            MathyShell.Init(@"H:\VS Projects\Cherimoya\Mathy.Web\Repository\Mathy");

            EvaluationContext ec = Plan.Parse(System.IO.File.ReadAllText(@"H:\Users\lkd\Desktop\TwilightPlain\diff.txt")).CreateEvaluationContext();

            ec.Settings = new Settings() { DecimalDigitCount = 5 };
            ec.Update();

            ComputePlanVisualizer.Visualize(ec).Save(@"H:\Users\lkd\Desktop\r12.jpg");

            // (ec.GetValue("graph") as Bitmap).Save(@"H:\Users\lkd\Desktop\r1.jpg");
        }

        public static void TestPartialDiffPlan()
        {
            MathyShell.Init(@"H:\VS Projects\Cherimoya\Mathy.Web\Repository\Mathy");

            EvaluationContext ec = Plan.Parse(System.IO.File.ReadAllText(@"H:\Users\lkd\Desktop\TwilightPlain\partial-diff.txt")).CreateEvaluationContext();

            ec.SetValueString("r", "[0,0.2,0,0,0;0,0.1,0.1,0,0;0.1,0.2,0,0,0;0,0.1,0.2,0,0;0,0.1,0.1,0,0]");
            ec.SetValueString("u", "[0.1,0.2,0.2,0.1,0.2]");

            ec.Settings = new Settings() { DecimalDigitCount = 5 };
            ec.Update();

            ComputePlanVisualizer.Visualize(ec).Save(@"H:\Users\lkd\Desktop\r12.jpg");

            int nodeCount = (ec.GetValue("f_1") as Expression).Flatten().Distinct().Count();
            double r1 = (double)ec.GetValue("r1");
        }
    }
}
