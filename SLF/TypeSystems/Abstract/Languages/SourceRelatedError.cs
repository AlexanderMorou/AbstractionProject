﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class SourceRelatedError :
        SourceRelatedMessage,
        ISourceRelatedError
    {

        public SourceRelatedError(string errorText, LineColumnPair start, LineColumnPair end, string fileName)
            : base(errorText, start, end, fileName)
        {
        }
    }
}