using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers.Tokens
{
    public enum CSharpOperatorCases :
        long
    {
        None                            = 0x0000000000000000,
        /// <summary>
        /// The token parsed is case Increment from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "++".
        /// </remarks>
        Increment                       = 0x0000000000000001,
        /// <summary>
        /// The token parsed is case Decrement from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "--".
        /// </remarks>
        Decrement                       = 0x0000000000000002,
        /// <summary>
        /// The token parsed is case Addition from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '+'.
        /// </remarks>
        Addition                        = 0x0000000000000004,
        /// <summary>
        /// The token parsed is case Subtraction from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '-'.
        /// </remarks>
        Subtraction                     = 0x0000000000000008,
        /// <summary>
        /// The token parsed is case Multiplication from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '*'.
        /// </remarks>
        Multiplication                  = 0x0000000000000010,
        /// <summary>
        /// The token parsed is case Division from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '/'.
        /// </remarks>
        Division                        = 0x0000000000000020,
        /// <summary>
        /// The token parsed is case Remainder from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '%'.
        /// </remarks>
        Remainder                       = 0x0000000000000040,
        /// <summary>
        /// The token parsed is case Equality from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "==".
        /// </remarks>
        Equality                        = 0x0000000000000080,
        /// <summary>
        /// The token parsed is case Inequality from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "!=".
        /// </remarks>
        Inequality                      = 0x0000000000000100,
        /// <summary>
        /// The token parsed is case LeftShift from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "<<".
        /// </remarks>
        LeftShift                       = 0x0000000000000200,
        /// <summary>
        /// The token parsed is case RightShift from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: ">>".
        /// </remarks>
        RightShift                      = 0x0000000000000400,
        /// <summary>
        /// The token parsed is case LessThan from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '<'.
        /// </remarks>
        LessThan                        = 0x0000000000000800,
        /// <summary>
        /// The token parsed is case LessThanOrEqualTo from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "<=".
        /// </remarks>
        LessThanOrEqualTo               = 0x0000000000001000,
        /// <summary>
        /// The token parsed is case GreaterThan from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '>'.
        /// </remarks>
        GreaterThan                     = 0x0000000000002000,
        /// <summary>
        /// The token parsed is case GreaterThanOrEqualTo from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: ">=".
        /// </remarks>
        GreaterThanOrEqualTo            = 0x0000000000004000,
        /// <summary>
        /// The token parsed is case IsType from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "is".
        /// </remarks>
        IsType                          = 0x0000000000008000,
        /// <summary>
        /// The token parsed is case CastToOrNull from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "as".
        /// </remarks>
        CastToOrNull                    = 0x0000000000010000,
        /// <summary>
        /// The token parsed is case SimpleAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '='.
        /// </remarks>
        SimpleAssign                    = 0x0000000000020000,
        /// <summary>
        /// The token parsed is case MultAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "*=".
        /// </remarks>
        MultAssign                      = 0x0000000000040000,
        /// <summary>
        /// The token parsed is case DivAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "/=".
        /// </remarks>
        DivAssign                       = 0x0000000000080000,
        /// <summary>
        /// The token parsed is case ModAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "%=".
        /// </remarks>
        ModAssign                       = 0x0000000000100000,
        /// <summary>
        /// The token parsed is case AddAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "+=".
        /// </remarks>
        AddAssign                       = 0x0000000000200000,
        /// <summary>
        /// The token parsed is case SubtAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "-=".
        /// </remarks>
        SubtAssign                      = 0x0000000000400000,
        /// <summary>
        /// The token parsed is case LShiftAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "<<=".
        /// </remarks>
        LShiftAssign                    = 0x0000000000800000,
        /// <summary>
        /// The token parsed is case RShiftAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: ">>=".
        /// </remarks>
        RShiftAssign                    = 0x0000000001000000,
        /// <summary>
        /// The token parsed is case BitwiseAndAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "&=".
        /// </remarks>
        BitwiseAndAssign                = 0x0000000002000000,
        /// <summary>
        /// The token parsed is case BitwiseExclusiveOrAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "^=".
        /// </remarks>
        BitwiseExclusiveOrAssign        = 0x0000000004000000,
        /// <summary>
        /// The token parsed is case BitwiseOrAssign from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "|=".
        /// </remarks>
        BitwiseOrAssign                 = 0x0000000008000000,
        /// <summary>
        /// The token parsed is case ConditionalAnd from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "&&".
        /// </remarks>
        ConditionalAnd                  = 0x0000000010000000,
        /// <summary>
        /// The token parsed is case ConditionalOr from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: "||".
        /// </remarks>
        ConditionalOr                   = 0x0000000020000000,
        /// <summary>
        /// The token parsed is case BitwiseExclusiveOr from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '^'.
        /// </remarks>
        BitwiseExclusiveOr              = 0x0000000040000000,
        /// <summary>
        /// The token parsed is case BitwiseAnd from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '&'.
        /// </remarks>
        BitwiseAnd                      = 0x0000000080000000,
        /// <summary>
        /// The token parsed is case BitwiseOr from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '|'.
        /// </remarks>
        BitwiseOr                       = 0x0000000100000000,
        /// <summary>
        /// The token parsed is case MemberSeparator from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '.'.
        /// </remarks>
        MemberSeparator                 = 0x0000000200000000,
        /// <summary>
        /// The token parsed is case OpenBlock from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '{'.
        /// </remarks>
        OpenBlock                       = 0x0000000400000000,
        /// <summary>
        /// The token parsed is case CloseBlock from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '}'.
        /// </remarks>
        CloseBlock                      = 0x0000000800000000,
        /// <summary>
        /// The token parsed is case Comma from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: ','.
        /// </remarks>
        Comma                           = 0x0000001000000000,
        /// <summary>
        /// The token parsed is case LeftParenthesis from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '('.
        /// </remarks>
        LeftParenthesis                 = 0x0000002000000000,
        /// <summary>
        /// The token parsed is case RightParenthesis from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: ')'.
        /// </remarks>
        RightParenthesis                = 0x0000004000000000,
        /// <summary>
        /// The token parsed is case EntryTerminal from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: ';'.
        /// </remarks>
        EntryTerminal                   = 0x0000008000000000,
        /// <summary>
        /// The token parsed is case LeftSquareBracket from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: '['.
        /// </remarks>
        LeftSquareBracket               = 0x0000010000000000,
        /// <summary>
        /// The token parsed is case RightSquareBracket from Operators.
        /// </summary>
        /// <remarks>
        /// This token was sourced from copy of keywords.oilexer (0x0000000000000000, 0x0000000000000000).
        /// Literal value: ']'.
        /// </remarks>
        RightSquareBracket              = 0x0000020000000000,
        /// <summary>
        /// The token parsed is case BooleanInversion from Operators
        /// </summary>
        Invert                          = 0x0000040000000000,
        /// <summary>
        /// The token parsed is a namespace alias qualifier used to indicate
        /// a lookup by a namespace alias
        /// </summary>
        NamespaceAliasQualifier         = 0x0000080000000000,
    }
    [CLSCompliant(false)]
    public interface ICSharpOperatorToken :
        IToken
    {
        /// <summary>
        /// Returns the <see cref="CSharpOperatorCases"/> which designates
        /// the operator defined by the current <see cref="ICSharpOperatorToken"/>.
        /// </summary>
        CSharpOperatorCases Operator { get; }
    }
}
