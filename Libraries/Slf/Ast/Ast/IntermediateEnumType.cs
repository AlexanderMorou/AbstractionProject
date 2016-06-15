using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System.ComponentModel;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    [EditorBrowsable(EditorBrowsableState.Always)]
    public partial class IntermediateEnumType :
        IntermediateTypeBase<IGeneralTypeUniqueIdentifier, IEnumType, IIntermediateEnumType>,
        IIntermediateEnumType
    {
        private IGeneralTypeUniqueIdentifier uniqueIdentifier;
        private FieldMemberDictionary fields;
        private IntermediateFullMemberDictionary members;
        private EnumerationBaseType? valueType;
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
            lock (this.SyncObject)
                if (this.fields == null)
                    this.fields = new FieldMemberDictionary(this._Members, this);
        }

        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                if (this.fields == null)
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
        public new EnumerationBaseType ValueType
        {
            get
            {
                if (this.valueType == null)
                {
                    if (this.Fields.Count == 0)
                        return EnumerationBaseType.Int32;
                    PrimitiveType maxPrimType = PrimitiveType.Null;
                    foreach (var element in this.Fields.Values)
                    {
                        if (element.Value.ValueType == EnumValueType.Constant)
                        {
                            var primitiveValue = (IPrimitiveExpression)element.Value;
                            if ((int)primitiveValue.PrimitiveType > (int)maxPrimType)
                                maxPrimType = primitiveValue.PrimitiveType;
                        }
                    }
                    switch (maxPrimType)
                    {
                        case PrimitiveType.Byte:
                            return EnumerationBaseType.Byte;
                        case PrimitiveType.SByte:
                            return EnumerationBaseType.SByte;
                        case PrimitiveType.Int16:
                            return EnumerationBaseType.Int16;
                        case PrimitiveType.UInt16:
                            return EnumerationBaseType.UInt16;
                        case PrimitiveType.Int32:
                            return EnumerationBaseType.Int32;
                        case PrimitiveType.UInt32:
                            return EnumerationBaseType.UInt32;
                        case PrimitiveType.Int64:
                            return EnumerationBaseType.Int64;
                        case PrimitiveType.UInt64:
                            return EnumerationBaseType.UInt64;
                        default:
                            return EnumerationBaseType.Default;
                    }
                }
                else
                    return this.valueType.Value;
            }
            set
            {
                this.valueType = value;
            }
        }

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
            get { return TypeKind.Enumeration; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }

        protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
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
            return other.Equals(IdentityManager.ObtainTypeReference(RuntimeCoreType.RootEnum)) ||
                   other.Equals(IdentityManager.ObtainTypeReference(RuntimeCoreType.RootStruct)) ||
                   other.Equals(IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType));
        }

        /// <summary>
        /// Implementation version of <see cref="TypeBase{TIdentifier}.BaseType"/> which 
        /// returns the base type of the current <see cref="IntermediateEnumType"/>
        /// </summary>
        protected override IType BaseTypeImpl
        {
            get
            {
                switch (this.ValueType)
                {
                    case EnumerationBaseType.SByte:
                        return IdentityManager.ObtainTypeReference(RuntimeCoreType.SByte);
                    case EnumerationBaseType.Byte:
                        return IdentityManager.ObtainTypeReference(RuntimeCoreType.Byte);
                    case EnumerationBaseType.Int16:
                        return IdentityManager.ObtainTypeReference(RuntimeCoreType.Int16);
                    case EnumerationBaseType.UInt16:
                        return IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt16);
                    case EnumerationBaseType.UInt32:
                        return IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt32);
                    case EnumerationBaseType.Int64:
                        return IdentityManager.ObtainTypeReference(RuntimeCoreType.Int64);
                    case EnumerationBaseType.UInt64:
                        return IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt64);
                    case EnumerationBaseType.Int32:
                        return IdentityManager.ObtainTypeReference(RuntimeCoreType.Int32);
                    /* *
                     * Use Enum in other cases where the base-type
                     * is not specified, since the type is language
                     * defined.
                     * */
                    case EnumerationBaseType.Default:
                    default:
                        return IdentityManager.ObtainTypeReference(RuntimeCoreType.RootEnum);
                }
            }
        }

        public override void Accept(IIntermediateTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return for the visitor.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <param name="visitor">The <see cref="IIntermediateTypeVisitor"/> to
        /// receive the <see cref="IntermediateEnumType"/> as a visitor.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        public override TResult Accept<TResult, TContext>(IIntermediateTypeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        /// <summary>
        /// Returns a series of string values which relate to the 
        /// identifiers contained within the <see cref="IntermediateEnumType"/>.
        /// </summary>
        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get
            {
                if (this.fields == null)
                    if (this.fields == null)
                        return TypeBase<IGeneralTypeUniqueIdentifier>.EmptyIdentifiers;
                return this.Fields.Keys;
            }
        }

        protected override IGeneralTypeUniqueIdentifier OnGetUniqueIdentifier()
        {
            lock (this.SyncObject)
                if (this.uniqueIdentifier == null)
                {
                    if (this.Parent is IType)
                        this.uniqueIdentifier = ((IType)this.Parent).UniqueIdentifier.GetNestedIdentifier(this.Name, 0);
                    else if (this.Parent is INamespaceDeclaration)
                        this.uniqueIdentifier = TypeSystemIdentifiers.GetTypeIdentifier(((INamespaceDeclaration)this.Parent).FullName, this.Name, 0);
                    else
                        this.uniqueIdentifier = TypeSystemIdentifiers.GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier)null, this.Name, 0);
                }
            return this.uniqueIdentifier;
        }

        protected override void OnIdentifierChanged(IGeneralTypeUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            if (this.uniqueIdentifier != null)
                this.uniqueIdentifier = null;
            base.OnIdentifierChanged(oldIdentifier, cause);
        }

        protected override IIntermediateIdentityManager OnGetIntermediateManager()
        {
            return this.Parent.IdentityManager;
        }

        protected override void ClearIdentifier()
        {
            this.uniqueIdentifier = null;
        }


        public bool HasFields
        {
            get { return this.fields != null && this.fields.Count > 0; }
        }

        public override bool HasMembers
        {
            get
            {
                return this.members != null && this.members.Count > 0;
            }
        }
    }
}
