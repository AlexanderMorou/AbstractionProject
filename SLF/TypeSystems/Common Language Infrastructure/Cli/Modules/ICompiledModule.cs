using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with a compiled module.
    /// </summary>
    public interface ICompiledModule :
        IModule
    {
        /// <summary>
        /// Returns the <see cref="Module"/> from 
        /// which the <see cref="ICompiledModule"/> is
        /// derived.
        /// </summary>
        Module UnderlyingModule { get; }
        /// <summary>
        /// Returns the <see cref="ICompiledAssembly"/> in 
        /// which the <see cref="ICompiledModule"/> was 
        /// defined.
        /// </summary>
        new ICompiledAssembly Parent { get; }
    }
}
