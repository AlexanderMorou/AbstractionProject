using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    /// <summary>
    /// The kind of transformation to perform on the target set.
    /// </summary>
    public enum TransformationKind
    {
        /// <summary>
        /// Denotes the transformer doesn't impact
        /// the current entity.
        /// </summary>
        Ignore,
        /// <summary>
        /// Denotes the transformer replaces the current entity with
        /// a new entity of the same kind, but the underlying 
        /// internal structure may change entirely.
        /// </summary>
        Replace,
        /// <summary>
        /// Denotes the transformer will merge the current entity with
        /// another entity within the same scope.
        /// </summary>
        Merge,
        /// <summary>
        /// Denotes the transformer will modify the structure of the current
        /// entity, and the internal structure will stay the same, but the
        /// data will differ.
        /// </summary>
        Modify,
        /// <summary>
        /// Denotes the transformer obviates the need of the current entity 
        /// and will therefore delete it from its container.
        /// </summary>
        Delete,
        /// <summary>
        /// Denotes the transformer will modify the current set by clearing
        /// it.
        /// </summary>
        Clear,
        /// <summary>
        /// Denotes the transformer will modify the current entity without
        /// side-effects out of the range of the container of the entity.
        /// </summary>
        Inline = 0x1000,
        /// <summary>
        /// Denotes the transformer will modify the current environment
        /// such that the scope of the container may change.
        /// </summary>
        Environment = 0x3000,

    }

    public interface IIntermediateCodeTransformer :
        IExpressionTransformer,
        IStatementTransformer,
        IIntermediateMemberTransformer,
        IIntermediateDeclarationTransformer,
        IIntermediateTypeTransformer,
        IIntermediateInclusionTransformer
    {

    }

    /// <summary>
    /// Defines properties and methods for working with 
    /// a transformation context which denotes the context-relative
    /// assemblies
    /// </summary>
    public interface ITransformationContext
    {
        /// <summary>
        /// Returns the compilation context for the assemblies pertinent to the
        /// transformation.
        /// </summary>
        ICompilationContext CompilationContext { get; }
    }
    public interface IExpressionTransformer :
        IPrimitiveTransformer,
        ILinqTransformer,
        IExpressionVisitor<TransformationKind, ITransformationContext>
    {
    }
    public interface IPrimitiveTransformer :
        IPrimitiveVisitor<TransformationKind, ITransformationContext>
    {
    }
    public interface ILinqTransformer :
        ILinqVisitor<TransformationKind, ITransformationContext>
    {
    }

    public interface IStatementTransformer :
        IStatementVisitor<TransformationKind, ITransformationContext>
    {

    }
    public interface IIntermediateMemberTransformer :
        IIntermediateMemberVisitor<TransformationKind, ITransformationContext>
    {
    }
    public interface IIntermediateDeclarationTransformer :
        IIntermediateDeclarationVisitor<TransformationKind, ITransformationContext>
    {
    }
    public interface IIntermediateTypeTransformer :
        IIntermediateTypeVisitor<TransformationKind, ITransformationContext>
    {
    }

    public interface IIntermediateInclusionTransformer :
        IIntermediateInclusionVisitor<TransformationKind, ITransformationContext>
    {
    }
}
