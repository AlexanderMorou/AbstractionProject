using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers.Tokens
{
    public class CSharpOperatorToken :
        Token,
        ICSharpOperatorToken
    {
        /// <summary>
        /// Creates a new <see cref="CSharpOperatorToken"/> with the 
        /// <paramref name="location"/> and <paramref name="operator"/>
        /// provided.
        /// </summary>
        /// <param name="location">The <see cref="FileLocale"/> which indicates where in the source
        /// the <see cref="CSharpOperatorToken"/> is.</param>
        /// <param name="operator">The <see cref="CSharpOperatorCases"/> which
        /// indicate which operator the <see cref="CSharpOperatorToken"/> represents.</param>
        internal CSharpOperatorToken(FileLocale location, CSharpOperatorCases @operator)
            : base(location)
        {
        }

        public override uint Length
        {
            get
            {
                switch (this.Operator)
                {
                    case CSharpOperatorCases.Addition:
                    case CSharpOperatorCases.BitwiseAnd:
                    case CSharpOperatorCases.BitwiseExclusiveOr:
                    case CSharpOperatorCases.BitwiseOr:
                    case CSharpOperatorCases.CloseBlock:
                    case CSharpOperatorCases.Comma:
                    case CSharpOperatorCases.Division:
                    case CSharpOperatorCases.EntryTerminal:
                    case CSharpOperatorCases.GreaterThan:
                    case CSharpOperatorCases.Invert:
                    case CSharpOperatorCases.LeftParenthesis:
                    case CSharpOperatorCases.LeftSquareBracket:
                    case CSharpOperatorCases.LessThan:
                    case CSharpOperatorCases.Multiplication:
                    case CSharpOperatorCases.OpenBlock:
                    case CSharpOperatorCases.Remainder:
                    case CSharpOperatorCases.RightParenthesis:
                    case CSharpOperatorCases.RightSquareBracket:
                    case CSharpOperatorCases.SimpleAssign:
                    case CSharpOperatorCases.Subtraction:
                        return 1;
                    case CSharpOperatorCases.AddAssign:
                    case CSharpOperatorCases.BitwiseAndAssign:
                    case CSharpOperatorCases.BitwiseExclusiveOrAssign:
                    case CSharpOperatorCases.BitwiseOrAssign:
                    case CSharpOperatorCases.CastToOrNull:
                    case CSharpOperatorCases.ConditionalAnd:
                    case CSharpOperatorCases.ConditionalOr:
                    case CSharpOperatorCases.Decrement:
                    case CSharpOperatorCases.DivAssign:
                    case CSharpOperatorCases.Equality:
                    case CSharpOperatorCases.GreaterThanOrEqualTo:
                    case CSharpOperatorCases.Increment:
                    case CSharpOperatorCases.Inequality:
                    case CSharpOperatorCases.IsType:
                    case CSharpOperatorCases.LeftShift:
                    case CSharpOperatorCases.LessThanOrEqualTo:
                    case CSharpOperatorCases.MemberSeparator:
                    case CSharpOperatorCases.ModAssign:
                    case CSharpOperatorCases.MultAssign:
                    case CSharpOperatorCases.RightShift:
                    case CSharpOperatorCases.SubtAssign:
                        return 2;
                    case CSharpOperatorCases.LShiftAssign:
                    case CSharpOperatorCases.RShiftAssign:
                        return 3;
                    case CSharpOperatorCases.None:
                    default:
                        return 0;
                };
            }
        }

        #region ICSharpOperatorToken Members

        public CSharpOperatorCases Operator { get; private set; }

        #endregion
    }
}
