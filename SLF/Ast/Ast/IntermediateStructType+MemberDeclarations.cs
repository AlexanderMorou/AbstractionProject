using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class IntermediateStructCtorMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.ConstructorMember,
        IIntermediateStructCtorMember
        where TInstanceIntermediateType :
            IntermediateStructType<TInstanceIntermediateType>
    {
        public IntermediateStructCtorMember(TInstanceIntermediateType parent, bool typeInitializer = false)
            : base(parent, typeInitializer)
        {

        }

        public override IIntermediateAssembly Assembly
        {
            get { return Parent.Assembly; }
        }
    }

    public class IntermediateStructEventMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.EventMember,
        IIntermediateStructEventMember
        where TInstanceIntermediateType :
            IntermediateStructType<TInstanceIntermediateType>
    {
        public IntermediateStructEventMember(TInstanceIntermediateType parent)
            : base(parent)
        {
        }

        public class EventMethodMember :
            IntermediateStructMethodMember<TInstanceIntermediateType>,
            IIntermediateEventMethodMember
        {
            private EventMethodType methodType;
            private IntermediateStructEventMember<TInstanceIntermediateType> parent;
            public EventMethodMember(IntermediateStructEventMember<TInstanceIntermediateType> parent, EventMethodType methodType)
                : base((TInstanceIntermediateType)parent.Parent)
            {
                this.parent = parent;
                this.methodType = methodType;
            }

            #region IIntermediateEventMethodMember Members

            public void GenerationTypeChanged(IntermediateEventManagementType newManagementType)
            {
                /* *
                 * ToDo: Place code here to restructure internal code base of the
                 * method, in automatic mode, utilize a standard add/remove 
                 * combinatory/reduction logic to the MultiCastDelegate.
                 * *
                 * This is to allow for those interested to view the code
                 * necessary to handle the automatic generation.
                 * *
                 * Should ensure analysis tools (if any will exist)
                 * will be able to analyze the code, without special
                 * hacks to handle things.
                 * */
                throw new NotImplementedException();
            }

            public EventMethodType MethodType
            {
                get { return this.methodType; }
            }

            #endregion

            protected override IType OnGetReturnType()
            {
                if (this.parent == null)
                    return base.ReturnType;
                else
                    return this.parent.ReturnType;
            }

            protected override void OnSetReturnType(IType value)
            {
                if (this.parent == null)
                    base.OnSetReturnType(value);
                else
                    this.parent.ReturnType = value;
            }

        }

        protected override IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember GetMethodMember(EventMethodType type)
        {
            return new EventMethodMember(this, type);
        }
    }
    public class IntermediateStructIndexerMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.IndexerMember,
        IIntermediateStructIndexerMember
        where TInstanceIntermediateType :
            IntermediateStructType<TInstanceIntermediateType>
    {
        public IntermediateStructIndexerMember(TInstanceIntermediateType parent)
            : base(parent)
        {
        }

        public IntermediateStructIndexerMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
        }

        private class IndexerValueParameter :
            IndexerMethodMember.ParameterMember
        {
            private IntermediateStructIndexerMember<TInstanceIntermediateType> owner;
            internal IndexerValueParameter(IntermediateStructIndexerMember<TInstanceIntermediateType> owner, IndexerMethodMember parent)
                : base(parent)
            {
                this.owner = owner;
            }

            protected override string OnGetName()
            {
                return "value";
            }

            protected override void OnSetName(string name)
            {
                throw new InvalidOperationException("Cannot set the name of the value parameter of the set method of an indexer.");
            }

            public override IType ParameterType
            {
                get
                {
                    return this.owner.PropertyType;
                }
                set
                {
                    throw new InvalidOperationException("Cannot set the parameter type of the value parameter of the set method of an indexer, set the indexer's property type.");
                }
            }

            public override ParameterCoercionDirection Direction
            {
                get
                {
                    return ParameterCoercionDirection.In;
                }
                set
                {
                    throw new InvalidOperationException("Cannot change the direction of the value parameter of the set method of an indexer.");
                }
            }
        }

        private class IndexerDependentParameter :
            IndexerMethodMember.ParameterMember
        {
            private ParameterMembersDictionary.ParameterMember original;
            internal IndexerDependentParameter(ParameterMembersDictionary.ParameterMember original, IndexerMethodMember parent)
                : base(parent)
            {
                this.original = original;
            }

            public override IType ParameterType
            {
                get
                {
                    return this.original.ParameterType;
                }
                set
                {
                    throw new InvalidOperationException("Cannot set the type of a parameter of a method of an indexer, set the indexer's parameter type.");
                }
            }
            protected override string OnGetName()
            {
                return this.original.Name;
            }
            protected override void OnSetName(string name)
            {
                throw new NotSupportedException("Cannot set the name of a parameter of a method of an indexer, set the indexer's parameter name. Rationale: the parameters of the method mirror the types of the indexer; however, there's no guarantee, from a framework perspective, that the parameters even require names.");
            }
            public override ParameterCoercionDirection Direction
            {
                get
                {
                    return this.original.Direction;
                }
                set
                {
                    throw new InvalidOperationException("Cannot set the direction of a parameter of a method of an indexer, set the indexer's parameter direction.");
                }
            }

            protected override MetadataDefinitionCollection InitializeCustomAttributes()
            {
                return new MetadataDefinitionCollection(this.original, this.assembly);
            }

            public override IGeneralMemberUniqueIdentifier UniqueIdentifier
            {
                get
                {
                    return this.original.UniqueIdentifier;
                }
            }

            protected override void Dispose(bool disposing)
            {
                this.original.Dispose();
                base.Dispose(disposing);
            }
        }
        protected override void OnParameterAdded(EventArgsR1<IIntermediateIndexerParameterMember<IStructIndexerMember, IIntermediateStructIndexerMember, IStructType, IIntermediateStructType>> e)
        {
            if (this.CanRead && this.IsGetMethodInitialized)
            {
                var gm = this.GetMethod;
                if (gm._AreParametersInitialized)
                {
                    var gmParams = gm.Parameters;
                    gmParams._Add(e.Arg1.UniqueIdentifier, new IndexerDependentParameter((ParameterMembersDictionary.ParameterMember)e.Arg1, gm));
                    gm._OnIdentifierChanged(gm.UniqueIdentifier, DeclarationChangeCause.Signature);
                }
            }
            if (this.CanWrite && this.IsSetMethodInitialized)
            {
                var sm = this.SetMethod;
                if (sm._AreParametersInitialized)
                {
                    var smParams = sm.Parameters;
                    var valueParam = (IndexerValueParameter)((IIntermediatePropertySetMethodMember)sm).ValueParameter;
                    smParams._Remove(valueParam.UniqueIdentifier);
                    smParams._Add(e.Arg1.UniqueIdentifier, new IndexerDependentParameter((ParameterMembersDictionary.ParameterMember)e.Arg1, sm));
                    smParams._Add(valueParam.UniqueIdentifier, valueParam);
                    sm._OnIdentifierChanged(sm.UniqueIdentifier, DeclarationChangeCause.Signature);
                }
            }
            base.OnParameterAdded(e);
        }

        public class IndexerSetMethodMember :
            IndexerMethodMember,
            IIntermediatePropertySetMethodMember
        {
            private IndexerValueParameter valueParameter;

            public IndexerSetMethodMember(IntermediateStructIndexerMember<TInstanceIntermediateType> owner)
                : base(PropertyMethodType.SetMethod, owner)
            {

            }

            protected override IntermediateParameterMemberDictionary<IStructMethodMember, IIntermediateStructMethodMember, IMethodParameterMember<IStructMethodMember, IStructType>, IIntermediateMethodParameterMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>> InitializeParameters()
            {
                var result = base.InitializeParameters();
                this.valueParameter = new IndexerValueParameter(this.Owner, this);
                result._Add(this.valueParameter.UniqueIdentifier, this.valueParameter);
                return result;
            }

            #region IIntermediatePropertySetMethodMember Members

            public IIntermediateMethodParameterMember ValueParameter
            {
                get
                {
                    if (this.valueParameter == null)
                        return this.Parameters["value"];
                    return valueParameter;
                }
            }

            #endregion

            #region IIntermediatePropertySignatureSetMethodMember Members

            IIntermediateSignatureParameterMember IIntermediatePropertySignatureSetMethodMember.ValueParameter
            {
                get { return this.ValueParameter; }
            }

            #endregion
        }

        public class IndexerMethodMember :
            IntermediateStructMethodMember<TInstanceIntermediateType>,
            IIntermediatePropertyMethodMember
        {
            private PropertyMethodType methodType;
            private IntermediateStructIndexerMember<TInstanceIntermediateType> owner;
            public IndexerMethodMember(PropertyMethodType methodType, IntermediateStructIndexerMember<TInstanceIntermediateType> owner)
                : base(owner == null ? null : (TInstanceIntermediateType)owner.Parent)
            {
                if (owner == null)
                    throw new ArgumentNullException("parent");
                this.owner = owner;
                this.methodType = methodType;
            }

            internal protected new bool _AreParametersInitialized { get { return base.AreParametersInitialized; } }

            protected override bool AreParametersInitialized
            {
                get
                {
                    return true;
                }
            }

            protected IntermediateStructIndexerMember<TInstanceIntermediateType> Owner
            {
                get
                {
                    return this.owner;
                }
            }

            protected override string OnGetName()
            {
                switch (this.methodType)
                {
                    case PropertyMethodType.GetMethod:
                        return string.Format("get_{0}", this.owner.Name);
                    case PropertyMethodType.SetMethod:
                        return string.Format("set_{0}", this.owner.Name);
                    default:
                        throw new InvalidOperationException();
                }
            }

            protected override IType OnGetReturnType()
            {
                switch (this.MethodType)
                {
                    case PropertyMethodType.GetMethod:
                        return this.Owner.PropertyType;
                    case PropertyMethodType.SetMethod:
                        return this.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
                    default:
                        return base.OnGetReturnType();
                }
            }

            IParameterMemberDictionary IIntermediatePropertySignatureMethodMember.Parameters
            {
                get { return (IParameterMemberDictionary)this.Parameters; }
            }

            #region IPropertySignatureMethodMember Members
            /// <summary>
            /// Returns the <see cref="PropertyMethodType"/> which 
            /// denotes which method of the property the <see cref="IndexerMethodMember"/> is.
            /// </summary>
            public PropertyMethodType MethodType
            {
                get { return this.methodType; }
            }

            #endregion
            protected override IntermediateParameterMemberDictionary<IStructMethodMember, IIntermediateStructMethodMember, IMethodParameterMember<IStructMethodMember, IStructType>, IIntermediateMethodParameterMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>> InitializeParameters()
            {
                var result = base.InitializeParameters();
                if (this.Owner.AreParametersInitialized)
                    foreach (var param in this.Owner.Parameters.Values)
                        result._Add(param.UniqueIdentifier, new IndexerDependentParameter((IntermediateStructIndexerMember<TInstanceIntermediateType>.ParameterMembersDictionary.ParameterMember)param, this));
                result.Lock();
                return result;
            }
        }

        protected override IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember GetMethodMember(PropertyMethodType methodType)
        {
            switch (methodType)
            {
                case PropertyMethodType.SetMethod:
                    return new IndexerSetMethodMember(this);
                default:
                case PropertyMethodType.GetMethod:
                    return new IndexerMethodMember(PropertyMethodType.GetMethod, this);
            }
        }
    }
    public class IntermediateStructMethodMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.MethodMember,
        IIntermediateStructMethodMember
        where TInstanceIntermediateType :
            IntermediateStructType<TInstanceIntermediateType>
    {
        private TypeCollection<IInterfaceType> implementationTypes;
        /// <summary>
        /// Data member for <see cref="Attributes"/>.
        /// </summary>
        private ExtendedMethodAttributes instanceFlags;

        public IntermediateStructMethodMember(TInstanceIntermediateType parent)
            : base(parent)
        {
        }
        public IntermediateStructMethodMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
        }

        #region IStructMethodMember Members

        public IClassMethodMember BaseDefinition
        {
            get
            {
                if (!this.IsOverride)
                    throw new InvalidOperationException();
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IIntermediateExtendedInstanceMember Members

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> is 
        /// abstract (must be implemented, or is not yet 
        /// implemented).
        /// </summary>
        public bool IsAbstract
        {
            get
            {
                return ((this.instanceFlags & ExtendedMethodAttributes.Abstract) == ExtendedMethodAttributes.Abstract);
            }
            set
            {
                if (this.IsAbstract == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedMethodAttributes.Abstract;
                else
                    this.instanceFlags &= ~ExtendedMethodAttributes.Abstract;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> is
        /// virtual (can be overridden).
        /// </summary>
        public bool IsVirtual
        {
            get
            {
                return ((this.instanceFlags & ExtendedMethodAttributes.Virtual) == ExtendedMethodAttributes.Virtual);
            }
            set
            {
                throw new InvalidOperationException("Structs cannot contain virtual methods.");
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>
        /// finalizes the member removing the overrideable 
        /// status.
        /// </summary>
        public bool IsFinal
        {
            get
            {
                return ((this.instanceFlags & ExtendedMethodAttributes.Final) == ExtendedMethodAttributes.Final);
            }
            set
            {
                if (this.IsFinal == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedMethodAttributes.Final;
                else
                    this.instanceFlags &= ~ExtendedMethodAttributes.Final;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> 
        /// is an override of a virtual member.
        /// </summary>
        public bool IsOverride
        {
            get
            {
                return ((this.instanceFlags & ExtendedMethodAttributes.Override) == ExtendedMethodAttributes.Override);
            }
            set
            {
                if (this.IsOverride == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedMethodAttributes.Override;
                else
                    this.instanceFlags &= ~ExtendedMethodAttributes.Override;
            }
        }

        #endregion

        #region IIntermediateInstanceMember Members

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>
        /// hides the original definition completely.
        /// </summary>
        public bool IsHideBySignature
        {
            get
            {
                return ((this.instanceFlags & ExtendedMethodAttributes.HideBySignature) == ExtendedMethodAttributes.HideBySignature);
            }
            set
            {
                if (this.IsHideBySignature == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedMethodAttributes.HideBySignature;
                else
                    this.instanceFlags &= ~ExtendedMethodAttributes.HideBySignature;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> is
        /// static.
        /// </summary>
        public bool IsStatic
        {
            get
            {
                return IsExplicitStatic;
            }
            set
            {
                if (this.IsStatic == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedMethodAttributes.Static;
                else
                    this.instanceFlags &= ~ExtendedMethodAttributes.Static;
            }
        }

        public bool IsExplicitStatic
        {
            get
            {
                return ((this.instanceFlags & ExtendedMethodAttributes.Static) == ExtendedMethodAttributes.Static);
            }
        }


        #endregion

        #region IInstanceMember Members

        InstanceMemberAttributes IInstanceMember.Attributes
        {
            get
            {
                return ((InstanceMemberAttributes)this.instanceFlags) & InstanceMemberAttributes.FlagsMask;
            }
        }

        #endregion

        #region IExtendedInstanceMember Members

        public bool IsAsynchronous
        {
            get
            {
                return (this.instanceFlags & ExtendedMethodAttributes.Async) == ExtendedMethodAttributes.Async;
            }
            set
            {
                if (value)
                    this.instanceFlags |= ExtendedMethodAttributes.Async;
                else
                    this.instanceFlags &= ~ExtendedMethodAttributes.Async;
            }
        }

        public ExtendedMethodAttributes Attributes
        {
            get
            {
                return this.instanceFlags;
            }
        }
        protected override void OnSetReturnType(IType value)
        {
            base.OnSetReturnType(value);
            var returnType = value;
            if (returnType != null)
            {
                bool isAsync = this.IsAsynchronous;
                ILanguageAsynchronousQueryService asyncService = null;
                if (this.Assembly.Provider.TryGetService<ILanguageAsynchronousQueryService>(LanguageGuids.Services.AsyncQueryService, out asyncService))
                {
                    if (asyncService.IsReturnAsynchronous(value) || asyncService.IsAsynchronousPattern(this))
                        this.IsAsynchronousCandidate = true;
                    else
                        this.IsAsynchronousCandidate = false;
                }
                else if (returnType == this.Assembly.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType) && this.Name != null && this.Name.Length >= 5)
                {
                    if (this.Name.Substring(this.Name.Length - 5).ToLower() == "async")
                        this.IsAsynchronousCandidate = true;
                }
                else if (returnType == this.Assembly.IdentityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Threading.Tasks", "Task", 0)))
                    this.IsAsynchronousCandidate = true;
                else if (returnType.ElementClassification == TypeElementClassification.GenericTypeDefinition && returnType.ElementType != null && returnType.ElementType == this.Assembly.IdentityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Threading.Tasks", "Task", 1)))
                    this.IsAsynchronousCandidate = true;
                else
                    this.IsAsynchronousCandidate = false;

            }
        }


        /// <summary>
        /// Returns the <see cref="ExtendedMemberAttributes"/> that determine how the
        /// <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> is shown in its scope and inherited 
        /// scopes.
        /// </summary>
        ExtendedMemberAttributes IExtendedInstanceMember.Attributes
        {
            get
            {
                return (ExtendedMemberAttributes)this.Attributes & ExtendedMemberAttributes.FlagsMask;
            }
        }

        #endregion

        protected override IStructMethodMember OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
        {
            return new _StructTypeBase._MethodsBase._Method(this, genericReplacements);
        }

        public override IIntermediateAssembly Assembly
        {
            get { return this.Parent.Assembly; }
        }

        /// <summary>
        /// Returns whether the <see cref="IntermediateStructMethodMember{TInstanceIntermediateType}"/> 
        /// is a candidate for asynchrony.
        /// </summary>
        public bool IsAsynchronousCandidate { get; private set; }

        IEnumerable<IInterfaceType> IExtendedMethodMember.Implementations
        {
            get {
                return from t in this.Implementations
                       where t is IInterfaceType
                       select (IInterfaceType)t;
            }
        }

        public ITypeCollection Implementations
        {
            get { return this.implementationTypes ?? (this.implementationTypes = new TypeCollection<IInterfaceType>()); }
        }

    }

    public class IntermediateStructFieldMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.FieldMember,
        IIntermediateStructFieldMember
        where TInstanceIntermediateType :
            IntermediateStructType<TInstanceIntermediateType>
    {
        private bool readOnly;
        private bool constant;
        public IntermediateStructFieldMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
        }

        public new bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }
            set
            {
                if (value && constant)
                    constant = false;
                this.readOnly = value;
            }
        }

        public new bool Constant
        {
            get
            {
                return this.constant;
            }
            set
            {
                if (value && readOnly)
                    this.readOnly = false;
                this.constant = value;
            }
        }

        protected override bool OnGetReadonly()
        {
            return this.ReadOnly;
        }

        protected override bool OnGetConstant()
        {
            return this.Constant;
        }

        public new InstanceFieldMemberAttributes Attributes
        {
            get { return ((InstanceFieldMemberAttributes)base.Attributes) | (Constant ? InstanceFieldMemberAttributes.Constant : InstanceFieldMemberAttributes.None) | (ReadOnly ? InstanceFieldMemberAttributes.ReadOnly : InstanceFieldMemberAttributes.None); }
        }
    }

    public class IntermediateStructPropertyMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.PropertyMember,
        IIntermediateStructPropertyMember
        where TInstanceIntermediateType :
            IntermediateStructType<TInstanceIntermediateType>
    {

        protected internal IntermediateStructPropertyMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
        }

        protected internal IntermediateStructPropertyMember(TInstanceIntermediateType parent)
            : base(parent)
        {
        }
        public class PropertySetMethodMember :
            PropertyMethodMember,
            IIntermediatePropertySetMethodMember
        {
            private _ValueParameter valueParameter;

            public PropertySetMethodMember(IntermediateStructPropertyMember<TInstanceIntermediateType> owner)
                : base(PropertyMethodType.SetMethod, owner)
            {
            }

            #region IIntermediatePropertySetMethodMember Members

            public IIntermediateMethodParameterMember ValueParameter
            {
                get
                {
                    if (this.valueParameter == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                        else
                            this.valueParameter = (_ValueParameter)this.Parameters["value"];
                    return valueParameter;
                }
            }

            #endregion

            #region IIntermediatePropertySignatureSetMethodMember Members

            IIntermediateSignatureParameterMember IIntermediatePropertySignatureSetMethodMember.ValueParameter
            {
                get { return this.ValueParameter; }
            }

            #endregion

            private class _ValueParameter :
                ParameterMember
            {

                public _ValueParameter(PropertySetMethodMember parent)
                    : base(parent)
                {
                }

                private PropertySetMethodMember _Parent { get { return (PropertySetMethodMember)base.Parent; } }
                protected override void OnSetName(string name)
                {
                    throw new NotSupportedException();
                }

                protected override string OnGetName()
                {
                    return "value";
                }

                public override IType ParameterType
                {
                    get
                    {
                        return this._Parent.Owner.PropertyType;
                    }
                    set
                    {
                        this._Parent.Owner.PropertyType = value;
                    }
                }
            }


            protected override IntermediateParameterMemberDictionary<IStructMethodMember, IIntermediateStructMethodMember, IMethodParameterMember<IStructMethodMember, IStructType>, IIntermediateMethodParameterMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>> InitializeParameters()
            {
                var result = base.InitializeParameters();
                result._Add(TypeSystemIdentifiers.GetMemberIdentifier("value"), this.valueParameter = new _ValueParameter(this));
                return result;
            }

            protected override void Dispose(bool disposing)
            {
                try
                {
                    this.valueParameter = null;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }
        public class PropertyMethodMember :
            IntermediateStructMethodMember<TInstanceIntermediateType>,
            IIntermediatePropertyMethodMember
        {
            private PropertyMethodType methodType;
            private IntermediateStructPropertyMember<TInstanceIntermediateType> owner;
            public PropertyMethodMember(PropertyMethodType methodType, IntermediateStructPropertyMember<TInstanceIntermediateType> owner)
                : base(owner == null ? null : (TInstanceIntermediateType)(owner.Parent))
            {
                this.methodType = methodType;
                this.owner = owner;
            }

            protected IntermediateStructPropertyMember<TInstanceIntermediateType> Owner
            {
                get
                {
                    return this.owner;
                }
            }

            IParameterMemberDictionary IIntermediatePropertySignatureMethodMember.Parameters
            {
                get { return (IParameterMemberDictionary)this.Parameters; }
            }

            #region IPropertySignatureMethodMember Members

            public PropertyMethodType MethodType
            {
                get { return this.methodType; }
            }

            #endregion

            protected override string OnGetName()
            {
                switch (this.MethodType)
                {
                    case PropertyMethodType.SetMethod:
                        return string.Format("set_{0}", this.owner.Name);
                    default:
                    case PropertyMethodType.GetMethod:
                        return string.Format("get_{0}", this.owner.Name);
                }
            }

            protected override sealed void OnSetName(string name)
            {
                throw new NotSupportedException(string.Format("Cannot set the name of the {0} method of a property.", MethodType == PropertyMethodType.SetMethod ? "set" : "get"));
            }

            protected override IntermediateParameterMemberDictionary<IStructMethodMember, IIntermediateStructMethodMember, IMethodParameterMember<IStructMethodMember, IStructType>, IIntermediateMethodParameterMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>> InitializeParameters()
            {
                var result = base.InitializeParameters();
                result.Lock();
                return result;
            }

            protected override IntermediateMethodSignatureMemberBase<IMethodParameterMember<IStructMethodMember, IStructType>, IIntermediateMethodParameterMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>, IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>.TypeParameterDictionary InitializeTypeParameters()
            {
                var result = base.InitializeTypeParameters();
                result.Lock();
                return result;
            }

            protected override IType OnGetReturnType()
            {
                switch (this.MethodType)
                {
                    case PropertyMethodType.GetMethod:
                        return this.Owner.PropertyType;
                    case PropertyMethodType.SetMethod:
                        return this.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
                    default:
                        return base.OnGetReturnType();
                }
            }

            protected override void Dispose(bool disposing)
            {
                try
                {
                    this.owner = null;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }

        protected override IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember GetMethodMember(PropertyMethodType methodType)
        {
            switch (methodType)
            {
                case PropertyMethodType.SetMethod:
                    return new PropertySetMethodMember(this);
                default:
                case PropertyMethodType.GetMethod:
                    return new PropertyMethodMember(PropertyMethodType.GetMethod, this);
            }
        }
    }
}
