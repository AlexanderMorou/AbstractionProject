using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a generic base dictionary for generic parameters.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter used by the
    /// dictionary.</typeparam>
    /// <typeparam name="TParent">The owner of the <typeparamref name="TGenericParameter"/>
    /// dictionary.</typeparam>
    internal class GenericParameterDictionaryBase<TGenericParameter, TParent> :
        ControlledStateDictionary<IGenericParameterUniqueIdentifier, TGenericParameter>,
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
        /// Creates a new <see cref="GenericParameterDictionaryBase{TGenericParameter, TParent}"/> with
        /// the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TParent"/> that contains the 
        /// <see cref="GenericParameterDictionaryBase{TGenericParameter, TParent}"/>.
        /// </param>
        internal protected GenericParameterDictionaryBase(TParent parent)
        {
            this.parent = parent;
        }

        #region IGenericParameterDictionary<TParameter,TParent> Members

        /// <summary>
        /// Returns the <typeparamref name="TParent"/> of the <see cref="GenericParameterDictionaryBase{TGenericParameter, TParent}"/>.
        /// </summary>
        public TParent Parent
        {
            get { return this.parent; }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes the current <see cref="GenericParameterDictionaryBase{TGenericParameter, TParent}"/>.
        /// </summary>
        public void Dispose()
        {
            if (this.valuesInstance != null)
            {
                var valuesCopy = this.Values.ToArray();
                foreach (var value in valuesCopy)
                    value.Dispose();
                base._Clear();
            }
            this.parent = default(TParent);
        }

        #endregion

        #region IDeclarationDictionary Members

        int IDeclarationDictionary.IndexOf(IDeclaration decl)
        {
            if (!(decl is TGenericParameter))
                return -1;
            return this.IndexOf((TGenericParameter)(decl));
        }

        #endregion

        public int IndexOf(TGenericParameter decl)
        {
            int index = 0;
            if (this.valuesInstance == null)
                return -1;
            foreach (var item in this.Values)
                if (object.ReferenceEquals(decl, item))
                    return index;
                else
                    index++;
            return -1;
        }

    }
}
