using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWorkDO.Models
{
    public class DataMatrix
    {
        public int Dimension { get; set; }
        public int[][] Flows { get; set; }
        public int[][] Distances { get; set; }
    }
}
