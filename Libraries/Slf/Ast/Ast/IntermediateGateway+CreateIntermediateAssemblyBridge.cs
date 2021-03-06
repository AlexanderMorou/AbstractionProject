using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Languages;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateGateway
    {
        [CompilerGenerated]
        private static class CreateAssemblyBridgeCache<T>
            where T :
                IIntermediateAssembly
        {
            private static ICreateAssemblyBridge<T> bridge;

            public static ICreateAssemblyBridge<T> Bridge
            {
                get
                {
                    if (bridge == null)
                        bridge = new CreateAssemblyDynamicBridge<T>();
                    return bridge;
                }
            }

            internal static void RegisterBridge(ICreateAssemblyBridge<T> bridge)
            {
                if (CreateAssemblyBridgeCache<T>.bridge != null && 
                  !(CreateAssemblyBridgeCache<T>.bridge is CreateAssemblyDynamicBridge<T>))
                    return;
                CreateAssemblyBridgeCache<T>.bridge = bridge;
            }
        }

        /// <summary>
        /// Registers a <see cref="ICreateAssemblyBridge{T}"/> which associates
        /// the create assembly functionality to the <paramref name="bridge"/> provided
        /// versus an automatically generated variant of the same.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IIntermediateAssembly"/> to
        /// construct through the <paramref name="bridge"/>
        /// provided.</typeparam>
        /// <param name="bridge">The <see cref="ICreateAssemblyBridge{T}"/> which
        /// provides the create assembly functionality.</param>
        public static void RegisterCreateAssemblyBridge<T>(this ICreateAssemblyBridge<T> bridge)
            where T :
                IIntermediateAssembly
        {
            CreateAssemblyBridgeCache<T>.RegisterBridge(bridge);
        }

    }
}
