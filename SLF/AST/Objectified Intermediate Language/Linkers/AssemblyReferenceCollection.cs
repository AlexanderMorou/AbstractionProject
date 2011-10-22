using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public sealed class AssemblyReferenceCollection :
        ControlledStateCollection<IAssemblyReference>,
        IAssemblyReferenceCollection,
        IDisposable
    {
        /// <summary>
        /// The default alias for new assembly references.
        /// </summary>
        public static readonly string DefaultAlias = "global";
        #region IAssemblyReferenceCollection Members

        public IEnumerable<IAssembly> ReferencedAssemblies
        {
            get { 
                return from @ref in this
                       select @ref.Reference;
            }
        }

        public IEnumerable<string> Aliases
        {
            get
            {
                return (from @ref in this
                        from alias in @ref.Aliases
                        select alias).Distinct();
            }
        }

        public IEnumerable<IGrouping<string, IAssemblyReference>> ReferencesByAlias
        {
            get
            {
                return from alias in this.Aliases
                       from reference in this
                       where reference.Aliases.Contains(alias)
                       group reference by alias;
            }
        }

        /// <summary>
        /// Adds an <paramref name="assembly"/> to the <see cref="AssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="IAssembly"/> to add to the
        /// <see cref="IAssemblyReferenceCollection"/>.</param>
        public IAssemblyReference Add(IAssembly assembly)
        {            
            IAssemblyReference result;
            if (this.TryGetValue(assembly, out result))
                return result;
            result = new AssemblyReference(assembly, this, DefaultAlias);
            base.AddImpl(result);
            return result;
        }

        /// <summary>
        /// Adds an <paramref name="assembly"/> to the <see cref="AssemblyReferenceCollection"/>
        /// with the <paramref name="aliases"/> provided.
        /// </summary>
        /// <param name="aliases">The series of <see cref="String"/> values used to alias the <paramref name="assembly"/>
        /// provided.</param>
        /// <param name="assembly">The <see cref="IAssembly"/> to add to the
        /// <see cref="IAssemblyReferenceCollection"/>.</param>
        public IAssemblyReference Add(IAssembly assembly, params string[] aliases)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            if (aliases == null)
                throw new ArgumentNullException("aliases");
            IAssemblyReference result;
            if (this.TryGetValue(assembly, out result))
            {
                var newAliases = aliases.Except(result.Aliases);
                foreach (var alias in newAliases)
                    result.Aliases.Add(alias);
                return result;
            }
            result = new AssemblyReference(assembly, this, aliases);
            base.AddImpl(result);
            return result;
        }

        /// <summary>
        /// Adds a series of <see cref="IAssemblyReference"/> instances
        /// to the <see cref="AssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="references">The <see cref="IEnumerable{T}"/>
        /// which contains the assembly references to add.</param>
        public void AddRange(IEnumerable<IAssemblyReference> references)
        {
            
            foreach (var existingReference in
                from reference in references
                where this.Any(myReference=>myReference.Reference == reference.Reference)
                select reference)
            {
                var currentReference = this[existingReference.Reference];
                foreach (var alias in existingReference.Aliases)
                    if (!currentReference.Aliases.Contains(alias))
                        currentReference.Aliases.Add(alias);
            }
            foreach (var element in
                from reference in references
                where !this.Any(myReference => myReference.Reference == reference.Reference)
                select reference)
                this.baseList.Add(element);
        }

        /// <summary>
        /// Adds a series of <paramref name="assemblies"/> to the
        /// <see cref="IAssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assemblies">The <see cref="IAssembly"/> array to add to the 
        /// <see cref="AssemblyReferenceCollection"/>.</param>
        /// <remarks>If an element of <paramref name="assemblies"/>
        /// is an existing reference, it will not be added in the result
        /// reference set.</remarks>
        public IAssemblyReference[] AddRange(params IAssembly[] assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException("assemblies");
            IAssemblyReference[] temp = new IAssemblyReference[assemblies.Length];
            int unused = 0;
            /* *
             * Preemptive scan to determine which elements need added and
             * which can be ignored.
             * */
            for (int i = 0; i < assemblies.Length; i++)
            {
                var current = assemblies[i];
                IAssemblyReference dummy;
                if (this.TryGetValue(current, out dummy))
                {
                    unused++;
                    continue;
                }
                var element = temp[i - unused] = new AssemblyReference(current, this, DefaultAlias);
                element.Aliases.Add(DefaultAlias);
            }
            /* *
             * If every element is new, just add it and return it; otherwise,
             * add a subset: all of the elements minus those not used.
             * If none of them were added, return nothing, to avoid a 
             * pointless copy from the array of null references.
             * */
            if (unused == 0)
            {
                base.AddRange(temp);
                return temp;
            }
            else if (unused == assemblies.Length)
                return new IAssemblyReference[0];
            else
            {
                var result = new IAssemblyReference[assemblies.Length - unused];
                Array.Copy(temp, result, result.Length);
                base.AddRange(result);
                return result;
            }
        }


        /// <summary>
        /// Removes an <paramref name="assembly"/> from the 
        /// <see cref="IAssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="IAssembly"/> to remove from the
        /// <see cref="IAssemblyReferenceCollection"/>.</param>
        public void Remove(IAssembly assembly)
        {
            foreach (var reference in this)
                if (reference.Reference == assembly)
                {
                    base.baseList.Remove(reference);
                    break;
                }
        }

        /// <summary>
        /// Removes a series of <paramref name="assemblies"/>
        /// from the <see cref="AssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assemblies">The series of <see cref="IAssembly"/> instances
        /// to remove from the <see cref="IAssemblyReferenceCollection"/>.</param>
        public void RemoveRange(IAssembly[] assemblies)
        {
            foreach (var reference in (from assembly in assemblies
                                       join reference in this
                                            on assembly equals reference.Reference
                                       select reference).Distinct().ToArray())
                baseList.Remove(reference);
        }

        public IAssemblyReference this[IAssembly target]
        {
            get
            {
                return (from r in this
                        where r.Reference == target
                        select r).FirstOrDefault();
            }
        }

        /// <summary>
        /// Attempts to obtain a specific <paramref name="target"/> assembly's
        /// <see cref="IAssemblyReference"/>.
        /// </summary>
        /// <param name="target">The target <see cref="IAssembly"/>
        /// to attempt to retrieve the reference of.</param>
        /// <param name="value">The <see cref="IAssemblyReference"/> which denotes the 
        /// assembly and its respective aliases.</param>
        /// <returns>true, if the <paramref name="target"/> <see cref="IAssembly"/>
        /// has an associated <see cref="IAssemblyReference"/>; false, otherwise.</returns>
        public bool TryGetValue(IAssembly target, out IAssemblyReference value)
        {
            var result = this[target];
            value = result;
            return result != null;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.RemoveRange((from @ref in this
                              select @ref.Reference).ToArray());
        }

        #endregion

        internal void ReferenceAliasAdded(AssemblyReference reference, string alias)
        {
        }
        internal void ReferenceAliasRemoved(AssemblyReference reference, string alias)
        {
        }
    }
}
