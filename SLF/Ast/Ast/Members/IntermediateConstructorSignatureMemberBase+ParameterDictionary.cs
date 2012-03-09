using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class IntermediateConstructorSignatureMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableParent<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
        protected class ParameterDictionary :
            IntermediateParameterMemberDictionary<TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>>
        {

            public ParameterDictionary(IntermediateConstructorSignatureMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType> parent)
                : base((TIntermediateCtor)(object)parent)
            {
            }

            private IntermediateConstructorSignatureMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType> _Parent
            {
                get
                {
                    return (IntermediateConstructorSignatureMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType>)(object)base.Parent;
                }
            }

            protected override IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType> GetNewParameter(string name, IType parameterType, ParameterDirection direction)
            {
                Parameter result = new Parameter(Parent) { Direction = direction, ParameterType = parameterType };
                result.AssignName(name);
                return result;
            }
            protected internal override void _Add(IGeneralMemberUniqueIdentifier key, IConstructorParameterMember<TCtor, TType> value)
            {
                if (this._Parent.typeInitializer)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                base._Add(key, value);
            }

            protected override void _AddRange(KeyValuePair<IGeneralMemberUniqueIdentifier, IConstructorParameterMember<TCtor, TType>>[] elements)
            {
                if (this._Parent.typeInitializer)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                base._AddRange(elements);
            }

            protected internal override bool _Remove(int index)
            {
                if (this._Parent.typeInitializer)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                return base._Remove(index);
            }
        }
    }
}
