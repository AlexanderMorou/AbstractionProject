﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Services;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguageProvider :
        IServiceProvider<ILanguageService>
    {
        /// <summary>
        /// Returns the <see cref="ILanguage"/>
        /// of the current <see cref="ILanguageProvider"/>.
        /// </summary>
        ILanguage Language { get; }
        /// <summary>
        /// Creates a new <see cref="IAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="IAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        IAssembly CreateAssembly(string name);
        /// <summary>
        /// Returns the <see cref="IIdentityManager"/> which maintains
        /// the identity of types within the current type model.
        /// </summary>
        IIdentityManager IdentityManager { get; }
    }

    public interface ILanguageProvider<TLanguage, TProvider> :
        ILanguageProvider
        where TLanguage :
            ILanguage<TLanguage, TProvider>
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>
    {
        /// <summary>
        /// Returns the <typeparamref name="TLanguage"/>
        /// of the current <see cref="ILanguageProvider{TLanguage, TProvider}"/>.
        /// </summary>
        new TLanguage Language { get; }
        /// <summary>
        /// Obtains a <typeparamref name="TService"/> by its
        /// <paramref name="service"/> <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="TService">The type of <see cref="ILanguageService{TLanguage, TProvider}"/>
        /// to retrieve.</typeparam>
        /// <param name="service">The <see cref="Guid"/> unique to the
        /// service requested.</param>
        /// <returns>The <typeparamref name="TService"/> by the <paramref name="service"/>
        /// <see cref="Guid"/> provided.</returns>
        new TService GetService<TService>(Guid service)
            where TService :
                ILanguageService<TLanguage, TProvider>;
        /// <summary>
        /// Returns whether the <paramref name="service"/>
        /// provided is assignable from the <typeparamref name="TService"/>
        /// provided.
        /// </summary>
        /// <typeparam name="TService">The kind of service to check against the
        /// active service in play.</typeparam>
        /// <param name="service">The <see cref="Guid"/> of the service
        /// to check for.</param>
        /// <returns>true, if the <paramref name="service"/> requested is assignable
        /// from the <typeparamref name="TService"/> provided.</returns>
        new bool ServiceIs<TService>(Guid service)
            where TService :
                ILanguageService<TLanguage, TProvider>;

        new bool TryGetService<TService>(Guid serviceGuid, out TService service)
            where TService :
                ILanguageService<TLanguage, TProvider>;

    }
}
