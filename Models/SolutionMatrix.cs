using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWorkDO.Models
{
    public class SolutionMatrix
    {
        public int Dimension { get; set; }
        public int Score { get; set; }
        public List<int> Solution { get; set; }
        public int[] SolutionArray { get; set; }
    }
}
