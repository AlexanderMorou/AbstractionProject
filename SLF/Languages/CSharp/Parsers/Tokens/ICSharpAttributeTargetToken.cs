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
    public enum CSharpAttributeTarget 
    {
        None                = 0x0000000,
        Assembly            = 0x0000001,
        Event               = 0x0000002,
        FieldTarget         = 0x0000004,
        MethodTarget        = 0x0000008,
        Property            = 0x0000010,
        ReturnType          = 0x0000020,
        Type                = 0x0000040,
    }
    public interface ICSharpAttributeTargetToken :
        IToken
    {
        /// <summary>
        /// Returns the <see cref="CSharpAttributeTarget"/> which designates the type of target
        /// the <see cref="ICSharpAttributeTargetToken"/> represents.
        /// </summary>
        CSharpAttributeTarget Target { get; }
    }
}
