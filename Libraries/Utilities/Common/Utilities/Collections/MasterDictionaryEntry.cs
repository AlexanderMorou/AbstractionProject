using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Provides a simple structure for linking a subordinate 
    /// dictionary's value back to the subordinate.
    /// </summary>
    /// <typeparam name="TEntry">
    /// The root type of value used in the master 
    /// dictionary.</typeparam>
    public struct MasterDictionaryEntry<TEntry> :
        IEquatable<MasterDictionaryEntry<TEntry>>
        where TEntry :
            class
    {
        /// <summary>
        /// Data member for <see cref="Subordinate"/>.
        /// </summary>
        private ISubordinateDictionary subordinate;
        /// <summary>
        /// Data member for <see cref="Entry"/>.
        /// </summary>
        private TEntry value;

        /// <summary>
        /// Creates a new <see cref="MasterDictionaryEntry{TEntry}"/> with the
        /// <paramref name="subordinate"/> and <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="subordinate">
        /// The <see cref="ISubordinateDictionary"/> 
        /// from which the <paramref name="value"/> is derived.</param>
        /// <param name="value">The <typeparamref name="TEntry"/>
        /// entered into the master dictionary from the 
        /// <paramref name="subordinate"/>.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when 
        /// <paramref name="subordinate"/> is null.</exception>
        public MasterDictionaryEntry(ISubordinateDictionary subordinate, TEntry value)
        {
            if (subordinate == null)
                throw new ArgumentNullException("subordinate");
            this.subordinate = subordinate;
            this.value = value;
        }
        /// <summary>
        /// Returns the <see cref="ISubordinateDictionary"/> 
        /// in which the original entry resides.
        /// </summary>
        public ISubordinateDictionary Subordinate
        {
            get
            {
                return this.subordinate;
            }
        }

        /// <summary>
        /// Returns the <typeparamref name="TEntry"/> of the
        /// <see cref="MasterDictionaryEntry{TEntry}"/>.
        /// </summary>
        /// <remarks>
        /// Name changed from 'Value' and 'TValue'
        /// to '<see cref="Entry"/>' and '<typeparamref name="TEntry"/> due 
        /// to structural similarities between
        /// <see cref="KeyValuePair{TKey, TValue}"/> and 
        /// <see cref="MasterDictionaryEntry{TEntry}"/>.</remarks>
        public TEntry Entry
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Converts the current <see cref="MasterDictionaryEntry{TEntry}"/> to a <see cref="String"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> representing the current 
        /// <see cref="MasterDictionaryEntry{TEntry}"/>.</returns>
        public override string ToString()
        {
            return this.Entry.ToString();
        }

        public override int GetHashCode()
        {
            if (this.Entry == null)
                return 0;
            return this.Entry.GetHashCode();
        }

        public static bool operator ==(MasterDictionaryEntry<TEntry> left, MasterDictionaryEntry<TEntry> right)
        {
            if (object.ReferenceEquals(left.value, right.value) && left.subordinate == right.subordinate)
                return true;
            return false;
        }

        public static bool operator !=(MasterDictionaryEntry<TEntry> left, MasterDictionaryEntry<TEntry> right)
        {
            if (object.ReferenceEquals(left.value, right.value) && left.subordinate == right.subordinate)
                return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MasterDictionaryEntry<TEntry>))
                return false;
            return this.Equals((MasterDictionaryEntry<TEntry>)obj);
        }

        #region IEquatable<MasterDictionaryEntry<TEntry>> Members

        public bool Equals(MasterDictionaryEntry<TEntry> other)
        {
            if (other.Subordinate == this.Subordinate)
                return other.Entry == this.Entry;
            return false;
        }

        #endregion
    }
}
