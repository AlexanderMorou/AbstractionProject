using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Utilities.Events;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides a base implementation for working with an intermediate indexer member.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of indexzer in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexerParent">The type the <typeparamref name="TIndexer"/> instances
    /// belong to in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexerParent">The type the <typeparamref name="TIntermediateIndexer"/> instances
    /// belong to in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TMethodMember">The type of <see cref="IIntermediatePropertyMethodMember"/>
    /// which relates to the get and set accessors in the intermediate abstract syntax tree.</typeparam>
    public abstract partial class IntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember> :
        IntermediateSignatureMemberBase<IGeneralSignatureMemberUniqueIdentifier, TIndexer, TIntermediateIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>, TIndexerParent, TIntermediateIndexerParent>,
        IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>,
        _IIntermediateIndexerSignatureMember
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>,
            IIntermediateTypeParent,
            TIndexerParent
        where TMethodMember :
            class,
            IIntermediatePropertyMethodMember
    {
        /// <summary>
        /// Data member for <see cref="AccessLevel"/>.
        /// </summary>
        private AccessLevelModifiers accessLevel;
        /// <summary>
        /// Data member for <see cref="Attributes"/>.
        /// </summary>
        private ExtendedMemberAttributes instanceFlags;
        /// <summary>
        /// Data member used for the <see cref="PropertyType"/> property.
        /// </summary>
        private IType propertyType;

        /// <summary>
        /// Data member for <see cref="GetMethod"/>.
        /// </summary>
        private TMethodMember getMethod;

        /// <summary>
        /// Data member for <see cref="SetMethod"/>.
        /// </summary>
        private TMethodMember setMethod;

        /// <summary>
        /// Data member for <see cref="CanRead"/>.
        /// </summary>
        private bool canRead;
        /// <summary>
        /// Data member for <see cref="CanWrite"/>.
        /// </summary>
        private bool canWrite;

        /// <summary>
        /// Data member for <see cref="UniqueIdentifier"/>.
        /// </summary>
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;

        private IMetadataCollection metadataBack;
        private IMetadataDefinitionCollection metadata;

        private TypeCollectionWithEvents<IInterfaceType> implementationTypes;


        /// <summary>
        /// Creates a new <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/>
        /// which contains the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/></param>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/> which
        /// contains the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        protected IntermediateIndexerMember(TIntermediateIndexerParent parent, IIntermediateAssembly assembly)
            : base(parent, assembly)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the unique identifier of the
        /// <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/> which contains
        /// the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/> which
        /// contains the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        protected IntermediateIndexerMember(string name, TIntermediateIndexerParent parent, IIntermediateAssembly assembly)
            : base(name, parent, assembly)
        {
        }

        public ITypeCollection Implementations
        {
            get { return this.implementationTypes ?? (this.implementationTypes = new TypeCollectionWithEvents<IInterfaceType>(this.implementationTypes_GlobalDelta)); }
        }

        private void implementationTypes_GlobalDelta(object sender, EventArgs e)
        {
            this.OnIdentifierChanged(this._UniqueIdentifier, DeclarationChangeCause.IdentityCardinality);
        }

        IEnumerable<IInterfaceType> IExtendedInstanceMember.Implementations
        {
            get
            {
                foreach (IType t in this.Implementations)
                    yield return (IInterfaceType)t;
            }
        }

        internal IGeneralSignatureMemberUniqueIdentifier _UniqueIdentifier
        {
            get
            {
                return this.uniqueIdentifier;
            }
        }


        #region IIntermediatePropertyMember Members

        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanRead"/> is false.</remarks>
        public TMethodMember GetMethod
        {
            get
            {
                if (this.canRead)
                {
                    if (this.getMethod == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.getMethod = this.GetMethodMember(PropertyMethodType.GetMethod);
                    return this.getMethod;
                }
                else
                    return null;
            }
        }



        protected abstract TMethodMember GetMethodMember(PropertyMethodType methodType);

        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanWrite"/> is false.</remarks>
        public TMethodMember SetMethod
        {
            get
            {
                if (this.canWrite)
                {
                    if (setMethod == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.setMethod = this.GetMethodMember(PropertyMethodType.SetMethod);
                    return this.setMethod;
                }
                else
                    return null;
            }
        }

        IIntermediatePropertyMethodMember IIntermediatePropertyMember.GetMethod
        {
            get
            {
                return this.GetMethod;
            }
        }

        IIntermediatePropertySetMethodMember IIntermediatePropertyMember.SetMethod
        {
            get
            {
                return (IIntermediatePropertySetMethodMember)this.SetMethod;
            }
        }

        #endregion

        #region IIntermediatePropertySignatureMember Members

        /// <summary>
        /// Returns/sets the type that the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// is defined as.
        /// </summary>
        public IType PropertyType
        {
            get
            {
                return this.propertyType;
            }
            set
            {
                this.propertyType = value;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// can be read from.
        /// </summary>
        /// <remarks>If set to false, from true, the <see cref="IIntermediateMethodSignatureMember"/> for the <see cref="GetMethod"/>
        /// will be disposed.</remarks>
        public bool CanRead
        {
            get
            {
                return this.canRead;
            }
            set
            {
                if (!value && this.canRead && this.getMethod != null)
                {
                    this.getMethod.Dispose();
                    this.getMethod = null;
                }
                this.canRead = value;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/> 
        /// can be written to.
        /// </summary>
        /// <remarks>If set to false, from true, the <see cref="IIntermediateMethodSignatureMember"/> for the <see cref="SetMethod"/>
        /// will be disposed.</remarks>
        public bool CanWrite
        {
            get
            {
                return this.canWrite;
            }
            set
            {
                if (!value && this.canWrite && this.setMethod != null)
                {
                    this.setMethod.Dispose();
                    this.setMethod = null;
                }
                this.canWrite = value;
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanRead"/> is false.</remarks>
        IIntermediatePropertySignatureMethodMember IIntermediatePropertySignatureMember.GetMethod
        {
            get
            {
                return this.GetMethod;
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanWrite"/> is false.</remarks>
        IIntermediatePropertySignatureSetMethodMember IIntermediatePropertySignatureMember.SetMethod
        {
            get
            {
                return (IIntermediatePropertySignatureSetMethodMember)this.SetMethod;
            }
        }

        #endregion

        #region IPropertySignatureMember Members

        IPropertySignatureMethodMember IPropertySignatureMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        #endregion

        #region IIntermediateExtendedInstanceMember Members

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/> is 
        /// abstract (must be implemented, or is not yet 
        /// implemented).
        /// </summary>
        public bool IsAbstract
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.Abstract) == ExtendedMemberAttributes.Abstract);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedMemberAttributes.Static | ExtendedMemberAttributes.Virtual | ExtendedMemberAttributes.Override | ExtendedMemberAttributes.Final);
                    this.instanceFlags |= ExtendedMemberAttributes.Abstract;
                }
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.Abstract;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/> is
        /// virtual (can be overridden).
        /// </summary>
        public bool IsVirtual
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.Virtual) == ExtendedMemberAttributes.Virtual);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedMemberAttributes.Static | ExtendedMemberAttributes.Abstract | ExtendedMemberAttributes.Override | ExtendedMemberAttributes.Final);
                    this.instanceFlags |= ExtendedMemberAttributes.Virtual;
                }
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.Virtual;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// finalizes the member removing the overrideable 
        /// status.
        /// </summary>
        public bool IsFinal
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.Final) == ExtendedMemberAttributes.Final);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedMemberAttributes.Virtual | ExtendedMemberAttributes.Abstract | ExtendedMemberAttributes.Static);
                    this.instanceFlags |= (ExtendedMemberAttributes.Final | ExtendedMemberAttributes.Override);
                }
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.Final;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/> 
        /// is an override of a virtual member.
        /// </summary>
        public bool IsOverride
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.Override) == ExtendedMemberAttributes.Override);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedMemberAttributes.Static | ExtendedMemberAttributes.Abstract | ExtendedMemberAttributes.Virtual);
                    this.instanceFlags |= ExtendedMemberAttributes.Override;
                }
                else
                    this.instanceFlags &= (ExtendedMemberAttributes.Override | ExtendedMemberAttributes.Final);
            }
        }

        #endregion

        #region IIntermediateInstanceMember Members

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// hides the original definition completely.
        /// </summary>
        public bool IsHideBySignature
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.HideBySignature) == ExtendedMemberAttributes.HideBySignature);
            }
            set
            {
                if (value == IsHideBySignature)
                    return;
                if (value)
                {
                    this.instanceFlags |= ExtendedMemberAttributes.HideBySignature;
                }
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.HideBySignature;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/> is
        /// static.
        /// </summary>
        public bool IsStatic
        {
            get
            {
                if (Parent is IIntermediateClassType)
                {
                    var intermediateParent = Parent as IIntermediateClassType;
                    var specialModifier = intermediateParent.SpecialModifier;
                    if ((specialModifier & SpecialClassModifier.Static) == SpecialClassModifier.Static ||
                        (specialModifier & SpecialClassModifier.Module) == SpecialClassModifier.Module)
                        return true;
                }
                return IsExplicitStatic;
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedMemberAttributes.Abstract | ExtendedMemberAttributes.Virtual | ExtendedMemberAttributes.Override | ExtendedMemberAttributes.Final);
                    this.instanceFlags |= ExtendedMemberAttributes.Static;
                }
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.Static;
            }
        }

        public bool IsExplicitStatic
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.Static) == ExtendedMemberAttributes.Static);
            }
        }
        #endregion

        #region IInstanceMember Members

        InstanceMemberAttributes IInstanceMember.Attributes
        {
            get { return ((InstanceMemberAttributes)(this.Attributes)); }
        }

        #endregion

        #region IExtendedInstanceMember Members

        /// <summary>
        /// Returns the <see cref="ExtendedMemberAttributes"/> that determine how the
        /// <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// is shown in its scope and inherited scopes.
        /// </summary>
        public ExtendedMemberAttributes Attributes
        {
            get { return this.instanceFlags; }
        }

        #endregion

        #region IIntermediateScopedDeclaration Members

        /// <summary>
        /// Returns/sets the access level of the
        /// <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.
        /// </summary>
        public AccessLevelModifiers AccessLevel
        {
            get
            {
                return this.accessLevel;
            }
            set
            {
                this.accessLevel = value;
            }
        }

        #endregion

        #region IPropertyMember Members

        IPropertyMethodMember IPropertyMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        IPropertyMethodMember IPropertyMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        #endregion

        #region IIndexerMember Members

        IPropertyMethodMember IIndexerMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        IPropertyMethodMember IIndexerMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        #endregion

        /// <summary>
        /// Initializes the <see cref="IntermediateParameterParentMemberBase{TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}.Parameters"/>.
        /// </summary>
        /// <returns>A new <see cref="ParameterMembersDictionary"/> associated to the current
        /// <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</returns>
        protected override IntermediateParameterMemberDictionary<TIndexer, TIntermediateIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>> InitializeParameters()
        {
            return new ParameterMembersDictionary((TIntermediateIndexer)(object)this);
        }

        #region IIntermediateIndexerMember<TIndexer,TIntermediateIndexer,TIndexerParent,TIntermediateIndexerParent> Members

        public IIndexerReferenceExpression<TIndexer, TIndexerParent> GetReference(IMemberParentReferenceExpression parent, params IExpression[] parameters)
        {
            return this.GetReference(parent, (IEnumerable<IExpression>)parameters);
        }

        public IIndexerReferenceExpression<TIndexer, TIndexerParent> GetReference(IMemberParentReferenceExpression source, IEnumerable<IExpression> parameters)
        {
            return new IndexerReferenceExpression<TIndexer, TIndexerParent>(((TIntermediateIndexer)(object)(this)), parameters, source);
        }

        #endregion

        #region IIntermediatePropertySignatureMember Members


        IPropertyReferenceExpression IIntermediatePropertySignatureMember.GetReference(IMemberParentReferenceExpression source)
        {
            return this.GetReference(source);
        }

        #endregion

        #region IIntermediateIndexerSignatureMember Members

        IIndexerReferenceExpression IIntermediateIndexerSignatureMember.GetReference(IMemberParentReferenceExpression parent, params IExpression[] parameters)
        {
            return this.GetReference(parent, parameters);
        }

        IIndexerReferenceExpression IIntermediateIndexerSignatureMember.GetReference(IMemberParentReferenceExpression parent, IEnumerable<IExpression> parameters)
        {
            return this.GetReference(parent, (IEnumerable<IExpression>)parameters);
        }

        #endregion

        public override void Accept(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        #region IIntermediateIndexerMember Members

        IIntermediatePropertyMethodMember IIntermediateIndexerMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        IIntermediatePropertySetMethodMember IIntermediateIndexerMember.SetMethod
        {
            get { return (IIntermediatePropertySetMethodMember)this.SetMethod; }
        }

        #endregion

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (uniqueIdentifier == null)
                    {
                        var service = this.Assembly.GetUniqueIdentifierService();
                        this.uniqueIdentifier = service.HandlesIndexerMemberIdentifier
                                                ? service.GetIdentifier(this)
                                                : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(this);
                    }
                    return this.uniqueIdentifier;
                }
            }
        }

        public IMetadataDefinitionCollection Metadata
        {
            get
            {
                var qe = this.Parent.Classes;
                if (this.metadata == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.metadata = new MetadataDefinitionCollection(this, this.Parent.Assembly);
                return this.metadata;
            }
        }

        IMetadataCollection IMetadataEntity.Metadata
        {
            get
            {
                if (this.metadataBack != null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                    else
                        this.metadataBack = ((MetadataDefinitionCollection) (this.Metadata)).GetWrapper();
                return this.metadataBack;
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (this.uniqueIdentifier != null)
                    this.uniqueIdentifier = null;
                if (this.metadata != null)
                    this.metadata = null;
                if (this.canRead)
                    this.canRead = false;
                if (this.getMethod != null)
                {
                    this.getMethod.Dispose();
                    this.getMethod = null;
                }
                if (this.canWrite)
                    this.canWrite = false;
                if (this.setMethod != null)
                {
                    this.setMethod.Dispose();
                    this.setMethod = null;
                }
                if (this.implementationTypes != null)
                {
                    this.implementationTypes.Dispose();
                    this.implementationTypes.Clear();
                    this.implementationTypes = null;
                }
                this.propertyType = null;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return this.Metadata.Contains(metadatumType);
        }

        protected internal bool IsGetMethodInitialized { get { return this.getMethod != null; } }
        protected internal bool IsSetMethodInitialized { get { return this.setMethod != null; } }

        protected override void ClearIdentifier()
        {
            lock (this.SyncObject)
                this.uniqueIdentifier = null;
        }

        bool _IIntermediateIndexerSignatureMember.AreParametersInitialized
        {
            get { return this.AreParametersInitialized; }
        }

        bool _IIntermediateIndexerSignatureMember.IsDisposed
        {
            get { return this.IsDisposed; }
        }

        public bool HideByName
        {
            get
            {
                return (this.Attributes & ExtendedMemberAttributes.HideByName) == ExtendedMemberAttributes.HideByName;
            }
            set
            {
                if ((this.instanceFlags & ExtendedMemberAttributes.HideBySignature) == ExtendedMemberAttributes.HideBySignature)
                    this.instanceFlags &= ~ExtendedMemberAttributes.HideBySignature;

                if (value)
                    this.instanceFlags |= ExtendedMemberAttributes.HideByName;
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.HideByName;
            }
        }
    }
    internal interface _IIntermediateIndexerSignatureMember
    {
        bool AreParametersInitialized { get; }
        bool IsDisposed { get; }
    }
}
