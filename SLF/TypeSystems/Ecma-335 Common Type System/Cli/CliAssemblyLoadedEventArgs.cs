using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Denotes
    /// </summary>
    public class CliAssemblyLoadedEventArgs :
        EventArgs
    {
        public ICliAssembly LoadedAssembly { get; internal set; }
        public IAssemblyUniqueIdentifier AssemblyIdentifier { get; internal set; }
        public string AssemblyLocation { get; internal set; }
    }
}
