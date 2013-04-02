using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.OldCodeGen.Statements;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    /// <summary>
    /// Defines properties and methods for working with a constructor on a creatable
    /// type.
    /// </summary>
    public interface IConstructorMember :
        IParameteredDeclaration<IConstructorParameterMember, CodeConstructor, IMemberParentType>,
        IMember<IMemberParentType, CodeConstructor>,
        IBlockParent,
        IInvokableMember,
        IAutoCommentMember
    {
        /// <summary>
        /// Returns/sets the target of the cascade expressions which is invoked
        /// before the constructor body.
        /// </summary>
        ConstructorCascadeTarget CascadeExpressionsTarget { get; set; }
        /// <summary>
        /// Returns the parameters of the <see cref="IConstructorMember"/>.
        /// </summary>
        new IConstructorParameterMembers Parameters { get; }
        /// <summary>
        /// Returns the cascade parameter expressions for the <see cref="IConstructorMember"/>.
        /// </summary>
        IExpressionCollection CascadeMembers { get; }
    }
}
