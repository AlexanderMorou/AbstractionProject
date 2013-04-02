using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a statement that exports a 
    /// <see cref="CodeStatement"/> of <typeparamref name="TDom"/>.
    /// </summary>
    /// <typeparam name="TDom">The type of statement that is exported into CodeDom</typeparam>
    public interface IStatement<TDom> :
        IStatement
        where TDom :
            CodeStatement
    {
        new TDom GenerateCodeDom(ICodeDOMTranslationOptions options);
    }
}
