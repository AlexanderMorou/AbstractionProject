using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction
{
    public struct CurrentPreviousPair 
    {
        private int previous;
        private int current;
        public CurrentPreviousPair(int previous, int current)
        {
            this.previous = previous;
            this.current = current;
        }

        public int Previous { get { return this.previous; } }
        public int Current { get { return this.current; } }
    }
}
