using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Properties;
//2.2046226218487755 -- kg -> pounds

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliGenericTypeBase<TIdentifier, TType>
    {
        private class GenericParameter :
            IGenericTypeParameter<TIdentifier, TType>,
            ICliGenericTypeParameter
        {
            private CliGenericTypeBase<TIdentifier, TType> owner;
            private ICliMetadataGenericParameterTableRow metadataEntry;
            private int index;
            private IGenericParameterConstructorMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> constructors;
            private IGenericParameterEventMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> events;
            private IGenericParameterIndexerMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> indexers;
            private IGenericParameterMethodMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> methods;
            private IGenericParameterPropertyMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> properties;
            internal GenericParameter(CliGenericTypeBase<TIdentifier, TType> owner, ICliMetadataGenericParameterTableRow metadataEntry, int index)
            {
                this.owner = owner;
                this.metadataEntry = metadataEntry;
                this.index = index;
            }
            //#region IGenericParameter<IGenericTypeParameter<TIdentifier,TType>,TType> Members

            public TType Parent
            {
                get { return (TType) (object) this.owner; }
            }

            public IGenericParameterUniqueIdentifier UniqueIdentifier
            {
                get { return AstIdentifier.GetGenericParameterIdentifier(this.index, this.Name); }
            }

            //#endregion

            //#region IGenericParameter<IGenericTypeParameter<TIdentifier,TType>> Members

            public IGenericParameterConstructorMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> Constructors
            {
                get { return this.constructors ?? (this.constructors = this.InitializeConstructors()); }
            }

            public IGenericParameterEventMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> Events
            {
                get { return this.events ?? (this.events = this.InitializeEvents()); }
            }

            public IGenericParameterIndexerMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> Indexers
            {
                get { return this.indexers ?? (this.indexers = this.InitializeIndexers()); }
            }

            public IGenericParameterMethodMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> Methods
            {
                get { return this.methods ?? (this.methods = this.InitializeMethods()); }
            }

            public IGenericParameterPropertyMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> Properties
            {
                get { return this.properties ?? (this.properties = this.InitializeProperties()); }
            }

            //#endregion

            //#region ICreatableParent<IGenericParameterConstructorMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IConstructorMemberDictionary<IGenericParameterConstructorMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> ICreatableParent<IGenericParameterConstructorMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Constructors
            {
                get { return this.Constructors; }
            }

            public IGenericParameterConstructorMember<IGenericTypeParameter<TIdentifier, TType>> TypeInitializer
            {
                get { return null; }
            }

            //#endregion

            //#region ICreatableParent Members

            IConstructorMemberDictionary ICreatableParent.Constructors
            {
                get { return (IConstructorMemberDictionary) this.Constructors; }
            }

            IConstructorMember ICreatableParent.TypeInitializer
            {
                get { return this.TypeInitializer; }
            }

            //#endregion

            //#region IType Members

            public TypeElementClassification ElementClassification
            {
                get { return TypeElementClassification.None; }
            }

            IType IType.ElementType
            {
                get { return this.ElementType; }
            }

            public bool IsGenericTypeParameter
            {
                get { return true; }
            }

            public bool IsGenericConstruct
            {
                get { return false; }
            }

            public IType DeclaringType
            {
                get { return this.owner; }
            }

            public TypeKind Type
            {
                get { return TypeKind.Class; }
            }

            public bool IsNullable
            {
                get { return this.SpecialConstraint == GenericTypeParameterSpecialConstraint.Struct; }
            }

            public IArrayType MakeArray(int rank)
            {
                return this.owner.IdentityManager.MakeArray(this, rank);
            }

            public IArrayType MakeArray()
            {
                return this.owner.IdentityManager.MakeArray(this);
            }

            public IArrayType MakeArray(int[] lowerBounds, uint[] lengths = null)
            {
                return this.owner.IdentityManager.MakeArray(this, lowerBounds, lengths);
            }

            public IType MakePointer()
            {
                return this.owner.IdentityManager.MakeClassificationType(this, TypeElementClassification.Pointer);
            }

            public IType MakeByReference()
            {
                return this.owner.IdentityManager.MakeClassificationType(this, TypeElementClassification.Reference);
            }

            public IType MakeNullable()
            {
                return this.owner.IdentityManager.MakeClassificationType(this, TypeElementClassification.Nullable);
            }

            public bool IsSubclassOf(IType other)
            {
                throw new NotImplementedException();
            }

            public bool IsAssignableFrom(IType target)
            {
                throw new NotImplementedException();
            }

            public string FullName
            {
                get { return null; }
            }

            public INamespaceDeclaration Namespace
            {
                get { return null; }
            }

            public string NamespaceName
            {
                get { return string.Empty; }
            }

            public IType BaseType
            {
                get { throw new NotImplementedException(); }
            }

            public ILockedTypeCollection ImplementedInterfaces
            {
                get { throw new NotImplementedException(); }
            }

            public IAssembly Assembly
            {
                get { return this.owner.Assembly; }
            }

            public IFullMemberDictionary Members
            {
                get { throw new NotImplementedException(); }
            }

            public IEnumerable<IDeclaration> Declarations
            {
                get { throw new NotImplementedException(); }
            }

            public IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
            {
                get { throw new NotImplementedException(); }
            }

            IGeneralTypeUniqueIdentifier IType.UniqueIdentifier
            {
                get { return this.UniqueIdentifier; }
            }

            public bool IsDefined(IType metadatumType, bool inherited)
            {
                throw new NotImplementedException();
            }

            public ITypeIdentityManager IdentityManager
            {
                get { return this.owner.IdentityManager; }
            }

            //#endregion

            //#region IEquatable<IType> Members

            public bool Equals(IType other)
            {
                return object.ReferenceEquals(this, other);
            }

            //#endregion

            //#region IMetadataEntity Members

            public IMetadataCollection Metadata
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsDefined(IType metadatumType)
            {
                throw new NotImplementedException();
            }

            //#endregion

            //#region IScopedDeclaration Members

            public AccessLevelModifiers AccessLevel
            {
                get { return AccessLevelModifiers.Public; }
            }

            //#endregion

            //#region IDeclaration Members

            public string Name
            {
                get { return this.metadataEntry.Name; }
            }

            IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
            {
                get { return this.UniqueIdentifier; }
            }

            public event EventHandler Disposed;

            //#endregion

            //#region IDisposable Members

            public void Dispose()
            {
                this.owner = null;
                this.metadataEntry = null;
            }

            //#endregion

            //#region IMethodSignatureParent<IGenericParameterMethodMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IMethodSignatureMemberDictionary<IGenericParameterMethodMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> IMethodSignatureParent<IGenericParameterMethodMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Methods
            {
                get { return this.Methods; }
            }

            //#endregion

            //#region IMethodSignatureParent Members

            IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
            {
                get { return (IMethodSignatureMemberDictionary) this.Methods; }
            }

            //#endregion

            //#region IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IEventSignatureMemberDictionary<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Events
            {
                get { return this.Events; }
            }

            //#endregion

            //#region IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier,TType>>,IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IEventSignatureMemberDictionary<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Events
            {
                get { return this.Events; }
            }

            //#endregion

            //#region IEventSignatureParent Members

            IEventSignatureMemberDictionary IEventSignatureParent.Events
            {
                get { return (IEventSignatureMemberDictionary) this.Events; }
            }

            //#endregion

            //#region IIndexerSignatureParent<IGenericParameterIndexerMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> IIndexerSignatureParent<IGenericParameterIndexerMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Indexers
            {
                get { return this.Indexers; }
            }

            //#endregion

            //#region IIndexerSignatureParent Members

            IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
            {
                get { return (IIndexerSignatureMemberDictionary)this.Indexers; }
            }

            //#endregion

            //#region IPropertySignatureParent<IGenericParameterPropertyMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IPropertySignatureMemberDictionary<IGenericParameterPropertyMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> IPropertySignatureParent<IGenericParameterPropertyMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Properties
            {
                get { return this.Properties; }
            }

            //#endregion

            //#region IPropertySignatureParent Members

            IPropertySignatureMemberDictionary IPropertySignatureParent.Properties
            {
                get { return (IPropertySignatureMemberDictionary) this.Properties; }
            }

            //#endregion

            //#region IType<IGenericParameterUniqueIdentifier,IGenericTypeParameter<TIdentifier,TType>> Members

            public IGenericTypeParameter<TIdentifier, TType> ElementType
            {
                get { throw new InvalidOperationException(Resources.ObjectStateThrowMessage); }
            }

            //#endregion

            //#region IGenericParameter Members

            public GenericParameterVariance Variance
            {
                get
                {
                    var variance = this.metadataEntry.Flags & GenericParameterAttributes.VarianceMask;
                    switch (variance)
                    {
                        case GenericParameterAttributes.Contravariant:
                            return GenericParameterVariance.Contravariant;
                        case GenericParameterAttributes.Covariant:
                            return GenericParameterVariance.Covariant;
                        default:
                            return GenericParameterVariance.Invariant;
                    }
                }
            }

            public GenericTypeParameterSpecialConstraint SpecialConstraint
            {
                get
                {
                    switch (metadataEntry.Flags & (GenericParameterAttributes.SpecialConstraintMask ^ GenericParameterAttributes.DefaultConstructorConstraint))
                    {
                        case GenericParameterAttributes.NotNullableValueTypeConstraint:
                            return GenericTypeParameterSpecialConstraint.Struct;
                        case GenericParameterAttributes.ReferenceTypeConstraint:
                            return GenericTypeParameterSpecialConstraint.Class;
                        default:
                            return GenericTypeParameterSpecialConstraint.None;
                    }
                }
            }

            public int Position
            {
                get { return this.index; }
            }

            public ILockedTypeCollection Constraints
            {
                get { throw new NotImplementedException(); }
            }

            IGenericParamParent IGenericParameter.Parent
            {
                get { return this.Parent; }
            }

            IGenericParameterConstructorMemberDictionary IGenericParameter.Constructors
            {
                get { return (IGenericParameterConstructorMemberDictionary) this.Constructors; }
            }

            IGenericParameterEventMemberDictionary IGenericParameter.Events
            {
                get { return (IGenericParameterEventMemberDictionary) this.Events; }
            }

            IGenericParameterIndexerMemberDictionary IGenericParameter.Indexers
            {
                get { return (IGenericParameterIndexerMemberDictionary) this.Indexers; }
            }

            IGenericParameterMethodMemberDictionary IGenericParameter.Methods
            {
                get { return (IGenericParameterMethodMemberDictionary) this.Methods; }
            }

            IGenericParameterPropertyMemberDictionary IGenericParameter.Properties
            {
                get { return (IGenericParameterPropertyMemberDictionary) this.Properties; }
            }

            //#endregion

            //#region IGenericTypeParameter Members

            IGenericType IGenericTypeParameter.Parent
            {
                get { return this.Parent; }
            }

            //#endregion

            //#region IType Members


            public IModifiedType MakeModified(TypeModification[] modifiers)
            {
                throw new NotImplementedException();
            }

            //#endregion

            public ICliMetadataGenericParameterTableRow MetadataEntry
            {
                get { return this.metadataEntry; }
            }

            private IGenericParameterConstructorMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> InitializeConstructors()
            {
                throw new NotImplementedException();
            }

            private IGenericParameterEventMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> InitializeEvents()
            {
                throw new NotImplementedException();
            }

            private IGenericParameterIndexerMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> InitializeIndexers()
            {
                throw new NotImplementedException();
            }

            private IGenericParameterMethodMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> InitializeMethods()
            {
                throw new NotImplementedException();
            }

            private IGenericParameterPropertyMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> InitializeProperties()
            {
                throw new NotImplementedException();
            }

        }
    }
}