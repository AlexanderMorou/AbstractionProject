using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Common
{
    [CLSCompliant(false)]
    public abstract class NumericBaseController
    {
        private List<char> baseEntities;
        protected NumericBaseController(IEnumerable<char> baseEntities) 
            : this(baseEntities, false)
        {
        }
        internal NumericBaseController(IEnumerable<char> baseEntities, bool skipCheck)
        {
            this.baseEntities = new List<char>();
            if (!skipCheck)
            {
                foreach (char c in baseEntities)
                    if (this.baseEntities.Contains(c))
                        continue;
                    else
                        this.baseEntities.Add(c);
            }
            else
                this.baseEntities.AddRange(baseEntities);
        }
        public NumericBaseController(string baseString)
            : this((IEnumerable<char>)baseString)
        {
        }
        private ulong ShiftBaseValue(int baseIndex, byte position)
        {
            return (ulong)(Math.Pow(baseEntities.Count, position) * baseIndex);
        }
        internal byte NumPlaces(ulong value)
        {
            double l = value == 0 ? 0 : Math.Log10(value) / Math.Log10(this.baseEntities.Count);
            if (((int)l) == l)
                return ((byte)(l + 1));
            return (byte)Math.Ceiling(l);
        }

        internal string Encode(ulong value)
        {
            byte nP = NumPlaces(value);
            char[] v = new char[nP];
            for (byte i = 0; i < nP; i++)
                v[i] = baseEntities[GetShiftIndex(value, i)];
            return new string(v.Reverse().ToArray());
        }

        internal ulong Decode(string value)
        {
            foreach (char c in value)
                if (!this.baseEntities.Contains(c))
                    throw new ArgumentException("value");
            ulong r = 0;
            for (byte b = 0; b < value.Length; b++)
                r += ShiftBaseValue((int)baseEntities.IndexOf(value[b]), (byte)(value.Length - (b + 1)));
            return r;
        }


        private int GetShiftIndex(ulong value, byte position)
        {
            ulong d = (value & (((ulong)(Math.Pow(baseEntities.Count, (position + 1)))) - (ulong)Math.Pow(baseEntities.Count, position))) / (ulong)Math.Pow(baseEntities.Count, position);
            return (int)d;
        }
    }
}
