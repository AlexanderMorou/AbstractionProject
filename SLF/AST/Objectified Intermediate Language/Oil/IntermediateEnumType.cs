﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public partial class IntermediateEnumType :
        IntermediateTypeBase<IEnumType, IIntermediateEnumType>,
        IIntermediateEnumType
    {
        private FieldMemberDictionary fields;
        private IntermediateFullMemberDictionary members;
        /// <summary>
        /// Creates a new <see cref="IntermediateEnumType"/> with the
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which
        /// contains the <see cref="IntermediateEnumType"/>.</param>
        protected internal IntermediateEnumType(string name, IIntermediateTypeParent parent)
            : base(parent)
        {
            base.AssignName(name);
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateEnumFieldMemberDictionary"/>
        /// for the current <see cref="IntermediateEnumType"/>.
        /// </summary>
        public FieldMemberDictionary Fields
        {
            get
            {
                this.CheckFields();
                return this.fields;
            }
        }

        private void CheckFields()
        {
            if (this.fields == null)
                this.fields = new FieldMemberDictionary(this._Members, this);
        }

        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                if (this.members == null)
                    this.members = new IntermediateFullMemberDictionary();
                return this.members;
            }
        }

        #region IIntermediateEnumType Members

        public new EnumerationBaseType BaseType { get; set; }

        IIntermediateEnumFieldMemberDictionary IIntermediateEnumType.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        #region IFieldParent<IEnumFieldMember,IEnumType> Members

        IFieldMemberDictionary<IEnumFieldMember, IEnumType> IFieldParent<IEnumFieldMember, IEnumType>.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        protected override IIntermediateFullMemberDictionary OnGetIntermediateMembers()
        {
            this.CheckFields();
            return this._Members;
        }

        protected override bool Equals(IEnumType other)
        {
            return object.ReferenceEquals(other, this);
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Enumerator; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }

        public override bool IsGenericType
        {
            get
            {
                if (this.Parent is IIntermediateType)
                    return ((IIntermediateType)(this.Parent)).IsGenericType;
                return false;
            }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            ICompiledType compiledType = other as ICompiledType;
            if (compiledType != null &&
                ((compiledType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Enum)) ||
                 (compiledType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.ValueType)) ||
                 (compiledType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Object))))
                return true;
            return false;
        }

        protected override IType BaseTypeImpl
        {
            get
            {
                switch (this.BaseType)
                {
                    case EnumerationBaseType.SByte:
                        return typeof(sbyte).GetTypeReference();
                    case EnumerationBaseType.Byte:
                        return typeof(byte).GetTypeReference();
                    case EnumerationBaseType.Int16:
                        return typeof(short).GetTypeReference();
                    case EnumerationBaseType.UInt16:
                        return typeof(ushort).GetTypeReference();
                    case EnumerationBaseType.UInt32:
                        return typeof(uint).GetTypeReference();
                    case EnumerationBaseType.Int64:
                        return typeof(long).GetTypeReference();
                    case EnumerationBaseType.UInt64:
                        return typeof(ulong).GetTypeReference();
                    case EnumerationBaseType.Int32:
                        return typeof(int).GetTypeReference();
                    /* *
                     * Use Enum in other cases where the base-type
                     * is not specified, since the type is language
                     * defined.
                     * */
                    case EnumerationBaseType.Default:
                    default:
                        return typeof(Enum).GetTypeReference();
                }
            }
        }
    }
}