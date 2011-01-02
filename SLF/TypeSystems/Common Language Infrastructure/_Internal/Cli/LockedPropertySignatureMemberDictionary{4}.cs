using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Reflection;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class LockedPropertySignatureMemberDictionary<TProperty, TPropertyParent> :
        LockedGroupedMembersBase<TPropertyParent, TProperty, PropertyInfo>,
        IPropertySignatureMemberDictionary<TProperty, TPropertyParent>,
        IPropertySignatureMemberDictionary
        where TProperty :
            class,
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertySignatureParentType<TProperty, TPropertyParent>
    {
        internal LockedPropertySignatureMemberDictionary(LockedFullMembersBase master, TPropertyParent parent, PropertyInfo[] properties, Func<PropertyInfo, TProperty> fetchImpl)
            : base(master, parent, properties, fetchImpl)
        {
        }
        protected override string FetchKey(PropertyInfo item)
        {
            return item.GetUniqueIdentifier();
        }

    }
}
