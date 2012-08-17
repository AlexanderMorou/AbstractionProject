using System;
using System.Collections.Generic;
using System.Text;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Provides extended signature data for signatures that have a name and
    /// return type.
    /// </summary>
    public struct ExtendedSignatureData
    {
        /// <summary>
        /// Data member for <see cref="Parameters"/>.
        /// </summary>
        private TypedName[] parameters;
        /// <summary>
        /// Data member for <see cref="Name"/> and <see cref="ReturnType"/>.
        /// </summary>
        private TypedName nameAndReturn;

        /// <summary>
        /// Creates a new <see cref="ExtendedSignatureData"/> instance
        /// with the <paramref name="nameAndReturn"/> and <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndReturn">The <see cref="TypedName"/> that represents the
        /// name of the signature and its return type.</param>
        /// <param name="parameters">The <see cref="TypedName"/> series that represents
        /// the parameters that make up the signature.</param>
        public ExtendedSignatureData(TypedName nameAndReturn, params TypedName[] parameters)
        {
            this.nameAndReturn = nameAndReturn;
            this.parameters = parameters;
        }

        /// <summary>
        /// Initializes a new <see cref="ExtendedSignatureData"/> instance
        /// with the <paramref name="name"/>, <paramref name="returnType"/> and
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="name">The name of the signature represented by the
        /// <see cref="ExtendedSignatureData"/>.</param>
        /// <param name="returnType">The return-type of the signature represented by the
        /// <see cref="ExtendedSignatureData"/>.</param>
        /// <param name="parameters">The <see cref="TypedName"/> series that represents
        /// the signatures parameters.</param>
        public ExtendedSignatureData(string name, IType returnType, params TypedName[] parameters)
            : this(new TypedName(name, returnType), parameters)
        {
        }

        /// <summary>
        /// Returns the name of the <see cref="ExtendedSignatureData"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return this.nameAndReturn.Name;
            }
        }

        /// <summary>
        /// Returns the return type of the <see cref="ExtendedSignatureData"/>.
        /// </summary>
        public IType ReturnType
        {
            get
            {
                return this.nameAndReturn.TypeReference;
            }
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/>
        /// instance for iterating the parameters.
        /// </summary>
        public IEnumerable<TypedName> Parameters
        {
            get
            {
                if (this.parameters == null)
                    yield break;
                for (int i = 0; i < this.parameters.Length; i++)
                    yield return this.parameters[i];
                yield break;
            }
        }
    }
}
