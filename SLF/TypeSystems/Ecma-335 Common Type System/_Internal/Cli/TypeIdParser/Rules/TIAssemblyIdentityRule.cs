using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules
{
    internal class TIAssemblyIdentityRule :
        ITIAssemblyIdentityRule
    {
        internal TIAssemblyIdentityRule(string name, ITIVersionRule version, ICultureIdentifier culture, byte[] publicKeyToken)
        {
            this.Name = name;
            this.Version = version;
            this.CultureIdentifier = culture;
            this.PublicKeyToken = publicKeyToken;
        }

        #region ITIAssemblyIdentityRule Members

        public string Name { get; private set; }

        public ITIVersionRule Version { get; private set; }

        public ICultureIdentifier CultureIdentifier { get; private set; }

        public byte[] PublicKeyToken { get; private set; }

        #endregion

        public override string ToString()
        {
            if (this.Name.Contains('\\') || this.Name.Contains(' '))
                return string.Format("\"{0}\", Version={1}, Culture={2}, PublicKeyToken={3}", this.Name, this.Version, this.CultureIdentifier.Name == string.Empty ? "neutral" : CultureIdentifier.Name, PublicKeyToken == null ? "null" : this.PublicKeyToken.FormatHexadecimal());
            else
                return string.Format("{0}, Version={1}, Culture={2}, PublicKeyToken={3}", this.Name, this.Version, this.CultureIdentifier.Name == string.Empty ? "neutral" : CultureIdentifier.Name, PublicKeyToken == null ? "null" : this.PublicKeyToken.FormatHexadecimal());
        }
    }
}
