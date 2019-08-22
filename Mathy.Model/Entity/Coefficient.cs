using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Model.Entity
{
    public class Coefficient
    {
        public int CoefficientID { get; set; }
        public string CoefficientName { get; set; }
        public string CoefficientContent { get; set; }
        public int DeleteFlag { get; set; }
        public string Creator { get; set; }
        public string CreateTime { get; set; }
    }

    [Serializable]
    public class CoefficientVM: Coefficient
    {  
        public List<CoefficientDetail> CoefficientDetails { get; set; }
    }
}
