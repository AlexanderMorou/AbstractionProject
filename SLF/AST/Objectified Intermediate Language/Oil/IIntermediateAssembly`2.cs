﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Oil
{

    /// <summary>
    /// Defines generic properties and methods for working with
    /// an intermediate assembly which belongs to a specific language.
    /// </summary>
    /// <typeparam name="TLanguage">The type of the language
    /// which yields information and services pertinent to the 
    /// tools which build the assembly.</typeparam>
    /// <typeparam name="TProvider">The language service provider
    /// which yields a set of services to aid in compilation
    /// of the <see cref="IIntermediateAssembly{TLanguage, TProvider}">assembly</see>.</typeparam>
    public interface IIntermediateAssembly<TLanguage, TProvider> :
        IIntermediateAssembly
        where TLanguage :
            ILanguage
        where TProvider :
            ILanguageProvider
    {
        /// <summary>
        /// Returns the <typeparamref name="TLanguage"/> in which the 
        /// <see cref="IIntermediateAssembly{TLanguage, TProvider}"/>
        /// is written in.
        /// </summary>
        TLanguage Language { get; }
        /// <summary>
        /// Returns the <typeparamref name="TProvider"/>
        /// which created the <see cref="IIntermediateAssembly{TLanguage, TProvider}"/>.
        /// </summary>
        TProvider Provider { get; }
    }
}