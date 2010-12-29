using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerErrorCollection : 
        ControlledStateCollection<ICompilerMessage>,
        ICompilerErrorCollection
    {
        #region ICompilerErrorCollection Members

        public ICompilerWarning Warning(ICompilerReferenceWarning message, int column, int line, string fileName, params string[] replacements)
        {
            CompilerWarning warning = new CompilerWarning(message, column, line, fileName, replacements);
            base.AddImpl(warning);
            return warning;
        }

        public ICompilerError Error(ICompilerReferenceError message, int column, int line, string fileName, params string[] replacements)
        {
            CompilerError error = new CompilerError(message, column, line, fileName, replacements);
            base.AddImpl(error);
            return error;
        }

        public bool HasErrors
        {
            get { return this.Any(p => p is ICompilerError); }
        }

        #endregion

    }
}
