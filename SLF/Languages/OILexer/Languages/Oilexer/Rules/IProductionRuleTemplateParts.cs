using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    /// <summary>
    /// Defines properties and methods for working with a series of template parts 
    /// for template expansion.
    /// </summary>
    public interface IProductionRuleTemplateParts :
        IReadOnlyCollection<IProductionRuleTemplatePart>
    {

    }
}
