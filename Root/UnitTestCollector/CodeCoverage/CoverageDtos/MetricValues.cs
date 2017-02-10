using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeCoverage.CoverageDtos
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class MetricValues
    {
        private string name;

        private string coveredStatements;

        private string totalStatements;

        private string coveragePercent;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CoveredStatements
        {
            get { return coveredStatements; }
            set { coveredStatements = value; }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalStatements
        {
            get { return totalStatements; }
            set { totalStatements = value; }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CoveragePercent
        {
            get { return coveragePercent; }
            set { coveragePercent = value; }
        }

    }
}
