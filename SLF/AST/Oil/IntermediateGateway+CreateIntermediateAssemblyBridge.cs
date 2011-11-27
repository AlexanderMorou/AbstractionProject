using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateGateway
    {

        [CompilerGenerated]
        private class CreateIntermediateAssemblyBridge :
            ICreateAssemblyBridge<CommonIntermediateAssembly>
        {

            #region ICreateAssemblyBridge<CommonIntermediateLanguageAssembly> Members

            public CommonIntermediateAssembly ctor(string name)
            {
                return new CommonIntermediateAssembly(name);
            }

            public CommonIntermediateAssembly ctor<TLanguage, TProvider>(string name, TProvider provider)
                where TLanguage :
                    ILanguage
                where TProvider :
                    ILanguageProvider
            {
                if (typeof(ICommonIntermediateLanguage).IsAssignableFrom(typeof(TLanguage)))
                    if (typeof(ICommonIntermediateProvider).IsAssignableFrom(typeof(TProvider)))
                        return new CommonIntermediateAssembly(name);
                throw new NotSupportedException();
            }

            #endregion

        }

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

        static IntermediateGateway()
        {
            (new CreateIntermediateAssemblyBridge()).RegisterCreateAssemblyBridge();
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
