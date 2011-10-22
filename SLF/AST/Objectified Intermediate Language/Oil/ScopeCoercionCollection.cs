using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a <see cref="IScopeCoercion"/> series which 
    /// aid in the  identity resolution (linking) phase of 
    /// compilation.
    /// </summary>
    public class ScopeCoercionCollection :
        ControlledStateCollection<IScopeCoercion>,
        IScopeCoercionCollection
    {
        private int protectionLevel = 0;
        #region IScopeCoercionCollection Members

        /// <summary>
        /// Adds a <see cref="ITypeInclusionScopeCoercion"/> with the
        /// <paramref name="includedType"/> and <paramref name="mergeStaticScope"/>
        /// explicitly given.
        /// </summary>
        /// <param name="includedType">The <see cref="IType"/> which
        /// is included explicitly into the active scope.</param>
        /// <param name="mergeStaticScope">Determines whether the inclusion
        /// should merge the identity of the type, or the static declarations
        /// of the type, into the active scope.</param>
        /// <returns>A <see cref="ITypeInclusionScopeCoercion"/> instance
        /// which denotes the specific type to include, and whether its to
        /// merge the type identity or the static declarations of the type.
        /// </returns>
        public ITypeInclusionScopeCoercion Add(IType includedType, bool mergeStaticScope)
        {
            TypeInclusionScopeCoercion result = null;
            if (!mergeStaticScope)
                result = new TypeInclusionScopeCoercion() { IncludedType = includedType };
            else
                result = new StaticInclusionScopeCoercion() { IncludedType = includedType };
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Adds a <see cref="ITypeInclusionScopeCoercion"/> with the 
        /// <paramref name="includedType"/>.
        /// </summary>
        /// <param name="includedType">The <see cref="IType"/> which
        /// is included explicitly into the active scope.</param>
        /// <param name="alternateName">The actual <see cref="String"/>
        /// name to refer to when performing identity resolution upon symbols
        /// representing a type.</param>
        /// <returns>a <see cref="ITypeInclusionRenameScopeCoercion"/>
        /// instance which denotes the specific type to include and the 
        /// <paramref name="alternateName"/> to refer to it by.</returns>
        public ITypeInclusionRenameScopeCoercion Add(IType includedType, string alternateName)
        {
            var result = new TypeInclusionRenameScopeCoercion() { IncludedType = includedType, NewName = alternateName };
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Adds a <see cref="INamespaceInclusionScopeCoercion"/> with the 
        /// <paramref name="namespaceName"/> identifying the target
        /// declarations to merge into the active scope.
        /// </summary>
        /// <param name="namespaceName">The <see cref="String"/> which represents
        /// the name of the namespace whose declarations should be merged into 
        /// the active scope during identity resolution.</param>
        /// <returns>A <see cref="INamespaceInclusionScopeCoercion"/> which
        /// denotes the details of the scope coercion.</returns>
        public INamespaceInclusionScopeCoercion Add(string namespaceName)
        {
            var result = new NamespaceInclusionScopeCoercion() { Namespace = namespaceName };
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Adds a <see cref="INamespaceInclusionScopeCoercion"/> with the 
        /// <paramref name="namespaceName"/> to merge into the active scope
        /// as the <paramref name="alternateName"/> specifies.
        /// </summary>
        /// <param name="namespaceName">The <see cref="String"/> which represents
        /// the name of the namespace whose which should be alternately addressed
        /// as <paramref name="alternateName"/>.</param>
        /// <param name="alternateName">The <see cref="String"/> new name of the
        /// <paramref name="namespaceName"/>.</param>
        /// <returns>A <see cref="INamespaceInclusionRenameScopeCoercion"/>
        /// which denotes the details of the scope coercion.</returns>
        public INamespaceInclusionRenameScopeCoercion Add(string namespaceName, string alternateName)
        {
            var result = new NamespaceInclusionRenameScopeCoercion() { Namespace = namespaceName, NewName = alternateName };
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Adds a <see cref="INamedInclusionScopeCoercion"/> which details the name
        /// of a namespace or a type whose identity needs resolved prior to further
        /// identity resolution.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the
        /// name to merge into the active scope during identity resolution.</param>
        /// <returns>A <see cref="INamedInclusionScopeCoercion"/> which denotes
        /// the details of the scope coercion.</returns>
        public INamedInclusionScopeCoercion AddName(string name)
        {
            var result = new NamedInclusionScopeCoercion() { IncludedName = name };
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Adds a <see cref="INamedInclusionScopeCoercion"/> which details the 
        /// <paramref name="name"/> of a namespace or a type whose identity needs 
        /// resolved prior to further identity resolution to be referred to by
        /// an <paramref name="alternateName"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the 
        /// type or namespace to refer to by an <paramref name="alternateName"/>.</param>
        /// <param name="alternateName">The <see cref="String"/> value representing
        /// the actual name the type or namespace is referred to as during
        /// identity resolution.</param>
        /// <returns>A <see cref="INamedInclusionScopeCoercion"/> which denotes
        /// the details of the scope coercion.</returns>
        public INamedInclusionRenameScopeCoercion AddName(string name, string alternateName)
        {
            var result = new NamedInclusionRenameScopeCoercion() { IncludedName = name, NewName = alternateName };
            this.baseList.Add(result);
            return result;
        }

        public IStaticInclusionScopeCoercion AddStaticName(string name)
        {
            var result = new StaticInclusionScopeCoercion() { IncludedType = name.GetSymbolType() };
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Adds a series of <see cref="INamedInclusionScopeCoercion"/> elements
        /// to the <see cref="ScopeCoercionCollection"/> with the <paramref name="names"/>
        /// provided.
        /// </summary>
        /// <param name="names">The series of <see cref="String"/> values which
        /// represent the names to include within the scope to aid in identity
        /// resolution.</param>
        /// <returns>A <see cref="INamedInclusionScopeCoercion"/>.</returns>
        public INamedInclusionScopeCoercion[] AddNames(params string[] names)
        {
            if (names == null)
                throw new ArgumentNullException("names");
            names = (from name in names
                     orderby name
                     orderby name.Length < 6 ? 2 : name.Substring(0, 6) == "System" ? 0 : name.Length < 9 ? 2 : name.Substring(0, 9) == "Microsoft" ? 1 : 2
                     select name).ToArray();
            INamedInclusionScopeCoercion[] result = new INamedInclusionScopeCoercion[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == null)
                    throw new ArgumentNullException("names");
                var currentElement = new NamedInclusionScopeCoercion() { IncludedName = names[i] };
                result[i] = currentElement;
                this.baseList.Add(currentElement);
            }

            return result;
        }

        /// <summary>
        /// Removes a <see cref="String"/> based namespace inclusion scope
        /// coercion.
        /// </summary>
        /// <param name="namespaceName">The name of the namespace
        /// which was included by the <see cref="INamespaceInclusionScopeCoercion"/>.</param>
        /// <returns>true if the namespace name was found and removed; false, otherwise.</returns>
        public bool Remove(string namespaceName)
        {
            INamespaceInclusionScopeCoercion foundItem = null;
            foreach (var element in this)
                if ((element is INamespaceInclusionScopeCoercion) &&
                    (foundItem = (INamespaceInclusionScopeCoercion)element).Namespace == namespaceName)
                    break;
                else
                    foundItem = null;
            if (foundItem == null)
                return false;
            this.baseList.Remove(foundItem);
            return true;
        }

        /// <summary>
        /// Removes a <see cref="String"/> based <see cref="INamedInclusionScopeCoercion"/>
        /// based off of the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value associated to the 
        /// <see cref="INamedInclusionScopeCoercion"/>.</param>
        /// <returns>returns true if a named inclusion was found and removed; false, otherwise.</returns>
        public bool RemoveName(string name)
        {
            INamedInclusionScopeCoercion foundItem = null;
            foreach (var element in this)
                if ((element is INamedInclusionScopeCoercion) &&
                    (foundItem = (INamedInclusionScopeCoercion)element).IncludedName == name)
                    break;
                else
                    foundItem = null;
            if (foundItem == null)
                return false;
            this.baseList.Remove(foundItem);
            return true;
        }

        /// <summary>
        /// Removes an <paramref name="includedType"/> from the <see cref="ScopeCoercionCollection"/>
        /// provided it is present.
        /// </summary>
        /// <param name="includedType">The <see cref="IType"/> to remove.</param>
        /// <returns>true if the <paramref name="includedType"/> was found within the 
        /// <see cref="IScopeCoercionCollection"/>.</returns>
        public bool Remove(IType includedType)
        {
            ITypeInclusionScopeCoercion foundItem = null;
            foreach (var element in this)
                if ((element is ITypeInclusionScopeCoercion) &&
                    (foundItem = (ITypeInclusionScopeCoercion)element).IncludedType == includedType)
                    break;
                else
                    foundItem = null;
            if (foundItem == null)
                return false;
            this.baseList.Remove(foundItem);
            return true;
        }

        /// <summary>
        /// Instructs the <see cref="ScopeCoercionCollection"/> to enter a 
        /// protected state wherein no changes may occur to the elements within.
        /// </summary>
        /// <remarks>Used to guarantee identity resolution consistency during the time the
        /// type is within scope.</remarks>
        public void EnterProtectedState()
        {
            this.protectionLevel++;
        }

        /// <summary>
        /// Instructs the <see cref="ScopeCoercionCollection"/> to exit
        /// a previously entered protected state, allowing entry changes to occur.
        /// </summary>
        public void ExitProtectedState()
        {
            if (this.InProtectedState)
                this.protectionLevel--;
        }

        /// <summary>
        /// Returns whether the <see cref="ScopeCoercionCollection"/> is within a protected state.
        /// </summary>
        public bool InProtectedState
        {
            get
            {
                return this.protectionLevel > 0;
            }
        }
        #endregion

    }
}
