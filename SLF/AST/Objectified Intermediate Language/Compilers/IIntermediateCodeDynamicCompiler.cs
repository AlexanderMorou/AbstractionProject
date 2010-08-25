using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface IIntermediateCodeDynamicCompiler :
        ICompiler<IIntermediateCodeDynamicCompilerAid>
    {
        /// <summary>
        /// Returns the current <see cref="AssemblyBuilder"/> for the <see cref="IIntermediateAssembly"/> being compiled.
        /// </summary>
        AssemblyBuilder CurrentAssembly { get; }

        /// <summary>
        /// Returns the current <see cref="ModuleBuilder"/> for the <see cref="IIntermediateModule"/> being created.
        /// </summary>
        ModuleBuilder CurrentModule { get; }

        /// <summary>
        /// Returns the current <see cref="TypeBuilder"/> for the <see cref="IIntermediateType"/> being created.
        /// </summary>
        TypeBuilder CurrentType { get; }

        /// <summary>
        /// Returns a dictionary containing the types associated to the current build.
        /// </summary>
        IControlledStateDictionary<IIntermediateType, TypeBuilder> ActiveTypes { get; }
    }
}
