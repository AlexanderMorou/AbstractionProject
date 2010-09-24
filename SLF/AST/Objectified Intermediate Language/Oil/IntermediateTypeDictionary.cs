using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
        /// Adds a series of <typeparamref name="TIntermediateType"/> instances
        /// through the <paramref name="types"/> provided.
        /// </summary>
        /// <param name="types">The <see cref="IEnumerable{T}"/>
        /// of <typeparamref name="TIntermediateType"/> elements
        /// to insert.</param>
        public void AddRange(IEnumerable<TIntermediateType> types)
        {
            this.AddRange(types.ToArray());
        }

        /// <summary>
        /// Adds a series of <typeparamref name="TIntermediateType"/> instances
        /// through the <paramref name="types"/> provided.
        /// </summary>
        /// <param name="types">The <typeparamref name="TIntermediateType"/>
        /// array to insert into the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}"/>.</param>
        public void AddRange(params TIntermediateType[] types)
        {
            foreach (var type in types)
                if (this.Values.Contains(type))
                    throw new InvalidOperationException("type already exists.");
                else if (type.Parent != this.Parent)
                    throw new ArgumentException("type's parent must be equal to dictionary parent.", "type");
            this.AddDeclarations(types);
        }

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
            if (this.Values.Contains(type))
            {
                int index = this.Values.IndexOf(type);
                type.Dispose();
                this._Remove(index);
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new ArgumentOutOfRangeException("index");
            this._Remove(index);
        }

        public IIntermediateTypeParent Parent
        {
            get { return this.parent; }
        }

        public void RemoveSoft(TIntermediateType type)
        {
            if (this.Values.Contains(type))
            {
                int index = this.Values.IndexOf(type);
                this._Remove(index);
            }
        }

        public void Add(TIntermediateType type)
        {
            if (this.Values.Contains(type))
                throw new InvalidOperationException("type already exists.");
            if (type.Parent != this.Parent)
                throw new ArgumentException("type's parent must be equal to dictionary parent.", "type");
            this._Add(new KeyValuePair<string, TType>(type.UniqueIdentifier, type));
        }
        #endregion
    }
}
