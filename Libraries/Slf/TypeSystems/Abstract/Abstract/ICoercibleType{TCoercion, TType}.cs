using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working 
    /// with a coercible <see cref="IType{TTypeIdentifier, TType}"/>
    /// </summary>
    /// <typeparam name="TCoercion">
    /// The type of <see cref="ICoercionMember"/> used 
    /// in the current implementation.</typeparam>
    /// <typeparam name="TCoercionIdentifier">The kind of unique identifier to use
    /// relative to the coercion member, to differentiate it from other
    /// similar coercions, and other members alike.</typeparam>
    /// <typeparam name="TCoercionParentIdentifier">The kind of unique identifier to use relative
    /// to the type, to differentiate it between its sibling types.</typeparam>
    /// <typeparam name="TCoercionParent">
    /// The type of coercible <see cref="IType{TTypeIdentifier, TType}"/> 
    /// that contains <typeparamref name="TCoercion"/> 
    /// members in the current implementation.</typeparam>
    public interface ICoercibleType<TCoercionIdentifier, TCoercion, TCoercionParent> :
        IMemberParent,
        ICoercibleType
        where TCoercionIdentifier :
            IMemberUniqueIdentifier
        where TCoercion :
            ICoercionMember<TCoercionIdentifier, TCoercion, TCoercionParent>
        where TCoercionParent :
            ICoercibleType<TCoercionIdentifier, TCoercion, TCoercionParent>
    {
    }
}
