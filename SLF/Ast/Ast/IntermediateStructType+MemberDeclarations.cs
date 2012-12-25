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
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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

        public class IndexerSetMethodMember :
            IndexerMethodMember,
            IIntermediatePropertySetMethodMember
        {
            private _ValueParameter valueParameter;

            public IndexerSetMethodMember(IntermediateStructIndexerMember<TInstanceIntermediateType> owner)
                : base(owner, PropertyMethodType.SetMethod)
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
                            valueParameter = (_ValueParameter)this.Parameters["value"];
                    return valueParameter;
                }
            }

            #endregion

            protected override bool AreParametersInitialized
            {
                get
                {
                    return true;
                }
            }

            private class _ValueParameter :
                ParameterMember
            {

                public _ValueParameter(IndexerSetMethodMember parent)
                    : base(parent)
                {
                }

                private IndexerSetMethodMember _Parent { get { return (IndexerSetMethodMember)base.Parent; } }
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


            #region IIntermediatePropertySignatureSetMethodMember Members

            IIntermediateSignatureParameterMember IIntermediatePropertySignatureSetMethodMember.ValueParameter
            {
                get { return this.ValueParameter; }
            }

            #endregion

            protected override IntermediateParameterMemberDictionary<IStructMethodMember, IIntermediateStructMethodMember, IMethodParameterMember<IStructMethodMember, IStructType>, IIntermediateMethodParameterMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>> InitializeParameters()
            {
                var result = base.InitializeParameters();
                result.Unlock();
                result._Add(AstIdentifier.GetMemberIdentifier("value"), this.valueParameter = new _ValueParameter(this));
                lock (this.Owner.Parameters.SyncRoot)
                    this.Owner.Parameters.ItemAdded += new EventHandler<EventArgsR1<IIntermediateIndexerParameterMember<IStructIndexerMember, IIntermediateStructIndexerMember, IStructType, IIntermediateStructType>>>(OwnerParameters_ItemAdded);
                result.Lock();
                return result;
            }

            void OwnerParameters_ItemAdded(object sender, EventArgsR1<IIntermediateIndexerParameterMember<IStructIndexerMember, IIntermediateStructIndexerMember, IStructType, IIntermediateStructType>> e)
            {
                var parameters = this.Parameters as IntermediateParameterMemberDictionary<IStructMethodMember, IIntermediateStructMethodMember, IMethodParameterMember<IStructMethodMember, IStructType>, IIntermediateMethodParameterMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>>;
                lock (this.Owner.Parameters.SyncRoot)
                {
                    lock (parameters.SyncRoot)
                    {
                        parameters._Remove(valueParameter.UniqueIdentifier);
                        parameters._Add(valueParameter.UniqueIdentifier, valueParameter);
                    }
                }
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

        public class IndexerMethodMember :
            IntermediateStructMethodMember<TInstanceIntermediateType>,
            IIntermediatePropertyMethodMember
        {
            private PropertyMethodType methodType;
            private IntermediateStructIndexerMember<TInstanceIntermediateType> owner;
            public IndexerMethodMember(IntermediateStructIndexerMember<TInstanceIntermediateType> owner, PropertyMethodType methodType)
                : base(owner == null ? null : (TInstanceIntermediateType)owner.Parent)
            {
                if (owner == null)
                    throw new ArgumentNullException("parent");
                this.owner = owner;
                this.methodType = methodType;
            }

            protected IntermediateStructIndexerMember<TInstanceIntermediateType> Owner
            {
                get
                {
                    return this.owner;
                }
            }

            protected override bool AreParametersInitialized
            {
                get
                {
                    return base.AreParametersInitialized || this.Owner.AreParametersInitialized;
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

            private class _WrappedParameter :
                ParameterMember<IStructIndexerMember, IIntermediateStructIndexerMember, IIndexerParameterMember<IStructIndexerMember, IStructType>, IIntermediateIndexerParameterMember<IStructIndexerMember, IIntermediateStructIndexerMember, IStructType, IIntermediateStructType>>
            {
                public _WrappedParameter(IIntermediateIndexerParameterMember<IStructIndexerMember, IIntermediateStructIndexerMember, IStructType, IIntermediateStructType> original, IndexerMethodMember parent)
                    : base(original, parent)
                {
                }
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
                lock (this.Owner.Parameters.SyncRoot)
                {
                    var items = this.Owner.Parameters.Values.ToArray();
                    lock (result.SyncRoot)
                        foreach (var element in items)
                            result._Add(AstIdentifier.GetMemberIdentifier(element.Name), new _WrappedParameter(element, this));
                    this.owner.Parameters.ItemAdded += new EventHandler<Utilities.Events.EventArgsR1<IIntermediateIndexerParameterMember<IStructIndexerMember, IIntermediateStructIndexerMember, IStructType, IIntermediateStructType>>>(OwnerParameters_ItemAdded);
                    this.owner.Parameters.ItemRemoved += new EventHandler<EventArgsR1<IIntermediateIndexerParameterMember<IStructIndexerMember, IIntermediateStructIndexerMember, IStructType, IIntermediateStructType>>>(OwnerParameters_ItemRemoved);
                }
                result.Lock();
                return result;
            }

            void OwnerParameters_ItemAdded(object sender, EventArgsR1<IIntermediateIndexerParameterMember<IStructIndexerMember, IIntermediateStructIndexerMember, IStructType, IIntermediateStructType>> e)
            {
                lock (this.Owner.Parameters.SyncRoot)
                {
                    var parameters = this.Parameters as IntermediateParameterMemberDictionary<IStructMethodMember, IIntermediateStructMethodMember, IMethodParameterMember<IStructMethodMember, IStructType>, IIntermediateMethodParameterMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>>;
                    lock (parameters.SyncRoot)
                    {
                        parameters.Unlock();
                        parameters._Add(e.Arg1.UniqueIdentifier, new _WrappedParameter(e.Arg1, this));
                        parameters.Lock();
                    }
                }
            }

            void OwnerParameters_ItemRemoved(object sender, EventArgsR1<IIntermediateIndexerParameterMember<IStructIndexerMember, IIntermediateStructIndexerMember, IStructType, IIntermediateStructType>> e)
            {
                if (this.owner == null)
                    return;
                lock (this.Owner.Parameters.SyncRoot)
                {
                    var parameters = this.Parameters as IntermediateParameterMemberDictionary<IStructMethodMember, IIntermediateStructMethodMember, IMethodParameterMember<IStructMethodMember, IStructType>, IIntermediateMethodParameterMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>>;
                    lock (parameters.SyncRoot)
                    {
                        parameters.Unlock();
                        var paramTarget = (from parameter in parameters.Values
                                           let _param = parameter as _WrappedParameter
                                           where _param != null
                                           where _param.AlternateParameter == e.Arg1
                                           select _param).FirstOrDefault();
                        if (paramTarget != null)
                            parameters._Remove(paramTarget.UniqueIdentifier);
                        parameters.Lock();
                    }
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

        protected override IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember GetMethodMember(PropertyMethodType methodType)
        {
            switch (methodType)
            {
                case PropertyMethodType.GetMethod:
                    return new IndexerMethodMember(this, methodType);
                case PropertyMethodType.SetMethod:
                    return new IndexerSetMethodMember(this);
                default:
                    throw new ArgumentOutOfRangeException("methodType");
            }
        }
    }
    public class IntermediateStructMethodMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.MethodMember,
        IIntermediateStructMethodMember
        where TInstanceIntermediateType :
            IntermediateStructType<TInstanceIntermediateType>
    {
        /// <summary>
        /// Data member for <see cref="InstanceFlags"/>.
        /// </summary>
        private ExtendedMethodMemberFlags instanceFlags;

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
                return ((this.instanceFlags & ExtendedMethodMemberFlags.Abstract) == ExtendedMethodMemberFlags.Abstract);
            }
            set
            {
                if (this.IsAbstract == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedMethodMemberFlags.Abstract;
                else
                    this.instanceFlags &= ~ExtendedMethodMemberFlags.Abstract;
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
                return ((this.instanceFlags & ExtendedMethodMemberFlags.Virtual) == ExtendedMethodMemberFlags.Virtual);
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
                return ((this.instanceFlags & ExtendedMethodMemberFlags.Final) == ExtendedMethodMemberFlags.Final);
            }
            set
            {
                if (this.IsFinal == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedMethodMemberFlags.Final;
                else
                    this.instanceFlags &= ~ExtendedMethodMemberFlags.Final;
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
                return ((this.instanceFlags & ExtendedMethodMemberFlags.Override) == ExtendedMethodMemberFlags.Override);
            }
            set
            {
                if (this.IsOverride == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedMethodMemberFlags.Override;
                else
                    this.instanceFlags &= ~ExtendedMethodMemberFlags.Override;
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
                return ((this.instanceFlags & ExtendedMethodMemberFlags.HideBySignature) == ExtendedMethodMemberFlags.HideBySignature);
            }
            set
            {
                if (this.IsHideBySignature == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedMethodMemberFlags.HideBySignature;
                else
                    this.instanceFlags &= ~ExtendedMethodMemberFlags.HideBySignature;
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
                    this.instanceFlags |= ExtendedMethodMemberFlags.Static;
                else
                    this.instanceFlags &= ~ExtendedMethodMemberFlags.Static;
            }
        }

        public bool IsExplicitStatic
        {
            get
            {
                return ((this.instanceFlags & ExtendedMethodMemberFlags.Static) == ExtendedMethodMemberFlags.Static);
            }
        }


        #endregion

        #region IInstanceMember Members

        InstanceMemberFlags IInstanceMember.InstanceFlags
        {
            get
            {
                return ((InstanceMemberFlags)this.instanceFlags) & InstanceMemberFlags.FlagsMask;
            }
        }

        #endregion

        #region IExtendedInstanceMember Members

        public bool IsAsynchronous
        {
            get
            {
                return (this.instanceFlags & ExtendedMethodMemberFlags.Async) == ExtendedMethodMemberFlags.Async;
            }
            set
            {
                if (value)
                    this.instanceFlags |= ExtendedMethodMemberFlags.Async;
                else
                    this.instanceFlags &= ~ExtendedMethodMemberFlags.Async;
            }
        }

        public ExtendedMethodMemberFlags InstanceFlags
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
                else if (returnType == this.Assembly.IdentityManager.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System.Threading.Tasks", "Task", 0)))
                    this.IsAsynchronousCandidate = true;
                else if (returnType.ElementClassification == TypeElementClassification.GenericTypeDefinition && returnType.ElementType != null && returnType.ElementType == this.Assembly.IdentityManager.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System.Threading.Tasks", "Task", 1)))
                    this.IsAsynchronousCandidate = true;
                else
                    this.IsAsynchronousCandidate = false;

            }
        }


        /// <summary>
        /// Returns the <see cref="ExtendedInstanceMemberFlags"/> that determine how the
        /// <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> is shown in its scope and inherited 
        /// scopes.
        /// </summary>
        ExtendedInstanceMemberFlags IExtendedInstanceMember.InstanceFlags
        {
            get
            {
                return (ExtendedInstanceMemberFlags)this.InstanceFlags & ExtendedInstanceMemberFlags.FlagsMask;
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
    }

    public class IntermediateStructFieldMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.FieldMember,
        IIntermediateStructFieldMember
        where TInstanceIntermediateType :
            IntermediateStructType<TInstanceIntermediateType>
    {
        public IntermediateStructFieldMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
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
                result._Add(AstIdentifier.GetMemberIdentifier("value"), this.valueParameter = new _ValueParameter(this));
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
