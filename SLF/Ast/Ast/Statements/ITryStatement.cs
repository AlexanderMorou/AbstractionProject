using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for defining a 
    /// try block which catches exceptions, or provides cleanup code
    /// for a given section of statements.
    /// </summary>
    public interface ITryStatement :
        IControlledDictionary<IType, ITypedCatchExceptionBlockStatement>,
        IBlockStatement
    {
        /// <summary>
        /// Returns the number of clauses within the <see cref="ITryStatement"/>.
        /// </summary>
        int ClauseCount { get; }
        /// <summary>
        /// Returns the number of statements within the <see cref="ITryStatement"/>'s
        /// main block.
        /// </summary>
        int StatementCount { get; }
        /// <summary>
        /// Returns the <see cref="IBlockStatement"/> which
        /// contains the statements to execute in the case where
        /// <see cref="HasCatchAll"/> is true.
        /// </summary>
        IBlockStatement CatchAll { get; }
        /// <summary>
        /// Returns/sets whether the <see cref="ITryStatement"/>
        /// has a catch-all block where all unaccounted for exceptions
        /// go.
        /// </summary>
        bool HasCatchAll { get; set; }
        /// <summary>
        /// Returns the <see cref="IBlockStatement"/> associated
        /// to the finally clause of a try block.
        /// </summary>
        IBlockStatement Finally { get; }
        /// <summary>
        /// Returns/sets whether the <see cref="ITryStatement"/>
        /// has a finally block.
        /// </summary>
        bool HasFinally { get; set; }
        /// <summary>
        /// Inserts and returns a <see cref="ITypedCatchExceptionBlockStatement"/>
        /// relative to the given <paramref name="exceptionType"/>.
        /// </summary>
        /// <param name="exceptionType">The <see cref="IType"/>,
        /// which derives from the root exception, to catch.</param>
        /// <returns>A <see cref="ITypedCatchExceptionBlockStatement"/>
        /// relative to a given <paramref name="exceptionType"/>.</returns>
        ITypedCatchExceptionBlockStatement Catch(IType exceptionType);
        /// <summary>
        /// Inserts and returns a <see cref="ITypeNamedCatchExceptionBlockStatement"/>
        /// relative to the given <paramref name="nameAndType"/>.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which designates the type and name of the exception to catch.</param>
        /// <returns>A <see cref="ITypeNamedCatchExceptionBlockStatement"/>
        /// relative to a given <paramref name="nameAndType"/>.</returns>
        ITypeNamedCatchExceptionBlockStatement Catch(TypedName nameAndType);
    }
}
