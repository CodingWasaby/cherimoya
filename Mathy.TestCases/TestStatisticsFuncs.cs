using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mathy.Planning;
using Mathy.Maths;
using System.Text;

namespace Mathy.TestCases
{
    [TestClass]
    public class TestStatisticsFuncs
    {
        [TestInitialize]
        public void Initialize()
        {
            MathyShell.Init(@"H:\Users\lkd\Desktop\TwilightPlain\Repository\Mathy");
        }

        [TestCleanup]
        public void Cleanup()
        {
        }


        // Grubbs

        [TestMethod]
        public void GrubbsPositive()
        {
            string expression = "sample=m[1];d=grubbs(sample,5);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);

            ec.SetValueString("m", "[1.2,1.3,1.5,1.9,1.6,2.2,5.9]");
            ec.Update();

            double[] items = ec.GetValue("d") as double[];
            Assert.AreEqual("1.2,1.3,1.5,1.9,1.6,2.2", string.Join(",", items));
        }

        [TestMethod]
        public void GrubbsNegative()
        {
            string expression = "sample=m[1];d=grubbs(sample,5);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);

            ec.SetValueString("m", "[1.2,1.3,1.5,1.9,1.6,2.2,2.9]");
            ec.Update();

            double[] items = ec.GetValue("d") as double[];
            Assert.AreEqual("1.2,1.3,1.5,1.9,1.6,2.2,2.9", string.Join(",", items));
        }

        public void powTest()
        {

        }


        // Dixon

        [TestMethod]
        public void DixonRemoveLargest()
        {
            string expression = "sample=m[1];d=dixon(sample,5);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);

            ec.SetValueString("m", "[5.1,2.6,3.5,7.9,1.2,6.9,9.6,7.2,29.5,16.5,11.9,10.6,12.5]");
            ec.Update();

            double[] items = ec.GetValue("d") as double[];
            Assert.AreEqual("5.1,2.6,3.5,7.9,1.2,6.9,9.6,7.2,16.5,11.9,10.6,12.5", string.Join(",", items));
        }

        [TestMethod]
        public void DixonRemoveSmallest()
        {
            string expression = "sample=m[1];d=dixon(sample,5);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);

            ec.SetValueString("m", "[25.1,22.6,23.5,22.9,21.2,26.9,29.6,29.2,1.9,36.5,41.9,30.6,22.5]");
            ec.Update();

            double[] items = ec.GetValue("d") as double[];
            Assert.AreEqual("25.1,22.6,23.5,22.9,21.2,26.9,29.6,29.2,36.5,41.9,30.6,22.5", string.Join(",", items));
        }

        [TestMethod]
        public void DixonNegative()
        {
            string expression = "sample=m[1];d=dixon(sample,5);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);

            ec.SetValueString("m", "[5.1,2.6,3.5,7.9,1.2,6.9,9.6,7.2,2.9,16.5,11.9,10.6,12.5]");
            ec.Update();

            double[] items = ec.GetValue("d") as double[];
            Assert.AreEqual("5.1,2.6,3.5,7.9,1.2,6.9,9.6,7.2,2.9,16.5,11.9,10.6,12.5", string.Join(",", items));
        }


        // Cochran

        [TestMethod]
        public void CochranPositive()
        {
            string expression = "d=cochran(m,5);isPositive=rowCount(d)!=rowCount(m);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);

            ec.SetValueString("m", "[1.20,1.22,1.25,1.29,1.25;1.22,1.26,1.29,1.25,1.22;1.25,1.55,1.29,1.35,1.26]");
            ec.Update();

            Matrix d = ec.GetValue("d") as Matrix;
            bool isPositive = (bool)ec.GetValue("isPositive");

            Assert.AreEqual("[1.20,1.22,1.25,1.29,1.25;1.22,1.26,1.29,1.25,1.22]", d.GetString(2).Replace(" ", null));
            Assert.AreEqual(true, isPositive);
        }

        [TestMethod]
        public void CochranNegative()
        {
            string expression = "d=cochran(m,5);isPositive=rowCount(d)!=rowCount(m);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);

            ec.SetValueString("m", "[1.20,1.22,1.25,1.29,1.25;1.22,1.26,1.29,1.25,1.22;1.25,1.25,1.29,1.22,1.26]");
            ec.Update();

            Matrix d = ec.GetValue("d") as Matrix;
            bool isPositive = (bool)ec.GetValue("isPositive");

            Assert.AreEqual("[1.20,1.22,1.25,1.29,1.25;1.22,1.26,1.29,1.25,1.22;1.25,1.25,1.29,1.22,1.26]", d.GetString(2).Replace(" ", null));
            Assert.AreEqual(false, isPositive);
        }


        // Shapiro

        [TestMethod]
        public void SharipoPass()
        {
            string expression = "sample=m[1];isPass=shapiro(sample,95);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);

            ec.SetValueString("m", "[2.4,2.4,2.5,2.7,2.7,2.7,2.83,2.86,2.86,2.9,2.92,2.92,3,3,3.01,3.03,3.04,3.04,3.08,3.08,,3.08,3.09,3.1,3.1,3.1,3.12,3.12,3.16,3.19,3.19,3.2,3.2,3.21,3.27,3.37,3.37,3.38,3.43,3.65,3.68]");
            ec.Update();

            bool isPass = (bool)ec.GetValue("isPass");

            Assert.AreEqual(true, isPass);
        }

        [TestMethod]
        public void SharipoNotPass()
        {
            string expression = "sample=m[1];isPass=shapiro(sample,99);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);

            ec.SetValueString("m", "[5,5,5,5,5,10,10,10,10,10]");
            ec.Update();

            bool isPass = (bool)ec.GetValue("isPass");

            Assert.AreEqual(false, isPass);
        }


        // Dagostino Pass

        [TestMethod]
        public void DagostinoPass()
        {
            string expression = "sample=m[1];isPass=dagostino(sample,95);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);

            ec.SetValueString("m", "[11.7,11.9,11.9,12.1,12.2,12.2,12.4,12.4,12.6,12.6,12.6,12.6,12.6,12.7,12.7,12.7,12.7,12.8,12.8,12.8,12.8,12.8,12.8,12.9,13,13,13.1,13.1,13.1,13.1,13.1,13.1,13.1,13.2]");
            ec.Update();

            bool isPass = (bool)ec.GetValue("isPass");

            Assert.AreEqual(true, isPass);
        }


        // Anova

        // Page 196

        [TestMethod]
        public void Avona()
        {
            string expression = "u=anovau(m);f=f(m,5);";
            EvaluationContext ec = Funcs.CreateEvaluationContext(expression);


            StringBuilder b = new StringBuilder();
            
            b.Append("3.59,3.57,3.57;");
            b.Append("3.59,3.58,3.58;");
            b.Append("3.57,3.56,3.57;");
            b.Append("3.57,3.60,3.59;");
            b.Append("3.56,3.56,3.59;");
            b.Append("3.59,3.59,3.59;");
            b.Append("3.57,3.57,3.56;");
            b.Append("3.56,3.57,3.57;");
            b.Append("3.57,3.59,3.57;");
            b.Append("3.56,3.57,3.57;");

            b.Append("3.57,3.57,3.56;");
            b.Append("3.59,3.59,3.57;");
            b.Append("3.57,3.59,3.57;");
            b.Append("3.59,3.57,3.56;");
            b.Append("3.56,3.57,3.59;");
            b.Append("3.57,3.59,3.59;");
            b.Append("3.56,3.59,3.59;");
            b.Append("3.60,3.57,3.59;");
            b.Append("3.60,3.59,3.59;");
            b.Append("3.59,3.59,3.57;");


            ec.SetValueString("m", "[" + b + "]");
            ec.Update();

            double u = (double)ec.GetValue("u");
            bool f = (bool)ec.GetValue("f");

            Console.WriteLine(u);
            Assert.AreEqual(true, f);
        }
    }
}
