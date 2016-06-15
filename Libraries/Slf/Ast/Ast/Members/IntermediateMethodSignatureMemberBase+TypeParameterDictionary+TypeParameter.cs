using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
//using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignatureParameter
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignature
        where TParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
        where TIntermediateParent :
            IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
            TParent
    {
        partial class TypeParameterDictionary
        {
            private class TypeParameter :
                IntermediateGenericParameterBase<IMethodSignatureGenericTypeParameterMember, IIntermediateMethodSignatureGenericTypeParameterMember, IMethodSignatureMember, IIntermediateMethodSignatureMember>,
                IIntermediateMethodSignatureGenericTypeParameterMember
            {
                private IGenericParameterUniqueIdentifier uniqueIdentifier;
                public TypeParameter(string name, IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> parent)
                    : base(name, parent, parent.IdentityManager)
                {
                }

                protected override bool Equals(IMethodSignatureGenericTypeParameterMember other)
                {
                    return object.ReferenceEquals(other, this);
                }

                protected override IGenericParameterUniqueIdentifier OnGetUniqueIdentifier()
                {
                    if (this.uniqueIdentifier == null)
                    {
                        if (this.Position > -1)
                            this.uniqueIdentifier = TypeSystemIdentifiers.GetGenericParameterIdentifier(this.Position, this.Name, false);
                        else
                            this.uniqueIdentifier = TypeSystemIdentifiers.GetGenericParameterIdentifier(this.Name, false);
                    }
                    return this.uniqueIdentifier;
                }

                protected override void ClearIdentifier()
                {
                    lock (this.SyncObject)
                        if (this.uniqueIdentifier != null)
                            this.uniqueIdentifier = null;
                }
            }
        }
    }
}
