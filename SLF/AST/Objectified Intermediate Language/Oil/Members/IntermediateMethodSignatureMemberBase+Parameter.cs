using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
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
            TSignature,
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TParent :
            IMethodSignatureParent<TSignature, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
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
    }
}
