using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    public class ParserResults<T> :
        IParserResults<T>
        where T :
            IConcreteNode
    {
        private IParserSyntaxErrorCollection syntaxErrors;
        #region IParserResults<T> Members

        public bool Successful
        {
            get { return !this.SyntaxErrors.HasErrors; }
        }

        public IParserSyntaxErrorCollection SyntaxErrors
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
