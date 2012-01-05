/*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a declaration
    /// that contains custom attributes.
    /// </summary>
    public interface ICustomAttributedEntity
    {
        /// <summary>
        /// Returns the <see cref="ICustomAttributeCollection"/> 
        /// associated to the <see cref="ICustomAttributedEntity"/>.
        /// </summary>
        ICustomAttributeCollection CustomAttributes { get; }
        /// <summary>
        /// Determines whether the <paramref name="attributeType"/> 
        /// is defined on the current 
        /// <see cref="ICustomAttributedEntity"/>.
        /// </summary>
        /// <param name="attributeType">The <see cref="IType"/> of
        /// the attribute to check the presence of.</param>
        /// <returns>true if an attribute of the given 
        /// <paramref name="attributeType"/> is defined
        /// on the current <see cref="ICustomAttributedEntity"/>.
        /// </returns>
        bool IsDefined(IType attributeType);
    }
}
