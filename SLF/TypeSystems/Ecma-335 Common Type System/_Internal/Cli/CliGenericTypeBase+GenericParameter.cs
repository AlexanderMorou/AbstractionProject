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

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliGenericTypeBase<TIdentifier, TType>
    {
        private class GenericParameter :
            IGenericTypeParameter<TIdentifier, TType>,
            ICliGenericTypeParameter
        {
            private CliGenericTypeBase<TIdentifier, TType> owner;
            private ICliMetadataGenericParameterTableRow metadata;
            private int index;
            #region IGenericParameter<IGenericTypeParameter<TIdentifier,TType>,TType> Members

            public TType Parent
            {
                get { return (TType) (object) this.owner; }
            }

            public IGenericParameterUniqueIdentifier UniqueIdentifier
            {
                get { return AstIdentifier.GetGenericParameterIdentifier(this.index); }
            }

            #endregion

            #region IGenericParameter<IGenericTypeParameter<TIdentifier,TType>> Members

            public IGenericParameterConstructorMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> Constructors
            {
                get { throw new NotImplementedException(); }
            }

            public IGenericParameterEventMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> Events
            {
                get { throw new NotImplementedException(); }
            }

            public IGenericParameterIndexerMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> Indexers
            {
                get { throw new NotImplementedException(); }
            }

            public IGenericParameterMethodMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> Methods
            {
                get { throw new NotImplementedException(); }
            }

            public IGenericParameterPropertyMemberDictionary<IGenericTypeParameter<TIdentifier, TType>> Properties
            {
                get { throw new NotImplementedException(); }
            }

            #endregion

            #region ICreatableParent<IGenericParameterConstructorMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IConstructorMemberDictionary<IGenericParameterConstructorMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> ICreatableParent<IGenericParameterConstructorMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Constructors
            {
                get { return this.Constructors; }
            }

            public IGenericParameterConstructorMember<IGenericTypeParameter<TIdentifier, TType>> TypeInitializer
            {
                get { return null; }
            }

            #endregion

            #region ICreatableParent Members

            IConstructorMemberDictionary ICreatableParent.Constructors
            {
                get { return (IConstructorMemberDictionary) this.Constructors; }
            }

            IConstructorMember ICreatableParent.TypeInitializer
            {
                get { return this.TypeInitializer; }
            }

            #endregion

            #region IType Members

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
                get { throw new NotImplementedException(); }
            }

            public TypeKind Type
            {
                get { return TypeKind.Class; }
            }

            public bool IsNullable
            {
                get { throw new NotImplementedException(); }
            }

            public IArrayType MakeArray(int rank)
            {
                throw new NotImplementedException();
            }

            public IArrayType MakeArray()
            {
                throw new NotImplementedException();
            }

            public IArrayType MakeArray(params int[] lowerBounds)
            {
                throw new NotImplementedException();
            }

            public IType MakePointer()
            {
                throw new NotImplementedException();
            }

            public IType MakeByReference()
            {
                throw new NotImplementedException();
            }

            public IType MakeNullable()
            {
                throw new NotImplementedException();
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
                get { throw new NotImplementedException(); }
            }

            public bool IsDefined(IType metadatumType, bool inherited)
            {
                throw new NotImplementedException();
            }

            public ITypeIdentityManager Manager
            {
                get { return this.owner.Manager; }
            }

            #endregion

            #region IEquatable<IType> Members

            public bool Equals(IType other)
            {
                return object.ReferenceEquals(this, other);
            }

            #endregion

            #region IMetadataEntity Members

            public IMetadataCollection CustomAttributes
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsDefined(IType metadatumType)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IScopedDeclaration Members

            public AccessLevelModifiers AccessLevel
            {
                get { return AccessLevelModifiers.Public; }
            }

            #endregion

            #region IDeclaration Members

            public string Name
            {
                get { return this.metadata.Name; }
            }

            IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
            {
                get { return this.UniqueIdentifier; }
            }

            public event EventHandler Disposed;

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.owner = null;
                this.metadata = null;
            }

            #endregion

            #region IMethodSignatureParent<IGenericParameterMethodMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IMethodSignatureMemberDictionary<IGenericParameterMethodMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> IMethodSignatureParent<IGenericParameterMethodMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Methods
            {
                get { return this.Methods; }
            }

            #endregion

            #region IMethodSignatureParent Members

            IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
            {
                get { return (IMethodSignatureMemberDictionary) this.Methods; }
            }

            #endregion

            #region IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IEventSignatureMemberDictionary<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Events
            {
                get { return this.Events; }
            }

            #endregion

            #region IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier,TType>>,IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IEventSignatureMemberDictionary<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Events
            {
                get { return this.Events; }
            }

            #endregion

            #region IEventSignatureParent Members

            IEventSignatureMemberDictionary IEventSignatureParent.Events
            {
                get { return (IEventSignatureMemberDictionary) this.Events; }
            }

            #endregion

            #region IIndexerSignatureParent<IGenericParameterIndexerMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> IIndexerSignatureParent<IGenericParameterIndexerMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Indexers
            {
                get { return this.Indexers; }
            }

            #endregion

            #region IIndexerSignatureParent Members

            IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
            {
                get { return (IIndexerSignatureMemberDictionary)this.Indexers; }
            }

            #endregion

            #region IPropertySignatureParent<IGenericParameterPropertyMember<IGenericTypeParameter<TIdentifier,TType>>,IGenericTypeParameter<TIdentifier,TType>> Members

            IPropertySignatureMemberDictionary<IGenericParameterPropertyMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>> IPropertySignatureParent<IGenericParameterPropertyMember<IGenericTypeParameter<TIdentifier, TType>>, IGenericTypeParameter<TIdentifier, TType>>.Properties
            {
                get { return this.Properties; }
            }

            #endregion

            #region IPropertySignatureParent Members

            IPropertySignatureMemberDictionary IPropertySignatureParent.Properties
            {
                get { return (IPropertySignatureMemberDictionary) this.Properties; }
            }

            #endregion

            #region IType<IGenericParameterUniqueIdentifier,IGenericTypeParameter<TIdentifier,TType>> Members

            public IGenericTypeParameter<TIdentifier, TType> ElementType
            {
                get { throw new InvalidOperationException(Resources.ObjectStateThrowMessage); }
            }

            #endregion

            #region IGenericParameter Members

            public GenericParameterVariance Variance
            {
                get
                {
                    var variance = this.metadata.Flags & GenericParameterAttributes.VarianceMask;
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
                    switch (metadata.Flags & GenericParameterAttributes.SpecialConstraintMask)
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

            #endregion

            #region IGenericTypeParameter Members

            IGenericType IGenericTypeParameter.Parent
            {
                get { return this.Parent; }
            }

            #endregion
        }
    }
}