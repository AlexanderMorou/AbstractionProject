using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a series of expression kinds innately supported by the
    /// Objectified Intermediate Language.
    /// </summary>
    public static class ExpressionKinds
    {
        /// <summary>
        /// An expression kind that represents no expression of 
        /// any kind.
        /// </summary>
        public static readonly ExpressionKind None                              = new ExpressionKind();
        /// <summary>
        /// An expression that represents an invocation on a multi-cast
        /// delegate.
        /// </summary>
        public static readonly ExpressionKind MultiCastDelegateCall             = ExpressionKind.InvocationSector.MultiCastDelegateCall;
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the right-operand.
        /// </summary>
        public static readonly ExpressionKind AssignExpression                  = ExpressionKind.BinaryOperationSector.AssignExpression;
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value multiplied by the right-operand.
        /// </summary>
        public static readonly ExpressionKind AssignMultiplyOperation           = ExpressionKind.BinaryOperationSector.AssignMultiplyOperation;
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value divided by the right-operand.
        /// </summary>
        public static readonly ExpressionKind AssignDivideOperation             = ExpressionKind.BinaryOperationSector.AssignDivideOperation;
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the remainder of the 
        /// left-operand's initial value divided by the right-operand.
        /// </summary>
        public static readonly ExpressionKind AssignModulusOperation            = ExpressionKind.BinaryOperationSector.AssignModulusOperation;
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the right-operand
        /// added to the  left-operand's initial value.
        /// </summary>
        public static readonly ExpressionKind AssignAddOperation                = ExpressionKind.BinaryOperationSector.AssignAddOperation;
        /// <summary>
        /// A binary expression which assigns the left-operand
        /// referenced local/member to the result of the left-operand's
        /// initial value minus the right-operand.
        /// </summary>
        public static readonly ExpressionKind AssignSubtractOperation           = ExpressionKind.BinaryOperationSector.AssignSubtractOperation;
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
        public static readonly ExpressionKind AssignLeftShiftOperation          = ExpressionKind.BinaryOperationSector.AssignLeftShiftOperation;
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
        public static readonly ExpressionKind AssignRightShiftOperation         = ExpressionKind.BinaryOperationSector.AssignRightShiftOperation;
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value and the right-operand's value bitwise-and'ed
        /// such that only the bits from both that overlap are the 
        /// result value.
        /// </summary>
        public static readonly ExpressionKind AssignBitwiseAndOperation         = ExpressionKind.BinaryOperationSector.AssignBitwiseAndOperation;
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value and the right-operand's value bitwise-or'ed 
        /// such that the bits from both are included, and overlap is 
        /// ignored.
        /// </summary>
        public static readonly ExpressionKind AssignBitwiseOrOperation          = ExpressionKind.BinaryOperationSector.AssignBitwiseOrOperation;
        /// <summary>
        /// A binary expression which assigns the left-operand 
        /// referenced local/member to the result of the left-operand's
        /// initial value and the right-operand's value bitwise-or'ed 
        /// such that bits from both that overlap are excluded, and 
        /// bits that don't overlap are included; thus, only bits
        /// which are mutually exclusive on both sides are in the 
        /// result value.
        /// </summary>
        public static readonly ExpressionKind AssignBitwiseExclusiveOrOperation = ExpressionKind.BinaryOperationSector.AssignBitwiseExclusiveOrOperation;
        /// <summary>
        /// A ternary expression which performs a conditional 
        /// short-circuit and evaluates only the true-part or 
        /// false-part based upon the conditional part's value.
        /// </summary>
        public static readonly ExpressionKind ConditionalOperation              = ExpressionKind.ExpansionRequiredSector.ConditionalOperation;
        /// <summary>
        /// An expression which is merely a forward from a conditional
        /// expression.
        /// </summary>
        public static readonly ExpressionKind ConditionalForwardTerm            = ExpressionKind.ExpansionRequiredSector.ConditionalForwardTerm;
        /// <summary>
        /// A binary expression which performs a logical short-circuit
        /// on the left and right operands; wherein if the left operand
        /// is true, the result is true, if the left is false and the
        /// right is true the result is true; thus if either of the two
        /// is true, success is assumed.
        /// </summary>
        /// <remarks>The short occurs when the left is true, the right
        /// is therefore not evaluated.</remarks>
        public static readonly ExpressionKind LogicalOrOperation                = ExpressionKind.BinaryOperationSector.LogicalOrOperation;
        /// <summary>
        /// A binary expression which performs a logical short-circuit
        /// on the left and right operands; wherein if the left 
        /// operation is false, the result is false, if the left is 
        /// true the result is the right-operand's value.
        /// </summary>
        public static readonly ExpressionKind LogicalAndOperation               = ExpressionKind.BinaryOperationSector.LogicalAndOperation;
        /// <summary>
        /// A binary expression which performs a bit-wise or operation
        /// on the left and right operands; wherein the bits from both
        /// are included, and overlap is included, and thus, 
        /// insignificant.
        /// </summary>
        public static readonly ExpressionKind BitwiseOrOperation                = ExpressionKind.BinaryOperationSector.BitwiseOrOperation;
        /// <summary>
        /// A binary expression which performs a bit-wise exclusive
        /// or operation on the left and right operands; wherein bits
        /// from both that overlap are excluded, and bits that don't
        /// overlap are included; thus, only bits which are mutually
        /// exclusive on both sides are in the result value.
        /// </summary>
        public static readonly ExpressionKind BitwiseExclusiveOrOperation       = ExpressionKind.BinaryOperationSector.BitwiseExclusiveOrOperation;
        /// <summary>
        /// A binary expression which performs a bitwise and operation
        /// on the left and right operands; wherein only the bits from
        /// both that overlap are in the result value.
        /// </summary>
        public static readonly ExpressionKind BitwiseAndOperation               = ExpressionKind.BinaryOperationSector.BitwiseAndOperation;
        /// <summary>
        /// A binary expression which represents an inequality check
        /// that determines whether the left-operand is not equal to
        /// the right-operand.
        /// </summary>
        public static readonly ExpressionKind InequalityOperation               = ExpressionKind.BinaryOperationSector.InequalityOperation;
        /// <summary>
        /// A binary expression which represents an equality check
        /// that determines whether the left-operand is equal to
        /// the right-operand.
        /// </summary>
        public static readonly ExpressionKind EqualityOperation                 = ExpressionKind.BinaryOperationSector.EqualityOperation;
        /// <summary>
        /// A binary expression which represents a relational check
        /// that determines whether the left-operand is less than 
        /// the right-operand.
        /// </summary>
        public static readonly ExpressionKind LessThanOperation                 = ExpressionKind.BinaryOperationSector.LessThanOperation;
        /// <summary>
        /// A binary expression which represents a relational check
        /// that determines whether the left-operand is less than
        /// or equal to the right-operand.
        /// </summary>
        public static readonly ExpressionKind LessThanOrEqualToOperation        = ExpressionKind.BinaryOperationSector.LessThanOrEqualToOperation;
        /// <summary>
        /// A binary expression which represents a relational check
        /// that determines whether the left-operand is greater than
        /// the right-operand.
        /// </summary>
        public static readonly ExpressionKind GreaterThanOperation              = ExpressionKind.BinaryOperationSector.GreaterThanOperation;
        /// <summary>
        /// A binary expression which represents a relational check
        /// that determines whether the left-operand is greater than
        /// or equal to the right-operand.
        /// </summary>
        public static readonly ExpressionKind GreaterThanOrEqualToOperation     = ExpressionKind.BinaryOperationSector.GreaterThanOrEqualToOperation;
        /// <summary>
        /// An expression which checks the type of another expression.
        /// </summary>
        /// <remarks>
        /// <para>C#: Expression "is" Type</para>
        /// <para>Visual Basic.NET: "TypeOf" expression "Is" Type
        /// </para></remarks>
        public static readonly ExpressionKind TypeCheckOperation                = ExpressionKind.BinaryOperationSector.TypeCheckOperation;
        /// <summary>
        /// An expression which represents a type-cast on another 
        /// expression; should the cast be invalid, the value is
        /// opted for null.
        /// </summary>
        public static readonly ExpressionKind TypeCastOrNull                    = ExpressionKind.BinaryOperationSector.TypeCastOrNull;
        /// <summary>
        /// An expression which represents a bit-shift to the left
        /// operation.
        /// </summary>
        public static readonly ExpressionKind ShiftLeftOperation                = ExpressionKind.BinaryOperationSector.ShiftLeftOperation;
        /// <summary>
        /// An expression which represents a bit-shift to the right
        /// operation.
        /// </summary>
        public static readonly ExpressionKind ShiftRightOperation               = ExpressionKind.BinaryOperationSector.ShiftRightOperation;
        /// <summary>
        /// An expression which represents an addition operation.
        /// </summary>
        public static readonly ExpressionKind AddOperation                      = ExpressionKind.BinaryOperationSector.AddOperation;
        /// <summary>
        /// An expression which represents a subtraction operation.
        /// </summary>
        public static readonly ExpressionKind SubtractOperation                 = ExpressionKind.BinaryOperationSector.SubtractOperation;
        /// <summary>
        /// An expression that represents a multiplication operation.
        /// </summary>
        public static readonly ExpressionKind MultiplyOperation                 = ExpressionKind.BinaryOperationSector.MultiplyOperation;
        /// <summary>
        /// An expression that represents a modulus operation.
        /// </summary>
        public static readonly ExpressionKind ModulusOperation                  = ExpressionKind.BinaryOperationSector.ModulusOperation;
        /// <summary>
        /// An expression that represents a strict division operation.
        /// </summary>
        public static readonly ExpressionKind StrictDivisionOperation           = ExpressionKind.BinaryOperationSector.StrictDivisionOperation;
        /// <summary>
        /// An expression that represents a division of integers 
        /// wherein the result is always an integer.
        /// </summary>
        public static readonly ExpressionKind IntegerDivisionOperation          = ExpressionKind.BinaryOperationSector.IntegerDivisionOperation;
        /// <summary>
        /// An expression that represents a flexible division 
        /// operation wherein the result is a double value.
        /// </summary>
        public static readonly ExpressionKind FlexibleDivisionOperation         = ExpressionKind.BinaryOperationSector.FlexibleDivisionOperation;
        /// <summary>
        /// An expression that represents a unary operation.
        /// </summary>
        public static readonly ExpressionKind UnarySignInversionOperation       = ExpressionKind.UnaryOperationSector.UnarySignInversionOperation;
        /// <summary>
        /// An expression that represents a primitive boolean literal.
        /// </summary>
        public static readonly ExpressionKind PrimitiveBooleanInsert            = ExpressionKind.PrimitiveInsertSector.PrimitiveBooleanInsert;
        /// <summary>
        /// An expression that represents a primitive signed byte
        /// literal.
        /// </summary>
        public static readonly ExpressionKind PrimitiveSByteInsert              = ExpressionKind.PrimitiveInsertSector.PrimitiveSByteInsert;
        /// <summary>
        /// An expression that represents a primitive byte literal.
        /// </summary>
        public static readonly ExpressionKind PrimitiveByteInsert               = ExpressionKind.PrimitiveInsertSector.PrimitiveByteInsert;
        /// <summary>
        /// An expression that represents a primitive unsigned 16-bit
        /// integer.
        /// </summary>
        public static readonly ExpressionKind PrimitiveUInt16Insert             = ExpressionKind.PrimitiveInsertSector.PrimitiveUInt16Insert;
        /// <summary>
        /// An expression that represents a primitive signed 16-bit
        /// integer.
        /// </summary>
        public static readonly ExpressionKind PrimitiveInt16Insert              = ExpressionKind.PrimitiveInsertSector.PrimitiveInt16Insert;
        /// <summary>
        /// An expression that represents a primitive unsigned 32-bit
        /// integer.
        /// </summary>
        public static readonly ExpressionKind PrimitiveUInt32Insert             = ExpressionKind.PrimitiveInsertSector.PrimitiveUInt32Insert;
        /// <summary>
        /// An expression that represents a primitive signed 32-bit
        /// integer.
        /// </summary>
        public static readonly ExpressionKind PrimitiveInt32Insert              = ExpressionKind.PrimitiveInsertSector.PrimitiveInt32Insert;
        /// <summary>
        /// An expression that represents a primitive unsigned 64-bit
        /// integer.
        /// </summary>
        public static readonly ExpressionKind PrimitiveUInt64Insert             = ExpressionKind.PrimitiveInsertSector.PrimitiveUInt64Insert;
        /// <summary>
        /// An expression that represents a primitive signed 64-bit
        /// integer.
        /// </summary>
        public static readonly ExpressionKind PrimitiveInt64Insert              = ExpressionKind.PrimitiveInsertSector.PrimitiveInt64Insert;
        /// <summary>
        /// An expression that represents a primitive single-precision
        /// floating-point literal.
        /// </summary>
        public static readonly ExpressionKind PrimitiveSingleInsert             = ExpressionKind.PrimitiveInsertSector.PrimitiveSingleInsert;
        /// <summary>
        /// An expression that represents a primitive double-precision
        /// floating-point literal.
        /// </summary>
        public static readonly ExpressionKind PrimitiveDoubleInsert             = ExpressionKind.PrimitiveInsertSector.PrimitiveDoubleInsert;
        /// <summary>
        /// An expression that represents a primitive number with high
        /// precision as a literal.
        /// </summary>
        public static readonly ExpressionKind PrimitiveDecimalInsert            = ExpressionKind.PrimitiveInsertSector.PrimitiveDecimalInsert;
        /// <summary>
        /// An expression that represents a primitive string of 
        /// characters as a literal.
        /// </summary>
        public static readonly ExpressionKind PrimitiveStringInsert             = ExpressionKind.PrimitiveInsertSector.PrimitiveStringInsert;
        /// <summary>
        /// An expression that represents a primitive character as a
        /// literal.
        /// </summary>
        public static readonly ExpressionKind PrimitiveCharInsert               = ExpressionKind.PrimitiveInsertSector.PrimitiveCharInsert;
        /// <summary>
        /// An expression that represents a reference or nullable
        /// type's null value.
        /// </summary>
        public static readonly ExpressionKind PrimitiveNullInsert               = ExpressionKind.PrimitiveInsertSector.PrimitiveNullInsert;
        /// <summary>
        /// An expression that represents a reference to a local
        /// variable in the scope in which it is used.
        /// </summary>
        public static readonly ExpressionKind LocalReference                    = ExpressionKind.ReferenceSector.LocalReference;
        /// <summary>
        /// An expression that represents a reference to an event in
        /// the scope in which it is used.
        /// </summary>
        public static readonly ExpressionKind EventReference                    = ExpressionKind.ReferenceSector.EventReference;
        /// <summary>
        /// An expression that represents an event being fired.
        /// </summary>
        public static readonly ExpressionKind EventFire                         = ExpressionKind.InvocationSector.EventFire;
        /// <summary>
        /// An expression that represents a reference to a field member
        /// in the scope in which it is used.
        /// </summary>
        public static readonly ExpressionKind FieldReference                    = ExpressionKind.ReferenceSector.FieldReference;
        /// <summary>
        /// An expression that represents a reference to an indexer
        /// member in the scope in which it is used.
        /// </summary>
        public static readonly ExpressionKind IndexerReference                  = ExpressionKind.ReferenceSector.IndexerReference;
        /// <summary>
        /// An expression that represents a reference to a method
        /// signature in the scope in which it is used.
        /// </summary>
        public static readonly ExpressionKind MethodReference                   = ExpressionKind.ReferenceSector.MethodReference;
        /// <summary>
        /// An expression that represents a call to a method in the
        /// scope in which it is used.
        /// </summary>
        public static readonly ExpressionKind MethodCall                        = ExpressionKind.InvocationSector.MethodCall;
        /// <summary>
        /// An expression that represents a call to a reference to
        /// a property in the scope in which it is used.
        /// </summary>
        public static readonly ExpressionKind PropertyReference                 = ExpressionKind.ReferenceSector.PropertyReference;
        /// <summary>
        /// An expression which references a specific type.
        /// </summary>
        public static readonly ExpressionKind TypeReference                     = ExpressionKind.ReferenceSector.TypeReference;
        /// <summary>
        /// An expression which references a specific type wherein
        /// the <see cref="RuntimeTypeHandle"/>
        /// is pushed onto the stack.
        /// </summary>
        public static readonly ExpressionKind TypeOfExpression                  = ExpressionKind.SpecialFunctionSector.TypeOfExpression;
        /// <summary>
        /// An expression which casts the result of another expression
        /// to the type defined by the cast.
        /// </summary>
        public static readonly ExpressionKind TypeCast                          = ExpressionKind.SpecialFunctionSector.TypeCast;
        /// <summary>
        /// An expression which overrides default compile-time behavior
        /// to re-enable overflow checks on an
        /// expression.
        /// </summary>
        public static readonly ExpressionKind CheckedExpression                 = ExpressionKind.SpecialFunctionSector.CheckedExpression;
        /// <summary>
        /// An epression which overrides default compile-time behavior
        /// to disable overflowchecks on an
        /// expression.
        /// </summary>
        public static readonly ExpressionKind UncheckedExpression               = ExpressionKind.SpecialFunctionSector.UncheckedExpression;
        /// <summary>
        /// An expression which represents an inlined procedure to a
        /// variable, or parameter to a method which requires a method
        /// signature.
        /// </summary>
        public static readonly ExpressionKind LambdaExpression                  = ExpressionKind.ExpansionRequiredSector.LambdaExpression;
        /// <summary>
        /// An expression which encases another expression within 
        /// parenthesis.
        /// </summary>
        public static readonly ExpressionKind ParenthesizedExpression           = ExpressionKind.SymbolSector.ParenthesizedExpression;
        /// <summary>
        /// An expression which alters member lookup to indicate the
        /// scope is relative to the current instance.
        /// </summary>
        public static readonly ExpressionKind ThisReference                     = ExpressionKind.ReferenceSector.ThisReference;
        /// <summary>
        /// An expression which alters member lookup to indicate the
        /// scope is relative to the base type of the current type;
        /// additionally disabling standard virtual calling conventions
        /// to enable use of previous functionality on override.
        /// </summary>
        public static readonly ExpressionKind BaseReference                     = ExpressionKind.ReferenceSector.BaseReference;
        /// <summary>
        /// An expression which alters member lookup to indicate the
        /// scope is relative to the current instance with no regard
        /// to standard virtual calling conventions; thus, effectively
        /// a selfish call.
        /// </summary>
        public static readonly ExpressionKind SelfReference                     = ExpressionKind.ReferenceSector.SelfReference;
        /// <summary>
        /// An expression which denotes the least possible bit of data
        /// about a member lookup requesting the compiler framework
        /// handle the translation.
        /// </summary>
        public static readonly ExpressionKind SymbolExpression                  = ExpressionKind.SymbolSector.SymbolExpression;
        /// <summary>
        /// An expression which alters member lookup to refer to a
        /// type relative to the inheritance tree at the highest point;
        /// the most-derived class in the current execution frame.  
        /// </summary>
        /// <remarks><para>The concept here is if you have a static
        /// method 'Test' in non-static class 'A' and 'B' derives
        /// from 'A', a new method to mask the original is created
        /// automatically.  The functionality of the method is
        /// implemented in a generic version that accepts the 'most
        /// derived type' as a type-parameter, replacing 
        /// '<see cref="CurrentTypeReference"/>' with the literal
        /// most-derived class' type.</para></remarks>
        public static readonly ExpressionKind CurrentTypeReference              = ExpressionKind.ReferenceSector.CurrentTypeReference;
        /// <summary>
        /// An expression which denotes a binary operation expression
        /// is indicating that it is purely a forward to another
        /// lower-precedence expression in the language's concrete 
        /// syntax tree.
        /// </summary>
        public static readonly ExpressionKind BinaryForwardTerm                 = ExpressionKind.BinaryOperationSector.BinaryForwardTerm;
        /// <summary>
        /// An expression which is a unary operation on a unary target
        /// which pre-increments the value before returning to the
        /// enclosing expression.
        /// </summary>
        /// <remarks>Typically processed before 
        /// <see cref="UnaryBitwiseInversion"/> and 
        /// <see cref="UnarySignInversionOperation"/>.</remarks>
        public static readonly ExpressionKind UnaryPreincrement                 = ExpressionKind.UnaryOperationSector.UnaryPreincrement;
        /// <summary>
        /// The unary operation caches the original target value
        /// and increments the target, returning the cached value.
        /// </summary>
        /// <remarks>Typically processed after 
        /// <see cref="UnaryBitwiseInversion"/> and 
        /// <see cref="UnarySignInversionOperation"/>.</remarks>
        public static readonly ExpressionKind UnaryPostincrement                = ExpressionKind.UnaryOperationSector.UnaryPostincrement;
        /// <summary>
        /// The unary operation pre-decrements the value specified
        /// and returns that value to the next containing expression.
        /// </summary>
        public static readonly ExpressionKind UnaryPredecrement                 = ExpressionKind.UnaryOperationSector.UnaryPredecrement;
        /// <summary>
        /// The unary operation caches the original target value and
        /// decrements the target, returning the cached value.
        /// </summary>
        public static readonly ExpressionKind UnaryPostdecrement                = ExpressionKind.UnaryOperationSector.UnaryPostdecrement;
        /// <summary>
        /// The unary operation inverts the boolean target value
        /// or invokes its unary logical not operator overload.
        /// </summary>
        /// <remarks>The underlying associated overload name is
        /// op_LogicalNot.</remarks>
        public static readonly ExpressionKind UnaryBooleanInversion             = ExpressionKind.UnaryOperationSector.UnaryBooleanInversion;
        /// <summary>
        /// The unary operation returns the inverted bits of the
        /// target numeric value.
        /// </summary>
        public static readonly ExpressionKind UnaryBitwiseInversion             = ExpressionKind.UnaryOperationSector.UnaryBitwiseInversion;
        /// <summary>
        /// The unary operation is not present and thus acts as a
        /// forward to the unary operation's primary term.
        /// </summary>
        public static readonly ExpressionKind UnaryForwardTerm                  = ExpressionKind.UnaryOperationSector.UnaryForwardTerm;
        /// <summary>
        /// The expression is a fusion of two expressions.
        /// </summary>
        /// <remarks>Typically used to denote an expression to be 
        /// fused with another expression.</remarks>
        public static readonly ExpressionKind ExpressionFusion                  = ExpressionKind.SymbolSector.ExpressionFusion;
        /// <summary>
        /// The expression is a fusion of another expression and a 
        /// series of expressions, delimited by a comma.
        /// </summary>
        /// <remarks>Typically an invocation type expression where
        /// the target is the left and the parameters are on the 
        /// right.
        /// </remarks>
        public static readonly ExpressionKind ExpressionToCommaFusion           = ExpressionKind.SymbolSector.ExpressionToCommaFusion;
        /// <summary>
        /// An series of expressions evaluated in verbatim order.
        /// </summary>
        public static readonly ExpressionKind CommaExpression                   = ExpressionKind.ExpansionRequiredSector.CommaExpression;
        /// <summary>
        /// The expression is a fusion of an expression and a series of
        /// type references, delimited by a comma.
        /// </summary>
        /// <remarks>Typically used to denote the fusion of an 
        /// expression which needs a series of type-parameters.
        /// </remarks>
        public static readonly ExpressionKind ExpressionToTypeCollectionFusion  = ExpressionKind.SymbolSector.ExpressionToTypeCollectionFusion;

        /// <summary>
        /// The expression is a language integrated query expression
        /// used to string together a series of data sources, 
        /// filtering predicates, ordering, grouping and/or data
        /// selection clauses which ultimately becomes a series
        /// of extension invocations, lambda inserts and further
        /// to become basic CIL code.
        /// </summary>
        public static readonly ExpressionKind LinqExpression                    = ExpressionKind.ExpansionRequiredSector.LinqExpression;

        /// <summary>
        /// The expression wraps a sub-expression and denotes the
        /// named parameter 
        /// </summary>
        public static readonly ExpressionKind NamedParameterReference           = ExpressionKind.ReferenceSector.NamedParameterReference;

        /// <summary>
        /// The expression operates upon the wrapped expression in order
        /// to modify it before it is sent to its recipient.
        /// </summary>
        public static readonly ExpressionKind WorkspaceExpression               = ExpressionKind.ExpansionRequiredSector.WorkspaceExpression;

        /// <summary>
        /// The expression references a method parameter.
        /// </summary>
        public static readonly ExpressionKind ParameterReference                = ExpressionKind.ReferenceSector.ParameterReference;
        /// <summary>
        /// The expression invokes a constructor member, generally
        /// returning an instance to an object.
        /// </summary>
        public static readonly ExpressionKind ConstructorInvoke                 = ExpressionKind.InvocationSector.ConstructorInvoke;
        /// <summary>
        /// The expression references a specific constructor.
        /// </summary>
        public static readonly ExpressionKind ConstructorReference              = ExpressionKind.ReferenceSector.ConstructorReference;
        /// <summary>
        /// The expression instructs the CLI to create an array.
        /// </summary>
        /// <remarks>Part of the expansions required sector in cases where 
        /// data at a specific location is required (for primitives).</remarks>
        public static readonly ExpressionKind CreateArray                       = ExpressionKind.ExpansionRequiredSector.CreateArray;
        /// <summary>
        /// The expression represents a switch case
        /// label which acts as a forward to the constant
        /// expression the label represents.
        /// </summary>
        public static readonly ExpressionKind SwitchCaseLabel                   = ExpressionKind.ReferenceSector.SwitchCaseLabel;
    }
}
