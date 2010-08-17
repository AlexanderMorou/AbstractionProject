using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Tuples;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class AssemblyWorkspace
    {
        /// <summary>
        /// Provides a base dictionary for functionally unifying multiple
        /// specially typed dictionaries together.
        /// </summary>
        /// <typeparam name="TType">The kind of type which is represented by the dictionary.</typeparam>
        protected abstract partial class GroupedTypeDictionary<TType> :
            SubordinateDictionary<string, TType, IType>,
            IGroupedDeclarationDictionary<TType>,
            IGroupedDeclarationDictionary
            where TType :
                class,
                IType<TType>
        {
            private class FullNameCombinedComparer :
                IEqualityComparer<TType>
            {
                public static FullNameCombinedComparer Singleton = new FullNameCombinedComparer();
                private FullNameCombinedComparer()
                {
                }
                #region IEqualityComparer<TType> Members

                public bool Equals(TType x, TType y)
                {
                    return x.FullName == y.FullName;
                }

                public int GetHashCode(TType obj)
                {
                    return obj.FullName.GetHashCode();
                }

                #endregion
            }
            /// <summary>
            /// Data member representing the unioned parent series that is
            /// represented by the union.
            /// </summary>
            private ITypeParent[] sources;
            /// <summary>
            /// Data member representing the unioned parent that contains
            /// the unionized type series.
            /// </summary>
            private ITypeParent owner;
            /// <summary>
            /// Data member holding the type-name and associated 
            /// type sets for the current type union.
            /// </summary>
            private IDictionary<string, TType[]> groups;
            /// <summary>
            /// Data member holding the type instances represented by the 
            /// <see cref="GroupedTypeDictionary{TType}"/>.
            /// </summary>
            private TType[] productions;

            /// <summary>
            /// Creates a new <see cref="GroupedTypeDictionary{TType}"/> with the 
            /// <paramref name="master"/>, <paramref name="owner"/>,
            /// <paramref name="sources"/> and <paramref name="selector"/> 
            /// provided.
            /// </summary>
            /// <param name="master">The <see cref="FullTypesMasterDictionary"/> dictionary which
            /// contains the <see cref="GroupedTypeDictionary{TType}"/> elements
            /// on top of all other elements.</param>
            /// <param name="owner">The <see cref="ITypeParent"/>
            /// which contains the <see cref="GroupedTypeDictionary{TType}"/>.</param>
            /// <param name="selector">The <see cref="Func{T, TResult}"/>
            /// which obtains a <see cref="IType"/> relative
            /// to a series of <typeparamref name="TType"/>
            /// instances as a source.</param>
            public GroupedTypeDictionary(FullTypesMasterDictionary master, ITypeParent owner, ITypeParent[] sources, Func<ITypeParent, TType[]> selector)
                : base(master)
            {
                this.owner = owner;
                this.sources = sources;
                this.groups = GroupedTypeDictionary<TType>.ObtainGroups(sources, selector);
                this.productions = new TType[this.groups.Count];
            }

            private static IDictionary<string, TType[]> ObtainGroups(ITypeParent[] sources, Func<ITypeParent, TType[]> selector)
            {
                IDictionary<ITypeParent, TType[]> parentChildRelations =
                    (
                        from source in sources
                        select new {Parent = source, Children = selector(source)}
                    )
                    .ToArray()
                    .ToDictionary(
                        item =>
                        TupleHelper.GetTuple(
                            item.Parent,
                            item.Children));

                return (
                           from source in sources
                           select parentChildRelations[source]
                       )
                       .ToArray()
                       .ConcatinateSeries()
                       .Distinct(FullNameCombinedComparer.Singleton)
                       .OrderBy(p => p.Name)
                       .ToDictionary(
                           item =>
                           TupleHelper.GetTuple(
                               item.Name,
                               (from source in sources
                                where parentChildRelations[source].Any(p => p.Name == item.Name)
                                select parentChildRelations[source].First(p => p.Name == item.Name)).OrderBy(p => p.Assembly.ToString()).ToArray()));
            }

            #region IDeclarationDictionary<TType> Members

            /// <summary>
            /// Returns the index of the <paramref name="decl"/> provided.
            /// </summary>
            /// <param name="decl">The <typeparamref name="TType"/> in the <see cref="GroupedTypeDictionary{TType}"/> to return
            /// the index of.</param>
            /// <returns>An <see cref="Int32"/> value representing the index of the <paramref name="method"/> in the
            /// <see cref="GroupedTypeDictionary{TType}"/>, if present; -1, otherwise.</returns>
            /// <exception cref="System.ArgumentNullException">thrown when <paramref name="decl"/> is null.</exception>
            public int IndexOf(TType decl)
            {
                var name = decl.Name;
                if (!this.Keys.Contains(name))
                    return -1;
                var index = this.keysCollection.GetIndexOf(name);
                if (this.productions[index] != decl)
                    if (this.groups[name].Contains(decl))
                        return index;
                    else
                        return -1;
                return index;
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.sources = null;
                this.owner = null;
                this.groups.Clear();
                this.groups = null;
                this.keysCollection = null;
                this.valuesCollection = null;
            }

            #endregion

            #region IDeclarationDictionary Members

            int IDeclarationDictionary.IndexOf(IDeclaration decl)
            {
                if (!(decl is TType))
                    throw new ArgumentException("decl");
                return this.IndexOf((TType)decl);
            }

            #endregion

            /// <summary>
            /// Not supported.
            /// </summary>
            /// <param name="name"></param>
            /// <param name="type"></param>
            /// <exception cref="System.NotSupportedException">thrown, always.</exception>
            protected override void Add(string name, TType type)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Not supported.
            /// </summary>
            /// <exception cref="System.NotSupportedException">thrown, always.</exception>
            protected override void Clear()
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Not supported.
            /// </summary>
            /// <param name="name"></param>
            /// <exception cref="System.NotSupportedException">thrown, always.</exception>
            protected override bool Remove(string name)
            {
                throw new NotSupportedException();
            }

            public override bool ContainsKey(string key)
            {
                return this.Keys.Contains(key);
            }

            protected override TType OnGetThis(string name)
            {
                int index = this.Keys.GetIndexOf(name);
                if (index == -1)
                    throw new KeyNotFoundException();
                this.CheckIndexAt(index, name);
                return this.productions[index];
            }

            private void CheckIndexAt(int index, string name)
            {
                if (this.productions[index] != null)
                    return;
                if (this.groups[name].Length > 1)
                {
                    this.productions[index] = this.GetAmbiguousType(name, this.groups[name]);
                }
                else
                {
                    this.productions[index] = this.groups[name][0];
                }
            }

            protected abstract TType GetAmbiguousType(string name, TType[] tType);

            /// <summary>
            /// Determines whether the <see cref="GroupedTypeDictionary{TType}"/> contains a specific 
            /// value.</summary>
            /// <param name="item">
            /// The object to locate in the <see cref="GroupedTypeDictionary{TType}"/>.
            /// </param>
            /// <returns>
            /// true if <paramref name="item"/> is found in the <see cref="GroupedTypeDictionary{TType}"/>;
            /// otherwise, false.
            /// </returns>
            public override bool Contains(KeyValuePair<string, TType> item)
            {
                if (!(this.ContainsKey(item.Key)))
                    return false;
                else
                    return this.IndexOf(item.Value) != -1;
            }

            public ITypeParent Parent
            {
                get
                {
                    return this.owner;
                }
            }

            public override int Count
            {
                get
                {
                    return this.groups.Count;
                }
            }

            public override IEnumerator<KeyValuePair<string, TType>> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    string name = this.groups.Keys.ElementAt(i);
                    this.CheckIndexAt(i, name);
                    yield return new KeyValuePair<string, TType>(name, this.productions[i]);
                }
                yield break;
            }

            protected override void RemoveImpl(int index)
            {
                throw new NotSupportedException();
            }

            protected override void RemoveImpl(string key)
            {
                throw new NotSupportedException();
            }

            protected override ControlledStateDictionary<string, TType>.KeysCollection InitializeKeysCollection()
            {
                return new KeysCollection(this);
            }

            protected override ControlledStateDictionary<string, TType>.ValuesCollection InitializeValuesCollection()
            {
                return new ValuesCollection(this);
            }
        }
    }
}
