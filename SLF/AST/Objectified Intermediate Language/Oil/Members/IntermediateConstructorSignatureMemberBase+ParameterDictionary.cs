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

namespace AllenCopeland.Abstraction.Slf.Oil.Members
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
            
            public ParameterDictionary(TIntermediateCtor parent)
                : base(parent)
            {
                if (!(parent is IntermediateConstructorSignatureMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType>))
                    throw new ArgumentException("Argument is of an invalid type.", "parent");
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
                Parameter p = new Parameter(Parent);
                p.Name = name;
                p.Direction = direction;
                p.ParameterType = parameterType;
                return p;
            }
            protected internal override void _Add(string key, IConstructorParameterMember<TCtor, TType> value)
            {
                if (this._Parent.typeInitializer)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                base._Add(key, value);
            }

            protected override void _AddRange(KeyValuePair<string, IConstructorParameterMember<TCtor, TType>>[] elements)
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
