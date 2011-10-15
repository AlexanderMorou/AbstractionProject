using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a type-strict dictionary of intermediate field members.
    /// </summary>
    /// <typeparam name="TField">The type of field in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateField">The type of field in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TFieldParent">The type which owns the fields
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateFieldParent">The type which owns the fields
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateFieldMemberDictionary<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> :
        IntermediateGroupedMemberDictionary<TFieldParent, TIntermediateFieldParent, IGeneralMemberUniqueIdentifier, TField, TIntermediateField>,
        IIntermediateFieldMemberDictionary<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>,
        IIntermediateFieldMemberDictionary
        where TField :
            IFieldMember<TField, TFieldParent>
        where TIntermediateField :
            TField,
            IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
        where TIntermediateFieldParent :
            class,
            TFieldParent,
            IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateFieldMemberDictionary{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/>
        /// with the <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/> which groups the 
        /// elements of the <see cref="IntermediateFieldMemberDictionary{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/>
        /// with the <paramref name="parent"/>s other members.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateFieldParent"/> which contains the 
        /// <see cref="IntermediateFieldMemberDictionary{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/></param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="master"/> is null; or
        /// when <paramref name="parent"/> is null.</exception>
        public IntermediateFieldMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateFieldParent parent)
            : base(master, parent)
        {
        }
        public IntermediateFieldMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateFieldParent parent, IntermediateFieldMemberDictionary<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> sibling)
            : base(master, parent, sibling)
        {
        }

        #region IFieldMemberDictionary Members

        IFieldParent IFieldMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IIntermediateFieldMemberDictionary Members

        IIntermediateFieldParent IIntermediateFieldMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IIntermediateFieldMemberDictionary<TField,TIntermediateField,TFieldParent,TIntermediateFieldParent> Members

        public TIntermediateField Add(TypedName nameAndType)
        {
            return this.Add(nameAndType, null);
        }

        public TIntermediateField Add(TypedName nameAndType, IExpression initializationExpression)
        {
            var result = this.GetField(nameAndType);
            result.InitializationExpression = initializationExpression;
            base.AddDeclaration(result);
            return result;
        }

        protected abstract TIntermediateField GetField(TypedName nameAndType);
        #endregion

        #region IIntermediateFieldMemberDictionary Members

        IIntermediateFieldMember IIntermediateFieldMemberDictionary.Add(TypedName nameAndType)
        {
            return this.Add(nameAndType);
        }

        IIntermediateFieldMember IIntermediateFieldMemberDictionary.Add(TypedName nameAndType, IExpression initializationExpression)
        {
            return this.Add(nameAndType, initializationExpression);
        }

        #endregion
    }
}
