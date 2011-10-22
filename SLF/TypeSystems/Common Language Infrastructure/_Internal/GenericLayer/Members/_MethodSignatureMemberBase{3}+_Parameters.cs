using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _MethodSignatureMemberBase<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParameter :
            class,
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        protected abstract class _Parameters :
            _ParametersBase<TSignature, TSignatureParameter>
        {

            protected _Parameters(_MethodSignatureMemberBase<TSignatureParameter, TSignature, TSignatureParent> parent, IParameterMemberDictionary<TSignature, TSignatureParameter> original)
                : base(((TSignature)((object)(parent))), original)
            {

            }
            protected abstract new class _Parameter :
                _ParametersBase<TSignature, TSignatureParameter>._Parameter,
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            {
                protected _Parameter(TSignatureParameter original, TSignature parent)
                    : base(original, parent)
                {
                }
                #region ISignatureParameterMember Members

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }

                #endregion

                protected override IType ParameterTypeImpl
                {
                    get
                    {
                        if (this.Parent.Parent is IGenericType)
                        {
                            IGenericType parent = ((IGenericType)(this.Parent.Parent));
                            if (parent.IsGenericConstruct)
                            {
                                if (!parent.IsGenericDefinition)
                                    if (this.Parent.IsGenericConstruct)
                                        if (!this.Parent.IsGenericDefinition)
                                            return this.Original.ParameterType.Disambiguify(parent.GenericParameters, this.Parent.GenericParameters, TypeParameterSources.Both);
                                        else
                                            return this.Original.ParameterType.Disambiguify(parent.GenericParameters, null, TypeParameterSources.Type);
                                    else
                                        return this.Original.ParameterType.Disambiguify(parent.GenericParameters, null, TypeParameterSources.Type);
                            }
                            else if (this.Parent.IsGenericConstruct && this.Parent.GenericParameters != null)
                                return this.Original.ParameterType.Disambiguify(null, this.Parent.GenericParameters, TypeParameterSources.Method);
                        }
                        else if (this.Parent.IsGenericConstruct && this.Parent.GenericParameters != null)
                            return this.Original.ParameterType.Disambiguify(null, this.Parent.GenericParameters, TypeParameterSources.Method);
                        return this.Original.ParameterType;
                    }
                }

                #region IMethodSignatureParameterMember Members
                IMethodSignatureMember IMethodSignatureParameterMember.Parent
                {
                    get
                    {
                        return this.Parent;
                    }
                }
                #endregion

            }

        }
    }
}
