﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public interface ISignatureSearchResultSet :
        IControlledDictionary<ISignatureMemberUniqueIdentifier, ISignatureSearchResult>
    {

    }
}
