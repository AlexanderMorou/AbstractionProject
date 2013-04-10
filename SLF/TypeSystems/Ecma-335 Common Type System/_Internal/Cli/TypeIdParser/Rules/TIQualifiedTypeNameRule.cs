using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules
{
    internal class TIQualifiedTypeNameRule :
        ITIQualifiedTypeNameRule
    {
        public TIQualifiedTypeNameRule(ITITypeIdentityRule typeIdentifier)
        {
            this.TypeIdentifier = typeIdentifier;
        }
        public TIQualifiedTypeNameRule(ITITypeIdentityRule typeIdentifier, ITIAssemblyIdentityRule assemblyIdentifier)
        {
            this.TypeIdentifier = typeIdentifier;
            this.AssemblyIdentifier = assemblyIdentifier;
        }
        #region ITIQualifiedTypeNameRule Members

        public ITITypeIdentityRule TypeIdentifier { get; private set; }

        public ITIAssemblyIdentityRule AssemblyIdentifier { get; private set; }

        #endregion

        public override string ToString()
        {
            if (this.AssemblyIdentifier != null)
                return string.Format("{0}, {1}", this.TypeIdentifier, this.AssemblyIdentifier);
            else
                return this.TypeIdentifier.ToString();
        }
    }
}
