using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal partial class _ConstructorBase<TCtor, TType> :
        _MemberBase<TCtor, TType>,
        IConstructorMember<TCtor, TType>,
        IGeneralDeclarationsParent<TCtor, IConstructorParameterMember<TCtor, TType>>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TType :
            ICreatableType<TCtor, TType>
    {
        private _Parameters parameters;
        protected _ConstructorBase(TCtor original, TType adjustedParent)
            : base(original, adjustedParent)
        {

        }

        #region IParameterParent<TCtor,IConstructorParameterMember<TCtor,TType>> Members

        public IParameterMemberDictionary<TCtor, IConstructorParameterMember<TCtor, TType>> Parameters
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

        public override string UniqueIdentifier
        {
            get
            {
                return string.Format(MemberExtensions.GetUniqueIdentifier<TCtor, IConstructorParameterMember<TCtor, TType>>((TCtor)(object)this), ".ctor");
            }
        }

        TCtor IGeneralDeclarationsParent<TCtor, IConstructorParameterMember<TCtor, TType>>.Original
        {
            get
            {
                return this.Original;
            }
        }

        public override string ToString()
        {
            return this.UniqueIdentifier;
        }

    }
}
