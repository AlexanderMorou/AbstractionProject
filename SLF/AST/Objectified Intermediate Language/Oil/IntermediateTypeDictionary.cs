using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public abstract class IntermediateTypeDictionary<TType, TIntermediateType> :
        IntermediateGroupedDeclarationDictionary<TType, IType, TIntermediateType>,
        IIntermediateTypeDictionary<TType, TIntermediateType>
        where TType :
            IType<TType>
        where TIntermediateType :
            class,
            IIntermediateType,
            TType
    {
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private IIntermediateTypeParent parent;
        public IntermediateTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(master)
        {
            this.parent = parent;
        }

        public IntermediateTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateTypeDictionary<TType, TIntermediateType> root)
            : base(master, root)
        {
            this.parent = parent;
        }

        #region IIntermediateTypeDictionary<TType,TIntermediateType> Members

        /// <summary>
        /// Creates and adds a new <typeparamref name="TIntermediateType"/> 
        /// instance with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the new
        /// <typeparamref name="TIntermediateType"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateType"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when a type by the <paramref name="name"/>
        /// provided already exists in the containing type parent, or <paramref name="name"/> equals
        /// <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        public TIntermediateType Add(string name)
        {
            var result = this.GetNewType(name);
            this.AddDeclaration(result);
            return result;
        }

        protected abstract TIntermediateType GetNewType(string name);

        /// <summary>
        /// Creates and inserts a new <typeparamref name="TIntermediateType"/> instance
        /// into the current <see cref="IIntermediateTypeDictionary{TType, TIntermediateType}"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> that defines the name of the
        /// new <typeparamref name="TIntermediateType"/> to create.</param>
        /// <param name="module">The <see cref="IIntermediateModule"/> to add the 
        /// new <typeparamref name="TIntermediateType"/> to.</param>
        /// <returns>A new <typeparamref name="TIntermediateType"/> instance.</returns>
        /// <remarks>Using this method creates a new partial instance 
        /// of the assembly to ensure the class can be translated into
        /// a file of its own.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>, or <paramref name="module"/>, is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when a type by the <paramref name="name"/>
        /// provided already exists in the containing type parent, or <paramref name="name"/> 
        /// is <see cref="String.Empty"/>.</exception>
        public TIntermediateType Add(string name, IIntermediateModule module)
        {

            if (name == null)
                throw new ArgumentNullException("name");
            if (module == null)
                throw new ArgumentNullException("module");
            if (name == string.Empty)
                throw new ArgumentException("The name provided cannot be empty.", "name");
            if (module.Parent.GetRoot() != this.Parent.Assembly.GetRoot())
                throw new ArgumentException("module");

            var result = this.Add(name);
            result.DeclaringModule = module;
            return result;
        }

        public void Remove(TIntermediateType type)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public IIntermediateTypeParent Parent
        {
            get { return this.parent; }
        }

        public void RemoveSoft(TIntermediateType type)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
