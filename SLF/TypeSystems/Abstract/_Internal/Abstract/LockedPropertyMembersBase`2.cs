using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class LockedPropertyMembersBase<TProperty, TPropertyParent> :
        LockedGroupedMembersBase<TPropertyParent, TProperty, PropertyInfo>,
        IPropertyMemberDictionary<TProperty, TPropertyParent>,
        IPropertyMemberDictionary
        where TProperty :
            class,
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParentType<TProperty, TPropertyParent>
    {

        public LockedPropertyMembersBase(LockedFullMembersBase master, TPropertyParent parent, IEnumerable<TProperty> series)
            : base(master, parent, series)
        {
        }

        public LockedPropertyMembersBase(LockedFullMembersBase master, TPropertyParent parent, PropertyInfo[] seriesData, Func<PropertyInfo, TProperty> funcImpl)
            : base(master, parent, seriesData, funcImpl)
        {
        }

        public LockedPropertyMembersBase(LockedFullMembersBase master, TPropertyParent parent)
            : base(master, parent)
        {
        }

        #region IPropertyMemberDictionary Members

        IPropertyParentType IPropertyMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        protected override string FetchKey(PropertyInfo item)
        {
            return item.GetUniqueIdentifier();
        }

    }
}
