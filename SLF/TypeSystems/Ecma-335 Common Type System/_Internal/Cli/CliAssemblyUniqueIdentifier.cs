using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    [DebuggerDisplay("{ToString(),nq}, Framework={GetTargetFrameworkString(),nq}")]
    internal class CliAssemblyUniqueIdentifier :
        TypeSystemIdentifiers.DefaultAssemblyUniqueIdentifier,
        ICliAssemblyUniqueIdentifier
    {

        public CliAssemblyUniqueIdentifier(IAssemblyUniqueIdentifier uniqueIdentifier, CliFrameworkVersion targetFramework)
            : base(uniqueIdentifier.Name, uniqueIdentifier.Version, uniqueIdentifier.Culture, uniqueIdentifier.PublicKeyToken)
        {
            this.TargetFramework = targetFramework;
        }
        public override bool Equals(IAssemblyUniqueIdentifier other)
        {
            bool otherIs;
            return base.Equals(other) &&
                (
                    (
                        otherIs = (other is CliAssemblyUniqueIdentifier) &&
                        ((CliAssemblyUniqueIdentifier)other).TargetFramework == this.TargetFramework
                    ) ||
                    !otherIs
                );
        }

        public CliFrameworkVersion TargetFramework { get; private set; }

        private string GetTargetFrameworkString()
        {
            return CliCommon.GetFrameworkStringFromVersion(this.TargetFramework);
        }
    }
}
