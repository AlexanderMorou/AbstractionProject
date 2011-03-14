using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class LockedGenericParameterPropertyMembers<TGenericParameter> :
        LockedPropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterPropertyMemberDictionary<TGenericParameter>,
        IGenericParameterPropertyMemberDictionary
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        internal LockedGenericParameterPropertyMembers(LockedFullMembersBase master, TGenericParameter parent, PropertyInfo[] properties, Func<PropertyInfo, IGenericParameterPropertyMember<TGenericParameter>> fetchImpl)
            : base(master, parent, properties, fetchImpl)
        {
        }


        #region IGenericParameterPropertyMemberDictionary Members

        public new IGenericParameter Parent
        {
            get { return base.Parent; }
        }

        #endregion

    }
}
