using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private ITypeCollection constraints;
        private ILockedTypeCollection _constraints;
        private ICustomAttributeDefinitionCollectionSeries customAttributes;
        private IntermediateFullMemberDictionary _members;
        private ConstructorMemberDictionary constructors;
        private EventMemberDictionary events;
        private IndexerMemberDictionary indexers;
        private MethodMemberDictionary methods;
        private PropertyMemberDictionary properties;

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name"><see cref="System.String"/> which represents
        /// the unique identifier of the <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/>
        /// which contains the <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.</param>
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

        protected override bool OnGetIsNullable()
        {
            return (this.SpecialConstraint & GenericTypeParameterSpecialConstraint.Struct) != GenericTypeParameterSpecialConstraint.None;
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

        public ConstructorMemberDictionary Constructors
        {
            get
            {
                this.CheckConstructors();
                return this.constructors;
            }
        }

        public IndexerMemberDictionary Indexers
        {
            get
            {
                this.CheckIndexers();
                return this.indexers;
            }
        }

        public EventMemberDictionary Events
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

        protected virtual ConstructorMemberDictionary InitializeConstructors()
        {
            return new ConstructorMemberDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        protected virtual EventMemberDictionary InitializeEvents()
        {
            return new EventMemberDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        private IndexerMemberDictionary InitializeIndexers()
        {
            return new IndexerMemberDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        private MethodMemberDictionary InitializeMethods()
        {
            return new MethodMemberDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        private PropertyMemberDictionary InitializeProperties()
        {
            return new PropertyMemberDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
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
            get { return this.Indexers; }
        }

        IGenericParameterMethodMemberDictionary<TGenericParameter> IGenericParameter<TGenericParameter>.Methods
        {
            get { return this.Methods; }
        }

        IGenericParameterPropertyMemberDictionary<TGenericParameter> IGenericParameter<TGenericParameter>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IGenericParameter<TGenericParameter,TParent> Members

        TParent IGenericParameter<TGenericParameter, TParent>.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Class; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return ((IGenericParameter)(this)).Constraints;
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

        public override bool IsGenericConstruct
        {
            get { return false; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return other.Equals(CommonTypeRefs.Object);
        }

        protected override IType BaseTypeImpl
        {
            get { return CommonTypeRefs.Object; }
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

        public override void Dispose()
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
                base.Dispose();
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

        public override string UniqueIdentifier
        {
            get
            {
                return this.Name;
            }
        }

        public override IEnumerable<string> AggregateIdentifiers
        {
            get {
                return (from member in this.Members.Values
                        select member.Entry.Name).Distinct();
            }
        }

        public override void Visit(IIntermediateTypeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
