using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public enum SignatureKinds
    {
        /// <summary>
        /// Represents the signature of a method definition.
        /// </summary>
        MethodDefSig,
        /// <summary>
        /// Represents the signature of a method reference.
        /// </summary>
        MethodRefSig,
        /// <summary>
        /// Represents the signature of a method called via a function pointer
        /// </summary>
        StandaloneMethodSig,
        /// <summary>
        /// Represents the signature of a field.
        /// </summary>
        FieldSig = 0x06,
        /// <summary>
        /// Represents the signature of a local variable signature.
        /// </summary>
        StandaloneLocalVarSig = 0x07,
        /// <summary>
        /// Represents the signature of a property.
        /// </summary>
        PropertySig = 0x08,
        /// <summary>
        /// Represents the signature of a custom modifier.
        /// </summary>
        CustomModifier,
        /// <summary>
        /// Represents the signature of a constraint.
        /// </summary>
        Constraint,
        /// <summary>
        /// Represents the signature of a parameter.
        /// </summary>
        Param,
        /// <summary>
        /// Represents the signature of a type.
        /// </summary>
        Type,
        /// <summary>
        /// Represents the structure of an array.
        /// </summary>
        ArrayShape,
        /// <summary>
        /// Represents the structure of a type specification.
        /// </summary>
        TypeSpec,
        /// <summary>
        /// Represents the structure of a method specification.
        /// </summary>
        MethodSpec,
        /// <summary>
        /// Represents the structure of a standalone signature as either
        /// the local variables of a method or the signature of a method
        /// used as a function pointer.
        /// </summary>
        StandaloneSignature,
        MemberRefSig,
        ArrayType,
        CustomAttribute,
        TypeWithElementType,
        ModifiedTypeWithElementType,
        FunctionPointerType,
        GenericTypeInstance,
        GenericParameter,
        NativeType,
        ReturnTypeSignature,
        ClassOrValueType,
        VariableArgumentParameter
    }
}
