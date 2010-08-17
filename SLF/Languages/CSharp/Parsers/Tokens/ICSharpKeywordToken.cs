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

    [Flags]
    public enum CSharpKeywords1 :
        ulong
    {
        None                    = 0x0000000000000000,
        ///<summary>
        ///C&#9839; Keyword for "protected" which is 9 characters long.
        ///</summary>
        Protected               = 0x0000000000000001,
        ///<summary>
        ///C&#9839; Keyword for "private" which is 7 characters long.
        ///</summary>
        Private                 = 0x0000000000000002,
        ///<summary>
        ///C&#9839; Keyword for "public" which is 6 characters long.
        ///</summary>
        Public                  = 0x0000000000000004,
        ///<summary>
        ///C&#9839; Keyword for "internal" which is 8 characters long.
        ///</summary>
        Internal                = 0x0000000000000008,
        ///<summary>
        ///C&#9839; Keyword for "enum" which is 4 characters long.
        ///</summary>
        Enum                    = 0x0000000000000010,
        ///<summary>
        ///C&#9839; Keyword for "abstract" which is 8 characters long.
        ///</summary>
        Abstract                = 0x0000000000000020,
        ///<summary>
        ///C&#9839; Keyword for "as" which is 2 characters long.
        ///</summary>
        As                      = 0x0000000000000040,
        ///<summary>
        ///C&#9839; Keyword for "base" which is 4 characters long.
        ///</summary>
        Base                    = 0x0000000000000080,
        ///<summary>
        ///C&#9839; Keyword for "break" which is 5 characters long.
        ///</summary>
        Break                   = 0x0000000000000100,
        ///<summary>
        ///C&#9839; Keyword for "case" which is 4 characters long.
        ///</summary>
        Case                    = 0x0000000000000200,
        ///<summary>
        ///C&#9839; Keyword for "catch" which is 5 characters long.
        ///</summary>
        Catch                   = 0x0000000000000400,
        ///<summary>
        ///C&#9839; Keyword for "checked" which is 7 characters long.
        ///</summary>
        Checked                 = 0x0000000000000800,
        ///<summary>
        ///C&#9839; Keyword for "class" which is 5 characters long.
        ///</summary>
        Class                   = 0x0000000000001000,
        ///<summary>
        ///C&#9839; Keyword for "const" which is 5 characters long.
        ///</summary>
        Const                   = 0x0000000000002000,
        ///<summary>
        ///C&#9839; Keyword for "continue" which is 8 characters long.
        ///</summary>
        Continue                = 0x0000000000004000,
        ///<summary>
        ///C&#9839; Keyword for "default" which is 7 characters long.
        ///</summary>
        Default                 = 0x0000000000008000,
        ///<summary>
        ///C&#9839; Keyword for "delegate" which is 8 characters long.
        ///</summary>
        Delegate                = 0x0000000000010000,
        ///<summary>
        ///C&#9839; Keyword for "do" which is 2 characters long.
        ///</summary>
        Do                      = 0x0000000000020000,
        ///<summary>
        ///C&#9839; Keyword for "else" which is 4 characters long.
        ///</summary>
        Else                    = 0x0000000000040000,
        ///<summary>
        ///C&#9839; Keyword for "event" which is 5 characters long.
        ///</summary>
        Event                   = 0x0000000000080000,
        ///<summary>
        ///C&#9839; Keyword for "explicit" which is 8 characters long.
        ///</summary>
        Explicit                = 0x0000000000100000,
        ///<summary>
        ///C&#9839; Keyword for "extern" which is 6 characters long.
        ///</summary>
        Extern                  = 0x0000000000200000,
        ///<summary>
        ///C&#9839; Keyword for "finally" which is 7 characters long.
        ///</summary>
        Finally                 = 0x0000000000400000,
        ///<summary>
        ///C&#9839; Keyword for "fixed" which is 5 characters long.
        ///</summary>
        Fixed                   = 0x0000000000800000,
        ///<summary>
        ///C&#9839; Keyword for "for" which is 3 characters long.
        ///</summary>
        For                     = 0x0000000001000000,
        ///<summary>
        ///C&#9839; Keyword for "foreach" which is 7 characters long.
        ///</summary>
        ForEach                 = 0x0000000002000000,
        ///<summary>
        ///C&#9839; Keyword for "get" which is 3 characters long.
        ///</summary>
        Get                     = 0x0000000004000000,
        ///<summary>
        ///C&#9839; Keyword for "goto" which is 4 characters long.
        ///</summary>
        GoTo                    = 0x0000000008000000,
        ///<summary>
        ///C&#9839; Keyword for "if" which is 2 characters long.
        ///</summary>
        If                      = 0x0000000010000000,
        ///<summary>
        ///C&#9839; Keyword for "implicit" which is 8 characters long.
        ///</summary>
        Implicit                = 0x0000000020000000,
        ///<summary>
        ///C&#9839; Keyword for "in" which is 2 characters long.
        ///</summary>
        In                      = 0x0000000040000000,
        ///<summary>
        ///C&#9839; Keyword for "interface" which is 9 characters long.
        ///</summary>
        Interface               = 0x0000000080000000,
        ///<summary>
        ///C&#9839; Keyword for "is" which is 2 characters long.
        ///</summary>
        ///<summary>
        ///C&#9839; Keyword for "lock" which is 4 characters long.
        ///</summary>
        Is                      = 0x0000000100000000,
        ///<summary>
        ///C&#9839; Keyword for "lock" which is 4 characters long.
        ///</summary>
        Lock                    = 0x0000000200000000,
        ///<summary>
        ///C&#9839; Keyword for "namespace" which is 9 characters long.
        ///</summary>
        Namespace               = 0x0000000400000000,
        ///<summary>
        ///C&#9839; Keyword for "new" which is 3 characters long.
        ///</summary>
        New                     = 0x0000000800000000,
        ///<summary>
        ///C&#9839; Keyword for "null" which is 4 characters long.
        ///</summary>
        Null                    = 0x0000001000000000,
        ///<summary>
        ///C&#9839; Keyword for "operator" which is 8 characters long.
        ///</summary>
        Operator                = 0x0000002000000000,
        ///<summary>
        ///C&#9839; Keyword for "out" which is 3 characters long.
        ///</summary>
        Out                     = 0x0000004000000000,
        ///<summary>
        ///C&#9839; Keyword for "override" which is 8 characters long.
        ///</summary>
        Override                = 0x0000008000000000,
        ///<summary>
        ///C&#9839; Keyword for "params" which is 6 characters long.
        ///</summary>
        Params                  = 0x0000010000000000,
        ///<summary>
        ///C&#9839; Keyword for "partial" which is 7 characters long.
        ///</summary>
        Partial                 = 0x0000020000000000,
        ///<summary>
        ///C&#9839; Keyword for "readonly" which is 8 characters long.
        ///</summary>
        ReadOnly                = 0x0000040000000000,
        ///<summary>
        ///C&#9839; Keyword for "ref" which is 3 characters long.
        ///</summary>
        Ref                     = 0x0000080000000000,
        ///<summary>
        ///C&#9839; Keyword for "return" which is 6 characters long.
        ///</summary>
        Return                  = 0x0000100000000000,
        ///<summary>
        ///C&#9839; Keyword for "sealed" which is 6 characters long.
        ///</summary>
        Sealed                  = 0x0000200000000000,
        ///<summary>
        ///C&#9839; Keyword for "set" which is 3 characters long.
        ///</summary>
        Set                     = 0x0000400000000000,
        ///<summary>
        ///C&#9839; Keyword for "sizeof" which is 6 characters long.
        ///</summary>
        SizeOf                  = 0x0000800000000000,
        ///<summary>
        ///C&#9839; Keyword for "stackalloc" which is 10 characters long.
        ///</summary>
        StackAlloc              = 0x0001000000000000,
        ///<summary>
        ///C&#9839; Keyword for "static" which is 6 characters long.
        ///</summary>
        Static                  = 0x0002000000000000,
        ///<summary>
        ///C&#9839; Keyword for "struct" which is 6 characters long.
        ///</summary>
        Struct                  = 0x0004000000000000,
        ///<summary>
        ///C&#9839; Keyword for "switch" which is 6 characters long.
        ///</summary>
        Switch                  = 0x0008000000000000,
        ///<summary>
        ///C&#9839; Keyword for "this" which is 4 characters long.
        ///</summary>
        This                    = 0x0010000000000000,
        ///<summary>
        ///C&#9839; Keyword for "throw" which is 5 characters long.
        ///</summary>
        Throw                   = 0x0020000000000000,
        ///<summary>
        ///C&#9839; Keyword for "try" which is 3 characters long.
        ///</summary>
        Try                     = 0x0040000000000000,
        ///<summary>
        ///C&#9839; Keyword for "typeof" which is 6 characters long.
        ///</summary>
        TypeOf                  = 0x0080000000000000,
        ///<summary>
        ///C&#9839; Keyword for "unchecked" which is 9 characters long.
        ///</summary>
        UnChecked               = 0x0100000000000000,
        ///<summary>
        ///C&#9839; Keyword for "unsafe" which is 6 characters long.
        ///</summary>
        UnSafe                  = 0x0200000000000000,
        ///<summary>
        ///C&#9839; Keyword for "using" which is 5 characters long.
        ///</summary>
        Using                   = 0x0400000000000000,
        ///<summary>
        ///C&#9839; Keyword for "virtual" which is 7 characters long.
        ///</summary>
        Virtual                 = 0x0800000000000000,
        ///<summary>
        ///C&#9839; Keyword for "volatile" which is 8 characters long.
        ///</summary>
        Volatile                = 0x1000000000000000,
        ///<summary>
        ///C&#9839; Keyword for "where" which is 5 characters long.
        ///</summary>
        Where                   = 0x2000000000000000,
        ///<summary>
        ///C&#9839; Keyword for "while" which is 5 characters long.
        ///</summary>
        While                   = 0x4000000000000000,
        ///<summary>
        ///C&#9839; Keyword for "yield" which is 5 characters long.
        ///</summary>
        Yield                   = 0x8000000000000000,
    }
    [Flags]
    [CLSCompliant(false)]
    public enum CSharpKeywords2 :
        ulong
    {
        None                    = 0x0000000000000000,
        ///<summary>
        ///C&#9839; Keyword for "__arglist" which is 9 characters long.
        ///</summary>
        __ArgList               = 0x0000000000000001,
        ///<summary>
        ///C&#9839; Keyword for "__makeref" which is 9 characters long.
        ///</summary>
        __MakeRef               = 0x0000000000000002,
        ///<summary>
        ///C&#9839; Keyword for "__reftype" which is 9 characters long.
        ///</summary>
        __RefType               = 0x0000000000000004,
        ///<summary>
        ///C&#9839; Keyword for "__refvalue" which is 10 characters long.
        ///</summary>
        __RefValue              = 0x0000000000000008,
        ///<summary>
        ///C&#9839; Keyword for "bool" which is 4 characters long.
        ///</summary>
        Boolean                 = 0x0000000000000010,
        ///<summary>
        ///C&#9839; Keyword for "sbyte" which is 5 characters long.
        ///</summary>
        SByte                   = 0x0000000000000020,
        ///<summary>
        ///C&#9839; Keyword for "byte" which is 4 characters long.
        ///</summary>
        Byte                    = 0x0000000000000040,
        ///<summary>
        ///C&#9839; Keyword for "decimal" which is 7 characters long.
        ///</summary>
        Decimal                 = 0x0000000000000080,
        ///<summary>
        ///C&#9839; Keyword for "ushort" which is 6 characters long.
        ///</summary>
        UInt16                  = 0x0000000000000100,
        ///<summary>
        ///C&#9839; Keyword for "uint" which is 4 characters long.
        ///</summary>
        UInt32                  = 0x0000000000000200,
        ///<summary>
        ///C&#9839; Keyword for "ulong" which is 5 characters long.
        ///</summary>
        UInt64                  = 0x0000000000000400,
        ///<summary>
        ///C&#9839; Keyword for "void" which is 4 characters long.
        ///</summary>
        Void                    = 0x0000000000000800,
        ///<summary>
        ///C&#9839; Keyword for "short" which is 5 characters long.
        ///</summary>
        Int16                   = 0x0000000000001000,
        ///<summary>
        ///C&#9839; Keyword for "int" which is 3 characters long.
        ///</summary>
        Int32                   = 0x0000000000002000,
        ///<summary>
        ///C&#9839; Keyword for "long" which is 4 characters long.
        ///</summary>
        Int64                   = 0x0000000000004000,
        ///<summary>
        ///C&#9839; Keyword for "float" which is 5 characters long.
        ///</summary>
        Float                   = 0x0000000000008000,
        ///<summary>
        ///C&#9839; Keyword for "double" which is 6 characters long.
        ///</summary>
        Double                  = 0x0000000000010000,
        ///<summary>
        ///C&#9839; Keyword for "string" which is 6 characters long.
        ///</summary>
        String                  = 0x0000000000020000,
        ///<summary>
        ///C&#9839; Keyword for "char" which is 4 characters long.
        ///</summary>
        Char                    = 0x0000000000040000,
        ///<summary>
        ///C&#9839; Keyword for "object" which is 6 characters long.
        ///</summary>
        Object                  = 0x0000000000080000,
        ///<summary>
        ///C&#9839; Keyword for "false" which is 5 characters long.
        ///</summary>
        False                   = 0x0000000000100000,
        ///<summary>
        ///C&#9839; Keyword for "true" which is 4 characters long.
        ///</summary>
        True                    = 0x0000000000200000,
        /// <summary>
        /// C&#9839; Keyword for "global" which is 6 characters long.
        /// </summary>
        Global                  = 0x0000000000400000,
        From                    = 0x0000000000800000,
        Join                    = 0x0000000001000000,
        Into                    = 0x0000000002000000,
        Select                  = 0x0000000004000000,
        Equals                  = 0x0000000008000000,
        Group                   = 0x0000000010000000,
        By                      = 0x0000000020000000,
        OrderBy                 = 0x0000000040000000,
        Ascending               = 0x0000000080000000,
        Descending              = 0x0000000100000000,
        On                      = 0x0000000200000000,
    }
    [Flags]
    public enum CSharpKeywordCases
    {
        None                    = 0x0000000000000000,
        CaseA                   = 0x0000000000000001,
        CaseB                   = 0x0000000000000002,
        Both                    = 0x0000000000000003,
    }
    /// <summary>
    /// Defines properties and methods for working with a keyword in the C&#9839; langauge.
    /// </summary>
    public interface ICSharpKeywordToken :
        IToken
    {
        /// <summary>
        /// Returns which keyword set the <see cref="ICSharpKeywordToken"/>
        /// is relative to.
        /// </summary>
        CSharpKeywordCases Case { get; }
        /// <summary>
        /// If <see cref="Case"/> is <see cref="CSharpKeywordCases.CaseA"/>, returns the
        /// keyword the <see cref="ICSharpKeywordToken"/> is.
        /// </summary>
        CSharpKeywords1 KeywordA { get; }
        /// <summary>
        /// If <see cref="Case"/> is <see cref="CSharpKeywordCases.CaseB"/>, returns the
        /// keyword the <see cref="ICSharpKeywordToken"/> is.
        /// </summary>
        CSharpKeywords2 KeywordB { get; }
    }
}
