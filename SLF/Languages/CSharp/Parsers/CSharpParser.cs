using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using System.IO;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    public class CSharpParser :
        ParserBase<ICSharpAssembly, ICSharpTokenizer>,
        ICSharpParser
    {

        /// <summary>
        /// Creates the tokenizer used by the <see cref="CSharpParser"/>
        /// with the <paramref name="stream"/> provided.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> which handles reading in data
        /// for the tokenizer.</param>
        /// <returns>A new <see cref="ICSharpTokenizer"/>
        /// instance linked to the current <paramref name="stream"/>.</returns>
        protected override ICSharpTokenizer GetTokenizer(Stream stream)
        {
            return new CSharpTokenizer(stream);
        }

        protected override ICSharpAssembly ProcessParse(Stream source)
        {
            CSharpAssembly result = new CSharpAssembly();
            this.ProcessParse(source, result);
            return result;
        }

        protected void ProcessParse(Stream source, CSharpAssembly target)
        {

        }

        #region ICSharpParser Members

        /// <summary>
        /// Returns the <see cref="ICSharpAssembly"/> of a parse operation as a partial
        /// to the <paramref name="root"/> provided.
        /// </summary>
        /// <param name="filename">The name of the file to parse.</param>
        public ICSharpAssembly Parse(string filename, ICSharpAssembly root)
        {
            CSharpAssembly result = new CSharpAssembly((CSharpAssembly)root);
            root.Parts.Add(result);
            FileStream source = new FileStream(filename, FileMode.Open);
            this.ProcessParse(source, result);
            source.Dispose();
            return result;
        }

        /// <summary>
        /// Returns the <see cref="ICSharpAssembly"/> of a parse operation on the
        /// <paramref name="source"/> <see cref="Stream"/> as a partial
        /// to the <paramref name="root"/> provided.
        /// </summary>
        /// <param name="source">The <see cref="Stream"/> from which to read bytes from.</param>
        public ICSharpAssembly Parse(Stream source, ICSharpAssembly root)
        {
            CSharpAssembly result = new CSharpAssembly((CSharpAssembly)root);
            root.Parts.Add(result);
            this.ProcessParse(source, result);
            return result;
        }

        #endregion

    }
}
