﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Globalization;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateEnumType
    {
        /// <summary>
        /// Provides a base field member for an intermediate enumeration.
        /// </summary>
        [DebuggerDisplay("{Name} = {Value},")]
        protected sealed partial class FieldMember :
            IntermediateMemberBase<IEnumType, IIntermediateEnumType>,
            IIntermediateEnumFieldMember
        {
            private IIntermediateEnumFieldValue value;
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

            #region IInstanceMember Members

            public InstanceMemberFlags InstanceFlags
            {
                get { return InstanceMemberFlags.None; }
            }

            public bool IsHideBySignature
            {
                get { return false; }
            }

            public bool IsStatic
            {
                get { return false; }
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
                    EnumerationBaseType parentBaseType = this.Parent.BaseType;
                    switch (parentBaseType)
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
                        case EnumerationBaseType.Default:
                        case EnumerationBaseType.Int32:
                        default:
                            return typeof(int).GetTypeReference();
                    }
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            #endregion

        }
    }
}