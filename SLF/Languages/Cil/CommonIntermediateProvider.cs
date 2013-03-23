using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.Cil
{
    /// <summary>
    /// Provides a base implementation for a common intermediate language provider.
    /// </summary>
    public partial class CommonIntermediateProvider :
        LanguageProvider<ICommonIntermediateLanguage, ICommonIntermediateProvider, IIntermediateCliManager, Type, Assembly>,
        ICommonIntermediateProvider
    {
        internal CommonIntermediateProvider(IIntermediateCliManager identityManager)
            : base(identityManager)
        {
            this.RegisterService<IIntermediateAssemblyCtorLanguageService>(LanguageGuids.Services.IntermediateAssemblyCreatorService, new AssemblyService(this));
        }

        #region ICommonIntermediateProvider Members

        public new ICommonIntermediateAssembly CreateAssembly(string name)
        {
            return (ICommonIntermediateAssembly)base.CreateAssembly(name);
        }

        #endregion

        protected override ICommonIntermediateLanguage OnGetLanguage()
        {
            return CommonIntermediateLanguage.Singleton;
        }

        public new IIntermediateCliManager IdentityManager
        {
            get
            {
                return (IIntermediateCliManager)base.IdentityManager;
            }
        }
    }
}
