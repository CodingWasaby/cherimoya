using Mathy.DAL;
using System;
using System.IO;

namespace Mathy.Planning
{
    public class PlanRepository
    {
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
            var dal = new PlanDAL();
            //dal.
            dal.AddPlanRepository(id, content);
            return plan;
        }

        public Plan Get(string id)
        {
            var content = new PlanDAL().GetRepository(id);
            return Plan.Parse(content.Text);
        }

        public bool Copy(string fromID, string toID)
        {
            return new PlanDAL().CopyRepository(fromID, toID);
        }

        public void Delete(string id)
        {
            // File.Delete(GetFilePath(id));
        }
    }
}
