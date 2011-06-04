using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with the 
    /// <see cref="IVisualBasicLanguage">Visual Basic.NET language</see>.
    /// </summary>
    public interface IVisualBasicLanguage :
        IVersionedHighLevelLanguage<VisualBasicVersion, IVisualBasicStart>
    {
        new IVisualBasicProvider GetProvider();
        /// <summary>
        /// Returns a new <see cref="IVisualBasicProvider"/> associated to the
        /// <see cref="IVisualBasicLanguage"/>.
        /// </summary>
        /// <param name="version">The <see cref="VisualBasicVersion"/>
        /// value which denotes what version of the visual basic 
        /// language to return the provider for.</param>
        /// <returns>A new <see cref="IVisualBasicProvider"/> for the current
        /// <see cref="IVisualBasicLanguage"/>.</returns>
        new IVisualBasicProvider GetProvider(VisualBasicVersion version);
        /// <summary>
        /// Creates a new <see cref="IVisualBasicAssembly"/>
        /// with the <paramref name="name"/> and 
        /// <paramref name="version"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <param name="version">The <see cref="VisualBasicVersion"/>
        /// of the language to which the <see cref="IVisualBasicAssembly"/>
        /// is built against.</param>
        /// <returns>A new <see cref="IIntermediateAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>
        /// or <paramref name="version"/> is out of the values allowed.</exception>
        IVisualBasicAssembly CreateAssembly(string name, VisualBasicVersion version);
        /// <summary>
        /// Creates a new <see cref="IVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="IVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        IVisualBasicAssembly CreateAssembly(string name);
    }
}
