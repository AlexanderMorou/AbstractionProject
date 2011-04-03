using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public class AssemblyReferenceCollection :
        ControlledStateCollection<IAssemblyReference>,
        IAssemblyReferenceCollection
    {
        private IAssemblyReferenceAliasAggregate rootAggregate;
        private int protectionLevel = 0;
        #region IAssemblyReferenceCollection Members

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
            result = new AssemblyReference(assembly, "global");
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
            IAssemblyReference result;
            if (this.TryGetValue(assembly, out result))
            {
                var newAliases = aliases.Except(result.Aliases);
                foreach (var alias in newAliases)
                    result.Aliases.Add(alias);
                return result;
            }
            if (!aliases.Contains("global"))
                aliases = aliases.Add("global");
            result = new AssemblyReference(assembly, aliases);
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
            foreach (var element in from reference in references
                                    join myReference in this on reference.Reference equals myReference.Reference into intersection
                                    where intersection.Count() == 0
                                    select reference)
                this.baseList.Add(element);
        }

        /// <summary>
        /// Adds a series of <paramref name="assemblies"/> to the
        /// <see cref="IAssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assemblies">The <see cref="IAssembly"/> array to add to the 
        /// <see cref="AssemblyReferenceCollection"/>.</param>
        public IAssemblyReference[] AddRange(params IAssembly[] assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException("assemblies");
            IAssemblyReference[] result = new IAssemblyReference[assemblies.Length];
            for (int i = 0; i < assemblies.Length; i++)
                result[i] = new AssemblyReference(assemblies[i], "global");
            base.AddRange(result);
            return result;
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
                                       join reference in this on assembly equals reference.Reference
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


        public IAssemblyReferenceAliasAggregate GetRootNamespaceAggregate()
        {
            throw new NotImplementedException();
        }

        #region IProtectableComponent Members

        public void EnterProtectedState()
        {
            this.protectionLevel++;
        }

        public void ExitProtectedState()
        {
            if (this.InProtectedState)
            {
                if (this.protectionLevel == 1 && this.rootAggregate != null)
                {
                    if (this.rootAggregate.IsDisposed)
                        this.rootAggregate = null;
                    else
                        throw new InvalidOperationException("Cannot make the assembly reference collection malleable until the root namespace aggregate is disposed.");
                }
                this.protectionLevel--;
            }
        }

        public bool InProtectedState
        {
            get { return this.protectionLevel > 0; }
        }

        #endregion
    }
}
