using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    public abstract class ParserBase<TResults, TTokenizer> :
        IParser<TResults, TTokenizer>
        where TResults :
            IParserResults
        where TTokenizer :
            ITokenizer
    {
        private TTokenizer tokenizer;

        /// <summary>
        /// Creates the tokenizer used by the <see cref="ParserBase{TResults, TTokenizer}"/>
        /// with the <paramref name="stream"/> provided.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> which handles reading in data
        /// for the tokenizer.</param>
        /// <returns>A new <typeparamref name="TTokenizer"/>
        /// instance linked to the current <paramref name="stream"/>.</returns>
        protected abstract TTokenizer GetTokenizer(Stream stream);

        #region IParser<TResults,TTokenizer> Members

        public TResults Parse(string fileName)
        {
            FileStream parseStream = new FileStream(fileName, FileMode.Open);
            TResults results = this.Parse(parseStream);
            parseStream.Dispose();
            return results;
        }

        public TResults Parse(Stream source)
        {
            if (this.tokenizer == null)
                this.tokenizer = this.GetTokenizer(source);
            TResults parserResult = this.ProcessParse(source);
            this.Tokenizer.Dispose();
            this.tokenizer = default(TTokenizer);
            return parserResult;
        }

        public TTokenizer Tokenizer
        {
            get
            {
                return this.tokenizer;
            }
        }

        #endregion

        #region IParser Members

        IParserResults IParser.Parse(string fileName)
        {
            return this.Parse(fileName);
        }

        IParserResults IParser.Parse(Stream source)
        {
            return this.Parse(source);
        }

        ITokenizer IParser.Tokenizer
        {
            get { return this.Tokenizer; }
        }

        #endregion
        protected abstract TResults ProcessParse(Stream source);

    }
}
