using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a simple 
    /// iteration statement.
    /// </summary>
    /// <remarks>
    /// <para>Used in Visual Basic for 
    ///     'For Target = Start To End [STEP Incremental]'.</para>
    /// <para>Simplest implementation in C&#9839;: 
    ///     'for (Type Target = Start; Target &lt;=? Start; Target+[+ | =Incremental])'.</para>
    /// </remarks>
    public interface ISimpleIterationBlockStatement :
        IBreakableBlockStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="ILocalDeclarationStatement"/> which
        /// denotes what local is referred to by the iteration block.
        /// </summary>
        ILocalDeclarationStatement Target { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which denotes the start of the
        /// <see cref="ISimpleIterationBlockStatement"/>.
        /// </summary>
        IExpression Start { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which denotes the end of the
        /// <see cref="ISimpleIterationBlockStatement"/>.
        /// </summary>
        IExpression End { get; set; }

        /// <summary>
        /// Returns/sets whether the <see cref="End"/> is exclusive, that is
        /// not included as a step in the iteration.
        /// </summary>
        bool EndExclusive { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which denotes how large
        /// the span is between each iteration.
        /// </summary>
        IExpression Incremental { get; set; }
    }
}
