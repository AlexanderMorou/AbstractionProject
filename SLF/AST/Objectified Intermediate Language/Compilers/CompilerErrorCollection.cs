using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerErrorCollection : 
        ControlledStateCollection<ICompilerMessage>,
        ICompilerErrorCollection
    {
        #region ICompilerErrorCollection Members

        public ICompilerSourceWarning Warning(ICompilerReferenceWarning message, string fileName, int line, int column, params string[] replacements)
        {
            CompilerSourceWarning warning = new CompilerSourceWarning(message, fileName, line, column, replacements);
            base.AddImpl(warning);
            return warning;
        }

        public ICompilerSourceError Error(ICompilerReferenceError message, string fileName, int line, int column, params string[] replacements)
        {
            CompilerError error = new CompilerError(message, fileName, line, column, replacements);
            base.AddImpl(error);
            return error;
        }

        public bool HasErrors
        {
            get { return this.Any(p => p is ICompilerSourceError); }
        }

        #endregion

    }
}
