using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Tokens;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Parsers.StateMachines
{
    /* *
        "protected":Protected; |
        "private":Private; |
        "public":Public; |
        "internal":Internal; |
        "enum":Enum; |
        "abstract":Abstract; |
        "as":As; |
        "base":Base; |
        "break":Break; |
        "case":Case; |
        "catch":Catch; |
        "checked":Checked; |
        "class":Class; |
        "const":Const; |
        "continue":Continue; |
        "default":Default; |
        "delegate":Delegate; |
        "do":Do; |
        "else":Else; |
        "event":Event; |
        "explicit":Explicit; |
        "extern":Extern; |
        "finally":Finally; |
        "fixed":Fixed; |
        "for":For; |
        "foreach":ForEach; |
        "get":Get; |
        "goto":GoTo; |
        "if":If; |
        "implicit":Implicit; |
        "in":In; |
        "interface":Interface; |
        "is":Is; |
        "lock":Lock; |
        "namespace":Namespace; |
        "new":New; |
        "operator":Operator; |
        "out":Out; |
        "override":Override; |
        "params":Params; |
        "partial":Partial; |
        "readonly":ReadOnly; |
        "ref":Ref; |
        "return":Return; |
        "sealed":Sealed; |
        "set":Set; |
        "sizeof":SizeOf; |
        "stackalloc":StackAlloc; |
        "static":Static; |
        "struct":Struct; |
        "switch":Switch; |
        "this":This; |
        "throw":Throw; |
        "try":Try; |
        "typeof":TypeOf; |
        "unchecked":UnChecked; |
        "unsafe":Unsafe; |
        "using":Using; |
        "virtual":Virtual; |
        "volatile":Volatile; |
        "where":Where; |
        "while":While; |
        "yield":Yield; |
        "__arglist":__ArgList; |
        "__makeref":__MakeRef; |
        "__reftype":__RefType; |
        "__refvalue":__RefValue; |
        CommonPrimitiveValues |
        CommonPrimitiveDataTypes;
     * */
    /// <summary>
    /// Provides a state machine that transitions between the possible states needed to parse
    /// any keyword in the C&#9839; language.
    /// </summary>
    [CLSCompliant(false)]
    public class CSharpKeywordStateMachine :
        ITokenizerStateMachine<ICSharpKeywordToken>
    {
        private int state,
                    exitState;
        public bool IsValidEndState
        {
            get
            {
                return this.exitState != 0;
            }
        }
        /* *
         * This was written by an older project, but still effective.
         * */
        public bool Next(char @char)
        {
            switch (this.state)
            {
                case 0:
                    switch (@char)
                    {
                        case '_':
                            this.state = 283;
                            // Current: _
                            return true;
                        case 'a':
                            this.state = 253;
                            // Current: a
                            return true;
                        case 'b':
                            this.state = 105;
                            // Current: b
                            return true;
                        case 'c':
                            this.state = 103;
                            // Current: c
                            return true;
                        case 'd':
                            this.state = 34;
                            // Current: d
                            return true;
                        case 'e':
                            this.state = 197;
                            // Current: e
                            return true;
                        case 'f':
                            this.state = 98;
                            // Current: f
                            return true;
                        case 'g':
                            this.state = 27;
                            // Current: g
                            return true;
                        case 'i':
                            this.state = 194;
                            // Current: i
                            return true;
                        case 'j':
                            this.state = 295;
                            // Current: j
                            return true;
                        case 'l':
                            this.state = 184;
                            // Current: l
                            return true;
                        case 'n':
                            this.state = 23;
                            // Current: n
                            return true;
                        case 'o':
                            this.state = 207;
                            // Current: o
                            return true;
                        case 'p':
                            this.state = 89;
                            // Current: p
                            return true;
                        case 'r':
                            this.state = 15;
                            // Current: r
                            return true;
                        case 's':
                            this.state = 12;
                            // Current: s
                            return true;
                        case 't':
                            this.state = 149;
                            // Current: t
                            return true;
                        case 'u':
                            this.state = 115;
                            // Current: u
                            return true;
                        case 'v':
                            this.state = 132;
                            // Current: v
                            return true;
                        case 'w':
                            this.state = 225;
                            // Current: w
                            return true;
                        case 'y':
                            this.state = 130;
                            // Current: y
                            return true;
                    }
                    break;
                case 1:
                    if ((@char == 'e'))
                    {
                        this.state = 110;
                        // Current: yie
                        return true;
                    }
                    break;
                case 2:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 361);
                        // Yields: While
                        return false;
                    }
                    break;
                case 3:
                    switch (@char)
                    {
                        case 'e':
                            this.state = 146;
                            // Current: whe
                            return true;
                        case 'i':
                            this.state = 111;
                            // Current: whi
                            return true;
                    }
                    break;
                case 4:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 362);
                        // Yields: Where
                        return false;
                    }
                    break;
                case 5:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 404);
                        // Yields: Volatile
                        return false;
                    }
                    break;
                case 6:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 377);
                        // Yields: Unsafe
                        return false;
                    }
                    break;
                case 7:
                    if ((@char == 'e'))
                    {
                        this.state = 158;
                        // Current: unche
                        return true;
                    }
                    break;
                case 8:
                    if ((@char == 'e'))
                    {
                        this.state = 218;
                        // Current: unchecke
                        return true;
                    }
                    break;
                case 9:
                    if ((@char == 'e'))
                    {
                        this.state = 178;
                        // Current: type
                        return true;
                    }
                    break;
                case 10:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 345);
                        // Yields: True
                        return false;
                    }
                    break;
                case 11:
                    if ((@char == 'e'))
                    {
                        this.state = 180;
                        // Current: size
                        return true;
                    }
                    break;
                case 12:
                    switch (@char)
                    {
                        case 'b':
                            this.state = 230;
                            // Current: sb
                            return true;
                        case 'e':
                            this.state = 57;
                            // Current: se
                            return true;
                        case 'h':
                            this.state = 181;
                            // Current: sh
                            return true;
                        case 'i':
                            this.state = 257;
                            // Current: si
                            return true;
                        case 't':
                            this.state = 86;
                            // Current: st
                            return true;
                        case 'w':
                            this.state = 135;
                            // Current: sw
                            return true;
                    }
                    break;
                case 13:
                    if ((@char == 'e'))
                    {
                        this.state = 219;
                        // Current: seale
                        return true;
                    }
                    break;
                case 14:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 367);
                        // Yields: SByte
                        return false;
                    }
                    break;
                case 15:
                    if ((@char == 'e'))
                    {
                        this.state = 59;
                        // Current: re
                        return true;
                    }
                    break;
                case 16:
                    if ((@char == 'e'))
                    {
                        this.state = 164;
                        // Current: prote
                        return true;
                    }
                    break;
                case 17:
                    if ((@char == 'e'))
                    {
                        this.state = 221;
                        // Current: protecte
                        return true;
                    }
                    break;
                case 18:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 397);
                        // Yields: Private
                        return false;
                    }
                    break;
                case 19:
                    if ((@char == 'e'))
                    {
                        this.state = 153;
                        // Current: ove
                        return true;
                    }
                    break;
                case 20:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 406);
                        // Yields: Override
                        return false;
                    }
                    break;
                case 21:
                    if ((@char == 'e'))
                    {
                        this.state = 265;
                        // Current: ope
                        return true;
                    }
                    break;
                case 22:
                    if ((@char == 'e'))
                    {
                        this.state = 165;
                        // Current: obje
                        return true;
                    }
                    break;
                case 23:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 242;
                            // Current: na
                            return true;
                        case 'e':
                            this.state = 256;
                            // Current: ne
                            return true;
                        case 'u':
                            this.state = 267;
                            // Current: nu
                            return true;
                    }
                    break;
                case 24:
                    if ((@char == 'e'))
                    {
                        this.state = 202;
                        // Current: name
                        return true;
                    }
                    break;
                case 25:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 415);
                        // Yields: Namespace
                        return false;
                    }
                    break;
                case 26:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 416);
                        // Yields: Interface
                        return false;
                    }
                    break;
                case 27:
                    switch (@char)
                    {
                        case 'e':
                            this.state = 69;
                            // Current: ge
                            return true;
                        case 'l':
                            this.state = 289;
                            // Current: gl
                            return true;
                        case 'o':
                            this.state = 68;
                            // Current: go
                            return true;
                        case 'r':
                            this.state = 305;
                            // Current: gr
                            return true;
                    }
                    break;
                case 28:
                    if ((@char == 'e'))
                    {
                        this.state = 223;
                        // Current: fixe
                        return true;
                    }
                    break;
                case 29:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 370);
                        // Yields: False
                        return false;
                    }
                    break;
                case 30:
                    if ((@char == 'e'))
                    {
                        this.state = 272;
                        // Current: exte
                        return true;
                    }
                    break;
                case 31:
                    if ((@char == 'e'))
                    {
                        this.state = 196;
                        // Current: eve
                        return true;
                    }
                    break;
                case 32:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 352);
                        // Yields: Else
                        return false;
                    }
                    break;
                case 33:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 390);
                        // Yields: Double
                        return false;
                    }
                    break;
                case 34:
                    switch (@char)
                    {
                        case 'e':
                            this.state = 172;
                            // Current: de
                            return true;
                        case 'o':
                            this.state = (this.exitState = 329);
                            /* ------------------------------------\
                            |  Yields Do if the next character     |
                            |  in the sequence isn't encountered.  |
                            \------------------------------------ */
                            return true;
                    }
                    break;
                case 35:
                    if ((@char == 'e'))
                    {
                        this.state = 240;
                        // Current: dele
                        return true;
                    }
                    break;
                case 36:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 410);
                        // Yields: Delegate
                        return false;
                    }
                    break;
                case 37:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 411);
                        // Yields: Continue
                        return false;
                    }
                    break;
                case 38:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 278;
                            // Current: cha
                            return true;
                        case 'e':
                            this.state = 173;
                            // Current: che
                            return true;
                    }
                    break;
                case 39:
                    if ((@char == 'e'))
                    {
                        this.state = 224;
                        // Current: checke
                        return true;
                    }
                    break;
                case 40:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 353);
                        // Yields: Case
                        return false;
                    }
                    break;
                case 41:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 354);
                        // Yields: Byte
                        return false;
                    }
                    break;
                case 42:
                    if ((@char == 'e'))
                    {
                        this.state = 104;
                        // Current: bre
                        return true;
                    }
                    break;
                case 43:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 355);
                        // Yields: Base
                        return false;
                    }
                    break;
                case 44:
                    if ((@char == 'e'))
                    {
                        this.state = 214;
                        // Current: __re
                        return true;
                    }
                    break;
                case 45:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 422);
                        // Yields: __RefValue
                        return false;
                    }
                    break;
                case 46:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 417);
                        // Yields: __RefType
                        return false;
                    }
                    break;
                case 47:
                    if ((@char == 'e'))
                    {
                        this.state = 285;
                        // Current: __make
                        return true;
                    }
                    break;
                case 48:
                    if ((@char == 'e'))
                    {
                        this.state = 215;
                        // Current: __makere
                        return true;
                    }
                    break;
                case 49:
                    if ((@char == 't'))
                    {
                        this.state = 131;
                        // Current: volat
                        return true;
                    }
                    break;
                case 50:
                    if ((@char == 't'))
                    {
                        this.state = 204;
                        // Current: virt
                        return true;
                    }
                    break;
                case 51:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 376);
                        // Yields: UInt16
                        return false;
                    }
                    break;
                case 52:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 344);
                        // Yields: UInt32
                        return false;
                    }
                    break;
                case 53:
                    if ((@char == 't'))
                    {
                        this.state = 159;
                        // Current: swit
                        return true;
                    }
                    break;
                case 54:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 380);
                        // Yields: Struct
                        return false;
                    }
                    break;
                case 55:
                    switch (@char)
                    {
                        case 'c':
                            this.state = 246;
                            // Current: stac
                            return true;
                        case 't':
                            this.state = 137;
                            // Current: stat
                            return true;
                    }
                    break;
                case 56:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 366);
                        // Yields: Int16
                        return false;
                    }
                    break;
                case 57:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 260;
                            // Current: sea
                            return true;
                        case 'l':
                            this.state = 298;
                            // Current: sel
                            return true;
                        case 't':
                            this.state = (this.exitState = 336);
                            // Yields: Set
                            return false;
                    }
                    break;
                case 58:
                    if ((@char == 't'))
                    {
                        this.state = 14;
                        // Current: sbyt
                        return true;
                    }
                    break;
                case 59:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 220;
                            // Current: rea
                            return true;
                        case 'f':
                            this.state = (this.exitState = 337);
                            // Yields: Ref
                            return false;
                        case 't':
                            this.state = 206;
                            // Current: ret
                            return true;
                    }
                    break;
                case 60:
                    if ((@char == 't'))
                    {
                        this.state = 16;
                        // Current: prot
                        return true;
                    }
                    break;
                case 61:
                    if ((@char == 't'))
                    {
                        this.state = 17;
                        // Current: protect
                        return true;
                    }
                    break;
                case 62:
                    if ((@char == 't'))
                    {
                        this.state = 18;
                        // Current: privat
                        return true;
                    }
                    break;
                case 63:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 241;
                            // Current: para
                            return true;
                        case 't':
                            this.state = 139;
                            // Current: part
                            return true;
                    }
                    break;
                case 64:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 338);
                        // Yields: Out
                        return false;
                    }
                    break;
                case 65:
                    if ((@char == 't'))
                    {
                        this.state = 183;
                        // Current: operat
                        return true;
                    }
                    break;
                case 66:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 388);
                        // Yields: Object
                        return false;
                    }
                    break;
                case 67:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 408);
                        // Yields: Implicit
                        return false;
                    }
                    break;
                case 68:
                    if ((@char == 't'))
                    {
                        this.state = 185;
                        // Current: got
                        return true;
                    }
                    break;
                case 69:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 340);
                        // Yields: Get
                        return false;
                    }
                    break;
                case 70:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 368);
                        // Yields: Float
                        return false;
                    }
                    break;
                case 71:
                    switch (@char)
                    {
                        case 'p':
                            this.state = 273;
                            // Current: exp
                            return true;
                        case 't':
                            this.state = 30;
                            // Current: ext
                            return true;
                    }
                    break;
                case 72:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 409);
                        // Yields: Explicit
                        return false;
                    }
                    break;
                case 73:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 371);
                        // Yields: Event
                        return false;
                    }
                    break;
                case 74:
                    if ((@char == 't'))
                    {
                        this.state = 36;
                        // Current: delegat
                        return true;
                    }
                    break;
                case 75:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 400);
                        // Yields: Default
                        return false;
                    }
                    break;
                case 76:
                    switch (@char)
                    {
                        case 's':
                            this.state = 77;
                            // Current: cons
                            return true;
                        case 't':
                            this.state = 145;
                            // Current: cont
                            return true;
                    }
                    break;
                case 77:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 372);
                        // Yields: Const
                        return false;
                    }
                    break;
                case 78:
                    switch (@char)
                    {
                        case 's':
                            this.state = 40;
                            // Current: cas
                            return true;
                        case 't':
                            this.state = 174;
                            // Current: cat
                            return true;
                    }
                    break;
                case 79:
                    if ((@char == 't'))
                    {
                        this.state = 282;
                        // Current: abst
                        return true;
                    }
                    break;
                case 80:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 412);
                        // Yields: Abstract
                        return false;
                    }
                    break;
                case 81:
                    switch (@char)
                    {
                        case 't':
                            this.state = 231;
                            // Current: __reft
                            return true;
                        case 'v':
                            this.state = 107;
                            // Current: __refv
                            return true;
                    }
                    break;
                case 82:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 419);
                        // Yields: __ArgList
                        return false;
                    }
                    break;
                case 83:
                    if ((@char == 'a'))
                    {
                        this.state = 49;
                        // Current: vola
                        return true;
                    }
                    break;
                case 84:
                    if ((@char == 'a'))
                    {
                        this.state = 114;
                        // Current: virtua
                        return true;
                    }
                    break;
                case 85:
                    if ((@char == 'a'))
                    {
                        this.state = 211;
                        // Current: unsa
                        return true;
                    }
                    break;
                case 86:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 55;
                            // Current: sta
                            return true;
                        case 'r':
                            this.state = 136;
                            // Current: str
                            return true;
                    }
                    break;
                case 87:
                    if ((@char == 'a'))
                    {
                        this.state = 116;
                        // Current: stacka
                        return true;
                    }
                    break;
                case 88:
                    if ((@char == 'a'))
                    {
                        this.state = 62;
                        // Current: priva
                        return true;
                    }
                    break;
                case 89:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 152;
                            // Current: pa
                            return true;
                        case 'r':
                            this.state = 138;
                            // Current: pr
                            return true;
                        case 'u':
                            this.state = 251;
                            // Current: pu
                            return true;
                    }
                    break;
                case 90:
                    if ((@char == 'a'))
                    {
                        this.state = 263;
                        // Current: partia
                        return true;
                    }
                    break;
                case 91:
                    if ((@char == 'a'))
                    {
                        this.state = 65;
                        // Current: opera
                        return true;
                    }
                    break;
                case 92:
                    if ((@char == 'a'))
                    {
                        this.state = 166;
                        // Current: namespa
                        return true;
                    }
                    break;
                case 93:
                    if ((@char == 'a'))
                    {
                        this.state = 121;
                        // Current: interna
                        return true;
                    }
                    break;
                case 94:
                    if ((@char == 'a'))
                    {
                        this.state = 168;
                        // Current: interfa
                        return true;
                    }
                    break;
                case 95:
                    if ((@char == 'a'))
                    {
                        this.state = 170;
                        // Current: forea
                        return true;
                    }
                    break;
                case 96:
                    if ((@char == 'a'))
                    {
                        this.state = 70;
                        // Current: floa
                        return true;
                    }
                    break;
                case 97:
                    if ((@char == 'a'))
                    {
                        this.state = 123;
                        // Current: fina
                        return true;
                    }
                    break;
                case 98:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 271;
                            // Current: fa
                            return true;
                        case 'i':
                            this.state = 195;
                            // Current: fi
                            return true;
                        case 'l':
                            this.state = 186;
                            // Current: fl
                            return true;
                        case 'o':
                            this.state = 269;
                            // Current: fo
                            return true;
                        case 'r':
                            this.state = 293;
                            // Current: fr
                            return true;
                    }
                    break;
                case 99:
                    if ((@char == 'a'))
                    {
                        this.state = 74;
                        // Current: delega
                        return true;
                    }
                    break;
                case 100:
                    if ((@char == 'a'))
                    {
                        this.state = 209;
                        // Current: defa
                        return true;
                    }
                    break;
                case 101:
                    if ((@char == 'a'))
                    {
                        this.state = 276;
                        // Current: decima
                        return true;
                    }
                    break;
                case 102:
                    if ((@char == 'a'))
                    {
                        this.state = 203;
                        // Current: cla
                        return true;
                    }
                    break;
                case 103:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 78;
                            // Current: ca
                            return true;
                        case 'h':
                            this.state = 38;
                            // Current: ch
                            return true;
                        case 'l':
                            this.state = 102;
                            // Current: cl
                            return true;
                        case 'o':
                            this.state = 198;
                            // Current: co
                            return true;
                    }
                    break;
                case 104:
                    if ((@char == 'a'))
                    {
                        this.state = 249;
                        // Current: brea
                        return true;
                    }
                    break;
                case 105:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 280;
                            // Current: ba
                            return true;
                        case 'o':
                            this.state = 279;
                            // Current: bo
                            return true;
                        case 'r':
                            this.state = 42;
                            // Current: br
                            return true;
                        case 'y':
                            this.state = (this.exitState = 327);
                            /* ------------------------------------\
                            |  Yields By if the next character     |
                            |  in the sequence isn't encountered.  |
                            \------------------------------------ */
                            return true;
                    }
                    break;
                case 106:
                    if ((@char == 'a'))
                    {
                        this.state = 175;
                        // Current: abstra
                        return true;
                    }
                    break;
                case 107:
                    if ((@char == 'a'))
                    {
                        this.state = 284;
                        // Current: __refva
                        return true;
                    }
                    break;
                case 108:
                    if ((@char == 'a'))
                    {
                        this.state = 250;
                        // Current: __ma
                        return true;
                    }
                    break;
                case 109:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 286;
                            // Current: __a
                            return true;
                        case 'm':
                            this.state = 108;
                            // Current: __m
                            return true;
                        case 'r':
                            this.state = 44;
                            // Current: __r
                            return true;
                    }
                    break;
                case 110:
                    if ((@char == 'l'))
                    {
                        this.state = 216;
                        // Current: yiel
                        return true;
                    }
                    break;
                case 111:
                    if ((@char == 'l'))
                    {
                        this.state = 2;
                        // Current: whil
                        return true;
                    }
                    break;
                case 112:
                    switch (@char)
                    {
                        case 'i':
                            this.state = 217;
                            // Current: voi
                            return true;
                        case 'l':
                            this.state = 83;
                            // Current: vol
                            return true;
                    }
                    break;
                case 113:
                    if ((@char == 'l'))
                    {
                        this.state = 5;
                        // Current: volatil
                        return true;
                    }
                    break;
                case 114:
                    if ((@char == 'l'))
                    {
                        this.state = (this.exitState = 396);
                        // Yields: Virtual
                        return false;
                    }
                    break;
                case 115:
                    switch (@char)
                    {
                        case 'i':
                            this.state = 190;
                            // Current: ui
                            return true;
                        case 'l':
                            this.state = 177;
                            // Current: ul
                            return true;
                        case 'n':
                            this.state = 157;
                            // Current: un
                            return true;
                        case 's':
                            this.state = 133;
                            // Current: us
                            return true;
                    }
                    break;
                case 116:
                    if ((@char == 'l'))
                    {
                        this.state = 259;
                        // Current: stackal
                        return true;
                    }
                    break;
                case 117:
                    if ((@char == 'o'))
                    {
                        this.state = 162;
                        // Current: stackallo
                        return true;
                    }
                    break;
                case 118:
                    if ((@char == 'y'))
                    {
                        this.state = (this.exitState = 405);
                        // Yields: ReadOnly
                        return false;
                    }
                    break;
                case 119:
                    if ((@char == 'i'))
                    {
                        this.state = 163;
                        // Current: publi
                        return true;
                    }
                    break;
                case 120:
                    if ((@char == 'l'))
                    {
                        this.state = (this.exitState = 347);
                        // Yields: Null
                        return false;
                    }
                    break;
                case 121:
                    if ((@char == 'l'))
                    {
                        this.state = (this.exitState = 407);
                        // Yields: Internal
                        return false;
                    }
                    break;
                case 122:
                    if ((@char == 'l'))
                    {
                        this.state = 141;
                        // Current: impl
                        return true;
                    }
                    break;
                case 123:
                    if ((@char == 'l'))
                    {
                        this.state = 270;
                        // Current: final
                        return true;
                    }
                    break;
                case 124:
                    if ((@char == 'y'))
                    {
                        this.state = (this.exitState = 399);
                        // Yields: Finally
                        return false;
                    }
                    break;
                case 125:
                    if ((@char == 's'))
                    {
                        this.state = 29;
                        // Current: fals
                        return true;
                    }
                    break;
                case 126:
                    if ((@char == 'i'))
                    {
                        this.state = 171;
                        // Current: expli
                        return true;
                    }
                    break;
                case 127:
                    if ((@char == 's'))
                    {
                        this.state = 32;
                        // Current: els
                        return true;
                    }
                    break;
                case 128:
                    if ((@char == 'u'))
                    {
                        this.state = 45;
                        // Current: __refvalu
                        return true;
                    }
                    break;
                case 129:
                    if ((@char == 'i'))
                    {
                        this.state = 288;
                        // Current: __argli
                        return true;
                    }
                    break;
                case 130:
                    if ((@char == 'i'))
                    {
                        this.state = 1;
                        // Current: yi
                        return true;
                    }
                    break;
                case 131:
                    if ((@char == 'i'))
                    {
                        this.state = 113;
                        // Current: volati
                        return true;
                    }
                    break;
                case 132:
                    switch (@char)
                    {
                        case 'i':
                            this.state = 147;
                            // Current: vi
                            return true;
                        case 'o':
                            this.state = 112;
                            // Current: vo
                            return true;
                    }
                    break;
                case 133:
                    switch (@char)
                    {
                        case 'h':
                            this.state = 176;
                            // Current: ush
                            return true;
                        case 'i':
                            this.state = 188;
                            // Current: usi
                            return true;
                    }
                    break;
                case 134:
                    switch (@char)
                    {
                        case 'i':
                            this.state = 200;
                            // Current: thi
                            return true;
                        case 'r':
                            this.state = 179;
                            // Current: thr
                            return true;
                    }
                    break;
                case 135:
                    if ((@char == 'i'))
                    {
                        this.state = 53;
                        // Current: swi
                        return true;
                    }
                    break;
                case 136:
                    switch (@char)
                    {
                        case 'i':
                            this.state = 191;
                            // Current: stri
                            return true;
                        case 'u':
                            this.state = 160;
                            // Current: stru
                            return true;
                    }
                    break;
                case 137:
                    if ((@char == 'i'))
                    {
                        this.state = 161;
                        // Current: stati
                        return true;
                    }
                    break;
                case 138:
                    switch (@char)
                    {
                        case 'i':
                            this.state = 254;
                            // Current: pri
                            return true;
                        case 'o':
                            this.state = 60;
                            // Current: pro
                            return true;
                    }
                    break;
                case 139:
                    if ((@char == 'i'))
                    {
                        this.state = 90;
                        // Current: parti
                        return true;
                    }
                    break;
                case 140:
                    if ((@char == 'i'))
                    {
                        this.state = 222;
                        // Current: overri
                        return true;
                    }
                    break;
                case 141:
                    if ((@char == 'i'))
                    {
                        this.state = 169;
                        // Current: impli
                        return true;
                    }
                    break;
                case 142:
                    if ((@char == 'i'))
                    {
                        this.state = 67;
                        // Current: implici
                        return true;
                    }
                    break;
                case 143:
                    if ((@char == 'i'))
                    {
                        this.state = 72;
                        // Current: explici
                        return true;
                    }
                    break;
                case 144:
                    if ((@char == 'i'))
                    {
                        this.state = 244;
                        // Current: deci
                        return true;
                    }
                    break;
                case 145:
                    if ((@char == 'i'))
                    {
                        this.state = 199;
                        // Current: conti
                        return true;
                    }
                    break;
                case 146:
                    if ((@char == 'r'))
                    {
                        this.state = 4;
                        // Current: wher
                        return true;
                    }
                    break;
                case 147:
                    if ((@char == 'r'))
                    {
                        this.state = 50;
                        // Current: vir
                        return true;
                    }
                    break;
                case 148:
                    if ((@char == 'r'))
                    {
                        this.state = 51;
                        // Current: ushor
                        return true;
                    }
                    break;
                case 149:
                    switch (@char)
                    {
                        case 'h':
                            this.state = 134;
                            // Current: th
                            return true;
                        case 'r':
                            this.state = 205;
                            // Current: tr
                            return true;
                        case 'y':
                            this.state = 232;
                            // Current: ty
                            return true;
                    }
                    break;
                case 150:
                    if ((@char == 'r'))
                    {
                        this.state = 56;
                        // Current: shor
                        return true;
                    }
                    break;
                case 151:
                    if ((@char == 'r'))
                    {
                        this.state = 192;
                        // Current: retur
                        return true;
                    }
                    break;
                case 152:
                    if ((@char == 'r'))
                    {
                        this.state = 63;
                        // Current: par
                        return true;
                    }
                    break;
                case 153:
                    if ((@char == 'r'))
                    {
                        this.state = 264;
                        // Current: over
                        return true;
                    }
                    break;
                case 154:
                    switch (@char)
                    {
                        case 'f':
                            this.state = 94;
                            // Current: interf
                            return true;
                        case 'n':
                            this.state = 93;
                            // Current: intern
                            return true;
                    }
                    break;
                case 155:
                    if ((@char == 'n'))
                    {
                        this.state = (this.exitState = 389);
                        // Yields: Extern
                        return false;
                    }
                    break;
                case 156:
                    if ((@char == 'g'))
                    {
                        this.state = 287;
                        // Current: __arg
                        return true;
                    }
                    break;
                case 157:
                    switch (@char)
                    {
                        case 'c':
                            this.state = 226;
                            // Current: unc
                            return true;
                        case 's':
                            this.state = 85;
                            // Current: uns
                            return true;
                    }
                    break;
                case 158:
                    if ((@char == 'c'))
                    {
                        this.state = 245;
                        // Current: unchec
                        return true;
                    }
                    break;
                case 159:
                    if ((@char == 'c'))
                    {
                        this.state = 227;
                        // Current: switc
                        return true;
                    }
                    break;
                case 160:
                    if ((@char == 'c'))
                    {
                        this.state = 54;
                        // Current: struc
                        return true;
                    }
                    break;
                case 161:
                    if ((@char == 'c'))
                    {
                        this.state = (this.exitState = 382);
                        // Yields: Static
                        return false;
                    }
                    break;
                case 162:
                    if ((@char == 'c'))
                    {
                        this.state = (this.exitState = 421);
                        // Yields: StackAlloc
                        return false;
                    }
                    break;
                case 163:
                    if ((@char == 'c'))
                    {
                        this.state = (this.exitState = 386);
                        // Yields: Public
                        return false;
                    }
                    break;
                case 164:
                    if ((@char == 'c'))
                    {
                        this.state = 61;
                        // Current: protec
                        return true;
                    }
                    break;
                case 165:
                    if ((@char == 'c'))
                    {
                        this.state = 66;
                        // Current: objec
                        return true;
                    }
                    break;
                case 166:
                    if ((@char == 'c'))
                    {
                        this.state = 25;
                        // Current: namespac
                        return true;
                    }
                    break;
                case 167:
                    switch (@char)
                    {
                        case 'c':
                            this.state = 247;
                            // Current: loc
                            return true;
                        case 'n':
                            this.state = 239;
                            // Current: lon
                            return true;
                    }
                    break;
                case 168:
                    if ((@char == 'c'))
                    {
                        this.state = 26;
                        // Current: interfac
                        return true;
                    }
                    break;
                case 169:
                    if ((@char == 'c'))
                    {
                        this.state = 142;
                        // Current: implic
                        return true;
                    }
                    break;
                case 170:
                    if ((@char == 'c'))
                    {
                        this.state = 228;
                        // Current: foreac
                        return true;
                    }
                    break;
                case 171:
                    if ((@char == 'c'))
                    {
                        this.state = 143;
                        // Current: explic
                        return true;
                    }
                    break;
                case 172:
                    switch (@char)
                    {
                        case 'c':
                            this.state = 144;
                            // Current: dec
                            return true;
                        case 'f':
                            this.state = 100;
                            // Current: def
                            return true;
                        case 'l':
                            this.state = 35;
                            // Current: del
                            return true;
                        case 's':
                            this.state = 319;
                            // Current: des
                            return true;
                    }
                    break;
                case 173:
                    if ((@char == 'c'))
                    {
                        this.state = 248;
                        // Current: chec
                        return true;
                    }
                    break;
                case 174:
                    if ((@char == 'c'))
                    {
                        this.state = 229;
                        // Current: catc
                        return true;
                    }
                    break;
                case 175:
                    if ((@char == 'c'))
                    {
                        this.state = 80;
                        // Current: abstrac
                        return true;
                    }
                    break;
                case 176:
                    if ((@char == 'o'))
                    {
                        this.state = 148;
                        // Current: usho
                        return true;
                    }
                    break;
                case 177:
                    if ((@char == 'o'))
                    {
                        this.state = 189;
                        // Current: ulo
                        return true;
                    }
                    break;
                case 178:
                    if ((@char == 'o'))
                    {
                        this.state = 212;
                        // Current: typeo
                        return true;
                    }
                    break;
                case 179:
                    if ((@char == 'o'))
                    {
                        this.state = 255;
                        // Current: thro
                        return true;
                    }
                    break;
                case 180:
                    if ((@char == 'o'))
                    {
                        this.state = 213;
                        // Current: sizeo
                        return true;
                    }
                    break;
                case 181:
                    if ((@char == 'o'))
                    {
                        this.state = 150;
                        // Current: sho
                        return true;
                    }
                    break;
                case 182:
                    if ((@char == 'o'))
                    {
                        this.state = 193;
                        // Current: reado
                        return true;
                    }
                    break;
                case 183:
                    if ((@char == 'o'))
                    {
                        this.state = 266;
                        // Current: operato
                        return true;
                    }
                    break;
                case 184:
                    if ((@char == 'o'))
                    {
                        this.state = 167;
                        // Current: lo
                        return true;
                    }
                    break;
                case 185:
                    if ((@char == 'o'))
                    {
                        this.state = (this.exitState = 350);
                        // Yields: GoTo
                        return false;
                    }
                    break;
                case 186:
                    if ((@char == 'o'))
                    {
                        this.state = 96;
                        // Current: flo
                        return true;
                    }
                    break;
                case 187:
                    if ((@char == 'l'))
                    {
                        this.state = (this.exitState = 341);
                        // Yields: Boolean
                        return false;
                    }
                    break;
                case 188:
                    if ((@char == 'n'))
                    {
                        this.state = 236;
                        // Current: usin
                        return true;
                    }
                    break;
                case 189:
                    if ((@char == 'n'))
                    {
                        this.state = 237;
                        // Current: ulon
                        return true;
                    }
                    break;
                case 190:
                    if ((@char == 'n'))
                    {
                        this.state = 52;
                        // Current: uin
                        return true;
                    }
                    break;
                case 191:
                    if ((@char == 'n'))
                    {
                        this.state = 238;
                        // Current: strin
                        return true;
                    }
                    break;
                case 192:
                    if ((@char == 'n'))
                    {
                        this.state = (this.exitState = 385);
                        // Yields: Return
                        return false;
                    }
                    break;
                case 193:
                    if ((@char == 'n'))
                    {
                        this.state = 261;
                        // Current: readon
                        return true;
                    }
                    break;
                case 194:
                    switch (@char)
                    {
                        case 'f':
                            this.state = (this.exitState = 331);
                            // Yields: If
                            return false;
                        case 'm':
                            this.state = 234;
                            // Current: im
                            return true;
                        case 'n':
                            this.state = (this.exitState = 326);
                            /* ------------------------------------\
                            |  Yields In if the next character     |
                            |  in the sequence isn't encountered.  |
                            \------------------------------------ */
                            return true;
                        case 's':
                            this.state = (this.exitState = 330);
                            // Yields: Is
                            return false;
                    }
                    break;
                case 195:
                    switch (@char)
                    {
                        case 'n':
                            this.state = 97;
                            // Current: fin
                            return true;
                        case 'x':
                            this.state = 28;
                            // Current: fix
                            return true;
                    }
                    break;
                case 196:
                    if ((@char == 'n'))
                    {
                        this.state = 73;
                        // Current: even
                        return true;
                    }
                    break;
                case 197:
                    switch (@char)
                    {
                        case 'l':
                            this.state = 127;
                            // Current: el
                            return true;
                        case 'n':
                            this.state = 208;
                            // Current: en
                            return true;
                        case 'q':
                            this.state = 301;
                            // Current: eq
                            return true;
                        case 'v':
                            this.state = 31;
                            // Current: ev
                            return true;
                        case 'x':
                            this.state = 71;
                            // Current: ex
                            return true;
                    }
                    break;
                case 198:
                    if ((@char == 'n'))
                    {
                        this.state = 76;
                        // Current: con
                        return true;
                    }
                    break;
                case 199:
                    if ((@char == 'n'))
                    {
                        this.state = 210;
                        // Current: contin
                        return true;
                    }
                    break;
                case 200:
                    if ((@char == 's'))
                    {
                        this.state = (this.exitState = 346);
                        // Yields: This
                        return false;
                    }
                    break;
                case 201:
                    if ((@char == 's'))
                    {
                        this.state = (this.exitState = 387);
                        // Yields: Params
                        return false;
                    }
                    break;
                case 202:
                    if ((@char == 's'))
                    {
                        this.state = 233;
                        // Current: names
                        return true;
                    }
                    break;
                case 203:
                    if ((@char == 's'))
                    {
                        this.state = 277;
                        // Current: clas
                        return true;
                    }
                    break;
                case 204:
                    if ((@char == 'u'))
                    {
                        this.state = 84;
                        // Current: virtu
                        return true;
                    }
                    break;
                case 205:
                    switch (@char)
                    {
                        case 'u':
                            this.state = 10;
                            // Current: tru
                            return true;
                        case 'y':
                            this.state = (this.exitState = 335);
                            // Yields: Try
                            return false;
                    }
                    break;
                case 206:
                    if ((@char == 'u'))
                    {
                        this.state = 151;
                        // Current: retu
                        return true;
                    }
                    break;
                case 207:
                    switch (@char)
                    {
                        case 'b':
                            this.state = 258;
                            // Current: ob
                            return true;
                        case 'n':
                            this.state = (this.exitState = 332);
                            // Yields: On
                            return false;
                        case 'p':
                            this.state = 21;
                            // Current: op
                            return true;
                        case 'r':
                            this.state = 308;
                            // Current: or
                            return true;
                        case 'u':
                            this.state = 64;
                            // Current: ou
                            return true;
                        case 'v':
                            this.state = 19;
                            // Current: ov
                            return true;
                    }
                    break;
                case 208:
                    if ((@char == 'u'))
                    {
                        this.state = 243;
                        // Current: enu
                        return true;
                    }
                    break;
                case 209:
                    if ((@char == 'u'))
                    {
                        this.state = 275;
                        // Current: defau
                        return true;
                    }
                    break;
                case 210:
                    if ((@char == 'u'))
                    {
                        this.state = 37;
                        // Current: continu
                        return true;
                    }
                    break;
                case 211:
                    if ((@char == 'f'))
                    {
                        this.state = 6;
                        // Current: unsaf
                        return true;
                    }
                    break;
                case 212:
                    if ((@char == 'f'))
                    {
                        this.state = (this.exitState = 378);
                        // Yields: TypeOf
                        return false;
                    }
                    break;
                case 213:
                    if ((@char == 'f'))
                    {
                        this.state = (this.exitState = 383);
                        // Yields: SizeOf
                        return false;
                    }
                    break;
                case 214:
                    if ((@char == 'f'))
                    {
                        this.state = 81;
                        // Current: __ref
                        return true;
                    }
                    break;
                case 215:
                    if ((@char == 'f'))
                    {
                        this.state = (this.exitState = 418);
                        // Yields: __MakeRef
                        return false;
                    }
                    break;
                case 216:
                    if ((@char == 'd'))
                    {
                        this.state = (this.exitState = 360);
                        // Yields: Yield
                        return false;
                    }
                    break;
                case 217:
                    if ((@char == 'd'))
                    {
                        this.state = (this.exitState = 343);
                        // Yields: Void
                        return false;
                    }
                    break;
                case 218:
                    if ((@char == 'd'))
                    {
                        this.state = (this.exitState = 413);
                        // Yields: UnChecked
                        return false;
                    }
                    break;
                case 219:
                    if ((@char == 'd'))
                    {
                        this.state = (this.exitState = 384);
                        // Yields: Sealed
                        return false;
                    }
                    break;
                case 220:
                    if ((@char == 'd'))
                    {
                        this.state = 182;
                        // Current: read
                        return true;
                    }
                    break;
                case 221:
                    if ((@char == 'd'))
                    {
                        this.state = (this.exitState = 414);
                        // Yields: Protected
                        return false;
                    }
                    break;
                case 222:
                    if ((@char == 'd'))
                    {
                        this.state = 20;
                        // Current: overrid
                        return true;
                    }
                    break;
                case 223:
                    if ((@char == 'd'))
                    {
                        this.state = (this.exitState = 369);
                        // Yields: Fixed
                        return false;
                    }
                    break;
                case 224:
                    if ((@char == 'd'))
                    {
                        this.state = (this.exitState = 401);
                        // Yields: Checked
                        return false;
                    }
                    break;
                case 225:
                    if ((@char == 'h'))
                    {
                        this.state = 3;
                        // Current: wh
                        return true;
                    }
                    break;
                case 226:
                    if ((@char == 'h'))
                    {
                        this.state = 7;
                        // Current: unch
                        return true;
                    }
                    break;
                case 227:
                    if ((@char == 'h'))
                    {
                        this.state = (this.exitState = 379);
                        // Yields: Switch
                        return false;
                    }
                    break;
                case 228:
                    if ((@char == 'h'))
                    {
                        this.state = (this.exitState = 398);
                        // Yields: ForEach
                        return false;
                    }
                    break;
                case 229:
                    if ((@char == 'h'))
                    {
                        this.state = (this.exitState = 373);
                        // Yields: Catch
                        return false;
                    }
                    break;
                case 230:
                    if ((@char == 'y'))
                    {
                        this.state = 58;
                        // Current: sby
                        return true;
                    }
                    break;
                case 231:
                    if ((@char == 'y'))
                    {
                        this.state = 235;
                        // Current: __refty
                        return true;
                    }
                    break;
                case 232:
                    if ((@char == 'p'))
                    {
                        this.state = 9;
                        // Current: typ
                        return true;
                    }
                    break;
                case 233:
                    if ((@char == 'p'))
                    {
                        this.state = 92;
                        // Current: namesp
                        return true;
                    }
                    break;
                case 234:
                    if ((@char == 'p'))
                    {
                        this.state = 122;
                        // Current: imp
                        return true;
                    }
                    break;
                case 235:
                    if ((@char == 'p'))
                    {
                        this.state = 46;
                        // Current: __reftyp
                        return true;
                    }
                    break;
                case 236:
                    if ((@char == 'g'))
                    {
                        this.state = (this.exitState = 363);
                        // Yields: Using
                        return false;
                    }
                    break;
                case 237:
                    if ((@char == 'g'))
                    {
                        this.state = (this.exitState = 364);
                        // Yields: UInt64
                        return false;
                    }
                    break;
                case 238:
                    if ((@char == 'g'))
                    {
                        this.state = (this.exitState = 381);
                        // Yields: String
                        return false;
                    }
                    break;
                case 239:
                    if ((@char == 'g'))
                    {
                        this.state = (this.exitState = 348);
                        // Yields: Int64
                        return false;
                    }
                    break;
                case 240:
                    if ((@char == 'g'))
                    {
                        this.state = 99;
                        // Current: deleg
                        return true;
                    }
                    break;
                case 241:
                    if ((@char == 'm'))
                    {
                        this.state = 201;
                        // Current: param
                        return true;
                    }
                    break;
                case 242:
                    if ((@char == 'm'))
                    {
                        this.state = 24;
                        // Current: nam
                        return true;
                    }
                    break;
                case 243:
                    if ((@char == 'm'))
                    {
                        this.state = (this.exitState = 351);
                        // Yields: Enum
                        return false;
                    }
                    break;
                case 244:
                    if ((@char == 'm'))
                    {
                        this.state = 101;
                        // Current: decim
                        return true;
                    }
                    break;
                case 245:
                    if ((@char == 'k'))
                    {
                        this.state = 8;
                        // Current: uncheck
                        return true;
                    }
                    break;
                case 246:
                    if ((@char == 'k'))
                    {
                        this.state = 87;
                        // Current: stack
                        return true;
                    }
                    break;
                case 247:
                    if ((@char == 'k'))
                    {
                        this.state = (this.exitState = 349);
                        // Yields: Lock
                        return false;
                    }
                    break;
                case 248:
                    if ((@char == 'k'))
                    {
                        this.state = 39;
                        // Current: check
                        return true;
                    }
                    break;
                case 249:
                    if ((@char == 'k'))
                    {
                        this.state = (this.exitState = 374);
                        // Yields: Break
                        return false;
                    }
                    break;
                case 250:
                    if ((@char == 'k'))
                    {
                        this.state = 47;
                        // Current: __mak
                        return true;
                    }
                    break;
                case 251:
                    if ((@char == 'b'))
                    {
                        this.state = 262;
                        // Current: pub
                        return true;
                    }
                    break;
                case 252:
                    if ((@char == 'b'))
                    {
                        this.state = 274;
                        // Current: doub
                        return true;
                    }
                    break;
                case 253:
                    switch (@char)
                    {
                        case 'b':
                            this.state = 281;
                            // Current: ab
                            return true;
                        case 's':
                            this.state = (this.exitState = 328);
                            /* ------------------------------------\
                            |  Yields As if the next character     |
                            |  in the sequence isn't encountered.  |
                            \------------------------------------ */
                            return true;
                    }
                    break;
                case 254:
                    if ((@char == 'v'))
                    {
                        this.state = 88;
                        // Current: priv
                        return true;
                    }
                    break;
                case 255:
                    if ((@char == 'w'))
                    {
                        this.state = (this.exitState = 365);
                        // Yields: Throw
                        return false;
                    }
                    break;
                case 256:
                    if ((@char == 'w'))
                    {
                        this.state = (this.exitState = 339);
                        // Yields: New
                        return false;
                    }
                    break;
                case 257:
                    if ((@char == 'z'))
                    {
                        this.state = 11;
                        // Current: siz
                        return true;
                    }
                    break;
                case 258:
                    if ((@char == 'j'))
                    {
                        this.state = 22;
                        // Current: obj
                        return true;
                    }
                    break;
                case 259:
                    if ((@char == 'l'))
                    {
                        this.state = 117;
                        // Current: stackall
                        return true;
                    }
                    break;
                case 260:
                    if ((@char == 'l'))
                    {
                        this.state = 13;
                        // Current: seal
                        return true;
                    }
                    break;
                case 261:
                    if ((@char == 'l'))
                    {
                        this.state = 118;
                        // Current: readonl
                        return true;
                    }
                    break;
                case 262:
                    if ((@char == 'l'))
                    {
                        this.state = 119;
                        // Current: publ
                        return true;
                    }
                    break;
                case 263:
                    if ((@char == 'l'))
                    {
                        this.state = (this.exitState = 394);
                        // Yields: Partial
                        return false;
                    }
                    break;
                case 264:
                    if ((@char == 'r'))
                    {
                        this.state = 140;
                        // Current: overr
                        return true;
                    }
                    break;
                case 265:
                    if ((@char == 'r'))
                    {
                        this.state = 91;
                        // Current: oper
                        return true;
                    }
                    break;
                case 266:
                    if ((@char == 'r'))
                    {
                        this.state = (this.exitState = 403);
                        // Yields: Operator
                        return false;
                    }
                    break;
                case 267:
                    if ((@char == 'l'))
                    {
                        this.state = 120;
                        // Current: nul
                        return true;
                    }
                    break;
                case 268:
                    if ((@char == 'r'))
                    {
                        this.state = 154;
                        // Current: inter
                        return true;
                    }
                    break;
                case 269:
                    if ((@char == 'r'))
                    {
                        this.state = (this.exitState = 334);
                        /* ------------------------------------\
                        |  Yields For if the next character    |
                        |  in the sequence isn't encountered.  |
                        \------------------------------------ */
                        return true;
                    }
                    break;
                case 270:
                    if ((@char == 'l'))
                    {
                        this.state = 124;
                        // Current: finall
                        return true;
                    }
                    break;
                case 271:
                    if ((@char == 'l'))
                    {
                        this.state = 125;
                        // Current: fal
                        return true;
                    }
                    break;
                case 272:
                    if ((@char == 'r'))
                    {
                        this.state = 155;
                        // Current: exter
                        return true;
                    }
                    break;
                case 273:
                    if ((@char == 'l'))
                    {
                        this.state = 126;
                        // Current: expl
                        return true;
                    }
                    break;
                case 274:
                    if ((@char == 'l'))
                    {
                        this.state = 33;
                        // Current: doubl
                        return true;
                    }
                    break;
                case 275:
                    if ((@char == 'l'))
                    {
                        this.state = 75;
                        // Current: defaul
                        return true;
                    }
                    break;
                case 276:
                    if ((@char == 'l'))
                    {
                        this.state = (this.exitState = 395);
                        // Yields: Decimal
                        return false;
                    }
                    break;
                case 277:
                    if ((@char == 's'))
                    {
                        this.state = (this.exitState = 359);
                        // Yields: Class
                        return false;
                    }
                    break;
                case 278:
                    if ((@char == 'r'))
                    {
                        this.state = (this.exitState = 342);
                        // Yields: Char
                        return false;
                    }
                    break;
                case 279:
                    if ((@char == 'o'))
                    {
                        this.state = 187;
                        // Current: boo
                        return true;
                    }
                    break;
                case 280:
                    if ((@char == 's'))
                    {
                        this.state = 43;
                        // Current: bas
                        return true;
                    }
                    break;
                case 281:
                    if ((@char == 's'))
                    {
                        this.state = 79;
                        // Current: abs
                        return true;
                    }
                    break;
                case 282:
                    if ((@char == 'r'))
                    {
                        this.state = 106;
                        // Current: abstr
                        return true;
                    }
                    break;
                case 283:
                    if ((@char == '_'))
                    {
                        this.state = 109;
                        // Current: __
                        return true;
                    }
                    break;
                case 284:
                    if ((@char == 'l'))
                    {
                        this.state = 128;
                        // Current: __refval
                        return true;
                    }
                    break;
                case 285:
                    if ((@char == 'r'))
                    {
                        this.state = 48;
                        // Current: __maker
                        return true;
                    }
                    break;
                case 286:
                    if ((@char == 'r'))
                    {
                        this.state = 156;
                        // Current: __ar
                        return true;
                    }
                    break;
                case 287:
                    if ((@char == 'l'))
                    {
                        this.state = 129;
                        // Current: __argl
                        return true;
                    }
                    break;
                case 288:
                    if ((@char == 's'))
                    {
                        this.state = 82;
                        // Current: __arglis
                        return true;
                    }
                    break;
                case 289:
                    if ((@char == 'o'))
                    {
                        this.state = 290;
                        // Current: glo
                        return true;
                    }
                    break;
                case 290:
                    if ((@char == 'b'))
                    {
                        this.state = 291;
                        // Current: glob
                        return true;
                    }
                    break;
                case 291:
                    if ((@char == 'a'))
                    {
                        this.state = 292;
                        // Current: globa
                        return true;
                    }
                    break;
                case 292:
                    if ((@char == 'l'))
                    {
                        this.state = (this.exitState = 391);
                        // Yields: Global
                        return false;
                    }
                    break;
                case 293:
                    if ((@char == 'o'))
                    {
                        this.state = 294;
                        // Current: fro
                        return true;
                    }
                    break;
                case 294:
                    if ((@char == 'm'))
                    {
                        this.state = (this.exitState = 356);
                        // Yields: From
                        return false;
                    }
                    break;
                case 295:
                    if ((@char == 'o'))
                    {
                        this.state = 296;
                        // Current: jo
                        return true;
                    }
                    break;
                case 296:
                    if ((@char == 'i'))
                    {
                        this.state = 297;
                        // Current: joi
                        return true;
                    }
                    break;
                case 297:
                    if ((@char == 'n'))
                    {
                        this.state = (this.exitState = 357);
                        // Yields: Join
                        return false;
                    }
                    break;
                case 298:
                    if ((@char == 'e'))
                    {
                        this.state = 299;
                        // Current: sele
                        return true;
                    }
                    break;
                case 299:
                    if ((@char == 'c'))
                    {
                        this.state = 300;
                        // Current: selec
                        return true;
                    }
                    break;
                case 300:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 392);
                        // Yields: Select
                        return false;
                    }
                    break;
                case 301:
                    if ((@char == 'u'))
                    {
                        this.state = 302;
                        // Current: equ
                        return true;
                    }
                    break;
                case 302:
                    if ((@char == 'a'))
                    {
                        this.state = 303;
                        // Current: equa
                        return true;
                    }
                    break;
                case 303:
                    if ((@char == 'l'))
                    {
                        this.state = 304;
                        // Current: equal
                        return true;
                    }
                    break;
                case 304:
                    if ((@char == 's'))
                    {
                        this.state = (this.exitState = 393);
                        // Yields: Equals
                        return false;
                    }
                    break;
                case 305:
                    if ((@char == 'o'))
                    {
                        this.state = 306;
                        // Current: gro
                        return true;
                    }
                    break;
                case 306:
                    if ((@char == 'u'))
                    {
                        this.state = 307;
                        // Current: grou
                        return true;
                    }
                    break;
                case 307:
                    if ((@char == 'p'))
                    {
                        this.state = (this.exitState = 375);
                        // Yields: Group
                        return false;
                    }
                    break;
                case 308:
                    if ((@char == 'd'))
                    {
                        this.state = 309;
                        // Current: ord
                        return true;
                    }
                    break;
                case 309:
                    if ((@char == 'e'))
                    {
                        this.state = 310;
                        // Current: orde
                        return true;
                    }
                    break;
                case 310:
                    if ((@char == 'r'))
                    {
                        this.state = 311;
                        // Current: order
                        return true;
                    }
                    break;
                case 311:
                    if ((@char == 'b'))
                    {
                        this.state = 312;
                        // Current: orderb
                        return true;
                    }
                    break;
                case 312:
                    if ((@char == 'y'))
                    {
                        this.state = (this.exitState = 402);
                        // Yields: OrderBy
                        return false;
                    }
                    break;
                case 313:
                    if ((@char == 'e'))
                    {
                        this.state = 314;
                        // Current: asce
                        return true;
                    }
                    break;
                case 314:
                    if ((@char == 'n'))
                    {
                        this.state = 315;
                        // Current: ascen
                        return true;
                    }
                    break;
                case 315:
                    if ((@char == 'd'))
                    {
                        this.state = 316;
                        // Current: ascend
                        return true;
                    }
                    break;
                case 316:
                    if ((@char == 'i'))
                    {
                        this.state = 317;
                        // Current: ascendi
                        return true;
                    }
                    break;
                case 317:
                    if ((@char == 'n'))
                    {
                        this.state = 318;
                        // Current: ascendin
                        return true;
                    }
                    break;
                case 318:
                    if ((@char == 'g'))
                    {
                        this.state = (this.exitState = 420);
                        // Yields: Ascending
                        return false;
                    }
                    break;
                case 319:
                    if ((@char == 'c'))
                    {
                        this.state = 320;
                        // Current: desc
                        return true;
                    }
                    break;
                case 320:
                    if ((@char == 'e'))
                    {
                        this.state = 321;
                        // Current: desce
                        return true;
                    }
                    break;
                case 321:
                    if ((@char == 'n'))
                    {
                        this.state = 322;
                        // Current: descen
                        return true;
                    }
                    break;
                case 322:
                    if ((@char == 'd'))
                    {
                        this.state = 323;
                        // Current: descend
                        return true;
                    }
                    break;
                case 323:
                    if ((@char == 'i'))
                    {
                        this.state = 324;
                        // Current: descendi
                        return true;
                    }
                    break;
                case 324:
                    if ((@char == 'n'))
                    {
                        this.state = 325;
                        // Current: descendin
                        return true;
                    }
                    break;
                case 325:
                    if ((@char == 'g'))
                    {
                        this.state = (this.exitState = 423);
                        // Yields: Descending
                        return false;
                    }
                    break;
                case 326:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 333);
                        /* ------------------------------------\
                        |  Yields Int32 if the next character  |
                        |  in the sequence isn't encountered.  |
                        \------------------------------------ */
                        return true;
                    }
                    break;
                case 327:
                    if ((@char == 't'))
                    {
                        this.state = 41;
                        // Current: byt
                        return true;
                    }
                    break;
                case 328:
                    if ((@char == 'c'))
                    {
                        this.state = 313;
                        // Current: asc
                        return true;
                    }
                    break;
                case 329:
                    if ((@char == 'u'))
                    {
                        this.state = 252;
                        // Current: dou
                        return true;
                    }
                    break;
                case 333:
                    switch (@char)
                    {
                        case 'e':
                            this.state = 268;
                            // Current: inte
                            return true;
                        case 'o':
                            this.state = (this.exitState = 358);
                            // Yields: Into
                            return false;
                    }
                    break;
                case 334:
                    if ((@char == 'e'))
                    {
                        this.state = 95;
                        // Current: fore
                        return true;
                    }
                    break;
            }
            return false;
        }

        public void Reset()
        {
            this.state = this.exitState = 0;
        }

        IToken ITokenizerStateMachine.GetToken(FileLocale fileLocale)
        {
            return this.GetToken(fileLocale);
        }

        public ICSharpKeywordToken GetToken(FileLocale fileLocale)
        {
            ulong sectorValue = 0;
            CSharpKeywordCases currentCase = CSharpKeywordCases.None;
            switch (this.exitState)
            {
                case 361:
                     sectorValue = (ulong)CSharpKeywords1.While;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build While here.    |
                     |  Actual value: while  |
                     \--------------------- */
                    break;
                case 362:
                     sectorValue = (ulong)CSharpKeywords1.Where;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build Where here.    |
                     |  Actual value: where  |
                     \--------------------- */
                    break;
                case 404:
                     sectorValue = (ulong)CSharpKeywords1.Volatile;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------------\
                     |  Build Volatile here.    |
                     |  Actual value: volatile  |
                     \------------------------ */
                    break;
                case 377:
                     sectorValue = (ulong)CSharpKeywords1.UnSafe;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build Unsafe here.    |
                     |  Actual value: unsafe  |
                     \---------------------- */
                    break;
                case 345:
                     sectorValue = (ulong)CSharpKeywords2.True;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------\
                     |  Build True here.    |
                     |  Actual value: true  |
                     \-------------------- */
                    break;
                case 367:
                     sectorValue = (ulong)CSharpKeywords2.SByte;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ---------------------\
                     |  Build SByte here.    |
                     |  Actual value: sbyte  |
                     \--------------------- */
                    break;
                case 397:
                     sectorValue = (ulong)CSharpKeywords1.Private;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -----------------------\
                     |  Build Private here.    |
                     |  Actual value: private  |
                     \----------------------- */
                    break;
                case 406:
                     sectorValue = (ulong)CSharpKeywords1.Override;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------------\
                     |  Build Override here.    |
                     |  Actual value: override  |
                     \------------------------ */
                    break;
                case 415:
                     sectorValue = (ulong)CSharpKeywords1.Namespace;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------------\
                     |  Build Namespace here.    |
                     |  Actual value: namespace  |
                     \------------------------- */
                    break;
                case 416:
                     sectorValue = (ulong)CSharpKeywords1.Interface;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------------\
                     |  Build Interface here.    |
                     |  Actual value: interface  |
                     \------------------------- */
                    break;
                case 370:
                     sectorValue = (ulong)CSharpKeywords2.False;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ---------------------\
                     |  Build False here.    |
                     |  Actual value: false  |
                     \--------------------- */
                    break;
                case 352:
                     sectorValue = (ulong)CSharpKeywords1.Else;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* --------------------\
                     |  Build Else here.    |
                     |  Actual value: else  |
                     \-------------------- */
                    break;
                case 390:
                     sectorValue = (ulong)CSharpKeywords2.Double;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ----------------------\
                     |  Build Double here.    |
                     |  Actual value: double  |
                     \---------------------- */
                    break;
                case 329:
                     sectorValue = (ulong)CSharpKeywords1.Do;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------\
                     |  Build Do here.    |
                     |  Actual value: do  |
                     \------------------ */
                    break;
                case 410:
                     sectorValue = (ulong)CSharpKeywords1.Delegate;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------------\
                     |  Build Delegate here.    |
                     |  Actual value: delegate  |
                     \------------------------ */
                    break;
                case 411:
                     sectorValue = (ulong)CSharpKeywords1.Continue;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------------\
                     |  Build Continue here.    |
                     |  Actual value: continue  |
                     \------------------------ */
                    break;
                case 353:
                     sectorValue = (ulong)CSharpKeywords1.Case;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* --------------------\
                     |  Build Case here.    |
                     |  Actual value: case  |
                     \-------------------- */
                    break;
                case 354:
                     sectorValue = (ulong)CSharpKeywords2.Byte;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------\
                     |  Build Byte here.    |
                     |  Actual value: byte  |
                     \-------------------- */
                    break;
                case 355:
                     sectorValue = (ulong)CSharpKeywords1.Base;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* --------------------\
                     |  Build Base here.    |
                     |  Actual value: base  |
                     \-------------------- */
                    break;
                case 422:
                     sectorValue = (ulong)CSharpKeywords2.__RefValue;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------------\
                     |  Build __RefValue here.    |
                     |  Actual value: __refvalue  |
                     \-------------------------- */
                    break;
                case 417:
                     sectorValue = (ulong)CSharpKeywords2.__RefType;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* -------------------------\
                     |  Build __RefType here.    |
                     |  Actual value: __reftype  |
                     \------------------------- */
                    break;
                case 376:
                     sectorValue = (ulong)CSharpKeywords2.UInt16;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ----------------------\
                     |  Build UInt16 here.    |
                     |  Actual value: ushort  |
                     \---------------------- */
                    break;
                case 344:
                     sectorValue = (ulong)CSharpKeywords2.UInt32;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------\
                     |  Build UInt32 here.  |
                     |  Actual value: uint  |
                     \-------------------- */
                    break;
                case 380:
                     sectorValue = (ulong)CSharpKeywords1.Struct;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build Struct here.    |
                     |  Actual value: struct  |
                     \---------------------- */
                    break;
                case 366:
                     sectorValue = (ulong)CSharpKeywords2.Int16;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ---------------------\
                     |  Build Int16 here.    |
                     |  Actual value: short  |
                     \--------------------- */
                    break;
                case 336:
                     sectorValue = (ulong)CSharpKeywords1.Set;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------\
                     |  Build Set here.    |
                     |  Actual value: set  |
                     \------------------- */
                    break;
                case 337:
                     sectorValue = (ulong)CSharpKeywords1.Ref;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------\
                     |  Build Ref here.    |
                     |  Actual value: ref  |
                     \------------------- */
                    break;
                case 338:
                     sectorValue = (ulong)CSharpKeywords1.Out;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------\
                     |  Build Out here.    |
                     |  Actual value: out  |
                     \------------------- */
                    break;
                case 388:
                     sectorValue = (ulong)CSharpKeywords2.Object;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ----------------------\
                     |  Build Object here.    |
                     |  Actual value: object  |
                     \---------------------- */
                    break;
                case 408:
                     sectorValue = (ulong)CSharpKeywords1.Implicit;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------------\
                     |  Build Implicit here.    |
                     |  Actual value: implicit  |
                     \------------------------ */
                    break;
                case 340:
                     sectorValue = (ulong)CSharpKeywords1.Get;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------\
                     |  Build Get here.    |
                     |  Actual value: get  |
                     \------------------- */
                    break;
                case 368:
                     sectorValue = (ulong)CSharpKeywords2.Float;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ---------------------\
                     |  Build Float here.    |
                     |  Actual value: float  |
                     \--------------------- */
                    break;
                case 409:
                     sectorValue = (ulong)CSharpKeywords1.Explicit;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------------\
                     |  Build Explicit here.    |
                     |  Actual value: explicit  |
                     \------------------------ */
                    break;
                case 371:
                     sectorValue = (ulong)CSharpKeywords1.Event;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build Event here.    |
                     |  Actual value: event  |
                     \--------------------- */
                    break;
                case 400:
                     sectorValue = (ulong)CSharpKeywords1.Default;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -----------------------\
                     |  Build Default here.    |
                     |  Actual value: default  |
                     \----------------------- */
                    break;
                case 372:
                     sectorValue = (ulong)CSharpKeywords1.Const;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build Const here.    |
                     |  Actual value: const  |
                     \--------------------- */
                    break;
                case 412:
                     sectorValue = (ulong)CSharpKeywords1.Abstract;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------------\
                     |  Build Abstract here.    |
                     |  Actual value: abstract  |
                     \------------------------ */
                    break;
                case 419:
                     sectorValue = (ulong)CSharpKeywords2.__ArgList;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* -------------------------\
                     |  Build __ArgList here.    |
                     |  Actual value: __arglist  |
                     \------------------------- */
                    break;
                case 327:
                     sectorValue = (ulong)CSharpKeywords2.By;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ------------------\
                     |  Build By here.    |
                     |  Actual value: by  |
                     \------------------ */
                    break;
                case 396:
                     sectorValue = (ulong)CSharpKeywords1.Virtual;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -----------------------\
                     |  Build Virtual here.    |
                     |  Actual value: virtual  |
                     \----------------------- */
                    break;
                case 405:
                     sectorValue = (ulong)CSharpKeywords1.ReadOnly;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------------\
                     |  Build ReadOnly here.    |
                     |  Actual value: readonly  |
                     \------------------------ */
                    break;
                case 347:
                     sectorValue = (ulong)CSharpKeywords1.Null;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* --------------------\
                     |  Build Null here.    |
                     |  Actual value: null  |
                     \-------------------- */
                    break;
                case 407:
                     sectorValue = (ulong)CSharpKeywords1.Internal;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------------\
                     |  Build Internal here.    |
                     |  Actual value: internal  |
                     \------------------------ */
                    break;
                case 399:
                     sectorValue = (ulong)CSharpKeywords1.Finally;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -----------------------\
                     |  Build Finally here.    |
                     |  Actual value: finally  |
                     \----------------------- */
                    break;
                case 389:
                     sectorValue = (ulong)CSharpKeywords1.Extern;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build Extern here.    |
                     |  Actual value: extern  |
                     \---------------------- */
                    break;
                case 382:
                     sectorValue = (ulong)CSharpKeywords1.Static;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build Static here.    |
                     |  Actual value: static  |
                     \---------------------- */
                    break;
                case 421:
                     sectorValue = (ulong)CSharpKeywords1.StackAlloc;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* --------------------------\
                     |  Build StackAlloc here.    |
                     |  Actual value: stackalloc  |
                     \-------------------------- */
                    break;
                case 386:
                     sectorValue = (ulong)CSharpKeywords1.Public;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build Public here.    |
                     |  Actual value: public  |
                     \---------------------- */
                    break;
                case 350:
                     sectorValue = (ulong)CSharpKeywords1.GoTo;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* --------------------\
                     |  Build GoTo here.    |
                     |  Actual value: goto  |
                     \-------------------- */
                    break;
                case 341:
                     sectorValue = (ulong)CSharpKeywords2.Boolean;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ---------------------\
                     |  Build Boolean here.  |
                     |  Actual value: bool   |
                     \--------------------- */
                    break;
                case 385:
                     sectorValue = (ulong)CSharpKeywords1.Return;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build Return here.    |
                     |  Actual value: return  |
                     \---------------------- */
                    break;
                case 331:
                     sectorValue = (ulong)CSharpKeywords1.If;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------\
                     |  Build If here.    |
                     |  Actual value: if  |
                     \------------------ */
                    break;
                case 326:
                     sectorValue = (ulong)CSharpKeywords1.In;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------\
                     |  Build In here.    |
                     |  Actual value: in  |
                     \------------------ */
                    break;
                case 330:
                     sectorValue = (ulong)CSharpKeywords1.Is;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------\
                     |  Build Is here.    |
                     |  Actual value: is  |
                     \------------------ */
                    break;
                case 346:
                     sectorValue = (ulong)CSharpKeywords1.This;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* --------------------\
                     |  Build This here.    |
                     |  Actual value: this  |
                     \-------------------- */
                    break;
                case 387:
                     sectorValue = (ulong)CSharpKeywords1.Params;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build Params here.    |
                     |  Actual value: params  |
                     \---------------------- */
                    break;
                case 335:
                     sectorValue = (ulong)CSharpKeywords1.Try;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------\
                     |  Build Try here.    |
                     |  Actual value: try  |
                     \------------------- */
                    break;
                case 332:
                     sectorValue = (ulong)CSharpKeywords2.On;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ------------------\
                     |  Build On here.    |
                     |  Actual value: on  |
                     \------------------ */
                    break;
                case 378:
                     sectorValue = (ulong)CSharpKeywords1.TypeOf;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build TypeOf here.    |
                     |  Actual value: typeof  |
                     \---------------------- */
                    break;
                case 383:
                     sectorValue = (ulong)CSharpKeywords1.SizeOf;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build SizeOf here.    |
                     |  Actual value: sizeof  |
                     \---------------------- */
                    break;
                case 418:
                     sectorValue = (ulong)CSharpKeywords2.__MakeRef;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* -------------------------\
                     |  Build __MakeRef here.    |
                     |  Actual value: __makeref  |
                     \------------------------- */
                    break;
                case 360:
                     sectorValue = (ulong)CSharpKeywords1.Yield;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build Yield here.    |
                     |  Actual value: yield  |
                     \--------------------- */
                    break;
                case 343:
                     sectorValue = (ulong)CSharpKeywords2.Void;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------\
                     |  Build Void here.    |
                     |  Actual value: void  |
                     \-------------------- */
                    break;
                case 413:
                     sectorValue = (ulong)CSharpKeywords1.UnChecked;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------------\
                     |  Build UnChecked here.    |
                     |  Actual value: unchecked  |
                     \------------------------- */
                    break;
                case 384:
                     sectorValue = (ulong)CSharpKeywords1.Sealed;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build Sealed here.    |
                     |  Actual value: sealed  |
                     \---------------------- */
                    break;
                case 414:
                     sectorValue = (ulong)CSharpKeywords1.Protected;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------------\
                     |  Build Protected here.    |
                     |  Actual value: protected  |
                     \------------------------- */
                    break;
                case 369:
                     sectorValue = (ulong)CSharpKeywords1.Fixed;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build Fixed here.    |
                     |  Actual value: fixed  |
                     \--------------------- */
                    break;
                case 401:
                     sectorValue = (ulong)CSharpKeywords1.Checked;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -----------------------\
                     |  Build Checked here.    |
                     |  Actual value: checked  |
                     \----------------------- */
                    break;
                case 379:
                     sectorValue = (ulong)CSharpKeywords1.Switch;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ----------------------\
                     |  Build Switch here.    |
                     |  Actual value: switch  |
                     \---------------------- */
                    break;
                case 398:
                     sectorValue = (ulong)CSharpKeywords1.ForEach;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -----------------------\
                     |  Build ForEach here.    |
                     |  Actual value: foreach  |
                     \----------------------- */
                    break;
                case 373:
                     sectorValue = (ulong)CSharpKeywords1.Catch;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build Catch here.    |
                     |  Actual value: catch  |
                     \--------------------- */
                    break;
                case 363:
                     sectorValue = (ulong)CSharpKeywords1.Using;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build Using here.    |
                     |  Actual value: using  |
                     \--------------------- */
                    break;
                case 364:
                     sectorValue = (ulong)CSharpKeywords2.UInt64;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ---------------------\
                     |  Build UInt64 here.   |
                     |  Actual value: ulong  |
                     \--------------------- */
                    break;
                case 381:
                     sectorValue = (ulong)CSharpKeywords2.String;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ----------------------\
                     |  Build String here.    |
                     |  Actual value: string  |
                     \---------------------- */
                    break;
                case 348:
                     sectorValue = (ulong)CSharpKeywords2.Int64;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------\
                     |  Build Int64 here.   |
                     |  Actual value: long  |
                     \-------------------- */
                    break;
                case 351:
                     sectorValue = (ulong)CSharpKeywords1.Enum;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* --------------------\
                     |  Build Enum here.    |
                     |  Actual value: enum  |
                     \-------------------- */
                    break;
                case 349:
                     sectorValue = (ulong)CSharpKeywords1.Lock;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* --------------------\
                     |  Build Lock here.    |
                     |  Actual value: lock  |
                     \-------------------- */
                    break;
                case 374:
                     sectorValue = (ulong)CSharpKeywords1.Break;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build Break here.    |
                     |  Actual value: break  |
                     \--------------------- */
                    break;
                case 328:
                     sectorValue = (ulong)CSharpKeywords1.As;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------\
                     |  Build As here.    |
                     |  Actual value: as  |
                     \------------------ */
                    break;
                case 365:
                     sectorValue = (ulong)CSharpKeywords1.Throw;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build Throw here.    |
                     |  Actual value: throw  |
                     \--------------------- */
                    break;
                case 339:
                     sectorValue = (ulong)CSharpKeywords1.New;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------\
                     |  Build New here.    |
                     |  Actual value: new  |
                     \------------------- */
                    break;
                case 394:
                     sectorValue = (ulong)CSharpKeywords1.Partial;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -----------------------\
                     |  Build Partial here.    |
                     |  Actual value: partial  |
                     \----------------------- */
                    break;
                case 403:
                     sectorValue = (ulong)CSharpKeywords1.Operator;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ------------------------\
                     |  Build Operator here.    |
                     |  Actual value: operator  |
                     \------------------------ */
                    break;
                case 334:
                     sectorValue = (ulong)CSharpKeywords1.For;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* -------------------\
                     |  Build For here.    |
                     |  Actual value: for  |
                     \------------------- */
                    break;
                case 395:
                     sectorValue = (ulong)CSharpKeywords2.Decimal;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* -----------------------\
                     |  Build Decimal here.    |
                     |  Actual value: decimal  |
                     \----------------------- */
                    break;
                case 359:
                     sectorValue = (ulong)CSharpKeywords1.Class;
                     currentCase = CSharpKeywordCases.CaseA;
                     /* ---------------------\
                     |  Build Class here.    |
                     |  Actual value: class  |
                     \--------------------- */
                    break;
                case 342:
                     sectorValue = (ulong)CSharpKeywords2.Char;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------\
                     |  Build Char here.    |
                     |  Actual value: char  |
                     \-------------------- */
                    break;
                case 391:
                     sectorValue = (ulong)CSharpKeywords2.Global;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ----------------------\
                     |  Build Global here.    |
                     |  Actual value: global  |
                     \---------------------- */
                    break;
                case 356:
                     sectorValue = (ulong)CSharpKeywords2.From;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------\
                     |  Build From here.    |
                     |  Actual value: from  |
                     \-------------------- */
                    break;
                case 357:
                     sectorValue = (ulong)CSharpKeywords2.Join;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------\
                     |  Build Join here.    |
                     |  Actual value: join  |
                     \-------------------- */
                    break;
                case 392:
                     sectorValue = (ulong)CSharpKeywords2.Select;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ----------------------\
                     |  Build Select here.    |
                     |  Actual value: select  |
                     \---------------------- */
                    break;
                case 393:
                     sectorValue = (ulong)CSharpKeywords2.Equals;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ----------------------\
                     |  Build Equals here.    |
                     |  Actual value: equals  |
                     \---------------------- */
                    break;
                case 375:
                     sectorValue = (ulong)CSharpKeywords2.Group;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* ---------------------\
                     |  Build Group here.    |
                     |  Actual value: group  |
                     \--------------------- */
                    break;
                case 402:
                     sectorValue = (ulong)CSharpKeywords2.OrderBy;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* -----------------------\
                     |  Build OrderBy here.    |
                     |  Actual value: orderby  |
                     \----------------------- */
                    break;
                case 420:
                     sectorValue = (ulong)CSharpKeywords2.Ascending;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* -------------------------\
                     |  Build Ascending here.    |
                     |  Actual value: ascending  |
                     \------------------------- */
                    break;
                case 423:
                     sectorValue = (ulong)CSharpKeywords2.Descending;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------------\
                     |  Build Descending here.    |
                     |  Actual value: descending  |
                     \-------------------------- */
                    break;
                case 333:
                     sectorValue = (ulong)CSharpKeywords2.Int32;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* -------------------\
                     |  Build Int32 here.  |
                     |  Actual value: int  |
                     \------------------- */
                    break;
                case 358:
                     sectorValue = (ulong)CSharpKeywords2.Into;
                     currentCase = CSharpKeywordCases.CaseB;
                     /* --------------------\
                     |  Build Into here.    |
                     |  Actual value: into  |
                     \-------------------- */
                    break;
            }
            switch (currentCase)
            {
                case CSharpKeywordCases.CaseA:
                case CSharpKeywordCases.CaseB:
                    return new CSharpKeywordToken(fileLocale, currentCase, sectorValue);
                case CSharpKeywordCases.None:
                default:
                    throw new InvalidOperationException("Invalid State");
            }
        }
        #region ITokenizerStateMachine Members

        public ulong BytesConsumed
        {
            get
            {
                switch (this.exitState)
                {
                    case 326:
                    case 327:
                    case 328:
                    case 329:
                    case 330:
                    case 331:
                    case 332:
                        return 2;
                    case 333:
                    case 334:
                    case 335:
                    case 336:
                    case 337:
                    case 338:
                    case 339:
                    case 340:
                        return 3;
                    case 341:
                    case 342:
                    case 343:
                    case 344:
                    case 345:
                    case 346:
                    case 347:
                    case 348:
                    case 349:
                    case 350:
                    case 351:
                    case 352:
                    case 353:
                    case 354:
                    case 355:
                    case 356:
                    case 357:
                    case 358:
                        return 4;
                    case 359:
                    case 360:
                    case 361:
                    case 362:
                    case 363:
                    case 364:
                    case 365:
                    case 366:
                    case 367:
                    case 368:
                    case 369:
                    case 370:
                    case 371:
                    case 372:
                    case 373:
                    case 374:
                    case 375:
                        return 5;
                    case 376:
                    case 377:
                    case 378:
                    case 379:
                    case 380:
                    case 381:
                    case 382:
                    case 383:
                    case 384:
                    case 385:
                    case 386:
                    case 387:
                    case 388:
                    case 389:
                    case 390:
                    case 391:
                    case 392:
                    case 393:
                        return 6;
                    case 394:
                    case 395:
                    case 396:
                    case 397:
                    case 398:
                    case 399:
                    case 400:
                    case 401:
                    case 402:
                        return 7;
                    case 403:
                    case 404:
                    case 405:
                    case 406:
                    case 407:
                    case 408:
                    case 409:
                    case 410:
                    case 411:
                    case 412:
                        return 8;
                    case 413:
                    case 414:
                    case 415:
                    case 416:
                    case 417:
                    case 418:
                    case 419:
                    case 420:
                        return 9;
                    case 421:
                    case 422:
                    case 423:
                        return 10;
                    default:
                        return 0;
                }
            }
        }

        #endregion

        public static uint GetLength(CSharpKeywords2 caseKeyword)
        {
            switch (caseKeyword)
            {
                case CSharpKeywords2.By:
                case CSharpKeywords2.On:
                    return 2;
                case CSharpKeywords2.Int32:
                    return 3;
                case CSharpKeywords2.Boolean:
                case CSharpKeywords2.Byte:
                case CSharpKeywords2.Char:
                case CSharpKeywords2.From:
                case CSharpKeywords2.Into:
                case CSharpKeywords2.Join:
                case CSharpKeywords2.Int64:
                case CSharpKeywords2.UInt32:
                case CSharpKeywords2.Void:
                case CSharpKeywords2.True:
                    return 4;
                case CSharpKeywords2.Int16:
                case CSharpKeywords2.Float:
                case CSharpKeywords2.False:
                case CSharpKeywords2.Group:
                case CSharpKeywords2.SByte:
                case CSharpKeywords2.UInt64:
                    return 5;
                case CSharpKeywords2.Double:
                case CSharpKeywords2.Equals:
                case CSharpKeywords2.Global:
                case CSharpKeywords2.Object:
                case CSharpKeywords2.Select:
                case CSharpKeywords2.String:
                case CSharpKeywords2.UInt16:
                    return 6;
                case CSharpKeywords2.Decimal:
                case CSharpKeywords2.OrderBy:
                    return 7;
                case CSharpKeywords2.__ArgList:
                case CSharpKeywords2.__MakeRef:
                case CSharpKeywords2.__RefType:
                case CSharpKeywords2.Ascending:
                    return 9;
                case CSharpKeywords2.Descending:
                case CSharpKeywords2.__RefValue:
                    return 10;
                default:
                    return 0;
            }
        }

        public static uint GetLength(CSharpKeywords1 caseKeyword)
        {
            switch (caseKeyword)
            {
                case CSharpKeywords1.As:
                case CSharpKeywords1.Do:
                case CSharpKeywords1.If:
                case CSharpKeywords1.In:
                case CSharpKeywords1.Is:
                    return 2;
                case CSharpKeywords1.For:
                case CSharpKeywords1.Get:
                case CSharpKeywords1.New:
                case CSharpKeywords1.Out:
                case CSharpKeywords1.Ref:
                case CSharpKeywords1.Set:
                case CSharpKeywords1.Try:
                    return 3;
                case CSharpKeywords1.Enum:
                case CSharpKeywords1.Base:
                case CSharpKeywords1.Case:
                case CSharpKeywords1.Else:
                case CSharpKeywords1.GoTo:
                case CSharpKeywords1.Lock:
                case CSharpKeywords1.This:
                case CSharpKeywords1.Null:
                    return 4;
                case CSharpKeywords1.Break:
                case CSharpKeywords1.Catch:
                case CSharpKeywords1.Class:
                case CSharpKeywords1.Const:
                case CSharpKeywords1.Event:
                case CSharpKeywords1.Fixed:
                case CSharpKeywords1.Throw:
                case CSharpKeywords1.Using:
                case CSharpKeywords1.Where:
                case CSharpKeywords1.While:
                case CSharpKeywords1.Yield:
                    return 5;
                case CSharpKeywords1.Public:
                case CSharpKeywords1.Extern:
                case CSharpKeywords1.Params:
                case CSharpKeywords1.Return:
                case CSharpKeywords1.Sealed:
                case CSharpKeywords1.SizeOf:
                case CSharpKeywords1.Static:
                case CSharpKeywords1.Struct:
                case CSharpKeywords1.Switch:
                case CSharpKeywords1.TypeOf:
                case CSharpKeywords1.UnSafe:
                    return 6;
                case CSharpKeywords1.Private:
                case CSharpKeywords1.Checked:
                case CSharpKeywords1.Default:
                case CSharpKeywords1.Finally:
                case CSharpKeywords1.ForEach:
                case CSharpKeywords1.Partial:
                case CSharpKeywords1.Virtual:
                    return 7;
                case CSharpKeywords1.Internal:
                case CSharpKeywords1.Abstract:
                case CSharpKeywords1.Continue:
                case CSharpKeywords1.Delegate:
                case CSharpKeywords1.Explicit:
                case CSharpKeywords1.Implicit:
                case CSharpKeywords1.Operator:
                case CSharpKeywords1.Override:
                case CSharpKeywords1.ReadOnly:
                case CSharpKeywords1.Volatile:
                    return 8;
                case CSharpKeywords1.Protected:
                case CSharpKeywords1.Interface:
                case CSharpKeywords1.Namespace:
                case CSharpKeywords1.UnChecked:
                    return 9;
                case CSharpKeywords1.StackAlloc:
                    return 10;
            }
            return 0;
        }

    }
}
