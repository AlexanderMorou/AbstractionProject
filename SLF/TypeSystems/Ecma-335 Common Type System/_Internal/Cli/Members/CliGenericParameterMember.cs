using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CliGenericParameterMember<TGenericParameter, TParent> :
        TypeBase<IGenericParameterUniqueIdentifier, TGenericParameter>,
        IGenericParameter<TGenericParameter, TParent>,
        ICliGenericParameter
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
    {
        private ICliMetadataGenericParameterTableRow metadataEntry;
        private TParent parent;
        private int index;

        public event EventHandler Disposed;
        private IGenericParameterConstructorMemberDictionary<TGenericParameter> constructors;
        private IGenericParameterEventMemberDictionary<TGenericParameter> events;
        private IGenericParameterIndexerMemberDictionary<TGenericParameter> indexers;
        private IGenericParameterMethodMemberDictionary<TGenericParameter> methods;
        private IGenericParameterPropertyMemberDictionary<TGenericParameter> properties;
        private static readonly ICliMetadataMethodDefinitionTableRow DefaultCtorEntry = CreateDefaultCtorEntry();
        private ILockedTypeCollection constraints;

        protected CliGenericParameterMember(TParent parent, ICliMetadataGenericParameterTableRow metadataEntry, int position)
        {
            this.parent = parent;
            this.index = position;
            this.metadataEntry = metadataEntry;
        }

        private static ICliMetadataMethodDefinitionTableRow CreateDefaultCtorEntry()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Data member for <see cref="Metadata"/>.
        /// </summary>
        private IMetadataCollection metadata;
        /// <summary>
        /// Data member for <see cref="_Members"/>.
        /// </summary>
        private IFullMemberDictionary _members;

        public TParent Parent
        {
            get { return this.parent; }
        }

        public IGenericParameterConstructorMemberDictionary<TGenericParameter> Constructors
        {
            get
            {
                this.CheckConstructors();
                return this.constructors; ;
            }
        }

        public IGenericParameterEventMemberDictionary<TGenericParameter> Events
        {
            get
            {
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

        IConstructorMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter> ICreatableParent<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>.Constructors
        {
            get { return this.Constructors; }
        }

        public IGenericParameterConstructorMember<TGenericParameter> TypeInitializer
        {
            get
            {
                return null;
            }
        }

        IConstructorMemberDictionary ICreatableParent.Constructors
        {
            get { return (IConstructorMemberDictionary)this.Constructors; }
        }

        IConstructorMember ICreatableParent.TypeInitializer
        {
            get { return this.TypeInitializer; }
        }

        public IType MakeNullable()
        {
            if (this.SpecialConstraint != GenericTypeParameterSpecialConstraint.Struct)
                throw new InvalidOperationException();
            return this.IdentityManager.MakeClassificationType(this, TypeElementClassification.Nullable);
        }

        IMethodSignatureMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter> IMethodSignatureParent<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>.Methods
        {
            get { return this.Methods; }
        }

        IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
        {
            get { return (IMethodSignatureMemberDictionary)this.Methods; }
        }

        IEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, TGenericParameter> IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>.Events
        {
            get { return this.Events; }
        }

        IEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>, TGenericParameter> IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>, TGenericParameter>.Events
        {
            get { return this.Events; }
        }

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { return (IEventSignatureMemberDictionary)this.Events; }
        }

        IIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<TGenericParameter>, TGenericParameter> IIndexerSignatureParent<IGenericParameterIndexerMember<TGenericParameter>, TGenericParameter>.Indexers
        {
            get { return this.Indexers; }
        }

        IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
        {
            get { return (IIndexerSignatureMemberDictionary)this.Indexers; }
        }

        IPropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter> IPropertySignatureParent<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter>.Properties
        {
            get { return this.Properties; }
        }

        IPropertySignatureMemberDictionary IPropertySignatureParent.Properties
        {
            get { return (IPropertySignatureMemberDictionary)this.Properties; }
        }

        TGenericParameter IType<IGenericParameterUniqueIdentifier, TGenericParameter>.ElementType
        {
            get { throw new NotImplementedException(); }
        }

        public GenericParameterVariance Variance
        {
            get
            {
                var varianceFlags = this.MetadataEntry.Flags & GenericParameterAttributes.VarianceMask;
                switch (varianceFlags)
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

        public GenericTypeParameterSpecialConstraint SpecialConstraint
        {
            get
            {
                var specialFlags = this.MetadataEntry.Flags & (GenericParameterAttributes.SpecialConstraintMask ^ GenericParameterAttributes.DefaultConstructorConstraint);
                switch (specialFlags)
                {
                    case GenericParameterAttributes.NotNullableValueTypeConstraint:
                        return GenericTypeParameterSpecialConstraint.Struct;
                    case GenericParameterAttributes.ReferenceTypeConstraint:
                        return GenericTypeParameterSpecialConstraint.Class;
                }
                return GenericTypeParameterSpecialConstraint.None;
            }
        }

        public int Position
        {
            get { return this.index; }
        }

        public ILockedTypeCollection Constraints
        {
            get
            {
                return this.constraints ?? (this.constraints = (from c in this.metadataEntry.Constraints
                                                                select this.IdentityManager.ObtainTypeReference(c.Constraint, (this.Parent as IType), null)).ToLockedCollection());
            }
        }

        IGenericParamParent IGenericParameter.Parent
        {
            get { return this.Parent; }
        }

        IGenericParameterConstructorMemberDictionary IGenericParameter.Constructors
        {
            get { return (IGenericParameterConstructorMemberDictionary)this.Constructors; }
        }

        IGenericParameterEventMemberDictionary IGenericParameter.Events
        {
            get { return (IGenericParameterEventMemberDictionary)this.Events; }
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

        private IGenericParameterConstructorMemberDictionary<TGenericParameter> InitializeConstructors()
        {
            throw new NotImplementedException();
        }

        private IGenericParameterEventMemberDictionary<TGenericParameter> InitializeEvents()
        {
            throw new NotImplementedException();
        }

        private IGenericParameterIndexerMemberDictionary<TGenericParameter> InitializeIndexers()
        {
            throw new NotImplementedException();
        }

        private IGenericParameterMethodMemberDictionary<TGenericParameter> InitializeMethods()
        {
            throw new NotImplementedException();
        }

        private IGenericParameterPropertyMemberDictionary<TGenericParameter> InitializeProperties()
        {
            throw new NotImplementedException();
        }

        public ICliMetadataGenericParameterTableRow MetadataEntry
        {
            get { return this.metadataEntry; }
        }

        protected override bool Equals(TGenericParameter other)
        {
            return object.ReferenceEquals(other, this);
        }

        protected override ITypeParent OnGetParent()
        {
            if (this.parent is ITypeParent)
                return (ITypeParent)this.parent;
            return null;
        }

        protected override TypeKind TypeImpl
        {
            get
            {
                if (this.SpecialConstraint == GenericTypeParameterSpecialConstraint.Struct)
                    return TypeKind.Struct;
                return TypeKind.Class;
            }
        }

        protected override bool CanCacheImplementsList
        {
            get
            {
                return true;
            }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return this.Constraints.Where(ConstraintIsInterface).ToLockedCollection();
        }

        private static bool ConstraintIsInterface(IType constraint)
        {
            if (constraint == null)
                return false;
            if (constraint is IClassType)
            {
                return false;
            }
            return true;
        }
        private static bool ConstraintIsClass(IType constraint)
        {
            if (constraint == null ||
                constraint.IdentityManager == null ||
                constraint.Equals(constraint.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootStruct)))
                return false;
            if (constraint is IClassType)
            {
                return true;
            }
            return false;
        }

        protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
        {
            return this.ImplementedInterfaces;
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            this.Check_Members();
            this.CheckConstructors();
            this.CheckEvents();
            this.CheckIndexers();
            this.CheckMethods();
            this.CheckProperties();
            return this._Members;
        }

        private void Check_Members()
        {
            throw new NotImplementedException();
        }

        protected override INamespaceDeclaration OnGetNamespace()
        {
            return null;
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return AccessLevelModifiers.Public;
        }

        public override bool IsGenericConstruct
        {
            get { return false; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            var classRequirement = this.Constraints.FirstOrDefault(ConstraintIsClass);
            if (classRequirement != null)
                for (IType current = classRequirement; current != null; current = current.BaseType)
                    if (current.Equals(other))
                        return true;
            return other.Equals(this.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType));
        }

        protected override string OnGetNamespaceName()
        {
            return null;
        }

        protected override IType BaseTypeImpl
        {
            get
            {
                var classRequirement = this.Constraints.FirstOrDefault(ConstraintIsClass);
                return classRequirement ?? this.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType);
            }
        }

        protected override IMetadataCollection InitializeCustomAttributes()
        {
            return new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, this.IdentityManager);
        }

        public new _ICliManager IdentityManager { get { return (_ICliManager)base.IdentityManager; } }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get
            {
                return (from m in this.Members.Values
                        select (IGeneralDeclarationUniqueIdentifier)m.Entry.UniqueIdentifier);
            }
        }

        protected override string OnGetName()
        {
            return this.MetadataEntry.Name;
        }

        public IFullMemberDictionary _Members
        {
            get
            {
                if (this._members == null)
                    this._members = this.Initialize_Members();
                return this._members;
            }
        }

        private IFullMemberDictionary Initialize_Members()
        {
            throw new NotSupportedException();
            //return new CliFullMemberDictionary();
        }
    }
}
