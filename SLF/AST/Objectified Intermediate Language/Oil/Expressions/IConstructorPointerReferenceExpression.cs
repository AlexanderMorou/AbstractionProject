using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with
    /// the pointer to a constructor as an expression.
    /// </summary>
    public interface IConstructorPointerReferenceExpression :
        IExpression,
        IFusionCommaTargetExpression
    {
        /// <summary>
        /// Returns the <see cref="IConstructorReferenceStub"/>
        /// associated to the <see cref="IConstructorPointerReferenceExpression"/>.
        /// </summary>
        /// <remarks>Used to provide initial context data 
        /// for the lookup.</remarks>
        IConstructorReferenceStub Reference { get; }
        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> of
        /// <see cref="IType"/> instances which relate to 
        /// the types of parameters used to
        /// bind to the constructor.
        /// </summary>
        ITypeCollection Signature { get; }
        /// <summary>
        /// Obtains a <see cref="IConstructorInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of 
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the 
        /// types of the parameters.</param>
        /// <returns>A new <see cref="IConstructorInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        IConstructorInvokeExpression Invoke(IExpressionCollection parameters);
        /// <summary>
        /// Obtains a <see cref="IConstructorInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the types 
        /// of the parameters.</param>
        /// <returns>A new <see cref="IConstructorInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        IConstructorInvokeExpression Invoke(params IExpression[] parameters);
    }
}
