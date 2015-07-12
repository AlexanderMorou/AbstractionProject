using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using MVATests.VreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVATests
{
    class TypeVersionDetails
    {
        public Dictionary<IMemberUniqueIdentifier, Tuple<IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>[]> Members { get; set; }

        public Dictionary<CliRuntimeEnvironmentVersion, IType> Versions { get; set; }
    }
}
