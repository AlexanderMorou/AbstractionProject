using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    /// <summary>
    /// Defines properties and methods for working with a Visual
    /// Basic assembly which emits the My* series of supplemental 
    /// development aids.
    /// </summary>
    public interface IMyVisualBasicProvider :
        ILanguageProvider<IVisualBasicLanguage, IMyVisualBasicProvider>,
        IVersionedHighLevelLanguageProvider<VisualBasicVersion, IVisualBasicStart>
    {
        /// <summary>
        /// Creates a new <see cref="IMyVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="IMyVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        new IMyVisualBasicAssembly CreateAssembly(string name);
    }
}
