using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a series of <typeparamref name="TField"/> members.
    /// </summary>
    /// <typeparam name="TField">The type of <see cref="IFieldMember{TField, TFieldParent}"/> used
    /// in the current implementation.</typeparam>
    /// <typeparam name="TFieldParent">The type of <see cref="IFieldParent{TField, TFieldParent}"/>
    /// used in the current implementation.</typeparam>
    public interface IFieldMemberDictionary<TField, TFieldParent> :
        IGroupedMemberDictionary<TFieldParent, IGeneralMemberUniqueIdentifier, TField>
        where TField :
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a series
    /// of <see cref="IFieldMember"/> instances.
    /// </summary>
    public interface IFieldMemberDictionary :
        IGroupedMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IFieldParent"/> which owns the <see cref="IFieldMember"/> series.
        /// </summary>
        new IFieldParent Parent { get; }
    }
}
