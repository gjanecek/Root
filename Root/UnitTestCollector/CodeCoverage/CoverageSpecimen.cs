using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCoverage
{
    public class CoverageSpecimen
    {
        public string Solution { get; set; }
        public string RunDescription { get; set; }
        public DateTime RunDate { get; set; }
        public Root RootSample { get; set; }
        public CodeCoverage.CoverageDtos.Root DtoRoot { get; set; }
    }
}
