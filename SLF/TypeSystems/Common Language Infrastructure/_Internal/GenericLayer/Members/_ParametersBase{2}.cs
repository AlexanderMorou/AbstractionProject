using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract partial class _ParametersBase<TParent, TParameter> :
        _MembersBase<TParent, TParameter, IParameterMemberDictionary<TParent, TParameter>>,
        IParameterMemberDictionary<TParent, TParameter>,
        IParameterMemberDictionary
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            class,
            IParameterMember<TParent>
    {
        private ParameterMemberDictionaryTypes<TParent, TParameter> parameterTypes;
        protected _ParametersBase(TParent parent, IParameterMemberDictionary<TParent, TParameter> original)
            : base(parent, original)
        {
        }

        #region IParameterMemberDictionary Members

        IParameterParent IParameterMemberDictionary.Parent
        {
            get {
                return base.Parent;
            }
        }

        #endregion

        #region IParameterMemberDictionary Members


        public ITypeCollectionBase ParameterTypes
        {
            get
            {
                if (this.parameterTypes == null)
                    this.parameterTypes = new ParameterMemberDictionaryTypes<TParent, TParameter>(this);
                return this.parameterTypes;
            }
        }

        #endregion
    }
}
