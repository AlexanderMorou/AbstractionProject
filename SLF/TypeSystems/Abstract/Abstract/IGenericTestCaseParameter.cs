/*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a test-case
    /// type-parameter used to represent the constraints, members
    /// and so forth with the type-replacements defined.
    /// </summary>
    public interface IGenericTestCaseParameter :
        IGenericParameter
    {
        /// <summary>
        /// Returns the original <see cref="IGenericParameter"/>
        /// from the <see cref="IGenericTestCaseParameter"/>
        /// </summary>
        IGenericParameter Original { get; }
    }
}
