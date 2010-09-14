using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens;

namespace AllenCopeland.Abstraction.Slf._Internal.Oilexer
{
    internal interface _GDResolutionAssistant
    {
        void ResolvedSinglePartToRule(ISoftReferenceProductionRuleItem item, IProductionRuleEntry primary);
        void ResolvedSinglePartToToken(ISoftReferenceProductionRuleItem item, ITokenEntry primary);
        void ResolvedSinglePartToToken(ISoftReferenceTokenItem item, ITokenEntry primary);
        void ResolvedDualPartToTokenItem(ISoftReferenceProductionRuleItem item, ITokenEntry primary, ITokenItem secondary);
        void ResolvedDualPartToTokenItem(ISoftReferenceTokenItem item, ITokenEntry primary, ITokenItem secondary);
        void ResolvedSinglePartToTemplateParameter(IProductionRuleTemplateEntry entry, IProductionRuleTemplatePart primaryTarget, ISoftReferenceProductionRuleItem primary);
        void ResolvedSinglePartToRuleTemplate(ISoftTemplateReferenceProductionRuleItem item, IProductionRuleTemplateEntry primary);
    }
}
