using System;
using System.Collections.Generic;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class LockedPropertyMembersBase<TProperty, TPropertyParent> :
        LockedGroupedMembersBase<TPropertyParent, IGeneralMemberUniqueIdentifier, TProperty, PropertyInfo>,
        IPropertyMemberDictionary<TProperty, TPropertyParent>,
        IPropertyMemberDictionary
        where TProperty :
            class,
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParent<TProperty, TPropertyParent>
    {

        public LockedPropertyMembersBase(LockedFullMembersBase master, TPropertyParent parent, IEnumerable<TProperty> series)
            : base(master, parent, series)
        {
        }

        public LockedPropertyMembersBase(LockedFullMembersBase master, TPropertyParent parent, PropertyInfo[] seriesData, Func<PropertyInfo, TProperty> funcImpl)
            : base(master, parent, seriesData, funcImpl, GetName)
        {
        }

        private static string GetName(PropertyInfo property)
        {
            return property.Name;
        }

        public LockedPropertyMembersBase(LockedFullMembersBase master, TPropertyParent parent)
            : base(master, parent)
        {
        }

        #region IPropertyMemberDictionary Members

        IPropertyParent IPropertyMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        protected override IGeneralMemberUniqueIdentifier FetchKey(PropertyInfo item)
        {
            return item.GetUniqueIdentifier();
        }

    }
}
