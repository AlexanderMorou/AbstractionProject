using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public enum StringTerminalKind
    {
        Unknown,
        Include,
        Root,
        AssemblyName,
        LexerName,
        GrammarName,
        ParserName,
        TokenPrefix,
        TokenSuffix,
        RulePrefix,
        RuleSuffix,
        Namespace,
    }
}
