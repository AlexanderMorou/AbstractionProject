using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    /// <summary>
    /// Defines properties and methods for working with a series of syntax error 
    /// messages.
    /// </summary>
    public interface IParserSyntaxErrorCollection :
        IControlledStateCollection<IParserSyntaxError>
    {
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="IParserSyntaxError"/>
        /// which denotes a point within the source which is syntactically invalid.
        /// </summary>
        /// <param name="errorText">The <see cref="String"/> representing the
        /// specifics of the syntax error.</param>
        /// <param name="line">The <see cref="Int32"/> pertinent to the syntax error's line.</param>
        /// <param name="column">The <see cref="Int32"/> pertinent to the syntax error's column
        /// on the <paramref name="line"/> provided.</param>
        /// <param name="fileName">The <see cref="String"/>
        /// value associated to the file on which the error occurred.</param>
        /// <returns>A new <see cref="IParserSyntaxError"/>
        /// which denotes the point within the source on which the 
        /// syntax error occurred.</returns>
        IParserSyntaxError SyntaxError(string errorText, int line, int column, string fileName);

        /// <summary>
        /// Inserts a syntax <paramref name="error"/> from the one provided.
        /// </summary>
        /// <param name="error">The <see cref="IParserSyntaxError"/> which denotes
        /// the error text, line, column, and file name associated to the error.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="error"/>
        /// is null.</exception>
        void SyntaxError(IParserSyntaxError error);

        /// <summary>
        /// Returns whether the <see cref="IParserSyntaxErrorCollection"/> 
        /// has an <see cref="ISourceRelatedError"/>.
        /// </summary>
        bool HasErrors { get; }

    }
}
