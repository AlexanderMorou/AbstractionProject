using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class SourceRelatedError :
        SourceRelatedMessage,
        ISourceRelatedError
    {

        public SourceRelatedError(string errorText, int line, int column, string fileName)
            : base(errorText, line, column, fileName)
        {
        }
    }
}
