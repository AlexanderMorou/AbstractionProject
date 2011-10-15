using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class MemberBase<TIdentifier, TParent>
        where TIdentifier :
            IMemberUniqueIdentifier<TIdentifier>
        where TParent :
            IMemberParent
    {
        protected class GeneralMemberUniqueIdentifier :
            GeneralDeclarationUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        {
            public GeneralMemberUniqueIdentifier(MemberBase<TIdentifier, TParent> source)
                : base(source)
            {
            }

            #region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is TIdentifier)
                    return this.Equals((TIdentifier)other);
                return false;
            }

            #endregion
        }
    }
}
