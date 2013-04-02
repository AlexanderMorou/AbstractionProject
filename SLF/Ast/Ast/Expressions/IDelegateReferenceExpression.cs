using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
/* *
 * Two types of delegate references:  One points to a method
 * one points to an actual variable typed to a delegate.
 * */
namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public enum DelegateReferenceKind
    {
        /// <summary>
        /// The delegate reference points to a method.
        /// </summary>
        MethodPointerReference,
        /// <summary>
        /// The delegate reference points to a delegate reference.
        /// </summary>
        DirectDelegateReference,
    }
    /// <summary>
    /// Defines properties and methods for referring to a delegate
    /// pointer expression which denotes the method pointed to by
    /// a delegate acceptor of some kind.
    /// </summary>
    public interface IDelegateReferenceExpression :
        IExpression
    {
        /// <summary>
        /// Returns the <see cref="DelegateReferenceKind"/> the
        /// <see cref="IDelegateReferenceExpression"/> is.
        /// </summary>
        DelegateReferenceKind ReferenceType { get; }
    }

    public interface IDelegateHolderReferenceExpression :
        IDelegateReferenceExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="IDelegateType"/> associated
        /// to the <see cref="IDelegateHolderReferenceExpression"/>.
        /// </summary>
        IType DelegateType { get; set; }
    }

    public interface IDelegateMethodPointerReferenceExpression :
        IDelegateHolderReferenceExpression
    {
        /// <summary>
        /// Returns/sets the method pointer the delegate
        /// reference points to.
        /// </summary>
        IMethodPointerReferenceExpression Reference { get; set; }
    }
}
