using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petunia
{
    class ArticleParser
    {
        public Article Parse(string filePath)
        {
            List<IArticleItem> items = new List<IArticleItem>();

            using (StreamReader file = new StreamReader(filePath, System.Text.Encoding.UTF8))
            {
                while (!file.EndOfStream)
                {
                    IArticleItem item = ParseLine(file.ReadLine());
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
            }


            return new Article() { Items = items.ToArray() };
        }


        private bool isInCode;

        private StringBuilder code;

        private IArticleItem ParseLine(string line)
        {
            if (isInCode)
            {
                if (line == "@>")
                {
                    isInCode = false;
                    return new Code() { Text = code.ToString() };
                }
                else
                {
                    code.AppendLine(line);
                    return null;
                }
            }
            else if (line.StartsWith("@"))
            {
                if (line.StartsWith("@{.}"))
                {
                    return new Bullet() { Text = line.Substring(5) };
                }
                else if (line == "@{ex}")
                {
                    return new Example();
                }
                else if (line == "@<")
                {
                    isInCode = true;
                    code = new StringBuilder();
                    return null;
                }
                else if (line.StartsWith("@{image}"))
                {
                    return new Image() { FileName = line.Substring(8).Trim() };
                }
                else
                {
                    return new Code() { Text = line.Substring(1).Trim() };
                }
            }
            else if (string.IsNullOrEmpty(line))
            {
                return new EmptyLine();
            }
            else
            {
                return new Paragraph() { Text = line };
            }
        }
    }
}
