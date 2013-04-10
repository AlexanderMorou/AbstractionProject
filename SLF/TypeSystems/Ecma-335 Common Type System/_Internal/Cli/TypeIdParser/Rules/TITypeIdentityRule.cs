using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules
{
    internal class TITypeIdentityRule :
        ITITypeIdentityRule
    {

        public TITypeIdentityRule(IEnumerable<string> names, string @namespace)
        {
            this.Names = names;
            this.Namespace = @namespace;
        }

        public TITypeIdentityRule(IEnumerable<string> names, string @namespace, int typeParameterCount)
            : this(names, @namespace)
        {
            this.TypeParameterCount = typeParameterCount;
        }

        public TITypeIdentityRule(IEnumerable<string> names, string @namespace, int typeParameterCount, IEnumerable<ITIElementClassificationRule> elementClassifications)
            : this(names, @namespace, typeParameterCount)
        {
            this.ElementClassifications = elementClassifications;
        }

        public TITypeIdentityRule(IEnumerable<string> names, string @namespace, int typeParameterCount, IEnumerable<ITITypeParameterIdentityRule> typeReplacements)
            : this(names, @namespace, typeParameterCount)
        {
            this.TypeReplacements = typeReplacements;
        }

        public TITypeIdentityRule(IEnumerable<string> names, string @namespace, int typeParameterCount, IEnumerable<ITITypeParameterIdentityRule> typeReplacements, IEnumerable<ITIElementClassificationRule> elementClassifications)
            : this(names, @namespace, typeParameterCount, typeReplacements)
        {
            this.ElementClassifications = elementClassifications;
        }

        #region ITITypeIdentityRule Members

        public string Namespace { get; private set; }

        public IEnumerable<string> Names { get; private set; }

        public bool HasTypeReplacements { get { return this.TypeReplacements != null; } }

        public IEnumerable<ITITypeParameterIdentityRule> TypeReplacements { get; private set; }

        public bool HasElementClassifications
        {
            get
            {
                return this.ElementClassifications != null;
            }
        }

        public IEnumerable<ITIElementClassificationRule> ElementClassifications { get; private set; }

        public int TypeParameterCount { get; private set; }

        #endregion

        public override string ToString()
        {
            if (this.HasElementClassifications)
                if (this.HasTypeReplacements)
                    return string.Format("{0}.{1}[{2}]{3}", this.Namespace, string.Join("+", this.Names), string.Join(",", this.TypeReplacements), string.Join(string.Empty, this.ElementClassifications));
                else
                    return string.Format("{0}.{1}{2}", this.Namespace, string.Join("+", this.Names), string.Join(string.Empty, this.ElementClassifications));
            else if (this.HasTypeReplacements)
                return string.Format("{0}.{1}[{2}]", this.Namespace, string.Join("+", this.Names), string.Join(",", this.TypeReplacements));
            return string.Format("{0}.{1}", this.Namespace, string.Join("+", this.Names));
        }
    }
}
