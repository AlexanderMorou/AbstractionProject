using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with an 
    /// ambiguous type within an assembly workspace.
    /// </summary>
    public interface IAmbiguousType :
        IType
    {
        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/> which contains the types
        /// that caused the ambiguity in the current scope.
        /// </summary>
        IControlledCollection<IType> Source { get; }
    }
}
