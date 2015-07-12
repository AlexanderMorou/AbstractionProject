/*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with an anonymous type
    /// whose structure is dependant upon the number of elements and their names.
    /// </summary>
    public interface IAnonymousType :
        IClassType
    {
        /// <summary>
        /// Returns the number of members in the <see cref="IAnonymousType"/>.
        /// </summary>
        int MemberCount { get; }
        /// <summary>
        /// Returns the number which correlates to the
        /// order in which the <see cref="IAnonymousType"/>
        /// was made in relation to the others.
        /// </summary>
        int Index { get; }
    }
}
