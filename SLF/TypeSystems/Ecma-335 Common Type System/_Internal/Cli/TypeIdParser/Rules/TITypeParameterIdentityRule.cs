using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules
{
    internal class TITypeParameterIdentityRule :
        ITITypeParameterIdentityRule
    {
        public TITypeParameterIdentityRule(ITIQualifiedTypeNameRule typeIdentity) { this.TypeIdentity = typeIdentity; }

        #region ITITypeParameterIdentityRule Members

        public ITIQualifiedTypeNameRule TypeIdentity { get; private set; }

        #endregion

        public override string ToString()
        {
            return string.Format("[{0}]", this.TypeIdentity);
        }
    }
}
