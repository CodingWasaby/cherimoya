using Cherimoya.Expressions;
using Cherimoya.Evaluation;
using Cherimoya.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathy.Maths;
using Mathy.Libs;
using System.Drawing;
using Mathy.Planning;

namespace Mathy.Language
{
    class MathyLanguageService : LanguageService
    {
        static MathyLanguageService()
        {
            ImportNamespace("System");
        }

        public override void OnCreateTypeCheckingContext(TypeCheckingContext context)
        {
            context.AddAutoCreatedValue(typeof(Vector), new Vector(0));
            context.AddAutoCreatedValue(typeof(Matrix), new Matrix(0, 0));
            context.AddAutoCreatedValue(typeof(Bitmap), new Bitmap(1, 1));
            context.AddAutoCreatedValue(typeof(VariableContextExpression), new VariableContextExpression(null, null, 0, 0));
            context.AddAutoCreatedValue(typeof(Expression), ConstantExpression.create(1, 0, 0));
        }

        public override ExpressionCompiler CreateCompiler()
        {
            return new MathyExpressionCompiler();
        }

        public override TypeChecker CreateTypeChecker()
        {
            return new MathyTypeChecker();
        }

        public override ExpressionEvaluator CreateEvaluator()
        {
            return new MathyExpressionEvaluator();
        }

        public static VariableContext CreateVariableContext()
        {
            VariableContext vc = new VariableContext();

            vc.AddMethodExtender(typeof(DateFuncs));
            vc.AddMethodExtender(typeof(GeneralFuncs));
            vc.AddMethodExtender(typeof(MatrixFuncs));
            vc.AddMethodExtender(typeof(GraphFuncs));
            vc.AddMethodExtender(typeof(MapReduceFuncs));
            vc.AddMethodExtender(typeof(StatisticsFuncs));
            vc.AddMethodExtender(typeof(ExpressionFuncs));
            vc.AddMethodExtender(typeof(CustomFuncFactory));
            vc.AddMethodExtender(typeof(Distribution));
            vc.AddMethodExtender(typeof(DrawFuncs));
            
            return vc;
        }
    }
}
