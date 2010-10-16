using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate field member.
    /// </summary>
    /// <typeparam name="TField">The type of field in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateField">The type of field in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TFieldParent">The type which owns the fields
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateFieldParent">The type which owns the fields
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> :
        IIntermediateMember<TFieldParent, TIntermediateFieldParent>,
        IIntermediateFieldMember,
        IFieldMember<TField, TFieldParent>
        where TField :
            IFieldMember<TField, TFieldParent>
        where TIntermediateField :
            TField,
            IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
        where TIntermediateFieldParent :
            TFieldParent,
            IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/>
        /// used to initialize the 
        /// <see cref="IIntermediateFieldMember{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/>.
        /// </summary>
        IExpression InitializationExpression { get; set; }
    }

    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// field member.
    /// </summary>
    public interface IIntermediateFieldMember :
        IIntermediateMember,
        IFieldMember
    {
        /// <summary>
        /// Returns the type of data stored in the field.
        /// </summary>
        new IType FieldType { get; set; }
        /// <summary>
        /// Obtains a reference expression which refers to the current
        /// <see cref="IIntermediateFieldMember"/> with the <paramref name="source"/>
        /// which leads up to it.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// which leads up to the field.</param>
        /// <returns>A <see cref="IFieldReferenceExpression"/> which refers to the current
        /// <see cref="IIntermediateFieldMember"/> with the <paramref name="source"/>
        /// which leads up to it.</returns>
        IFieldReferenceExpression GetReference(IMemberParentReferenceExpression source = null);
        /// <summary>
        /// Returns/sets the <see cref="String"/> associated to the
        /// documentation summary related to the current 
        /// <see cref="IIntermediateEnumFieldMember"/>.
        /// </summary>
        string Summary { get; set; }
    }
}
