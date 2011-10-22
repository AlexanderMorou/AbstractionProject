using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// The kind of action associated to an <see cref="IExpression"/>.
    /// </summary>
    public enum ExpressionKind :
        ulong
    {
        /// <summary>
        /// An expression kind that represents no expression of 
        /// any kind.
        /// </summary>
        None = 0,
        /// <summary>
        /// An expression that represents an invocation on a multi-cast
        /// delegate.
        /// </summary>
        MultiCastDelegateCall,
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the right-operand.
        /// </summary>
        AssignExpression,
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value multiplied by the right-operand.
        /// </summary>
        AssignMultiplyOperation,
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value divided by the right-operand.
        /// </summary>
        AssignDivideOperation,
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the remainder of the 
        /// left-operand's initial value divided by the right-operand.
        /// </summary>
        AssignModulusOperation,
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the right-operand
        /// added to the  left-operand's initial value.
        /// </summary>
        AssignAddOperation,
        /// <summary>
        /// A binary expression which assigns the left-operand
        /// referenced local/member to the result of the left-operand's
        /// initial value minus the right-operand.
        /// </summary>
        AssignSubtractOperation,
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value left bit-shifted by the number of bits 
        /// represented by the right-operand.
        /// </summary>
        /// <remarks><para>The number of bits observed by the shift
        /// operation are determined by the left-operand's type;
        /// following is a list of the different types, and the mask
        /// used to determine the bit-shift:</para>
        /// <para>
        /// <see cref="SByte"/>, <see cref="Byte"/>: 7 (0x07)
        /// </para>
        /// <para>
        /// <see cref="Int16"/>, <see cref="UInt16"/>: 15 (0x0F)
        /// </para>
        /// <para>
        /// <see cref="Int32"/>, <see cref="UInt32"/>: 31 (0x1F)
        /// </para>
        /// <para>
        /// <see cref="Int64"/>, <see cref="UInt64"/>: 63 (0x3F)
        /// </para>
        /// </remarks>
        AssignLeftShiftOperation,
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value right bit-shifted by the number of bits 
        /// represented by the right-operand.
        /// </summary>
        /// <remarks><para>The number of bits observed by the shift 
        /// operation are determined by the left-operand's type;
        /// following is a list of the different types, and the mask
        /// used to determine the bit-shift.</para>
        /// <para>
        /// <see cref="SByte"/>, <see cref="Byte"/>: 7 (0x07)
        /// </para>
        /// <para>
        /// <see cref="Int16"/>, <see cref="UInt16"/>: 15 (0x0F)
        /// </para>
        /// <para>
        /// <see cref="Int32"/>, <see cref="UInt32"/>: 31 (0x1F)
        /// </para>
        /// <para>
        /// <see cref="Int64"/>, <see cref="UInt64"/>: 63 (0x3F)
        /// </para>
        /// </remarks>
        AssignRightShiftOperation,
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value and the right-operand's value bitwise-and'ed
        /// such that only the bits from both that overlap are the 
        /// result value.
        /// </summary>
        AssignBitwiseAndOperation,
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value and the right-operand's value bitwise-or'ed 
        /// such that the bits from both are included, and overlap is 
        /// ignored.
        /// </summary>
        AssignBitwiseOrOperation,
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value and the right-operand's value bitwise-or'ed 
        /// such that bits from both that overlap are excluded, and 
        /// bits that don't overlap are included; thus, only bits
        /// which are mutually exclusive on both sides are in the 
        /// result value.
        /// </summary>
        AssignBitwiseExclusiveOrOperation,
        /// <summary>
        /// A ternary expression which performs a conditional 
        /// short-circuit and evaluates only the true-part or 
        /// false-part based upon the conditional part's value.
        /// </summary>
        ConditionalOperation,
        /// <summary>
        /// An expression which is merely a forward from a conditional
        /// expression.
        /// </summary>
        ConditionalForwardTerm,
        /// <summary>
        /// A binary expression which performs a logical short-circuit
        /// on the left and right operands; wherein if the left operand
        /// is true, the result is true, if the left is false and the
        /// right is true the result is true; thus if either of the two
        /// is true, success is assumed.
        /// </summary>
        /// <remarks>The short occurs when the left is true, the right
        /// is therefore not evaluated.</remarks>
        LogicalOrOperation,
        /// <summary>
        /// A binary expression which performs a logical short-circuit
        /// on the left and right operands; wherein if the left 
        /// operation is false, the result is false, if the left is 
        /// true the result is the right-operand's value.
        /// </summary>
        LogicalAndOperation,
        /// <summary>
        /// A binary expression which performs a bit-wise or operation
        /// on the left and right operands; wherein the bits from both
        /// are included, and overlap is included, and thus, 
        /// insignificant.
        /// </summary>
        BitwiseOrOperation,
        /// <summary>
        /// A binary expression which performs a bit-wise exclusive
        /// or operation on the left and right operands; wherein bits
        /// from both that overlap are excluded, and bits that don't
        /// overlap are included; thus, only bits which are mutually
        /// exclusive on both sides are in the result value.
        /// </summary>
        BitwiseExclusiveOrOperation,
        /// <summary>
        /// A binary expression which performs a bitwise and operation
        /// on the left and right operands; wherein only the bits from
        /// both that overlap are in the result value.
        /// </summary>
        BitwiseAndOperation,
        /// <summary>
        /// A binary expression which represents an inequality check
        /// that determines whether the left-operand is not equal to
        /// the right-operand.
        /// </summary>
        InequalityOperation,
        /// <summary>
        /// A binary expression which represents an equality check
        /// that determines whether the left-operand is equal to
        /// the right-operand.
        /// </summary>
        EqualityOperation,
        /// <summary>
        /// A binary expression which represents a relational check
        /// that determines whether the left-operand is less than 
        /// the right-operand.
        /// </summary>
        LessThanOperation,
        /// <summary>
        /// A binary expression which represents a relational check
        /// that determines whether the left-operand is less than
        /// or equal to the right-operand.
        /// </summary>
        LessThanOrEqualToOperation,
        /// <summary>
        /// A binary expression which represents a relational check
        /// that determines whether the left-operand is greater than
        /// the right-operand.
        /// </summary>
        GreaterThanOperation,
        /// <summary>
        /// A binary expression which represents a relational check
        /// that determines whether the left-operand is greater than
        /// or equal to the right-operand.
        /// </summary>
        GreaterThanOrEqualToOperation,
        /// <summary>
        /// An expression which checks the type of another expression.
        /// </summary>
        /// <remarks>
        /// <para>C#: Expression "is" Type</para>
        /// <para>Visual Basic.NET: "TypeOf" expression "Is" Type
        /// </para></remarks>
        TypeCheckOperation,
        /// <summary>
        /// An expression which represents a type-cast on another 
        /// expression; should the cast be invalid, the value is
        /// opted for null.
        /// </summary>
        TypeCastOrNull,
        /// <summary>
        /// An expression which represents a bit-shift to the left
        /// operation.
        /// </summary>
        ShiftLeftOperation,
        /// <summary>
        /// An expression which represents a bit-shift to the right
        /// operation.
        /// </summary>
        ShiftRightOperation,
        /// <summary>
        /// An expression which represents an addition operation.
        /// </summary>
        AddOperation,
        /// <summary>
        /// An expression which represents a subtraction operation.
        /// </summary>
        SubtractOperation,
        /// <summary>
        /// An expression that represents a multiplication operation.
        /// </summary>
        MultiplyOperation,
        /// <summary>
        /// An expression that represents a modulus operation.
        /// </summary>
        ModulusOperation,
        /// <summary>
        /// An expression that represents a strict division operation.
        /// </summary>
        StrictDivisionOperation,
        /// <summary>
        /// An expression that represents a division of integers 
        /// wherein the result is always an integer.
        /// </summary>
        IntegerDivisionOperation,
        /// <summary>
        /// An expression that represents a flexible division 
        /// operation wherein the result is a double value.
        /// </summary>
        FlexibleDivisionOperation,
        /// <summary>
        /// An expression that represents a primitive boolean literal.
        /// </summary>
        PrimitiveBooleanInsert,
        /// <summary>
        /// An expression that represents a primitive signed byte
        /// literal.
        /// </summary>
        PrimitiveSByteInsert,
        /// <summary>
        /// An expression that represents a primitive byte literal.
        /// </summary>
        PrimitiveByteInsert,
        /// <summary>
        /// An expression that represents a primitive unsigned 16-bit
        /// integer.
        /// </summary>
        PrimitiveUInt16Insert,
        /// <summary>
        /// An expression that represents a primitive signed 16-bit
        /// integer.
        /// </summary>
        PrimitiveInt16Insert,
        /// <summary>
        /// An expression that represents a primitive unsigned 32-bit
        /// integer.
        /// </summary>
        PrimitiveUInt32Insert,
        /// <summary>
        /// An expression that represents a primitive signed 32-bit
        /// integer.
        /// </summary>
        PrimitiveInt32Insert,
        /// <summary>
        /// An expression that represents a primitive unsigned 64-bit
        /// integer.
        /// </summary>
        PrimitiveUInt64Insert,
        /// <summary>
        /// An expression that represents a primitive signed 64-bit
        /// integer.
        /// </summary>
        PrimitiveInt64Insert,
        /// <summary>
        /// An expression that represents a primitive single-precision
        /// floating-point literal.
        /// </summary>
        PrimitiveSingleInsert,
        /// <summary>
        /// An expression that represents a primitive double-precision
        /// floating-point literal.
        /// </summary>
        PrimitiveDoubleInsert,
        /// <summary>
        /// An expression that represents a primitive number with high
        /// precision as a literal.
        /// </summary>
        PrimitiveDecimalInsert,
        /// <summary>
        /// An expression that represents a primitive string of 
        /// characters as a literal.
        /// </summary>
        PrimitiveStringInsert,
        /// <summary>
        /// An expression that represents a primitive character as a
        /// literal.
        /// </summary>
        PrimitiveCharInsert,
        /// <summary>
        /// An expression that represents a reference or nullable
        /// type's null value.
        /// </summary>
        PrimitiveNullInsert,
        /// <summary>
        /// An expression that represents a reference to a local
        /// variable in the scope in which it is used.
        /// </summary>
        LocalReference,
        /// <summary>
        /// An expression that represents a reference to an event in
        /// the scope in which it is used.
        /// </summary>
        EventReference,
        /// <summary>
        /// An expression that represents an event being fired.
        /// </summary>
        EventFire,
        /// <summary>
        /// An expression that represents a reference to a field member
        /// in the scope in which it is used.
        /// </summary>
        FieldReference,
        /// <summary>
        /// An expression that represents a reference to an indexer
        /// member in the scope in which it is used.
        /// </summary>
        IndexerReference,
        /// <summary>
        /// An expression that represents a reference to a method
        /// signature in the scope in which it is used.
        /// </summary>
        MethodReference,
        /// <summary>
        /// An expression that represents a call to a method in the
        /// scope in which it is used.
        /// </summary>
        MethodCall,
        /// <summary>
        /// An expression that represents a call to a reference to
        /// a property in the scope in which it is used.
        /// </summary>
        PropertyReference,
        /// <summary>
        /// An expression which references a specific type.
        /// </summary>
        TypeReference,
        /// <summary>
        /// An expression which references a specific type wherein
        /// the <see cref="RuntimeTypeHandle"/>
        /// is pushed onto the stack.
        /// </summary>
        TypeOfExpression,
        /// <summary>
        /// An expression which casts the result of another expression
        /// to the type defined by the cast.
        /// </summary>
        TypeCast,
        /// <summary>
        /// An expression which overrides default compile-time behavior
        /// to re-enable overflow checks on an
        /// expression.
        /// </summary>
        CheckedExpression,
        /// <summary>
        /// An epression which overrides default compile-time behavior
        /// to disable overflowchecks on an
        /// expression.
        /// </summary>
        UncheckedExpression,
        /// <summary>
        /// An expression which represents an inlined procedure to a
        /// variable, or parameter to a method which requires a method
        /// signature.
        /// </summary>
        LambdaExpression,
        /// <summary>
        /// An expression which encases another expression within 
        /// parenthesis.
        /// </summary>
        ParenthesizedExpression,
        /// <summary>
        /// An expression which alters member lookup to indicate the
        /// scope is relative to the current instance.
        /// </summary>
        ThisReference,
        /// <summary>
        /// An expression which alters member lookup to indicate the
        /// scope is relative to the base type of the current type;
        /// additionally disabling standard virtual calling conventions
        /// to enable use of previous functionality on override.
        /// </summary>
        BaseReference,
        /// <summary>
        /// An expression which alters member lookup to indicate the
        /// scope is relative to the current instance with no regard
        /// to standard virtual calling conventions; thus, effectively
        /// a selfish call.
        /// </summary>
        SelfReference,
        /// <summary>
        /// An expression which denotes the least possible bit of data
        /// about a member lookup requesting the compiler framework
        /// handle the translation.
        /// </summary>
        SymbolExpression,
        /// <summary>
        /// An expression which denotes a binary operation expression
        /// is indicating that it is purely a forward to another
        /// lower-precedence expression in the language's concrete 
        /// syntax tree.
        /// </summary>
        BinaryForwardTerm,
        /// <summary>
        /// The expression is a fusion of two expressions.
        /// </summary>
        /// <remarks>Typically used to denote an expression to be 
        /// fused with another expression.</remarks>
        ExpressionFusion,
        /// <summary>
        /// The expression is a fusion of another expression and a 
        /// series of expressions, delimited by a comma.
        /// </summary>
        /// <remarks>Typically an invocation type expression where
        /// the target is the left and the parameters are on the 
        /// right.
        /// </remarks>
        ExpressionToCommaFusion,
        /// <summary>
        /// An series of expressions evaluated in verbatim order.
        /// </summary>
        CommaExpression,
        /// <summary>
        /// The expression is a fusion of an expression and a series of
        /// type references, delimited by a comma.
        /// </summary>
        /// <remarks>Typically used to denote the fusion of an 
        /// expression which needs a series of type-parameters.
        /// </remarks>
        ExpressionToTypeCollectionFusion,

        /// <summary>
        /// The expression is a language integrated query expression
        /// used to string together a series of data sources, 
        /// filtering predicates, ordering, grouping and/or data
        /// selection clauses which ultimately becomes a series
        /// of extension invocations, lambda inserts and further
        /// to become basic CIL code.
        /// </summary>
        LinqExpression,

        /// <summary>
        /// The expression wraps a sub-expression and denotes the
        /// named parameter 
        /// </summary>
        NamedParameterReference,

        /// <summary>
        /// The expression operates upon the wrapped expression in order
        /// to modify it before it is sent to its recipient.
        /// </summary>
        WorkspaceExpression,

        /// <summary>
        /// The expression references a method parameter.
        /// </summary>
        ParameterReference,
        /// <summary>
        /// The expression invokes a constructor member, generally
        /// returning an instance to an object.
        /// </summary>
        ConstructorInvoke,
        /// <summary>
        /// The expression references a specific constructor.
        /// </summary>
        ConstructorReference,
        /// <summary>
        /// The expression instructs the CLI to create an array.
        /// </summary>
        /// <remarks>Part of the expansions required sector in cases where 
        /// data at a specific location is required (for primitives).</remarks>
        CreateArray,
        /// <summary>
        /// The expression instructs the CLI to create an array's nested
        /// detail as a part of a multidimensional array.
        /// </summary>
        CreateArrayNestedDetail,
        /// <summary>
        /// The expression represents a switch case
        /// label which acts as a forward to the constant
        /// expression the label represents.
        /// </summary>
        SwitchCaseLabel,
        /// <summary>
        /// The expression represents a reference to a range variable
        /// within a language integrated query expression.
        /// </summary>
        RangeVariableReference,
        UnaryOperation,
        UnaryForwardTerm, /* 1110110100100010000000 */
    }
}
