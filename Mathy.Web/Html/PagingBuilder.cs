using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Html
{

    /**
     * PagingBuilder is used to build a Paging.
     */
    public class PagingBuilder
    {
        public string[] ItemTemplates { get; set; }


        public int PageIndex { get; set; }

        public int PageCount { get; set; }


        public PagingMode Mode { get; set; }


        private object[] parameters;

        private List<PagingItem> items = new List<PagingItem>();


        public Paging Build()
        {
            int paramCount = 0;

            for (int i = 0; i <= ItemTemplates.Length - 1; i++)
            {
                paramCount = Math.Max(FindParamCount(ItemTemplates[i]), paramCount);
            }

            parameters = new object[paramCount];


            int prevIndex = 0;

            foreach (int index in Mode.GetItems(PageCount, PageIndex))
            {

                if (index != prevIndex + 1)
                {
                    AddGap();
                }

                AddButton(index);

                prevIndex = index;
            }


            return new Paging() { Items = items.ToArray() };
        }

        private int FindParamCount(String template)
        {
            int count = 0;
            int pos = 0;

            while (true)
            {
                int pos1 = -1;

                while (true)
                {
                    pos1 = template.IndexOf('{', pos);
                    if (pos1 != -1)
                    {
                        if (template[pos1 + 1] == '{')
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (pos1 == -1)
                {
                    break;
                }


                count++;
                pos = pos1 + 1;
            }


            return count;
        }


        private void AddButton(int pageIndex)
        {
            string[] texts = new string[ItemTemplates.Length];

            for (int i = 0; i <= ItemTemplates.Length - 1; i++)
            {

                for (int j = 0; j <= parameters.Length - 1; j++)
                {
                    parameters[j] = pageIndex;
                }

                texts[i] = string.Format(ItemTemplates[i], parameters);
            }

            items.Add(new PagingItem()
            {
                Index = pageIndex,
                Texts = texts,
                IsCurrent = pageIndex == PageIndex
            });
        }

        private void AddGap()
        {
            items.Add(new PagingItem()
            {
                IsGap = true
            });
        }
    }
}