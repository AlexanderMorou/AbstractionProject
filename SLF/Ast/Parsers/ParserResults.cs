using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    public class ParserResults<T> :
        IParserResults<T>
        where T :
            IConcreteNode
    {
        private IParserSyntaxMessageCollection syntaxErrors;
        #region IParserResults<T> Members

        public bool Successful
        {
            get { return !this.SyntaxErrors.HasErrors; }
        }

        public IParserSyntaxMessageCollection SyntaxErrors
        {
            get {
                if (this.syntaxErrors == null)
                    this.syntaxErrors = new ParserSyntaxErrorCollection();
                return this.syntaxErrors; }
        }

        public T Result { get; protected set; }

        #endregion
    }
}
