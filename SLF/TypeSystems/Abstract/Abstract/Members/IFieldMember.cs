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
    /// Defines generic properties and methods for 
    /// working with a field member.
    /// </summary>
    /// <typeparam name="TField">The type of field 
    /// used in the current implementation.</typeparam>
    /// <typeparam name="TFieldParent">The type 
    /// of parent used to contain the 
    /// <typeparamref name="TField"/> instances
    /// in the current implementation.</typeparam>
    public interface IFieldMember<TField, TFieldParent> :
        IMember<IGeneralMemberUniqueIdentifier, TFieldParent>,
        IFieldMember
        where TField :
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with a field member.
    /// </summary>
    public interface IFieldMember :
        IMember
    {
        /// <summary>
        /// Returns the type of data stored in the field.
        /// </summary>
        IType FieldType { get; }
    }
}
