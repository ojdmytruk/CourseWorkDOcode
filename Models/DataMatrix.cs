using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWorkDO.Models
{
    public class DataMatrix
    {
        public int Dimension { get; set; }
        public double[][] Floats { get; set; }
        public double[][] Distances { get; set; }
    }
}
