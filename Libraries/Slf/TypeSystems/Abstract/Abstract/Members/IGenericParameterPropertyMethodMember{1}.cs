using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with the method of a 
    /// property on a generic parameter.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter in the
    /// abstract type system.</typeparam>
    public interface IGenericParameterPropertyMethodMember<TGenericParameter> :
        IGenericParameterMethodMember<TGenericParameter>,
        IPropertySignatureMethodMember
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
    }
}
