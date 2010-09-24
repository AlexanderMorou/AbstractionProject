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
    public class CSharpAttributeTargetToken :
        Token,
        ICSharpAttributeTargetToken
    {
        public CSharpAttributeTargetToken(FileLocale location, CSharpAttributeTarget target)
            : base(location)
        {
            this.Target = target;
        }

        #region ICSharpAttributeTargetToken Members

        public CSharpAttributeTarget Target { get; private set; }

        #endregion

        public override uint Length
        {
            get {
                switch (Target)
                {
                    case CSharpAttributeTarget.Property:
                    case CSharpAttributeTarget.Assembly:
                        return 8;
                    case CSharpAttributeTarget.Event:
                    case CSharpAttributeTarget.FieldTarget:
                        return 5;
                    case CSharpAttributeTarget.MethodTarget:
                    case CSharpAttributeTarget.ReturnType:
                        return 6;
                    case CSharpAttributeTarget.Type:
                        return 4;
                    default:
                        return 0;
                }
            }
        }
    }
}
