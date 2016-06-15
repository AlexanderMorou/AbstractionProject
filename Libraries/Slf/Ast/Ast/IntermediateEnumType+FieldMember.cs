using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateEnumType
    {
        /// <summary>
        /// Provides a base field member for an intermediate enumeration.
        /// </summary>
        [DebuggerDisplay("{Name,nq} = {Value},")]
        protected sealed partial class FieldMember :
            IntermediateMemberBase<IGeneralMemberUniqueIdentifier, IEnumType, IIntermediateEnumType>,
            IIntermediateEnumFieldMember
        {
            private IGeneralMemberUniqueIdentifier uniqueIdentifier;
            private IIntermediateEnumFieldValue value;
            private IMetadataDefinitionCollection metadata;
            private IMetadataCollection metadataBack;

            internal FieldMember(string name, IntermediateEnumType parent)
                : base(parent)
            {
                base.AssignName(name);
            }

            #region IIntermediateEnumFieldMember Members

            public IIntermediateEnumFieldValue Value
            {
                get
                {
                    if (this.value == null)
                        return IntermediateEnumFieldAutomaticValue.AutomaticValue;
                    else
                        return this.value;
                }
                set
                {
                    this.value = value;
                }
            }

            #endregion

            public override string ToString()
            {
                const string patternMainExt = "/* ({0}) */";
                const string patternMain = "{1}";
                const string patternValue = patternMain + " = {2} " + patternMainExt;
                const string patternAuto = patternMain + " /* Automatic ({0}) Value */";
                if (this.value == null)
                    return string.Format(CultureInfo.CurrentCulture, patternAuto, this.FieldType, this.Name);
                switch (this.value.ValueType)
                {
                    case EnumValueType.Constant:
                    case EnumValueType.Mixed:
                        return string.Format(CultureInfo.CurrentCulture, patternValue, this.FieldType, this.Name, this.Value);
                    default:
                    case EnumValueType.Automatic:
                        return string.Format(CultureInfo.CurrentCulture, patternAuto, this.FieldType, this.Name);
                }
            }

            #region IIntermediateFieldMember Members

            public IType FieldType
            {
                get
                {
                    EnumerationBaseType parentBaseType = this.Parent.ValueType;
                    switch (parentBaseType)
                    {
                        case EnumerationBaseType.SByte:
                            return this.Parent.Parent.IdentityManager.ObtainTypeReference(RuntimeCoreType.SByte);
                        case EnumerationBaseType.Byte:
                            return this.Parent.Parent.IdentityManager.ObtainTypeReference(RuntimeCoreType.Byte);
                        case EnumerationBaseType.Int16:
                            return this.Parent.Parent.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int16);
                        case EnumerationBaseType.UInt16:
                            return this.Parent.Parent.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt16);
                        case EnumerationBaseType.UInt32:
                            return this.Parent.Parent.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt32);
                        case EnumerationBaseType.Int64:
                            return this.Parent.Parent.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int64);
                        case EnumerationBaseType.UInt64:
                            return this.Parent.Parent.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt64);
                        case EnumerationBaseType.Default:
                        case EnumerationBaseType.Int32:
                        default:
                            return this.Parent.Parent.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int32);
                    }
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            /// <summary>
            /// Obtains a reference expression which refers to the current
            /// <see cref="FieldMember"/> with the <paramref name="source"/>
            /// that leads up to it.
            /// </summary>
            /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
            /// which leads up to the field.</param>
            /// <returns>A <see cref="IFieldReferenceExpression"/> which refers to the current
            /// <see cref="FieldMember"/> with the <paramref name="source"/>
            /// that leads up to it.</returns>
            public IFieldReferenceExpression GetReference(IMemberParentReferenceExpression source)
            {
                if (source != null)
                    throw new ArgumentOutOfRangeException("source");
                return new ReferenceExpression(this, this.Parent.GetTypeExpression());
            }

            #endregion

            public override void Accept(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }

            public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
            {
                return visitor.Visit(this, context);
            }

            protected override void ClearIdentifier()
            {
                lock (this.SyncObject)
                    this.uniqueIdentifier = null;
            }

            public override IGeneralMemberUniqueIdentifier UniqueIdentifier
            {
                get {
                    if (this.uniqueIdentifier == null)
                        this.uniqueIdentifier = TypeSystemIdentifiers.GetMemberIdentifier(this.Name);
                    return this.uniqueIdentifier; }
            }
            public IIntermediateIdentityManager IdentityManager
            {
                get { return this.Parent.IdentityManager; }
            }


            IMetadataCollection IMetadataEntity.Metadata
            {
                get
                {
                    lock (this.SyncObject)
                    {
                        if (this.metadataBack != null)
                            if (this.IsDisposed)
                                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                            else
                                this.metadataBack = ((MetadataDefinitionCollection)(this.Metadata)).GetWrapper();
                        return this.metadataBack;
                    }
                }
            }

            public IMetadataDefinitionCollection Metadata
            {
                get
                {
                    lock (this.SyncObject)
                    {
                        if (this.metadata == null)
                            if (this.IsDisposed)
                                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                            else
                                this.metadata = new MetadataDefinitionCollection(this, this.Parent.Assembly);
                        return this.metadata;
                    }
                }
            }

            public bool IsDefined(IType metadatumType)
            {
                return this.Metadata.Contains(metadatumType);
            }

            #region IFieldMember Members

            public bool ReadOnly
            {
                get { return false; }
            }

            public bool Constant
            {
                get { return true; }
            }

            #endregion

            #region IEnumFieldMember Members

            public FieldMemberAttributes Attributes
            {
                get { return FieldMemberAttributes.Constant; }
            }

            #endregion
        }
    }
}
