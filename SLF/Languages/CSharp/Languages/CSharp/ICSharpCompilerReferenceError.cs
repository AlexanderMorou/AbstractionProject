using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    public interface ICSharpCompilerReferenceError :
        ICompilerReferenceError
    {
        /// <summary>
        /// Returns the <see cref="CSharpErrorIdentifiers"/> value unique to the reference message.
        /// </summary>
        new CSharpErrorIdentifiers MessageIdentifier { get; }
    }
}
