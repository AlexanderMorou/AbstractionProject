﻿using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    internal interface _ICustomAttributeDefinitionParameterCollection :
        ICustomAttributeDefinitionParameterCollection
    {
        ICustomAttributeDefinitionParameter AddInternal<T>(T value);
        ICustomAttributeDefinitionParameter AddInternal<T>(string name, T value);
    }
}