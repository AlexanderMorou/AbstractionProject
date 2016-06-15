using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a base <see cref="ITypeCollection"/> implementation which provides a list
    /// of <see cref="IType"/> instances.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public class TypeCollection :
        List<IType>,
        ITypeCollection,
        IEquatable<IControlledTypeCollection>
    {
        /// <summary>
        /// Returns a blank <see cref="ITypeCollection"/> which is locked.
        /// </summary>
        public static readonly ITypeCollection Empty = (LockedTypeCollection)LockedTypeCollection.Empty;
        internal TypeCollection()
        {
        }
        /// <summary>
        /// Creates a new <see cref="TypeCollection"/> instance
        /// with the <paramref name="references"/> provided.
        /// </summary>
        /// <param name="references">The initial array of <see cref="IType"/> to initialize
        /// the <see cref="TypeCollection"/>.</param>
        public TypeCollection(params IType[] references)
        {
            this.AddRange(references);
        }

        internal string StringForm
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            return string.Join(", ", this.OnAll(p => p.ToString()).ToArray());
        }

        #region ITypeCollection Members

        /// <summary>
        /// Inserts and returns the <see cref="IType"/> instances translated from <paramref name="types"/>.
        /// </summary>
        /// <param name="types">The <see cref="IType"/> array to insert as a series.</param>
        public void AddRange(params IType[] types)
        {
            base.AddRange(types);
        }

        #endregion


        #region IEquatable<IControlledTypeCollection> Members

        public bool Equals(IControlledTypeCollection other)
        {
            if (other == null)
                return false;
            if (object.ReferenceEquals(other, this))
                return true;
            if (other.Count != this.Count)
                return false;
            return this.SequenceEqual(other);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is IControlledTypeCollection)
                return this.Equals((IControlledTypeCollection)(obj));
            return false;
        }

        public override int GetHashCode()
        {
            return this.Count.GetHashCode();
        }
    }
}
