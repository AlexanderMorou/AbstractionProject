using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class IntermediateCliClassTypeCreatorService :
        IIntermediateTypeCtorLanguageService<IIntermediateClassType>
    {
        private ILanguage language;
        private ILanguageProvider provider;
        internal IntermediateCliClassTypeCreatorService(ILanguageProvider provider, ILanguage language)
        {
            this.language = language;
            this.provider = provider;
        }

        public IIntermediateClassType New(string name, IIntermediateTypeParent parent)
        {
            return new IntermediateCliClassType(name, parent);
        }

        #region ILanguageService Members

        public ILanguageProvider Provider
        {
            get { return this.provider; }
        }

        public ILanguage Language
        {
            get { return this.language; }
        }

        public Guid ServiceGuid
        {
            get { return LanguageGuids.Services.ClassServices.ClassCreatorService; }
        }

        #endregion

        IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
        {
            get
            {
                return this.Provider;
            }
        }
    };

    public class IntermediateCliStructTypeCreatorService :
        IIntermediateTypeCtorLanguageService<IIntermediateStructType>
    {
        private ILanguage language;
        private ILanguageProvider provider;
        internal IntermediateCliStructTypeCreatorService(ILanguageProvider provider, ILanguage language)
        {
            this.language = language;
            this.provider = provider;
        }

        public IIntermediateStructType New(string name, IIntermediateTypeParent parent)
        {
            return new IntermediateCliStructType(name, parent);
        }

        #region ILanguageService Members

        public ILanguageProvider Provider
        {
            get { return this.provider; }
        }

        public ILanguage Language
        {
            get { return this.language; }
        }

        public Guid ServiceGuid
        {
            get { return LanguageGuids.Services.StructServices.StructCreatorService; }
        }

        #endregion

        IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
        {
            get
            {
                return this.Provider;
            }
        }
    };

    public class IntermediateCliInterfaceTypeCreatorService :
        IIntermediateTypeCtorLanguageService<IIntermediateInterfaceType>
    {
        private ILanguage language;
        private ILanguageProvider provider;
        internal IntermediateCliInterfaceTypeCreatorService(ILanguageProvider provider, ILanguage language)
        {
            this.language = language;
            this.provider = provider;
        }

        public IIntermediateInterfaceType New(string name, IIntermediateTypeParent parent)
        {
            return new IntermediateCliInterfaceType(name, parent);
        }

        #region ILanguageService Members

        public ILanguageProvider Provider
        {
            get { return this.provider; }
        }

        public ILanguage Language
        {
            get { return this.language; }
        }

        public Guid ServiceGuid
        {
            get { return LanguageGuids.Services.InterfaceServices.InterfaceCreatorService; }
        }

        #endregion

        IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
        {
            get
            {
                return this.Provider;
            }
        }
    }

    public class IntermediateCliDelegateTypeCreatorService :
        IIntermediateTypeCtorLanguageService<IIntermediateDelegateType>
    {
        private ILanguage language;
        private ILanguageProvider provider;
        internal IntermediateCliDelegateTypeCreatorService(ILanguageProvider provider, ILanguage language)
        {
            this.language = language;
            this.provider = provider;
        }

        public IIntermediateDelegateType New(string name, IIntermediateTypeParent parent)
        {
            return new IntermediateCliDelegateType(name, parent);
        }

        #region ILanguageService Members

        public ILanguageProvider Provider
        {
            get { return this.provider; }
        }

        public ILanguage Language
        {
            get { return this.language; }
        }

        public Guid ServiceGuid
        {
            get { return LanguageGuids.Services.IntermediateDelegateCreatorService; }
        }

        #endregion

        IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
        {
            get
            {
                return this.Provider;
            }
        }
    }

    public class IntermediateCliEnumTypeCreatorService :
        IIntermediateTypeCtorLanguageService<IIntermediateEnumType>
    {
        private ILanguage language;
        private ILanguageProvider provider;
        internal IntermediateCliEnumTypeCreatorService(ILanguageProvider provider, ILanguage language)
        {
            this.language = language;
            this.provider = provider;
        }

        public IIntermediateEnumType New(string name, IIntermediateTypeParent parent)
        {
            return new IntermediateCliEnumType(name, parent);
        }

        #region ILanguageService Members

        public ILanguageProvider Provider
        {
            get { return this.provider; }
        }

        public ILanguage Language
        {
            get { return this.language; }
        }

        public Guid ServiceGuid
        {
            get { return LanguageGuids.Services.IntermediateEnumCreatorService; }
        }

        #endregion

        IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
        {
            get
            {
                return this.Provider;
            }
        }
    }

}
