using AllenCopeland.Abstraction.Slf._Internal.Languages;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public abstract class CliVersionedLanguageProvider<
            TLanguage, TProvider, TVersion,
            TIdentityManager, TTypeIdentity,
            TAssemblyIdentity> :
        VersionedLanguageProvider<TLanguage, TProvider, TVersion, TIdentityManager, TTypeIdentity, TAssemblyIdentity>,
        ICliLanguageProvider<TLanguage, TProvider>
        where TLanguage :
            IVersionedLanguage<TVersion>,
            ILanguage<TLanguage, TProvider>
        where TProvider :
            ICliLanguageProvider<TLanguage, TProvider>
        where TIdentityManager :
            IIdentityManager<TTypeIdentity, TAssemblyIdentity>,
            IIntermediateIdentityManager
    {
        protected CliVersionedLanguageProvider(TVersion version, TIdentityManager identityManager)
            : base(version, identityManager)
        {
            this.RegisterService<IMetadatumMarshalService>(LanguageGuids.Services.MetadatumMarshalService, new IntermediateMetadatumMarshalService(this));
            this.RegisterService<IParameterArrayDeterminationService>(LanguageGuids.Services.ParameterArrayDeterminationService, new CliParameterArrayDeterminationService(this.Language, this));
            this.RegisterService(LanguageGuids.Services.ClassServices.ClassCreatorService, new IntermediateCliClassTypeCreatorService(this, this.Language));
            this.RegisterService(LanguageGuids.Services.IntermediateDelegateCreatorService, new IntermediateCliDelegateTypeCreatorService(this, this.Language));
            this.RegisterService(LanguageGuids.Services.IntermediateEnumCreatorService, new IntermediateCliEnumTypeCreatorService(this, this.Language));
            this.RegisterService(LanguageGuids.Services.InterfaceServices.InterfaceCreatorService, new IntermediateCliInterfaceTypeCreatorService(this, this.Language));
            this.RegisterService(LanguageGuids.Services.StructServices.StructCreatorService, new IntermediateCliStructTypeCreatorService(this, this.Language));
        }
    }
}
