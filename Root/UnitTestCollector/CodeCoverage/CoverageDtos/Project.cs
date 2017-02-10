using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCoverage.CoverageDtos
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Project : MetricValues
    {
        private List<Namespace> namespaces;

        [System.Xml.Serialization.XmlElementAttribute("Namespace", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<Namespace> Namespaces
        {
            get { return namespaces; }
            set { namespaces = value; }
        }
    }
}
