using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal partial class LockedGenericTypeParameterConstructorMembers<TTypeIdentifier, TType> :
        LockedGroupedMembersBase<IGenericTypeParameter<TTypeIdentifier, TType>, IGeneralSignatureMemberUniqueIdentifier, IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, ConstructorInfo>,
        IGenericParameterConstructorMemberDictionary<IGenericTypeParameter<TTypeIdentifier, TType>>
        where TTypeIdentifier : 
            IGenericTypeUniqueIdentifier
        where TType :
            IGenericType<TTypeIdentifier, TType>
    {
        private ConstructorInfo[] seriesData;
        public LockedGenericTypeParameterConstructorMembers(LockedFullMembersBase master, ICompiledGenericTypeParameter<TTypeIdentifier, TType> parent, ConstructorInfo[] seriesData, Func<ConstructorInfo, IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>> fetchImpl)
            : base(master, parent, seriesData, fetchImpl, GetName)
        {
            this.seriesData = seriesData;
        }

        private static string GetName(ConstructorInfo target)
        {
            //Constructors aren't called via their name, calling convention is specialized.
            return null;
        }

        #region ICompiledGenericTypeGenericConstructorMembers<TTypeIdentifier, TType> Members

        public new ICompiledGenericTypeParameter<TTypeIdentifier, TType> Parent
        {
            get { return (ICompiledGenericTypeParameter<TTypeIdentifier, TType>)base.Parent; }
        }

        #endregion

        public override int Count
        {
            get
            {
                return this.seriesData.Length;
            }
        }

        public override IEnumerator<KeyValuePair<IGeneralSignatureMemberUniqueIdentifier, IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>>> GetEnumerator()
        {
            for (int i = 0; i < this.seriesData.Length; i++)
                yield return new KeyValuePair<IGeneralSignatureMemberUniqueIdentifier, IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>>(this.Keys[i], this.Values[i]);
            yield break;
        }

        #region ISignatureMemberDictionary<IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>,IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>,IGenericTypeParameter<TTypeIdentifier, TType>>,IGenericTypeParameter<TTypeIdentifier, TType>> Members

        public IFilteredSignatureMemberDictionary<IGeneralSignatureMemberUniqueIdentifier, IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>> Find(bool strict, ITypeCollectionBase search)
        {
            return CliCommon.FindCache<IGeneralSignatureMemberUniqueIdentifier, IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>>(this.Values, search, strict);
        }

        public IFilteredSignatureMemberDictionary<IGeneralSignatureMemberUniqueIdentifier, IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>> Find(ITypeCollectionBase search)
        {
            return this.Find(true, search);
        }

        public IFilteredSignatureMemberDictionary<IGeneralSignatureMemberUniqueIdentifier, IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>> Find(bool strict, params IType[] search)
        {
            return CliCommon.FindCache<IGeneralSignatureMemberUniqueIdentifier, IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>>(this.Values, search, strict);
        }

        public IFilteredSignatureMemberDictionary<IGeneralSignatureMemberUniqueIdentifier, IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>>, IGenericTypeParameter<TTypeIdentifier, TType>> Find(params IType[] search)
        {
            return this.Find(true, search);
        }

        #endregion

        protected override IGeneralSignatureMemberUniqueIdentifier FetchKey(ConstructorInfo item)
        {
            return item.GetUniqueIdentifier(this.Parent.Manager);
        }
    }
}
