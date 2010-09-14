using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    /// <summary>
    /// Defines properties and methods for working with a <see cref="IEntry"/> production rule.
    /// Used to express a part of syntax for a <see cref="IGDFile"/>.
    /// </summary>
    public interface IProductionRuleEntry :
        IProductionRuleSeries,
        IScannableEntry
    {
        /// <summary>
        /// Returns/sets whether the elements of 
        /// the <see cref="IProductionRuleEntry"/>
        /// inherit the name of the 
        /// <see cref="IProductionRuleEntry"/>.
        /// </summary>
        bool ElementsAreChildren { get; set; }
    }
}
