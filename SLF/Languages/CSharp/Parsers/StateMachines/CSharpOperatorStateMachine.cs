using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Tokens;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers.StateMachines
{
    public class CSharpOperatorStateMachine :
        ITokenizerStateMachine<ICSharpOperatorToken>
    {
        #region CSharpOperatorStateMachine data members.
        private int state;

        private int exitState;
        #endregion // CSharpKeywordStateMachine data members.
        #region ITokenizerStateMachine<ICSharpOperatorToken> Members

        public ICSharpOperatorToken GetToken(FileLocale location)
        {
            CSharpOperatorCases op = CSharpOperatorCases.None;
            switch (this.exitState)
            {
                case 24:
                    op = CSharpOperatorCases.Invert;
                    /* --------------------\
                    |  Build BooleanInversion here.  |
                    |  Actual value: !     |
                    \-------------------- */
                    break;
                case 12:
                    op = CSharpOperatorCases.Remainder;
                    /* -----------------------\
                    |  Build Remainder here.  |
                    |  Actual value: %        |
                    \----------------------- */
                    break;
                case 23:
                    op = CSharpOperatorCases.BitwiseAnd;
                    /* ------------------------\
                    |  Build BitwiseAnd here.  |
                    |  Actual value: &         |
                    \------------------------ */
                    break;
                case 22:
                    op = CSharpOperatorCases.LeftParenthesis;
                    /* -----------------------------\
                    |  Build LeftParenthesis here.  |
                    |  Actual value: (              |
                    \----------------------------- */
                    break;
                case 21:
                    op = CSharpOperatorCases.RightParenthesis;
                    /* ------------------------------\
                    |  Build RightParenthesis here.  |
                    |  Actual value: )               |
                    \------------------------------ */
                    break;
                case 11:
                    op = CSharpOperatorCases.Multiplication;
                    /* ----------------------------\
                    |  Build Multiplication here.  |
                    |  Actual value: *             |
                    \---------------------------- */
                    break;
                case 20:
                    op = CSharpOperatorCases.Addition;
                    /* ----------------------\
                    |  Build Addition here.  |
                    |  Actual value: +       |
                    \---------------------- */
                    break;
                case 19:
                    op = CSharpOperatorCases.Comma;
                    /* -------------------\
                    |  Build Comma here.  |
                    |  Actual value: ,    |
                    \------------------- */
                    break;
                case 10:
                    op = CSharpOperatorCases.Subtraction;
                    /* -------------------------\
                    |  Build Subtraction here.  |
                    |  Actual value: -          |
                    \------------------------- */
                    break;
                case 18:
                    op = CSharpOperatorCases.MemberSeparator;
                    /* -----------------------------\
                    |  Build MemberSeparator here.  |
                    |  Actual value: .              |
                    \----------------------------- */
                    break;
                case 17:
                    op = CSharpOperatorCases.Division;
                    /* ----------------------\
                    |  Build Division here.  |
                    |  Actual value: /       |
                    \---------------------- */
                    break;
                case 16:
                    op = CSharpOperatorCases.EntryTerminal;
                    /* ---------------------------\
                    |  Build EntryTerminal here.  |
                    |  Actual value: ;            |
                    \--------------------------- */
                    break;
                case 9:
                    op = CSharpOperatorCases.LessThan;
                    /* ----------------------\
                    |  Build LessThan here.  |
                    |  Actual value: <       |
                    \---------------------- */
                    break;
                case 8:
                    op = CSharpOperatorCases.SimpleAssign;
                    /* --------------------------\
                    |  Build SimpleAssign here.  |
                    |  Actual value: =           |
                    \-------------------------- */
                    break;
                case 7:
                    op = CSharpOperatorCases.GreaterThan;
                    /* -------------------------\
                    |  Build GreaterThan here.  |
                    |  Actual value: >          |
                    \------------------------- */
                    break;
                case 15:
                    op = CSharpOperatorCases.LeftSquareBracket;
                    /* -------------------------------\
                    |  Build LeftSquareBracket here.  |
                    |  Actual value: [                |
                    \------------------------------- */
                    break;
                case 14:
                    op = CSharpOperatorCases.RightSquareBracket;
                    /* --------------------------------\
                    |  Build RightSquareBracket here.  |
                    |  Actual value: ]                 |
                    \-------------------------------- */
                    break;
                case 6:
                    op = CSharpOperatorCases.BitwiseExclusiveOr;
                    /* --------------------------------\
                    |  Build BitwiseExclusiveOr here.  |
                    |  Actual value: ^                 |
                    \-------------------------------- */
                    break;
                case 13:
                    op = CSharpOperatorCases.OpenBlock;
                    /* -----------------------\
                    |  Build OpenBlock here.  |
                    |  Actual value: {        |
                    \----------------------- */
                    break;
                case 5:
                    op = CSharpOperatorCases.BitwiseOr;
                    /* -----------------------\
                    |  Build BitwiseOr here.  |
                    |  Actual value: |        |
                    \----------------------- */
                    break;
                case 41:
                    op = CSharpOperatorCases.IsType;
                    /* --------------------\
                    |  Build IsType here.  |
                    |  Actual value: is    |
                    \-------------------- */
                    break;
                case 42:
                    op = CSharpOperatorCases.CastToOrNull;
                    /* --------------------------\
                    |  Build CastToOrNull here.  |
                    |  Actual value: as          |
                    \-------------------------- */
                    break;
                case 40:
                    op = CSharpOperatorCases.BitwiseOrAssign;
                    /* -----------------------------\
                    |  Build BitwiseOrAssign here.  |
                    |  Actual value: |=             |
                    \----------------------------- */
                    break;
                case 35:
                    op = CSharpOperatorCases.ConditionalOr;
                    /* ---------------------------\
                    |  Build ConditionalOr here.  |
                    |  Actual value: ||           |
                    \--------------------------- */
                    break;
                case 43:
                    op = CSharpOperatorCases.BitwiseExclusiveOrAssign;
                    /* --------------------------------------\
                    |  Build BitwiseExclusiveOrAssign here.  |
                    |  Actual value: ^=                      |
                    \-------------------------------------- */
                    break;
                case 44:
                    op = CSharpOperatorCases.GreaterThanOrEqualTo;
                    /* ----------------------------------\
                    |  Build GreaterThanOrEqualTo here.  |
                    |  Actual value: >=                  |
                    \---------------------------------- */
                    break;
                case 25:
                    op = CSharpOperatorCases.RightShift;
                    /* ------------------------\
                    |  Build RightShift here.  |
                    |  Actual value: >>        |
                    \------------------------ */
                    break;
                case 26:
                    op = CSharpOperatorCases.Equality;
                    /* ----------------------\
                    |  Build Equality here.  |
                    |  Actual value: ==      |
                    \---------------------- */
                    break;
                case 36:
                    op = CSharpOperatorCases.LeftShift;
                    /* -----------------------\
                    |  Build LeftShift here.  |
                    |  Actual value: <<       |
                    \----------------------- */
                    break;
                case 27:
                    op = CSharpOperatorCases.LessThanOrEqualTo;
                    /* -------------------------------\
                    |  Build LessThanOrEqualTo here.  |
                    |  Actual value: <=               |
                    \------------------------------- */
                    break;
                case 37:
                    op = CSharpOperatorCases.Decrement;
                    /* -----------------------\
                    |  Build Decrement here.  |
                    |  Actual value: --       |
                    \----------------------- */
                    break;
                case 29:
                    op = CSharpOperatorCases.SubtAssign;
                    /* ------------------------\
                    |  Build SubtAssign here.  |
                    |  Actual value: -=        |
                    \------------------------ */
                    break;
                case 31:
                    op = CSharpOperatorCases.MultAssign;
                    /* ------------------------\
                    |  Build MultAssign here.  |
                    |  Actual value: *=        |
                    \------------------------ */
                    break;
                case 33:
                    op = CSharpOperatorCases.ModAssign;
                    /* -----------------------\
                    |  Build ModAssign here.  |
                    |  Actual value: %=       |
                    \----------------------- */
                    break;
                case 28:
                    op = CSharpOperatorCases.DivAssign;
                    /* -----------------------\
                    |  Build DivAssign here.  |
                    |  Actual value: /=       |
                    \----------------------- */
                    break;
                case 38:
                    op = CSharpOperatorCases.Increment;
                    /* -----------------------\
                    |  Build Increment here.  |
                    |  Actual value: ++       |
                    \----------------------- */
                    break;
                case 30:
                    op = CSharpOperatorCases.AddAssign;
                    /* -----------------------\
                    |  Build AddAssign here.  |
                    |  Actual value: +=       |
                    \----------------------- */
                    break;
                case 39:
                    op = CSharpOperatorCases.ConditionalAnd;
                    /* ----------------------------\
                    |  Build ConditionalAnd here.  |
                    |  Actual value: &&            |
                    \---------------------------- */
                    break;
                case 32:
                    op = CSharpOperatorCases.BitwiseAndAssign;
                    /* ------------------------------\
                    |  Build BitwiseAndAssign here.  |
                    |  Actual value: &=              |
                    \------------------------------ */
                    break;
                case 34:
                    op = CSharpOperatorCases.Inequality;
                    /* ------------------------\
                    |  Build Inequality here.  |
                    |  Actual value: !=        |
                    \------------------------ */
                    break;
                case 46:
                    op = CSharpOperatorCases.RShiftAssign;
                    /* --------------------------\
                    |  Build RShiftAssign here.  |
                    |  Actual value: >>=         |
                    \-------------------------- */
                    break;
                case 45:
                    op = CSharpOperatorCases.LShiftAssign;
                    /* --------------------------\
                    |  Build LShiftAssign here.  |
                    |  Actual value: <<=         |
                    \-------------------------- */
                    break;
                case 48:
                    op = CSharpOperatorCases.NamespaceAliasQualifier;
                    /* -------------------------------------\
                    |  Build NamespaceAliasQualifier here.  |
                    |  Actual value: ::                     |
                    \------------------------------------- */
                    break;
            }
            if (op == CSharpOperatorCases.None)
                throw new InvalidOperationException("Invalid state.");
            return new CSharpOperatorToken(location, op);
        }

        #endregion

        #region ITokenizerStateMachine Members

        public bool Next(char @char)
        {
            switch (this.state)
            {
                case 0:
                    switch (@char)
                    {
                        case ':':
                            this.state = 47;
                            return true;
                        case '!':
                            this.state = (this.exitState = 24);
                            /* -----------------------------------------------\
                            |  Yields BooleanInversion if the next character  |
                            |  in the sequence isn't encountered.             |
                            \----------------------------------------------- */
                            return true;
                        case '%':
                            this.state = (this.exitState = 12);
                            /* ----------------------------------------\
                            |  Yields Remainder if the next character  |
                            |  in the sequence isn't encountered.      |
                            \---------------------------------------- */
                            return true;
                        case '&':
                            this.state = (this.exitState = 23);
                            /* -----------------------------------------\
                            |  Yields BitwiseAnd if the next character  |
                            |  in the sequence isn't encountered.       |
                            \----------------------------------------- */
                            return true;
                        case '(':
                            this.state = (this.exitState = 22);
                            // Yields: LeftParenthesis
                            return false;
                        case ')':
                            this.state = (this.exitState = 21);
                            // Yields: RightParenthesis
                            return false;
                        case '*':
                            this.state = (this.exitState = 11);
                            /* ---------------------------------------------\
                            |  Yields Multiplication if the next character  |
                            |  in the sequence isn't encountered.           |
                            \--------------------------------------------- */
                            return true;
                        case '+':
                            this.state = (this.exitState = 20);
                            /* ---------------------------------------\
                            |  Yields Addition if the next character  |
                            |  in the sequence isn't encountered.     |
                            \--------------------------------------- */
                            return true;
                        case ',':
                            this.state = (this.exitState = 19);
                            // Yields: Comma
                            return false;
                        case '-':
                            this.state = (this.exitState = 10);
                            /* ------------------------------------------\
                            |  Yields Subtraction if the next character  |
                            |  in the sequence isn't encountered.        |
                            \------------------------------------------ */
                            return true;
                        case '.':
                            this.state = (this.exitState = 18);
                            // Yields: MemberSeparator
                            return false;
                        case '/':
                            this.state = (this.exitState = 17);
                            /* ---------------------------------------\
                            |  Yields Division if the next character  |
                            |  in the sequence isn't encountered.     |
                            \--------------------------------------- */
                            return true;
                        case ';':
                            this.state = (this.exitState = 16);
                            // Yields: EntryTerminal
                            return false;
                        case '<':
                            this.state = (this.exitState = 9);
                            /* ---------------------------------------\
                            |  Yields LessThan if the next character  |
                            |  in the sequence isn't encountered.     |
                            \--------------------------------------- */
                            return true;
                        case '=':
                            this.state = (this.exitState = 8);
                            /* -------------------------------------------\
                            |  Yields SimpleAssign if the next character  |
                            |  in the sequence isn't encountered.         |
                            \------------------------------------------- */
                            return true;
                        case '>':
                            this.state = (this.exitState = 7);
                            /* ------------------------------------------\
                            |  Yields GreaterThan if the next character  |
                            |  in the sequence isn't encountered.        |
                            \------------------------------------------ */
                            return true;
                        case '[':
                            this.state = (this.exitState = 15);
                            // Yields: LeftSquareBracket
                            return false;
                        case ']':
                            this.state = (this.exitState = 14);
                            // Yields: RightSquareBracket
                            return false;
                        case '^':
                            this.state = (this.exitState = 6);
                            /* -------------------------------------------------\
                            |  Yields BitwiseExclusiveOr if the next character  |
                            |  in the sequence isn't encountered.               |
                            \------------------------------------------------- */
                            return true;
                        case 'a':
                            this.state = 2;
                            // Current: a
                            return true;
                        case 'i':
                            this.state = 1;
                            // Current: i
                            return true;
                        case '{':
                            this.state = (this.exitState = 13);
                            // Yields: OpenBlock
                            return false;
                        case '|':
                            this.state = (this.exitState = 5);
                            /* ----------------------------------------\
                            |  Yields BitwiseOr if the next character  |
                            |  in the sequence isn't encountered.      |
                            \---------------------------------------- */
                            return true;
                        case '}':
                            this.state = 4;
                            // Current: CloseBlock
                            return true;
                    }
                    break;
                case 1:
                    if ((@char == 's'))
                    {
                        this.state = (this.exitState = 41);
                        // Yields: IsType
                        return false;
                    }
                    break;
                case 2:
                    if ((@char == 's'))
                    {
                        this.state = (this.exitState = 42);
                        // Yields: CastToOrNull
                        return false;
                    }
                    break;
                case 5:
                    switch (@char)
                    {
                        case '=':
                            this.state = (this.exitState = 40);
                            // Yields: BitwiseOrAssign
                            return false;
                        case '|':
                            this.state = (this.exitState = 35);
                            // Yields: ConditionalOr
                            return false;
                    }
                    break;
                case 6:
                    if ((@char == '='))
                    {
                        this.state = (this.exitState = 43);
                        // Yields: BitwiseExclusiveOrAssign
                        return false;
                    }
                    break;
                case 7:
                    switch (@char)
                    {
                        case '=':
                            this.state = (this.exitState = 44);
                            // Yields: GreaterThanOrEqualTo
                            return false;
                        case '>':
                            this.state = (this.exitState = 25);
                            /* -----------------------------------------\
                            |  Yields RightShift if the next character  |
                            |  in the sequence isn't encountered.       |
                            \----------------------------------------- */
                            return true;
                    }
                    break;
                case 8:
                    if ((@char == '='))
                    {
                        this.state = (this.exitState = 26);
                        // Yields: Equality
                        return false;
                    }
                    break;
                case 9:
                    switch (@char)
                    {
                        case '<':
                            this.state = (this.exitState = 36);
                            /* ----------------------------------------\
                            |  Yields LeftShift if the next character  |
                            |  in the sequence isn't encountered.      |
                            \---------------------------------------- */
                            return true;
                        case '=':
                            this.state = (this.exitState = 27);
                            // Yields: LessThanOrEqualTo
                            return false;
                    }
                    break;
                case 10:
                    switch (@char)
                    {
                        case '-':
                            this.state = (this.exitState = 37);
                            // Yields: Decrement
                            return false;
                        case '=':
                            this.state = (this.exitState = 29);
                            // Yields: SubtAssign
                            return false;
                    }
                    break;
                case 11:
                    if ((@char == '='))
                    {
                        this.state = (this.exitState = 31);
                        // Yields: MultAssign
                        return false;
                    }
                    break;
                case 12:
                    if ((@char == '='))
                    {
                        this.state = (this.exitState = 33);
                        // Yields: ModAssign
                        return false;
                    }
                    break;
                case 17:
                    if ((@char == '='))
                    {
                        this.state = (this.exitState = 28);
                        // Yields: DivAssign
                        return false;
                    }
                    break;
                case 20:
                    switch (@char)
                    {
                        case '+':
                            this.state = (this.exitState = 38);
                            // Yields: Increment
                            return false;
                        case '=':
                            this.state = (this.exitState = 30);
                            // Yields: AddAssign
                            return false;
                    }
                    break;
                case 23:
                    switch (@char)
                    {
                        case '&':
                            this.state = (this.exitState = 39);
                            // Yields: ConditionalAnd
                            return false;
                        case '=':
                            this.state = (this.exitState = 32);
                            // Yields: BitwiseAndAssign
                            return false;
                    }
                    break;
                case 24:
                    if ((@char == '='))
                    {
                        this.state = (this.exitState = 34);
                        // Yields: Inequality
                        return false;
                    }
                    break;
                case 25:
                    if ((@char == '='))
                    {
                        this.state = (this.exitState = 46);
                        // Yields: RShiftAssign
                        return false;
                    }
                    break;
                case 36:
                    if ((@char == '='))
                    {
                        this.state = (this.exitState = 45);
                        // Yields: LShiftAssign
                        return false;
                    }
                    break;
                case 47:
                    if (@char == ':')
                    {
                        this.state = (this.exitState = 48);
                        // Yields NamespaceAliasQualifier.
                        return false;
                    }
                    break;
            }
            return false;
        }

        public bool IsValidEndState
        {
            get { return this.exitState != 0; }
        }

        public void Reset()
        {
            this.state = this.exitState = 0;
        }

        IToken ITokenizerStateMachine.GetToken(FileLocale location)
        {
            return this.GetToken(location);
        }

        public ulong BytesConsumed
        {
            get
            {
                switch (this.exitState)
                {
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                        return 1;
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                    case 41:
                    case 42:
                    case 43:
                    case 44:
                    case 48:
                        return 2;
                    case 45:
                    case 46:
                        return 3;
                    default:
                        return 0;
                }
            }
        }

        #endregion
    }
}
