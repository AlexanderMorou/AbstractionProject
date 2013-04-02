using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines properties and methods for working with the
    /// method of an intermediate property signature.
    /// </summary>
    public interface IIntermediatePropertySignatureMethodMember :
        IIntermediateMethodSignatureMember,
        IPropertySignatureMethodMember
    {
        /// <summary>
        /// Returns the dictionary of <see cref="IParameterMember"/> instances for the current <see cref="IIntermediatePropertySignatureMethodMember"/>
        /// implementation.
        /// </summary>
        new IParameterMemberDictionary Parameters { get; }

        /// <summary>
        /// Returns the <see cref="IType"/> that the <see cref="IIntermediatePropertySignatureMethodMember"/>
        /// yields upon return.
        /// </summary>
        new IType ReturnType { get; }
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
