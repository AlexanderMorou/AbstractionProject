﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
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
    /// Provides a base <see cref="ITypeCollection"/> implementation which provides a list
    /// of <see cref="IType"/> instances.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public class TypeCollection :
        List<IType>,
        ITypeCollection
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
        /*//
        public TypeCollection(params Type[] references)
        {
            this.AddRange(references);
        }
        //*/

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
        public void AddRange(IType[] types)
        {
            base.AddRange(types);
        }

        #endregion
    }
}