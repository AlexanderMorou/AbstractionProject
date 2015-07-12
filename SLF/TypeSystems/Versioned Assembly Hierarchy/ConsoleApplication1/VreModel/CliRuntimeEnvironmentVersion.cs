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
    public class CliRuntimeEnvironmentVersion :
        VreXmlEnvironmentVersion<CliRuntimeEnvironment, CliRuntimeEnvironmentVersion, ICliManager>
    {
        internal CliRuntimeEnvironmentVersion(CliRuntimeEnvironment environment, XmlElement xmlNode, ICliManager identityManager)
            : base(environment, xmlNode, identityManager)
        {
        }

        internal CliRuntimeEnvironmentVersion(CliRuntimeEnvironment environment, CliRuntimeEnvironmentVersion previousVersion, XmlElement derivedVersion, ICliManager identityManager)
            : base(environment, previousVersion, derivedVersion, identityManager)
        {
        }
    }
}
