using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate parameter
    /// with the <typeparamref name="TIntermediateParent"/> as its owner.
    /// </summary>
    /// <typeparam name="TParent">The <see cref="IParameterParent"/>
    /// type which owns the abstract definition of the current 
    /// <see cref="IIntermediateParameterMember{TParent, TIntermediateParent}"/>.</typeparam>
    /// <typeparam name="TIntermediateParent">The <see cref="IIntermediateParameterParent"/>
    /// which owns the current <see cref="IIntermediateParameterMember{TParent, TIntermediateParent}"/>.</typeparam>
#if DEBUG
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true,
                                                         YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateMemberVisitor")]
#endif
    public interface IIntermediateParameterMember<TParent, TIntermediateParent> :
        IIntermediateMember<IGeneralMemberUniqueIdentifier, TParent, TIntermediateParent>,
        IIntermediateParameterMember,
        IParameterMember<TParent>
        where TParent :
            IParameterParent
        where TIntermediateParent :
            TParent, 
            IIntermediateParameterParent
    {
        new TIntermediateParent Parent { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate parameter
    /// member.
    /// </summary>
    public interface IIntermediateParameterMember :
        IIntermediateMember,
        IIntermediateMetadataEntity,
        IParameterMember
    {
        /// <summary>
        /// Returns the parent of the <see cref="IIntermediateParameterMember"/>.
        /// </summary>
        new IIntermediateParameterParent Parent { get; }
        /// <summary>
        /// Returns/sets the type that the <see cref="IIntermediateParameterMember"/>
        /// is defined as.
        /// </summary>
        new IType ParameterType { get; set; }
        /// <summary>
        /// Occurs when the parameter type changes.
        /// </summary>
        event EventHandler<EventArgsR1R2<IType, IType>> ParameterTypeChanged;
        /// <summary>
        /// Returns/sets the direction the parameter is coerced.
        /// </summary>
        new ParameterCoercionDirection Direction { get; set; }
        /// <summary>
        /// Obtains a <see cref="IParameterReferenceExpression"/> for the
        /// current <see cref="IIntermediateParameterMember"/>.
        /// </summary>
        /// <returns>A <see cref="IParameterReferenceExpression"/> for the
        /// current <see cref="IIntermediateParameterMember"/>.</returns>
        IParameterReferenceExpression GetReference();
    }
}
