using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _FieldMembersBase<TField, TFieldParent> :
        _GroupedMembersBase<TFieldParent, IGeneralMemberUniqueIdentifier, TField, IFieldMemberDictionary<TField, TFieldParent>>,
        IFieldMemberDictionary<TField, TFieldParent>,
        IFieldMemberDictionary
        where TField :
            class,
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            class,
            IFieldParent<TField, TFieldParent>
    {
        protected _FieldMembersBase(_FullMembersBase master, IFieldMemberDictionary<TField, TFieldParent> originalSet, TFieldParent parent)
            : base(master, originalSet, parent)
        {
        }

        #region IFieldMemberDictionary Members

        IFieldParent IFieldMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion
    }
}
