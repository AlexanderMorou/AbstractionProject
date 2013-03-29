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
    /// <typeparam name="TProperty">The type of property signature used in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property signature used in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which acts as the parent of the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which acts as the parent of the 
    /// properties in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TMethodMember">The type of method member used within
    /// the property signatures within the intermediate abstract
    /// syntax tree.</typeparam>
    public abstract class IntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember> :
        IntermediateMemberBase<IGeneralMemberUniqueIdentifier, TPropertyParent, TIntermediatePropertyParent>,
        IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TPropertyParent :
            IPropertySignatureParent<TProperty, TPropertyParent>
        where TIntermediatePropertyParent :
            TPropertyParent,
            IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
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
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;
        private IMetadataDefinitionCollection metadata;
        private IMetadataCollection metadataBack;

        /// <summary>
        /// Creates a new <see cref="IntermediatePropertySignatureMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the unique name of the 
        /// <see cref="IntermediatePropertySignatureMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediatePropertyParent"/> which contains the
        /// <see cref="IntermediatePropertySignatureMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>.</param>
        public IntermediatePropertySignatureMember(string name, TIntermediatePropertyParent parent)
            : base(name, parent)
        {
        }

        #region IIntermediatePropertySignatureMember Members

        /// <summary>
        /// Returns/sets the type that the <see cref="IntermediatePropertySignatureMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>
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
        /// Returns/sets whether the <see cref="IntermediatePropertySignatureMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>
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
        /// Returns/sets whether the <see cref="IntermediatePropertySignatureMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/> 
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
        /// <see cref="IntermediatePropertySignatureMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanRead"/> is false.</remarks>
        public IIntermediatePropertySignatureMethodMember GetMethod
        {
            get {
                lock (this.SyncObject)
                {
                    if (this.canRead)
                    {
                        if (this.getMethod == null)
                            if (this.IsDisposed)
                                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
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
        /// <see cref="IntermediatePropertySignatureMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>.
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
                                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
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

        /// <summary>
        /// Obtains a <typeparamref name="TMethodMember"/> associated to the
        /// <paramref name="methodType"/> provided.
        /// </summary>
        /// <param name="methodType">The <see cref="PropertyMethodType"/> which denotes
        /// the kind of method to return, for either the get or set accessor.</param>
        /// <returns>A <typeparamref name="TMethodMember"/>
        /// instance which represents the get or set accessor of the property.</returns>
        protected abstract TMethodMember GetMethodSignatureMember(PropertyMethodType methodType);

        #region IIntermediatePropertySignatureMember Members

        IPropertyReferenceExpression IIntermediatePropertySignatureMember.GetReference(IMemberParentReferenceExpression source)
        {
            return IntermediateGateway.GetPropertySignatureReference<TProperty, TPropertyParent>(((TProperty)(object)(this)), source);
        }

        #endregion

        protected override void OnIdentifierChanged(IGeneralMemberUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            lock (this.SyncObject)
            {
                if (this.uniqueIdentifier != null)
                    this.uniqueIdentifier = null;
                base.OnIdentifierChanged(oldIdentifier, cause);
            }
        }

        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get {
                lock (this.SyncObject)
                {
                    if (this.uniqueIdentifier == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.uniqueIdentifier = TypeSystemIdentifiers.GetMemberIdentifier(this.Name);
                    return this.uniqueIdentifier;
                }
            }
        }

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
                            this.metadata = new MetadataDefinitionCollection(this, this.Parent.IdentityManager);
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

        protected override void ClearIdentifier()
        {
            lock (this.SyncObject)
                if (this.uniqueIdentifier != null)
                    this.uniqueIdentifier = null;
        }

        public bool IsDefined(IType metadatumType)
        {
            return this.Metadata.Contains(metadatumType);
        }
    }
}
