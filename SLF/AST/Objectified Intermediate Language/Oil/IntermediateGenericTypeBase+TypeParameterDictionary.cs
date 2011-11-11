using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType>
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier<TTypeIdentifier>,
            IGeneralDeclarationUniqueIdentifier
        where TType :
            class,
            IGenericType<TTypeIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
            TType
    {
        protected partial class TypeParameterDictionary :
            IntermediateGenericParameterDictionary<IGenericTypeParameter<TTypeIdentifier, TType>, IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>, TType, TIntermediateType>
        {
            protected internal TypeParameterDictionary(IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType> parent)
                : base((TIntermediateType)(object)parent)
            {
            }

            private new IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType> Parent
            {
                get
                {
                    return (IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType>)(object)base.Parent;
                }
            }

            private _IIntermediateGenericType<TTypeIdentifier> _Parent
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

            protected override IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType> GetNew(string name)
            {
                return new TypeParameter(name, (IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType>)(object)this.Parent);
            }

            protected internal override void _Add(IGenericParameterUniqueIdentifier key, IGenericTypeParameter<TTypeIdentifier, TType> value)
            {
                var oldIdentifier = this.Parent.UniqueIdentifier;
                _Parent.ItemAdded(value);
                base._Add(key, value);
                this._Parent.CardinalityChanged(oldIdentifier);
            }

            protected internal override bool _Remove(int index)
            {
                if (index < 0 || index >= this.Count)
                    return false;
                var oldIdentifier = this.Parent.UniqueIdentifier;
                _Parent.ItemRemoved(base[index].Value);
                try
                {
                    return base._Remove(index);
                }
                finally
                {
                    this._Parent.CardinalityChanged(oldIdentifier);
                }
            }

        }
    }
}
