using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a dictionary
    /// of field members.
    /// </summary>
    /// <typeparam name="TField">The type of field in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateField">The type of field in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TFieldParent">The type which owns the fields
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateFieldParent">The type which owns the fields
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateFieldMemberDictionary<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> :
        IIntermediateGroupedMemberDictionary<TFieldParent, TIntermediateFieldParent, IGeneralMemberUniqueIdentifier, TField, TIntermediateField>,
        IFieldMemberDictionary<TField, TFieldParent>
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
        /// Adds a new <typeparamref name="TIntermediateField"/> with
        /// the <paramref name="nameAndType"/> provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which specifies the type of the field and its name.</param>
        /// <returns>A new <typeparamref name="TIntermediateField"/>
        /// which represents the field added.</returns>
        TIntermediateField Add(TypedName nameAndType);

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateField"/> with the
        /// <paramref name="nameAndType"/> and
        /// <paramref name="initializationExpression"/> provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which specifies the type of the field and its name.</param>
        /// <param name="initializationExpression">The <see cref="IExpression"/>
        /// to which the <typeparamref name="TIntermediateField"/> 
        /// is initialized to.</param>
        /// <returns>A new <typeparamref name="TIntermediateField"/>
        /// which represents the field added.</returns>
        TIntermediateField Add(TypedName nameAndType, IExpression initializationExpression);
    }
    /// <summary>
    /// Defines properties and methods for working with a dictionary of field
    /// members.
    /// </summary>
    public interface IIntermediateFieldMemberDictionary :
        IIntermediateGroupedMemberDictionary,
        IFieldMemberDictionary
    {
        /// <summary>
        /// Adds a new <see cref="IIntermediateFieldMember"/> with
        /// the <paramref name="nameAndType"/> provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which specifies the type of the field and its name.</param>
        /// <returns>A new <see cref="IIntermediateFieldMember"/>
        /// which represents the field added.</returns>
        IIntermediateFieldMember Add(TypedName nameAndType);
        /// <summary>
        /// Adds a new <see cref="IIntermediateFieldMember"/> with the
        /// <paramref name="nameAndType"/> and
        /// <paramref name="initializationExpression"/> provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which specifies the type of the field and its name.</param>
        /// <param name="initializationExpression">The <see cref="IExpression"/>
        /// to which the <see cref="IIntermediateFieldMember"/> 
        /// is initialized to.</param>
        /// <returns>A new <see cref="IIntermediateFieldMember"/>
        /// which represents the field added.</returns>
        IIntermediateFieldMember Add(TypedName nameAndType, IExpression initializationExpression);
        /// <summary>
        /// Returns the <see cref="IIntermediateFieldParent"/> which
        /// owns the <see cref="IIntermediateFieldMemberDictionary"/>.
        /// </summary>
        new IIntermediateFieldParent Parent { get; }
    }
}
