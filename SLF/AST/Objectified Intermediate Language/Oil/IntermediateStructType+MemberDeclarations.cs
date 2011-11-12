using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
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
            private IntermediateEventMethodType methodType;
            private IntermediateStructEventMember<TInstanceIntermediateType> parent;
            public EventMethodMember(IntermediateStructEventMember<TInstanceIntermediateType> parent, IntermediateEventMethodType methodType)
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

            public IntermediateEventMethodType MethodType
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

        protected override IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember GetMethodMember(IntermediateEventMethodType type)
        {
            return new EventMethodMember(this, type);
        }
    }
    public class IntermediateStructIndexerMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.IndexerMember
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
            private IIntermediateMethodParameterMember valueParameter;

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
                        valueParameter = this.Parameters._Add(new TypedName("value", this.Owner.PropertyType));
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
                result.Lock();
                return result;
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
                if (returnType == CommonTypeRefs.Void)
                {
                    if (this.Name.Substring(this.Name.Length - 5).ToLower() == "async")
                        this.IsAsynchronousCandidate = true;
                }
                else if (returnType == CommonTypeRefs.Task)
                {
                    this.IsAsynchronousCandidate = true;
                }
                else if (returnType.ElementClassification == TypeElementClassification.GenericTypeDefinition && returnType.ElementType != null && returnType.ElementType == CommonTypeRefs.TaskOfT)
                {
                    this.IsAsynchronousCandidate = true;
                }
                else
                {
                    this.IsAsynchronousCandidate = false;
                }
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

        protected override IStructMethodMember OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
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
            private IIntermediateMethodParameterMember valueParameter;

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

            protected override IntermediateParameterMemberDictionary<IStructMethodMember, IIntermediateStructMethodMember, IMethodParameterMember<IStructMethodMember, IStructType>, IIntermediateMethodParameterMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>> InitializeParameters()
            {
                var result = base.InitializeParameters();
                this.valueParameter = result._Add(new TypedName("value", this.Owner.PropertyType));
                return result;
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
