﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Services;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Provides a series of default language services.
    /// </summary>
    internal static class DefaultLanguageServices
    {
        /// <summary>
        /// The default intermediate class creator service.
        /// </summary>
        public static readonly IIntermediateTypeCtorLanguageService<IIntermediateClassType> IntermediateClassCreatorService = new DefaultIntermediateClassCreatorService();
        /// <summary>
        /// The default intermediate delegate creator service.
        /// </summary>
        public static readonly IIntermediateTypeCtorLanguageService<IIntermediateDelegateType> IntermediateDelegateCreatorService = new DefaultIntermediateDelegateCreatorService();
        /// <summary>
        /// The default intermediate enum creator service.
        /// </summary>
        public static readonly IIntermediateTypeCtorLanguageService<IIntermediateEnumType> IntermediateEnumCreatorService = new DefaultIntermediateEnumCreatorService();
        /// <summary>
        /// The default intermediate interface creator service.
        /// </summary>
        public static readonly IIntermediateTypeCtorLanguageService<IIntermediateInterfaceType> IntermediateInterfaceCreatorService = new DefaultIntermediateInterfaceCreatorService();
        /// <summary>
        /// The default struct data type creator service.
        /// </summary>
        public static readonly IIntermediateTypeCtorLanguageService<IIntermediateStructType> IntermediateStructCreatorService = new DefaultIntermediateStructCreatorService();

        internal static IIntermediateTypeCtorLanguageService<IIntermediateClassType> GetBoundIntermediateClassCreatorService(ILanguage language, ILanguageProvider provider)
        {
            return new DefaultIntermediateClassCreatorService(provider, language);
        }

        internal static IIntermediateTypeCtorLanguageService<IIntermediateDelegateType> GetBoundIntermediateDelegateCreatorService(ILanguage language, ILanguageProvider provider)
        {
            return new DefaultIntermediateDelegateCreatorService(provider, language);
        }

        internal static IIntermediateTypeCtorLanguageService<IIntermediateEnumType> GetBoundIntermediateEnumCreatorService(ILanguage language, ILanguageProvider provider)
        {
            return new DefaultIntermediateEnumCreatorService(provider, language);
        }

        internal static IIntermediateTypeCtorLanguageService<IIntermediateInterfaceType> GetBoundIntermediateInterfaceCreatorService(ILanguage language, ILanguageProvider provider)
        {
            return new DefaultIntermediateInterfaceCreatorService(provider, language);
        }

        internal static IIntermediateTypeCtorLanguageService<IIntermediateStructType> GetBoundIntermediateStructCreatorService(ILanguage language, ILanguageProvider provider)
        {
            return new DefaultIntermediateStructCreatorService(provider, language);
        }

        internal static ILanguageService GetBoundIntermediateAssemblyCreatorService<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity>(TLanguage language, TProvider provider)
            where TLanguage :
                ILanguage
            where TProvider :
                ILanguageProvider
            where TIdentityManager :
                IIdentityManager<TTypeIdentity, TAssemblyIdentity>,
                IIntermediateIdentityManager
        {
            return new DefaultIntermediateAssemblyCreatorService<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity>(provider, language);
        }

        private class DefaultIntermediateClassCreatorService :
            IIntermediateTypeCtorLanguageService<IIntermediateClassType>
        {
            private ILanguageProvider provider;
            private ILanguage language;
            internal DefaultIntermediateClassCreatorService()
            {
            }

            internal DefaultIntermediateClassCreatorService(ILanguageProvider provider, ILanguage language)
            {
                this.language = language;
                this.provider = provider;
            }

            #region IIntermediateTypeCtorLanguageService<IIntermediateClassType> Members

            public IIntermediateClassType New(string name, IIntermediateTypeParent parent)
            {
                return new IntermediateClassType(name, parent);
            }

            #endregion

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

        }

        private class DefaultIntermediateDelegateCreatorService :
            IIntermediateTypeCtorLanguageService<IIntermediateDelegateType>
        {
            private ILanguageProvider provider;
            private ILanguage language;
            internal DefaultIntermediateDelegateCreatorService()
            {
            }

            internal DefaultIntermediateDelegateCreatorService(ILanguageProvider provider, ILanguage language)
            {
                this.language = language;
                this.provider = provider;
            }

            #region IIntermediateTypeCtorLanguageService<IIntermediateDelegateType> Members

            public IIntermediateDelegateType New(string name, IIntermediateTypeParent parent)
            {
                return new IntermediateDelegateType(name, parent);
            }

            #endregion

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

        private class DefaultIntermediateEnumCreatorService :
            IIntermediateTypeCtorLanguageService<IIntermediateEnumType>
        {
            private ILanguageProvider provider;
            private ILanguage language;
            internal DefaultIntermediateEnumCreatorService()
            {
            }

            internal DefaultIntermediateEnumCreatorService(ILanguageProvider provider, ILanguage language)
            {
                this.language = language;
                this.provider = provider;
            }

            #region IIntermediateTypeCtorLanguageService<IIntermediateEnumType> Members

            public IIntermediateEnumType New(string name, IIntermediateTypeParent parent)
            {
                return new IntermediateEnumType(name, parent);
            }

            #endregion

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

        private class DefaultIntermediateInterfaceCreatorService :
            IIntermediateTypeCtorLanguageService<IIntermediateInterfaceType>
        {
            private ILanguageProvider provider;
            private ILanguage language;
            internal DefaultIntermediateInterfaceCreatorService()
            {
            }

            internal DefaultIntermediateInterfaceCreatorService(ILanguageProvider provider, ILanguage language)
            {
                this.language = language;
                this.provider = provider;
            }

            #region IIntermediateTypeCtorLanguageService<IIntermediateInterfaceType> Members

            public IIntermediateInterfaceType New(string name, IIntermediateTypeParent parent)
            {
                return new IntermediateInterfaceType(name, parent);
            }

            #endregion

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

        private class DefaultIntermediateStructCreatorService :
            IIntermediateTypeCtorLanguageService<IIntermediateStructType>
        {
            private ILanguageProvider provider;
            private ILanguage language;
            internal DefaultIntermediateStructCreatorService()
            {
            }

            internal DefaultIntermediateStructCreatorService(ILanguageProvider provider, ILanguage language)
            {
                this.language = language;
                this.provider = provider;
            }

            #region IIntermediateTypeCtorLanguageService<IIntermediateStructType> Members

            public IIntermediateStructType New(string name, IIntermediateTypeParent parent)
            {
                return new IntermediateStructType(name, parent);
            }

            #endregion

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

        }

        private class DefaultIntermediateAssemblyCreatorService<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity> :
            IIntermediateAssemblyCtorLanguageService
            where TLanguage :
                ILanguage
            where TProvider :
                ILanguageProvider
            where TIdentityManager :
                IIdentityManager<TTypeIdentity, TAssemblyIdentity>,
                IIntermediateIdentityManager
        {
            #region IIntermediateAssemblyCtorLanguageService Members

            public IIntermediateAssembly New(string name)
            {
                return new IntermediateAssembly<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity>(name, this.Provider, this.Language);
            }

            #endregion

            #region ILanguageService Members

            ILanguageProvider ILanguageService.Provider
            {
                get { return this.Provider; }
            }

            ILanguage ILanguageService.Language
            {
                get { return this.Language; }
            }

            public TLanguage Language { get; private set; }

            public TProvider Provider { get; private set; }

            public Guid ServiceGuid
            {
                get { return LanguageGuids.Services.IntermediateAssemblyCreatorService; }
            }

            #endregion

            public DefaultIntermediateAssemblyCreatorService(TProvider provider, TLanguage language)
            {
                this.Provider = provider;
                this.Language = language;
            }

            IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
            {
                get
                {
                    return this.Provider;
                }
            }

        }


    }
}
