using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class IntermediateGenericParameterBase<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TIntermediateGenericParameter :
            class,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TGenericParameter
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
        where TIntermediateParent :
            class,
            IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TParent
    {
        /// <summary>
        /// Provides a constructor member for the <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        protected class ConstructorMember :
            IntermediateConstructorSignatureMemberBase<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>
        {
            /// <summary>
            /// Creates a new <see cref="ConstructorMember"/> with the
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateGenericParameter"/>
            /// in which the <see cref="ConstructorMember"/> is contained.</param>
            public ConstructorMember(TIntermediateGenericParameter parent)
                : base(parent, parent.IdentityManager)
            {
            }

            /// <summary>
            /// Returns <see cref="AccessLevelModifiers.Public"/>;
            /// additionally,
            /// </summary>
            /// <exception cref="System.InvalidOperationException">The acces level cannot be set on generic parameter
            /// constructors; occurs when set through <see cref="IIntermediateScopedDeclaration.AccessLevel"/>.</exception>
            public override AccessLevelModifiers AccessLevel
            {
                get
                {
                    return AccessLevelModifiers.Public;
                }
                set
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public class MethodMember :
            IntermediateMethodSignatureMemberBase<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>
        {
            internal MethodMember(string name, TIntermediateGenericParameter parent)
                : base(name, parent, parent.IdentityManager)
            {
            }

            internal new bool _AreParametersInitialized{get{return base.AreParametersInitialized;}}

            internal MethodMember(TIntermediateGenericParameter parent)
                : base(parent, parent.IdentityManager)
            {
            }

            protected override IntermediateParameterMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>> InitializeParameters()
            {
                return base.InitializeParameters();
            }

            protected override IGenericParameterMethodMember<TGenericParameter> OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
            {
                return new _Internal.GenericLayer.Members._GenericParameterMethodMemberBase<TGenericParameter>(this, genericReplacements);
            }

        }

        public class IndexerMember :
            IntermediateIndexerSignatureMember<IGenericParameterIndexerMember<TGenericParameter>, IIntermediateGenericParameterIndexerMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter, IndexerMember.IndexerMethodMember>,
            IIntermediateGenericParameterIndexerMember<TGenericParameter, TIntermediateGenericParameter>
        {
            internal IndexerMember(string name, TIntermediateGenericParameter parent, ITypeIdentityManager identityManager)
                : base(name, parent, identityManager)
            {
            }
            internal IndexerMember(TIntermediateGenericParameter parent, ITypeIdentityManager identityManager)
                : base(parent, identityManager)
            {
            }

            protected override void OnParameterAdded(EventArgsR1<IIntermediateIndexerSignatureParameterMember<IGenericParameterIndexerMember<TGenericParameter>, IIntermediateGenericParameterIndexerMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>> e)
            {
                if (this.CanRead && base.IsGetMethodInitialized)
                {
                    var gm = (IndexerMethodMember)this.GetMethod;
                    if (gm._AreParametersInitialized)
                    {
                        var gmParams = gm.Parameters;
                        gmParams._Add(e.Arg1.UniqueIdentifier, new IndexerDependentParameter((ParametersDictionary.ParameterMember)e.Arg1, gm));
                    }
                }
                if (this.CanWrite && this.IsSetMethodInitialized)
                {
                    var sm = (IndexerSetMethodMember)this.SetMethod;
                    if (sm._AreParametersInitialized)
                    {
                        var smParams = sm.Parameters;
                        var valueParam = (IndexerValueParameter)((IIntermediatePropertySetMethodMember)sm).ValueParameter;
                        smParams._Remove(valueParam.UniqueIdentifier);
                        smParams._Add(e.Arg1.UniqueIdentifier, new IndexerDependentParameter((ParametersDictionary.ParameterMember)e.Arg1, sm));
                        smParams._Add(valueParam.UniqueIdentifier, valueParam);
                    }
                }
                base.OnParameterAdded(e);
            }

            protected override IndexerMember.IndexerMethodMember GetMethodSignatureMember(PropertyMethodType methodType)
            {
                switch (methodType)
                {
                    case PropertyMethodType.SetMethod:
                        return new IndexerSetMethodMember(this);
                    case PropertyMethodType.GetMethod:
                    default:
                        return new IndexerMethodMember(PropertyMethodType.GetMethod, this);
                }
            }


            private class IndexerValueParameter :
                IndexerMethodMember.ParameterMember
            {
                private IndexerMember owner;
                internal IndexerValueParameter(IndexerMember owner, IndexerSetMethodMember parent)
                    : base(parent, parent.IdentityManager)
                {
                    this.owner = owner;
                }

                protected override string OnGetName()
                {
                    return "value";
                }

                protected override void OnSetName(string name)
                {
                    throw new InvalidOperationException("Cannot set the name of the value parameter of the set method of an indexer.");
                }

                public override IType ParameterType
                {
                    get
                    {
                        return this.owner.PropertyType;
                    }
                    set
                    {
                        throw new InvalidOperationException("Cannot set the parameter type of the value parameter of the set method of an indexer, set the indexer's property type.");
                    }
                }

                public override ParameterCoercionDirection Direction
                {
                    get
                    {
                        return ParameterCoercionDirection.In;
                    }
                    set
                    {
                        throw new InvalidOperationException("Cannot change the direction of the value parameter of the set method of an indexer.");
                    }
                }
            }

            private class IndexerDependentParameter :
                IndexerMethodMember.ParameterMember
            {
                private ParametersDictionary.ParameterMember original;
                internal IndexerDependentParameter(ParametersDictionary.ParameterMember original, IndexerMethodMember parent)
                    : base(parent, parent.IdentityManager)
                {
                    this.original = original;
                    this.original.IdentifierChanged += original_IdentifierChanged;
                }

                void original_IdentifierChanged(object sender, DeclarationIdentifierChangeEventArgs<IGeneralMemberUniqueIdentifier> e)
                {
                    this.OnIdentifierChanged(e.OldIdentifier, e.Cause);
                }

                public override IType ParameterType
                {
                    get
                    {
                        return this.original.ParameterType;
                    }
                    set
                    {
                        throw new InvalidOperationException("Cannot set the type of a parameter of a method of an indexer, set the indexer's parameter type.");
                    }
                }
                protected override string OnGetName()
                {
                    return this.original.Name;
                }
                protected override void OnSetName(string name)
                {
                    throw new InvalidOperationException("Cannot set the name of a parameter of a method of an indexer, set the indexer's parameter name.");
                }
                public override ParameterCoercionDirection Direction
                {
                    get
                    {
                        return this.original.Direction;
                    }
                    set
                    {
                        throw new InvalidOperationException("Cannot set the direction of a parameter of a method of an indexer, set the indexer's parameter direction.");
                    }
                }

                protected override MetadataDefinitionCollection InitializeCustomAttributes()
                {
                    return new MetadataDefinitionCollection(this.original, this.identityManager);
                }

                public override IGeneralMemberUniqueIdentifier UniqueIdentifier
                {
                    get
                    {

                        return this.original.UniqueIdentifier;
                    }
                }

                protected override void Dispose(bool disposing)
                {
                    this.original.Dispose();
                    base.Dispose(disposing);
                }
            }


            public class IndexerSetMethodMember :
                IndexerMethodMember,
                IIntermediatePropertySignatureSetMethodMember
            {
                private IndexerValueParameter valueParameter;

                public IndexerSetMethodMember(IndexerMember owner)
                    : base(PropertyMethodType.SetMethod, owner)
                {

                }

                protected override IntermediateParameterMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    this.valueParameter = new IndexerValueParameter(this.Owner, this);
                    result._Add(this.valueParameter.UniqueIdentifier, this.valueParameter);
                    return result;
                }

                #region IIntermediatePropertySetMethodMember Members

                public IIntermediateSignatureParameterMember ValueParameter
                {
                    get
                    {
                        if (this.valueParameter == null)
                            return this.Parameters["value"];
                        return valueParameter;
                    }
                }

                #endregion

                #region IIntermediatePropertySignatureSetMethodMember Members

                IIntermediateSignatureParameterMember IIntermediatePropertySignatureSetMethodMember.ValueParameter
                {
                    get { return this.ValueParameter; }
                }

                #endregion
            }

            public class IndexerMethodMember :
                MethodMember,
                IIntermediatePropertySignatureMethodMember
            {
                private PropertyMethodType methodType;
                private IndexerMember owner;
                public IndexerMethodMember(PropertyMethodType methodType, IndexerMember owner)
                    : base(owner == null ? null : owner.Parent)
                {
                    if (owner == null)
                        throw new ArgumentNullException("parent");
                    this.owner = owner;
                    this.methodType = methodType;
                }

                protected override IntermediateParameterMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    if (this.Owner.AreParametersInitialized)
                        foreach (var param in this.Owner.Parameters.Values)
                            result._Add(param.UniqueIdentifier, new IndexerDependentParameter((IndexerMember.ParametersDictionary.ParameterMember)param, this));
                    result.Lock();
                    return result;
                }

                public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return base.UniqueIdentifier;
                    }
                }

                protected override bool AreParametersInitialized
                {
                    get
                    {
                        return true;
                    }
                }

                protected IndexerMember Owner
                {
                    get
                    {
                        return this.owner;
                    }
                }

                protected override string OnGetName()
                {
                    switch (this.methodType)
                    {
                        case PropertyMethodType.GetMethod:
                            return string.Format("get_{0}", this.owner.Name);
                        case PropertyMethodType.SetMethod:
                            return string.Format("set_{0}", this.owner.Name);
                        default:
                            throw new InvalidOperationException();
                    }
                }

                protected override IType OnGetReturnType()
                {
                    switch (this.MethodType)
                    {
                        case PropertyMethodType.GetMethod:
                            return this.Owner.PropertyType;
                        case PropertyMethodType.SetMethod:
                            return this.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
                        default:
                            return base.OnGetReturnType();
                    }
                }

                IParameterMemberDictionary IIntermediatePropertySignatureMethodMember.Parameters
                {
                    get { return (IParameterMemberDictionary)this.Parameters; }
                }

                #region IPropertySignatureMethodMember Members
                /// <summary>
                /// Returns the <see cref="PropertyMethodType"/> which 
                /// denotes which method of the property the <see cref="IndexerMethodMember"/> is.
                /// </summary>
                public PropertyMethodType MethodType
                {
                    get { return this.methodType; }
                }

                #endregion

            }
        }
        public class PropertyMember :
            IntermediatePropertySignatureMember<IGenericParameterPropertyMember<TGenericParameter>, IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter, PropertyMember.PropertyMethodMember>,
            IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>
        {

            /// <summary>
            /// Creates a new <see cref="PropertyMember"/> with the <paramref name="name"/>,
            /// and <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="name">The <see cref="String"/> value that denotes the unique
            /// name of the property.</param>
            /// <param name="parent">The <typeparamref name="TIntermediateGenericParameter"/>
            /// which contains the <see cref="PropertyMember"/>.</param>
            internal PropertyMember(string name, TIntermediateGenericParameter parent)
                : base(name, parent)
            {

            }

            protected override PropertyMember.PropertyMethodMember GetMethodSignatureMember(PropertyMethodType methodType)
            {
                switch (methodType)
                {
                    case PropertyMethodType.SetMethod:
                        return new PropertySetMethodMember(this);
                    case PropertyMethodType.GetMethod:
                    default:
                        return new PropertyMethodMember(PropertyMethodType.GetMethod, this);
                }
            }

            public override void Visit(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }

            public override TResult Visit<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
            {
                return visitor.Visit(this, context);
            }

            public sealed class PropertySetMethodMember :
                PropertyMethodMember,
                IIntermediatePropertySignatureSetMethodMember
            {
                private IIntermediateSignatureParameterMember valueParameter;

                public PropertySetMethodMember(PropertyMember owner)
                    : base(PropertyMethodType.SetMethod, owner)
                {
                }

                #region IIntermediatePropertySignatureSetMethodMember Members

                public IIntermediateSignatureParameterMember ValueParameter
                {
                    get
                    {
                        if (this.valueParameter == null)
                            return this.Parameters["value"];
                        return this.valueParameter;
                    }
                }

                #endregion

                protected override IntermediateParameterMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    this.valueParameter = result._Add(new TypedName("value", this.Owner.PropertyType));
                    return result;
                }
            }

            public class PropertyMethodMember :
                MethodMember, 
                IIntermediatePropertySignatureMethodMember
            {
                public PropertyMethodMember(PropertyMethodType methodType, PropertyMember owner)
                    : base(owner.Parent)
                {
                    this.Owner = owner;
                    this.MethodType = methodType;
                }
                #region IPropertySignatureMethodMember Members

                public PropertyMethodType MethodType { get; private set; }

                #endregion

                protected override IntermediateParameterMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    result.Lock();
                    return result;
                }

                protected override IntermediateMethodSignatureMemberBase<IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>, IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.TypeParameterDictionary InitializeTypeParameters()
                {
                    var result = base.InitializeTypeParameters();
                    result.Lock();
                    return result;
                }

                protected override void OnSetReturnType(IType value)
                {
                    throw new NotSupportedException(string.Format("Cannot set the return-type of the {0} method of a property.", MethodType == PropertyMethodType.SetMethod ? "set" : "get"));
                }

                protected override string OnGetName()
                {
                    switch (this.MethodType)
                    {
                        case PropertyMethodType.SetMethod:
                            return string.Format("set_{0}", this.Owner.Name);
                        default:
                        case PropertyMethodType.GetMethod:
                            return string.Format("get_{0}", this.Owner.Name);
                    }
                }

                protected override IType OnGetReturnType()
                {
                    switch (this.MethodType)
                    {
                        case PropertyMethodType.GetMethod:
                            return this.Owner.PropertyType;
                        case PropertyMethodType.SetMethod:
                            return this.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
                        default:
                            return base.OnGetReturnType();
                    }
                }

                protected override sealed void OnSetName(string name)
                {
                    throw new NotSupportedException(string.Format("Cannot set the name of the {0} method of a property.", MethodType == PropertyMethodType.SetMethod ? "set" : "get"));
                }

                protected PropertyMember Owner { get; private set; }

                IParameterMemberDictionary IIntermediatePropertySignatureMethodMember.Parameters
                {
                    get { return (IParameterMemberDictionary)this.Parameters; }
                }
            }
        }

    }
}
