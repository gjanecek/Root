using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CodeCoverage;
using CodeCoverage.CoverageDtos;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.Win.UltraWinTree;
using Utility;
using SolutionFolder = CodeCoverage.CoverageDtos.SolutionFolder;
using ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle;

namespace UnitTestCollectorWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ultraGrid1.DisplayLayout.ViewStyle = ViewStyle.MultiBand;
            ultraGrid1.DisplayLayout.MaxBandDepth = 10;

            this.ultraChart1.Tooltips.Format = TooltipStyle.LabelPlusDataValue;

            FillCoverageReportsTree();
            //FillDataGrid();
        }

        private void FillCoverageReportsTree()
        {
            foreach (var folder in Directory.GetDirectories(@"C:\Temp\CodeCoveage"))
            {
                var solution = Path.GetFileName(folder);
                var topNode = new UltraTreeNode(solution) { Key = solution };
                this.ultraTree1.Nodes.Add(topNode);
                FillNodeChildren(topNode, folder);
            }
        }

        private static void FillNodeChildren(UltraTreeNode clientTopNode, string directoryPath)
        {

            foreach (var file in Directory.GetFiles(directoryPath))
            {
                var node = new UltraTreeNode(Path.GetFileNameWithoutExtension(file).Substring(0, 10));
                var sample = GenericSerializer.DeserializeFilePath<CodeCoverage.CoverageDtos.Root>(file);
                var coverageSpecimen = new CoverageSpecimen
                {
                    Solution = clientTopNode.Key,
                    //RootSample = sample,
                    DtoRoot = sample,
                    RunDescription = node.Text
                };
                node.Key = file;
                node.Tag = coverageSpecimen;
                Decimal detailedCoveragePercent = Convert.ToDecimal(sample.CoveredStatements) / Convert.ToDecimal(sample.TotalStatements) * 100m;
                node.Text += $" ({sample.CoveredStatements} / {sample.TotalStatements} / {detailedCoveragePercent.ToString("0,0.000")}%)";
                clientTopNode.Nodes.Add(node);
            }
        }


        private List<CoverageMetric> GetCoverageMetrics(CoverageSpecimen coverageSpecimen)
        {
            var metrics = new List<CoverageMetric>();

            var key = coverageSpecimen.Solution + "Root";
            var id = 0;

            var folderMetrics = ProcessSolutionFolders(coverageSpecimen, coverageSpecimen.DtoRoot.SolutionFolders, key);
            metrics.AddRange(folderMetrics);

            var projectMetrics = ProcessProjectFolders(coverageSpecimen, coverageSpecimen.DtoRoot.Projects, key);
            metrics.AddRange(projectMetrics);

            return metrics;
        }

        private List<CoverageMetric> ProcessProjectFolders(CoverageSpecimen coverageSpecimen, List<Project> projects, string key)
        {
            var metrics = new List<CoverageMetric>();
            if (projects == null) return metrics;
            foreach (var project in projects)
            {
                var projectKey = $"{key}.{project.Name}";
                if (!project.Namespaces.Any()) continue;
                foreach (var projectNamespace in project.Namespaces)
                {
                    var projectNamespaceKey = $"{projectKey}.{projectNamespace.Name}";
                    metrics.Add(CreateCoverageMetric1(coverageSpecimen, projectNamespaceKey, projectNamespace.TotalStatements, projectNamespace.CoveredStatements, projectNamespace.CoveragePercent));
                    if (!projectNamespace.NamespaceTypes.Any()) continue;
                    foreach (var namespaceType in projectNamespace.NamespaceTypes)
                    {
                        var namespaceTypeKey = $"{projectNamespaceKey}.{namespaceType.Name}";
                        metrics.Add(CreateCoverageMetric1(coverageSpecimen, namespaceTypeKey, namespaceType.TotalStatements, namespaceType.CoveredStatements, namespaceType.CoveragePercent));
                    }
                }

            }
            return metrics;

        }

        private List<CoverageMetric> ProcessSolutionFolders(CoverageSpecimen coverageSpecimen, List<CodeCoverage.CoverageDtos.SolutionFolder> solutionFolders, string key)
        {
            var metrics = new List<CoverageMetric>();

            foreach (var childSolutionFolder in solutionFolders)
            {
                var projectMetrics = ProcessProjectFolders(coverageSpecimen, childSolutionFolder.Projects, key);
                metrics.AddRange(projectMetrics);

                List<CoverageMetric> folderMetrics = ProcessSolutionFolders(coverageSpecimen,
                    childSolutionFolder.SolutionFolders, key);
                metrics.AddRange(folderMetrics);
            }
            return metrics;
        }


        private CoverageMetric CreateCoverageMetric1(CoverageSpecimen coverageSpecimen, string key, string totalStatements, string coveredStatements, string coveragePercent)
        {
            var coverageMetric = CreateCoverageMetric(key, totalStatements, coveredStatements, coveragePercent);
            coverageMetric.Source = coverageSpecimen.RunDescription;
            coverageMetric.Solution = coverageSpecimen.Solution;
            return coverageMetric;
        }

        private CoverageMetric CreateCoverageMetric(string key, string totalStatements, string coveredStatements, string coveragePercent)
        {
            return new CoverageMetric
            {
                Key = key,
                CoveragePercent = int.Parse(coveragePercent),
                TotalStatements = int.Parse(totalStatements),
                CoveredStatements = int.Parse(coveredStatements),
                ChangeStatus = "No Change"
            };
        }

        private void ultraTree1_MouseClick(object sender, MouseEventArgs e)
        {
            ultraChart1.LineChart.ChartComponent.Series.Clear();
            if (ultraTree1.SelectedNodes.Count != 2)
                return;

            var coverageMetrics = new List<CoverageMetric>();
            foreach (var node in ultraTree1.SelectedNodes)
            {
                coverageMetrics.AddRange(GetCoverageMetrics((CoverageSpecimen)node.Tag));
            }


            var unmatchedMetrics = new List<CoverageMetric>();
            foreach (var metricKeyGroup in coverageMetrics.GroupBy(x => x.Key))
            {
                var uniqueMetrics = metricKeyGroup
                        .GroupBy(p => p.CoveragePercent)
                        .Select(g => g.First())
                        .ToList();
                if (uniqueMetrics.Count() != 1)
                {
                    uniqueMetrics.ForEach(x => x.ChangeStatus = "No Change");
                    if (uniqueMetrics[0].CoveragePercent > uniqueMetrics[1].CoveragePercent)
                    {
                        uniqueMetrics.ForEach(x => x.ChangeStatus = "Reduced");
                    }
                    else if (uniqueMetrics[0].CoveragePercent < uniqueMetrics[1].CoveragePercent)
                    {
                        uniqueMetrics.ForEach(x => x.ChangeStatus = "Improved");
                    }
                    unmatchedMetrics.AddRange(uniqueMetrics);
                    continue;
                }

                uniqueMetrics = metricKeyGroup
                        .GroupBy(p => p.TotalStatements)
                        .Select(g => g.First())
                        .OrderBy(x => x.Source).ToList();
                if (uniqueMetrics.Count() != 1)
                {
                    foreach (var a in uniqueMetrics)
                    {
                        if (a.ChangeStatus == "No Change")
                            a.ChangeStatus = "Total Statements Changed";
                    }
                    unmatchedMetrics.AddRange(metricKeyGroup);
                    continue;
                }
                //uniqueMetrics = metricKeyGroup
                //        .GroupBy(p => p.CoveredStatements)
                //        .Select(g => g.First())
                //        .ToList();
                uniqueMetrics = metricKeyGroup
                        .GroupBy(p => p.CoveredStatements)
                        .Select(g => g.First())
                        .OrderBy(x => x.Source).ToList();
                if (uniqueMetrics.Count() != 1)
                {
                    foreach (var a in uniqueMetrics)
                    {
                        if (a.ChangeStatus == "Total Statements Changed")
                            a.ChangeStatus = "Total & Covered Statements Changed";
                    }
                    foreach (var a in uniqueMetrics)
                    {
                        if (a.ChangeStatus == "No Change")
                            a.ChangeStatus = "Covered Statements Changed";
                    }

                    unmatchedMetrics.AddRange(metricKeyGroup);
                    continue;
                }
            }
            ultraGrid1.DataSource = unmatchedMetrics;
        }

        private string LastElement(string key)
        {
            return key.Split('.').Last();
        }

    }


}
