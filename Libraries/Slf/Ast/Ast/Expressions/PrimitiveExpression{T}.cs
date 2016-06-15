using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
                    if (value is IType)
                        this.primitiveType = PrimitiveType.Type;
                    else
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.Primitive_NonStringObject);
                    break;
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
                    return string.Format("((byte)(0x{0,2:X}))", value).Replace(' ', '0');
                case PrimitiveType.SByte:
                    return string.Format("((sbyte)({0}))", value);
                case PrimitiveType.Int16:
                    return string.Format("((short)({0}))", value);
                case PrimitiveType.UInt16:
                    return string.Format("((ushort)(0x{0,4:X}))", value).Replace(' ', '0');
                case PrimitiveType.Int32:
                    return string.Format("{0}", value);
                case PrimitiveType.UInt32:
                    return string.Format("0x{0,8:X}U", value).Replace(' ', '0');
                case PrimitiveType.Int64:
                    return string.Format("{0}L", value);
                case PrimitiveType.UInt64:
                    return string.Format("0x{0,16:X}UL", value).Replace(' ', '0');
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

        public override void Accept(IExpressionVisitor visitor)
        {
            this.Accept((IPrimitiveVisitor)visitor);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return this.Visit((IPrimitiveVisitor<TResult, TContext>)visitor, context);
        }

        #region IPrimitiveExpression Members

        public TResult Visit<TResult, TContext>(IPrimitiveVisitor<TResult, TContext> visitor, TContext context)
        {
            switch (this.PrimitiveType)
            {
                case PrimitiveType.Boolean:
                    return visitor.Visit((IPrimitiveExpression<bool>)this, context);
                case PrimitiveType.Byte:
                    return visitor.Visit((IPrimitiveExpression<byte>)this, context);
                case PrimitiveType.SByte:
                    return visitor.Visit((IPrimitiveExpression<sbyte>)this, context);
                case PrimitiveType.Int16:
                    return visitor.Visit((IPrimitiveExpression<short>)this, context);
                case PrimitiveType.UInt16:
                    return visitor.Visit((IPrimitiveExpression<ushort>)this, context);
                case PrimitiveType.Int32:
                    return visitor.Visit((IPrimitiveExpression<int>)this, context);
                case PrimitiveType.UInt32:
                    return visitor.Visit((IPrimitiveExpression<uint>)this, context);
                case PrimitiveType.Int64:
                    return visitor.Visit((IPrimitiveExpression<long>)this, context);
                case PrimitiveType.UInt64:
                    return visitor.Visit((IPrimitiveExpression<ulong>)this, context);
                case PrimitiveType.Decimal:
                    return visitor.Visit((IPrimitiveExpression<decimal>)this, context);
                case PrimitiveType.Float:
                    return visitor.Visit((IPrimitiveExpression<float>)this, context);
                case PrimitiveType.Double:
                    return visitor.Visit((IPrimitiveExpression<double>)this, context);
                case PrimitiveType.Char:
                    return visitor.Visit((IPrimitiveExpression<char>)this, context);
                case PrimitiveType.String:
                    return visitor.Visit((IPrimitiveExpression<string>)this, context);
                case PrimitiveType.Null:
                    return visitor.VisitNull(context);
                default:
                    return default(TResult);
                case Abstract.PrimitiveType.Type:
                    return visitor.Visit((IPrimitiveExpression<IType>)this, context);
            }
        }

        public void Accept(IPrimitiveVisitor visitor)
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
                case Abstract.PrimitiveType.Type:
                    visitor.Visit((IPrimitiveExpression<IType>)this);
                    break;
                default:
                    return;
            }
        }

        #endregion
    }

    public class PrimitiveRepresentationExpression<T> :
        ExpressionBase
    {
        private IExpression _representation;

        public PrimitiveRepresentationExpression(IExpression representation) { this._representation = representation; }
        internal PrimitiveRepresentationExpression() { }

        public static implicit operator PrimitiveRepresentationExpression<T>(T input)
        {
            return new PrimitiveRepresentationExpression<T>(new PrimitiveExpression<T>(input));
        }

        public override ExpressionKind Type
        {
            get { return ExpressionKind.PrimitiveRepresentation; }
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            if (this._representation != null)
                this._representation.Accept(visitor);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            if (this._representation != null)
                return this._representation.Accept(visitor, context);
            return default(TResult);
        }

        public override string ToString()
        {
            return this._representation == null ? string.Empty : this._representation.ToString();
        }
    }
}
