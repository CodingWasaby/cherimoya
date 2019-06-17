using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petunia
{
    public class Article
    {
        public IArticleItem[] Items { get; set; }


        public static Article Parse(string filePath)
        {
            return System.IO.File.Exists(filePath) ? new ArticleParser().Parse(filePath) : new Article() { Items = new IArticleItem[] { } };
        }
    }

    public interface IArticleItem
    {
    }

    public class Bullet : IArticleItem
    {
        public string Text { get; set; }
    }

    public class EmptyLine : IArticleItem
    {
    }

    public class Example : IArticleItem
    {
    }

    public class Code : IArticleItem
    {
        public string Text { get; set; }
    }

    public class Image : IArticleItem
    {
        public string FileName { get; set; }
    }

    public class Paragraph : IArticleItem
    {
        public string Text { get; set; }
    }
}
