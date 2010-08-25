using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface IHighLevelLanguage<TRootNode> :
        ILanguage
        where TRootNode :
            IConcreteNode
    {
        /// <summary>
        /// Returns a new <see cref="IHighLevelLanguageProvider{TRootNode}"/> associated to the current
        /// <see cref="IHighLevelLanguage{TRootNode}"/>.
        /// </summary>
        /// <returns>A new <see cref="IHighLevelLanguageProvider{TRootNode}"/> for the current
        /// <see cref="IHighLevelLanguage{TRootNode}"/>.</returns>
        new IHighLevelLanguageProvider<TRootNode> GetProvider();
    }
}
