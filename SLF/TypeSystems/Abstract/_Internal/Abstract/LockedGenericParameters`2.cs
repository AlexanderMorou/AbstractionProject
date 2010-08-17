using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal sealed class LockedGenericParameters<TGenericParameter, TParent> :
        LockedDeclarationsBase<TGenericParameter>,
        IGenericParameterDictionary<TGenericParameter, TParent>,
        IGenericParameterDictionary
        where TGenericParameter :
            IGenericParameter<TGenericParameter, TParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
    {
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private TParent parent;

        /// <summary>
        /// Creates a new <see cref="LockedGenericParameters{TGenericParameter, TParent}"/> with
        /// the <paramref name="parent"/> and <paramref name="items"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TParent"/> that contains the 
        /// <see cref="LockedGenericParameters{TGenericParameter, TParent}"/>.
        /// </param>
        /// <param name="items">The <see cref="IEnumerable{T}"/> series of <typeparamref name="TGenericParameter"/>
        /// to contain and lock.</param>
        internal LockedGenericParameters(TParent parent, IEnumerable<TGenericParameter> items) :
            base(items)
        {
            this.parent = parent;
        }

        internal LockedGenericParameters(TParent parent)
            : base(new Dictionary<string, TGenericParameter>())
        {
            this.parent = parent;
        }

        #region IGenericParameterDictionary<TGenericParameter,TParent> Members

        /// <summary>
        /// Returns the <typeparamref name="TParent"/> of the <see cref="LockedGenericParameters{TGenericParameter, TParent}"/>.
        /// </summary>
        public TParent Parent
        {
            get { return this.parent; }
        }

        #endregion

    }
}
