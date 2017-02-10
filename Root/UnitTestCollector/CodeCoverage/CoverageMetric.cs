using System.Diagnostics.CodeAnalysis;

namespace CodeCoverage
{
    public partial class CoverageMetric : CoverageMetricData
    {
        private static int m_Counter = 0;

        public CoverageMetric() 
        {
            this.Id = System.Threading.Interlocked.Increment(ref m_Counter);
        }

        public int Id;

    }

    [ExcludeFromCodeCoverage]
    public partial class CoverageMetricData
    {

        public string Source { get; set; }

        public string Key { get; set; }

        public string Solution { get; set; }

        public int CoveredStatements { get; set; }

        public int TotalStatements { get; set; }

        public int CoveragePercent { get; set; }

        public string ChangeStatus { get; set; }

    }
}
