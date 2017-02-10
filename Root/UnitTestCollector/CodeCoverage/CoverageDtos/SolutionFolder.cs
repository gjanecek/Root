using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCoverage.CoverageDtos
{
    public class SolutionFolder : MetricValues
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
