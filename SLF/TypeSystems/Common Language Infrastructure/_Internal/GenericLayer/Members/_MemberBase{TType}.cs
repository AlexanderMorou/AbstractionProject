using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _MemberBase<TMember, TParent> :
        MemberBase<TParent>
        where TMember :
            IMember<TParent>
        where TParent :
            IMemberParent
    {
        /// <summary>
        /// Data member for <see cref="Original"/>.
        /// </summary>
        private TMember original;

        /// <summary>
        /// Creates a new <see cref="_MemberBase{TMember, TParent}"/>
        /// with the <paramref name="original"/>
        /// and <paramref name="adjustedParent"/> provided.
        /// </summary>
        /// <param name="original">The original <typeparamref name="TMember"/>
        /// from the generic definition.</param>
        /// <param name="adjustedParent">The proposed generic source.</param>
        protected _MemberBase(TMember original, TParent adjustedParent)
            : base(adjustedParent)
        {
            this.original = original;
        }

        internal protected TMember Original
        {
            get
            {
                return this.original;
            }
        }

        public override void Dispose()
        {
            try
            {
                this.original = default(TMember);
            }
            finally
            {
                base.Dispose();
            }
        }

        protected override string OnGetName()
        {
            return this.Original.Name;
        }

        public override string ToString()
        {
            return this.UniqueIdentifier;
        }
    }
}
