using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class CliParameterArrayDeterminationService :
        ParameterArrayDeterminationServiceBase
    {
        public CliParameterArrayDeterminationService(ILanguage language, ILanguageProvider provider) : base(language, provider) { }

        public ICliManager IdentityManager 
        {
            get
            {
                return this.Provider.IdentityManager as ICliManager;
            }
        }

        public override bool SignatureSupportsParameterArray<TParent, TParameter>(IParameterParent<TParent, TParameter> signature, IAssembly associatedAssembly)
        {
            if (signature.Parameters.Count == 0)
                return false;
            IParameterMember<TParent> lastParameter = signature.Parameters.Values[signature.Parameters.Count - 1];
            if (lastParameter != null)
                return lastParameter.IsDefined(this.IdentityManager.ObtainTypeReference(this.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.ParamArrayMetadatum, associatedAssembly)));
            return false;
        }

        public override void ChangeSignatureParameterArraySupport<TParent, TIntermediateParent, TParameter, TIntermediateParameter>(IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter> target, IIntermediateAssembly associatedAssembly, bool support)
        {
            var metadatumType = this.IdentityManager.ObtainTypeReference(this.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.ParamArrayMetadatum, associatedAssembly));
            if (metadatumType == null)
                return;
            if (target.Parameters.Count == 0)
                return;
            IIntermediateParameterMember<TParent, TIntermediateParent> lastParameter = target.Parameters.Values[target.Parameters.Count - 1];
            if (SignatureSupportsParameterArray(target, associatedAssembly))
            {
                if (support)
                    return;
                var metadatum = lastParameter.Metadata[metadatumType];
                lastParameter.Metadata.Remove(metadatum);
                return;
            }
            else if (support)
                lastParameter.Metadata.Add(new MetadatumDefinitionParameterValueCollection(metadatumType));

        }
    }
}
