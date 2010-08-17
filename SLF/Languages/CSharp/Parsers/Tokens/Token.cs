using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers.Tokens
{
    public abstract class Token :
        IToken
    {
        private FileLocale location;
        public Token(FileLocale location)
        {
            this.location = location;
        }

        #region IToken Members

        public FileLocale Location
        {
            get { return this.location; }
        }

        public abstract uint Length { get; }

        #endregion
    }
}
