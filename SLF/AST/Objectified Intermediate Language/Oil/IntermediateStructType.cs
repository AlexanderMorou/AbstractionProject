﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class IntermediateStructType :
        IntermediateStructType<IntermediateStructType>
    {
        public IntermediateStructType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {

        }

        public IntermediateStructType(IntermediateStructType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        protected override IntermediateStructType GetNewPartial(IntermediateStructType root, IIntermediateTypeParent parent)
        {
            return new IntermediateStructType(root, parent);
        }

    }

    /// <summary>
    /// Provides a generic base class for working with an intermediate
    /// data structure type declaration.
    /// </summary>
    /// <typeparam name="TInstanceIntermediateType">The specific kind of
    /// <see cref="IIntermediateStructType"/>
    /// within the intermediate abstract syntax tree for segmentation
    /// across multiple instances through <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.GetNewPartial(TInstanceIntermediateType,IIntermediateTypeParent)"/>.</typeparam>
    public abstract class IntermediateStructType<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>,
        IIntermediateStructType
        where TInstanceIntermediateType :
            IntermediateStructType<TInstanceIntermediateType>
    {
        public IntermediateStructType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }
        public IntermediateStructType(TInstanceIntermediateType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }
        protected override IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.ConstructorMember GetNewConstructor(TypedNameSeries parameters)
        {
            throw new NotImplementedException();
        }

        protected override IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.EventMember GetNewEvent(TypedName nameAndDelegateType)
        {
            throw new NotImplementedException();
        }

        protected override IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.MethodMember GetNewMethod(string name)
        {
            return new IntermediateStructMethodMember<TInstanceIntermediateType>((TInstanceIntermediateType)this);
        }

        protected override IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.EventMember GetNewEvent(string name, TypedNameSeries eventSignature)
        {
            throw new NotImplementedException();
        }

        protected override IStructType OnMakeGenericType(ITypeCollectionBase typeParameters)
        {
            return new _StructTypeBase(this, typeParameters);
        }

        protected override bool Equals(IStructType other)
        {
            if (other is TInstanceIntermediateType)
            {
                TInstanceIntermediateType iOther = (TInstanceIntermediateType)other;
                
                if (this.IsRoot)
                    if (!iOther.IsRoot)
                        return object.ReferenceEquals(iOther.GetRoot(), this);
                    else
                        return object.ReferenceEquals(this, iOther);
                else if (iOther.IsRoot)
                    return object.ReferenceEquals(this.GetRoot(), iOther);
                else
                    return object.ReferenceEquals(this.GetRoot(), iOther.GetRoot());
            }
            return false;
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Struct; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            throw new NotImplementedException();
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            if (other.Equals(IntermediateGateway.CommonlyUsedTypeReferences.ValueType) ||
                other.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Object))
                return true;
            return false;
        }

        protected override IType BaseTypeImpl
        {
            get { return IntermediateGateway.CommonlyUsedTypeReferences.ValueType; }
        }

        protected override IndexerMember GetNewIndexer(TypedName nameAndReturn)
        {
            return new IntermediateStructIndexerMember<TInstanceIntermediateType>(nameAndReturn.Name, (TInstanceIntermediateType)(object)this)
            {
                PropertyType = nameAndReturn.Source == TypedNameSource.TypeReference ?
                               nameAndReturn.Reference :
                               nameAndReturn.Source == TypedNameSource.SymbolReference ?
                               nameAndReturn.SymbolReference.GetSymbolType() :
                               null
            };
        }


        protected override PropertyMember GetNewProperty(TypedName nameAndType)
        {
            return new IntermediateStructPropertyMember<TInstanceIntermediateType>(nameAndType.Name, (TInstanceIntermediateType)this)
            {
                PropertyType = nameAndType.Source == TypedNameSource.TypeReference ?
                   nameAndType.Reference :
                   nameAndType.Source == TypedNameSource.SymbolReference ?
                   nameAndType.SymbolReference.GetSymbolType() :
                   null
            };
        }
    }
}