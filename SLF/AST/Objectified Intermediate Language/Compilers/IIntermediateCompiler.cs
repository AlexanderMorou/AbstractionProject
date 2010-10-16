using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Languages;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface IIntermediateCompiler<TRootNode> :
        ICompiler<IIntermediateCompilerAid<TRootNode>>
        where TRootNode :
            IConcreteNode
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

        /// <summary>
        /// Returns the <see cref="IHighLevelLanguage{TRootNode}"/> associated to the
        /// <see cref="IIntermediateCompiler{TRootNode}"/>.
        /// </summary>
        new IHighLevelLanguage<TRootNode> Language { get; }
        /// <summary>
        /// Returns the <see cref="IHighLevelLanguageProvider{TRootNode}"/> associated to the 
        /// <see cref="IIntermediateCompiler{TRootNode}"/>.
        /// </summary>
        new IHighLevelLanguageProvider<TRootNode> Provider { get; }
    }
}
