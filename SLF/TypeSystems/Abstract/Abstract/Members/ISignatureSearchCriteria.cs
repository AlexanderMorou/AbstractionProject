using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public interface ISignatureSearchCriteria
    {
        /// <summary>
        /// Returns the name of the element to find.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the <see cref="ISignatureSearchParameterCriteria"/> which denotes
        /// information about the parameters of the search.
        /// </summary>
        ISignatureSearchParameterCriteria ParameterInfo { get; }
    }
}
