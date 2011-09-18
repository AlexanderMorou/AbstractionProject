using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal partial class _TypeParameterDictionary<TGenericParameter, TParent, TInternalParent> :
        _DeclarationsBase<IGenericParameter, TGenericParameter, TParent, IGenericParameterDictionary<TGenericParameter, TParent>>
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
        where TInternalParent :
            TParent
    {
        protected _TypeParameterDictionary(TInternalParent parent, IGenericParameterDictionary<TGenericParameter, TParent> original)
            : base(parent, original)
        {
        }

        protected override TGenericParameter GetWrapper(TGenericParameter original, TParent parent)
        {
            throw new NotImplementedException();
        }
    }
}
