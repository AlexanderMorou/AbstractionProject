using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{

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
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.Primitive_NonStringObject);
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
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.Primitive_Invalid);
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
                if (value != null && !(value is T))
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.value), value.GetType().ToString(), typeof(T).ToString());
                    //throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Must be of type T ({0}).", typeof(T)));
                this.value = (T)value;
            }
        }

        public PrimitiveType PrimitiveType
        {
            get { return this.primitiveType; }
        }

        #endregion

        #region IExpression Members
        
        public override ExpressionKind Type
        {
            get {
                switch (this.PrimitiveType)
                {
                    case PrimitiveType.Boolean:
                        return ExpressionKind.PrimitiveBooleanInsert;
                    case PrimitiveType.Byte:
                        return ExpressionKind.PrimitiveByteInsert;
                    case PrimitiveType.SByte:
                        return ExpressionKind.PrimitiveSByteInsert;
                    case PrimitiveType.Int16:
                        return ExpressionKind.PrimitiveInt16Insert;
                    case PrimitiveType.UInt16:
                        return ExpressionKind.PrimitiveUInt16Insert;
                    case PrimitiveType.Int32:
                        return ExpressionKind.PrimitiveInt32Insert;
                    case PrimitiveType.UInt32:
                        return ExpressionKind.PrimitiveUInt32Insert;
                    case PrimitiveType.Int64:
                        return ExpressionKind.PrimitiveInt64Insert;
                    case PrimitiveType.UInt64:
                        return ExpressionKind.PrimitiveUInt64Insert;
                    case PrimitiveType.Decimal:
                        return ExpressionKind.PrimitiveDecimalInsert;
                    case PrimitiveType.Float:
                        return ExpressionKind.PrimitiveSingleInsert;
                    case PrimitiveType.Double:
                        return ExpressionKind.PrimitiveDoubleInsert;
                    case PrimitiveType.Char:
                        return ExpressionKind.PrimitiveCharInsert;
                    case PrimitiveType.String:
                        return ExpressionKind.PrimitiveStringInsert;
                }
                throw new InvalidOperationException();
                //ToDo: Add series of conditions to relate the proper ExpressionKind.
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
            this.Visit((IPrimitiveVisitor)visitor);
        }

        #region IPrimitiveExpression Members

        public void Visit(IPrimitiveVisitor visitor)
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
