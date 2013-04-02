using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Languages;
/*---------------------------------------------------------------------\
| Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateGateway
    {
        /// <summary>
        /// Generic <see cref="CreateAssembly{T}(string)"/>
        /// bridge type.
        /// </summary>
        /// <typeparam name="T">The type of assembly that implements
        /// <see cref="IIntermediateAssembly"/>.</typeparam>
        [CompilerGenerated]
        public interface ICreateAssemblyBridge<T>
            where T :
                IIntermediateAssembly
        {
            /// <summary>
            /// Creates a new <typeparamref name="T"/>
            /// instance with the <paramref name="name"/>
            /// provided.
            /// </summary>
            /// <param name="name">The <see cref="String"/>
            /// value representing the name of the assembly.
            /// </param>
            /// <returns>A new <typeparamref name="T"/>
            /// instance with the <paramref name="name"/>
            /// provided.</returns>
            T ctor(string name);
            /// <summary>
            /// Creates a new <typeparamref name="T"/>
            /// instance with the <paramref name="name"/>
            /// provided.
            /// </summary>
            /// <typeparam name="TLanguage"></typeparam>
            /// <typeparam name="TProvider"></typeparam>
            /// <param name="name"></param>
            /// <param name="provider"></param>
            /// <returns></returns>
            T ctor<TLanguage, TProvider>(string name, TProvider provider)
                where TLanguage :
                    ILanguage
                where TProvider :
                    ILanguageProvider;
        }
    }
}
