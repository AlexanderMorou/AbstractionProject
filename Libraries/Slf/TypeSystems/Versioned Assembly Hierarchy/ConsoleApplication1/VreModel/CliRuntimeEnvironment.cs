using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Vre;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Xml;

namespace MVATests.VreModel
{
    public class CliRuntimeEnvironment :
        VreXmlEnvironment<CliRuntimeEnvironment, CliRuntimeEnvironmentVersion, ICliManager>
    {
        protected override CliRuntimeEnvironmentVersion GetVersion(CliRuntimeEnvironmentVersion previousVersion, XmlElement derivedVersion)
        {
            List<string> previousHintPaths = new List<string>();
            CliFrameworkVersion version = GetVersion(CliRuntimeEnvironmentVersion.GetXmlVersionString(derivedVersion));

            //if (derivedVersion.Name != "vre:BreakingVersion")
            //{
                var currentVersion = previousVersion.InitialVersion;
                while (currentVersion != null)
                {
                    previousHintPaths.AddRange(currentVersion.HintPaths);
                    currentVersion = currentVersion.NextVersion;
                }
            //}
            previousHintPaths.AddRange(CliRuntimeEnvironmentVersion.GetHintPaths(derivedVersion, this.XmlNamespaceManager));
            return new CliRuntimeEnvironmentVersion(this, previousVersion, derivedVersion, CliGateway.CreateIdentityManager(CliFrameworkPlatform.AnyPlatform, version, true, false, false, ((IEnumerable<string>)previousHintPaths).Reverse().ToArray()));
        }

        private static CliFrameworkVersion GetVersion(string versionId)
        {
            CliFrameworkVersion version = CliFrameworkVersion.CurrentVersion;
            string id = versionId;
            var idMajor = id.Substring(1, 1);
            var idMinor = id.Substring(3, 1);
            switch (idMajor)
            {
                case "1":
                    if (idMinor == "0")
                        version = CliFrameworkVersion.v1_0_3705;
                    else
                        version = CliFrameworkVersion.v1_1_4322;
                    break;
                case "2":
                    version = CliFrameworkVersion.v2_0_50727;
                    break;
                case "3":
                    if (idMinor == "0")
                        version = CliFrameworkVersion.v3_0;
                    else
                        version = CliFrameworkVersion.v3_5;
                    break;
                case "4":
                    if (idMinor == "0")
                        version = CliFrameworkVersion.v4_0_30319;
                    else
                        version = CliFrameworkVersion.v4_5;
                    break;
            }
            return version;
        }

        protected override CliRuntimeEnvironmentVersion GetVersion(XmlElement xmlNode)
        {
            return new CliRuntimeEnvironmentVersion(this, xmlNode, CliGateway.CreateIdentityManager(CliFrameworkPlatform.AnyPlatform, CliFrameworkVersion.v1_0_3705, true, false, false, CliRuntimeEnvironmentVersion.GetHintPaths(xmlNode, this.XmlNamespaceManager).ToArray()));
        }

        internal CliRuntimeEnvironment(XmlDocument document, XmlNamespaceManager xmlNamespaceManager)
            : base(document, xmlNamespaceManager)
        {
        }
    }
}
