using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    public class ParserSyntaxError :
        SourceRelatedError,
        IParserSyntaxError
    {

        public ParserSyntaxError(string errorText, int line, int column, string fileName)
            : base(errorText, line, column, fileName)
        {
        }
    }
}
