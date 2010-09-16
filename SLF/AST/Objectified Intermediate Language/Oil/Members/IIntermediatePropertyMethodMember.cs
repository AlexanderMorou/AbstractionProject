using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// the method of an intermediate property.
    /// </summary>
    public interface IIntermediatePropertyMethodMember :
        IIntermediatePropertySignatureMethodMember,
        IIntermediateMethodMember,
        IPropertyMethodMember
    {
    }

    public interface IIntermediatePropertySetMethodMember :
        IIntermediatePropertySignatureSetMethodMember,
        IIntermediatePropertyMethodMember
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateMethodParameterMember"/>
        /// which is associated to the <see cref="IIntermediatePropertySetMethodMember"/>.
        /// </summary>
        new IIntermediateMethodParameterMember ValueParameter { get; }
    }
}
