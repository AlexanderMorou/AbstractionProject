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
using AllenCopeland.Abstraction.Slf._Internal.Languages;
using AllenCopeland.Abstraction.Slf.Ast.Cli;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    internal partial class MyVisualBasicProvider :
        VersionedLanguageProvider<IVisualBasicLanguage, IMyVisualBasicProvider, VisualBasicVersion, IIntermediateCliManager, Type, Assembly>,
        //IVisualBasicProvider<TAssembly, TProvider>
        //VisualBasicProvider<IMyVisualBasicAssembly, IMyVisualBasicProvider>,
        IMyVisualBasicProvider
    {
        internal MyVisualBasicProvider(VisualBasicVersion version, IIntermediateCliManager identityManager)
            : base(version, identityManager)
        {
            this.RegisterService<IIntermediateAssemblyCtorLanguageService<IMyVisualBasicProvider, IVisualBasicLanguage, IMyVisualBasicAssembly>>(LanguageGuids.Services.IntermediateAssemblyCreatorService, new AssemblyService(this));
            this.RegisterService<IIntermediateTypeCtorLanguageService<IIntermediateClassType>>(LanguageGuids.Services.ClassServices.ClassCreatorService, new ClassService(this));
            this.RegisterService<IMetadatumMarshalService>(LanguageGuids.Services.MetadatumMarshalService, new IntermediateMetadatumMarshalService(this));
        }

        protected override IVisualBasicLanguage OnGetLanguage()
        {
            return VisualBasicLanguage.Singleton;
        }

        #region IMyVisualBasicProvider Members

        /// <summary>
        /// Creates a new <see cref="IMyVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="IMyVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        public new IMyVisualBasicAssembly CreateAssembly(string name)
        {
            return (IMyVisualBasicAssembly)base.CreateAssembly(name);
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
