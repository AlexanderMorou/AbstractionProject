using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateMethodSignatureMemberBase<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignature, TParent>
        where TIntermediateSignature :
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignature
        where TParent :
            IMethodSignatureParent<TSignature, TParent>
        where TIntermediateParent :
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TParent
    {
        /// <summary>
        /// Provides a base class for the intermediate method signature member parameters to derive from.
        /// </summary>
        protected new class ParameterMember :
            IntermediateMethodSignatureMemberBase<IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>, TSignature, TIntermediateSignature, TParent, TIntermediateParent>.ParameterMember,
            IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        {
            /// <summary>
            /// Creates a new <see cref="ParameterMember"/> with the 
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateSignature"/>
            /// which owns the <see cref="ParameterMember"/>.</param>
            public ParameterMember(TIntermediateSignature parent)
                : base(parent)
            {
            }
        }

        protected class ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter> :
            IntermediateMethodSignatureMemberBase<IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>, TSignature, TIntermediateSignature, TParent, TIntermediateParent>.ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter>>,
            IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            where TAltParent :
                IParameterParent<TAltParent, TAltParameter>
            where TIntermediateAltParent :
                IIntermediateParameterParent<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter>,
                TAltParent
            where TAltParameter :
                IParameterMember<TAltParent>
            where TIntermediateAltParameter :
                class,
                IIntermediateParameterMember<TAltParent, TIntermediateAltParent>,
                TAltParameter
        {
            internal ParameterMember(TIntermediateAltParameter original, TIntermediateSignature parent)
                : base(original, parent)
            {
                if (original == null)
                    throw new ArgumentNullException("original");
            }
        }
    }
    partial class IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TParent :
            ISignatureParent<TSignature, TSignatureParameter, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>
    {
        /// <summary>
        /// Provides a base for the method signature parameter members to derive from.
        /// </summary>
        protected abstract class ParameterMember :
            IntermediateSignatureParameterMemberBase<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        {
            /// <summary>
            /// Creates a new <see cref="ParameterMember"/> with the 
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateSignature"/>
            /// which owns the <see cref="ParameterMember"/>.</param>
            protected ParameterMember(TIntermediateSignature parent)
                : base(parent)
            {
            }

            #region IMethodSignatureParameterMember Members

            IMethodSignatureMember IMethodSignatureParameterMember.Parent
            {
                get { return this.Parent; }
            }

            #endregion
        }

        protected abstract class ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter> :
            ParameterMember,
            ParameterDictionary<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter>.IWrapperParameter
            where TAltParent :
                IParameterParent<TAltParent, TAltParameter>
            where TIntermediateAltParent :
                IIntermediateParameterParent<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter>,
                TAltParent
            where TAltParameter :
                IParameterMember<TAltParent>
            where TIntermediateAltParameter :
                class,
                IIntermediateParameterMember<TAltParent, TIntermediateAltParent>,
                TAltParameter
            where TWrapperParameter :
                ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter>,
                TIntermediateSignatureParameter,
                ParameterDictionary<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter>.IWrapperParameter
        {

            public ParameterMember(TIntermediateAltParameter original, TIntermediateSignature parent)
                : base(parent)
            {
                this.AlternateParameter = original;
            }

            #region IWrapperParameter Members

            public TIntermediateAltParameter AlternateParameter { get; private set; }

            #endregion

            public override IType ParameterType
            {
                get
                {
                    return this.AlternateParameter.ParameterType;
                }
                set
                {
                    this.AlternateParameter.ParameterType = value;
                }
            }
        }
    }
}
