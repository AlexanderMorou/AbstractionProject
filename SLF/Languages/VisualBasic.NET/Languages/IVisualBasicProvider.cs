﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a provider
    /// for the <see cref="IVisualBasicLanguage">Visual Basic.NET language</see>.
    /// </summary>
    public interface IVisualBasicProvider :
        IVersionedHighLevelLanguageProvider<VisualBasicVersion, IVisualBasicStart>
    {
        /// <summary>
        /// Returns the <see cref="IVisualBasicLanguage">Visual Basic.NET
        /// language</see>
        /// associated to the <see cref="IVisualBasicProvider"/>.
        /// </summary>
        new IVisualBasicLanguage Language { get; }
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
        new IVisualBasicAssembly CreateAssembly(string name);
    }
}