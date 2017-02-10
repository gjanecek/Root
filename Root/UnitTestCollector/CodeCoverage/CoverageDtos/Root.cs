using System.Collections.Generic;

namespace CodeCoverage.CoverageDtos
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class Root : MetricValues
    {
        private List<SolutionFolder> solutionFolders;

        private List<Project> projects;

        [System.Xml.Serialization.XmlElementAttribute("SolutionFolder", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<SolutionFolder> SolutionFolders
        {
            get { return solutionFolders; }
            set { solutionFolders = value; }
        }

        [System.Xml.Serialization.XmlElementAttribute("Project", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<Project> Projects
        {
            get { return projects; }
            set { projects = value; }
        }
    }
}
