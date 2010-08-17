using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using System.Runtime.CompilerServices;
/*---------------------------------------------------------------------\
| Copyright © 2009 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
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
        }
    }
}
