using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cst;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    /// <summary>
    /// Defines properties and methods for working with a Visual Basic
    /// assembly defined for the Common Language Infrastructure.
    /// </summary>
    public abstract class VisualBasicAssembly<TAssembly, TProvider, TInstanceAssembly> :
        IntermediateCliAssembly<IVisualBasicLanguage, TProvider, TInstanceAssembly, IIntermediateCliManager, Type, Assembly>,
        IVisualBasicAssembly<TAssembly, TProvider>
        where TInstanceAssembly :
            VisualBasicAssembly<TAssembly, TProvider, TInstanceAssembly>,
            TAssembly
        where TAssembly :
            IVisualBasicAssembly<TAssembly, TProvider>
        where TProvider :
            IVersionedLanguageProvider<VisualBasicVersion>,            
            ILanguageProvider
    {
        private TProvider provider;
        protected VisualBasicAssembly(string name, TProvider provider, IIntermediateCliRuntimeEnvironmentInfo runtimeEnvironment)
            : base(name, runtimeEnvironment)
        {
            this.provider = provider;
        }

        protected VisualBasicAssembly(TInstanceAssembly root)
            : base(root)
        {
        }

        #region IIntermediateAssembly<IVisualBasicLanguage,IVisualBasicStart,IVisualBasicProvider> Members

        public override IVisualBasicLanguage Language
        {
            get { return (IVisualBasicLanguage)this.Provider.Language; }
        }

        public override TProvider Provider
        {
            get {
                if (this.IsRoot)
                    return this.provider;
                else
                    return this.GetRoot().provider;
            }
        }

        #endregion


    }
}
