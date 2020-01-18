using Mathy.Planning;
using Mathy.Visualization.Computation;

namespace Cherimoya.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            //   TestDiff.TestDiffPlan();
            //   return;



            /*
            MathyShell.Init(@"H:\VS Projects\Cherimoya\Mathy.Web\Repository\Mathy");

            Expression[] roots = MathyShell.Compile("x=[1,2,3,4,5,6];x1=mapd(x, i->pow(i,2));sum1=sum([1.0,2.0])", new Dictionary<string,object>());
            object result = MathyShell.Evaluate(roots, new Dictionary<string, object>());
            */


            EvaluationContext ec = Plan.Parse(System.IO.File.ReadAllText(@"H:\Users\lkd\Desktop\10faf6b7-6e03-4dd1-a6fd-33e4dd6fea92.txt")).CreateEvaluationContext();

            ec.Settings = new Settings() { DecimalDigitCount = 5 };
            ec.SetValueString("a", "1");
            ec.SetValueString("b", "8");
            ec.Update();



            ComputePlanVisualizer.Visualize(ec).Save(@"H:\Users\lkd\Desktop\r1.jpg");


            /*
            MathyShell.Init(@"H:\VS Projects\Cherimoya\Mathy.Web\Repository\Mathy");

            EvaluationContext ec = Plan.Parse(System.IO.File.ReadAllText(@"H:\Users\lkd\Desktop\TwilightPlain\demo1.txt")).CreateEvaluationContext();

            StringBuilder b = new StringBuilder();
            b.Append("121.3,-,119.91;");
      //      b.Append("121.3,128.74,119.91;");
            b.Append("120.87,121.32,119.24;");
            b.Append("122.44,122.96,123.45;");
            b.Append("117.6,119.66,118.96;");
            b.Append("110.65,112.34,110.29;");
            b.Append("117.29,120.79,121.42;");
            b.Append("115.27,121.45,117.48;");
            b.Append("118.96,123.78,123.29;");
            b.Append("118.67,116.67,114.58;");
            b.Append("126.24,123.51,126.2;");
            b.Append("128.65,122.02,121.93;");
            b.Append("126.84,124.72,123.14;");
            b.Append("122.61,128.48,126.2;");
            b.Append("118.95,123.82,118.11;");
            b.Append("118.74,118.23,117.38;");
            b.Append("119.74,121.78,121.01;");
            b.Append("121.21,123.28,116.38;");
            b.Append("129.3,124.1,122.02;");
            b.Append("136.81,129.8,128.47;");
            b.Append("127.81,117.66,122.9");


            ec.SetValueString("m", "[" + b + "]");
            ec.Settings = new Settings() { DecimalDigitCount = 3 };
            ec.Update();

             * /

            ec = null;

            /*
            EvaluationContext ec = Plan.Parse(System.IO.File.ReadAllText(@"H:\Users\lkd\Desktop\Petunia\Repository\Plans\2.txt")).CreateEvaluationContext();
            ec.SetValueString("from", "1");
            ec.SetValueString("to", "10");
            ec.SetValueString("p", "5");
            ec.SetValueString("a", "2");
            ec.SetValueString("width", "400");
            ec.SetValueString("height", "200");
            ec.Update();

            Report.FromEvaluationContext(ec).SaveAsWordDocument(@"H:\Users\lkd\Desktop\report.docx");
            */


            /*
            EvaluationContext ec = Plan.Parse(System.IO.File.ReadAllText(@"H:\Users\lkd\Desktop\Petunia\Repository\Plans\1.txt")).CreateEvaluationContext();
            ec.SetValueString("x", "[0.98464,0.98971,0.98028,0.98233,0.98854,0.98188,0.98963]");
            ec.SetValueString("im", "[0.0135,0.0012]");
            ec.SetValueString("mf", "[0.955,1.939]");
            ec.SetValueString("uim", "0.00013");
            ec.SetValueString("ub", "1");
            ec.SetValueString("uv", "0.35");
            ec.SetValueString("us", "0.01");
            ec.Update();

            Report.FromEvaluationContext(ec).SaveAsWordDocument(@"H:\Users\lkd\Desktop\report.docx");
            */





            /*
            string expression = "draw(@[expression(0,10,@x:double -> p*sin(a*x))], {margin:@[40,20,20,20],padding:@[10,10,10,10]}, 300, 300)";

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("p", 5);
            dict.Add("a", 2);

            Bitmap bitmap = MathyShell.Visualize(expression, dict);
            bitmap.Save(@"H:\Users\lkd\Desktop\pr1.jpg");


            byte[] data = (byte[])MathyShell.Evaluate(expression, dict);
 
            System.IO.File.WriteAllBytes(@"H:\Users\lkd\Desktop\r12.jpg", data);
             */
        }

        /*
        private static void TestDraw()
        {
            Grapher grapher = new Grapher();

            DiscreteDataSource dataSource1 = new DiscreteDataSource();
            dataSource1.Data = new KeyValuePair<double, double>[] 
            {
                new KeyValuePair<double, double>(20, 10),
                new KeyValuePair<double, double>(25, 20),
                new KeyValuePair<double, double>(50, 90),
                new KeyValuePair<double, double>(75, 120),
                new KeyValuePair<double, double>(129, 201)
            };

            DiscreteDataSource dataSource2 = new DiscreteDataSource();
            dataSource2.Style.PointStyle = PointRenderStyle.Square;
            dataSource2.Data = new KeyValuePair<double, double>[] 
            {
                new KeyValuePair<double, double>(10, 25),
                new KeyValuePair<double, double>(20, 50),
                new KeyValuePair<double, double>(79, 175),
                new KeyValuePair<double, double>(125, 160),
                new KeyValuePair<double, double>(220, 350)
            };

            ExpressionDataSource dataSource3 = new ExpressionDataSource();
            dataSource3.From = 20;
            dataSource3.To = 200;
            dataSource3.VariableName = "x";
            dataSource3.Expression = "sin(x / 20) * 50";

            grapher.DataSources = new GraphDataSource[] { dataSource1, dataSource2, dataSource3 };


            grapher.Margin = new Inset(40, 20, 20, 20);
            grapher.Padding = new Inset(20);

            grapher.Style = new GraphStyle()
            {
                StartColor = Color.LightBlue,
                EndColor = Color.LightCoral
            };

            Bitmap bitmap = grapher.Draw(400, 350);
            bitmap.Save(@"H:\Users\lkd\Desktop\r1.jpg");
        }
         */
    }
}
