﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataGenericInstanceTypeSignature :
        ICliMetadataValueOrClassTypeSignature
    {
        IControlledCollection<ICliMetadataTypeSignature> GenericParameters { get; }
    }
}
