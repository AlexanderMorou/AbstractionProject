using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    public interface IProductionRuleTemplatePart :
        IProductionRuleItem
    {
        TemplatePartExpectedSpecial SpecialExpectancy { get; }
        IProductionRuleItem ExpectedSpecific { get; }
    }
}
