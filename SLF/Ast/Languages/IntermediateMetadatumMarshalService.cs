using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal class IntermediateMetadatumMarshalService :
        CliMetadatumMarshalService
    {

        public IntermediateMetadatumMarshalService(ILanguageProvider provider)
            : base(provider)
        {
        }

        public sealed override void MakeMetadatum(IType targetMetadatumType)
        {
            if (targetMetadatumType == null)
                throw new ArgumentNullException("targetMetadatumType");
            if (targetMetadatumType is IIntermediateClassType)
            {
                var targetClass = (IIntermediateClassType)targetMetadatumType;
                var rootMetadatum = this.IdentityManager.ObtainTypeReference(this.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum)); ;
                if (rootMetadatum == null)
                    throw new InvalidOperationException("Root metadatum type not found.");
                if (!(rootMetadatum is IClassType))
                    throw new InvalidOperationException("Root metadatum type expected to be a class.");
                
                targetClass.BaseType = (IClassType)this.IdentityManager.ObtainTypeReference(this.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum));
            }
            else
                throw new ArgumentException("targetMetadatumType must be an intermediate class.", "targetMetadatumType");
        }

        public IIntermediateCliManager IdentityManager
        {
            get
            {
                return (IIntermediateCliManager)this.Provider.IdentityManager;
            }
        }

        public sealed override void MakeMetadatum(IType targetMetadatumType, IType baseMetadatumType)
        {
            if (!this.IsMetadatum(baseMetadatumType))
                throw new ArgumentException("baseMetadatumType must be a valid metadatum type.", "baseMetadatumType");
            if (!(targetMetadatumType is IIntermediateClassType))
                throw new ArgumentException("targetMetadatumType must be an intermediate class.", "targetMetadatumType");
            ((IIntermediateClassType)targetMetadatumType).BaseType = (IClassType)baseMetadatumType;
            throw new NotImplementedException();
        }

        public override void SetTargets(IType targetMetadatumType, MetadatumTargets targets)
        {
            throw new NotImplementedException();
        }
    }
}
