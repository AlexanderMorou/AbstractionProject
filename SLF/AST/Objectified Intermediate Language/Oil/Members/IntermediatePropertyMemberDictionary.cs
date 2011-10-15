using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a generic base class for working with a series of
    /// intermediate property signatures.
    /// </summary>
    /// <typeparam name="TProperty">The type of property signature used in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property signature used in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which acts as the parent of the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which acts as the parent of the 
    /// properties in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediatePropertyMemberDictionary<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        IntermediateGroupedMemberDictionary<TPropertyParent, TIntermediatePropertyParent, IGeneralMemberUniqueIdentifier, TProperty, TIntermediateProperty>,
        IIntermediatePropertyMemberDictionary<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>,
        IIntermediatePropertyMemberDictionary
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TPropertyParent :
            IPropertyParent<TProperty, TPropertyParent>
        where TIntermediatePropertyParent :
            class,
            TPropertyParent,
            IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediatePropertyMemberDictionary{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/>
        /// with the <paramref name="master"/> and
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the <paramref name="parent"/>'s full set of members.</param>
        /// <param name="parent">The <typeparamref name="TIntermediatePropertyParent"/>
        /// which contains the <see cref="IntermediatePropertyMemberDictionary{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/>.</param>
        internal protected IntermediatePropertyMemberDictionary(IntermediateFullMemberDictionary master, TIntermediatePropertyParent parent)
            : base(master, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediatePropertyMemberDictionary{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/>
        /// instance with the <paramref name="master"/>, <paramref name="parent"/> and
        /// <paramref name="root"/> provided.
        /// provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the <paramref name="parent"/>'s full set of members.</param>
        /// <param name="parent">The <typeparamref name="TIntermediatePropertyParent"/>
        /// which contains the <see cref="IntermediatePropertyMemberDictionary{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/>.</param>
        /// <param name="root">The root <see cref="IntermediatePropertyMemberDictionary{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/>
        /// instance which shares a common set of elements with a different partial
        /// instance of the <paramref name="parent"/>.</param>
        internal protected IntermediatePropertyMemberDictionary(IntermediateFullMemberDictionary master, TIntermediatePropertyParent parent, IntermediatePropertyMemberDictionary<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> root)
            : base(master, parent, root)
        {
        }

        /// <summary>
        /// Obtains a new <typeparamref name="TIntermediateProperty"/>
        /// with the <paramref name="nameAndType"/> provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which designates the <see cref="String"/> name and <see cref="IType"/> of the
        /// <typeparamref name="TIntermediateProperty"/> to obtain.</param>
        /// <returns></returns>
        protected abstract TIntermediateProperty GetProperty(TypedName nameAndType);

        #region IIntermediatePropertyMemberDictionary<TProperty,TIntermediateProperty,TPropertyParent,TIntermediatePropertyParent> Members


        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateProperty"/> instance with
        /// the <paramref name="nameAndType"/> provided.
        /// </summary>
        /// <param name="nameAndType">The name and type of the property to add.</param>
        /// <returns>A new <typeparamref name="TIntermediateProperty"/> instance.</returns>
        /// <exception cref="System.ArgumentException">The <see cref="TypedName.Name"/> on <paramref name="nameAndType"/> is null -or-
        /// the <see cref="TypedName.Source"/> on <paramref name="nameAndType"/> is 
        /// <see cref="TypedNameSource.InvalidReference"/>.</exception>
        public virtual TIntermediateProperty Add(TypedName nameAndType)
        {
            return Add(nameAndType, true, true);
        }

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateProperty"/> instance with
        /// the <paramref name="nameAndType"/>, <paramref name="canGet"/>, 
        /// and <paramref name="canSet"/> provided.
        /// </summary>
        /// <param name="nameAndType">The name and type of the property to add.</param>
        /// <param name="canGet">Whether the property can be read.</param>
        /// <param name="canSet">Whether the property can be written.</param>
        /// <returns>A new <typeparamref name="TIntermediateProperty"/> instance.</returns>
        /// <exception cref="System.ArgumentException">The <see cref="TypedName.Name"/> on <paramref name="nameAndType"/> is null -or-
        /// the <see cref="TypedName.Source"/> on <paramref name="nameAndType"/> is 
        /// <see cref="TypedNameSource.InvalidReference"/>.</exception>
        public TIntermediateProperty Add(TypedName nameAndType, bool canGet, bool canSet)
        {
            var result = this.GetProperty(nameAndType);
            result.CanRead = canGet;
            result.CanWrite = canSet;
            this.AddDeclaration(result);
            return result;
        }

        #endregion

        #region IIntermediatePropertyMemberDictionary Members

        IIntermediatePropertyMember IIntermediatePropertyMemberDictionary.Add(TypedName nameAndType)
        {
            return this.Add(nameAndType, true, true);
        }

        IIntermediatePropertyMember IIntermediatePropertyMemberDictionary.Add(TypedName nameAndType, bool canGet, bool canSet)
        {
            return this.Add(nameAndType, canGet, canSet);
        }

        #endregion

        #region IIntermediatePropertySignatureMemberDictionary Members

        IIntermediatePropertySignatureMember IIntermediatePropertySignatureMemberDictionary.Add(TypedName nameAndType)
        {
            return this.Add(nameAndType, true, true);
        }

        IIntermediatePropertySignatureMember IIntermediatePropertySignatureMemberDictionary.Add(TypedName nameAndType, bool canGet, bool canSet)
        {
            return this.Add(nameAndType, canGet, canSet);
        }

        #endregion

        #region IPropertyMemberDictionary Members

        IPropertyParent IPropertyMemberDictionary.Parent
        {
            get { return base.Parent; }
        }

        #endregion

    }
}
