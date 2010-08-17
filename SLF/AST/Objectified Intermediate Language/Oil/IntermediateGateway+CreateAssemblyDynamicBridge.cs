using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.CompilerServices;
using System.Reflection;
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
        private class CreateAssemblyDynamicBridge<TAssembly> :
            ICreateAssemblyBridge<TAssembly>
            where TAssembly :
                IIntermediateAssembly
        {
            private static Func<string, TAssembly> ctorDelegate;
            private static RuntimeMethodHandle ctorHandle;

            private static Func<string, TAssembly> CtorDelegete
            {
                get
                {
                    if (ctorDelegate == null)
                        ctorDelegate = ((ConstructorInfo)MethodInfo.GetMethodFromHandle(ctorHandle, typeof(TAssembly).TypeHandle)).BuildOptimizedConstructorDelegateEx<Func<string, TAssembly>>();
                    return ctorDelegate;
                }
            }

            static CreateAssemblyDynamicBridge()
            {
                var type = typeof(TAssembly);
                var ctor = LanguageMetaHelper.FindConstructor(type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic), typeof(string));
                if (ctor == null)
                    throw new ArgumentException("TAssembly");
                ctorHandle = ctor.MethodHandle;
            }

            #region ICreateAssemblyBridge<TAssembly> Members

            public TAssembly ctor(string name)
            {
                return CtorDelegete(name);
            }

            #endregion
        }
    }
}
