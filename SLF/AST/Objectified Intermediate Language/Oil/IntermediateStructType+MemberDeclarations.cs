﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Oil
{
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
            public EventMethodMember(IntermediateStructEventMember<TInstanceIntermediateType> parent, IntermediateEventMethodType methodType)
                : base((TInstanceIntermediateType)parent.Parent)
            {

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

        }

        protected override IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember GetMethodMember(IntermediateEventMethodType type)
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

        public class IndexerMethodMember :
            IntermediateStructMethodMember<TInstanceIntermediateType>,
            IIntermediatePropertyMethodMember
        {
            private PropertyMethodType methodType;
            private IntermediateStructIndexerMember<TInstanceIntermediateType> parent;
            public IndexerMethodMember(IntermediateStructIndexerMember<TInstanceIntermediateType> parent, PropertyMethodType methodType)
                : base(parent == null ? null : (TInstanceIntermediateType)parent.Parent)
            {
                if (parent == null)
                    throw new ArgumentNullException("parent");
                this.parent = parent;
                this.methodType = methodType;
            }

            protected override string OnGetName()
            {
                switch (this.methodType)
                {
                    case PropertyMethodType.GetMethod:
                        return string.Format("get_{0}", this.parent.Name);
                    case PropertyMethodType.SetMethod:
                        return string.Format("set_{0}", this.parent.Name);
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
            
        }


        protected override IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember GetMethodMember(PropertyMethodType methodType)
        {
            return new IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember(this, methodType);
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
        private ExtendedInstanceMemberFlags instanceFlags;
        /// <summary>
        /// Data member for <see cref="IsExtensionMethod"/>.
        /// </summary>
        private bool isExtensionMethod;

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
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Abstract) == ExtendedInstanceMemberFlags.Abstract);
            }
            set
            {
                if (this.IsAbstract == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Abstract;
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
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Final) == ExtendedInstanceMemberFlags.Final);
            }
            set
            {
                if (this.IsFinal == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Final;
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
                if (this.IsOverride == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Override;
                else
                    this.instanceFlags &= ~ExtendedInstanceMemberFlags.Override;
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
                return IsExplicitStatic;
            }
            set
            {
                if (this.IsStatic == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Static;
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
            get
            {
                return (InstanceMemberFlags)(this.instanceFlags & (ExtendedInstanceMemberFlags)(InstanceMemberFlags.InstanceMemberFlagsMask));
            }
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
            get
            {
                return this.instanceFlags;
            }
        }

        #endregion

        protected override IStructMethodMember OnMakeGenericMethod(ITypeCollection genericReplacements)
        {
            return new _StructTypeBase._MethodsBase._Method(this, genericReplacements);
        }

        public override IIntermediateAssembly Assembly
        {
            get { return this.Parent.Assembly; }
        }
    }

    public class IntermediateStructPropertyMember<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, IIntermediateStructEventMember, IntermediateStructEventMember<TInstanceIntermediateType>.EventMethodMember, IStructFieldMember, IIntermediateStructFieldMember, IStructIndexerMember, IIntermediateStructIndexerMember, IntermediateStructIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IStructType, IIntermediateStructType, TInstanceIntermediateType>.PropertyMember,
        IIntermediateStructPropertyMember
        where TInstanceIntermediateType :
            IntermediateStructType<TInstanceIntermediateType>
    {
        public IntermediateStructPropertyMember(string name, TInstanceIntermediateType parent)
            : base(name, parent)
        {
        }

        public IntermediateStructPropertyMember(TInstanceIntermediateType parent)
            : base(parent)
        {
        }

        public class PropertyMethodMember :
            IntermediateStructMethodMember<TInstanceIntermediateType>,
            IIntermediatePropertyMethodMember
        {
            private PropertyMethodType methodType;
            private IntermediateStructPropertyMember<TInstanceIntermediateType> owner;
            public PropertyMethodMember(PropertyMethodType methodType, IntermediateStructPropertyMember<TInstanceIntermediateType> owner, TInstanceIntermediateType parent)
                : base(parent)
            {
                this.methodType = methodType;
                this.owner = owner;
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
        }

        protected override IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember GetMethodMember(PropertyMethodType methodType)
        {
            return new IntermediateStructPropertyMember<TInstanceIntermediateType>.PropertyMethodMember(methodType, this, (TInstanceIntermediateType)this.Parent);
        }
    }
}