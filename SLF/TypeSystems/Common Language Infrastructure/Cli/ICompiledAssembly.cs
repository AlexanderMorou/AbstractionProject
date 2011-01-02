using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for working with a compiled assembly.
    /// </summary>
    public interface ICompiledAssembly  :
        IAssembly
    {
        /// <summary>
        /// Returns the <see cref="System.Reflection.Assembly"/> that the <see cref="ICompiledAssembly"/>
        /// relates to.
        /// </summary>
        Assembly UnderlyingAssembly { get; }
    }
}
