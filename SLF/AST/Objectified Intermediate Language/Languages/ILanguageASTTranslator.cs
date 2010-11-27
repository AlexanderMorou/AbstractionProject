using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguageASTTranslator<TRootNode> :
        ILanguageProcessor<IIntermediateAssembly, TRootNode>,
        ILanguageProcessor<IIntermediateAssembly, TRootNode, IIntermediateAssembly>
    {
        /// <summary>
        /// Processes the <paramref name="input"/> <typeparamref name="TRootNode"/>
        /// with the <paramref name="context"/> provided.
        /// </summary>
        /// <param name="nextInput">The <typeparamref name="TRootNode"/> which denotes the concrete
        /// syntax tree node to yield a <see cref="IIntermediateAssembly"/>.</param>
        /// <param name="currentAssembly">The <see cref="IIntermediateAssembly"/> from a previous
        /// translation.</param>
        /// <returns>The <paramref name="currentAssembly"/> with the contents
        /// of the <paramref name="nextInput"/> merged within the intermediate context.</returns>
        new IIntermediateAssembly Process(TRootNode nextInput, IIntermediateAssembly currentAssembly);
    }
}
