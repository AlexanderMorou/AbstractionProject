using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
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
