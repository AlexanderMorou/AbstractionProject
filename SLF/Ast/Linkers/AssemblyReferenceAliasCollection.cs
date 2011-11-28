using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public class AssemblyReferenceAliasCollection :
        ControlledCollection<string>,
        IAssemblyReferenceAliasCollection
    {

        #region IAssemblyReferenceAliasCollection Members

        /// <summary>
        /// Adds an <paramref name="alias"/> to the <see cref="IAssemblyReferenceAliasCollection"/>.
        /// </summary>
        /// <param name="alias">The <see cref="String"/> value representing the
        /// alias to inject.</param>
        public void Add(string alias)
        {
            base.AddImpl(alias);
        }

        /// <summary>
        /// Removes a <see cref="String"/> <paramref name="alias"/> from the 
        /// <see cref="IAssemblyReferenceAliasCollection"/>.
        /// </summary>
        /// <param name="alias">The <see cref="String"/> value representing the
        /// alias to remove.</param>
        /// <returns>whether the <paramref name="alias"/>
        /// was removed.</returns>
        public bool Remove(string alias)
        {
            bool result = base.Contains(alias);
            if (result)
                base.baseList.Remove(alias);
            return result;
        }

        /// <summary>
        /// Adds a series of <see cref="String"/> <paramref name="aliases"/> 
        /// to the <see cref="IAssemblyReferenceAliasCollection"/>
        /// </summary>
        /// <param name="aliases">A series of <see cref="String"/>
        /// values representing the <paramref name="aliases"/>
        /// to insert.</param>
        public new void AddRange(params string[] aliases)
        {
            base.AddRange(aliases);
        }

        /// <summary>
        /// Removes a series of <paramref name="aliases"/> from the 
        /// <see cref="IAssemblyReferenceAliasCollection"/>.
        /// </summary>
        /// <param name="aliases">The series of <see cref="String"/>
        /// <paramref name="aliases"/> to remove from the 
        /// <see cref="IAssemblyReferenceAliasCollection"/>.
        /// </param>
        /// <returns>A series of <see cref="Boolean"/> values
        /// which relate to which <paramref name="aliases"/>
        /// were removed.</returns>
        public bool[] RemoveRange(params string[] aliases)
        {
            if (aliases == null)
                throw new ArgumentNullException("aliases");
            if (aliases.Length == 0)
                return new bool[0];
            bool[] result = new bool[aliases.Length];
            for (int i = 0; i < result.Length; i++)
            {
                var current = aliases[i];
                if (this.Contains(current))
                {
                    result[i] = true;
                    base.baseList.Remove(current);
                    this.OnRemove(current);
                }
            }
            return result;
        }

        /// <summary>
        /// Clears the <see cref="IAssemblyReferenceAliasCollection"/>.
        /// </summary>
        public void Clear()
        {
            base.baseList.Clear();
        }


        /// <summary>
        /// Occurs when an alias is added.
        /// </summary>
        public event EventHandler<EventArgsR1<string>> AliasAdded;

        /// <summary>
        /// Occurs when an alias is removed.
        /// </summary>
        public event EventHandler<EventArgsR1<string>> AliasRemoved;

        #endregion

        private void OnAdd(string alias)
        {
            this.AliasAdded(this, new EventArgsR1<string>(alias));
        }

        private void OnRemove(string alias)
        {
            this.AliasRemoved(this, new EventArgsR1<string>(alias));
        }

    }
}
