using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private IAssemblyReferenceAliasAggregate rootAggregate;
        private int protectionLevel = 0;
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
            if (this.InProtectedState)
                throw new InvalidOperationException(Resources.AssemblyReferencesCollection_ProtectedError);
            IAssemblyReference result;
            if (this.TryGetValue(assembly, out result))
                return result;
            result = new AssemblyReference(assembly, DefaultAlias);
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
            if (this.InProtectedState)
                throw new InvalidOperationException(Resources.AssemblyReferencesCollection_ProtectedError);
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
            if (this.InProtectedState)
                throw new InvalidOperationException(Resources.AssemblyReferencesCollection_ProtectedError);
            
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
        public IAssemblyReference[] AddRange(params IAssembly[] assemblies)
        {
            if (this.InProtectedState)
                throw new InvalidOperationException(Resources.AssemblyReferencesCollection_ProtectedError);
            if (assemblies == null)
                throw new ArgumentNullException("assemblies");
            IAssemblyReference[] temp = new IAssemblyReference[assemblies.Length];
            int unused = 0;
            for (int i = 0; i < assemblies.Length; i++)
            {
                var current = assemblies[i];
                IAssemblyReference dummy;
                if (this.TryGetValue(current, out dummy))
                    continue;
                temp[i - unused] = new AssemblyReference(current, DefaultAlias);
            }
            if (unused == 0)
            {
                base.AddRange(temp);
                return temp;
            }
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
            if (this.InProtectedState)
                throw new InvalidOperationException(Resources.AssemblyReferencesCollection_ProtectedError);
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
            if (this.InProtectedState)
                throw new InvalidOperationException(Resources.AssemblyReferencesCollection_ProtectedError);
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
            this.EnterProtectedState();
            var result = new AssemblyReferenceAliasAggregate(this);
            return result;
        }

        #region IProtectableComponent Members

        /// <summary>
        /// Instructs the <see cref="AssemblyReferenceCollection"/> to enter a 
        /// protected state wherein no changes may occur to the object state.
        /// </summary>
        /// <remarks>Used to guarantee changeless state during an identity 
        /// sensitive operation.</remarks>
        public void EnterProtectedState()
        {
            this.protectionLevel++;
        }

        /// <summary>
        /// Instructs the <see cref="AssemblyReferenceCollection"/> to exit
        /// a previously entered protected state, allowing state changes to occur.
        /// </summary>
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

        /// <summary>
        /// Returns whether the <see cref="AssemblyReferenceCollection"/> is
        /// within a protected state.
        /// </summary>
        public bool InProtectedState
        {
            get { return this.protectionLevel > 0; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.rootAggregate != null)
                if (!this.rootAggregate.IsDisposed)
                {
                    this.rootAggregate.Dispose();
                    this.rootAggregate = null;
                }
                else
                    this.rootAggregate = null;
            this.RemoveRange((from @ref in this
                              select @ref.Reference).ToArray());
        }

        #endregion
    }
}
