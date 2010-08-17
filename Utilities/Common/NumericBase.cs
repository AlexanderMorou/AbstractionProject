using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
        private ulong value;
        protected NumericBase(NumericBaseController controller)
        {
            this.controller = controller;
        }

        protected NumericBase(NumericBaseController controller, string value)
            : this(controller, Decoder(controller, value))
        {
        }

        private static ulong Decoder(NumericBaseController controller, string value)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value == string.Empty)
                return 0;
            return controller.Decode(value);
        }

        protected NumericBase(NumericBaseController controller, ulong value)
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
        public ulong Value
        {
            get
            {
                return this.value;
            }
        }

        protected abstract NumericBase GetNew(ulong value);

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

    }
}
