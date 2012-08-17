using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Utilities;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal partial class _ConstructorBase<TCtor, TCtorParent> :
        _MemberBase<IGeneralSignatureMemberUniqueIdentifier, TCtor, TCtorParent>,
        IConstructorMember<TCtor, TCtorParent>,
        IGeneralDeclarationsParent<TCtor, IConstructorParameterMember<TCtor, TCtorParent>>
        where TCtor :
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent :
            ICreatableParent<TCtor, TCtorParent>
    {
        private _Parameters parameters;
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
        protected _ConstructorBase(TCtor original, TCtorParent adjustedParent)
            : base(original, adjustedParent)
        {

        }

        #region IParameterParent<TCtor,IConstructorParameterMember<TCtor,TCtorParent>> Members

        public IParameterMemberDictionary<TCtor, IConstructorParameterMember<TCtor, TCtorParent>> Parameters
        {
            get
            {
                if (this.parameters == null)
                    this.parameters = this.InitializeParameters();
                return this.parameters;
            }
        }

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get {
                return (IParameterMemberDictionary)this.parameters;
            }
        }

        public bool LastIsParams
        {
            get { return this.Original.LastIsParams; }
        }

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return this.Original.AccessLevel; }
        }

        #endregion

        private _Parameters InitializeParameters()
        {
            return new _Parameters(this, this.Original.Parameters);
        }

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = AstIdentifier.GetSignatureIdentifier(".ctor", this.Parameters.ParameterTypes.ToArray());
                return this.uniqueIdentifier;
            }
        }

        TCtor IGeneralDeclarationsParent<TCtor, IConstructorParameterMember<TCtor, TCtorParent>>.Original
        {
            get
            {
                return this.Original;
            }
        }

        public override string ToString()
        {
            return this.UniqueIdentifier.ToString();
        }

    }
}
