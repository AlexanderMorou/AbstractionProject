using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Properties;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;

namespace AllenCopeland.Abstraction.Slf._Internal.Oilexer
{
    partial class GrammarCore
    {
        internal class CompilerErrors
        {
            /// <summary>
            /// Reference compiler error message for when a rule is used like a template,
            /// but no template of the same name can be found.
            /// </summary>
            public static readonly ICompilerReferenceError RuleNotTemplate = new CompilerReferenceError(Resources.GrammarParserErrors_RuleNotTemplate, (int)GDLogicErrors.RuleNotTemplate);
            /// <summary>
            /// Reference compiler error message for when a rule is used without template
            /// insertions, but is only defined as a template.
            /// </summary>
            public static readonly ICompilerReferenceError RuleIsTemplate = new CompilerReferenceError(Resources.GrammarParserErrors_RuleIsTemplate, (int)GDLogicErrors.RuleIsTemplate);
            /// <summary>
            /// Reference compiler error message for when a token expression refers 
            /// to some term which is undefined.
            /// </summary>
            public static readonly ICompilerReferenceError UndefinedTokenReference = new CompilerReferenceError(Resources.GrammarParserErrors_UndefinedTokenReference, (int)GDLogicErrors.UndefinedTokenReference);
            /// <summary>
            /// Reference compiler error message for when a rule expression refers 
            /// to some rule or token which is undefined.
            /// </summary>
            public static readonly ICompilerReferenceError UndefinedRuleReference = new CompilerReferenceError(Resources.GrammarParserErrors_UndefinedRuleReference, (int)GDLogicErrors.UndefinedRuleReference);
            /// <summary>
            /// Reference compiler error message for when a grammar has no start rule.
            /// </summary>
            public static readonly ICompilerReferenceError NoStartDefined = new CompilerReferenceError(Resources.GrammarParserErrors_NoStartDefined, (int)GDLogicErrors.NoStartDefined);
            /// <summary>
            /// Reference compiler error message for when a grammar has an invalid
            /// start defined, such as a terminal instead of a nonterminal.
            /// </summary>
            public static readonly ICompilerReferenceError InvalidStartDefined = new CompilerReferenceError(Resources.GrammarParserErrors_InvalidStartDefined, (int)GDLogicErrors.InvalidStartDefined);
            /// <summary>
            /// Reference compiler error message for when a template is used with a 
            /// series of dynamic arguments, but the number of arguments provided, 
            /// past the fixed arguments, does not evenly distribute across the dynamic
            /// arguments.
            /// </summary>
            public static readonly ICompilerReferenceError DynamicArgumentCountError = new CompilerReferenceError(Resources.GrammarErrors_DynamicArgumentCountError, (int)GDLogicErrors.DynamicArgumentCountError);
            /// <summary>
            /// Reference compiler error message associated to templates which define a series 
            /// of dynamic arguments, and then define a series of fixed arguments, 
            /// which is invalid.
            /// </summary>
            public static readonly ICompilerReferenceError InvalidRepeatOptions = new CompilerReferenceError(Resources.GrammarParserErrors_InvalidRepeatOptions, (int)GDLogicErrors.InvalidRepeatOptions);
            /// <summary>
            /// Reference compiler error message associated to a duplicate term being defined
            /// by the '&#35;define' directive.
            /// </summary>
            public static readonly ICompilerReferenceError DuplicateTermDefined = new CompilerReferenceError(Resources.GrammarErrors_DuplicateTermDefined, (int)GDLogicErrors.DuplicateTermDefined);
            /// <summary>
            /// Reference compiler error message associated to the &#35;AddRule
            /// directive arises when the rule associated to the directive is
            /// not defined.
            /// </summary>
            public static readonly ICompilerReferenceError UndefinedAddRuleTarget = new CompilerReferenceError(Resources.GrammarErrors_AddRuleTargetUndefined, (int)GDLogicErrors.UndefinedAddRuleTarget);
            /// <summary>
            /// Reference compiler error message associated to the deliteralization
            /// process of the linking stage when an unexpected, or unknown, literal
            /// is encountered.
            /// </summary>
            /// <remarks>Likely to occur if compiler is extended, but the appropriate code isn't
            /// updated to follow suit.</remarks>
            public static readonly ICompilerReferenceError UnexpectedLiteralEntry = new CompilerReferenceError(Resources.GrammarCompilerErrors_UnexpectedLiteralEntry, (int)GDLogicErrors.UnexpectedLiteralEntry);
            public static readonly ICompilerReferenceError InvalidPreprocessorCondition = new CompilerReferenceError(Resources.GrammarCompilerErrors_InvalidPreprocessorCondition, (int)GDLogicErrors.InvalidPreprocessorCondition);
            public static readonly ICompilerReferenceError IsDefinedTemplateParameterMustExpectRule = new CompilerReferenceError(Resources.IsDefinedTemplateParameterMustExpectRule, (int)GDLogicErrors.ParameterMustExpectRule);
            public static readonly ICompilerReferenceError InvalidDefinedTarget = new CompilerReferenceError(Resources.GrammarCompilerErrors_InvalidIsDefinedTarget, (int)GDLogicErrors.InvalidIsDefinedTarget);
            public static readonly ICompilerReferenceError LanguageDefinedError = new CompilerReferenceError(Resources.GrammarCompilerErrors_LanguageDefinedError, (int)GDLogicErrors.LanguageDefinedError);
            public static readonly ICompilerReferenceError UnexpectedUndefinedEntry = new CompilerReferenceError(Resources.GrammarCompilerErrors_UnexpectedUndefinedEntry, (int)GDLogicErrors.UnexpectedUndefinedEntry);
        }
    }
}
