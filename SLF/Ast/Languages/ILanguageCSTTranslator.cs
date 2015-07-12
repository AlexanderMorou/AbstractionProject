using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cst;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{

    public interface ILanguageCSTTranslator<TRootNode> :
        ILanguageProcessor<IIntermediateAssembly, TRootNode>
        where TRootNode :
            IConcreteNode
    {
        /// <summary>
        /// Processes the <paramref name="nextInput"/> <typeparamref name="TRootNode"/>
        /// with the <paramref name="currentAssembly"/> provided.
        /// </summary>
        /// <param name="nextInput">The <typeparamref name="TRootNode"/> which denotes the concrete
        /// syntax tree node to yield a <see cref="IIntermediateAssembly"/>.</param>
        /// <param name="currentAssembly">The multiple-part <see cref="IIntermediateAssembly"/> from a previous
        /// translation.</param>
        void Process(TRootNode nextInput, IIntermediateAssembly currentAssembly);
    }
}
