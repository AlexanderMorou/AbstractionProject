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
    /// Defines properties and methods for working with 
    /// the method of a property on a class.
    /// </summary>
    public interface IClassPropertyMethodMember :
        IClassMethodMember,
        IPropertyMethodMember
    {
    }
}
