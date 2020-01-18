using Mathy.Planning;

namespace Mathy.TestCases
{
    class Funcs
    {
        public static EvaluationContext CreateEvaluationContext(string expression)
        {
            string content = string.Format("{{\"Expressions\":[{{\"Expression\":\"{0}\"}}],\"Variables\":[{{\"Name\":\"m\", \"Type\":\"Matrix\"}}]}}", expression);

            EvaluationContext instance = Plan.Parse(content).CreateEvaluationContext();
            instance.Settings = new Settings() { DecimalDigitCount = 5 };


            return instance;
        }
    }
}
