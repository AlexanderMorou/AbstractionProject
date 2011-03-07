using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
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

        protected override EventMethodMember GetMethodMember(IntermediateEventMethodType type)
        {
            return new EventMethodMember(this, type);
        }

        public class EventMethodMember :
            IntermediateClassMethodMember<TInstanceIntermediateType>,
            IIntermediateEventMethodMember
        {
            private IntermediateClassEventMember<TInstanceIntermediateType> parent;
            private IntermediateEventMethodType methodType;
            public EventMethodMember(IntermediateClassEventMember<TInstanceIntermediateType> parent, IntermediateEventMethodType methodType)
                : base((TInstanceIntermediateType)parent.Parent)
            {
                this.parent = parent;
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
    }

    public class IntermediateClassCtorMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.ConstructorMember,
        IIntermediateClassCtorMember
        where TInstanceIntermediateType :
            IntermediateClassType<TInstanceIntermediateType>
    {
        public IntermediateClassCtorMember(TInstanceIntermediateType parent)
            : base(parent)
        {

        }

        public override IIntermediateAssembly Assembly
        {
            get { return Parent.Assembly; }
        }
    }

    public class IntermediateClassIndexerMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.IndexerMember
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
        
        public class IndexerSetMethodMember :
            IndexerMethodMember,
            IIntermediatePropertySetMethodMember
        {
            private IIntermediateMethodParameterMember valueParameter;

            public IndexerSetMethodMember(IntermediateClassIndexerMember<TInstanceIntermediateType> owner)
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
            IntermediateClassMethodMember<TInstanceIntermediateType>,
            IIntermediatePropertyMethodMember
        {
            private PropertyMethodType methodType;
            private IntermediateClassIndexerMember<TInstanceIntermediateType> owner;
            public IndexerMethodMember(IntermediateClassIndexerMember<TInstanceIntermediateType> owner, PropertyMethodType methodType)
                : base(owner == null ? null : (TInstanceIntermediateType)owner.Parent)
            {
                if (owner == null)
                    throw new ArgumentNullException("parent");
                this.owner = owner;
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
                result.Lock();
                return result;
            }
        }

        protected override IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember GetMethodMember(PropertyMethodType methodType)
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
            private IIntermediateMethodParameterMember valueParameter;

            public PropertySetMethodMember(IntermediateClassPropertyMember<TInstanceIntermediateType> owner, TInstanceIntermediateType parent)
                : base(PropertyMethodType.SetMethod,owner, parent)
            {
            }

            #region IIntermediatePropertySetMethodMember Members

            public IIntermediateMethodParameterMember ValueParameter
            {
                get {
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
        public class PropertyMethodMember :
            IntermediateClassMethodMember<TInstanceIntermediateType>,
            IIntermediatePropertyMethodMember
        {
            private PropertyMethodType methodType;
            private IntermediateClassPropertyMember<TInstanceIntermediateType> owner;
            public PropertyMethodMember(PropertyMethodType methodType, IntermediateClassPropertyMember<TInstanceIntermediateType> owner, TInstanceIntermediateType parent)
                : base(parent)
            {
                this.methodType = methodType;
                this.owner = owner;
            }

            protected IntermediateClassPropertyMember<TInstanceIntermediateType> Owner
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

            protected override IntermediateParameterMemberDictionary<IClassMethodMember, IIntermediateClassMethodMember, IMethodParameterMember<IClassMethodMember, IClassType>, IIntermediateMethodParameterMember<IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType>> InitializeParameters()
            {
                var result =  base.InitializeParameters();
                result.Lock();
                return result;
            }

        }

        protected override IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember GetMethodMember(PropertyMethodType methodType)
        {
            switch (methodType)
            {
                case PropertyMethodType.GetMethod:
                    return new IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember(methodType, this, (TInstanceIntermediateType)this.Parent);
                case PropertyMethodType.SetMethod:
                    return new IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertySetMethodMember(this, (TInstanceIntermediateType)this.Parent);
            }
            throw new ArgumentOutOfRangeException("methodType");
        }
    }

    public class IntermediateClassFieldMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.FieldMember,
        IIntermediateClassFieldMember
        where TInstanceIntermediateType :
            IntermediateClassType<TInstanceIntermediateType>
    {
        public IntermediateClassFieldMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
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
        /// Data member for <see cref="InstanceFlags"/>.
        /// </summary>
        private ExtendedInstanceMemberFlags instanceFlags;
        /// <summary>
        /// Data member for <see cref="IsExtensionMethod"/>.
        /// </summary>
        private bool isExtensionMethod;
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
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> is asynchronous.
        /// </summary>
        public bool IsAsync { get; set; }

        /// <summary>
        /// Returns/sets whether the current <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>
        /// is an extension method.
        /// </summary>
        public bool IsExtensionMethod
        {
            get
            {
                return this.isExtensionMethod;
            }
            set
            {
                this.isExtensionMethod = value;
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
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> is
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
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>
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
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> 
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
        /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>
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
                if (this.IsHideBySignature == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.HideBySignature;
                else
                    this.instanceFlags &= ~ExtendedInstanceMemberFlags.HideBySignature;
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
                if (Parent.SpecialModifier != SpecialClassModifier.None)
                    return true;
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
            get { return (InstanceMemberFlags)(this.instanceFlags & (ExtendedInstanceMemberFlags)(InstanceMemberFlags.InstanceMemberFlagsMask)); }
        }

        #endregion

        #region IExtendedInstanceMember Members

        /// <summary>
        /// Returns the <see cref="ExtendedInstanceMemberFlags"/> that determine how the
        /// <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> is shown in its scope and inherited 
        /// scopes.
        /// </summary>
        public ExtendedInstanceMemberFlags InstanceFlags
        {
            get { return this.instanceFlags; }
        }

        #endregion

        #region IClassMethodMember Members

        /// <summary>
        /// Returns the base definition of a virtual method that is an override
        /// of the original.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>
        /// is not an overridden member.</exception>
        public IClassMethodMember BaseDefinition
        {
            get {
                if (!this.IsOverride)
                    throw new InvalidOperationException();
                for (IClassType p = this.Parent; p != null; p = p.BaseType)
                    foreach (var methodMember in p.Methods.Values)
                        if (methodMember.Name != this.Name ||
                            methodMember.Parameters.Count != this.Parameters.Count ||
                            methodMember.TypeParameters.Count != this.TypeParameters.Count)
                            continue;
                        else
                        {
                            bool match = true;
                            /* *
                             * For the sake of this find operation,
                             * the type-parameters will be mostly ignored to 
                             * ensure that the base declaration is found.
                             * *
                             * If the signature matches, it's a valid override;
                             * however, if the constraints upon the type-parameter
                             * don't match, then that's the compiler's domain to
                             * notify.
                             * */
                            for (int i = 0; i < this.Parameters.Count; i++)
                            {
                                /* *
                                 * Var variable declaration is helpful for 
                                 * cases like this.
                                 * */
                                var targetParam = methodMember.Parameters.Values[i];
                                var sourceParam = this.Parameters.Values[i];
                                if (targetParam.Direction != sourceParam.Direction)
                                {
                                    match = false;
                                    break;
                                }
                                else if (targetParam.ParameterType.IsGenericTypeParameter)
                                {
                                    /* *
                                     * Rewrite this code so that when the source parameter
                                     * is a generic parameter, and its parent is the enclosing
                                     * type, that the newly declared version is equal to the
                                     * generic parameter defined in the inheritance chain.
                                     * */
                                    if (!sourceParam.ParameterType.IsGenericTypeParameter)
                                    {
                                        match = false;
                                        break;
                                    }

                                    IGenericParameter sourceTParam = (IGenericParameter)sourceParam.ParameterType,
                                                      targetTParam = (IGenericParameter)targetParam.ParameterType;
                                    if (targetTParam.Parent == methodMember)
                                    {
                                        if (sourceTParam.Parent != this)
                                        {
                                            match = false;
                                            break;
                                        }
                                        match = sourceTParam.Position == targetTParam.Position;
                                    }
                                    else if (targetTParam.Parent == this.Parent)
                                    {
                                        if (sourceTParam.Parent != this.Parent)
                                        {
                                            match = false;
                                            break;
                                        }
                                        match = sourceTParam.Position == targetTParam.Position;
                                    }
                                }
                                else
                                    match = targetParam.ParameterType.Equals(sourceParam.ParameterType);
                                if (!match)
                                    break;
                            }
                            if (match)
                                return methodMember;
                        }
                throw new InvalidOperationException("match not found");
            }
        }

        #endregion

        protected override IClassMethodMember OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
        {
            return new _ClassTypeBase._MethodsBase._Method(this, genericReplacements);
        }
    }

}
