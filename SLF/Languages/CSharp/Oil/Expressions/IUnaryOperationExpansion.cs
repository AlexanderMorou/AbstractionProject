using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2009 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public interface IUnaryOperationExpansion
    {
        /// <summary>
        /// The <see cref="IType"/> which was the source
        /// of the <see cref="IUnaryOperationExpansion"/>.
        /// </summary>
        IType BeginType { get; }
        /// <summary>
        /// The <see cref="IType"/> which was the result of the
        /// <see cref="IUnaryOperationExpansion"/>.
        /// </summary>
        IType EndType { get; }
        /// <summary>
        /// The <see cref="IUnaryOperatorCoercionMember"/>
        /// which will be used for negation.
        /// </summary>
        IUnaryOperatorCoercionMember Negate { get; }
        /// <summary>
        /// The <see cref="IUnaryOperatorCoercionMember"/>
        /// which will be used for inversion.
        /// </summary>
        IUnaryOperatorCoercionMember Invert { get; }
        /// <summary>
        /// The <see cref="IBinaryOperationExpansion"/> which
        /// will be used for the increment/decrement operation
        /// with an <see cref="Int32"/> of '1'.
        /// </summary>
        IBinaryOperationExpansion CrementOperation { get; }
    }
}
