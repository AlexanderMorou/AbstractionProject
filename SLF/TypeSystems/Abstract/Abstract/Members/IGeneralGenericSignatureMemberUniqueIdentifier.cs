﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public interface IGeneralGenericSignatureMemberUniqueIdentifier :
        IGenericSignatureMemberUniqueIdentifier<IGeneralGenericSignatureMemberUniqueIdentifier>,
        IGeneralSignatureMemberUniqueIdentifier
    {
    }
}