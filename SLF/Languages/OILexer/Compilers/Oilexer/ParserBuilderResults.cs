using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Compilers.Oilexer
{
    /// <summary>
    /// Provides a set of results for the build phase of the Grammar Description
    /// builder.
    /// </summary>
    public class ParserBuilderResults
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> which results
        /// from the build.
        /// </summary>
        public IIntermediateAssembly Project { get; internal set; }

        /// <summary>
        /// Returns the readonly dictionary of time spans for each
        /// phase in the build.
        /// </summary>
        public ReadOnlyDictionary<ParserBuilderPhase, TimeSpan> PhaseTimes { get; internal set; }
        public ReadOnlyDictionary<IProductionRuleEntry, SyntacticalDFARootState> RuleStateMachines { get; internal set; }
        public ICompilerErrorCollection CompilationErrors { get; internal set; }
    }
}
