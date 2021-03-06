using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate property member.
    /// </summary>
    /// <typeparam name="TProperty">The type of property as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property as it exists
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which owns the properties
    /// in the intermediate abstract syntax tree.</typeparam>
#if DEBUG
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true,
                                                         YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateMemberVisitor")]
#endif
    public interface IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        IIntermediateMember<IGeneralMemberUniqueIdentifier, TPropertyParent, TIntermediatePropertyParent>,
        IIntermediatePropertyMember,
        IPropertyMember<TProperty, TPropertyParent>
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TPropertyParent :
            IPropertyParent<TProperty, TPropertyParent>
        where TIntermediatePropertyParent :
            TPropertyParent,
            IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a property member.
    /// </summary>
    public interface IIntermediatePropertyMember :
        IIntermediatePropertySignatureMember,
        IIntermediateExtendedInstanceMember,
        IIntermediateScopedDeclaration,
        IPropertyMember
    {
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IIntermediatePropertyMember"/>.
        /// </summary>
        [VisitorPropertyRequirement("CanRead")]
        new IIntermediatePropertyMethodMember GetMethod { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IIntermediatePropertyMember"/>.
        /// </summary>
        [VisitorPropertyRequirement("CanWrite")]
        new IIntermediatePropertySetMethodMember SetMethod { get; }
    }
}
