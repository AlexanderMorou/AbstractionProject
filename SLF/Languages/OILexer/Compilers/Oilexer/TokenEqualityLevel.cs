using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;
using AllenCopeland.Abstraction.Slf._Internal.Oilexer.Inlining;

namespace AllenCopeland.Abstraction.Slf.Compilers.Oilexer
{
    internal class TokenEqualityLevel :
        List<InlinedTokenEntry>
    {
        public TokenEqualityLevel(IEnumerable<InlinedTokenEntry> set)
            : base(set)
        {
        }

        public TokenEqualityLevel()
        {

        }
        
        new public void Sort()
        {
            InlinedTokenEntry[] items = (from i in this
                                         orderby i.Name
                                         select i).ToArray();
            this.Clear();
            this.AddRange(items);
        }
    }
}
