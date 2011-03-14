using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateGenericTypeBase<TType, TIntermediateType>
        where TType :
            class,
            IGenericType<TType>
        where TIntermediateType :
            class,
            TType,
            IIntermediateGenericType<TType, TIntermediateType>
    {
        protected partial class TypeParameterDictionary :
            IntermediateGenericParameterDictionary<IGenericTypeParameter<TType>, IIntermediateGenericTypeParameter<TType, TIntermediateType>, TType, TIntermediateType>
        {
            protected internal TypeParameterDictionary(IntermediateGenericTypeBase<TType, TIntermediateType> parent)
                : base((TIntermediateType)(object)parent)
            {
            }

            private new IntermediateGenericTypeBase<TType, TIntermediateType> Parent
            {
                get
                {
                    return (IntermediateGenericTypeBase<TType, TIntermediateType>)(object)base.Parent;
                }
            }

            private _IIntermediateGenericType _Parent
            {
                get
                {
                    return this.Parent;
                }
            }
            protected override void OnRearranged(GenericParameterMovedEventArgs e)
            {
                this.Parent.OnRearranged(e);
                base.OnRearranged(e);
            }

            protected override IIntermediateGenericTypeParameter<TType, TIntermediateType> GetNew(string name)
            {
                return new TypeParameter(name, (IntermediateGenericTypeBase<TType, TIntermediateType>)(object)this.Parent);
            }

            protected internal override void _Add(string key, IGenericTypeParameter<TType> value)
            {
                _Parent.ItemAdded(value);
                base._Add(key, value);
            }

            protected internal override bool _Remove(int index)
            {
                if (index < 0 || index >= this.Count)
                    return false;
                _Parent.ItemRemoved(base[index].Value);
                return base._Remove(index);
            }

        }
    }
}
