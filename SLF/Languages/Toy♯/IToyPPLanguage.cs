using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.ToySharp
{
    public interface IToySharpLanguage :
        ILanguage<IToySharpLanguage, IToySharpProvider>,
        IVersionedLanguage<ToySharpLanguageVersion>
    {
        /// <summary>
        /// Returns a new <see cref="IToySharpProvider"/> associated to the current
        /// <see cref="IToySharpLanguage"/>.
        /// </summary>
        /// <param name="version">The <typeparamref name="TVersion"/>
        /// which denotes what specific version of the language to obtain
        /// the provider for.</param>
        /// <returns>A new <see cref="IToySharpProvider"/> for the current
        /// <see cref="IToySharpLanguage"/>.</returns>
        IToySharpProvider GetProvider(ToySharpLanguageVersion version);
        /// <summary>
        /// Returns a new <see cref="IToySharpProvider"/> associated to the
        /// <see cref="IToySharpLanguage">T*y++ language</see> relative
        /// to the <paramref name="version"/>.
        /// </summary>
        /// <param name="version">The <see cref="ToySharpLanguageVersion"/>
        /// value which denotes what version of the language to return 
        /// the provider for.</param>
        /// <param name="identityManager">The <see cref="IIntermediateCliManager"/>
        /// which is used to marshal type identities in the current type model.</param>
        /// <returns>A new <see cref="IToySharpProvider"/> for the 
        /// <see cref="IToySharpLanguage">T*y++ language</see>.</returns>
        IToySharpProvider GetProvider(ToySharpLanguageVersion version, IIntermediateCliManager identityManager);
        /// <summary>
        /// Returns a new <see cref="IToySharpProvider"/> associated to the
        /// <see cref="IToySharpLanguage">T*y++</see> .
        /// </summary>
        /// <param name="identityManager">The <see cref="IIntermediateCliManager"/>
        /// which is used to marshal type identities in the current type model.</param>
        /// <returns>A new <see cref="IToySharpProvider"/> for the 
        /// <see cref="IToySharpLanguage">T*y++ language</see>.</returns>
        IToySharpProvider GetProvider(IIntermediateCliManager identityManager);

        /// <summary>
        /// Returns a new <see cref="IToySharpProvider"/> associated to the current
        /// <see cref="IToySharpLanguage"/>.
        /// </summary>
        /// <param name="version">The <typeparamref name="TVersion"/>
        /// which denotes what specific version of the language to obtain
        /// the provider for.</param>
        /// <param name="frameworkVersion">The <see cref="CliFrameworkVersion"/>
        /// which denotes the features that are available.</param>
        /// <param name="platform">The <see cref="CliFrameworkPlatform"/> which denotes the platform
        /// the Common Language Infrastructure should target.</param>
        /// <returns>A new <see cref="IToySharpProvider"/> for the current
        /// <see cref="IToySharpLanguage"/>.</returns>
        IToySharpProvider GetProvider(ToySharpLanguageVersion version, CliFrameworkPlatform platform, CliFrameworkVersion frameworkVersion);

    }
}
