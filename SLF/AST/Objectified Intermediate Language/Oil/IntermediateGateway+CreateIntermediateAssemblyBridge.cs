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

        [CompilerGenerated]
        private class CreateIntermediateAssemblyBridge :
            ICreateAssemblyBridge<IntermediateAssembly>
        {

            #region ICreateAssemblyBridge<IntermediateAssembly> Members

            public IntermediateAssembly ctor(string name)
            {
                return new IntermediateAssembly(name);
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

        [CompilerGenerated]
        public static void RegisterCreateAssemblyBridge<T>(this ICreateAssemblyBridge<T> bridge)
            where T :
                IIntermediateAssembly
        {
            CreateAssemblyBridgeCache<T>.RegisterBridge(bridge);
        }
    }
}
