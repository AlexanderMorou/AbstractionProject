using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateAssembly<TLanguage, TRootNode, TProvider> :
        IIntermediateAssembly
        where TLanguage :
            IHighLevelLanguage<TRootNode>
        where TRootNode :
            IConcreteNode
        where TProvider :
            IHighLevelLanguageProvider<TRootNode>
    {
        /// <summary>
        /// Returns the <typeparamref name="TLanguage"/> in which the 
        /// <see cref="IIntermediateAssembly{TLanguage, TRootNode, TProvider}"/>
        /// is written in.
        /// </summary>
        TLanguage Language { get; }
        /// <summary>
        /// Returns the <typeparamref name="TProvider"/>
        /// which created the <see cref="IIntermediateAssembly{TLanguage, TRootNode, TProvider}"/>.
        /// </summary>
        TProvider Provider { get; }
    }
}
