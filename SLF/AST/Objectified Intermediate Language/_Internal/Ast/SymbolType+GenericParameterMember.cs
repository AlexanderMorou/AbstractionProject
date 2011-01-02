using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    partial class SymbolType
    {
        partial class GenericParameterDictionary
        {
            private class GenericParameterMember :
                TypeBase<IGenericTypeParameter<ISymbolType>>,
                IGenericTypeParameter<ISymbolType>
            {
                private SymbolType Parent { get; set; }
                internal GenericParameterMember(SymbolType parent, int position)
                {
                    this.Parent = parent;
                    this.Position = position;
                }

                protected override bool Equals(IGenericTypeParameter<ISymbolType> other)
                {
                    return ReferenceEquals(other, this);
                }

                protected override IType OnGetDeclaringType()
                {
                    return this.Parent;
                }

                protected override TypeKind TypeImpl
                {
                    get { return TypeKind.Class; }
                }

                protected override bool CanCacheImplementsList
                {
                    get { return true; }
                }

                protected override ILockedTypeCollection OnGetImplementedInterfaces()
                {
                    return LockedTypeCollection.Empty;
                }

                protected override IFullMemberDictionary OnGetMembers()
                {
                    return LockedFullMembersBase.Empty;
                }

                protected override INamespaceDeclaration OnGetNameSpace()
                {
                    return null;
                }

                protected override AccessLevelModifiers OnGetAccessLevel()
                {
                    return AccessLevelModifiers.Public;
                }

                protected override IAssembly OnGetAssembly()
                {
                    return null;
                }

                protected override IArrayType OnMakeArray(int rank)
                {
                    return new ArrayType(this, rank);
                }

                protected override IArrayType OnMakeArray(params int[] lowerBounds)
                {
                    return new ArrayType(this, lowerBounds);
                }

                protected override IType OnMakeByReference()
                {
                    return new ByRefType(this);
                }

                protected override IType OnMakePointer()
                {
                    return new PointerType(this);
                }

                protected override IType OnMakeNullable()
                {
                    return new NullableType(this);
                }

                public override bool IsGenericConstruct
                {
                    get { return false; }
                }

                protected override bool IsSubclassOfImpl(IType other)
                {
                    return false;
                }

                protected override IType BaseTypeImpl
                {
                    get { return null; }
                }

                protected override ICustomAttributeCollection InitializeCustomAttributes()
                {
                    return new CustomAttributeCollection(this);
                }

                protected override string OnGetName()
                {
                    if (this.Parent == null)
                        return null;
                    if (this.Parent.typeParameters != null)
                        return this.Parent.typeParameters.tParamNames[this.Position];
                    return null;
                }

                #region IGenericParameter<IGenericTypeParameter<ISymbolType>,ISymbolType> Members

                ISymbolType IGenericParameter<IGenericTypeParameter<ISymbolType>, ISymbolType>.Parent
                {
                    get { return this.Parent; }
                }

                #endregion

                #region IGenericParameter<IGenericTypeParameter<ISymbolType>> Members

                public IGenericParameterConstructorMemberDictionary<IGenericTypeParameter<ISymbolType>> Constructors
                {
                    get { return null; }
                }

                public IGenericParameterEventMemberDictionary<IGenericTypeParameter<ISymbolType>> Events
                {
                    get { return null; }
                }

                public IGenericParameterIndexerMemberDictionary<IGenericTypeParameter<ISymbolType>> Indexers
                {
                    get { return null; }
                }

                public IGenericParameterMethodMemberDictionary<IGenericTypeParameter<ISymbolType>> Methods
                {
                    get { return null; }
                }

                public IGenericParameterPropertyMemberDictionary<IGenericTypeParameter<ISymbolType>> Properties
                {
                    get { return null; }
                }

                #endregion

                #region ICreatableType<IGenericParameterConstructorMember<IGenericTypeParameter<ISymbolType>>,IGenericTypeParameter<ISymbolType>> Members

                IConstructorMemberDictionary<IGenericParameterConstructorMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>> ICreatableType<IGenericParameterConstructorMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>>.Constructors
                {
                    get { return null; }
                }

                public IGenericParameterConstructorMember<IGenericTypeParameter<ISymbolType>> TypeInitializer
                {
                    get { return null; }
                }

                #endregion

                #region ICreatableType Members

                IConstructorMemberDictionary ICreatableType.Constructors
                {
                    get { return null; }
                }

                IConstructorMember ICreatableType.TypeInitializer
                {
                    get { return null; }
                }

                #endregion

                #region IType<IGenericTypeParameter<ISymbolType>> Members

                public new IGenericTypeParameter<ISymbolType> ElementType
                {
                    get { return null; }
                }

                #endregion

                #region IMethodSignatureParent<IGenericParameterMethodMember<IGenericTypeParameter<ISymbolType>>,IGenericTypeParameter<ISymbolType>> Members

                IMethodSignatureMemberDictionary<IGenericParameterMethodMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>> IMethodSignatureParent<IGenericParameterMethodMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>>.Methods
                {
                    get { return null; }
                }

                #endregion

                #region IMethodSignatureParent Members

                IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
                {
                    get { return null; }
                }

                #endregion

                #region IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<ISymbolType>>,IGenericTypeParameter<ISymbolType>> Members

                IEventSignatureMemberDictionary<IGenericParameterEventMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>> IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>>.Events
                {
                    get { return null; }
                }

                #endregion

                #region IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<ISymbolType>>,IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<ISymbolType>>,IGenericTypeParameter<ISymbolType>>,IGenericTypeParameter<ISymbolType>> Members

                IEventSignatureMemberDictionary<IGenericParameterEventMember<IGenericTypeParameter<ISymbolType>>, IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>> IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<ISymbolType>>, IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>>.Events
                {
                    get { return null; }
                }

                #endregion

                #region IEventSignatureParent Members

                IEventSignatureMemberDictionary IEventSignatureParent.Events
                {
                    get { return null; }
                }

                #endregion

                #region IIndexerSignatureParent<IGenericParameterIndexerMember<IGenericTypeParameter<ISymbolType>>,IGenericTypeParameter<ISymbolType>> Members

                IIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>> IIndexerSignatureParent<IGenericParameterIndexerMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>>.Indexers
                {
                    get { return null; }
                }

                #endregion

                #region IIndexerSignatureParent Members

                IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
                {
                    get { return null; }
                }

                #endregion

                #region IPropertySignatureParentType<IGenericParameterPropertyMember<IGenericTypeParameter<ISymbolType>>,IGenericTypeParameter<ISymbolType>> Members

                IPropertySignatureMemberDictionary<IGenericParameterPropertyMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>> IPropertySignatureParentType<IGenericParameterPropertyMember<IGenericTypeParameter<ISymbolType>>, IGenericTypeParameter<ISymbolType>>.Properties
                {
                    get { return null; }
                }

                #endregion

                #region IPropertySignatureParentType Members

                IPropertySignatureMemberDictionary IPropertySignatureParentType.Properties
                {
                    get { return null; }
                }

                #endregion

                #region IGenericParameter Members

                public bool RequiresNewConstructor
                {
                    get { return false; }
                }

                public GenericTypeParameterSpecialConstraint SpecialConstraint
                {
                    get { return GenericTypeParameterSpecialConstraint.None; }
                }

                public int Position { get; private set; }

                public ILockedTypeCollection Constraints
                {
                    get { return LockedTypeCollection.Empty; }
                }

                IGenericParamParent IGenericParameter.Parent
                {
                    get { return this.Parent; }
                }

                IGenericParameterConstructorMemberDictionary IGenericParameter.Constructors
                {
                    get { return null; }
                }

                IGenericParameterEventMemberDictionary IGenericParameter.Events
                {
                    get { return null; }
                }

                IGenericParameterIndexerMemberDictionary IGenericParameter.Indexers
                {
                    get { return null; }
                }

                IGenericParameterMethodMemberDictionary IGenericParameter.Methods
                {
                    get { return null; }
                }

                IGenericParameterPropertyMemberDictionary IGenericParameter.Properties
                {
                    get { return null; }
                }

                #endregion

                #region IGenericTypeParameter Members

                IGenericType IGenericTypeParameter.Parent
                {
                    get { return this.Parent; }
                }

                #endregion

                public GenericParameterVariance Variance
                {
                    get
                    {
                        return GenericParameterVariance.Invariant;
                    }
                }

                protected override string OnGetNamespaceName()
                {
                    return null;
                }
            }
        }
    }
}