﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public abstract class IntermediateGenericTypeDictionary<TType, TIntermediateType> :
        IntermediateTypeDictionary<TType, TIntermediateType>,
        IIntermediateGenericTypeDictionary<TType, TIntermediateType>
        where TType :
            IGenericType<TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TType, TIntermediateType>,
            TType
    {
        public IntermediateGenericTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }

        public IntermediateGenericTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateTypeDictionary<TType, TIntermediateType> root)
            : base(parent, master, root)
        {
        }

        protected override void Add(string key, TType value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            else
            {
                var intermediateType = (TIntermediateType)value;
                intermediateType.TypeParameterAdded += new EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>>(intermediateType_TypeParameterAddOrRemove);
                intermediateType.TypeParameterRemoved += new EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>>(intermediateType_TypeParameterAddOrRemove);
            }
            base.Add(key, value);
        }

        void intermediateType_TypeParameterAddOrRemove(object sender, EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>> e)
        {
            base.IncrementVersion();
        }

        protected override bool RemoveImpl(string key)
        {
            if (base.ContainsKey(key))
            {
                var element = base[key];
                element.TypeParameterAdded -= new EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>>(intermediateType_TypeParameterAddOrRemove);
                element.TypeParameterRemoved -= new EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>>(intermediateType_TypeParameterAddOrRemove);
            }
            return base.RemoveImpl(key);
        }


        #region IIntermediateGenericTypeDictionary<TType,TIntermediateType> Members

        public TIntermediateType Add(string name, params GenericParameterData[] typeParameters)
        {
            var result = this.GetNewType(name);
            foreach (var paramData in typeParameters)
                result.TypeParameters.Add(paramData);
            this.Add(result.UniqueIdentifier, result);
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