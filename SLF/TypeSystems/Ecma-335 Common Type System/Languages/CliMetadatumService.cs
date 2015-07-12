using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal abstract class CliMetadatumMarshalService :
        IMetadatumMarshalService
    {
        internal CliMetadatumMarshalService(ILanguageProvider provider)
        {
            this.Provider = provider;
        }

        public abstract void MakeMetadatum(IType targetMetadatumType);

        public abstract void MakeMetadatum(IType targetMetadatumType, IType baseMetadatumType);

        public abstract void SetTargets(IType targetMetadatumType, MetadatumTargets targets);

        public virtual MetadatumTargets GetTargets(IType metadatumType)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsMetadatum(IType metadatumType)
        {
            var identityManager = metadatumType.IdentityManager;
            if (identityManager is ICliManager)
            {
                var cliManager = (ICliManager)identityManager;
                var metadatumRoot = cliManager.ObtainTypeReference(cliManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum), metadatumType.Assembly);
                return metadatumRoot != null && metadatumRoot.IsAssignableFrom(metadatumType);
            }
            return false;
        }

        public ILanguageProvider Provider { get; private set; }

        public ILanguage Language
        {
            get { return this.Provider.Language; }
        }

        public Guid ServiceGuid
        {
            get { return AbstractGateway.MetadatumMarshalServiceGuid; }
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
