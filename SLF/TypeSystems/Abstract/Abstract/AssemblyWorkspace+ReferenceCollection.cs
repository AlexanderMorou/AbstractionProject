using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Utilities.Collections;
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
        /// Provides a series of referenced assemblies for the 
        /// assembly workspace.
        /// </summary>
        public sealed class ReferenceCollection :
            ControlledStateCollection<IAssembly>,
            IAssemblyReferenceCollection
        {
            /// <summary>
            /// Data member relating back to the <see cref="IAssemblyWorkspace"/>
            /// which owns the current <see cref="ReferenceCollection"/>.
            /// </summary>
            AssemblyWorkspace workspace;

            /// <summary>
            /// Creates a new <see cref="ReferenceCollection"/> with the
            /// <paramref name="workspace"/> provided.
            /// </summary>
            /// <param name="workspace">The <see cref="AssemblyWorkspace"/> 
            /// which owns the <see cref="ReferenceCollection"/>.</param>
            internal ReferenceCollection(AssemblyWorkspace workspace)
            {
                this.workspace = workspace;
            }
            #region IAssemblyReferenceCollection Members

            /// <summary>
            /// Adds an <paramref name="assembly"/> to the <see cref="ReferenceCollection"/>.
            /// </summary>
            /// <param name="assembly">The <see cref="IAssembly"/> to add to the
            /// <see cref="ReferenceCollection"/>.</param>
            public void Add(IAssembly assembly)
            {
                if (this.Contains(assembly))
                    return;
                UpdateNamespaces(new IAssembly[1] { assembly });
                base.baseCollection.Add(assembly);
            }

            /// <summary>
            /// Adds a series of <paramref name="assemblies"/> to the
            /// <see cref="ReferenceCollection"/>.
            /// </summary>
            /// <param name="assemblies">The <see cref="IAssembly"/> array to add to the 
            /// <see cref="ReferenceCollection"/>.</param>
            public void AddRange(params IAssembly[] assemblies)
            {
                foreach (var assembly in assemblies)
                    if (assembly == this.workspace.Assembly ||
                        this.Contains(assembly))
                        continue;
                    else
                        base.baseCollection.Add(assembly);
            }

            /// <summary>
            /// Removes an <paramref name="assembly"/> from the 
            /// <see cref="ReferenceCollection"/>.
            /// </summary>
            /// <param name="assembly">The <see cref="IAssembly"/> to remove from the
            /// <see cref="ReferenceCollection"/>.</param>
            public void Remove(IAssembly assembly)
            {
                if (assembly == this.workspace.Assembly)
                    return;
                base.baseCollection.Remove(assembly);
            }

            /// <summary>
            /// Removes a series of <paramref name="assemblies"/>
            /// from the <see cref="ReferenceCollection"/>.
            /// </summary>
            /// <param name="assemblies">The series of <see cref="IAssembly"/> instances
            /// to remove from the <see cref="ReferenceCollection"/>.</param>
            public void RemoveRange(IAssembly[] assemblies)
            {
                foreach (var assembly in assemblies)
                    if (assembly == this.workspace.Assembly)
                        continue;
                    else
                        base.baseCollection.Remove(assembly);
            }

            #endregion

            public override IEnumerator<IAssembly> GetEnumerator()
            {
                yield return this.workspace.Assembly;
                foreach (var assembly in base.baseCollection)
                    yield return assembly;
                yield break;
            }

            public override IAssembly[] ToArray()
            {
                IAssembly[] result = new IAssembly[this.Count];
                result[0] = this.workspace.Assembly;
                int index = 0;
                foreach (var item in this.baseCollection)
                    result[++index] = item;
                return result;
            }

            public override int Count
            {
                get
                {
                    return base.Count + 1;
                }
            }

            public override bool Contains(IAssembly item)
            {
                return
                    base.Contains(item) ||
                    item == this.workspace.Assembly;
            }

            public override IAssembly this[int index]
            {
                get
                {

                    if (index == 0)
                        return this.workspace.Assembly;
                    else if (index >= this.Count || index < 0)
                        throw new ArgumentOutOfRangeException("index");
                    else
                        return base[index - 1];
                }
            }

            /* *
             * Instructs the namespaces of the assembly that the combined series it's using 
             * unify the namespaces and other symbols has changed.
             * */
            private void UpdateNamespaces(IAssembly[] set)
            {
                if (this.workspace == null)
                    return;
                //No need to update the namespaces if they aren't created yet.
                if (this.workspace.namespaces == null)
                    return;
                this.workspace.namespaces.Added((INamespaceParent[])set);
            }

        }
    }
}