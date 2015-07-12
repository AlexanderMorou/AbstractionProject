using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Denotes the kinds of targets for metadatum instances.
    /// </summary>
    [Flags]
    public enum MetadatumTargets
    {
        None                = 0x00000000,
        /// <summary>
        /// The metadatum targets assemblies.
        /// </summary>
        Assembly            = 0x00000001,
        /// <summary>
        /// The metadatum targets classes.
        /// </summary>
        Class               = 0x00000002,
        /// <summary>
        /// The metadatum targets delegates.
        /// </summary>
        Delegate            = 0x00000004,
        /// <summary>
        /// The metadatum targets enumerations.
        /// </summary>
        Enum                = 0x00000008,
        /// <summary>
        /// THe metadatum targets interfaces.
        /// </summary>
        Interface           = 0x00000010,
        /// <summary>
        /// The metadatum targets data structures.
        /// </summary>
        Struct              = 0x00000020,
        /// <summary>
        /// The metadatum targets generic parameters.
        /// </summary>
        GenericParameter    = 0x00000040,
        /// <summary>
        /// The metadatum targets constructors.
        /// </summary>
        Constructor         = 0x00000080,
        /// <summary>
        /// The metadatum targets events.
        /// </summary>
        Event               = 0x00000100,
        /// <summary>
        /// The metadatum targets fields.
        /// </summary>
        Field               = 0x00000200,
        /// <summary>
        /// The metadatum targets indexers.
        /// </summary>
        Indexer             = 0x00000400,
        /// <summary>
        /// The metadatum targets methods.
        /// </summary>
        Method              = 0x00000800,
        /// <summary>
        /// The metadatum targets properties.
        /// </summary>
        Property            = 0x00001000,
        /// <summary>
        /// The metadatum targets paramters.
        /// </summary>
        Parameter           = 0x00002000,
        /// <summary>
        /// The metadatum targets the return-type of a method.
        /// </summary>
        ReturnValue         = 0x00004000,
        /// <summary>
        /// The metadatum targets a module.
        /// </summary>
        Module              = 0x00008000,
        /// <summary>
        /// The metadatum targets all classifications.
        /// </summary>
        All                 = 0x0000FFFF,
    }
    /// <summary>
    /// Defines properties and methods for working with a metadatum marshalling service.
    /// </summary>
    public interface IMetadatumMarshalService :
        ILanguageService
    {
        /// <summary>
        /// Makes a <paramref name="targetMetadatumType"/> a valid metadatum.
        /// </summary>
        /// <param name="targetMetadatumType">The <see cref="IType"/>
        /// to set up as a metadatum.</param>
        void MakeMetadatum(IType targetMetadatumType);
        /// <summary>
        /// Makes a <paramref name="targetMetadatumType"/> a metadatum which derives from
        /// the <paramref name="baseMetadatumType"/>.
        /// </summary>
        /// <param name="targetMetadatumType">The <see cref="IType"/>
        /// to set up as a metadatum variant of <paramref name="baseMetadatumType"/>.</param>
        /// <param name="baseMetadatumType">The <see cref="IType"/> which acts as the
        /// base metadatum.</param>
        void MakeMetadatum(IType targetMetadatumType, IType baseMetadatumType);
        /// <summary>
        /// Sets the <see cref="MetadatumTargets"/> of the <paramref name="targetMetadatumType"/>.
        /// </summary>
        /// <param name="targetMetadatumType">The <see cref="IType"/>
        /// to set the <paramref name="targets"/> for.</param>
        /// <param name="targets">The <see cref="MetadatumTargets"/> which <paramref name="targetMetadatumType"/>
        /// is intended for.</param>
        void SetTargets(IType targetMetadatumType, MetadatumTargets targets);
        /// <summary>
        /// Returns the <see cref="MetadatumTargets"/> for a particular
        /// <paramref name="metadatumType"/>.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/> which functions as a metadatum
        /// type.</param>
        /// <returns>The <see cref="MetadatumTargets"/> which the <paramref name="metadatumType"/>
        /// is intended for.</returns>
        MetadatumTargets GetTargets(IType metadatumType);
        /// <summary>
        /// Returns whether the <paramref name="metadatumType"/> is a metadatum.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/> to check for metadatum status.</param>
        /// <returns>true, if <paramref name="metadatumType"/> is a valid type for metadata;
        /// false, otherwise.</returns>
        bool IsMetadatum(IType metadatumType);
    }
}
