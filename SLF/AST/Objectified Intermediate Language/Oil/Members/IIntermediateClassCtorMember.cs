using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with the constructor
    /// on an intermediate class.
    /// </summary>
    public interface IIntermediateClassCtorMember :
        IIntermediateConstructorMember<IClassCtorMember, IIntermediateClassCtorMember, IClassType, IIntermediateClassType>,
        IClassCtorMember
    {
    }
}
