using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
        IMetadataEntity,
        IMember
    {
        /// <summary>
        /// Returns the <see cref="FieldMemberAttributes"/> that determine how the
        /// <see cref="IFieldMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        new FieldMemberAttributes Attributes { get; }
        /// <summary>
        /// Returns whether the <see cref="IFieldMember"/> is read-only.
        /// </summary>
        /// <remarks>Read-only fields can only be initialized during the 
        /// constructor phase of a type or instance.</remarks>
        bool ReadOnly { get; }
        /// <summary>
        /// Returns whether the <see cref="IFieldMember"/> is a constant value.
        /// </summary>
        /// <remarks>Constant values are evaluated at compile-time and folded into
        /// a single value of the appropriate data-type.</remarks>
        bool Constant { get; }
        /// <summary>
        /// Returns the type of data stored in the field.
        /// </summary>
        IType FieldType { get; }
    }
}
