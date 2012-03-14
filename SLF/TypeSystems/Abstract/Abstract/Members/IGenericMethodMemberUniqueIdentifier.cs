using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public interface IGenericSignatureMemberUniqueIdentifier :
        ISignatureMemberUniqueIdentifier,
        IGenericParamParentUniqueIdentifier
    {
        /// <summary>
        /// Creates a <see cref="String"/> value which represents
        /// the <see cref="IGenericSignatureMemberUniqueIdentifier"/> 
        /// as a string, with the <paramref name="parentName"/> provided.
        /// </summary>
        /// <param name="parentName">The <see cref="String"/> value
        /// which denotes the name of the parent containing the identity 
        /// attached to the <see cref="IGenericSignatureMemberUniqueIdentifier"/></param>
        string ToString(string parentName);
    }
}
