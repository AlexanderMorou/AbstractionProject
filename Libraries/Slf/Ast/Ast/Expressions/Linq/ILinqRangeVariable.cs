using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a range
    /// variable within a language integrated query.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true,
                                                         YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateMemberVisitor")]
#endif
    public interface ILinqRangeVariable :
        IIntermediateMember<IGeneralMemberUniqueIdentifier, ILinqRangeVariableParent, ILinqRangeVariableParent>,
        IIntermediateMember
    {
        /// <summary>
        /// Returns the <see cref="ILinqRangeVariableParent"/> which defined the
        /// <see cref="ILinqRangeVariable"/>.
        /// </summary>
        new ILinqRangeVariableParent Parent { get; }
        /// <summary>
        /// Obtains a <see cref="ILinqRangeVariableReference"/> which
        /// refers back to the <see cref="ILinqRangeVariable"/>
        /// as an expression.
        /// </summary>
        /// <returns>A <see cref="ILinqRangeVariableReference"/>
        /// associated to the current <see cref="ILinqRangeVariable"/>.</returns>
        ILinqRangeVariableReference GetReference();
    }
    public interface ILinqRangeVariableParent :
        IIntermediateMemberParent
    {
    }
}
