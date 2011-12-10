using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    /// <summary>
    /// Defines properties and methods for working with the 
    /// <see cref="IVisualBasicLanguage">Visual Basic.NET language</see>.
    /// </summary>
    public interface IVisualBasicLanguage :
        ILanguage<IVisualBasicLanguage, ICoreVisualBasicProvider>,
        ILanguage<IVisualBasicLanguage, IMyVisualBasicProvider>,
        IVersionedHighLevelLanguage<VisualBasicVersion, IVisualBasicStart>
    {
        /// <summary>
        /// Returns a new <see cref="IICoreVisualBasicProvider"/> associated to the current
        /// <see cref="IVisualBasicLanguage"/>.
        /// </summary>
        /// <returns>A new <see cref="ICoreVisualBasicProvider"/> for the
        /// <see cref="IVisualBasicLanguage"/>.</returns>
        new ICoreVisualBasicProvider GetProvider();
        /// <summary>
        /// Returns a new <see cref="ICoreVisualBasicProvider"/> associated to the
        /// <see cref="IVisualBasicLanguage"/>.
        /// </summary>
        /// <param name="version">The <see cref="VisualBasicVersion"/>
        /// value which denotes what version of the visual basic 
        /// language to return the provider for.</param>
        /// <returns>A new <see cref="ICoreVisualBasicProvider"/> for the current
        /// <see cref="IVisualBasicLanguage"/>.</returns>
        new ICoreVisualBasicProvider GetProvider(VisualBasicVersion version);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        IMyVisualBasicProvider GetMyProvider(VisualBasicVersion version = VisualBasicLanguage.DefaultVersion);
        /// <summary>
        /// Creates a new <see cref="ICoreVisualBasicAssembly"/>
        /// with the <paramref name="name"/> and 
        /// <paramref name="version"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <param name="version">The <see cref="VisualBasicVersion"/>
        /// of the language to which the <see cref="ICoreVisualBasicAssembly"/>
        /// is built against.</param>
        /// <returns>A new <see cref="ICoreVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>
        /// or <paramref name="version"/> is out of the values allowed.</exception>
        new ICoreVisualBasicAssembly CreateAssembly(string name, VisualBasicVersion version);
        /// <summary>
        /// Creates a new <see cref="ICoreVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="ICoreVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        new ICoreVisualBasicAssembly CreateAssembly(string name);
    }
}
