using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguageASTTranslator<TRootNode> :
        ILanguageProcessor<IIntermediateAssembly, TRootNode>
    {
        /// <summary>
        /// Processes the <paramref name="nextInput"/> <typeparamref name="TRootNode"/>
        /// with the <paramref name="currentAssembly"/> provided.
        /// </summary>
        /// <param name="nextInput">The <typeparamref name="TRootNode"/> which denotes the concrete
        /// syntax tree node to yield a <see cref="IIntermediateAssembly"/>.</param>
        /// <param name="currentAssembly">The <see cref="IIntermediateAssembly"/> from a previous
        /// translation.</param>
        void Process(TRootNode nextInput, IIntermediateAssembly currentAssembly);
    }
}
