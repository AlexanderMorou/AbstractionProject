﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a functional base for generic parameters to derive from.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateGenericParameter">The type of generic parameter in
    /// the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TParent">The type which owns the generic parameters in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type which owns the generic parameters in the intermediate
    /// abstract syntax tree.</typeparam>
    public abstract partial class IntermediateGenericParameterBase<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> :
        IntermediateTypeBase<TGenericParameter, TIntermediateGenericParameter>,
        IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TIntermediateGenericParameter :
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TGenericParameter
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
        where TIntermediateParent :
            IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TParent
    {
        private ITypeCollection constraints;
        private ILockedTypeCollection _constraints;
        private ICustomAttributeDefinitionCollectionSeries customAttributes;
        private IntermediateFullMemberDictionary _members;
        private ConstructorsDictionary constructors;
        private EventsDictionary events;
        private IndexersDictionary indexers;
        private IIntermediateGenericParameterMethodMemberDictionary<TGenericParameter, TIntermediateGenericParameter> methods;
        private IIntermediateGenericParameterPropertyMemberDictionary<TGenericParameter, TIntermediateGenericParameter> properties;

        protected IntermediateGenericParameterBase(string name, TIntermediateParent parent)
            : base()
        {
            base.Name = name;
            if (parent is IIntermediateTypeParent)
                base.Parent = (IIntermediateTypeParent)parent;
            this.Parent = parent;
        }

        public override IIntermediateAssembly Assembly
        {
            get
            {
                var bAssembly = base.Assembly;
                if (bAssembly != null)
                    return bAssembly;
                IIntermediateMember mParent = this.Parent as IIntermediateMember;

                while (mParent != null)
                {
                    
                    var tParent = mParent.Parent as IIntermediateType;
                    if (tParent != null)
                        return tParent.Assembly;
                    else
                        mParent = mParent.Parent as IIntermediateMember;
                }
                return null;
            }
        }

        #region Check Methods

        private void Check_Members()
        {
            if (this._members == null)
                this._members = new IntermediateFullMemberDictionary();
        }

        private void CheckConstructors()
        {
            if (this.constructors == null)
                this.constructors = this.InitializeConstructors();
        }

        private void CheckEvents()
        {
            if (this.events == null)
                this.events = this.InitializeEvents();
        }

        private void CheckIndexers()
        {
            if (this.indexers == null)
                this.indexers = this.InitializeIndexers();
        }

        private void CheckMethods()
        {
            if (this.methods == null)
                this.methods = this.InitializeMethods();
        }

        private void CheckProperties()
        {
            if (this.properties == null)
                this.properties = this.InitializeProperties();
        }
        #endregion

        #region IIntermediateGenericParameter<TGenericParameter,TIntermediateGenericParameter,TParent,TIntermediateParent> Members

        public new TIntermediateParent Parent { get; internal set; }

        #endregion

        public ConstructorsDictionary Constructors
        {
            get
            {
                this.CheckConstructors();
                return this.constructors;
            }
        }

        public IndexersDictionary Indexers
        {
            get
            {
                this.CheckIndexers();
                return this.indexers;
            }
        }

        public EventsDictionary Events
        {
            get
            {
                this.CheckEvents();
                return this.events;
            }
        }

        public IIntermediateGenericParameterMethodMemberDictionary<TGenericParameter, TIntermediateGenericParameter> Methods
        {
            get
            {
                this.CheckMethods();
                return this.methods;
            }
        }

        public IIntermediateGenericParameterPropertyMemberDictionary<TGenericParameter, TIntermediateGenericParameter> Properties
        {
            get
            {
                this.CheckProperties();
                return this.properties;
            }
        }

        protected virtual ConstructorsDictionary InitializeConstructors()
        {
            return new ConstructorsDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        protected virtual EventsDictionary InitializeEvents()
        {
            return new EventsDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        private IndexersDictionary InitializeIndexers()
        {
            return new IndexersDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        private IIntermediateGenericParameterMethodMemberDictionary<TGenericParameter, TIntermediateGenericParameter> InitializeMethods()
        {
            throw new NotImplementedException();
        }

        private IIntermediateGenericParameterPropertyMemberDictionary<TGenericParameter, TIntermediateGenericParameter> InitializeProperties()
        {
            throw new NotImplementedException();
        }

        #region IIntermediateGenericParameter<TGenericParameter,TIntermediateGenericParameter> Members

        IIntermediateGenericParameterConstructorMemberDictionary<TGenericParameter, TIntermediateGenericParameter> IIntermediateGenericParameter<TGenericParameter,TIntermediateGenericParameter>.Constructors
        {
            get
            {
                return this.Constructors;
            }
        }

        IIntermediateGenericParameterEventMemberDictionary<TGenericParameter, TIntermediateGenericParameter> IIntermediateGenericParameter<TGenericParameter,TIntermediateGenericParameter>.Events
        {
            get
            {
                return this.Events;
            }
        }

        IIntermediateGenericParameterIndexerMemberDictionary<TGenericParameter, TIntermediateGenericParameter> IIntermediateGenericParameter<TGenericParameter,TIntermediateGenericParameter>.Indexers
        {
            get { return this.Indexers; }
        }

        IIntermediateGenericParameterMethodMemberDictionary<TGenericParameter, TIntermediateGenericParameter> IIntermediateGenericParameter<TGenericParameter,TIntermediateGenericParameter>.Methods
        {
            get { return this.Methods; }
        }

        IIntermediateGenericParameterPropertyMemberDictionary<TGenericParameter, TIntermediateGenericParameter> IIntermediateGenericParameter<TGenericParameter,TIntermediateGenericParameter>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IIntermediateCreatableSignatureType<IGenericParameterConstructorMember<TGenericParameter>,IIntermediateGenericParameterConstructorMember<TGenericParameter,TIntermediateGenericParameter>,TGenericParameter,TIntermediateGenericParameter> Members

        IIntermediateConstructorSignatureMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter> IIntermediateCreatableSignatureType<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.Constructors
        {
            get { return this.Constructors; }
        }
        #endregion

        #region IIntermediateCreatableSignatureType Members

        IIntermediateConstructorSignatureMemberDictionary IIntermediateCreatableSignatureType.Constructors
        {
            get { return this.Constructors; }
        }

        #endregion

        #region ICreatableType Members

        IConstructorMemberDictionary ICreatableType.Constructors
        {
            get { return ((IConstructorMemberDictionary)(this.Constructors)); }
        }

        IConstructorMember ICreatableType.TypeInitializer
        {
            get { return null; }
        }

        #endregion

        #region ICreatableType<IGenericParameterConstructorMember<TGenericParameter>,TGenericParameter> Members

        IConstructorMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter> ICreatableType<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>.Constructors
        {
            get { return this.Constructors; }
        }

        IGenericParameterConstructorMember<TGenericParameter> ICreatableType<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>.TypeInitializer
        {
            get { return null; }
        }

        #endregion

        #region IType<TGenericParameter> Members

        public new TGenericParameter ElementType
        {
            get { throw new InvalidOperationException("Not an array, by-ref, pointer, or nullable type."); }
        }

        #endregion

        #region IIntermediateEventSignatureParent<IGenericParameterEventMember<TGenericParameter>,IIntermediateGenericParameterEventMember<TGenericParameter,TIntermediateGenericParameter>,TGenericParameter,TIntermediateGenericParameter> Members

        IIntermediateEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter> IIntermediateEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.Events
        {
            get { return this.Events; }
        }

        #endregion

        #region IIntermediateEventSignatureParent<IGenericParameterEventMember<TGenericParameter>,IIntermediateGenericParameterEventMember<TGenericParameter,TIntermediateGenericParameter>,IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>,TGenericParameter>,IIntermediateEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>,IIntermediateGenericParameterEventMember<TGenericParameter,TIntermediateGenericParameter>,TGenericParameter,TIntermediateGenericParameter>,TGenericParameter,TIntermediateGenericParameter> Members

        IIntermediateEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter>, IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>, IIntermediateEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter> IIntermediateEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter>, IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>, IIntermediateEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.Events
        {
            get { return this.Events; }
        }

        #endregion

        #region IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>,IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>,TGenericParameter>,TGenericParameter> Members

        IEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>, TGenericParameter> IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>, TGenericParameter>.Events
        {
            get { return this.Events; }
        }

        #endregion

        #region IEventSignatureParent Members

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { return ((IEventSignatureMemberDictionary)(this.Events)); }
        }

        #endregion

        #region IIntermediateEventSignatureParent Members

        IIntermediateEventSignatureMemberDictionary IIntermediateEventSignatureParent.Events
        {
            get { return ((IIntermediateEventSignatureMemberDictionary)(this.Events)); }
        }

        #endregion

        #region IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>,TGenericParameter> Members

        IEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, TGenericParameter> IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>.Events
        {
            get { return this.Events; }
        }

        #endregion

        #region IIndexerSignatureParent<IGenericParameterIndexerMember<TGenericParameter>,TGenericParameter> Members

        IIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<TGenericParameter>, TGenericParameter> IIndexerSignatureParent<IGenericParameterIndexerMember<TGenericParameter>, TGenericParameter>.Indexers
        {
            get { return this.Indexers; }
        }

        #endregion

        #region IIndexerSignatureParent Members

        IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
        {
            get { return ((IIndexerSignatureMemberDictionary)(this.Indexers)); }
        }

        #endregion

        #region IIntermediatePropertySignatureParentType<IGenericParameterPropertyMember<TGenericParameter>,IIntermediateGenericParameterPropertyMember<TGenericParameter,TIntermediateGenericParameter>,TGenericParameter,TIntermediateGenericParameter> Members

        IIntermediatePropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter> IIntermediatePropertySignatureParentType<IGenericParameterPropertyMember<TGenericParameter>, IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IIntermediatePropertySignatureParentType Members

        IIntermediatePropertySignatureMemberDictionary IIntermediatePropertySignatureParentType.Properties
        {
            get { return (IIntermediatePropertySignatureMemberDictionary)this.Properties; }
        }

        #endregion

        #region IPropertySignatureParentType Members

        IPropertySignatureMemberDictionary IPropertySignatureParentType.Properties
        {
            get { return (IPropertySignatureMemberDictionary)this.Properties; }
        }

        #endregion

        #region IPropertySignatureParentType<IGenericParameterPropertyMember<TGenericParameter>,TGenericParameter> Members

        IPropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter> IPropertySignatureParentType<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        IIntermediateMethodSignatureMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter> IIntermediateMethodSignatureParent<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.Methods
        {
            get
            {
                return this.Methods;
            }
        }

        IIntermediateMethodSignatureMemberDictionary<IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>, IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter> IIntermediateMethodSignatureParent<IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>, IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.Methods
        {
            get
            {
                return this.Methods;
            }
        }

        #region IIntermediateGenericParameter Members

        IIntermediateGenericParameterParent IIntermediateGenericParameter.Parent
        {
            get
            {
                return this.Parent;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/> requires a new
        /// constructor constraint.
        /// </summary>
        public bool RequiresNewConstructor { get; set; }

        /// <summary>
        /// Returns/sets the special constraint placed upon the <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        public GenericTypeParameterSpecialConstraint SpecialConstraint { get; set; }

        IIntermediateGenericParameterConstructorMemberDictionary IIntermediateGenericParameter.Constructors
        {
            get { return this.Constructors; }
        }

        IIntermediateGenericParameterEventMemberDictionary IIntermediateGenericParameter.Events
        {
            get { return this.Events; }
        }

        IIntermediateGenericParameterIndexerMemberDictionary IIntermediateGenericParameter.Indexers
        {
            get { return this.Indexers; }
        }

        IIntermediateGenericParameterMethodMemberDictionary IIntermediateGenericParameter.Methods
        {
            get { return (IIntermediateGenericParameterMethodMemberDictionary)this.Methods; }
        }

        IIntermediateGenericParameterPropertyMemberDictionary IIntermediateGenericParameter.Properties
        {
            get { return (IIntermediateGenericParameterPropertyMemberDictionary)this.Properties; }
        }

        #endregion

        #region IMethodSignatureParent Members

        IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
        {
            get { return (IMethodSignatureMemberDictionary)this.Methods; }
        }

        #endregion

        #region IGenericParameter Members

        bool IGenericParameter.RequiresNewConstructor
        {
            get { return this.RequiresNewConstructor; }
        }

        GenericTypeParameterSpecialConstraint IGenericParameter.SpecialConstraint
        {
            get { return this.SpecialConstraint; }
        }

        public virtual int Position
        {
            get
            {
                return this.PositionInternal;
            }
            set
            {
                if (this.Position == value)
                    return;
                this.Parent.TypeParameters.Rearrange(PositionInternal, value);
            }
        }

        private int PositionInternal
        {
            get
            {
                int index = -1;
                foreach (var item in ((IGenericParameterDictionary<TGenericParameter, TParent>)this.Parent.TypeParameters))
                    if (item.Value == this)
                        return ++index;
                    else
                        index++;
                return index;
            }
        }

        ILockedTypeCollection IGenericParameter.Constraints
        {
            get {
                if (this._constraints == null)
                    this._constraints = new LockedTypeCollection(this.Constraints);
                return this._constraints;
            }
        }

        public ITypeCollection Constraints
        {
            get
            {
                if (this.constraints == null)
                    this.constraints = new TypeCollection();
                return this.constraints;
            }
        }

        IGenericParamParent IGenericParameter.Parent
        {
            get { return this.Parent; }
        }

        IGenericParameterConstructorMemberDictionary IGenericParameter.Constructors
        {
            get { return ((IGenericParameterConstructorMemberDictionary)(this.Constructors)); }
        }

        IGenericParameterEventMemberDictionary IGenericParameter.Events
        {
            get { return ((IGenericParameterEventMemberDictionary)(this.Events)); }
        }

        IGenericParameterIndexerMemberDictionary IGenericParameter.Indexers
        {
            get { return (IGenericParameterIndexerMemberDictionary)this.Indexers; }
        }

        IGenericParameterMethodMemberDictionary IGenericParameter.Methods
        {
            get { return (IGenericParameterMethodMemberDictionary)this.Methods; }
        }

        IGenericParameterPropertyMemberDictionary IGenericParameter.Properties
        {
            get { return (IGenericParameterPropertyMemberDictionary)this.Properties; }
        }

        #endregion

        #region IGenericParameter<TGenericParameter> Members

        IGenericParameterConstructorMemberDictionary<TGenericParameter> IGenericParameter<TGenericParameter>.Constructors
        {
            get {
                return this.Constructors;
            }
        }

        IGenericParameterEventMemberDictionary<TGenericParameter> IGenericParameter<TGenericParameter>.Events
        {
            get
            {
                return this.Events;
            }
        }

        IGenericParameterIndexerMemberDictionary<TGenericParameter> IGenericParameter<TGenericParameter>.Indexers
        {
            get { throw new NotImplementedException(); }
        }

        IGenericParameterMethodMemberDictionary<TGenericParameter> IGenericParameter<TGenericParameter>.Methods
        {
            get { throw new NotImplementedException(); }
        }

        IGenericParameterPropertyMemberDictionary<TGenericParameter> IGenericParameter<TGenericParameter>.Properties
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IGenericParameter<TGenericParameter,TParent> Members

        TParent IGenericParameter<TGenericParameter, TParent>.Parent
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Class; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            throw new NotImplementedException();
        }

        protected override IIntermediateFullMemberDictionary OnGetIntermediateMembers()
        {
            this.CheckConstructors();
            this.CheckEvents();
            this.CheckIndexers();
            this.CheckMethods();
            this.CheckProperties();
            return this._Members;
        }

        public override bool IsGenericType
        {
            get { return false; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            throw new NotImplementedException();
        }

        protected override IType BaseTypeImpl
        {
            get { throw new NotImplementedException(); }
        }

        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                Check_Members();
                return this._members;
            }
        }

        #region IIntermediateCustomAttributedDeclaration Members

        /// <summary>
        /// Returns the <see cref="ICustomAttributeDefinitionCollectionSeries"/> associated
        /// to the current <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        public new ICustomAttributeDefinitionCollectionSeries CustomAttributes
        {
            get {
                if (this.customAttributes == null)
                    this.customAttributes = new CustomAttributeDefinitionCollectionSeries(this);
                return this.customAttributes;
            }
        }

        #endregion

        protected override void Dispose(bool dispose)
        {
            try
            {
                if (this.customAttributes != null)
                {
                    this.customAttributes.Dispose();
                    this.customAttributes = null;
                }
                if (this.constraints != null)
                {
                    this.constraints.Clear();
                    this.constraints = null;
                }
                if (this._constraints != null)
                {
                    this._constraints.Dispose();
                    this._constraints = null;
                }
                if (this.constructors != null)
                {
                    this.constructors.Dispose();
                    this.constructors = null;
                }
                if (this.events != null)
                {
                    this.events.Dispose();
                    this.events = null;
                }
                if (this.indexers != null)
                {
                    this.indexers.Dispose();
                    this.indexers = null;
                }
                if (this.methods != null)
                {
                    this.methods.Dispose();
                    this.methods = null;
                }
                if (this.properties != null)
                {
                    this.properties.Dispose();
                    this.properties = null;
                }
                if (this._members != null)
                {
                    this._members.Dispose();
                    this._members = null;
                }
            }
            finally
            {
                base.Dispose(dispose);
            }
        }

        #region IMethodSignatureParent<IGenericParameterMethodMember<TGenericParameter>,TGenericParameter> Members

        IMethodSignatureMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter> IMethodSignatureParent<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        #region IIntermediateMethodSignatureParent Members

        IIntermediateMethodSignatureMemberDictionary IIntermediateMethodSignatureParent.Methods
        {
            get { return (IIntermediateMethodSignatureMemberDictionary)this.Methods; }
        }

        #endregion

        #region IIntermediateIndexerSignatureParent<IGenericParameterIndexerMember<TGenericParameter>,IIntermediateGenericParameterIndexerMember<TGenericParameter,TIntermediateGenericParameter>,TGenericParameter,TIntermediateGenericParameter> Members

        IIntermediateIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<TGenericParameter>, IIntermediateGenericParameterIndexerMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter> IIntermediateIndexerSignatureParent<IGenericParameterIndexerMember<TGenericParameter>, IIntermediateGenericParameterIndexerMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.Indexers
        {
            get { return this.Indexers; }
        }

        #endregion

        #region IIntermediateIndexerSignatureParent Members

        IIntermediateIndexerSignatureMemberDictionary IIntermediateIndexerSignatureParent.Indexers
        {
            get { return this.Indexers; }
        }

        #endregion

        public GenericParameterVariance Variance { get; set; }

    }
}