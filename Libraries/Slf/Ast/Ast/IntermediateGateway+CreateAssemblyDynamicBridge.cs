using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Languages;
#if TYPESYSTEM_CLI
//using AllenCopeland.Abstraction.Slf.Cli;
#endif
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
        private class CreateAssemblyDynamicBridge<TAssembly> :
            ICreateAssemblyBridge<TAssembly>
            where TAssembly :
                IIntermediateAssembly
        {
            private static Func<string, TAssembly> ctorDelegate;
            #if TYPESYSTEM_CLI
            private static RuntimeMethodHandle ctorHandle;
            #endif
            private static Func<string, TAssembly> CtorDelegete
            {
                get
                {
                    if (ctorDelegate == null)
                        ctorDelegate = ((ConstructorInfo)MethodInfo.GetMethodFromHandle(ctorHandle, typeof(TAssembly).TypeHandle)).BuildOptimizedConstructorDelegate<Func<string, TAssembly>>();
                    return ctorDelegate;
                }
            }

            static CreateAssemblyDynamicBridge()
            {
                var type = typeof(TAssembly);

                //#if TYPESYSTEM_CLI //Common Language Infrastructure

                //var typeRef = type.GetTypeReference();
                //if (typeRef.Type != Abstract.TypeKind.Class)
                //    throw new ArgumentException("TAssembly");
                //var classRef = (ICompiledClassType)typeRef;
                //var ctor = classRef.Constructors.Find(true, CommonTypeRefs.String).Values.First() as ICompiledCtorMember;
                //if (ctor == null)
                //    throw new ArgumentException("TAssembly");
                //ctorHandle = ctor.MemberInfo.MethodHandle;

                //#endif

                var ctor = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, new Type[] { typeof(string) }, null);
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
