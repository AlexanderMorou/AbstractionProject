using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with the
    /// method of an intermediate property signature.
    /// </summary>
    public interface IIntermediatePropertySignatureMethodMember :
        IIntermediateMethodSignatureMember,
        IPropertySignatureMethodMember
    {
    }

    public interface IIntermediatePropertySignatureSetMethodMember :
        IIntermediatePropertySignatureMethodMember
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateSignatureParameterMember"/>
        /// which is associated to the <see cref="IIntermediatePropertySignatureSetMethodMember"/>.
        /// </summary>
        IIntermediateSignatureParameterMember ValueParameter { get; }
    }
}
