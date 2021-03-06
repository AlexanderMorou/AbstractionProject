using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.OldCodeGen.Statements;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    /// <summary>
    /// Defines properties and methods for working with a method member.
    /// </summary>
    public interface IMethodMember :
        IMethodSignatureMember<IMethodParameterMember, IMethodTypeParameterMember, CodeMemberMethod, IMemberParentType>,
        IBlockParent,
        IStatementBlockInsertBase,
        IImplementedMember,
        ICodeBodyTableMember
    {
        /// <summary>
        /// Returns the parameters for the <see cref="IMethodMember"/>.
        /// </summary>
        new IMethodParameterMembers Parameters { get; }
        /// <summary>
        /// Returns the type-parameters for the <see cref="IMethodMember"/>.
        /// </summary>
        new IMethodTypeParameterMembers TypeParameters { get; }
        new IMethodReferenceExpression GetReference();
        new IMethodReferenceExpression GetReference(IMemberParentExpression source);

    }
}
