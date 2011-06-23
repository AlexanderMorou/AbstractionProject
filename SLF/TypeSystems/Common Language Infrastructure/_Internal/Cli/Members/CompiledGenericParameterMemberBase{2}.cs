using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CompiledGenericParameterMemberBase<TGenericParameter, TParent> :
        CompiledTypeBase<TGenericParameter>,
        IGenericParameter<TGenericParameter, TParent>,
        ICompiledGenericParameter
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
    {
        /// <summary>
        /// Data member for <see cref="Events"/>.
        /// </summary>
        private IGenericParameterEventMemberDictionary<TGenericParameter> events;
        /// <summary>
        /// Data member for <see cref="Indexers"/>.
        /// </summary>
        private IGenericParameterIndexerMemberDictionary<TGenericParameter> indexers;
        /// <summary>
        /// Data member for <see cref="Methods"/>.
        /// </summary>
        private IGenericParameterMethodMemberDictionary<TGenericParameter> methods;
        /// <summary>
        /// Data member for <see cref="Properties"/>.
        /// </summary>
        private IGenericParameterPropertyMemberDictionary<TGenericParameter> properties;
        /// <summary>
        /// Data member for <see cref="Constraints"/>.
        /// </summary>
        private ILockedTypeCollection constraints;
        private ITypeCollection _constraints;
        /// <summary>
        /// Data member for <see cref="SpecialConstraint"/>.
        /// </summary>
        private GenericTypeParameterSpecialConstraint? specialConstraint;
        bool? newCtorConstraint;
        private TParent parent;
        /// <summary>
        /// Data member for <see cref="Constructors"/>.
        /// </summary>
        private IGenericParameterConstructorMemberDictionary<TGenericParameter> constructors;

        protected CompiledGenericParameterMemberBase(TParent parent, Type underlyingSystemType)
            : base(underlyingSystemType)
        {
            this.parent = parent;
        }

        #region IGenericParameter<TGenericParameter, TParent> Members

        public TParent Parent
        {
            get { return this.parent; }
        }

        /// <summary>
        /// Returns whether the <see cref="IGenericParameter"/> requires a new
        /// constructor constraint.
        /// </summary>
        public bool RequiresNewConstructor
        {
            get
            {
                if (newCtorConstraint == null)
                    newCtorConstraint = ((base.UnderlyingSystemType.GenericParameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) == GenericParameterAttributes.DefaultConstructorConstraint);
                return newCtorConstraint.Value;
            }
        }

        /// <summary>
        /// Returns the special constraint placed upon the <see cref="IGenericParameter"/>.
        /// </summary>
        public GenericTypeParameterSpecialConstraint SpecialConstraint
        {
            get
            {
                if (specialConstraint == null)
                {
                    bool structReq = ((base.UnderlyingSystemType.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) == GenericParameterAttributes.NotNullableValueTypeConstraint);
                    bool classReq = ((base.UnderlyingSystemType.GenericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) == GenericParameterAttributes.ReferenceTypeConstraint);
                    if (structReq)
                        specialConstraint = GenericTypeParameterSpecialConstraint.Struct;
                    else if (classReq)
                        specialConstraint = GenericTypeParameterSpecialConstraint.Class;
                    else
                        specialConstraint = GenericTypeParameterSpecialConstraint.None;
                }
                return specialConstraint.Value;
            }
        }

        #endregion

        #region IDeclaration Members


        /// <summary>
        /// Returns the name of the <see cref="CompiledGenericTypeParameter{TType}"/>.
        /// </summary>
        protected override string OnGetName()
        {
            if (this.ElementClassification == TypeElementClassification.None)
                return this.UnderlyingSystemType.Name;
            else
                return this.ElementType.Name;
        }

        /*
        /// <summary>
        /// Returns the unique identifier for the current <see cref="CompiledGenericTypeParameter{TType}"/> where 
        /// <see cref="TypeBase{TType}.Name"/> is not enough to distinguish between two <see cref="IDeclaration"/> entities.
        /// </summary>
        public string UniqueIdentifier
        {
            get { return this.Name; }
        }
        */
        #endregion

        #region IGenericParameter Members

        public int Position
        {
            get { return this.UnderlyingSystemType.GenericParameterPosition; }
        }


        public ILockedTypeCollection Constraints
        {
            get
            {
                if (this.constraints == null)
                    this.constraints = new LockedTypeCollection(this._Constraints);
                return this.constraints;
            }
        }

        private ITypeCollection _Constraints
        {
            get {
                if (this._constraints == null)
                    this._constraints = ObtainConstraints();
                return this._constraints;
            }
        }



        /// <summary>
        /// Obtains the constraints for the <see cref="CompiledGenericParameterMemberBase{TGenericParameter, TParent}"/>.
        /// </summary>
        /// <returns></returns>
        private ITypeCollection ObtainConstraints()
        {
            /* *
             * To ensure that the constraints provided are accurate, this checks for the Nullable<T>
             * type constraint.  To prevent 'issues' it is removed since it's not a valid
             * constraint in C&#9839; or VB
             * */
            return this.UnderlyingSystemType.GetGenericParameterConstraints().Filter(
                t =>
                {
                    if (t.IsGenericType && !t.IsGenericTypeDefinition &&
                        t.GetGenericTypeDefinition() == typeof(Nullable<>))
                        return false;
                    return true;
                }).OnAll(t => t.GetTypeReference()).ToCollection();
        }

        #endregion

        #region Internal Property Initializer Checks

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
        public override void Dispose()
        {
            if (this._constraints != null)
            {
                this._constraints.Clear();
                this._constraints = null;
            }
            if (this.constraints != null)
            {
                this.constraints.Dispose();
                this.constraints = null;
            }
            if (this.parent != null)
                this.parent = default(TParent);
            base.Dispose();
        }

        #region IGenericParameter<TGenericParameter,TGenericParameterConstructor,TGenericParameterConstructorParameter> Members

        public IGenericParameterConstructorMemberDictionary<TGenericParameter> Constructors
        {
            get
            {
                CheckConstructors();
                return this.constructors;
            }
        }

        #endregion

        #region IGenericParameter Members

        IGenericParamParent IGenericParameter.Parent
        {
            get { return this.Parent; }
        }

        IGenericParameterConstructorMemberDictionary IGenericParameter.Constructors
        {
            get { return (IGenericParameterConstructorMemberDictionary)this.Constructors; }
        }

        #endregion

        private IGenericParameterConstructorMemberDictionary<TGenericParameter> InitializeConstructors()
        {
            if (this.ElementClassification == TypeElementClassification.None || this.ElementClassification == TypeElementClassification.GenericTypeDefinition)
                return new LockedGenericParameterConstructorMembers<TGenericParameter>(this._Members, (TGenericParameter)(object)this, UnderlyingSystemType.ObtainTypeParameterConstrutors(), (ConstructorInfo k) => new CompiledGenericParameterConstructorMemberBase<TGenericParameter>((TGenericParameter)(object)this, k));
            else
                throw new InvalidOperationException("Cannot obtain the constructors of an array, pointer, byref, or nullable genric-type-parameter.");
        }

        private IGenericParameterEventMemberDictionary<TGenericParameter> InitializeEvents()
        {
            throw new NotImplementedException();
        }

        private IGenericParameterIndexerMemberDictionary<TGenericParameter> InitializeIndexers()
        {
            throw new NotImplementedException();
        }

        private IGenericParameterPropertyMemberDictionary<TGenericParameter> InitializeProperties()
        {
            if (this.ElementClassification == TypeElementClassification.None || this.ElementClassification == TypeElementClassification.GenericTypeDefinition)
                return new LockedGenericParameterPropertyMembers<TGenericParameter>(this._Members, (TGenericParameter)(object)this, UnderlyingSystemType.ObtainTypeParameterProperties(), (PropertyInfo k) => new CompiledGenericParameterPropertyMember<TGenericParameter>((TGenericParameter)(object)this, k));
            else
                throw new InvalidOperationException("Cannot obtain the methods of an array, pointer, byref, or nullable genric-type-parameter.");
        }

        private IGenericParameterMethodMemberDictionary<TGenericParameter> InitializeMethods()
        {
            if (this.ElementClassification == TypeElementClassification.None || this.ElementClassification == TypeElementClassification.GenericTypeDefinition)
                return new LockedGenericParameterMethodMembers<TGenericParameter>(this._Members, (TGenericParameter)(object)this, UnderlyingSystemType.ObtainTypeParameterMethods(), (MethodInfo k) => new CompiledGenericParameterMethodMember<TGenericParameter>((TGenericParameter)(object)this, k));
            else
                throw new InvalidOperationException("Cannot obtain the methods of an array, pointer, byref, or nullable genric-type-parameter.");
        }

        protected override IType OnGetDeclaringType()
        {
            if (this.UnderlyingSystemType.DeclaringType != null && 
                this.UnderlyingSystemType.DeclaringMethod != null)
                return null;
            return base.OnGetDeclaringType();
        }

        #region IGenericParameter<TGenericParameter,TGenericParameterConstructor,TGenericParameterConstructorParameter> Members

        public IGenericParameterEventMemberDictionary<TGenericParameter> Events
        {
            get {
                this.CheckEvents();
                return this.events;
            }
        }

        public IGenericParameterIndexerMemberDictionary<TGenericParameter> Indexers
        {
            get
            {
                this.CheckIndexers();
                return this.indexers;
            }
        }

        public IGenericParameterMethodMemberDictionary<TGenericParameter> Methods
        {
            get
            {
                this.CheckMethods();
                return this.methods;
            }
        }

        public IGenericParameterPropertyMemberDictionary<TGenericParameter> Properties
        {
            get
            {
                this.CheckProperties();
                return this.properties;
            }
        }

        #endregion

        #region IMethodSignatureParent Members

        IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
        {
            get { return ((IMethodSignatureMemberDictionary)this.Methods); }
        }

        #endregion

        #region IEventSignatureParent<IGenericParameterEventMember<TGenericParameter,TGenericParameterConstructor,TGenericParameterConstructorParameter>,TGenericParameter> Members

        IEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, TGenericParameter> IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>.Events
        {
            get { return this.Events; ; }
        }

        #endregion

        #region IEventSignatureParent<IGenericParameterEventMember<TGenericParameter,TGenericParameterConstructor,TGenericParameterConstructorParameter>,IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter,TGenericParameterConstructor,TGenericParameterConstructorParameter>,TGenericParameter>,TGenericParameter> Members

        IEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>, TGenericParameter> IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>, TGenericParameter>.Events
        {
            get { return this.Events; ; }
        }

        #endregion

        #region IEventSignatureParent Members

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { return ((IEventSignatureMemberDictionary)(this.Events)); }
        }

        #endregion

        #region IIndexerSignatureParent<IGenericParameterIndexerMember<TGenericParameter,TGenericParameterConstructor,TGenericParameterConstructorParameter>,TGenericParameter> Members

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

        #region IPropertySignatureParentType<IGenericParameterPropertyMember<TGenericParameter,TGenericParameterConstructor,TGenericParameterConstructorParameter>,TGenericParameter> Members

        IPropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter> IPropertySignatureParentType<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IPropertySignatureParentType Members

        IPropertySignatureMemberDictionary IPropertySignatureParentType.Properties
        {
            get { return ((IPropertySignatureMemberDictionary)(this.Properties)); }
        }

        #endregion

        #region IGenericParameter Members

        public GenericParameterVariance Variance
        {
            get
            {
                var variance = this.UnderlyingSystemType.GenericParameterAttributes & GenericParameterAttributes.VarianceMask;
                switch (variance)
                {
                    case GenericParameterAttributes.Contravariant:
                        return GenericParameterVariance.Contravariant;
                    case GenericParameterAttributes.Covariant:
                        return GenericParameterVariance.Covariant;
                    default:
                        return GenericParameterVariance.Invariant;
                }
            }
        }

        IGenericParameterEventMemberDictionary IGenericParameter.Events
        {
            get { return ((IGenericParameterEventMemberDictionary)(this.Events)); }
        }

        IGenericParameterIndexerMemberDictionary IGenericParameter.Indexers
        {
            get { return ((IGenericParameterIndexerMemberDictionary)(this.Indexers)); }
        }

        IGenericParameterMethodMemberDictionary IGenericParameter.Methods
        {
            get { return ((IGenericParameterMethodMemberDictionary)(this.Methods)); }
        }

        IGenericParameterPropertyMemberDictionary IGenericParameter.Properties
        {
            get { return ((IGenericParameterPropertyMemberDictionary)(this.Properties)); }
        }

        #endregion


        ////

        #region ICreatableType<IGenericParameterConstructorMember<TGenericParameter>,TGenericParameter> Members

        IConstructorMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter> ICreatableType<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>.Constructors
        {
            get { return this.Constructors; }
        }

        public IGenericParameterConstructorMember<TGenericParameter> TypeInitializer
        {
            get { return null; }
        }

        #endregion

        #region ICreatableType Members

        IConstructorMemberDictionary ICreatableType.Constructors
        {
            get { return ((IConstructorMemberDictionary)(this.Constructors)); }
        }

        IConstructorMember ICreatableType.TypeInitializer
        {
            get { return this.TypeInitializer; }
        }

        #endregion

        #region IMethodSignatureParent<IGenericParameterMethodMember<TGenericParameter>,TGenericParameter> Members

        IMethodSignatureMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter> IMethodSignatureParent<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        public override IEnumerable<string> AggregateIdentifiers
        {
            get { throw new NotImplementedException(); }
        }

    }
}
