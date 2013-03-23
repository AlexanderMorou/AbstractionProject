using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public enum NamedParameterTargetType
    {
        Property,
        Field,
    }

    public enum CustomAttributeParameterValueType
    {
        String,
        BoxedNativeType,
        NativeType,
        EnumValue,
        Type,
        VectorArray,
    }

    public enum VectorArrayType
    {
        String,
        BoxedNativeType,
        NativeType,
        EnumValue,
        Type,
    }

    public interface ICliMetadataVectorArray
    {
        /// <summary>
        /// Returns the number of elements in the vector array blob.
        /// </summary>
        int Length { get; }
    }

    public interface ICliMetadataCustomAttributeParameter
    {

    }

    /// <summary>
    /// Defines properties and methods for working with a named argument for
    /// a custom attribute blob.
    /// </summary>
    public interface ICliMetadataCustomAttributeNamedParameter :
        ICliMetadataCustomAttributeParameter
    {
        /// <summary>
        /// Returns the <see cref="Name"/> targeted by the <see cref="ICliMetadataCustomAttributeNamedParameter"/>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the <see cref="NamedParameterTargetType"/> which denotes
        /// whether the argument targets a <see cref="NamedParameterTargetType.Field"/>
        /// or a <see cref="NamedParameterTargetType.Property"/>.
        /// </summary>
        NamedParameterTargetType ParameterTarget { get; }
        /// <summary>
        /// Returns the <see cref="CustomAttributeParameterValueType"/> which denotes the type of
        /// information stored within a custom attribute's argument.
        /// </summary>
        CustomAttributeParameterValueType ValueType { get; }
    }
}
