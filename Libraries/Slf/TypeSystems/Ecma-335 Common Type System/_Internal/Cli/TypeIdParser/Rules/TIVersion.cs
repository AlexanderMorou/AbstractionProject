using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules
{
    internal class TIVersionRule :
        _Version,
        ITIVersionRule
    {
        public TIVersionRule(int major, int minor, int build, int revision)
            : base(major, minor, build, revision)
        {
        }
    }
}
