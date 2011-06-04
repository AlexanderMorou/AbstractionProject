using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Languages;
/*---------------------------------------------------------------------\
| Copyright © 2011 Allen Copeland Jr.                                  |
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

            #region ICreateAssemblyBridge<TAssembly> Members


            public TAssembly ctor<TLanguage, TProvider>(string name, TProvider provider)
                where TLanguage : 
                    ILanguage
                where TProvider : 
                    ILanguageProvider
            {
                throw new NotSupportedException();
            }

            #endregion
        }
    }
}
