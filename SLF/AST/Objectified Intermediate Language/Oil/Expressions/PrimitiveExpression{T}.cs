using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// The kind of primitive the <see cref="IPrimitiveExpression{T}"/> is.
    /// </summary>
    public enum PrimitiveType
    {
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is a boolean value.
        /// </summary>
        Boolean,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is a byte value.
        /// </summary>
        Byte,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is a signed byte value.
        /// </summary>
        SByte,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is a signed
        /// 16-bit value.
        /// </summary>
        Int16,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is an unsigned
        /// 16-bit value.
        /// </summary>
        UInt16,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is a signed
        /// 32-bit value.
        /// </summary>
        Int32,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is an unsigned
        /// 32-bit value.
        /// </summary>
        UInt32,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is a signed
        /// 64-bit value.
        /// </summary>
        Int64,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is an unsigned
        /// 64-bit value.
        /// </summary>
        UInt64,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is decimal value.
        /// </summary>
        Decimal,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is single precision
        /// floating point value.
        /// </summary>
        Float,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is double precision
        /// floating point value.
        /// </summary>
        Double,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is a unicode character.
        /// </summary>
        Char,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is a string of characters.
        /// </summary>
        String,
        /// <summary>
        /// The <see cref="IPrimitiveExpression"/> is a primitive null value.
        /// </summary>
        Null
    }

    [DebuggerDisplay("{Value} /* {PrimitiveType} */")]
    public class PrimitiveExpression<T> :
        ExpressionBase,
        IPrimitiveExpression<T>
    {
        /// <summary>
        /// Data member for <see cref="Value"/>.
        /// </summary>
        private T value;
        /// <summary>
        /// Data member for <see cref="PrimitiveExpression{T}.PrimitiveType"/>.
        /// </summary>
        private PrimitiveType primitiveType;

        /// <summary>
        /// Creates a new <see cref="PrimitiveExpression{T}"/> with the <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The value that the <see cref="PrimitiveExpression{T}"/>
        /// represents.</param>
        internal PrimitiveExpression(T value)
        {
            Type t = typeof(T);
            if (t.IsClass)
                if (((object)(value)) == null)
                    throw new ArgumentNullException("value");
            switch (global::System.Type.GetTypeCode(t))
            {
                case TypeCode.Boolean:
                    this.primitiveType = PrimitiveType.Boolean;
                    break;
                case TypeCode.Byte:
                    this.primitiveType = PrimitiveType.Byte;
                    break;
                case TypeCode.Char:
                    this.primitiveType = PrimitiveType.Char;
                    break;
                case TypeCode.Decimal:
                    this.primitiveType = PrimitiveType.Decimal;
                    break;
                case TypeCode.Double:
                    this.primitiveType = PrimitiveType.Double;
                    break;
                case TypeCode.Int16:
                    this.primitiveType = PrimitiveType.Int16;
                    break;
                case TypeCode.Int32:
                    this.primitiveType = PrimitiveType.Int32;
                    break;
                case TypeCode.Int64:
                    this.primitiveType = PrimitiveType.Int64;
                    break;
                case TypeCode.Object:
                    throw new ArgumentException("Cannot have object primitives of anything other than string.", "value");
                case TypeCode.SByte:
                    this.primitiveType = PrimitiveType.SByte;
                    break;
                case TypeCode.Single:
                    this.primitiveType = PrimitiveType.Float;
                    break;
                case TypeCode.String:
                    this.primitiveType = PrimitiveType.String;
                    break;
                case TypeCode.UInt16:
                    this.primitiveType = PrimitiveType.UInt16;
                    break;
                case TypeCode.UInt32:
                    this.primitiveType = PrimitiveType.UInt32;
                    break;
                case TypeCode.UInt64:
                    this.primitiveType = PrimitiveType.UInt64;
                    break;
                default:
                    throw new ArgumentException("Cannot have primitive values of any other type than listed in the PrimitiveType enum.", "value");
            }
            this.value = value;
        }

        #region IPrimitiveExpression<T> Members

        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        #endregion

        #region IPrimitiveExpression Members

        object IPrimitiveExpression.Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (!(value is T))
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Must be of type T ({0}).", typeof(T)));
                this.value = (T)value;
            }
        }

        public PrimitiveType PrimitiveType
        {
            get { return this.primitiveType; }
        }

        #endregion

        #region IExpression Members
        
        public override ExpressionKinds Type
        {
            get {
                switch (this.PrimitiveType)
                {
                    case PrimitiveType.Boolean:
                        return ExpressionKinds.PrimitiveBooleanInsert;
                    case PrimitiveType.Byte:
                        return ExpressionKinds.PrimitiveByteInsert;
                    case PrimitiveType.SByte:
                        return ExpressionKinds.PrimitiveSByteInsert;
                    case PrimitiveType.Int16:
                        return ExpressionKinds.PrimitiveInt16Insert;
                    case PrimitiveType.UInt16:
                        return ExpressionKinds.PrimitiveUInt16Insert;
                    case PrimitiveType.Int32:
                        return ExpressionKinds.PrimitiveInt32Insert;
                    case PrimitiveType.UInt32:
                        return ExpressionKinds.PrimitiveUInt32Insert;
                    case PrimitiveType.Int64:
                        return ExpressionKinds.PrimitiveInt64Insert;
                    case PrimitiveType.UInt64:
                        return ExpressionKinds.PrimitiveUInt64Insert;
                    case PrimitiveType.Decimal:
                        return ExpressionKinds.PrimitiveDecimalInsert;
                    case PrimitiveType.Float:
                        return ExpressionKinds.PrimitiveSingleInsert;
                    case PrimitiveType.Double:
                        return ExpressionKinds.PrimitiveDoubleInsert;
                    case PrimitiveType.Char:
                        return ExpressionKinds.PrimitiveCharInsert;
                    case PrimitiveType.String:
                        return ExpressionKinds.PrimitiveStringInsert;
                }
                throw new InvalidOperationException();
                //ToDo: Add series of conditions to relate the proper ExpressionKinds.
            }
        }

        #endregion

        public override string ToString()
        {
            switch (this.PrimitiveType)
            {
                case PrimitiveType.Boolean:
                    return string.Format("{0}", value);
                case PrimitiveType.Byte:
                    return string.Format("((byte)({0}))", value);
                case PrimitiveType.SByte:
                    return string.Format("((sbyte)({0}))", value);
                case PrimitiveType.Int16:
                    return string.Format("((short)({0}))", value);
                case PrimitiveType.UInt16:
                    return string.Format("((ushort)({0}))", value);
                case PrimitiveType.Int32:
                    return string.Format("{0}", value);
                case PrimitiveType.UInt32:
                    return string.Format("{0}U", value);
                case PrimitiveType.Int64:
                    return string.Format("{0}L", value);
                case PrimitiveType.UInt64:
                    return string.Format("{0}UL", value);
                case PrimitiveType.Decimal:
                    return string.Format("{0}M", value);
                case PrimitiveType.Float:
                    return string.Format("{0}F", value);
                case PrimitiveType.Double:
                    return string.Format("{0}D", value);
                case PrimitiveType.Char:
                    return string.Format("'{0}'", this.value);
                case PrimitiveType.String:
                    return ((string)(object)(this.value)).EscapeStringOrCharCILAndCS();
                case PrimitiveType.Null:
                    return "<null>";
                default:
                    return this.value.ToString();
            }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            this.Visit((IIntermediatePrimitiveVisitor)visitor);
        }

        #region IPrimitiveExpression Members

        public void Visit(IIntermediatePrimitiveVisitor visitor)
        {
            switch (this.PrimitiveType)
            {
                case PrimitiveType.Boolean:
                    visitor.Visit((IPrimitiveExpression<bool>)this);
                    break;
                case PrimitiveType.Byte:
                    visitor.Visit((IPrimitiveExpression<byte>)this);
                    break;
                case PrimitiveType.SByte:
                    visitor.Visit((IPrimitiveExpression<sbyte>)this);
                    break;
                case PrimitiveType.Int16:
                    visitor.Visit((IPrimitiveExpression<short>)this);
                    break;
                case PrimitiveType.UInt16:
                    visitor.Visit((IPrimitiveExpression<ushort>)this);
                    break;
                case PrimitiveType.Int32:
                    visitor.Visit((IPrimitiveExpression<int>)this);
                    break;
                case PrimitiveType.UInt32:
                    visitor.Visit((IPrimitiveExpression<uint>)this);
                    break;
                case PrimitiveType.Int64:
                    visitor.Visit((IPrimitiveExpression<long>)this);
                    break;
                case PrimitiveType.UInt64:
                    visitor.Visit((IPrimitiveExpression<ulong>)this);
                    break;
                case PrimitiveType.Decimal:
                    visitor.Visit((IPrimitiveExpression<decimal>)this);
                    break;
                case PrimitiveType.Float:
                    visitor.Visit((IPrimitiveExpression<float>)this);
                    break;
                case PrimitiveType.Double:
                    visitor.Visit((IPrimitiveExpression<double>)this);
                    break;
                case PrimitiveType.Char:
                    visitor.Visit((IPrimitiveExpression<char>)this);
                    break;
                case PrimitiveType.String:
                    visitor.Visit((IPrimitiveExpression<string>)this);
                    break;
                case PrimitiveType.Null:
                    visitor.VisitNull();
                    break;
                default:
                    return;
            }
        }

        #endregion
    }
}
