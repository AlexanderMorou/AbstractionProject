using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
        /// Initializes the <see cref="Parameters"/> property.
        /// </summary>
        /// <returns>An instance of <see cref="ParameterDictionary"/>.</returns>
        protected override IntermediateParameterMemberDictionary<TSignature, TIntermediateSignature, IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>> InitializeParameters()
        {
            return new ParameterDictionary(((TIntermediateSignature)(object)(this)));
        }
        /// <summary>
        /// Provides a dictionary for the parameters of the 
        /// <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.
        /// </summary>
        protected class ParameterDictionary :
            IntermediateParameterMemberDictionary<TSignature, TIntermediateSignature, IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>>
        {
            /// <summary>
            /// Creates a new <see cref="ParameterDictionary"/> with the 
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateSignature"/> which owns
            /// the <see cref="ParameterDictionary"/>.</param>
            public ParameterDictionary(TIntermediateSignature parent)
                : base(parent)
            {
            }
            /// <summary>
            /// Obtains a <see cref="ParameterMember"/> 
            /// for insertion into the <see cref="ParameterDictionary"/>.
            /// </summary>
            /// <param name="name">The name of the parameter to create.</param>
            /// <param name="parameterType">The type of the parameter to create.</param>
            /// <param name="direction">The direction in which the <see cref="ParameterMember"/>
            /// is coerced.</param>
            /// <returns>A new <see cref="ParameterMember"/> instance.</returns>
            protected override IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> GetNewParameter(string name, IType parameterType, ParameterDirection direction)
            {
                ParameterMember result = new ParameterMember(Parent);
                result.Direction = direction;
                result.ParameterType = parameterType;
                result.Name = name;
                return result;
            }
        }
    }
}
