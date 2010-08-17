using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
using AllenCopeland.Abstraction.Slf._Internal;
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
        public partial class CombinedNamespaceDictionary :
            INamespaceDictionary,
            IControlledStateDictionary
        {
            /* *
             * ToDo: Insert injection, and extraction code to enable the combined namespace 
             *       dictionary, combined namespace, individual type dictionaries, the full
             *       types dictionary, and assembly workspace types to be malleable, so when
             *       references are added, or removed, the data is properly structured to the
             *       functional nature of the combined architecture.
             * */
            private KeysCollection keys;
            private ValuesCollection values;
            private INamespaceParent[] sources;
            private INamespaceParent owner;
            private IDictionary<string, INamespaceDeclaration[]> groups;
            private CombinedNamespace[] productions;
            /// <summary>
            /// Creates a new <see cref="CombinedNamespaceDictionary"/> with the <paramref name="sources"/>
            /// provided.
            /// </summary>
            /// <param name="owner">The <see cref="INamespaceParent"/> which represents
            /// the union of the <paramref name="sources"/>.</param>
            /// <param name="sources">The series of <see cref="INamespaceParent"/> instances from which
            /// the current instance derives.</param>
            internal CombinedNamespaceDictionary(INamespaceParent owner, INamespaceParent[] sources)
            {
                if (owner == null)
                    throw new ArgumentNullException("owner");
                if (sources == null)
                    throw new ArgumentNullException("sources");
                this.sources = sources;
                this.owner = owner;
                this.groups = ObtainGroupings(sources);
                this.productions = new CombinedNamespace[this.groups.Count];
            }

            private static IDictionary<string, INamespaceDeclaration[]> ObtainGroupings(INamespaceParent[] sources)
            {
                return (
                            from source in sources
                            select source.Namespaces.Keys
                                .ToArray() //Get the array form
                        )
                        //Immediately process the query
                        .ToArray()
                        //Concatinate the array of arrays into one.
                        .ConcatinateSeries()
                        //Obtain the distinct elements
                        .Distinct()
                        //Order them by the namespace name.
                        .OrderBy(p => p)
                        //Convert it to a dictionary...
                        .ToDictionary
                        (
                        //... using the namespace name as uniqueName, ...
                            uniqueName =>
                                TupleHelper.GetTuple(
                                //... selecting the name as the key, and...
                                    uniqueName,
                                //... the list of namespaces from the sources 
                                //    that match that name.
                                    (
                                      from parent in sources
                                      where parent.Namespaces.ContainsKey(uniqueName)
                                      select parent.Namespaces[uniqueName]
                                    ).ToArray())
                        );
            }

            #region INamespaceDictionary Members

            /// <summary>
            /// Returns the <see cref="INamespaceParent"/>
            /// which contains the 
            /// <see cref="CombinedNamespaceDictionary"/>.
            /// </summary>
            public INamespaceParent Parent
            {
                get { return this.owner; }
            }

            /// <summary>
            /// Returns whether the <paramref name="path"/> provided
            /// exists in the <see cref="CombinedNamespaceDictionary"/> and 
            /// its elements and their children.
            /// </summary>
            /// <param name="path">The <see cref="String"/> that 
            /// represents the path of the namespace to check 
            /// the existance of.</param>
            /// <returns>true if a <see cref="INamespaceDeclaration"/>
            /// exists under the path provided; false, otherwise.</returns>
            /// <remarks>The path must be a series of 
            /// quantifiable namespace names delimited by a
            /// period.</remarks>
            public bool PathExists(string path)
            {
                return _CoreHelperMethods.PathExists(this, path);
            }

            #endregion

            #region IDeclarationDictionary<INamespaceDeclaration> Members

            public int IndexOf(INamespaceDeclaration decl)
            {
                var name = decl.Name;
                if (!this.ContainsKey(name))
                    return -1;
                int index = this.groups.Keys.GetIndexOf(name);
                if (this.productions[index] == null ||
                    this.productions[index] != decl)
                    if (this.groups[name].Contains(decl))
                        return index;
                return index;
            }

            #endregion

            #region IControlledStateDictionary<string,INamespaceDeclaration> Members

            public IControlledStateCollection<string> Keys
            {
                get {
                    if (this.keys == null)
                        this.keys = new KeysCollection(this);
                    return this.keys;
                }
            }

            public IControlledStateCollection<INamespaceDeclaration> Values
            {
                get {
                    if (this.values == null)
                        this.values = new ValuesCollection(this);
                    return this.values;
                }
            }

            public INamespaceDeclaration this[string key]
            {
                get {
                    return this.GetThis(key);
                }
            }

            public bool ContainsKey(string key)
            {
                return this.groups.ContainsKey(key);
            }

            public bool TryGetValue(string key, out INamespaceDeclaration value)
            {
                if (!this.ContainsKey(key))
                {
                    value = null;
                    return false;
                }
                int index = this.groups.Keys.GetIndexOf(key);
                this.CheckIndexAt(index, key);
                value = this.productions[index];
                return true;
            }

            #endregion

            #region IControlledStateCollection<KeyValuePair<string,INamespaceDeclaration>> Members

            public int Count
            {
                get { return this.groups.Count; }
            }

            public bool Contains(KeyValuePair<string, INamespaceDeclaration> item)
            {
                if (!this.ContainsKey(item.Key))
                    return false;
                int index = this.groups.Keys.GetIndexOf(item.Key);
                if (this.productions[index] == null)
                    return this.groups[item.Key].Contains(item.Value);
                return item.Value == this.productions[index];
            }

            void IControlledStateCollection<KeyValuePair<string,INamespaceDeclaration>>.CopyTo(KeyValuePair<string, INamespaceDeclaration>[] array, int arrayIndex)
            {
                this.ToArray().CopyTo(array, arrayIndex);
            }

            public KeyValuePair<string, INamespaceDeclaration> this[int index]
            {
                get
                {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException("index");
                    string key = this.groups.Keys.ElementAt(index);
                    this.CheckIndexAt(index, key);
                    return new KeyValuePair<string, INamespaceDeclaration>(key, this.productions[index]);
                }
            }

            public KeyValuePair<string, INamespaceDeclaration>[] ToArray()
            {
                string[] keys = new string[this.Count];
                this.groups.Keys.CopyTo(keys, 0);
                KeyValuePair<string, INamespaceDeclaration>[] result = new KeyValuePair<string, INamespaceDeclaration>[this.Count];
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckIndexAt(i, keys[i]);
                    result[i] = new KeyValuePair<string, INamespaceDeclaration>(keys[i], this.productions[i]);
                }
                return result;
            }

            #endregion

            #region IEnumerable<KeyValuePair<string,INamespaceDeclaration>> Members

            public IEnumerator<KeyValuePair<string, INamespaceDeclaration>> GetEnumerator()
            {
                string[] keys = new string[this.Count];
                this.groups.Keys.CopyTo(keys, 0);
                KeyValuePair<string, INamespaceDeclaration>[] result = new KeyValuePair<string, INamespaceDeclaration>[this.Count];
                for (int i = 0; i < this.Count; i++)
                {
                    this.CheckIndexAt(i, keys[i]);
                    yield return new KeyValuePair<string, INamespaceDeclaration>(keys[i], this.productions[i]); ;
                }
                yield break;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IControlledStateDictionary Members

            IControlledStateCollection IControlledStateDictionary.Keys
            {
                get { return (IControlledStateCollection)this.Keys; }
            }

            IControlledStateCollection IControlledStateDictionary.Values
            {
                get { return (IControlledStateCollection)this.Values; }
            }

            object IControlledStateDictionary.this[object key]
            {
                get {
                    if (!(key is String))
                        throw new ArgumentException("key must be of type 'System.String'", "key");
                    return this[((string)(key))];
                }
            }

            bool IControlledStateDictionary.ContainsKey(object key)
            {
                if (!(key is String))
                    throw new ArgumentException("key must be of type 'System.String'", "key");
                return this.ContainsKey((string)(key));
            }

            IDictionaryEnumerator IControlledStateDictionary.GetEnumerator()
            {
                return new SimpleDictionaryEnumerator<string, INamespaceDeclaration>(this.GetEnumerator());
            }

            #endregion

            #region IControlledStateCollection Members


            bool IControlledStateCollection.Contains(object item)
            {
                throw new NotImplementedException();
            }

            void IControlledStateCollection.CopyTo(Array array, int arrayIndex)
            {
                ((ICollection)(this)).CopyTo(array, arrayIndex);
            }

            object IControlledStateCollection.this[int index]
            {
                get { return this[index]; }
            }

            #endregion

            #region ICollection Members


            void ICollection.CopyTo(Array array, int arrayIndex)
            {
                this.ToArray().CopyTo(array, arrayIndex);
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return null; }
            }

            #endregion

            private void CheckIndexAt(int index, string key)
            {
                if (this.productions[index] == null)
                    this.productions[index] = new CombinedNamespace(key, owner, this.groups[key]);
            }

            /* *
             * An update method to enable proper shifting of productions should the 
             * list contain names that exist prior to other names in the 
             * series before the update.
             * *
             * Also enables proper udpate of used names in the namespace parent series
             * */
            internal void Added(INamespaceParent[] parents)
            {
                string[] currentNames = this.groups.Keys.ToArray();
                var newGroups = ObtainGroupings(parents);
                string[] newNames = newGroups.Keys.ToArray();
                //For rebuilding the final groups dictionary.
                string[] unionNames = currentNames.Concat(newNames).Distinct().OrderBy(p => p).ToArray();
                var resultSet = new Dictionary<string, INamespaceDeclaration[]>();
                CombinedNamespace[] newProductions = new CombinedNamespace[unionNames.Length];
                foreach (var name in unionNames)
                {
                    int productionIndex = -1;
                    if (currentNames.Contains(name))
                    {
                        resultSet.Add(name, this.groups[name]);
                        productionIndex = currentNames.GetIndexOf(name);
                    }
                    if (newNames.Contains(name))
                        if (!resultSet.ContainsKey(name))
                            resultSet.Add(name, newGroups[name]);
                        else
                            resultSet[name] = resultSet[name].Concat(newGroups[name]).Distinct().ToArray();
                    if (productionIndex != -1 &&
                        this.productions[productionIndex] != null)
                    {
                        int newIndex = unionNames.GetIndexOf(name);
                        newProductions[newIndex] = this.productions[productionIndex];
                        /* *
                         * Update the production so its sub-namespace names and type-name
                         * dictionaries are updated accordingly.
                         * */
                        if (resultSet[name].Length > this.groups[name].Length)
                            newProductions[newIndex].Added(resultSet[name].Except(this.groups[name]).ToArray());
                    }
                }
                this.groups.Clear();
                this.groups = null;
                this.productions = null;
                this.groups = resultSet;
                this.productions = newProductions;
            }

        }
    }
}
