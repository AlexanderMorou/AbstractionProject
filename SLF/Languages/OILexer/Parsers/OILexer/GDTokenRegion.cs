using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;

namespace AllenCopeland.Abstraction.Slf.Parsers.Oilexer
{
    internal class GDTokenRegion :
        IGDRegion
    {
        private ITokenEntry entry;

        public GDTokenRegion(ITokenEntry entry, long bodyStart, long bodyEnd)
        {
            this.entry = entry;
            this.Start = (int)bodyStart;
            this.End = (int)bodyEnd;
        }

        #region IGDRegion Members

        public int Start { get; private set; }

        public int End { get; private set; }

        public string Description
        {
            get { return this.entry.Branches.GetBodyString(); }
        }

        public string CollapseForm
        {
            get { return string.Format("..."); }
        }

        #endregion
    }
}
