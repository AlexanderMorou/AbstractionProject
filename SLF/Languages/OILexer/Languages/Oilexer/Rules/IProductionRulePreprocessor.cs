using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    public interface IProductionRulePreprocessorDirective :
        IProductionRuleItem
    {
        /// <summary>
        /// Returns the <see cref="IPreprocessorDirective"/> which was parsed 
        /// </summary>
        IPreprocessorDirective Directive { get; }
        /// <summary>
        /// Creates a copy of the current <see cref="IProductionRulePreprocessorDirective"/>.
        /// </summary>
        /// <returns>A new <see cref="IProductionRulePreprocessorDirective"/> with the data
        /// members of the current <see cref="IProductionRulePreprocessorDirective"/>.</returns>
        new IProductionRulePreprocessorDirective Clone();
    }
}
