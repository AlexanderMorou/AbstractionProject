using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal partial class _DelegateTypeBase :
        _GenericTypeBase<IGeneralGenericTypeUniqueIdentifier, IDelegateType>,
        IDelegateType,
        IGeneralDeclarationsParent<IDelegateType, IDelegateTypeParameterMember>
    {
        private _Parameters parameters;
        private _FullMembersBase members;
        internal _DelegateTypeBase(IDelegateType original, IControlledTypeCollection genericParameters)
            : base(original, genericParameters)
        {

        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Delegate; }
        }

        #region IDelegateType Members

        public IType ReturnType
        {
            get {
                return Original.ReturnType.Disambiguify(this.GenericParameters, null, TypeParameterSources.Type);
            }
        }

        public IDelegateTypeParameterDictionary Parameters
        {
            get
            {
                CheckParameters();
                return this.parameters;
            }
        }

        private void CheckParameters()
        {
            if (this.parameters == null)
                this.parameters = this.InitializeParameters();
        }

        private _Parameters InitializeParameters()
        {
            return new _Parameters(this, this.Original.Parameters);
        }

        #endregion

        #region IParameterParent<IDelegateType,IDelegateTypeParameterMember> Members

        IParameterMemberDictionary<IDelegateType, IDelegateTypeParameterMember> IParameterParent<IDelegateType, IDelegateTypeParameterMember>.Parameters
        {
            get { return this.Parameters; }
        }

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return ((IParameterMemberDictionary)(this.Parameters)); }
        }

        public bool LastIsParams
        {
            get { return Original.LastIsParams; }
        }

        #endregion

        #region IGeneralDeclarationsParent<IDelegateType,IDelegateTypeParameterMember> Members

        IDelegateType IGeneralDeclarationsParent<IDelegateType,IDelegateTypeParameterMember>.Original
        {
            get { return this.Original; }
        }

        #endregion

        protected override IFullMemberDictionary OnGetMembers()
        {
            if (this.members == null)
                this.members = new _FullMembersBase();
            return this.members;
        }

        protected override IGeneralGenericTypeUniqueIdentifier OnGetUniqueIdentifier()
        {
            return this.Original.UniqueIdentifier;
        }
    }
}
