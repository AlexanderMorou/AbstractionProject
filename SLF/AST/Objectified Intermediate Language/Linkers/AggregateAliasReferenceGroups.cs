using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public sealed class AggregateAliasReferenceGroups :
        ControlledStateDictionary<string, IAssemblyReferenceIdentityAggregate>,
        IAggregateAliasReferenceGroups
    {
        private AssemblyReferenceCollection parent;
        public AggregateAliasReferenceGroups(AssemblyReferenceCollection parent)
        {
            var aliasGroupings = parent.ReferencesByAlias.ToDictionary(key => key.Key, value => value.ToArray());
            List<Tuple<List<string>, IAssemblyReference[]>> aliasEquivalents = new List<Tuple<List<string>, IAssemblyReference[]>>();
            /* *
             * To avoid type symbol table from reproducing entries, 
             * aggregate aliases together whose references are identical
             * in count and identity.
             * *
             * While not likely to happen often, it prevents unnecessary
             * resolution tables from being generated.
             * */
            foreach (var kvp in aliasGroupings)
            {
                var alias = kvp.Key;
                var references = kvp.Value;
                Tuple<List<string>, IAssemblyReference[]> equivalentMatch = null;

                foreach (var equivalent in aliasEquivalents)
                {
                    bool currentIsEquivalent = true;
                    var equivalentReferences = equivalent.Item2;
                    if (references.Length != equivalentReferences.Length)
                        continue;
                    /* *
                     * Elements selected through LINQ should be in the same 
                     * order; however, the Slf was written by someone other
                     * than the person who wrote the LINQ methods.
                     * */
                    for (int i = 0, length = references.Length; i < length; i++)
                    {
                        bool currentReferenceFound = false;
                        for (int j = 0; j < length; j++)
                        {
                            if (references[i] == equivalentReferences[j])
                            {
                                currentReferenceFound = true;
                                break;
                            }
                        }
                        if (!currentReferenceFound)
                        {
                            currentIsEquivalent = false;
                            break;
                        }
                    }
                    if (currentIsEquivalent)
                        break;
                }
                if (equivalentMatch == null)
                    aliasEquivalents.Add(new Tuple<List<string>, IAssemblyReference[]>(new List<string>() { { alias } }, references));
                else
                    equivalentMatch.Item1.Add(alias);
            }
            /* *
             * Now that identity equivalent aliases have been grouped, construct
             * an identity aggregate for the root-level namespace for the
             * references contained within the aliases groups.
             * */

            this.parent = parent;
        }

        #region IAggregateReferenceAliasSets Members

        public bool IsDisposed
        {
            get { return this.parent == null; }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes the <see cref="AggregateAliasReferenceGroups"/>.
        /// </summary>
        public void Dispose()
        {
            var parent = this.parent;
            foreach (var asliasedIdentityAggregate in this.Values)
                asliasedIdentityAggregate.Dispose();
            base._Clear();
            this.parent = null;
        }

        #endregion

        public void AddReferenceToAlias(string alias, IAssemblyReference reference)
        {
            IAssemblyReferenceIdentityAggregate element;
            if (!this.TryGetValue(alias, out element))
            {
            }
            
        }
    }
}
