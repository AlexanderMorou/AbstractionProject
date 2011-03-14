/*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a type that can be instantiated.
    /// </summary>
    public interface IInstantiableType :
        ICreatableType,
        IPropertyParentType,
        IFieldParent,
        ITypeParent,
        IMethodParent
    {
    }
}
