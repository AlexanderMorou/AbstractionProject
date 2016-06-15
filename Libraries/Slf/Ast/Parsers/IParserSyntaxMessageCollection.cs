using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    /// <summary>
    /// Defines properties and methods for working with a series of syntax error 
    /// messages.
    /// </summary>
    public interface IParserSyntaxMessageCollection :
        IControlledCollection<IParserSyntaxMessage>
    {
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="IParserSyntaxError"/>
        /// which denotes a point within the source which is syntactically invalid.
        /// </summary>
        /// <param name="errorText">The <see cref="String"/> representing the
        /// specifics of the syntax error.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> which denotes the
        /// start location of the syntax error.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes the
        /// end location of the syntax error.</param>
        /// <param name="source">The <see cref="Uri"/>
        /// associated to the location on which the error occurred.</param>
        /// <returns>A new <see cref="IParserSyntaxError"/>
        /// which denotes the point, within the source, on which the 
        /// syntax error occurred.</returns>
        IParserSyntaxError SyntaxError(string errorText, LineColumnPair start, LineColumnPair end, Uri source);

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="IParserSyntaxWarning"/>
        /// which denotes a point within the source which is syntactically suspect.
        /// </summary>
        /// <param name="warningText">The <see cref="String"/> representing the
        /// specifics of the syntax warning.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> which denotes the
        /// start location of the syntax warning.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes the
        /// end location of the syntax warning.</param>
        /// <param name="source">The <see cref="Uri"/>
        /// associated to the location on which the warning occurred.</param>
        /// <returns>A new <see cref="IParserSyntaxError"/>
        /// which denotes the point, within the source, on which the 
        /// syntax warning occurred.</returns>
        IParserSyntaxWarning SyntaxWarning(string warningText, LineColumnPair start, LineColumnPair end, Uri source);

        /// <summary>
        /// Inserts a syntax <paramref name="error"/> from the one provided.
        /// </summary>
        /// <param name="error">The <see cref="IParserSyntaxError"/> which denotes
        /// the error text, line, column, and file name associated to the error.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="error"/>
        /// is null.</exception>
        void SyntaxError(IParserSyntaxError error);

        /// <summary>
        /// Inserts a syntax <paramref name="warning"/> from the one provided.
        /// </summary>
        /// <param name="warning">The <see cref="IParserSyntaxWarning"/> which denotes
        /// the error text, line, column, and file name associated to the error.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="warning"/>
        /// is null.</exception>
        void SyntaxWarning(IParserSyntaxWarning warning);

        /// <summary>
        /// Returns whether the <see cref="IParserSyntaxMessageCollection"/> 
        /// has an <see cref="IParserSyntaxError"/>.
        /// </summary>
        bool HasErrors { get; }

    }
}
