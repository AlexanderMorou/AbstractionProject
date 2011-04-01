using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal partial class LockedGenericTypeParameterConstructorMembers<TType> :
        LockedGroupedMembersBase<IGenericTypeParameter<TType>, IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, ConstructorInfo>,
        IGenericParameterConstructorMemberDictionary<IGenericTypeParameter<TType>>
        where TType :
            IGenericType<TType>
    {
        private ConstructorInfo[] seriesData;
        public LockedGenericTypeParameterConstructorMembers(LockedFullMembersBase master, ICompiledGenericTypeParameter<TType> parent, ConstructorInfo[] seriesData, Func<ConstructorInfo, IGenericParameterConstructorMember<IGenericTypeParameter<TType>>> fetchImpl)
            : base(master, parent, seriesData, fetchImpl)
        {
            this.seriesData = seriesData;
        }

        #region ICompiledGenericTypeGenericConstructorMembers<TType> Members

        public new ICompiledGenericTypeParameter<TType> Parent
        {
            get { return (ICompiledGenericTypeParameter<TType>)base.Parent; }
        }

        #endregion

        public override int Count
        {
            get
            {
                return this.seriesData.Length;
            }
        }

        public override IEnumerator<KeyValuePair<string, IGenericParameterConstructorMember<IGenericTypeParameter<TType>>>> GetEnumerator()
        {
            for (int i = 0; i < this.seriesData.Length; i++)
                yield return new KeyValuePair<string, IGenericParameterConstructorMember<IGenericTypeParameter<TType>>>(this.Keys[i], this.Values[i]);
            yield break;
        }

        #region ISignatureMemberDictionary<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>,IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>,IGenericTypeParameter<TType>>,IGenericTypeParameter<TType>> Members

        public IFilteredSignatureMemberDictionary<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>> Find(bool strict, ITypeCollection search)
        {
            return CLICommon.FindCache<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>>(this.Values, search, strict);
        }

        public IFilteredSignatureMemberDictionary<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>> Find(ITypeCollection search)
        {
            return this.Find(true, search);
        }

        public IFilteredSignatureMemberDictionary<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>> Find(bool strict, params IType[] search)
        {
            return CLICommon.FindCache<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>>(this.Values, search, strict);
        }

        public IFilteredSignatureMemberDictionary<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IConstructorParameterMember<IGenericParameterConstructorMember<IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>>, IGenericTypeParameter<TType>> Find(params IType[] search)
        {
            return this.Find(true, search);
        }

        #endregion

        protected override string FetchKey(ConstructorInfo item)
        {
            return item.GetUniqueIdentifier();
        }
    }
}
