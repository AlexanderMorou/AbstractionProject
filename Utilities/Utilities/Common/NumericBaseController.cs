using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private bool caseSensitive;
        protected NumericBaseController(IEnumerable<char> baseEntities) 
            : this(baseEntities, false)
        {
        }
        internal NumericBaseController(IEnumerable<char> baseEntities, bool skipCheck, bool caseSensitive=true)
        {
            this.baseEntities = new List<char>();
            this.caseSensitive = caseSensitive;
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
        public NumericBaseController(string baseString, bool caseSensitive = true)
            : this((IEnumerable<char>)(!caseSensitive ? baseString.ToUpper() : baseString), false, caseSensitive)
        {
        }

        private BigInteger ShiftBaseValue(int baseIndex, byte position)
        {
            return (BigInteger.Pow(baseEntities.Count, position) * (BigInteger)baseIndex);
        }
        internal int NumPlaces(BigInteger value)
        {
            var l = value == 0 ? 0 : BigInteger.Log10(value) / Math.Log10(this.baseEntities.Count);
            if (((int)l) == l)
                return ((int)(l + 1));
            return (int)Math.Ceiling(l);
        }

        internal string Encode(BigInteger value)
        {
            int nP = NumPlaces(value);
            char[] v = new char[nP];
            for (int i = 0; i < nP; i++)
                v[nP - (i + 1)] = baseEntities[GetShiftIndex(value, i)];
            return new string(v);
        }

        internal BigInteger Decode(string value)
        {
            BigInteger r = 0;
            if (caseSensitive)
            {
                foreach (char c in value)
                    if (!this.baseEntities.Contains(c))
                        throw new ArgumentException("value");
                for (byte b = 0; b < value.Length; b++)
                    r += ShiftBaseValue((int)baseEntities.IndexOf(value[b]), (byte)(value.Length - (b + 1)));
            }
            else
            {
                value = value.ToUpper();
                for (byte b = 0; b < value.Length; b++)
                {
                    char current = value[b];
                    if (!this.baseEntities.Contains(current))
                        throw new ArgumentException("value");
                    r += ShiftBaseValue((int)baseEntities.IndexOf(current), (byte)(value.Length - (b + 1)));
                }
            }
            return (BigInteger)r;
        }


        private int GetShiftIndex(BigInteger value, int position)
        {
            BigInteger valueCopy = value;
            for (int i = 1; i <= position; i++)
                valueCopy /= (BigInteger)this.baseEntities.Count;
            return (int)(valueCopy % (BigInteger)this.baseEntities.Count);
        }

        public bool CaseSensitive { get { return this.caseSensitive; } }

    }
}
