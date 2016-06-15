using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
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
    /// Provides a base class for working with an intermediate
    /// property signature.
    /// </summary>
    /// <typeparam name="TIndexer">The type of property signature used in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of property signature used in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexerParent">The type which acts as the parent of the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexerParent">The type which acts as the parent of the 
    /// properties in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TMethodMember">The type of method member used within
    /// the property signatures within the intermediate abstract
    /// syntax tree.</typeparam>
    public abstract partial class IntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember> :
        IntermediateSignatureMemberBase<IGeneralSignatureMemberUniqueIdentifier, TIndexer, TIntermediateIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>, TIndexerParent, TIntermediateIndexerParent>,
        IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>,
        _IIntermediateIndexerSignatureMember
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            TIndexerParent,
            IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TMethodMember :
            class,
            IIntermediatePropertySignatureMethodMember
    {
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
        private IMetadataDefinitionCollection metadata;
        private IMetadataCollection metadataBack;

        /// <summary>
        /// Creates a new <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// with the <paramref name="name"/>, <paramref name="parent"/> and <paramref name="assembly"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the unique name of the 
        /// <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/> which contains the
        /// <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/> which
        /// contains the <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        public IntermediateIndexerSignatureMember(string name, TIntermediateIndexerParent parent, IIntermediateAssembly assembly)
            : base(name, parent, assembly)
        {
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// with the <paramref name="parent"/> and <paramref name="assembly"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/> which contains the
        /// <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/> which
        /// contains the <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        protected IntermediateIndexerSignatureMember(TIntermediateIndexerParent parent, IIntermediateAssembly assembly)
            : base(parent, assembly)
        {
        }

        #region IIntermediateIndexerSignatureMember Members

        /// <summary>
        /// Returns/sets the type that the <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// is defined as.
        /// </summary>
        public IType PropertyType
        {
            get
            {
                lock (this.SyncObject)
                    return this.propertyType;
            }
            set
            {
                lock (this.SyncObject)
                    this.propertyType = value;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// can be read from.
        /// </summary>
        /// <remarks>If set to false, from true, the <see cref="IIntermediateMethodSignatureMember"/> for the <see cref="GetMethod"/>
        /// will be disposed.</remarks>
        public bool CanRead
        {
            get
            {
                lock (this.SyncObject)
                    return this.canRead;
            }
            set
            {
                lock (this.SyncObject)
                {
                    if (!value && this.canRead && this.getMethod != null)
                    {
                        this.getMethod.Dispose();
                        this.getMethod = null;
                    }
                    this.canRead = value;
                }
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/> 
        /// can be written to.
        /// </summary>
        /// <remarks>If set to false, from true, the <see cref="IIntermediateMethodSignatureMember"/> for the <see cref="SetMethod"/>
        /// will be disposed.</remarks>
        public bool CanWrite
        {
            get
            {
                lock (this.SyncObject)
                    return this.canWrite;
            }
            set
            {
                lock (this.SyncObject)
                {
                    if (!value && this.canWrite && this.setMethod != null)
                    {
                        this.setMethod.Dispose();
                        this.setMethod = null;
                    }
                    this.canWrite = value;
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanRead"/> is false.</remarks>
        public IIntermediatePropertySignatureMethodMember GetMethod
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.canRead)
                    {
                        if (this.getMethod == null)
                            if (this.IsDisposed)
                                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                            else
                                this.getMethod = this.GetMethodSignatureMember(PropertyMethodType.GetMethod);
                        return this.getMethod;
                    }
                    else
                        return null;
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanWrite"/> is false.</remarks>
        public IIntermediatePropertySignatureSetMethodMember SetMethod
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.canWrite)
                    {
                        if (this.setMethod == null)
                            if (this.IsDisposed)
                                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                            else
                                this.setMethod = this.GetMethodSignatureMember(PropertyMethodType.SetMethod);
                        return (IIntermediatePropertySignatureSetMethodMember)this.setMethod;
                    }
                    else
                        return null;
                }
            }
        }

        #endregion

        #region IIndexerSignatureMember Members

        IPropertySignatureMethodMember IPropertySignatureMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        #endregion

        /// <summary>
        /// Obtains a <typeparamref name="TMethodMember"/> associated to the
        /// <paramref name="methodType"/> provided.
        /// </summary>
        /// <param name="methodType">The <see cref="PropertyMethodType"/> which denotes
        /// the kind of method to return, for either the get or set accessor.</param>
        /// <returns>A <typeparamref name="TMethodMember"/>
        /// instance which represents the get or set accessor of the property.</returns>
        protected abstract TMethodMember GetMethodSignatureMember(PropertyMethodType methodType);

        #region IIntermediateIndexerSignatureMember Members

        IIndexerReferenceExpression IIntermediateIndexerSignatureMember.GetReference(IMemberParentReferenceExpression source, IEnumerable<IExpression> parameters)
        {
            return this.GetReference(source, parameters);
        }

        IIndexerReferenceExpression IIntermediateIndexerSignatureMember.GetReference(IMemberParentReferenceExpression source, IExpression[] parameters)
        {
            return this.GetReference(source, parameters);
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
                        this.uniqueIdentifier = service.HandlesIndexerSignatureMemberIdentifier
                                                ? service.GetIdentifier(this)
                                                : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(this);
                    }
                    return this.uniqueIdentifier;
                }
            }
        }

        #region IIntermediateIndexerSignatureMember<TIndexer,TIntermediateIndexer,TIndexerParent,TIntermediateIndexerParent> Members

        public IIndexerSignatureReferenceExpression<TIndexer, TIndexerParent> GetReference(IMemberParentReferenceExpression source, params IExpression[] parameters)
        {
            return IntermediateGateway.GetIndexerSignatureReference<TIndexer, TIndexerParent>(((TIndexer)(object)(this)), source, parameters);
        }

        public IIndexerSignatureReferenceExpression<TIndexer, TIndexerParent> GetReference(IMemberParentReferenceExpression source, IEnumerable<IExpression> parameters)
        {
            return IntermediateGateway.GetIndexerSignatureReference<TIndexer, TIndexerParent>(((TIndexer)(object)(this)), source, parameters);
        }

        #endregion

        protected override IntermediateParameterMemberDictionary<TIndexer, TIntermediateIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>> InitializeParameters()
        {
            return new ParametersDictionary((TIntermediateIndexer)(object)this, this.Assembly);
        }

        public override void Accept(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
    
        #region IIntermediatePropertySignatureMember Members


        public IPropertyReferenceExpression GetReference(IMemberParentReferenceExpression source = null)
        {
            return this.GetReference(source);
        }

        #endregion

        IMetadataCollection IMetadataEntity.Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadataBack != null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                        else
                            this.metadataBack = ((MetadataDefinitionCollection)(this.Metadata)).GetWrapper();
                    return this.metadataBack;
                }
            }
        }

        public IMetadataDefinitionCollection Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadata == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.metadata = new MetadataDefinitionCollection(this, this.Assembly);
                    return this.metadata;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                lock (this.SyncObject)
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
                    this.propertyType = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            //Metadata inheritance rules don't apply to methods.
            return this.Metadata.Contains(metadatumType);
        }

        internal bool IsGetMethodInitialized { get { return this.getMethod != null; } }
        internal bool IsSetMethodInitialized { get { return this.setMethod != null; } }

        protected override void ClearIdentifier()
        {
            lock (this.SyncObject)
                if (this.uniqueIdentifier != null)
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

        public bool HideByName { get; set; }
    }
}
