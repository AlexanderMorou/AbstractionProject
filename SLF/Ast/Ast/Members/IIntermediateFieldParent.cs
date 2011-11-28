using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with the parent
    /// of a series of fields.
    /// </summary>
    /// <typeparam name="TField">The type of field in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateField">The type of field in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TFieldParent">The type which owns the fields
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateFieldParent">The type which owns the fields
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> :
        IFieldParent<TField, TFieldParent>,
        IIntermediateFieldParent
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
        /// Returns the <see cref="IIntermediateFieldMemberDictionary{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/> defined on the current 
        /// <see cref="IIntermediateFieldParent{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/>.
        /// </summary>
        new IIntermediateFieldMemberDictionary<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> Fields { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with the parent of a 
    /// series of fields.
    /// </summary>
    public interface IIntermediateFieldParent :
        IIntermediateMemberParent,
        IFieldParent
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateFieldMemberDictionary"/> defined 
        /// on the current <see cref="IIntermediateFieldParent"/>.
        /// </summary>
        new IIntermediateFieldMemberDictionary Fields { get; }
    }
}
