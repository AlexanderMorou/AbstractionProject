using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
/*---------------------------------------------------------------------\
| Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
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
        IntermediateTypeBase<IGenericParameterUniqueIdentifier, TGenericParameter, TIntermediateGenericParameter>,
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
        private IMetadataDefinitionCollection metadata;
        private IntermediateFullMemberDictionary _members;
        private ConstructorMemberDictionary constructors;
        private EventMemberDictionary events;
        private IndexerMemberDictionary indexers;
        private MethodMemberDictionary methods;
        private PropertyMemberDictionary properties;
        private ITypeIdentityManager identityManager;

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name"><see cref="System.String"/> which represents
        /// the unique identifier of the <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/>
        /// which contains the <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.</param>
        /// <param name="identityManager">The <see cref="ITypeIdentityManager"/> which aids in type identity resolution.</param>
        protected IntermediateGenericParameterBase(string name, TIntermediateParent parent, ITypeIdentityManager identityManager)
        {
            base.AssignName(name);
            this.identityManager = identityManager;
            if (parent is IIntermediateTypeParent)
                base.Parent = (IIntermediateTypeParent)parent;
            this.Parent = parent;
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> to which
        /// the current 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
        /// belongs.
        /// </summary>
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

        /// <summary>
        /// Returns whether the current 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
        /// is nullable.
        /// </summary>
        /// <returns>true if the <see cref="SpecialConstraint"/> is
        /// <see cref="GenericTypeParameterSpecialConstraint.Struct"/>; 
        /// false, otherwise.</returns>
        protected override bool OnGetIsNullable()
        {
            return (this.SpecialConstraint == GenericTypeParameterSpecialConstraint.Struct);
        }

        #region Check Methods

        private void Check_Members()
        {
            lock (this.SyncObject)
                if (this._members == null)
                    this._members = new IntermediateFullMemberDictionary();
        }

        private void CheckConstructors()
        {
            lock (this.SyncObject)
                if (this.constructors == null)
                    this.constructors = this.InitializeConstructors();
        }

        private void CheckEvents()
        {
            lock (this.SyncObject)
                if (this.events == null)
                    this.events = this.InitializeEvents();
        }

        private void CheckIndexers()
        {
            lock (this.SyncObject)
                if (this.indexers == null)
                    this.indexers = this.InitializeIndexers();
        }

        private void CheckMethods()
        {
            lock (this.SyncObject)
                if (this.methods == null)
                    this.methods = this.InitializeMethods();
        }

        private void CheckProperties()
        {
            lock (this.SyncObject)
                if (this.properties == null)
                    this.properties = this.InitializeProperties();
        }
        #endregion

        #region IIntermediateGenericParameter<TGenericParameter,TIntermediateGenericParameter,TParent,TIntermediateParent> Members

        public new TIntermediateParent Parent { get; internal set; }

        #endregion

        /// <summary>
        /// Returns the <see cref="ConstructorMemberDictionary"/>
        /// associated to the current
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        public ConstructorMemberDictionary Constructors
        {
            get
            {
                lock (this.SyncObject)
                {
                    this.CheckConstructors();
                    return this.constructors;
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="IndexerMemberDictionary"/>
        /// associated to the current
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        public IndexerMemberDictionary Indexers
        {
            get
            {
                lock (this.SyncObject)
                {
                    this.CheckIndexers();
                    return this.indexers;
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="EventMemberDictionary"/>
        /// associated to the current
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        public EventMemberDictionary Events
        {
            get
            {
                lock (this.SyncObject)
                {
                    this.CheckEvents();
                    return this.events;
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="MethodMemberDictionary"/>
        /// associated to the current
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        public MethodMemberDictionary Methods
        {
            get
            {
                lock (this.SyncObject)
                {
                    this.CheckMethods();
                    return this.methods;
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="PropertyMemberDictionary"/>
        /// associated to the current
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        public PropertyMemberDictionary Properties
        {
            get
            {
                lock (this.SyncObject)
                {
                    this.CheckProperties();
                    return this.properties;
                }
            }
        }

        /// <summary>
        /// Initializes the <see cref="Constructors"/> property of the 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
        /// </summary>
        /// <returns>A <see cref="ConstructorMemberDictionary"/>
        /// which contains the constructors of the 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/></returns>
        protected virtual ConstructorMemberDictionary InitializeConstructors()
        {
            return new ConstructorMemberDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        /// <summary>
        /// Initializes the <see cref="Events"/> property of the 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
        /// </summary>
        /// <returns>A <see cref="EventMemberDictionary"/>
        /// which contains the events of the 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/></returns>
        protected virtual EventMemberDictionary InitializeEvents()
        {
            return new EventMemberDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        /// <summary>
        /// Initializes the <see cref="Indexers"/> property of the 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
        /// </summary>
        /// <returns>A <see cref="IndexerMemberDictionary"/>
        /// which contains the indexers of the 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/></returns>
        protected virtual IndexerMemberDictionary InitializeIndexers()
        {
            return new IndexerMemberDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        /// <summary>
        /// Initializes the <see cref="Methods"/> property of the 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
        /// </summary>
        /// <returns>A <see cref="MethodMemberDictionary"/>
        /// which contains the methods of the 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/></returns>
        protected virtual MethodMemberDictionary InitializeMethods()
        {
            return new MethodMemberDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        /// <summary>
        /// Initializes the <see cref="Properties"/> property of the 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
        /// </summary>
        /// <returns>A <see cref="PropertyMemberDictionary"/>
        /// which contains the properties of the 
        /// <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/></returns>
        protected virtual PropertyMemberDictionary InitializeProperties()
        {
            return new PropertyMemberDictionary(this._Members, ((TIntermediateGenericParameter)((object)(this))));
        }

        #region IIntermediateGenericParameter<TGenericParameter,TIntermediateGenericParameter> Members

        IIntermediateGenericParameterConstructorMemberDictionary<TGenericParameter, TIntermediateGenericParameter> IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>.Constructors
        {
            get
            {
                return this.Constructors;
            }
        }

        IIntermediateGenericParameterEventMemberDictionary<TGenericParameter, TIntermediateGenericParameter> IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>.Events
        {
            get
            {
                return this.Events;
            }
        }

        IIntermediateGenericParameterIndexerMemberDictionary<TGenericParameter, TIntermediateGenericParameter> IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>.Indexers
        {
            get { return this.Indexers; }
        }

        IIntermediateGenericParameterMethodMemberDictionary<TGenericParameter, TIntermediateGenericParameter> IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>.Methods
        {
            get { return this.Methods; }
        }

        IIntermediateGenericParameterPropertyMemberDictionary<TGenericParameter, TIntermediateGenericParameter> IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IIntermediateCreatableSignatureParent<IGenericParameterConstructorMember<TGenericParameter>,IIntermediateGenericParameterConstructorMember<TGenericParameter,TIntermediateGenericParameter>,TGenericParameter,TIntermediateGenericParameter> Members

        IIntermediateConstructorSignatureMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter> IIntermediateCreatableSignatureParent<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.Constructors
        {
            get { return this.Constructors; }
        }
        #endregion

        #region IIntermediateCreatableSignatureParent Members

        IIntermediateConstructorSignatureMemberDictionary IIntermediateCreatableSignatureParent.Constructors
        {
            get { return this.Constructors; }
        }

        #endregion

        #region ICreatableParent Members

        IConstructorMemberDictionary ICreatableParent.Constructors
        {
            get { return this.Constructors; }
        }

        IConstructorMember ICreatableParent.TypeInitializer
        {
            get { return null; }
        }

        #endregion

        #region ICreatableParent<IGenericParameterConstructorMember<TGenericParameter>,TGenericParameter> Members

        IConstructorMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter> ICreatableParent<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>.Constructors
        {
            get { return this.Constructors; }
        }

        IGenericParameterConstructorMember<TGenericParameter> ICreatableParent<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>.TypeInitializer
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
            get { return this.Events; }
        }

        #endregion

        #region IIntermediateEventSignatureParent Members

        IIntermediateEventSignatureMemberDictionary IIntermediateEventSignatureParent.Events
        {
            get { return this.Events; }
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
            get { return this.Indexers; }
        }

        #endregion

        #region IIntermediatePropertySignatureParent<IGenericParameterPropertyMember<TGenericParameter>,IIntermediateGenericParameterPropertyMember<TGenericParameter,TIntermediateGenericParameter>,TGenericParameter,TIntermediateGenericParameter> Members

        IIntermediatePropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter> IIntermediatePropertySignatureParent<IGenericParameterPropertyMember<TGenericParameter>, IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IIntermediatePropertySignatureParent Members

        IIntermediatePropertySignatureMemberDictionary IIntermediatePropertySignatureParent.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IPropertySignatureParent Members

        IPropertySignatureMemberDictionary IPropertySignatureParent.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IPropertySignatureParent<IGenericParameterPropertyMember<TGenericParameter>,TGenericParameter> Members

        IPropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter> IPropertySignatureParent<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter>.Properties
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
            get { return this.Methods; }
        }

        IIntermediateGenericParameterPropertyMemberDictionary IIntermediateGenericParameter.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IMethodSignatureParent Members

        IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        #region IGenericParameter Members

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
                foreach (var item in this.Parent.TypeParameters.Values)
                    if (item == this)
                        return ++index;
                    else
                        index++;
                return index;
            }
        }

        ILockedTypeCollection IGenericParameter.Constraints
        {
            get
            {
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
            get { return this.Constructors; }
        }

        IGenericParameterEventMemberDictionary IGenericParameter.Events
        {
            get { return this.Events; }
        }

        IGenericParameterIndexerMemberDictionary IGenericParameter.Indexers
        {
            get { return this.Indexers; }
        }

        IGenericParameterMethodMemberDictionary IGenericParameter.Methods
        {
            get { return this.Methods; }
        }

        IGenericParameterPropertyMemberDictionary IGenericParameter.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IGenericParameter<TGenericParameter> Members

        IGenericParameterConstructorMemberDictionary<TGenericParameter> IGenericParameter<TGenericParameter>.Constructors
        {
            get
            {
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

        protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
        {
            return this.ImplementedInterfaces;
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
            return other.Equals(this.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType));
        }

        protected override IType BaseTypeImpl
        {
            get { return this.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType); }
        }

        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                lock (this.SyncObject)
                {
                    Check_Members();
                    return this._members;
                }
            }
        }

        #region IIntermediateMetadataEntity Members

        /// <summary>
        /// Returns the <see cref="IMetadataDefinitionCollection"/> associated
        /// to the current <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        public new IMetadataDefinitionCollection Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadata == null)
                        this.metadata = new MetadataDefinitionCollection(this, this.IdentityManager);
                    return this.metadata;
                }
            }
        }

        #endregion

        public override void Dispose()
        {
            try
            {
                lock (this.SyncObject)
                {
                    if (this.metadata != null)
                    {
                        this.metadata.Dispose();
                        this.metadata = null;
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

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get
            {
                return this._Members.Keys;
            }
        }

        public override void Visit(IIntermediateTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return for the visitor.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <param name="visitor">The <see cref="IIntermediateTypeVisitor"/> to
        /// receive the <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/> as a visitor.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        public override TResult Visit<TResult, TContext>(IIntermediateTypeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        protected override ITypeIdentityManager OnGetManager()
        {
            lock (SyncObject)
                return this.identityManager;
        }

    }
}
