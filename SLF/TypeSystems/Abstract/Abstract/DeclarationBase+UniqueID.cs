using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class DeclarationBase<TIdentifier>
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>
    {
        protected internal abstract class DeclarationUniqueIdentifierBase :
            IDeclarationUniqueIdentifier<TIdentifier>
        {
            private DeclarationBase<TIdentifier> source;

            protected DeclarationUniqueIdentifierBase(DeclarationBase<TIdentifier> source)
            {
                this.source = source;
            }

            #region IDeclarationUniqueIdentifier<TIdentifier> Members

            public string Name
            {
                get { return this.source.Name; }
            }

            #endregion

            #region IEquatable<TIdentifier> Members

            public virtual bool Equals(TIdentifier other)
            {
                if (this.GetType() == other.GetType())
                    return this.Name == other.Name;
                return false;
            }

            #endregion

            public override int GetHashCode()
            {
                return this.Name.GetHashCode();
            }
        }

        protected internal class GeneralDeclarationUniqueIdentifier :
            DeclarationUniqueIdentifierBase,
            IGeneralDeclarationUniqueIdentifier
        {
            public GeneralDeclarationUniqueIdentifier(DeclarationBase<TIdentifier> source)
                : base(source)
            {
            }

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is TIdentifier)
                    return this.Equals((TIdentifier)other);
                else
                    return false;
            }

            #endregion
        }
    }
}
