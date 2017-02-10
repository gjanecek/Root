using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCoverage
{
    public class CoverageSpeciment2
    {
        public string Solution { get; set; }
        public string RunDescription { get; set; }
        public DateTime RunDate { get; set; }
        public CoverageDtos.Root RootSample { get; set; }
    }
}
