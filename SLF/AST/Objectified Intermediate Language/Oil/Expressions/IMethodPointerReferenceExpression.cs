using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// pointer to a method.
    /// </summary>
    public interface IMethodPointerReferenceExpression :
        IExpression,
        IFusionCommaTargetExpression
        /* ILinkableExpression*/
    {
        /*
        /// <summary>
        /// Returns the member associated to the 
        /// <see cref="IMemberReferenceExpression"/>.
        /// </summary>
        IMethodSignatureMember AssociatedMember { get; }
         */
        /// <summary>
        /// Returns the <see cref="IMethodReferenceStub"/>
        /// associated to the <see cref="IMethodPointerReferenceExpression"/>.
        /// </summary>
        /// <remarks>Used to provide initial context data 
        /// for the lookup.</remarks>
        IMethodReferenceStub Reference { get; }
        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> of
        /// <see cref="IType"/> instances which relate to 
        /// the types of parameters used to
        /// bind to the method.
        /// </summary>
        ITypeCollection Signature { get; }
        /// <summary>
        /// Obtains a <see cref="IMethodInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of 
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the 
        /// types of the parameters.</param>
        /// <returns>A new <see cref="IMethodInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        IMethodInvokeExpression Invoke(IExpressionCollection parameters);
        /// <summary>
        /// Obtains a <see cref="IMethodInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the types 
        /// of the parameters.</param>
        /// <returns>A new <see cref="IMethodInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        IMethodInvokeExpression Invoke(params IExpression[] parameters);
    }
}
