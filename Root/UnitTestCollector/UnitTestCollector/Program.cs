
using System.Collections.Generic;
using CodeCoverage.CoverageDtos;
using Utility;
using SolutionFolder = CodeCoverage.CoverageDtos.SolutionFolder;

namespace UnitTestCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            //SerializeSampleCoverage(@"C:\Temp\Ace.ClientCoverageReport.xml");
            //var rootSample = GenericSerializer.DeserializeFilePath<CodeCoverage.CoverageDtos.Root>(@"C:\Temp\Ace.ClientCoverageReport.xml");
            var rootSample = GenericSerializer.DeserializeFilePath<CodeCoverage.CoverageDtos.Root>(@"C:\Temp\CodeCoveage\Ace.Server\2016.08.10.AceServerCodeCoverage.xml");

            var abc = GenericSerializer.DeserializeFilePath<Root>(@"C:\Temp\Ace.ClientCoverageReport.xml");
            //foreach (var project in abc.Project)
            //{
            //    foreach (var projectNamespace in project.Namespace)
            //    {
            //        foreach (var namespaceType in projectNamespace.Type)
            //        {
            //            foreach (var method in namespaceType.Method)
            //            {

            //            }
            //            foreach (var property in namespaceType.Property)
            //            {

            //            }
            //        }
            //    }
            //}
        }

        private static void SerializeSampleCoverage(string cTempAceClientcoveragereportXml)
        {
            var root = new CodeCoverage.CoverageDtos.Root
            {
                Name = "Root",
                CoveredStatements = "1866",
                TotalStatements = "3124",
                CoveragePercent = "60",
                SolutionFolders = new List<CodeCoverage.CoverageDtos.SolutionFolder>
                {
                    new CodeCoverage.CoverageDtos.SolutionFolder
                    {
                        Name = "Tests",
                        CoveredStatements = "841",
                        TotalStatements = "855",
                        CoveragePercent = "98",
                        SolutionFolders = new List<CodeCoverage.CoverageDtos.SolutionFolder>
                        {
                            new CodeCoverage.CoverageDtos.SolutionFolder
                            {
                                Name = "Unit",
                                CoveredStatements = "841",
                                TotalStatements = "855",
                                CoveragePercent = "98",
                                Projects = new List<CodeCoverage.CoverageDtos.Project>
                                {
                                    new CodeCoverage.CoverageDtos.Project
                                    {
                                        Name = "CapitalGroup.Afsd.Server.Infrastructure.UnitTest",
                                        CoveredStatements = "841",
                                        TotalStatements = "855",
                                        CoveragePercent = "98",
                                        Namespaces = new List<Namespace>
                                        {
                                            new Namespace
                                            {
                                                Name = "CapitalGroup.Afsd.Server.Infrastructure.UnitTest.Attributes",
                                                CoveredStatements = "5",
                                                TotalStatements = "5",
                                                CoveragePercent = "100",
                                                NamespaceTypes = new List<NamespaceType>
                                                {
                                                    new NamespaceType
                                                    {
                                                        Name = "FromHeaderAttributeTests",
                                                        CoveredStatements = "5",
                                                        TotalStatements = "5",
                                                        CoveragePercent = "100",
                                                    }
                                                }
                                            },
                                            new Namespace
                                            {
                                                Name = "CapitalGroup.Afsd.Server.Infrastructure.UnitTest.BusinessLogic",
                                                CoveredStatements = "141",
                                                TotalStatements = "141",
                                                CoveragePercent = "100",
                                                NamespaceTypes = new List<NamespaceType>
                                                {
                                                    new NamespaceType
                                                    {
                                                        Name = "AccountsProcessorTests",
                                                        CoveredStatements = "48",
                                                        TotalStatements = "48",
                                                        CoveragePercent = "100",
                                                    },
                                                    new NamespaceType
                                                    {
                                                        Name = "CustomersProcessorTests",
                                                        CoveredStatements = "48",
                                                        TotalStatements = "48",
                                                        CoveragePercent = "100",
                                                    },
                                                    new NamespaceType
                                                    {
                                                        Name = "IntermediaryFactoryHarness",
                                                        CoveredStatements = "29",
                                                        TotalStatements = "29",
                                                        CoveragePercent = "100",
                                                    },
                                                    new NamespaceType
                                                    {
                                                        Name = "IntermediaryTransformerTests",
                                                        CoveredStatements = "16",
                                                        TotalStatements = "16",
                                                        CoveragePercent = "100",
                                                    },
                                                }
                                            },
                                            new Namespace
                                            {
                                                Name = "CapitalGroup.Afsd.Server.Infrastructure.UnitTest.Extensions",
                                                CoveredStatements = "261",
                                                TotalStatements = "261",
                                                CoveragePercent = "100",
                                                NamespaceTypes = new List<NamespaceType>
                                                {
                                                    new NamespaceType
                                                    {
                                                        Name = "NameParsingTests",
                                                        CoveredStatements = "248",
                                                        TotalStatements = "248",
                                                        CoveragePercent = "100",
                                                    },
                                                    new NamespaceType
                                                    {
                                                        Name = "StandardHeaderExtensionsTests",
                                                        CoveredStatements = "13",
                                                        TotalStatements = "13",
                                                        CoveragePercent = "100",
                                                    },
                                                }
                                            },
                                            new Namespace
                                            {
                                                Name = "CapitalGroup.Afsd.Server.Infrastructure.UnitTest.IpsAccess",
                                                CoveredStatements = "19",
                                                TotalStatements = "19",
                                                CoveragePercent = "100",
                                            },
                                            new Namespace
                                            {
                                                Name = "CapitalGroup.Afsd.Server.Infrastructure.UnitTest.Logging",
                                                CoveredStatements = "57",
                                                TotalStatements = "61",
                                                CoveragePercent = "93",
                                                NamespaceTypes = new List<NamespaceType>
                                                {
                                                    new NamespaceType
                                                    {
                                                        Name = "LoggingFilterAttributeTest",
                                                        CoveredStatements = "30",
                                                        TotalStatements = "30",
                                                        CoveragePercent = "100",
                                                    }
                                                }
                                            },

                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            };
            GenericSerializer.Serialize<CodeCoverage.CoverageDtos.Root>(root, cTempAceClientcoveragereportXml);

        }
    }
}
