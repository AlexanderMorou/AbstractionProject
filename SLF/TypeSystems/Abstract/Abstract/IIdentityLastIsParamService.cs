using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a service
    /// which determines whether a parameter is a parameter array
    /// candidate.
    /// </summary>
    public interface IIdentityVariableLengthParameterService :
        IIdentityService
    {
        /// <summary>
        /// Returns whether the <paramref name="parameter"/>
        /// provided is a parameter array parameter.
        /// </summary>
        /// <param name="parameter">The <see cref="IParameterMember"/>
        /// to check for parameter array stauts.</param>
        /// <returns>true if the <paramref name="parameter"/> is a 
        /// parameter array candidate; false, otherwise.</returns>
        bool IsVariableLengthParameter(IParameterMember parameter);
        /// <summary>
        /// Returns whether the last parameter of the 
        /// <paramref name="paramParent"/> is a variable length
        /// candidate.
        /// </summary>
        /// <param name="paramParent">The <see cref="IParameterParent"/>
        /// to check the last parameter for variable length candidacy.</param>
        /// <returns>true, if the last parameter of the <paramref name="paramParent"/>
        /// is a parameter array candidate; false, otherwise.</returns>
        bool LastIsVariableLengthParameter(IParameterParent paramParent);
    }
}
