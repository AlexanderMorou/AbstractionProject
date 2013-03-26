using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Properties;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
        IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
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
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the unique name of the 
        /// <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/> which contains the
        /// <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.</param>
        /// <param name="identityManager">The <see cref="ITypeIdentityManager"/>
        /// which is responsible for maintaining type identity within the current type
        /// model.</param>
        public IntermediateIndexerSignatureMember(string name, TIntermediateIndexerParent parent, ITypeIdentityManager identityManager)
            : base(name, parent, identityManager)
        {
        }

        protected IntermediateIndexerSignatureMember(TIntermediateIndexerParent parent, ITypeIdentityManager identityManager)
            : base(parent, identityManager)
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
                return this.propertyType;
            }
            set
            {
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
        /// Returns/sets whether the <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/> 
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
        /// <see cref="IntermediateIndexerSignatureMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanRead"/> is false.</remarks>
        public IIntermediatePropertySignatureMethodMember GetMethod
        {
            get
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

        protected override void OnIdentifierChanged(IGeneralSignatureMemberUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            if (this.uniqueIdentifier != null)
                this.uniqueIdentifier = null;
            base.OnIdentifierChanged(oldIdentifier, cause);
        }

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                    else
                        if (this.AreParametersInitialized)
                            this.uniqueIdentifier = AstIdentifier.GetSignatureIdentifier(this.Name, this.Parameters.ParameterTypes.ToArray());
                        else
                            this.uniqueIdentifier = AstIdentifier.GetSignatureIdentifier(this.Name);
                return this.uniqueIdentifier;
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
            return new ParametersDictionary((TIntermediateIndexer)(object)this, this.IdentityManager);
        }

        public override void Visit(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
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
                if (this.metadataBack != null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                    else
                        this.metadataBack = ((MetadataDefinitionCollection) (this.Metadata)).GetWrapper();
                return this.metadataBack;
            }
        }

        public IMetadataDefinitionCollection Metadata
        {
            get
            {
                if (this.metadata == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.metadata = new MetadataDefinitionCollection(this, this.IdentityManager);
                return this.metadata;
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
                this.propertyType = null;
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
    }
}
