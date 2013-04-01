using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateDelegateType
    {
        /// <summary>
        /// Provides a parameter member for a delegate type.
        /// </summary>
        protected internal sealed class ParameterMember :
            IntermediateParameterMemberBase<IDelegateType, IIntermediateDelegateType, IDelegateTypeParameterMember, IIntermediateDelegateTypeParameterMember>,
            IIntermediateDelegateTypeParameterMember
        {
            internal ParameterMember(IntermediateDelegateType parent)
                : base(parent, parent.IdentityManager)
            {

            }

        }
        partial class TypeParameterDictionary
        {
            protected new internal sealed class TypeParameter :
                IntermediateGenericTypeBase<IGeneralGenericTypeUniqueIdentifier, IDelegateType, IIntermediateDelegateType>.TypeParameterDictionary.TypeParameter,
                IIntermediateDelegateTypeParameterType
            {
                /// <summary>
                /// Creates a new <see cref="TypeParameter"/> instance
                /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
                /// </summary>
                /// <param name="name">The <see cref="String"/> representing the unique
                /// name of the <see cref="TypeParameter"/>.</param>
                /// <param name="parent">The <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/> 
                /// which contains the <see cref="TypeParameter"/>.</param>
                public TypeParameter(string name, IntermediateDelegateType parent)
                    : base(name, parent)
                {
                }
            }
        }
    }
}
