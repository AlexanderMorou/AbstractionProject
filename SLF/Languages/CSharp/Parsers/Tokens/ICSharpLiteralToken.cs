using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers.Tokens
{
    [Flags]
    public enum CSharpLiteralType
    {
        Number      = 0x00000001,
        String      = 0x00000002,
        Character   = 0x00000004,
    }
    public enum CSharpNumberDataType
    {
        Single,
        Double,
        Decimal,
        Int32,
        UInt32,
        Int64,
        UInt64,
    }

    public interface ICSharpLiteralToken
    {
        /// <summary>
        /// Returns the <see cref="IPrimitiveExpression"/> that makes up the literal value.
        /// </summary>
        IPrimitiveExpression Value { get; }
        /// <summary>
        /// Returns the <see cref="CSharpLiteralType"/> which indicates whether the literal
        /// was a number, string, or character.
        /// </summary>
        /// <remarks><para><see cref="CSharpLiteralType"/> allows multiple literal kinds to be expressed
        /// in unison; however, implementations of <see cref="ICSharpLiteralToken"/> should yield
        /// a single value.</para>
        /// <para>The <see cref="FlagsAttribute"/> was applied to express state-machine awareness on what
        /// possible values are allowable at any given point.</para></remarks>
        CSharpLiteralType LiteralType { get; }
    }
}
