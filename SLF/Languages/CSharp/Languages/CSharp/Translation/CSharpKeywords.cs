using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation
{
    public enum CSharpKeywords
    {
        /// <summary>
        /// Represents a keyword for the CSharp Language, 2 characters long; expressed as "as".
        ///</summary>
        As,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 2 characters long; expressed as "do".
        ///</summary>
        Do,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 2 characters long; expressed as "if".
        ///</summary>
        If,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 2 characters long; expressed as "in".
        ///</summary>
        In,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 2 characters long; expressed as "is".
        ///</summary>
        Is,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 3 characters long; expressed as "for".
        ///</summary>
        For,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 3 characters long; expressed as "get".
        ///</summary>
        Get,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 3 characters long; expressed as "int".
        ///</summary>
        Int,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 3 characters long; expressed as "let".
        ///</summary>
        Let,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 3 characters long; expressed as "new".
        ///</summary>
        New,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 3 characters long; expressed as "out".
        ///</summary>
        Out,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 3 characters long; expressed as "ref".
        ///</summary>
        Ref,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 3 characters long; expressed as "set".
        ///</summary>
        Set,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 3 characters long; expressed as "try".
        ///</summary>
        Try,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 3 characters long; expressed as "var".
        ///</summary>
        Var,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "base".
        ///</summary>
        Base,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "bool".
        ///</summary>
        Bool,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "byte".
        ///</summary>
        Byte,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "case".
        ///</summary>
        Case,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "char".
        ///</summary>
        Char,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "else".
        ///</summary>
        Else,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "enum".
        ///</summary>
        Enum,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "from".
        ///</summary>
        From,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "goto".
        ///</summary>
        Goto,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "join".
        ///</summary>
        Join,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "lock".
        ///</summary>
        Lock,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "long".
        ///</summary>
        Long,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "null".
        ///</summary>
        Null,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "this".
        ///</summary>
        This,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "true".
        ///</summary>
        True,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "type".
        ///</summary>
        Type,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "uint".
        ///</summary>
        Uint,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 4 characters long; expressed as "void".
        ///</summary>
        Void,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "async".
        ///</summary>
        Async,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "await".
        ///</summary>
        Await,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "break".
        ///</summary>
        Break,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "catch".
        ///</summary>
        Catch,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "class".
        ///</summary>
        Class,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "const".
        ///</summary>
        Const,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "event".
        ///</summary>
        Event,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "false".
        ///</summary>
        False,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "field".
        ///</summary>
        Field,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "fixed".
        ///</summary>
        Fixed,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "float".
        ///</summary>
        Float,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "group".
        ///</summary>
        Group,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "param".
        ///</summary>
        Param,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "sbyte".
        ///</summary>
        Sbyte,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "short".
        ///</summary>
        Short,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "throw".
        ///</summary>
        Throw,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "ulong".
        ///</summary>
        Ulong,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "using".
        ///</summary>
        Using,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "where".
        ///</summary>
        Where,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "while".
        ///</summary>
        While,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 5 characters long; expressed as "yield".
        ///</summary>
        Yield,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "double".
        ///</summary>
        Double,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "equals".
        ///</summary>
        Equals,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "extern".
        ///</summary>
        Extern,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "method".
        ///</summary>
        Method,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "module".
        ///</summary>
        Module,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "object".
        ///</summary>
        Object,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "params".
        ///</summary>
        Params,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "public".
        ///</summary>
        Public,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "return".
        ///</summary>
        Return,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "sealed".
        ///</summary>
        Sealed,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "select".
        ///</summary>
        Select,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "sizeof".
        ///</summary>
        Sizeof,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "static".
        ///</summary>
        Static,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "string".
        ///</summary>
        String,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "struct".
        ///</summary>
        Struct,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "switch".
        ///</summary>
        Switch,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "typeof".
        ///</summary>
        Typeof,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "unsafe".
        ///</summary>
        Unsafe,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 6 characters long; expressed as "ushort".
        ///</summary>
        Ushort,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 7 characters long; expressed as "checked".
        ///</summary>
        Checked,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 7 characters long; expressed as "decimal".
        ///</summary>
        Decimal,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 7 characters long; expressed as "default".
        ///</summary>
        Default,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 7 characters long; expressed as "dynamic"
        ///</summary>
        Dynamic,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 7 characters long; expressed as "finally".
        ///</summary>
        Finally,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 7 characters long; expressed as "foreach".
        ///</summary>
        Foreach,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 7 characters long; expressed as "orderby".
        ///</summary>
        OrderBy,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 7 characters long; expressed as "partial".
        ///</summary>
        Partial,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 7 characters long; expressed as "private".
        ///</summary>
        Private,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 7 characters long; expressed as "virtual".
        ///</summary>
        Virtual,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "abstract".
        ///</summary>
        Abstract,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "assembly".
        ///</summary>
        Assembly,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "continue".
        ///</summary>
        Continue,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "delegate".
        ///</summary>
        Delegate,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "explicit".
        ///</summary>
        Explicit,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "implicit".
        ///</summary>
        Implicit,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "internal".
        ///</summary>
        Internal,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "operator".
        ///</summary>
        Operator,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "override".
        ///</summary>
        Override,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "property".
        ///</summary>
        Property,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "readonly".
        ///</summary>
        Readonly,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 8 characters long; expressed as "volatile".
        ///</summary>
        Volatile,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 9 characters long; expressed as "__arglist".
        ///</summary>
        __ArgList,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 9 characters long; expressed as "__makeref".
        ///</summary>
        __MakeRef,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 9 characters long; expressed as "__reftype".
        ///</summary>
        __RefType,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 9 characters long; expressed as "ascending".
        ///</summary>
        Ascending,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 9 characters long; expressed as "interface".
        ///</summary>
        Interface,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 9 characters long; expressed as "namespace".
        ///</summary>
        Namespace,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 9 characters long; expressed as "protected".
        ///</summary>
        Protected,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 9 characters long; expressed as "unchecked".
        ///</summary>
        Unchecked,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 10 characters long; expressed as "__refvalue".
        ///</summary>
        __RefValue,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 10 characters long; expressed as "descending".
        ///</summary>
        Descending,
        /// <summary>
        /// Represents a keyword for the CSharp Language, 10 characters long; expressed as "stackalloc".
        ///</summary>
        Stackalloc,
    };
};
