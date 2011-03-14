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
    /// Defines generic properties and methods 
    /// for working with the parent of a series 
    /// of <typeparamref name="TField"/> instances.
    /// </summary>
    /// <typeparam name="TField">The type of <see cref="IFieldMember{TField, TFieldMember}"/> in the current implementation.</typeparam>
    /// <typeparam name="TParent">The type of parent of a series of <typeparamref name="TField"/> in the current implementation.</typeparam>
    public interface IFieldParent<TField, TParent> :
        IFieldParent
        where TField :
            IFieldMember<TField, TParent>
        where TParent :
            IFieldParent<TField, TParent>
    {
        /// <summary>
        /// Returns the <see cref="IFieldMemberDictionary{TField, TParent}"/> defined on the current 
        /// <see cref="IFieldParent{TField, TParent}"/>.
        /// </summary>
        new IFieldMemberDictionary<TField, TParent> Fields { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a type that can contain fields.
    /// </summary>
    public interface IFieldParent :
        IMemberParent
    {
        /// <summary>
        /// Returns the <see cref="IFieldMemberDictionary"/> defined on the current <see cref="IFieldParent"/>.
        /// </summary>
        IFieldMemberDictionary Fields { get; }
    }
}
