using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public abstract class IntermediateGenericTypeDictionary<TTypeIdentifier, TType, TIntermediateType> :
        IntermediateTypeDictionary<TTypeIdentifier, TType, TIntermediateType>,
        IIntermediateGenericTypeDictionary<TTypeIdentifier, TType, TIntermediateType>
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier<TTypeIdentifier>
        where TType :
            IGenericType<TTypeIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
            TType
    {
        public IntermediateGenericTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }

        public IntermediateGenericTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateGenericTypeDictionary<TTypeIdentifier, TType, TIntermediateType> root)
            : base(parent, master, root)
        {
        }

        protected internal override void _Add(TTypeIdentifier key, TType value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            else
            {
                var intermediateType = (TIntermediateType)value;
                intermediateType.TypeParameterAdded += new EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>>>(intermediateType_TypeParameterAddOrRemove);
                intermediateType.TypeParameterRemoved += new EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>>>(intermediateType_TypeParameterAddOrRemove);
            }
            base._Add(key, value);
        }

        void intermediateType_TypeParameterAddOrRemove(object sender, EventArgsR1<IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>> e)
        {
            if (sender is TIntermediateType)
                this.RekeyElement((TIntermediateType)sender);
        }

        private void RekeyElement(TIntermediateType type)
        {
            int valueIndex = this.Values.IndexOf(type);
            if (valueIndex != -1)
                this.Keys[valueIndex] = type.UniqueIdentifier;
        }

        protected internal override bool _Remove(int index)
        {
            if (index >= 0 && index < this.Count)
            {
                var element = base[index].Value;
                element.TypeParameterAdded -= new EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>>>(intermediateType_TypeParameterAddOrRemove);
                element.TypeParameterRemoved -= new EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>>>(intermediateType_TypeParameterAddOrRemove);
            }
            return base._Remove(index);
        }


        #region IIntermediateGenericTypeDictionary<TType,TIntermediateType> Members

        public TIntermediateType Add(string name, params GenericParameterData[] typeParameters)
        {
            var result = this.GetNewType(name);
            foreach (var paramData in typeParameters)
                result.TypeParameters.Add(paramData);
            this.AddDeclaration(result);
            return result;
        }

        public TIntermediateType Add(string name, Modules.IIntermediateModule module, params GenericParameterData[] typeParameters)
        {
            var result = this.Add(name, typeParameters);
            result.DeclaringModule = module;
            return result;
        }

        #endregion

    }
}
