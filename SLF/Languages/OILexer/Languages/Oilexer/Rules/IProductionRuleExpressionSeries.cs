using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    public interface IProductionRuleSeries :
        IReadOnlyCollection<IProductionRule>,
        IProductionRuleSource
    {

        /// <summary>
        /// Obtains the string form of the body of the 
        /// <see cref="IProductionRuleSeries"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing
        /// the elements within the description of the
        /// <see cref="IProductionRuleSeries"/>.</returns>
        string GetBodyString();
    }
}
