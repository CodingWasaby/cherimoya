using Mathy.DAL;
using Mathy.Planning;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            var dal = new PlanDAL();
            DirectoryInfo root = new DirectoryInfo("D:/个人/Cherimoya20161008/Cherimoya20180726/Cherimoya/Mathy.Web/Repository/Plans1");
            FileInfo[] files = root.GetFiles();
            foreach (var n in files.OrderByDescending(m=>m.CreationTime))
            {
                var text = File.ReadAllText(n.FullName, Encoding.UTF8);
                var plan = Plan.Parse(text);
                plan.Description = "";
                var f = JsonConvert.SerializeObject(plan);
                dal.AddPlanRepository(n.Name.Replace(".txt", ""), f);
            }
        }
    }
}
