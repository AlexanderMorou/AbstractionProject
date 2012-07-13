using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public enum CliMetadataTypeSignatureKind 
    {
        ArrayType,
        ElementType,
        ElementTypeAndModifiers,
        GenericParameter,
        FunctionPointerType,
        NativeType,
        ReturnType,
        ValueOrClassType,
        GenericInstType,
        VectorArrayType,
    }
}
