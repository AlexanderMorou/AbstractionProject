﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers.Oilexer
{
    internal interface IUnicodeCollectiveTargetGraph :
        IControlledStateCollection<IUnicodeTargetGraph>
    {
        void Add(IUnicodeTargetGraph graph);

        IUnicodeTargetGraph Find(IUnicodeTargetGraph duplicate);
    }
}