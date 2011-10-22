using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Provides a container for a series of signatures which define a series of parameters.
    /// </summary>
    public struct SignaturesData
    {
        /// <summary>
        /// An empty signature data set.
        /// </summary>
        public static readonly SignaturesData Empty = new SignaturesData();
        public static readonly SignaturesData DefaultConstructorSet = new SignaturesData(SignatureData.Empty);
        /// <summary>
        /// Data member for <see cref="Signatures"/>.
        /// </summary>
        private SignatureData[] signatures;

        /// <summary>
        /// Creates a new <see cref="SignaturesData"/> instance with the 
        /// <paramref name="signatures"/> provided.
        /// </summary>
        /// <param name="signatures">The series of <see cref="SignatureData"/>
        /// which the <see cref="SignaturesData"/> represent.</param>
        public SignaturesData(params SignatureData[] signatures)
        {
            this.signatures = signatures;
        }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> which
        /// enumerates the <see cref="SignatureData"/> elements
        /// within the <see cref="SignaturesData"/> set.
        /// </summary>
        public IEnumerable<SignatureData> Signatures
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
