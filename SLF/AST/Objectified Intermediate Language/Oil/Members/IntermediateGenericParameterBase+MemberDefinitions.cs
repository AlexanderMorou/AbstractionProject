using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateGenericParameterBase<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TIntermediateGenericParameter :
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TGenericParameter
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
        where TIntermediateParent :
            IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TParent
    {
        /// <summary>
        /// Provides a constructor member for the <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        protected class ConstructorMember :
            IntermediateConstructorSignatureMemberBase<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>
        {
            /// <summary>
            /// Creates a new <see cref="ConstructorMember"/> with the
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateGenericParameter"/>
            /// in which the <see cref="ConstructorMember"/> is contained.</param>
            public ConstructorMember(TIntermediateGenericParameter parent)
                : base(parent)
            {
            }

            /// <summary>
            /// Returns <see cref="AccessLevelModifiers.Public"/>;
            /// additionally,
            /// </summary>
            /// <exception cref="System.InvalidOperationException">The acces level cannot be set on generic parameter
            /// constructors; occurs when set through <see cref="IIntermediateScopedDeclaration.AccessLevel"/>.</exception>
            public override AccessLevelModifiers AccessLevel
            {
                get
                {
                    return AccessLevelModifiers.Public;
                }
                set
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
