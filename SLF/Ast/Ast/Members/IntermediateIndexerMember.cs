﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
        IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            TIndexerParent,
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TMethodMember :
            class,
            IIntermediatePropertyMethodMember
    {
        /// <summary>
        /// Data member for <see cref="AccessLevel"/>.
        /// </summary>
        private AccessLevelModifiers accessLevel;
        /// <summary>
        /// Data member for <see cref="InstanceFlags"/>.
        /// </summary>
        private ExtendedInstanceMemberFlags instanceFlags;
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

        /// <summary>
        /// Creates a new <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/>
        /// which contains the <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/></param>
        protected IntermediateIndexerMember(TIntermediateIndexerParent parent)
            : base(parent)
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
        protected IntermediateIndexerMember(string name, TIntermediateIndexerParent parent)
            : base(name, parent)
        {
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
                    if (this.IsGetMethodInitialized)
                        this.getMethod = this.GetMethodMember(PropertyMethodType.GetMethod);
                    return this.getMethod;
                }
                else
                    return null;
            }
        }

        internal bool IsGetMethodInitialized
        {
            get
            {
                return this.getMethod == null;
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
                    if (this.IsSetMethodInitialized)
                        this.setMethod = this.GetMethodMember(PropertyMethodType.SetMethod);
                    return this.setMethod;
                }
                else
                    return null;
            }
        }

        internal bool IsSetMethodInitialized
        {
            get
            {
                return this.setMethod == null;
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
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Abstract) == ExtendedInstanceMemberFlags.Abstract);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedInstanceMemberFlags.Static | ExtendedInstanceMemberFlags.Virtual | ExtendedInstanceMemberFlags.Override | ExtendedInstanceMemberFlags.Final);
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Abstract;
                }
                else
                    this.instanceFlags &= ~ExtendedInstanceMemberFlags.Abstract;
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
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Virtual) == ExtendedInstanceMemberFlags.Virtual);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedInstanceMemberFlags.Static | ExtendedInstanceMemberFlags.Abstract | ExtendedInstanceMemberFlags.Override | ExtendedInstanceMemberFlags.Final);
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Virtual;
                }
                else
                    this.instanceFlags &= ~ExtendedInstanceMemberFlags.Virtual;
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
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Final) == ExtendedInstanceMemberFlags.Final);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedInstanceMemberFlags.Virtual | ExtendedInstanceMemberFlags.Abstract | ExtendedInstanceMemberFlags.Static);
                    this.instanceFlags |= (ExtendedInstanceMemberFlags.Final | ExtendedInstanceMemberFlags.Override);
                }
                else
                    this.instanceFlags &= ~ExtendedInstanceMemberFlags.Final;
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
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Override) == ExtendedInstanceMemberFlags.Override);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedInstanceMemberFlags.Static | ExtendedInstanceMemberFlags.Abstract | ExtendedInstanceMemberFlags.Virtual);
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Override;
                }
                else
                    this.instanceFlags &= (ExtendedInstanceMemberFlags.Override | ExtendedInstanceMemberFlags.Final);
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
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.HideBySignature) == ExtendedInstanceMemberFlags.HideBySignature);
            }
            set
            {
                if (value == IsHideBySignature)
                    return;
                if (value)
                {
                    this.instanceFlags |= ExtendedInstanceMemberFlags.HideBySignature;
                }
                else
                    this.instanceFlags &= ~ExtendedInstanceMemberFlags.HideBySignature;
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
                if (Parent is IClassType)
                {
                    var parent = (IClassType)Parent;
                    if (parent.SpecialModifier != SpecialClassModifier.None)
                        return true;
                }
                return IsExplicitStatic;
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedInstanceMemberFlags.Abstract | ExtendedInstanceMemberFlags.Virtual | ExtendedInstanceMemberFlags.Override | ExtendedInstanceMemberFlags.Final);
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Static;
                }
                else
                    this.instanceFlags &= ~ExtendedInstanceMemberFlags.Static;
            }
        }

        public bool IsExplicitStatic
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Static) == ExtendedInstanceMemberFlags.Static);
            }
        }
        #endregion

        #region IInstanceMember Members

        InstanceMemberFlags IInstanceMember.InstanceFlags
        {
            get { return ((InstanceMemberFlags)(this.InstanceFlags)); }
        }

        #endregion

        #region IExtendedInstanceMember Members

        /// <summary>
        /// Returns the <see cref="ExtendedInstanceMemberFlags"/> that determine how the
        /// <see cref="IntermediateIndexerMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember}"/>
        /// is shown in its scope and inherited scopes.
        /// </summary>
        public ExtendedInstanceMemberFlags InstanceFlags
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

        public override void Visit(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
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
            get {
                if (this.uniqueIdentifier == null)
                    if (this.AreParametersInitialized)
                        this.uniqueIdentifier = AstIdentifier.Signature(this.Name, this.Parameters.ParameterTypes.ToArray());
                    else
                        this.uniqueIdentifier = AstIdentifier.Signature(this.Name);
                return this.uniqueIdentifier;
            }
        }

        protected override void OnIdentifierChanged(IGeneralSignatureMemberUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            if (this.uniqueIdentifier != null)
                this.uniqueIdentifier = null;
            base.OnIdentifierChanged(oldIdentifier, cause);
        }
    }
}