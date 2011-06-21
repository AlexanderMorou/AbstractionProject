using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a statement which
    /// represents the action of enumerating the elements of a set and performing
    /// a set of actions on each element.
    /// </summary>
    public interface IEnumerateSetBreakableBlockStatement :
        IBreakableBlockStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="ILocalDeclarationStatement"/> which
        /// designates the <see cref="ILocalMember"/> to utilize within the
        /// scope of the enumeration.
        /// </summary>
        ILocalDeclarationStatement LocalDeclaration { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which provides the
        /// source set for the enumeration.
        /// </summary>
        IExpression Source { get; set; }
    }
}
