using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Planning
{
    public class PlanRepository
    {
        public void Load(string repositoryPath)
        {
            this.repositoryPath = repositoryPath;
        }


        private string repositoryPath;


        public Plan Save(string id, Stream stream)
        {
            string content = null;

            using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }


            Plan plan = null;

            try
            {
                plan = Plan.Parse(content);
            }
            catch (Exception ex)
            {
                throw new Exception("解析试验计划文件错误。\r\n通常是因为实验文件内容错误造成。请检查您的实验文件，或联系相关人员协助解决。\r\n错误信息：\r\n" + ex.Message);
            }


            File.WriteAllText(GetFilePath(id), content);


            return plan;
        }

        public Plan Get(string id)
        {
            return Plan.Parse(File.ReadAllText(GetFilePath(id), System.Text.Encoding.UTF8));
        }

        public void Delete(string id)
        {
            File.Delete(GetFilePath(id));
        }


        private string GetFilePath(string id)
        {
            return Path.Combine(repositoryPath, id + ".txt");
        }
    }
}
