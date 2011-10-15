using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    [EditorBrowsable(EditorBrowsableState.Always)]
    public partial class IntermediateEnumType :
        IntermediateTypeBase<IGeneralTypeUniqueIdentifier, IEnumType, IIntermediateEnumType>,
        IIntermediateEnumType
    {
        private FieldMemberDictionary fields;
        private IntermediateFullMemberDictionary members;
        /// <summary>
        /// Creates a new <see cref="IntermediateEnumType"/> with the
        /// <paramref name="name"/> and <paramref name="parent"/> 
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value denoting
        /// the unique name of the <see cref="IntermediateEnumType"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which
        /// contains the <see cref="IntermediateEnumType"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// or <paramref name="parent"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is
        /// <see cref="String.Empty"/>.</exception>
        protected internal IntermediateEnumType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
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

        /// <summary>
        /// Returns/sets the <see cref="EnumerationBaseType"/> for the 
        /// <see cref="IIntermediateEnumType"/>.
        /// </summary>
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

        /// <summary>
        /// Returns whether the current type is a generic
        /// type with generic parameters.
        /// </summary>
        public override bool IsGenericConstruct
        {
            get
            {

                if (this.Parent is IIntermediateDeclaration)
                    return ((IIntermediateDeclaration)(this.Parent)).IsDeclarationGenericConstruct();
                return false;
            }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            ICompiledType compiledType = other as ICompiledType;
            if (compiledType != null &&
                ((compiledType.Equals(CommonTypeRefs.Enum)) ||
                 (compiledType.Equals(CommonTypeRefs.ValueType)) ||
                 (compiledType.Equals(CommonTypeRefs.Object))))
                return true;
            return false;
        }

        /// <summary>
        /// Implementation version of <see cref="TypeBase.BaseType"/> which 
        /// returns the base type of the current <see cref="IntermediateEnumType"/>
        /// </summary>
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

        public override void Visit(IIntermediateTypeVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Returns a series of string values which relate to the 
        /// identifiers contained within the <see cref="IntermediateEnumType"/>.
        /// </summary>
        public override IEnumerable<string> AggregateIdentifiers
        {
            get {
                return from f in this.Fields.Values
                       select f.Name;
            }
        }
    }
}
