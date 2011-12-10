using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.Cil
{
    public partial class CommonIntermediateProvider :
        LanguageProvider<ICommonIntermediateLanguage, ICommonIntermediateProvider>,
        ICommonIntermediateProvider
    {
        internal CommonIntermediateProvider()
        {
            this.RegisterService<IIntermediateAssemblyCtorLanguageService>(LanguageGuids.ConstructorServices.IntermediateAssemblyCreatorService, new AssemblyService(this));
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
    }
}
