using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>Defines properties and methods for working with a service that determines </summary>
    public interface IParameterArrayDeterminationService :
        ILanguageService
    {
        bool SignatureSupportsParameterArray<TParent, TParameter>(IParameterParent<TParent, TParameter> signature, IAssembly associatedAssembly)
            where TParent :
                IParameterParent<TParent, TParameter>
            where TParameter :
                IParameterMember<TParent>;
        void ChangeSignatureParameterArraySupport<TParent, TIntermediateParent, TParameter, TIntermediateParameter>(IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter> target, IIntermediateAssembly associatedAssembly, bool support)
            where TParent :
                IParameterParent<TParent, TParameter>
            where TIntermediateParent :
                IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>,
                TParent
            where TParameter :
                IParameterMember<TParent>
            where TIntermediateParameter :
                IIntermediateParameterMember<TParent, TIntermediateParent>,
                TParameter;
    }
}
