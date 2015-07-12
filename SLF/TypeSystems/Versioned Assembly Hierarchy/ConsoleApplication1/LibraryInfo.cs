using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVATests
{
    class LibraryInfo
    {
        public string Name { get; set; }

        public Dictionary<VreModel.CliRuntimeEnvironmentVersion, AllenCopeland.Abstraction.Slf.Abstract.IAssembly> Versions { get; set; }

        public VreModel.CliRuntimeEnvironmentVersion FirstVersion { get; set; }
    }
}
