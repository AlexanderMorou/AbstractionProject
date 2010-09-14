using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    /// <summary>
    /// Defines properties and methods for working with a soft reference to an
    /// <see cref="IProductionRuleTemplateEntry"/>.
    /// </summary>
    public interface ISoftTemplateReferenceProductionRuleItem :
        ISoftReferenceProductionRuleItem
    {
        /// <summary>
        /// Returns the parts to suppliment the template's parts.
        /// </summary>
        IReadOnlyCollection<IProductionRuleSeries> Parts { get; } 
    }
}
