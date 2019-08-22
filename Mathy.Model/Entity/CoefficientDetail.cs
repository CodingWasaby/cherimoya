using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Model.Entity
{
    public class CoefficientDetail
    {
        public int CoefficientID { get; set; }
        public string CoefficientDetailName { get; set; }
        public decimal CoefficientDetailValue { get; set; }
        public int CoefficientDetailIndex { get; set; }
        public int CoefficientDetailRow { get; set; }
    }
}
