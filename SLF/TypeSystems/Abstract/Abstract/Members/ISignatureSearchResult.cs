using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public interface ISignatureSearchResult
    {
        /// <summary>
        /// Returns the <see cref="ISignatureMember"/> associated with the 
        /// <see cref="ISignatureSearchResult"/>.
        /// </summary>
        ISignatureMember ResultMember { get; }
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of <see cref="IParameterMember"/>
        /// elements as they appear relative to the search.
        /// </summary>
        IEnumerable<IParameterMember> ParameterOrder { get; }
    }
}
