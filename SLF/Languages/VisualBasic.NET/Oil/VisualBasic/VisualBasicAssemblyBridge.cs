﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;

namespace AllenCopeland.Abstraction.Slf.Oil.VisualBasic
{
    internal class VisualBasicAssemblyBridge :
        IntermediateGateway.ICreateAssemblyBridge<VisualBasicAssembly>
    {
        public static readonly IntermediateGateway.ICreateAssemblyBridge<VisualBasicAssembly> Singleton = new VisualBasicAssemblyBridge();
        private static bool isRegistered = false;
        private VisualBasicAssemblyBridge() { }

        #region ICreateAssemblyBridge<VisualBasicAssembly> Members

        public VisualBasicAssembly ctor(string name)
        {
            return ctor<IVisualBasicLanguage, IVisualBasicProvider>(name, VisualBasicLanguage.Singleton.GetProvider(VisualBasicLanguage.DefaultVersion));
        }

        public VisualBasicAssembly ctor<TLanguage, TProvider>(string name, TProvider provider)
            where TLanguage : 
                ILanguage
            where TProvider : 
                ILanguageProvider
        {
            if (typeof(IVisualBasicLanguage).IsAssignableFrom(typeof(TLanguage)))
                if (typeof(IVisualBasicProvider).IsAssignableFrom(typeof(TProvider)))
                    return new VisualBasicAssembly(name, (IVisualBasicProvider)provider);
            throw new NotSupportedException();
        }

        #endregion

        internal static void Register()
        {
            if (!VisualBasicAssemblyBridge.isRegistered)
            {
                IntermediateGateway.RegisterCreateAssemblyBridge<VisualBasicAssembly>(VisualBasicAssemblyBridge.Singleton);
                VisualBasicAssemblyBridge.isRegistered = true;
            }
        }
    }
}