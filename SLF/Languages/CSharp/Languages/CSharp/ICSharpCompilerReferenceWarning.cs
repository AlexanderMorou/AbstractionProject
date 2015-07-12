using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    public interface ICSharpCompilerReferenceWarning :
        ICompilerReferenceWarning
    {
        /// <summary>
        /// Returns the <see cref="CSharpWarningIdentifiers"/> value unique to the reference message.
        /// </summary>
        new CSharpWarningIdentifiers MessageIdentifier { get; }
        /// <summary>
        /// Returns the <see cref="CSharpWarningLevels"/> value indicating how 
        /// severe the warning is.
        /// </summary>
        new CSharpWarningLevels WarningLevel { get; }
    }
}
