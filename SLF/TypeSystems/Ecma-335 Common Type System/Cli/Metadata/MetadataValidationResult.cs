using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    /// <summary>
    /// The kinds of categories validation rules exist in.
    /// </summary>
    public enum MetadataValidationRuleCategory
    {
        /// <summary>
        /// Validation is not performed on the specified metadata.
        /// </summary>
        None,
        /// <summary>
        /// The metadata validation rule expects a certain structure,
        /// if the structure is not followed, then an error will result.
        /// </summary>
        /// <remarks><para>Metadata that violates rules in the <see cref="Error"/> 
        /// category might not be acceptable by conforming implementations
        /// of the Common Language Infrastructure, and thus, such metadata
        /// is not valid nor is it portable.</para></remarks>
        Error,
        /// <summary>
        /// The metadata validation rule expects a certain structure,
        /// the information would technically be correct; however, if
        /// it fails the validation, it is not represented in the
        /// shortest format possible.
        /// </summary>
        /// <remarks><para>
        /// Information failing this rule is not actually wrong,
        /// it's just that the compiler could have condensed the data
        /// more efficiently, the information is, thus, still portable.
        /// </para>
        /// <para>All conforming implementations shall support metadata
        /// that only violates rules in the warning caregory.</para>
        /// <para>See ECMA-335 22.1 Metadata Validation Rules.</para></remarks>
        Warning,
        /// <summary>
        /// The metadata validation rule expects a certain structure in order
        /// to be in compliance with the Common Language Specification (See Partition I
        /// of ECMA-335.)
        /// </summary>
        /// <remarks>Metadata with violates the expected CLS rules is still valid and portable;
        /// however some languages may exist that are incapable of processing it, even though
        /// the blobCacheData structures themselves are data-wise accurate.</remarks>
        Cls,
    }
}
