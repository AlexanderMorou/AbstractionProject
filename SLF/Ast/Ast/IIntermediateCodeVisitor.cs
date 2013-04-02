using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with an
    /// intermediate code visitor.
    /// </summary>
    public interface IIntermediateCodeVisitor :
        IExpressionVisitor,
        IStatementVisitor,
        IIntermediateDeclarationVisitor,
        IIntermediateTypeVisitor, 
        IIntermediateMemberVisitor,
        IIntermediateInclusionVisitor
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an
    /// intermediate code visitor.
    /// </summary>
    /// <typeparam name="TResult">The value returned upon visiting the expression.</typeparam>
    /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
    public interface IIntermediateCodeVisitor<TResult, TContext> :
        IExpressionVisitor<TResult, TContext>,
        IStatementVisitor<TResult, TContext>,
        IIntermediateDeclarationVisitor<TResult, TContext>,
        IIntermediateTypeVisitor<TResult, TContext>,
        IIntermediateMemberVisitor<TResult, TContext>,
        IIntermediateInclusionVisitor<TResult, TContext>
    {
    }
}
