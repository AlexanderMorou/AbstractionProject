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
    internal class LockedFieldMembersBase<TField, TFieldParent> :
        LockedGroupedMembersBase<TFieldParent, IGeneralMemberUniqueIdentifier, TField, FieldInfo>,
        IFieldMemberDictionary<TField, TFieldParent>,
        IFieldMemberDictionary
        where TField :
            class,
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {
        public LockedFieldMembersBase(LockedFullMembersBase master, TFieldParent parent, IEnumerable<TField> series)
            : base(master, parent, series)
        {
        }

        public LockedFieldMembersBase(LockedFullMembersBase master, TFieldParent parent, FieldInfo[] sourceData, Func<FieldInfo, TField> fetchImpl)
            : base(master, parent, sourceData, fetchImpl, GetName)
        {
        }

        private static string GetName(FieldInfo field)
        {
            return field.Name;
        }

        public LockedFieldMembersBase(LockedFullMembersBase master, TFieldParent parent)
            : base(master, parent) { }

        #region IFieldMemberDictionary Members

        IFieldParent IFieldMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        protected override IGeneralMemberUniqueIdentifier FetchKey(FieldInfo item)
        {
            return item.GetUniqueIdentifier();
        }


    }
}
