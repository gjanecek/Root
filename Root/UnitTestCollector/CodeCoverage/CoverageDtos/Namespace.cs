using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCoverage.CoverageDtos
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Namespace : MetricValues
    {
        private List<NamespaceType> namespaceTypes;

        [System.Xml.Serialization.XmlElementAttribute("Type", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<NamespaceType> NamespaceTypes
        {
            get { return namespaceTypes; }
            set { namespaceTypes = value; }
        }
    }
}
