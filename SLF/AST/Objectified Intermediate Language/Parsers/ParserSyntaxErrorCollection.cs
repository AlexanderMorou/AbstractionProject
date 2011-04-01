using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Languages;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    /// <summary>
    /// Provides a series of syntax error 
    /// messages.
    /// </summary>
    internal class ParserSyntaxErrorCollection :
        ControlledStateCollection<IParserSyntaxError>,
        IParserSyntaxErrorCollection
    {
        #region IParserSyntaxErrorCollection Members

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
        public IParserSyntaxError SyntaxError(string errorText, int line, int column, string fileName)
        {
            var result = new ParserSyntaxError(errorText, line, column, fileName);
            base.AddImpl(result);
            return result;
        }


        public void SyntaxError(IParserSyntaxError error)
        {
            base.AddImpl(error);
        }

        /// <summary>
        /// Returns whether the <see cref="IParserSyntaxErrorCollection"/> 
        /// has an <see cref="IParserSyntaxError"/>.
        /// </summary>
        public bool HasErrors
        {
            get { return this.Count > 0; }
        }

        #endregion
    }
}
