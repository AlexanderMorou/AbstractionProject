using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal class FilteredSignatureMembers<TSignature, TSignatureParameter, TSignatureParent> :
        SignatureMembersBase<TSignature, TSignatureParameter, TSignatureParent>,
        IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent>,
        IFilteredSignatureMemberDictionary
        where TSignature :
            ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
    {
        /// <summary>
        /// The data member which holds the 'deviataion' counts for every signature contained.
        /// </summary>
        internal IDictionary<TSignature, int> deviations;
        internal FilteredSignatureMembers(TSignature[] items)
            : base(null)
        {
            foreach (TSignature ts in items)
                this.Add(ts.UniqueIdentifier, ts);
        }

        /// <summary>
        /// Sets the <paramref name="deviation"/> for the <paramref name="member"/>.
        /// </summary>
        /// <param name="member">The <typeparamref name="TSignature"/> that deviates.</param>
        /// <param name="deviation">How far the <paramref name="member"/> deviates from the
        /// original typed signature.</param>
        internal void SetDeviataion(TSignature member, int deviation)
        {
            if (this.deviations == null)
                this.deviations = new Dictionary<TSignature, int>();
            this.deviations[member] = deviation;
        }

        /// <summary>
        /// Returns how far a <paramref name="deviant"/> strayed from the original type signature.
        /// </summary>
        /// <param name="deviant">The <typeparamref name="TSignature"/> expected of deviating.</param>
        /// <returns>A <see cref="System.Int32"/> which indicates how far the <paramref name="deviant"/>
        /// strayed.</returns>
        public int this[TSignature deviant]
        {
            get
            {
                if (this.deviations == null)
                    return 0;
                return this.deviations[deviant];
            }
        }

        internal void SortByDeviations()
        {
            if (this.deviations == null)
                throw new InvalidOperationException("Invalid state.");
            List<TSignature> values = new List<TSignature>(this.Values);
            values.Sort((a, b) => this.deviations[a].CompareTo(this.deviations[b]));
            base.Clear();
            foreach (TSignature s in values)
                this.Add(s.UniqueIdentifier, s);
        }

        int IFilteredSignatureMemberDictionary.this[IMember deviant]
        {
            get
            {
                if (!(deviant is TSignature))
                    throw new ArgumentException(string.Format("must be a TSignature ({0})", typeof(TSignature)), "deviant");
                return this[(TSignature)deviant];
            }
        }
    }
}
