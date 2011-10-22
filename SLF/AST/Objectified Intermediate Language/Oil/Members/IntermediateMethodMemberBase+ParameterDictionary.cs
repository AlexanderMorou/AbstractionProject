using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateMethodMemberBase<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>
        where TMethod :
            class,
            IMethodMember<TMethod, TMethodParent>
        where TIntermediateMethod :
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            TMethod
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
        where TIntermediateMethodParent :
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            TMethodParent
    {
        /// <summary>
        /// Initializes the <see cref="IntermediateParameterParentMemberBase{TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}.Parameters"/> property.
        /// </summary>
        /// <returns>An instance of <see cref="ParameterDictionary"/>.</returns>
        protected override IntermediateParameterMemberDictionary<TMethod, TIntermediateMethod, IMethodParameterMember<TMethod, TMethodParent>, IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>> InitializeParameters()
        {
            return new ParameterDictionary(((TIntermediateMethod)((object)(this))));
        }

        /// <summary>
        /// Provides a dictionary for the parameters for the intermediate
        /// method member.
        /// </summary>
        protected class ParameterDictionary :
            IntermediateParameterMemberDictionary<TMethod, TIntermediateMethod, IMethodParameterMember<TMethod, TMethodParent>, IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>>
        {
            /// <summary>
            /// Creates a new <see cref="ParameterDictionary"/> with the 
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateMethod"/> which owns the
            /// <see cref="ParameterDictionary"/>.</param>
            public ParameterDictionary(TIntermediateMethod parent)
                : base(parent)
            {
            }

            protected override IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> GetNewParameter(string name, IType parameterType, ParameterDirection direction)
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
