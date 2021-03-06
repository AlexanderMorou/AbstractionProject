﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast;
//using AllenCopeland.Abstraction.Slf._Internal.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
                TypeBase<IGenericParameterUniqueIdentifier, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>,
                IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>
            {
                private IGenericParameterUniqueIdentifier uniqueIdentifier;
                private new SymbolType Parent { get; set; }
                internal GenericParameterMember(SymbolType parent, int position)
                {
                    this.Parent = parent;
                    this.Position = position;
                }

                protected override bool Equals(IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType> other)
                {
                    return ReferenceEquals(other, this);
                }

                protected override ITypeParent OnGetParent()
                {
                    return (ITypeParent)this.Parent;
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

                protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
                {
                    return LockedTypeCollection.Empty;
                }

                protected override IFullMemberDictionary OnGetMembers()
                {
                    return LockedFullMembersBase.Empty;
                }

                protected override INamespaceDeclaration OnGetNamespace()
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

                protected override IMetadataCollection InitializeMetadata()
                {
                    return new CustomAttributeCollection(this);
                }


                protected override string OnGetName()
                {
                    if (this.Parent == null)
                        return null;
                    if (this.Parent.typeParameters != null)
                        return this.Parent.typeParameters.tParamNames[this.Position].Name;
                    return null;
                }
                #region IGenericParameter<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>,ISymbolType> Members

                ISymbolType IGenericParameter<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>, ISymbolType>.Parent
                {
                    get { return this.Parent; }
                }

                #endregion

                #region IGenericParameter<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

                public IGenericParameterConstructorMemberDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Constructors
                {
                    get { return null; }
                }

                public IGenericParameterEventMemberDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Events
                {
                    get { return null; }
                }

                public IGenericParameterIndexerMemberDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Indexers
                {
                    get { return null; }
                }

                public IGenericParameterMethodMemberDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Methods
                {
                    get { return null; }
                }

                public IGenericParameterPropertyMemberDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Properties
                {
                    get { return null; }
                }

                #endregion

                #region ICreatableParent<IGenericParameterConstructorMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>,IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

                IConstructorMemberDictionary<IGenericParameterConstructorMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> ICreatableParent<IGenericParameterConstructorMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>.Constructors
                {
                    get { return null; }
                }

                public IGenericParameterConstructorMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> TypeInitializer
                {
                    get { return null; }
                }

                #endregion

                #region ICreatableParent Members

                IConstructorMemberDictionary ICreatableParent.Constructors
                {
                    get { return null; }
                }

                IConstructorMember ICreatableParent.TypeInitializer
                {
                    get { return null; }
                }

                #endregion

                #region IType<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

                public new IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType> ElementType
                {
                    get { return null; }
                }

                #endregion

                #region IMethodSignatureParent<IGenericParameterMethodMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>,IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

                IMethodSignatureMemberDictionary<IGenericParameterMethodMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> IMethodSignatureParent<IGenericParameterMethodMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>.Methods
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

                #region IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>,IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

                IEventSignatureMemberDictionary<IGenericParameterEventMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>.Events
                {
                    get { return null; }
                }

                #endregion

                #region IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>,IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>,IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>,IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

                IEventSignatureMemberDictionary<IGenericParameterEventMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> IEventSignatureParent<IGenericParameterEventMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IEventSignatureParameterMember<IGenericParameterEventMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>.Events
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

                #region IIndexerSignatureParent<IGenericParameterIndexerMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>,IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

                IIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> IIndexerSignatureParent<IGenericParameterIndexerMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>.Indexers
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

                #region IPropertySignatureParent<IGenericParameterPropertyMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>,IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> Members

                IPropertySignatureMemberDictionary<IGenericParameterPropertyMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>> IPropertySignatureParent<IGenericParameterPropertyMember<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>>.Properties
                {
                    get { return null; }
                }

                #endregion

                #region IPropertySignatureParent Members

                IPropertySignatureMemberDictionary IPropertySignatureParent.Properties
                {
                    get { return null; }
                }

                #endregion

                #region IGenericParameter Members

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

                public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
                {
                    get { return TypeBase<IGeneralGenericTypeUniqueIdentifier>.EmptyIdentifiers; }
                }

                protected override IGenericParameterUniqueIdentifier OnGetUniqueIdentifier()
                {
                    if (this.uniqueIdentifier == null)
                        this.uniqueIdentifier = this.Parent.TypeParameters.Keys[this.Position];
                    return this.uniqueIdentifier;
                }

                //protected override bool IsAttributeInheritable(IType attribute)
                //{
                //    if (attribute is ICompiledType)
                //    {
                //        var cType = attribute as ICompiledType;
                //        return CliAssist.GetAttributeUsage(cType.UnderlyingSystemType).AllowMultiple;
                //    }
                //    else
                //        return CliAssist.GetAttributeUsage(attribute).AllowMultiple;
                //}

                protected override IIdentityManager OnGetManager()
                {
                    return this.Parent.IdentityManager;
                }

                public bool HasEmptyConstructor { get { return false; } }
            }
        }
    }
}