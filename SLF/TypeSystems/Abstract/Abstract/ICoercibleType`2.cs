using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working 
    /// with a coercible <see cref="IType{TType}"/>
    /// </summary>
    /// <typeparam name="TCoercion">
    /// The type of <see cref="ICoercionMember"/> used 
    /// in the current implementation.</typeparam>
    /// <typeparam name="TType">
    /// The type of coercible <see cref="IType{TType}"/> 
    /// that contains <typeparamref name="TCoercion"/> 
    /// members in the current implementation.</typeparam>
    public interface ICoercibleType<TCoercion, TType> :
        IType<TType>,
        IMemberParent,
        ICoercibleType
        where TCoercion :
            ICoercionMember<TCoercion, TType>
        where TType :
            ICoercibleType<TCoercion, TType>
    {
    }
}
