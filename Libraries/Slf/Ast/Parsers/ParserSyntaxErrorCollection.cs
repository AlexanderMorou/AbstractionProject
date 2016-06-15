using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Languages;
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
    /// Provides a series of syntax error 
    /// messages.
    /// </summary>
    internal class ParserSyntaxErrorCollection :
        ControlledCollection<IParserSyntaxMessage>,
        IParserSyntaxMessageCollection
    {
        #region IParserSyntaxMessageCollection Members

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="IParserSyntaxError"/>
        /// which denotes a point within the source which is syntactically invalid.
        /// </summary>
        /// <param name="errorText">The <see cref="String"/> representing the
        /// specifics of the syntax error.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="IParserSyntaxError"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="IParserSyntaxError"/> which results.</param>
        /// <param name="source">The <see cref="Uri"/>
        /// value associated to the file on which the error occurred.</param>
        /// <returns>A new <see cref="IParserSyntaxError"/>
        /// which denotes the point within the source on which the 
        /// syntax error occurred.</returns>
        public IParserSyntaxError SyntaxError(string errorText, LineColumnPair start, LineColumnPair end, Uri source)
        {
            var result = new ParserSyntaxError(errorText, start, end, source);
            base.AddImpl(result);
            return result;
        }


        public void SyntaxError(IParserSyntaxError error)
        {
            base.AddImpl(error);
        }

        /// <summary>
        /// Returns whether the <see cref="IParserSyntaxMessageCollection"/> 
        /// has an <see cref="IParserSyntaxError"/>.
        /// </summary>
        public bool HasErrors
        {
            get { return this.Count > 0; }
        }

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
        /// <param name="source">The <see cref="Uri"/> value associated to the file on which the warning occurred.</param>
        /// <returns>A new <see cref="IParserSyntaxError"/>
        /// which denotes the point, within the source, on which the 
        /// syntax warning occurred.</returns>
        public IParserSyntaxWarning SyntaxWarning(string warningText, LineColumnPair start, LineColumnPair end, Uri source)
        {
            var result = new ParserSyntaxWarning(warningText, start, end, source);
            base.AddImpl(result);
            return result;
        }

        /// <summary>
        /// Inserts a syntax <paramref name="warning"/> from the one provided.
        /// </summary>
        /// <param name="warning">The <see cref="IParserSyntaxWarning"/> which denotes
        /// the error text, line, column, and file name associated to the error.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="warning"/>
        /// is null.</exception>
        public void SyntaxWarning(IParserSyntaxWarning warning)
        {
            base.AddImpl(warning);
        }

        #endregion
    }
}
