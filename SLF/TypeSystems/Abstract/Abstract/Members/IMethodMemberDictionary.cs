using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a series of <typeparamref name="TMethod"/> instances
    /// contained within a <typeparamref name="TMethodParent"/> instance.
    /// </summary>
    /// <typeparam name="TMethod">The type of <see cref="IMethodMember{TMethod, TMethodParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TMethodParent">The type of <see cref="IMethodParent{TMethod, TType}"/> in the current
    /// implementation.</typeparam>
    public interface IMethodMemberDictionary<TMethod, TMethodParent> :
        IMethodSignatureMemberDictionary<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IGroupedMemberDictionary<TMethodParent, IGeneralGenericSignatureMemberUniqueIdentifier, TMethod>
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with a series of <see cref="IMethodMember"/> instances
    /// on a <see cref="IMethodParent"/>.
    /// </summary>
    public interface IMethodMemberDictionary :
        IMethodSignatureMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IMethodParent"/> which owns the <see cref="IMethodMember"/> series.
        /// </summary>
        new IMethodParent Parent { get; }
        /// <summary>
        /// Returns the index of the <paramref name="method"/> provided.
        /// </summary>
        /// <param name="method">The <see cref="IMethodMember"/> in the <see cref="IMethodMemberDictionary"/> to return
        /// the index of.</param>
        /// <returns>An <see cref="Int32"/> value representing the index of the <paramref name="method"/> in the
        /// <see cref="IMethodMemberDictionary"/>, if present; -1, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="method"/> is null.</exception>
        int IndexOf(IMethodMember method);

    }
}
