using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
//using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base class for events of a class type.
    /// </summary>
    public class IntermediateClassEventMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.EventMember,
        IIntermediateClassEventMember
        where TInstanceIntermediateType :
            IntermediateClassType<TInstanceIntermediateType>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateClassEventMember{TInstanceIntermediateType}"/> instance
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IntermediateClassType"/>
        /// to which the <see cref="IntermediateClassEventMember{TInstanceIntermediateType}"/> exists upon.</param>
        public IntermediateClassEventMember(TInstanceIntermediateType parent)
            : base(parent)
        {
        }

        public ExtendedMemberAttributes ImplicitAttributes
        {
            get
            {
                return this.Attributes | (this.IsStatic ? ExtendedMemberAttributes.Static : ExtendedMemberAttributes.None);
            }
        }

        public class EventMethodMember :
            IntermediateClassMethodMember<TInstanceIntermediateType>,
            IIntermediateEventMethodMember
        {
            private IntermediateClassEventMember<TInstanceIntermediateType> parent;
            private EventMethodType methodType;
            public EventMethodMember(IntermediateClassEventMember<TInstanceIntermediateType> parent, EventMethodType methodType)
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

        protected override EventMethodMember GetMethodMember(EventMethodType type)
        {
            return new EventMethodMember(this, type);
        }
    }

    public class IntermediateClassCtorMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.ConstructorMember,
        IIntermediateClassCtorMember
        where TInstanceIntermediateType :
            IntermediateClassType<TInstanceIntermediateType>
    {
        public IntermediateClassCtorMember(TInstanceIntermediateType parent, bool typeInitializer = false)
            : base(parent, typeInitializer)
        {
        }

        public override IIntermediateAssembly Assembly
        {
            get { return Parent.Assembly; }
        }
    }

    public class IntermediateClassIndexerMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.IndexerMember,
        IIntermediateClassIndexerMember
        where TInstanceIntermediateType :
            IntermediateClassType<TInstanceIntermediateType>
    {

        public IntermediateClassIndexerMember(TInstanceIntermediateType parent)
            : base(parent)
        {
        }

        public IntermediateClassIndexerMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
        }

        public ExtendedMemberAttributes ImplicitAttributes
        {
            get
            {
                return this.Attributes | (this.IsStatic ? ExtendedMemberAttributes.Static : ExtendedMemberAttributes.None);
            }
        }

        protected override void OnParameterAdded(EventArgsR1<IIntermediateIndexerParameterMember<IClassIndexerMember, IIntermediateClassIndexerMember, IClassType, IIntermediateClassType>> e)
        {
            if (this.CanRead && this.IsGetMethodInitialized)
            {
                var gm = this.GetMethod;
                if (gm._AreParametersInitialized)
                {
                    var gmParams = gm.Parameters;
                    gmParams._Add(e.Arg1.UniqueIdentifier, GetIndexerDependentParameter(gm, (ParameterMembersDictionary.ParameterMember)e.Arg1));
                }
            }
            if (this.CanWrite && this.IsSetMethodInitialized)
            {
                var sm = this.SetMethod;
                if (sm._AreParametersInitialized)
                {
                    var smParams = sm.Parameters;
                    var valueParam = (IndexerMethodMember.IndexerValueParameter)((IIntermediatePropertySetMethodMember)sm).ValueParameter;
                    smParams._Remove(valueParam.UniqueIdentifier);
                    smParams._Add(e.Arg1.UniqueIdentifier, GetIndexerDependentParameter(sm, (ParameterMembersDictionary.ParameterMember)e.Arg1));
                    smParams._Add(valueParam.UniqueIdentifier, valueParam);
                }
            }
            base.OnParameterAdded(e);
        }

        internal virtual IndexerMethodMember.IndexerDependentParameter GetIndexerDependentParameter(IndexerMethodMember method, ParameterMembersDictionary.ParameterMember indexerParameter)
        {
            return new IndexerMethodMember.IndexerDependentParameter(indexerParameter, method);
        }


        public class IndexerSetMethodMember :
            IndexerMethodMember,
            IIntermediatePropertySetMethodMember
        {
            private IndexerValueParameter valueParameter;

            public IndexerSetMethodMember(IntermediateClassIndexerMember<TInstanceIntermediateType> owner)
                : base(PropertyMethodType.SetMethod, owner)
            {

            }

            protected override IntermediateParameterMemberDictionary<IClassMethodMember, IIntermediateClassMethodMember, IMethodParameterMember<IClassMethodMember, IClassType>, IIntermediateMethodParameterMember<IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType>> InitializeParameters()
            {
                var result = base.InitializeParameters();
                this.valueParameter = GetIndexerValueParameter();
                result._Add(this.valueParameter.UniqueIdentifier, this.valueParameter);
                return result;
            }

            protected virtual IndexerValueParameter GetIndexerValueParameter()
            {
                return new IndexerValueParameter(this.Owner, this);
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
            IntermediateClassMethodMember<TInstanceIntermediateType>,
            IIntermediatePropertyMethodMember
        {
            private PropertyMethodType methodType;
            private IntermediateClassIndexerMember<TInstanceIntermediateType> owner;
            public IndexerMethodMember(PropertyMethodType methodType, IntermediateClassIndexerMember<TInstanceIntermediateType> owner)
                : base(owner == null ? null : (TInstanceIntermediateType)owner.Parent)
            {
                if (owner == null)
                    throw new ArgumentNullException("parent");
                this.owner = owner;
                this.methodType = methodType;
            }

            /* *
             * Actual
             * */
            internal protected new bool _AreParametersInitialized { get { return base.AreParametersInitialized; } }

            protected internal class IndexerValueParameter :
                IndexerMethodMember.ParameterMember
            {
                private IntermediateClassIndexerMember<TInstanceIntermediateType> owner;
                internal IndexerValueParameter(IntermediateClassIndexerMember<TInstanceIntermediateType> owner, IIntermediateClassMethodMember parent)
                    : base(parent)
                {
                    this.owner = owner;
                }

                protected override string OnGetName()
                {
                    return "value";
                }

                protected IntermediateClassIndexerMember<TInstanceIntermediateType> Owner { get { return this.owner; } }

                protected override sealed void OnSetName(string name)
                {
                    throw new NotSupportedException("Cannot set the name of the value parameter of the set method of an indexer, this is typically set by the target language.");
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

            protected internal class IndexerDependentParameter :
                IndexerMethodMember.ParameterMember
            {
                private ParameterMembersDictionary.ParameterMember original;
                internal IndexerDependentParameter(ParameterMembersDictionary.ParameterMember original, IIntermediateClassMethodMember parent)
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

                protected override sealed void OnSetName(string name)
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
                    return (MetadataDefinitionCollection)this.original.Metadata;
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

            /* *
             * Perceived
             * */
            protected override bool AreParametersInitialized
            {
                get
                {
                    return true;
                }
            }

            protected override sealed void OnSetName(string name)
            {
                throw new NotSupportedException("Cannot set the name of a method of an indexer, set the indexer's name.  Rationale: indexer method names are usually set by the language, values provided through get_Name() are for reference only.");
            }

            protected IntermediateClassIndexerMember<TInstanceIntermediateType> Owner
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
            protected override IntermediateParameterMemberDictionary<IClassMethodMember, IIntermediateClassMethodMember, IMethodParameterMember<IClassMethodMember, IClassType>, IIntermediateMethodParameterMember<IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType>> InitializeParameters()
            {
                var result = base.InitializeParameters();
                if (this.Owner.AreParametersInitialized)
                    foreach (var param in this.Owner.Parameters.Values)
                        result._Add(param.UniqueIdentifier, this.Owner.GetIndexerDependentParameter(this, (IntermediateClassIndexerMember<TInstanceIntermediateType>.ParameterMembersDictionary.ParameterMember)param));
                result.Lock();
                return result;
            }
        }

        protected override IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember GetMethodMember(PropertyMethodType methodType)
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

    public class IntermediateClassPropertyMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.PropertyMember,
        IIntermediateClassPropertyMember
        where TInstanceIntermediateType :
            IntermediateClassType<TInstanceIntermediateType>
    {

        protected internal IntermediateClassPropertyMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
        }

        protected internal IntermediateClassPropertyMember(TInstanceIntermediateType parent)
            : base(parent)
        {
        }

        public class PropertySetMethodMember :
            PropertyMethodMember,
            IIntermediatePropertySetMethodMember
        {
            private ValueParameterMember valueParameter;

            public PropertySetMethodMember(IntermediateClassPropertyMember<TInstanceIntermediateType> owner, TInstanceIntermediateType parent)
                : base(PropertyMethodType.SetMethod,owner, parent)
            {
            }
            protected internal class ValueParameterMember :
                ParameterMember
            {

                public ValueParameterMember(PropertySetMethodMember parent)
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

            #region IIntermediatePropertySetMethodMember Members

            public IIntermediateMethodParameterMember ValueParameter
            {
                get {
                    if (this.valueParameter == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                        else
                            return this.valueParameter = (ValueParameterMember)this.Parameters["value"];
                    return valueParameter;
                }
            }

            #endregion

            protected override IntermediateParameterMemberDictionary<IClassMethodMember, IIntermediateClassMethodMember, IMethodParameterMember<IClassMethodMember, IClassType>, IIntermediateMethodParameterMember<IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType>> InitializeParameters()
            {
                var result = base.InitializeParameters();
                result.Unlock();
                result._Add(TypeSystemIdentifiers.GetMemberIdentifier("value"), this.valueParameter = GetValueParameter());
                result.Lock();
                return result;
            }

            protected virtual ValueParameterMember GetValueParameter()
            {
                return new ValueParameterMember(this);
            }

            #region IIntermediatePropertySignatureSetMethodMember Members

            IIntermediateSignatureParameterMember IIntermediatePropertySignatureSetMethodMember.ValueParameter
            {
                get { return this.ValueParameter; }
            }

            #endregion

        }
        public class PropertyMethodMember :
            IntermediateClassMethodMember<TInstanceIntermediateType>,
            IIntermediatePropertyMethodMember
        {
            private PropertyMethodType methodType;
            public PropertyMethodMember(PropertyMethodType methodType, IntermediateClassPropertyMember<TInstanceIntermediateType> owner, TInstanceIntermediateType parent)
                : base(parent)
            {
                this.methodType = methodType;
                this.Owner = owner;
            }

            protected IntermediateClassPropertyMember<TInstanceIntermediateType> Owner { get; private set; }

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
                        return string.Format("set_{0}", this.Owner.Name);
                    default:
                    case PropertyMethodType.GetMethod:
                        return string.Format("get_{0}", this.Owner.Name);
                }
            }

            IParameterMemberDictionary IIntermediatePropertySignatureMethodMember.Parameters
            {
                get { return (IParameterMemberDictionary)this.Parameters; }
            }

            protected override sealed void OnSetName(string name)
            {
                throw new NotSupportedException(string.Format("Cannot set the name of the {0} method of a property.", MethodType == PropertyMethodType.SetMethod ? "set" : "get"));
            }

            protected override IntermediateParameterMemberDictionary<IClassMethodMember, IIntermediateClassMethodMember, IMethodParameterMember<IClassMethodMember, IClassType>, IIntermediateMethodParameterMember<IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType>> InitializeParameters()
            {
                var result = base.InitializeParameters();
                result.Lock();
                return result;
            }
            protected override IntermediateMethodSignatureMemberBase<IMethodParameterMember<IClassMethodMember, IClassType>, IIntermediateMethodParameterMember<IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType>, IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType>.TypeParameterDictionary InitializeTypeParameters()
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
                    this.Owner = null;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }

        public ExtendedMemberAttributes ImplicitAttributes
        {
            get
            {
                return this.Attributes | (this.IsStatic ? ExtendedMemberAttributes.Static : ExtendedMemberAttributes.None);
            }
        }

        protected override IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember GetMethodMember(PropertyMethodType methodType)
        {
            switch (methodType)
            {
                case PropertyMethodType.SetMethod:
                    return new IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertySetMethodMember(this, (TInstanceIntermediateType)this.Parent);
                default:
                case PropertyMethodType.GetMethod:
                    return new IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember(PropertyMethodType.GetMethod, this, (TInstanceIntermediateType)this.Parent);
            }
        }
    }

    public class IntermediateClassFieldMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.FieldMember,
        IIntermediateClassFieldMember
        where TInstanceIntermediateType :
            IntermediateClassType<TInstanceIntermediateType>
    {
        private bool readOnly;
        private bool constant;
        public IntermediateClassFieldMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
        }

        public InstanceFieldMemberAttributes ImplicitAttributes
        {
            get
            {
                return this.Attributes | (this.IsStatic ? InstanceFieldMemberAttributes.Static : InstanceFieldMemberAttributes.None);
            }
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

    /// <summary>
    /// Provides a base class for methods of a class type.
    /// </summary>
    public class IntermediateClassMethodMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.MethodMember,
        IIntermediateClassMethodMember
        where TInstanceIntermediateType :
            IntermediateClassType<TInstanceIntermediateType>
    {
        /// <summary>
        /// Data member for <see cref="Attributes"/>.
        /// </summary>
        private ClassMethodMemberFlags instanceFlags;
        /// <summary>
        /// Creates a new <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> instance
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TInstanceIntermediateType"/> which owns the
        /// <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>.</param>
        public IntermediateClassMethodMember(TInstanceIntermediateType parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> instance
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the unique name of the 
        /// <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>.</param>
        /// <param name="parent">The <typeparamref name="TInstanceIntermediateType"/> which owns the
        /// <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>.</param>
        public IntermediateClassMethodMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
        }

        #region IIntermediateClassMethodMember Members

        /// <summary>
        /// Returns/sets whether the current <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>
        /// is an extension method.
        /// </summary>
        public bool IsExtensionMethod
        {
            get
            {
                return (this.instanceFlags & ClassMethodMemberFlags.Extension) == ClassMethodMemberFlags.Extension;
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ClassMethodMemberFlags.Virtual | ClassMethodMemberFlags.Override | ClassMethodMemberFlags.Final | ClassMethodMemberFlags.HideBySignature | ClassMethodMemberFlags.HideByName);
                    this.instanceFlags |= ClassMethodMemberFlags.Extension;
                }
                else
                    this.instanceFlags &= ~(ClassMethodMemberFlags.Extension);
            }
        }

        public bool IsAsynchronous
        {
            get
            {
                return (this.instanceFlags & ClassMethodMemberFlags.Async) == ClassMethodMemberFlags.Async;
            }
            set
            {
                if (value)
                    this.instanceFlags |= ClassMethodMemberFlags.Async;
                else
                    this.instanceFlags &= ~ClassMethodMemberFlags.Async;
            }
        }

        #endregion

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
                //else if (returnType == this.Assembly.IdentityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Threading.Tasks", "Task", 0)))
                //    this.IsAsynchronousCandidate = true;
                //else if (returnType.ElementClassification == TypeElementClassification.GenericTypeDefinition && returnType.ElementType != null && returnType.ElementType == this.Assembly.IdentityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Threading.Tasks", "Task", 1)))
                //    this.IsAsynchronousCandidate = true;
                else
                    this.IsAsynchronousCandidate = false;
            }
        }

        ExtendedMethodAttributes IExtendedMethodMember.Attributes
        {
            get
            {
                return (ExtendedMethodAttributes)this.Attributes & ExtendedMethodAttributes.FlagsMask;
            }
        }

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
                return ((this.instanceFlags & ClassMethodMemberFlags.Abstract) == ClassMethodMemberFlags.Abstract);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ClassMethodMemberFlags.Static | ClassMethodMemberFlags.Virtual | ClassMethodMemberFlags.Override | ClassMethodMemberFlags.Final | ClassMethodMemberFlags.Extension);
                    this.instanceFlags |= ClassMethodMemberFlags.Abstract;
                }
                else
                    this.instanceFlags &= ~ClassMethodMemberFlags.Abstract;
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
                return ((this.instanceFlags & ClassMethodMemberFlags.Virtual) == ClassMethodMemberFlags.Virtual);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ClassMethodMemberFlags.Static | ClassMethodMemberFlags.Abstract | ClassMethodMemberFlags.Override | ClassMethodMemberFlags.Final | ClassMethodMemberFlags.Extension);
                    this.instanceFlags |= ClassMethodMemberFlags.Virtual;
                }
                else
                    this.instanceFlags &= ~ClassMethodMemberFlags.Virtual;
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
                return ((this.instanceFlags & ClassMethodMemberFlags.Final) == ClassMethodMemberFlags.Final);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ClassMethodMemberFlags.Virtual | ClassMethodMemberFlags.Abstract | ClassMethodMemberFlags.Static | ClassMethodMemberFlags.Extension);
                    this.instanceFlags |= (ClassMethodMemberFlags.Final | ClassMethodMemberFlags.Override);
                }
                else
                    this.instanceFlags &= ~ClassMethodMemberFlags.Final;
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
                return ((this.instanceFlags & ClassMethodMemberFlags.Override) == ClassMethodMemberFlags.Override);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ClassMethodMemberFlags.Static | ClassMethodMemberFlags.Abstract | ClassMethodMemberFlags.Virtual | ClassMethodMemberFlags.Extension);
                    this.instanceFlags |= ClassMethodMemberFlags.Override;
                }
                else
                    this.instanceFlags &= (ClassMethodMemberFlags.Override | ClassMethodMemberFlags.Final);
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
                return ((this.instanceFlags & ClassMethodMemberFlags.HideBySignature) == ClassMethodMemberFlags.HideBySignature);
            }
            set
            {
                if (this.IsHideBySignature == value)
                    return;
                if (value)
                    this.instanceFlags |= ClassMethodMemberFlags.HideBySignature;
                else
                    this.instanceFlags &= ~ClassMethodMemberFlags.HideBySignature;
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
                var specialModifier = Parent.SpecialModifier;
                if ((specialModifier & SpecialClassModifier.Static) == SpecialClassModifier.Static ||
                    (specialModifier & SpecialClassModifier.Module) == SpecialClassModifier.Module)
                    return true;
                return IsExplicitStatic;
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ClassMethodMemberFlags.Abstract | ClassMethodMemberFlags.Virtual | ClassMethodMemberFlags.Override | ClassMethodMemberFlags.Final);
                    this.instanceFlags |= ClassMethodMemberFlags.Static;
                }
                else
                    this.instanceFlags &= ~ClassMethodMemberFlags.Static;
            }
        }

        public bool IsExplicitStatic
        {
            get
            {
                return ((this.instanceFlags & ClassMethodMemberFlags.Static) == ClassMethodMemberFlags.Static);
            }
        }

        #endregion

        #region IInstanceMember Members

        InstanceMemberAttributes IInstanceMember.Attributes
        {
            get { return ((InstanceMemberAttributes)this.instanceFlags) & InstanceMemberAttributes.FlagsMask; }
        }

        #endregion

        #region IExtendedInstanceMember Members

        /// <summary>
        /// Returns the <see cref="ExtendedMemberAttributes"/> that determine how the
        /// <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> is shown in its scope and inherited 
        /// scopes.
        /// </summary>
        ExtendedMemberAttributes IExtendedInstanceMember.Attributes
        {
            get { return (ExtendedMemberAttributes)this.Attributes; }
        }

        #endregion

        #region IClassMethodMember Members

        public ClassMethodMemberFlags Attributes
        {
            get
            {
                return this.instanceFlags;
            }
        }

        public ClassMethodMemberFlags ImplicitAttributes
        {
            get
            {
                return this.Attributes | (this.IsStatic ? ClassMethodMemberFlags.Static : ClassMethodMemberFlags.None);
            }
        }

        public IClassMethodMember PreviousDefinition
        {
            get
            {
                if (!this.IsOverride)
                    throw new InvalidOperationException();
                return this.ObtainPreviousDefinition();
            }
        }

        private IClassMethodMember ObtainPreviousDefinition()
        {
            return AbstractGateway.ObtainPreviousDefinition(this);
        }

        /// <summary>
        /// Returns the base definition of a virtual method that is an override
        /// of the original.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>
        /// is not an overridden member.</exception>
        public IClassMethodMember BaseDefinition
        {
            get
            {
                return AbstractGateway.ObtainBaseDefinition(this);
            }
        }

        #endregion

        protected override IClassMethodMember OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
        {
            return new _ClassTypeBase._MethodsBase._Method(this, genericReplacements);
        }

        /// <summary>
        /// Returns whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> 
        /// is a candidate for asynchrony.
        /// </summary>
        public bool IsAsynchronousCandidate { get; private set; }


        IEnumerable<IInterfaceType> IExtendedInstanceMember.Implementations
        {
            get
            {
                foreach (IType t in this.Implementations)
                    yield return (IInterfaceType)t;
            }
        }

    }

}
