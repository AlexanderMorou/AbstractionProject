using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Common
{
    [CLSCompliant(false)]
    public abstract class NumericBase 
    {
        private NumericBaseController controller;
        private BigInteger value;
        protected NumericBase(NumericBaseController controller)
        {
            this.controller = controller;
        }

        protected NumericBase(NumericBaseController controller, string value)
            : this(controller, Decoder(controller, value))
        {
        }

        private static BigInteger Decoder(NumericBaseController controller, string value)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value == string.Empty)
                return 0;
            return controller.Decode(value);
        }

        protected NumericBase(NumericBaseController controller, BigInteger value)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");
            this.value = value;
            this.controller = controller;
        }

        public override string ToString()
        {
            return this.controller.Encode(this.value);
        }
        public BigInteger Value
        {
            get
            {
                return this.value;
            }
        }

        protected abstract NumericBase GetNew(BigInteger value);

        public static NumericBase operator +(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value + right.Value);
        }

        public static NumericBase operator -(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value - right.Value);
        }

        public static NumericBase operator &(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value & right.Value);
        }

        public static NumericBase operator |(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value | right.Value);
        }

        public static NumericBase operator *(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value & right.Value);
        }

        public static NumericBase operator /(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value / right.Value);
        }

        public static NumericBase operator ^(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value ^ right.Value);
        }


        public static NumericBase operator +(NumericBase left, uint right)
        {
            return left.GetNew(left.Value + right);
        }

        public static NumericBase operator -(NumericBase left, uint right)
        {
            return left.GetNew(left.Value - right);
        }

        public static NumericBase operator &(NumericBase left, uint right)
        {
            return left.GetNew(left.Value & right);
        }

        public static NumericBase operator |(NumericBase left, uint right)
        {
            return left.GetNew(left.Value | right);
        }

        public static NumericBase operator *(NumericBase left, uint right)
        {
            return left.GetNew(left.Value & right);
        }

        public static NumericBase operator /(NumericBase left, uint right)
        {
            return left.GetNew(left.Value / right);
        }

        public static NumericBase operator ^(NumericBase left, uint right)
        {
            return left.GetNew(left.Value ^ right);
        }

        public static explicit operator int(NumericBase value)
        {
            return (int)value.value;
        }

        public static implicit operator BigInteger(NumericBase value)
        {
            return value.value;
        }

    }
}
