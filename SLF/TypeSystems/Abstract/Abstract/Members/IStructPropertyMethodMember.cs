using System;
using System.Collections.Generic;
using System.Text;
/*----------------------------------------\
| Copyright © 2010 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with the method of a 
    /// property member defined on a data structure.
    /// </summary>
    public interface IStructPropertyMethodMember :
        IStructMethodMember,
        IPropertyMethodMember
    {
    }
}
