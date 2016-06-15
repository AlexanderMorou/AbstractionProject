using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public abstract class ParameterArrayDeterminationServiceBase :
        IParameterArrayDeterminationService
    {

        public ParameterArrayDeterminationServiceBase(ILanguage language, ILanguageProvider provider)
        {
            this.Language = language;
            this.Provider = provider;
        }

        public ILanguageProvider Provider { get; private set; }

        public ILanguage Language { get; private set; }

        IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
        {
            get { return this.Provider; }
        }

        public Guid ServiceGuid
        {
            get { return LanguageGuids.Services.ParameterArrayDeterminationService; }
        }


        public abstract bool SignatureSupportsParameterArray<TParent, TParameter>(IParameterParent<TParent, TParameter> signature, IAssembly associatedAssembly)
            where TParent :
                IParameterParent<TParent, TParameter>
            where TParameter :
                IParameterMember<TParent>;

        public abstract void ChangeSignatureParameterArraySupport<TParent, TIntermediateParent, TParameter, TIntermediateParameter>(IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter> target, IIntermediateAssembly associatedAssembly, bool support)
            where TParent :
                IParameterParent<TParent, TParameter>
            where TIntermediateParent :
                IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>, TParent
            where TParameter :
                IParameterMember<TParent>
            where TIntermediateParameter :
                IIntermediateParameterMember<TParent, TIntermediateParent>, TParameter;
    }
}
