using Mathy.Planning;
using System;
using System.Linq;

namespace Mathy.Web.ServiceModels
{
    public class EditPlanSM
    {
        public string ID { get; set; }

        public string EditID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }
        public int UserRole { get; set; }
        public int PlanType { get; set; }
        public string PlanCategory { get; set; }


        public EditVariableSM[] Variables { get; set; }

        public EditExpressionSM[] Expressions { get; set; }


        public SourceVariable[] ToSourceVariables()
        {
            int index = 0;

            if (Variables != null)
            {
                foreach (EditVariableSM variable in Variables)
                {
                    if (string.IsNullOrEmpty(variable.Name))
                    {
                        throw new Exception(string.Format("第{0}个变量名称为空。", index + 1));
                    }


                    index++;
                }
            }

            return Variables == null ? new SourceVariable[] { } : Variables.Select(i =>
                    new SourceVariable()
                    {
                        Name = i.Name,
                        Type = (DataType)Enum.Parse(typeof(DataType), Funcs.GetDataTypeName(i.Type)),
                        Description = i.Description
                    }
                ).ToArray();
        }

        public Plan ToPlan()
        {
            int index = 0;

            if (Expressions == null || Expressions.Length == 0)
            {
                throw new Exception("必须至少有一个步骤。");
            }

            foreach (EditExpressionSM expression in Expressions)
            {
                if (string.IsNullOrEmpty(expression.Title))
                {
                    throw new Exception(string.Format("第{0}个步骤标题为空。", index + 1));
                }
                else if (string.IsNullOrEmpty(expression.Expression))
                {
                    throw new Exception(string.Format("第{0}个步骤表达式为空。", index + 1));
                }


                index++;
            }


            return new Plan()
            {
                ID = ID,
                Title = Title,
                Description = Description,
                Author = Author,
                Variables = ToSourceVariables(),
                Expressions = Expressions == null ? new SourceExpression[] { } : Expressions.Select(i =>
                    new SourceExpression()
                    {
                        Title = i.Title,
                        Description = i.Description,
                        Expression = i.Expression,
                        Condition = i.Condition
                    }
                ).ToArray(),
                Styles = ToStyles(),
                PlanType = PlanType,
                PlanCategory = PlanCategory,
                UserRole = UserRole
            };
        }

        public Style[] ToStyles()
        {
            return Variables == null ? new Style[] { } : Variables.Where(i => i.Style != null).Select(i => new Style()
            {
                Target = i.Name,
                Size = string.IsNullOrEmpty(i.Style.Size) ? 0 : int.Parse(i.Style.Size),
                ColumnNames = string.IsNullOrEmpty(i.Style.ColumnNames) ? null : i.Style.ColumnNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries),
                RowHeaderWidth = string.IsNullOrEmpty(i.Style.RowHeaderWidth) ? 0 : int.Parse(i.Style.RowHeaderWidth),
                RowName = string.IsNullOrEmpty(i.Style.RowName) ? null : i.Style.RowName
            }).ToArray();
        }
    }

    public class EditVariableSM
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public EditStyleSM Style { get; set; }
    }

    public class EditExpressionSM
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Expression { get; set; }

        public string Condition { get; set; }
    }

    public class EditStyleSM
    {
        public string Size { get; set; }

        public string ColumnNames { get; set; }

        public string RowHeaderWidth { get; set; }

        public string RowName { get; set; }
    }
}