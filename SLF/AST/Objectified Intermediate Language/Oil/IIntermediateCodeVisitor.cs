using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.Slf.Oil
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
}
