using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    public interface _ICliType :
        ICliType
    {
        IModifiedType MakeModified(IControlledCollection<ICliMetadataCustomModifierSignature> modifiers);
    }
}
