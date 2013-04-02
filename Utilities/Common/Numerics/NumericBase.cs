using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Numerics
{
    /// <summary>
    /// Provides a numeric base which can vary based upon the finite
    /// sequence of characters used to denote the value of the characters
    /// by their order in the sequence.
    /// </summary>
    public abstract class NumericBase 
    {
        private NumericBaseController controller;
        private BigInteger value;

        /// <summary>
        /// Creates a new <see cref="NumericBase"/> which receives
        /// the <paramref name="controller"/> used for managing the current instance.
        /// </summary>
        /// <param name="controller">The <see cref="NumericBaseController"/>
        /// which contains the information relative to translation from a number
        /// to the finite sequence equivalent.</param>
        protected NumericBase(NumericBaseController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Creates a new <see cref="NumericBase"/> which receives
        /// the <paramref name="controller"/> used for managing the current instance
        /// with the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="controller">The <see cref="NumericBaseController"/>
        /// which contains the information relative to translation from a number
        /// to the finite sequence equivalent.</param>
        /// <param name="value">The <see cref="String"/> value that is decoded
        /// using the <paramref name="controller"/>.</param>
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

        /// <summary>
        /// Creates a new <see cref="NumericBase"/> which receives
        /// the <paramref name="controller"/> used for managing the current instance
        /// with the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="controller">The <see cref="NumericBaseController"/>
        /// which contains the information relative to translation from a number
        /// to the finite sequence equivalent.</param>
        /// <param name="value">The <see cref="BigInteger"/> which represents the numeric
        /// value of the <see cref="NumericBase"/> which is encoded into the finite sequence 
        /// equivalent.</param>
        protected NumericBase(NumericBaseController controller, BigInteger value)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");
            this.value = value;
            this.controller = controller;
        }

        /// <summary>
        /// Provides the <see cref="String"/> equivalent of the
        /// current <see cref="NumericBase"/> using the controller
        /// passed during construction.
        /// </summary>
        /// <returns>A <see cref="String"/> value that represents
        /// the finite sequence equivalent of the 
        /// <see cref="Value"/>.</returns>
        public override string ToString()
        {
            return this.controller.Encode(this.value);
        }

        /// <summary>
        /// Returns the <see cref="BigInteger"/> which 
        /// </summary>
        public BigInteger Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Obtains a new <see cref="NumericBase"/> relative to the
        /// type which inherits the abstract class.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/>
        /// which represents the value of the new <see cref="NumericBase"/>
        /// derivation.
        /// </param>
        /// <returns>A <see cref="NumericBase"/> relative to the
        /// type which inherits the abstract class.</returns>
        protected abstract NumericBase GetNew(BigInteger value);

        /// <summary>
        /// Adds a <see cref="NumericBase"/> to another <see cref="NumericBase"/>.
        /// </summary>
        /// <param name="left">The first <see cref="NumericBase"/> to add.</param>
        /// <param name="right">The second <see cref="NumericBase"/> to add.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the addition of
        /// the <paramref name="left"/> and <paramref name="right"/> operands.</returns>
        /// <exception cref="System.ArgumentException">thrown when the 
        /// <paramref name="left"/> <see cref="NumericBase"/> is not of the
        /// same kind of <see cref="NumericBase"/> as the <paramref name="right"/>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> or <paramref name="right"/> are null.</exception>
        public static NumericBase operator +(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value + right.Value);
        }


        /// <summary>
        /// Subtracts a <see cref="NumericBase"/> from another <see cref="NumericBase"/>.
        /// </summary>
        /// <param name="left">The first <see cref="NumericBase"/> to subtract.</param>
        /// <param name="right">The second <see cref="NumericBase"/> to subtract.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the subtraction of
        /// the <paramref name="right"/> from the <paramref name="left"/> operand.</returns>
        /// <exception cref="System.ArgumentException">thrown when the 
        /// <paramref name="left"/> <see cref="NumericBase"/> is not of the
        /// same kind of <see cref="NumericBase"/> as the <paramref name="right"/>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> or <paramref name="right"/> are null.</exception>
        public static NumericBase operator -(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value - right.Value);
        }

        /// <summary>
        /// Performs a bitwise intersection of the bits between two 
        /// <see cref="NumericBase"/> instances.
        /// </summary>
        /// <param name="left">The first <see cref="NumericBase"/> to perform a bitwise intersection between.</param>
        /// <param name="right">The second <see cref="NumericBase"/> to perform a bitwise intersection between.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the bitwise intersection of
        /// the <paramref name="left"/> and <paramref name="right"/> operands.</returns>
        /// <exception cref="System.ArgumentException">thrown when the 
        /// <paramref name="left"/> <see cref="NumericBase"/> is not of the
        /// same kind of <see cref="NumericBase"/> as the <paramref name="right"/>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> or <paramref name="right"/> are null.</exception>
        public static NumericBase operator &(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value & right.Value);
        }

        /// <summary>
        /// Performs a bitwise union of the bits between two 
        /// <see cref="NumericBase"/> instances.
        /// </summary>
        /// <param name="left">The first <see cref="NumericBase"/> to perform a bitwise union on.</param>
        /// <param name="right">The second <see cref="NumericBase"/> to perform a bitwise union on.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the bitwise union of
        /// the <paramref name="left"/> and <paramref name="right"/> operands.</returns>
        /// <exception cref="System.ArgumentException">thrown when the 
        /// <paramref name="left"/> <see cref="NumericBase"/> is not of the
        /// same kind of <see cref="NumericBase"/> as the <paramref name="right"/>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> or <paramref name="right"/> are null.</exception>
        public static NumericBase operator |(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value | right.Value);
        }

        /// <summary>
        /// Performs a multiplicitive operation between two <see cref="NumericBase"/> 
        /// instances.
        /// </summary>
        /// <param name="left">The first <see cref="NumericBase"/> within the multiplication
        /// operation.</param>
        /// <param name="right">The second <see cref="NumericBase"/> within the multiplication
        /// operation.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the product
        /// of the <paramref name="left"/> and <paramref name="right"/>
        /// operands.</returns>
        /// <exception cref="System.ArgumentException">thrown when the 
        /// <paramref name="left"/> <see cref="NumericBase"/> is not of the
        /// same kind of <see cref="NumericBase"/> as the <paramref name="right"/>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> or <paramref name="right"/> are null.</exception>
        public static NumericBase operator *(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value * right.Value);
        }

        /// <summary>
        /// Performs a division operation between two <see cref="NumericBase"/>
        /// instances.
        /// </summary>
        /// <param name="left">The numerator <see cref="NumericBase"/>
        /// instance.</param>
        /// <param name="right">The denominator <see cref="NumericBase"/>
        /// instance.</param>
        /// <returns>A new <see cref="NumericBase"/>
        /// instance that represents the integer quotient between the 
        /// <paramref name="left"/> and <paramref name="right"/>
        /// operands.</returns>
        /// <exception cref="System.ArgumentException">thrown when the 
        /// <paramref name="left"/> <see cref="NumericBase"/> is not of the
        /// same kind of <see cref="NumericBase"/> as the <paramref name="right"/>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> or <paramref name="right"/> are null.</exception>
        public static NumericBase operator /(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value / right.Value);
        }

        /// <summary>
        /// Performs a bitwise symmetric difference between two
        /// <see cref="NumericBase"/> instances.
        /// </summary>
        /// <param name="left">The first <see cref="NumericBase"/> in the 
        /// symmetric difference operation.</param>
        /// <param name="right">The second <see cref="NumericBase"/> in the 
        /// symmetric difference operation.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the
        /// symmetric difference between the <paramref name="left"/>
        /// and <paramref name="right"/> operands.</returns>
        /// <exception cref="System.ArgumentException">thrown when the 
        /// <paramref name="left"/> <see cref="NumericBase"/> is not of the
        /// same kind of <see cref="NumericBase"/> as the <paramref name="right"/>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> or <paramref name="right"/> are null.</exception>
        public static NumericBase operator ^(NumericBase left, NumericBase right)
        {
            if (left.controller != right.controller)
                throw new ArgumentException("left");
            return left.GetNew(left.Value ^ right.Value);
        }

        /// <summary>
        /// Adds a <see cref="BigInteger"/> to a <see cref="NumericBase"/> instance.
        /// </summary>
        /// <param name="left">The <see cref="NumericBase"/> augend of the addition operation.</param>
        /// <param name="right">The <see cref="BigInteger"/> addend of the addition operation.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the sum of
        /// the <paramref name="left"/> and <paramref name="right"/> operands.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> is null.</exception>
        public static NumericBase operator +(NumericBase left, BigInteger right)
        {
            return left.GetNew(left.Value + right);
        }

        /// <summary>
        /// Subtracts a <see cref="BigInteger"/> from a <see cref="NumericBase"/>.
        /// </summary>
        /// <param name="left">The <see cref="NumericBase"/> minuend of the subtraction operation.</param>
        /// <param name="right">The <see cref="BigInteger"/> subtrahend of the subtraction operation.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the subtraction of
        /// the <paramref name="right"/> from the <paramref name="left"/> operand.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> is null.</exception>
        public static NumericBase operator -(NumericBase left, BigInteger right)
        {
            return left.GetNew(left.Value - right);
        }

        /// <summary>
        /// Performs a bitwise intersection of the bits between a 
        /// <see cref="NumericBase"/> instance and a <see cref="BigInteger"/>.
        /// </summary>
        /// <param name="left">The <see cref="NumericBase"/> to perform a bitwise intersection on.</param>
        /// <param name="right">The <see cref="BigInteger"/> to perform a bitwise intersection on.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the bitwise intersection of
        /// the <paramref name="left"/> and <paramref name="right"/> operands.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> is null.</exception>
        public static NumericBase operator &(NumericBase left, BigInteger right)
        {
            return left.GetNew(left.Value & right);
        }

        /// <summary>
        /// Performs a bitwise union of the bits between a 
        /// <see cref="NumericBase"/> instance and a <see cref="BigInteger"/>.
        /// </summary>
        /// <param name="left">The <see cref="NumericBase"/> to perform a bitwise union on.</param>
        /// <param name="right">The <see cref="NumericBase"/> to perform a bitwise union on.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the bitwise union of
        /// the <paramref name="left"/> and <paramref name="right"/> operands.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> is null.</exception>
        public static NumericBase operator |(NumericBase left, BigInteger right)
        {
            return left.GetNew(left.Value | right);
        }

        /// <summary>
        /// Performs a multiplicitive operation between a <see cref="NumericBase"/> 
        /// instance and a <see cref="BigInteger"/>.
        /// </summary>
        /// <param name="left">The <see cref="NumericBase"/> within the multiplication
        /// operation.</param>
        /// <param name="right">The <see cref="BigInteger"/> within the multiplication
        /// operation.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the product
        /// of the <paramref name="left"/> and <paramref name="right"/>
        /// operands.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> is null.</exception>
        public static NumericBase operator *(NumericBase left, BigInteger right)
        {
            return left.GetNew(left.Value * right);
        }

        /// <summary>
        /// Performs a division operation between a <see cref="NumericBase"/>
        /// instance and a <see cref="BigInteger"/>.
        /// </summary>
        /// <param name="left">The numerator <see cref="NumericBase"/>
        /// instance.</param>
        /// <param name="right">The denominator <see cref="BigInteger"/>.</param>
        /// <returns>A new <see cref="NumericBase"/>
        /// instance that represents the integer quotient between the 
        /// <paramref name="left"/> and <paramref name="right"/>
        /// operands.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> is null.</exception>
        public static NumericBase operator /(NumericBase left, BigInteger right)
        {
            return left.GetNew(left.Value / right);
        }

        /// <summary>
        /// Performs a bitwise symmetric difference between a
        /// <see cref="NumericBase"/> instance and a <see cref="BigInteger"/>.
        /// </summary>
        /// <param name="left">The <see cref="NumericBase"/> of the 
        /// symmetric difference operation.</param>
        /// <param name="right">The <see cref="BigInteger"/> of the 
        /// symmetric difference operation.</param>
        /// <returns>A new <see cref="NumericBase"/> which represents the
        /// symmetric difference between the <paramref name="left"/>
        /// and <paramref name="right"/> operands.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="left"/> is null.</exception>
        public static NumericBase operator ^(NumericBase left, BigInteger right)
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

        /// <summary>
        /// Creates a new <see cref="SeptemvigesimalBase"/>, base-27,
        /// instance with the intrinsic <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to parse
        /// into base-27.</param>
        /// <returns>A new <see cref="SeptemvigesimalBase"/>
        /// instance.</returns>
        public static SeptemvigesimalBase GetBase27(string value)
        {
            return new SeptemvigesimalBase(value);
        }

        /// <summary>
        /// Creates a new <see cref="SeptemvigesimalBase"/>, base-27,
        /// instance with the intrinsic <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The intrinsic <see cref="BigInteger"/> 
        /// base-27 value.</param>
        /// <returns>A new <see cref="SeptemvigesimalBase"/>
        /// instance.</returns>
        public static SeptemvigesimalBase GetBase27(BigInteger value)
        {
            return new SeptemvigesimalBase(value);
        }

        /// <summary>
        /// Creates a new <see cref="DuosexagesimalBase"/>, base-62,
        /// instance with the intrinsic <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to parse
        /// into base-62.</param>
        /// <returns>A new <see cref="DuosexagesimalBase"/>
        /// instance.</returns>
        public static DuosexagesimalBase GetBase62(string value)
        {
            return new DuosexagesimalBase(value);
        }

        /// <summary>
        /// Creates a new <see cref="DuosexagesimalBase"/>, base-62,
        /// instance with the intrinsic <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The intrinsic <see cref="BigInteger"/> 
        /// base-62 value.</param>
        /// <returns>A new <see cref="DuosexagesimalBase"/>
        /// instance.</returns>
        public static DuosexagesimalBase GetBase62(BigInteger value)
        {
            return new DuosexagesimalBase(value);
        }

        /// <summary>
        /// Creates a new <see cref="OctalBase"/>, base-8,
        /// instance with the intrinsic <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to parse
        /// into base-8.</param>
        /// <returns>A new <see cref="OctalBase"/>
        /// instance.</returns>
        public static OctalBase GetBase8(string value)
        {
            return new OctalBase(value);
        }

        /// <summary>
        /// Creates a new <see cref="OctalBase"/>, base-8,
        /// instance with the intrinsic <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The intrinsic <see cref="BigInteger"/> 
        /// base-8 value.</param>
        /// <returns>A new <see cref="OctalBase"/>
        /// instance.</returns>
        public static OctalBase GetBase8(BigInteger value)
        {
            return new OctalBase(value);
        }


        /// <summary>
        /// Creates a new <see cref="OctodecimalBase"/>, base-18,
        /// instance with the intrinsic <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to parse
        /// into base-18.</param>
        /// <returns>A new <see cref="OctodecimalBase"/>
        /// instance.</returns>
        public static OctodecimalBase GetBase18(string value)
        {
            return new OctodecimalBase(value);
        }

        /// <summary>
        /// Creates a new <see cref="OctodecimalBase"/>, base-18,
        /// instance with the intrinsic <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The intrinsic <see cref="BigInteger"/> 
        /// base-18 value.</param>
        /// <returns>A new <see cref="OctodecimalBase"/>
        /// instance.</returns>
        public static OctodecimalBase GetBase18(BigInteger value)
        {
            return new OctodecimalBase(value);
        }
    }
}
