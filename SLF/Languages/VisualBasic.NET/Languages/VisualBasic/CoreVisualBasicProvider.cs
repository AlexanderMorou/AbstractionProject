using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Compilers;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    internal partial class CoreVisualBasicProvider :
        VersionedLanguageProvider<IVisualBasicLanguage, ICoreVisualBasicProvider, VisualBasicVersion, IIntermediateCliManager, Type, Assembly>,
        //IVisualBasicProvider<TAssembly, TProvider>
        //VisualBasicProvider<ICoreVisualBasicAssembly, ICoreVisualBasicProvider>,
        ICoreVisualBasicProvider
    {
        internal CoreVisualBasicProvider(VisualBasicVersion version, ITypeIdentityManager identityManager)
            : base(version, identityManager)
        {
            this.RegisterService<IIntermediateAssemblyCtorLanguageService<ICoreVisualBasicProvider, IVisualBasicLanguage, ICoreVisualBasicAssembly>>(LanguageGuids.Services.IntermediateAssemblyCreatorService, new AssemblyService(this));
            this.RegisterService<IMetadatumMarshalService>(LanguageGuids.Services.MetadatumMarshalService, new IntermediateMetadatumMarshalService(this));
        }

        protected override IVisualBasicLanguage OnGetLanguage()
        {
            return VisualBasicLanguage.Singleton;
        }

        #region ICoreVisualBasicProvider Members

        /// <summary>
        /// Creates a new <see cref="ICoreVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="ICoreVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        public new ICoreVisualBasicAssembly CreateAssembly(string name)
        {
            return (ICoreVisualBasicAssembly)base.CreateAssembly(name);
        }

        public IIntermediateCliManager IdentityManager
        {
            get
            {
                return (IIntermediateCliManager)base.IdentityManager;
            }
        }

        #endregion

    }
}
