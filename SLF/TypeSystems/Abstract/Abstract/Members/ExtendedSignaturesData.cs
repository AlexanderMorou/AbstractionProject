using System;
using System.Collections.Generic;
using System.Text;
/*----------------------------------------\
| Copyright © 2010 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Provides a data structure for defining a series of extended signature declarations which
    /// contain a parameter set and return-type.
    /// </summary>
    public struct ExtendedSignaturesData
    {
        /// <summary>
        /// An empty extended signature series data set.
        /// </summary>
        public static readonly ExtendedSignaturesData Empty = new ExtendedSignaturesData();
        /// <summary>
        /// Data member for <see cref="Signatures"/>.
        /// </summary>
        private ExtendedSignatureData[] signatures;

        /// <summary>
        /// Creates a new <see cref="ExtendedSignaturesData"/> instance with the 
        /// <paramref name="signatures"/> provided.
        /// </summary>
        /// <param name="signatures">The series of <see cref="ExtendedSignatureData"/>
        /// which the <see cref="ExtendedSignaturesData"/> represent.</param>
        public ExtendedSignaturesData(params ExtendedSignatureData[] signatures)
        {
            this.signatures = signatures;
        }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> which
        /// enumerates the <see cref="ExtendedSignatureData"/> elements
        /// within the <see cref="ExtendedSignaturesData"/> set.
        /// </summary>
        public IEnumerable<ExtendedSignatureData> Signatures
        {
            get
            {
                if (this.signatures == null)
                    yield break;
                for (int i = 0; i < this.signatures.Length; i++)
                    yield return this.signatures[i];
                yield break;
            }
        }
    }
}
