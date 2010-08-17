using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a data structure for defining the specifics
    /// of a member declared on an anonymous type.
    /// </summary>
    [DebuggerVisualizer("{StringForm}")]
    public struct AnonymousTypeMember :
        IEquatable<AnonymousTypeMember>
    {
        private bool IndexSet { get; set; }

        /// <summary>
        /// Creates a new <see cref="AnonymousTypeMember"/>
        /// with the <paramref name="name"/> and whether
        /// it is <paramref name="immutable"/>.
        /// </summary>
        /// <param name="name">The name of the new
        /// <see cref="AnonymousTypeMember"/>.</param>
        /// <param name="immutable">Whether the 
        /// <see cref="AnonymousTypeMember"/> is read-only.</param>
        public AnonymousTypeMember(string name, bool immutable)
            : this() /* initialize property backup fields */
        {
            this.Immutable = immutable;
            this.Name = name;
        }

        /// <summary>
        /// Creates a new <see cref="AnonymousTypeMember"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// which represents the name of the property 
        /// of the anonymous type member.</param>
        public AnonymousTypeMember(string name)
            : this(name, true)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AnonymousTypeMember"/>
        /// with the <paramref name="original"/> and 
        /// <paramref name="index"/> provided.
        /// </summary>
        /// <param name="original">The original <see cref="AnonymousTypeMember"/>
        /// which needs indexed.</param>
        /// <param name="index">The <see cref="Int32"/>
        /// which represents the index of the member within the anonymous type
        /// to be generated.</param>
        internal AnonymousTypeMember(AnonymousTypeMember original, int index)
            : this(original.Name, original.Immutable)
        {
            this.Position = index;
            IndexSet = true;
        }

        #region IEquatable<AnonymousTypeMember> Members

        /// <summary>
        /// Determines whether the <paramref name="other"/>
        /// <see cref="AnonymousTypeMember"/> is equal to the
        /// current <see cref="AnonymousTypeMember"/>
        /// </summary>
        /// <param name="other">The <see cref="AnonymousTypeMember"/>
        /// to check equality against.</param>
        /// <returns>true if the <paramref name="other"/>
        /// <see cref="AnonymousTypeMember"/> is equivalent to the
        /// current <see cref="AnonymousTypeMember"/>.</returns>
        public bool Equals(AnonymousTypeMember other)
        {
            return (other.Immutable == this.Immutable &&
                    this.Name.Equals(other.Name));
        }
        
        #endregion

        /// <summary>
        /// Returns the hash code for the <see cref="AnonymousTypeMember"/>.
        /// </summary>
        /// <returns>A <see cref="Int32"/> that is the hash code for the
        /// <see cref="AnonymousTypeMember"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                /* *
                 * There's no magic to programming: Just logic.
                 * The logic here?  To obfuscate.
                 * After all, Local consts are replaced with literals
                 * on compilation and these names and values, 
                 * for the most part, disappear.
                 * *
                 * As for why I'm obfuscating such a simple thing:
                 *      No idea.
                 * */
                const int magic1 = -12405032,
                          magic2 = 20021453,
                          magic3 = 8675309,
                          magic4 = magic2 % magic3,
                          magic5 = magic1 * magic4;
                return magic5 +
                    ((this.Name.GetHashCode() % magic3 + this.Immutable.GetHashCode()) * magic4);
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AnonymousTypeMember))
                return false;
            return this.Equals((AnonymousTypeMember)obj);
        }

        /// <summary>
        /// Returns whether the <see cref="AnonymousTypeMember"/>
        /// is immutable.
        /// </summary>
        public bool Immutable { get; private set; }

        /// <summary>
        /// Returns the name of the <see cref="AnonymousTypeMember"/>.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Implicitly converts an a string 
        /// into a <see cref="AnonymousTypeMember"/>.
        /// </summary>
        /// <param name="value">The name of the 
        /// <see cref="AnonymousTypeMember"/>.</param>
        /// <returns>A new <see cref="AnonymousTypeMember"/>
        /// which is <see cref="Immutable"/>.</returns>
        public static implicit operator AnonymousTypeMember(string value)
        {
            return new AnonymousTypeMember(value);
        }

        /// <summary>
        /// Determines whether the <paramref name="left"/> <see cref="AnonymousTypeMember"/>
        /// is equal to the <paramref name="right"/> <see cref="AnonymousTypeMember"/>.
        /// </summary>
        /// <param name="left">The <see cref="AnonymousTypeMember"/> to check against
        /// the <paramref name="right"/> side.</param>
        /// <param name="right">The <see cref="AnonymousTypeMember"/> to be checked
        /// against the <paramref name="left"/> side.</param>
        /// <returns>True if <paramref name="left"/> equals <paramref name="right"/>; false otherwise.</returns>
        public static bool operator ==(AnonymousTypeMember left, AnonymousTypeMember right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the <paramref name="left"/> <see cref="AnonymousTypeMember"/>
        /// is not equal to the <paramref name="right"/> <see cref="AnonymousTypeMember"/>.
        /// </summary>
        /// <param name="left">The <see cref="AnonymousTypeMember"/> to check against
        /// the <paramref name="right"/> side.</param>
        /// <param name="right">The <see cref="AnonymousTypeMember"/> to be checked
        /// against the <paramref name="left"/> side.</param>
        /// <returns>False if <paramref name="left"/> equals <paramref name="right"/>; true otherwise.</returns>
        public static bool operator !=(AnonymousTypeMember left, AnonymousTypeMember right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> value which
        /// represents what the index of the 
        /// <see cref="AnonymousTypeMember"/> is.
        /// </summary>
        /// <returns>Zero if the <see cref="AnonymousTypeMember"/>
        /// has not been assigned to an <see cref="IAnonymousType"/>.</returns>
        public int Position { get; private set; }

        private string StringForm
        {
            get
            {
                return this.ToString();
            }
        }

        /// <summary>
        /// Returns the <see cref="String"/> which represents
        /// the current <see cref="AnonymousTypeMember"/>.
        /// </summary>
        /// <returns>A <see cref="String"/>
        /// which represents the current <see cref="AnonymousTypeMember"/>.</returns>
        public override string ToString()
        {
            if (this.Immutable)
                if (IndexSet)
                    return string.Format("[{0}] {1} (read-only)", this.Position, this.Name);
                else
                    return string.Format("{0} (read-only)", this.Name);
            else if (IndexSet)
                return string.Format("[{0}] {1}", this.Position, this.Name);
            return this.Name;
        }
    }
}
