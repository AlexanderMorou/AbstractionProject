using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
    /// across multiple instances through <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}.GetNewPartial(TInstanceIntermediateType,IIntermediateTypeParent)"/>.</typeparam>
    [EditorBrowsable(EditorBrowsableState.Always)]
    public abstract partial class IntermediateStructType<TInstanceIntermediateType> :
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

        internal IntermediateStructType(IIntermediateTypeParent parent)
            : base(parent)
        {

        }
        protected override IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.ConstructorMember GetNewConstructor()
        {
            return new IntermediateStructCtorMember<TInstanceIntermediateType>((TInstanceIntermediateType)this);
        }

        protected override IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.ConstructorMember GetTypeInitializer()
        {
            return new IntermediateStructCtorMember<TInstanceIntermediateType>((TInstanceIntermediateType)(object)this, true);
        }

        protected override IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.EventMember GetNewEvent(TypedName nameAndDelegateType)
        {
            var result = new IntermediateStructEventMember<TInstanceIntermediateType>((TInstanceIntermediateType)this);
            result.Name = nameAndDelegateType.Name;
            
            var signatureType = nameAndDelegateType.GetTypeRef();
            if (signatureType.ContainsSymbols())
                signatureType = signatureType.SimpleSymbolDisambiguation(result);

            result.SignatureSource = EventSignatureSource.Delegate;
            if (signatureType is IDelegateType)
                result.SignatureType = (IDelegateType)signatureType;
            return result;
        }

        protected override IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.MethodMember GetNewMethod(string name)
        {
            return new IntermediateStructMethodMember<TInstanceIntermediateType>((TInstanceIntermediateType)this);
        }

        protected override IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.EventMember GetNewEvent(string name, TypedNameSeries eventSignature)
        {
            var result = new IntermediateStructEventMember<TInstanceIntermediateType>((TInstanceIntermediateType)this);
            result.Name = name;
            result.Parameters.AddRange(eventSignature.ToArray());
            return result;
        }

        protected override IStructType OnMakeGenericClosure(ITypeCollectionBase typeParameters)
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

        protected override bool IsSubclassOfImpl(IType other)
        {
            if (other.Equals(CommonTypeRefs.ValueType) ||
                other.Equals(CommonTypeRefs.Object))
                return true;
            return false;
        }

        protected override IType BaseTypeImpl
        {
            get { return CommonTypeRefs.ValueType; }
        }

        protected override IndexerMember GetNewIndexer(TypedName nameAndReturn)
        {
            return new IntermediateStructIndexerMember<TInstanceIntermediateType>(nameAndReturn.Name, (TInstanceIntermediateType)(object)this)
            {
                PropertyType = nameAndReturn.Source == TypedNameSource.TypeReference ?
                               nameAndReturn.TypeReference :
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
                   nameAndType.TypeReference :
                   nameAndType.Source == TypedNameSource.SymbolReference ?
                   nameAndType.SymbolReference.GetSymbolType() :
                   null
            };
        }

        protected override FieldMember GetNewField(TypedName nameAndType)
        {
            var member = new IntermediateStructFieldMember<TInstanceIntermediateType>(nameAndType.Name, (TInstanceIntermediateType)this);
            var kind = nameAndType.GetTypeRef();
            if (kind.ContainsSymbols())
                member.FieldType = kind.SimpleSymbolDisambiguation(member);
            else
                member.FieldType = kind;
            return member;
        }

        public override void Visit(IIntermediateTypeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public ImplementedInterfacesDictionary ImplementedInterfaces
        {
            get
            {
                return (ImplementedInterfacesDictionary)base.ImplementedInterfaces;
            }
        }

        IIntermediateStructImplementedInterfaces IIntermediateStructType.ImplementedInterfaces
        {
            get
            {
                return this.ImplementedInterfaces;
            }
        }

        #region IStructType Members

        public new IStructInterfaceMapping GetInterfaceMap(IInterfaceType type)
        {
            if (this.ImplementedInterfaces.ContainsKey(type))
                return this.ImplementedInterfaces[type];
            throw new KeyNotFoundException();
        }

        #endregion

        /// <summary>
        /// Obtains the instance for <see cref="ImplementedInterfaces"/>
        /// </summary>
        /// <returns></returns>
        protected override ImplementedInterfacesDictionary<IStructEventMember, IIntermediateStructEventMember, IStructIndexerMember, IIntermediateStructIndexerMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IStructType, IIntermediateStructType> InitializeImplementedInterfaces()
        {
            return new ImplementedInterfacesDictionary((TInstanceIntermediateType)this);
        }
    }
}
