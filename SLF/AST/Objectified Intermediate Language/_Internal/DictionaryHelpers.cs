using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal
{
    internal static class DictionaryHelpers
    {
        internal static TIntermediateSignature 
            AddIntermediateMethodByDelegate<
                TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
            (
                string name, 
                IDelegateType signature,
                Func<string, TIntermediateSignature> addHelper,
                Func<TypedName, TypedNameSeries, TIntermediateSignature> addHelperAlt
            )
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TIntermediateSignatureParameter :
                TSignatureParameter,
                IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TIntermediateSignature :
                TSignature,
                IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
            where TIntermediateSignatureParent :
                TSignatureParent,
                IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        {
            /* *
             * If the method being added wants to mirror the declaration 
             * of the delegate, and the delegate is a generic type with
             * no specific type references added: mirror the definition
             * down to the generic parameters.
             * */
            if (signature.IsGenericConstruct && signature.IsGenericDefinition && signature.DeclaringType == null)
            {
                var method = addHelper(name);
                var genericParameters = new IIntermediateGenericParameter[signature.TypeParameters.Count];
                var originalGenericParameters = signature.TypeParameters.Values.ToArray();

                for (int i = 0; i < originalGenericParameters.Length; i++)
                    genericParameters[i] = method.TypeParameters.Add(originalGenericParameters[i].Name);
                var genericParameterCollection = new LockedTypeCollection(genericParameters);

                for (int i = 0; i < originalGenericParameters.Length; i++)
                {
                    var originalGenericParameter = originalGenericParameters[i];
                    var currentGenericParameter = genericParameters[i];
                    foreach (var constraint in originalGenericParameter.Constraints)
                        currentGenericParameter.Constraints.Add(constraint.Disambiguify(genericParameterCollection, null, TypeParameterSources.Type));
                    currentGenericParameter.SpecialConstraint = originalGenericParameter.SpecialConstraint;
                }

                method.Parameters.AddRange((from p in signature.Parameters.Values
                                            let paramType = p.ParameterType.Disambiguify(genericParameterCollection, null, TypeParameterSources.Type)
                                            select new TypedName(p.Name, paramType)).ToArray());
                method.ReturnType = signature.ReturnType.Disambiguify(genericParameterCollection, null, TypeParameterSources.Type);
                return method;
            }
            /* *
             * Otherwise, just copy the types defined in the parameters.
             * */
            else if (!(signature.IsGenericConstruct && signature.IsGenericDefinition))
                return addHelperAlt(new TypedName(name, signature.ReturnType), new TypedNameSeries((from p in signature.Parameters.Values
                                                                                                    select new TypedName(p.Name, p.ParameterType)).ToArray()));
            else
                throw new NotSupportedException("Generic type provided must be a top-level type.");
        }
    }
}
