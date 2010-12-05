using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides information about an expression or series of expressions.
    /// </summary>
    public struct ExpressionKind :
        IEquatable<ExpressionKind>
    {
        /// <summary>
        /// Data member for <see cref="BinaryOperations"/>.
        /// </summary>
        private   BinaryOperationSector binaryOps;
        /// <summary>
        /// Data member for <see cref="ExpansionsRequired"/>.
        /// </summary>
        private ExpansionRequiredSector expansionsRequired;
        /// <summary>
        /// Data member for <see cref="Invocations"/>.
        /// </summary>
        private        InvocationSector invocations;
        /// <summary>
        /// Data member for <see cref="PrimitiveInserts"/>.
        /// </summary>
        private   PrimitiveInsertSector primitives;
        /// <summary>
        /// Data member for <see cref="ReferenceKinds"/>.
        /// </summary>
        private         ReferenceSector referenceKinds;
        /// <summary>
        /// Data member for <see cref="SpecialFunctions"/>.
        /// </summary>
        private   SpecialFunctionSector specialFuncs;
        /// <summary>
        /// Data member for <see cref="Symbols"/>.
        /// </summary>
        private            SymbolSector symbols;
        /// <summary>
        /// Data member for <see cref="UnaryOperators"/>.
        /// </summary>
        private    UnaryOperationSector unaryOps;

        /// <summary>
        /// Data member for <see cref="CustomExpressionKinds"/>.
        /// </summary>
        private Guid[] custom;

        /// <summary>
        /// Returns the <see cref="BinaryOperationSector"/> associated
        /// to the <see cref="ExpressionKind"/>.
        /// </summary>
        /// <remarks>
        /// <para>Can be multiple values from this sector for cases
        /// of describing language feature implementation.</para>
        /// <para>It is required that all expressions return exactly 
        /// one value from each sector.</para>
        /// </remarks>
        public BinaryOperationSector BinaryOperations
        {
            get
            {
                return this.binaryOps;
            }
        }

        /// <summary>
        /// Returns the <see cref="ExpansionRequiredSector"/> associated
        /// to the <see cref="ExpressionKind"/>.
        /// </summary>
        /// <remarks>
        /// <para>Can be multiple values from this sector for cases
        /// of describing language feature implementation.</para>
        /// <para>It is required that all expressions return exactly 
        /// one value from each sector.</para>
        /// </remarks>
        public ExpansionRequiredSector ExpansionsRequired
        {
            get
            {
                return this.expansionsRequired;
            }
        }

        /// <summary>
        /// Returns the <see cref="InvocationSector"/> associated to the
        /// <see cref="ExpressionKind"/>.
        /// </summary>
        /// <remarks>
        /// <para>Can be multiple values from this sector for cases
        /// of describing language feature implementation.</para>
        /// <para>It is required that all expressions return exactly 
        /// one value from each sector.</para>
        /// </remarks>
        public InvocationSector Invocations
        {
            get
            {
                return this.invocations;
            }
        }

        /// <summary>
        /// Returns the <see cref="PrimitiveInsertSector"/> associated
        /// to the <see cref="ExpressionKind"/>.
        /// </summary>
        /// <remarks>
        /// <para>Can be multiple values from this sector for cases
        /// of describing language feature implementation.</para>
        /// <para>It is required that all expressions return exactly 
        /// one value from each sector.</para>
        /// </remarks>
        public PrimitiveInsertSector PrimitiveInserts
        {
            get
            {
                return this.primitives;
            }
        }

        /// <summary>
        /// Returns the <see cref="ReferenceSector"/> associated
        /// to the <see cref="ExpressionKind"/>.
        /// </summary>
        /// <remarks>
        /// <para>Can be multiple values from this sector for cases
        /// of describing language feature implementation.</para>
        /// <para>It is required that all expressions return exactly 
        /// one value from each sector.</para>
        /// </remarks>
        public ReferenceSector ReferenceKinds
        {
            get
            {
                return this.referenceKinds;
            }
        }

        /// <summary>
        /// Returns the <see cref="SpecialFunctionSector"/>
        /// associated to the <see cref="ExpressionKind"/>.
        /// </summary>
        public SpecialFunctionSector SpecialFunctions
        {
            get
            {
                return this.specialFuncs;
            }
        }

        /// <summary>
        /// Returns the <see cref="SymbolSector"/> associated to
        /// the <see cref="ExpressionKind"/>.
        /// </summary>
        public SymbolSector Symbols
        {
            get
            {
                return this.symbols;
            }
        }

        /// <summary>
        /// Returns the <see cref="UnaryOperationSector"/> 
        /// associated to the <see cref="ExpressionKind"/>.
        /// </summary>
        public UnaryOperationSector UnaryOperators
        {
            get
            {
                return this.unaryOps;
            }
        }

        private void AppendSector<T>(T sector, T noneValue, string sectorName, StringBuilder target, ref bool first)
            where T : 
                struct
        {
            if (!sector.Equals(noneValue))
            {
                if (first)
                    first = false;
                else
                    target.Append(", ");
                target.AppendFormat("{0} ({1})", sectorName, sector);
            }
        }

        /// <summary>
        /// Converts the current <see cref="ExpressionKind"/> to a 
        /// <see cref="String"/> value.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the current
        /// <see cref="ExpressionKind"/> instance.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            AppendSector(this.binaryOps, BinaryOperationSector.None, "Binary Operations", sb, ref first);
            AppendSector(this.expansionsRequired, ExpansionRequiredSector.None, "Expansions Required", sb, ref first);
            AppendSector(this.invocations, InvocationSector.None, "Invocations", sb, ref first);
            AppendSector(this.primitives, PrimitiveInsertSector.None, "Primitive Inserts", sb, ref first);
            AppendSector(this.referenceKinds, ReferenceSector.None, "ReferenceKinds", sb, ref first);
            AppendSector(this.specialFuncs, SpecialFunctionSector.None, "Special Functions", sb, ref first);
            AppendSector(this.symbols, SymbolSector.None, "Symbols", sb, ref first);
            AppendSector(this.unaryOps, UnaryOperationSector.None, "Unary Operations", sb, ref first);
            if (this.custom != null)
            {
                if (first)
                    first = false;
                else
                    sb.Append(", ");
                sb.AppendFormat("Custom Expressions ({0})", this.GuidsToString());
            }
            if (first)
                sb.Append("None");
            return sb.ToString();
        }

        private string GuidsToString()
        {
            if (custom == null)
                return null;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.custom.Length; i++)
            {
                if (i > 0)
                    sb.Append(", ");
                sb.Append(this.custom[i].ToString());
            }
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is ExpressionKind)
                return this.Equals(((ExpressionKind)(obj)));
            return false;
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> value which represents the 
        /// fairly unique value associated to the <see cref="ExpressionKind"/>.
        /// </summary>
        /// <returns>A <see cref="Int32"/> value representing the
        /// somewhat unique value of the <see cref="ExpressionKind"/>.</returns>
        /// <remarks>Used to expedite equality comparisons on large scale where
        /// the hash is stored and used as the key for further comparison among
        /// a series.  Only in cases where the hash is equal to the stored hash
        /// is equality checked and identity differentiation discovered.</remarks>
        public override int GetHashCode()
        {
            int hc = 0;
            if (this.custom != null)
            {
                for (int i = 0; i < this.custom.Length; i++)
                    if (i % 6 == 0)
                        hc |= this.custom[i].GetHashCode();
                    else if (i % 5 == 0)
                        hc &= this.custom[i].GetHashCode();
                    else if (i % 4 == 0)
                        hc += this.custom[i].GetHashCode();
                    else if (i % 3 == 0)
                        hc -= this.custom[i].GetHashCode();
                    else
                        hc ^= this.custom[i].GetHashCode();
            }
            return (hc |= (int)((int)((ulong)this.binaryOps) / 48 ^ (int)this.expansionsRequired << 16)) ^ (int)this.invocations << 8 ^ (int)this.primitives << 4 ^ (int)this.specialFuncs ^ (int)this.symbols ^ (int)this.unaryOps;
        }

        #region IEquatable<ExpressionKind> Members

        /// <summary>
        /// Determines whether the <paramref name="other"/> 
        /// <see cref="ExpressionKind"/> is equal to the current 
        /// <see cref="ExpressionKind"/>.
        /// </summary>
        /// <param name="other">The <see cref="ExpressionKind"/> to check 
        /// equality against.</param>
        /// <returns>true if the <paramref name="other"/> is data-wise 
        /// equal to the current <see cref="ExpressionKind"/>; false, 
        /// otherwise.</returns>
        public bool Equals(ExpressionKind other)
        {
            if (other.binaryOps != this.binaryOps ||
                other.expansionsRequired != this.expansionsRequired ||
                other.invocations != this.invocations ||
                other.primitives != this.primitives ||
                other.referenceKinds != this.referenceKinds ||
                other.specialFuncs != this.specialFuncs ||
                other.symbols != this.symbols ||
                other.unaryOps != this.unaryOps) 
                return false;
            if ((this.custom == null || this.custom.Length == 0) && (other.custom == null || other.custom.Length == 0))
                return true;
            if (other.custom == null && (this.custom != null && this.custom.Length > 0))
                return false;
            if (this.custom == null && (other.custom != null && other.custom.Length > 0))
                return false;
            if (other.custom.Length != this.custom.Length)
                return false;
            var otherGuid = other.custom;
            /* *
             * Sometimes it's best to avoid the easy route.
             * *
             * In this case CS1673 would result in just
             * using predicates and LINQ
             * */
            for (int i = 0; i < this.custom.Length; i++)
            {
                Guid iCurrent = this.custom[i];
                bool encountered = false;
                for (int j = 0; j < otherGuid.Length; j++)
                    if (iCurrent == otherGuid[j])
                    {
                        encountered = true;
                        break;
                    }
                if (!encountered)
                    return false;
            }
            return true;
        }


        #endregion

        /// <summary>
        /// Creates a new <see cref="ExpressionKind"/> with the
        /// <paramref name="binaryOps"/>, <paramref name="expansionsRequired"/>,
        /// <paramref name="invocations"/>, <paramref name="primitiveInserts"/>,
        /// <paramref name="referenceKinds"/>, <paramref name="specialFuncs"/>,
        /// <paramref name="symbols"/> and <paramref name="unaryOperations"/>
        /// provided.
        /// </summary>
        /// <param name="binaryOps">The <see cref="BinaryOperationSector"/>
        /// associated to the <see cref="ExpressionKind"/>.</param>
        /// <param name="expansionsRequired">The 
        /// <see cref="ExpansionRequiredSector"/>
        /// associated to the <see cref="ExpressionKind"/>.</param>
        /// <param name="invocations">The 
        /// <see cref="InvocationSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="primitiveInserts">The 
        /// <see cref="PrimitiveInsertSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="referenceKinds">The 
        /// <see cref="ReferenceSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="specialFuncs">The 
        /// <see cref="SpecialFunctionSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="symbols">The 
        /// <see cref="SymbolSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="unaryOperations">The 
        /// <see cref="UnaryOperationSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        public ExpressionKind(BinaryOperationSector binaryOps, ExpansionRequiredSector expansionsRequired, InvocationSector invocations, PrimitiveInsertSector primitiveInserts, ReferenceSector referenceKinds, SpecialFunctionSector specialFuncs, SymbolSector symbols, UnaryOperationSector unaryOperations)
        {
            this.binaryOps = binaryOps;
            this.expansionsRequired = expansionsRequired;
            this.invocations = invocations;
            this.primitives = primitiveInserts;
            this.referenceKinds = referenceKinds;
            this.specialFuncs = specialFuncs;
            this.symbols = symbols;
            this.unaryOps = unaryOperations;
            this.custom = null;
        }

        /// <summary>
        /// Creates a new <see cref="ExpressionKind"/> with the
        /// <paramref name="binaryOps"/>, <paramref name="expansionsRequired"/>,
        /// <paramref name="invocations"/>, <paramref name="primitiveInserts"/>,
        /// <paramref name="referenceKinds"/>, <paramref name="specialFuncs"/>,
        /// <paramref name="symbols"/>, <paramref name="unaryOperations"/>
        /// and <paramref name="custom"/> expression kinds provided.
        /// </summary>
        /// <param name="binaryOps">The <see cref="BinaryOperationSector"/>
        /// associated to the <see cref="ExpressionKind"/>.</param>
        /// <param name="expansionsRequired">The 
        /// <see cref="ExpansionRequiredSector"/>
        /// associated to the <see cref="ExpressionKind"/>.</param>
        /// <param name="invocations">The 
        /// <see cref="InvocationSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="primitiveInserts">The 
        /// <see cref="PrimitiveInsertSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="referenceKinds">The 
        /// <see cref="ReferenceSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="specialFuncs">The 
        /// <see cref="SpecialFunctionSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="symbols">The 
        /// <see cref="SymbolSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="unaryOperations">The 
        /// <see cref="UnaryOperationSector"/> associated to the 
        /// <see cref="ExpressionKind"/>.</param>
        /// <param name="custom">The array of <see cref="Guid"/> values
        /// which denote the custom expression kinds represented by
        /// the current <see cref="ExpressionKind"/>.</param>
        public ExpressionKind(BinaryOperationSector binaryOps, ExpansionRequiredSector expansionsRequired, InvocationSector invocations, PrimitiveInsertSector primitiveInserts, ReferenceSector referenceKinds, SpecialFunctionSector specialFuncs, SymbolSector symbols, UnaryOperationSector unaryOperations, params Guid[] custom)
            : this(binaryOps, expansionsRequired, invocations, primitiveInserts, referenceKinds, specialFuncs, symbols, unaryOperations)
        {
            if (custom == null)
                return;
            this.custom = custom;
        }

        public ExpressionKind(BinaryOperationSector value)
            : this(value,
                   ExpansionRequiredSector.None,
                   InvocationSector.None,
                   PrimitiveInsertSector.None,
                   ReferenceSector.None,
                   SpecialFunctionSector.None,
                   SymbolSector.None,
                   UnaryOperationSector.None)
        {

        }
        public ExpressionKind(ExpansionRequiredSector value)
            : this(BinaryOperationSector.None,
                   value,
                   InvocationSector.None,
                   PrimitiveInsertSector.None,
                   ReferenceSector.None,
                   SpecialFunctionSector.None,
                   SymbolSector.None,
                   UnaryOperationSector.None)
        {

        }
        public ExpressionKind(InvocationSector value)
            : this(BinaryOperationSector.None,
                   ExpansionRequiredSector.None,
                   value,
                   PrimitiveInsertSector.None,
                   ReferenceSector.None,
                   SpecialFunctionSector.None,
                   SymbolSector.None,
                   UnaryOperationSector.None)
        {

        }
        public ExpressionKind(PrimitiveInsertSector value)
            : this(BinaryOperationSector.None,
                   ExpansionRequiredSector.None,
                   InvocationSector.None,
                   value,
                   ReferenceSector.None,
                   SpecialFunctionSector.None,
                   SymbolSector.None,
                   UnaryOperationSector.None)
        {

        }
        public ExpressionKind(ReferenceSector value)
            : this(BinaryOperationSector.None,
                   ExpansionRequiredSector.None,
                   InvocationSector.None,
                   PrimitiveInsertSector.None,
                   value,
                   SpecialFunctionSector.None,
                   SymbolSector.None,
                   UnaryOperationSector.None)
        {

        }
        public ExpressionKind(SpecialFunctionSector value)
            : this(BinaryOperationSector.None,
                   ExpansionRequiredSector.None,
                   InvocationSector.None,
                   PrimitiveInsertSector.None,
                   ReferenceSector.None,
                   value,
                   SymbolSector.None,
                   UnaryOperationSector.None)
        {

        }
        public ExpressionKind(SymbolSector value)
            : this(BinaryOperationSector.None,
                   ExpansionRequiredSector.None,
                   InvocationSector.None,
                   PrimitiveInsertSector.None,
                   ReferenceSector.None,
                   SpecialFunctionSector.None,
                   value,
                   UnaryOperationSector.None)
        {

        }
        public ExpressionKind(UnaryOperationSector value)
            : this(BinaryOperationSector.None,
                   ExpansionRequiredSector.None,
                   InvocationSector.None,
                   PrimitiveInsertSector.None,
                   ReferenceSector.None,
                   SpecialFunctionSector.None,
                   SymbolSector.None,
                   value)
        {

        }


        public ExpressionKind(params Guid[] custom)
            : this(BinaryOperationSector.None, ExpansionRequiredSector.None, InvocationSector.None, PrimitiveInsertSector.None, ReferenceSector.None, SpecialFunctionSector.None, SymbolSector.None, UnaryOperationSector.None)
        {
            if (custom == null)
                throw new ArgumentNullException("custom");
            this.custom = custom.Distinct().ToArray();
        }

        /// <summary>
        /// Returns the <see cref="ActiveSectorFlags"/> which denote the
        /// active portions of the <see cref="ExpressionKind"/> which
        /// contain data about what kind of expression or series
        /// of expressions are represented by the current instance.
        /// </summary>
        public ActiveSectorFlags ActiveSectors
        {
            get
            {
                ActiveSectorFlags activeSectors = ActiveSectorFlags.None;
                if (this.custom != null && this.custom.Length > 0)
                    activeSectors = ActiveSectorFlags.Custom;
                if (this.binaryOps != BinaryOperationSector.None)
                    activeSectors |= ActiveSectorFlags.BinaryOperationExpression;
                if (this.expansionsRequired != ExpansionRequiredSector.None)
                    activeSectors |= ActiveSectorFlags.ExpansionRequiredExpression;
                if (this.invocations != InvocationSector.None)
                    activeSectors |= ActiveSectorFlags.InvocationExpression;
                if (this.primitives != PrimitiveInsertSector.None)
                    activeSectors |= ActiveSectorFlags.PrimitiveExpression;
                if (this.referenceKinds != ReferenceSector.None)
                    activeSectors |= ActiveSectorFlags.ReferenceExpression;
                if (this.specialFuncs != SpecialFunctionSector.None)
                    activeSectors |= ActiveSectorFlags.SpecialFunctionExpression;
                if (this.symbols != SymbolSector.None)
                    activeSectors |= ActiveSectorFlags.SymbolExpression;
                if (this.unaryOps != UnaryOperationSector.None)
                    activeSectors |= ActiveSectorFlags.UnaryOperationExpression;
                return activeSectors;
            }
        }

        /// <summary>
        /// Implicitly casts a <see cref="BinaryOperationSector"/> value to an <see cref="ExpressionKind"/>
        /// instance.
        /// </summary>
        /// <param name="value">The <see cref="BinaryOperationSector"/> values which indicate
        /// which expression kinds are represented by the <see cref="ExpressionKind"/> 
        /// value.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the
        /// <see cref="ActiveSectorFlags.BinaryOperationExpression"/> set.</returns>
        public static implicit operator ExpressionKind(BinaryOperationSector value)
        {
            return new ExpressionKind(value);
        }
        /// <summary>
        /// Implicitly casts a <see cref="ExpansionRequiredSector"/> value to an <see cref="ExpressionKind"/>
        /// instance.
        /// </summary>
        /// <param name="value">The <see cref="ExpansionRequiredSector"/> values which indicate
        /// which expression kinds are represented by the <see cref="ExpressionKind"/> 
        /// value.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the
        /// <see cref="ActiveSectorFlags.ExpansionRequiredExpression"/> set.</returns>
        public static implicit operator ExpressionKind(ExpansionRequiredSector value)
        {
            return new ExpressionKind(value);
        }
        /// <summary>
        /// Implicitly casts a <see cref="InvocationSector"/> value to an <see cref="ExpressionKind"/>
        /// instance.
        /// </summary>
        /// <param name="value">The <see cref="InvocationSector"/> values which indicate
        /// which expression kinds are represented by the <see cref="ExpressionKind"/> 
        /// value.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the
        /// <see cref="ActiveSectorFlags.InvocationExpression"/> set.</returns>
        public static implicit operator ExpressionKind(InvocationSector value)
        {
            return new ExpressionKind(value);
        }
        /// <summary>
        /// Implicitly casts a <see cref="PrimitiveInsertSector"/> value to an <see cref="ExpressionKind"/>
        /// instance.
        /// </summary>
        /// <param name="value">The <see cref="PrimitiveInsertSector"/> values which indicate
        /// which expression kinds are represented by the <see cref="ExpressionKind"/> 
        /// value.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the
        /// <see cref="ActiveSectorFlags.PrimitiveExpression"/> set.</returns>
        public static implicit operator ExpressionKind(PrimitiveInsertSector value)
        {
            return new ExpressionKind(value);
        }
        /// <summary>
        /// Implicitly casts a <see cref="ReferenceSector"/> value to an <see cref="ExpressionKind"/>
        /// instance.
        /// </summary>
        /// <param name="value">The <see cref="ReferenceSector"/> values which indicate
        /// which expression kinds are represented by the <see cref="ExpressionKind"/> 
        /// value.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the
        /// <see cref="ActiveSectorFlags.ReferenceExpression"/> set.</returns>
        public static implicit operator ExpressionKind(ReferenceSector value)
        {
            return new ExpressionKind(value);
        }
        /// <summary>
        /// Implicitly casts a <see cref="SpecialFunctionSector"/> value to an <see cref="ExpressionKind"/>
        /// instance.
        /// </summary>
        /// <param name="value">The <see cref="SpecialFunctionSector"/> values which indicate
        /// which expression kinds are represented by the <see cref="ExpressionKind"/> 
        /// value.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the
        /// <see cref="ActiveSectorFlags.SpecialFunctionExpression"/> set.</returns>
        public static implicit operator ExpressionKind(SpecialFunctionSector value)
        {
            return new ExpressionKind(value);
        }
        /// <summary>
        /// Implicitly casts a <see cref="SymbolSector"/> value to an <see cref="ExpressionKind"/>
        /// instance.
        /// </summary>
        /// <param name="value">The <see cref="SymbolSector"/> values which indicate
        /// which expression kinds are represented by the <see cref="ExpressionKind"/> 
        /// value.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the
        /// <see cref="ActiveSectorFlags.SymbolExpression"/> set.</returns>
        public static implicit operator ExpressionKind(SymbolSector value)
        {
            return new ExpressionKind(value);
        }
        /// <summary>
        /// Implicitly casts a <see cref="UnaryOperationSector"/> value to an <see cref="ExpressionKind"/>
        /// instance.
        /// </summary>
        /// <param name="value">The <see cref="UnaryOperationSector"/> values which indicate
        /// which expression kinds are represented by the <see cref="ExpressionKind"/> 
        /// value.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the
        /// <see cref="ActiveSectorFlags.UnaryOperationExpression"/> set.</returns>
        public static implicit operator ExpressionKind(UnaryOperationSector value)
        {
            return new ExpressionKind(value);
        }

        /// <summary>
        /// Implicitly casts an array of <see cref="Guid"/> instances
        /// to an <see cref="ExpressionKind"/>.
        /// </summary>
        /// <param name="custom">The array of <see cref="Guid"/> instances
        /// which represent the custom expression kind flag set.</param>
        /// <returns>An instance of <see cref="ExpressionKind"/> with the
        /// <see cref="ActiveSectorFlags.Custom"/> set, and the 
        /// <paramref name="custom"/> expression guids contained.</returns>
        public static implicit operator ExpressionKind(Guid[] custom)
        {
            return new ExpressionKind(custom);
        }

        /// <summary>
        /// Implicitly casts a <see cref="Guid"/> to an <see cref="ExpressionKind"/>.
        /// </summary>
        /// <param name="custom">The <see cref="Guid"/> which represents
        /// the custom expression.</param>
        /// <returns>An instance of <see cref="ExpressionKind"/> with the
        /// <see cref="ActiveSectorFlags.Custom"/> set and the
        /// expression guid contained.</returns>
        public static implicit operator ExpressionKind(Guid custom)
        {
            return new ExpressionKind(custom);
        }

        /// <summary>
        /// Adds an expression kind value with another custom expression kind's
        /// guid.
        /// </summary>
        /// <param name="a">The <see cref="ExpressionKind"/> to base the value off of.</param>
        /// <param name="b">The <see cref="Guid"/> to add to the <see cref="ExpressionKind"/>.</param>
        /// <returns>A new <see cref="ExpressionKind"/> which contains the flags from <paramref name="a"/>
        /// with the custom <see cref="Guid"/> <paramref name="b"/> expression kind.</returns>
        public static ExpressionKind operator +(ExpressionKind a, Guid b)
        {
            if (a.custom != null && a.custom.Contains(b))
                return a;
            else
            {
                Guid[] series = new Guid[a.custom == null ? 1 : a.custom.Length + 1];
                if (a.custom != null)
                    for (int i = 0; i < a.custom.Length; i++)
                        series[i] = a.custom[i];
                series[series.Length - 1] = b;
                return new ExpressionKind(a.binaryOps, a.expansionsRequired, a.invocations, a.primitives, a.referenceKinds, a.specialFuncs, a.symbols, a.unaryOps, series);
            }
        }
        /// <summary>
        /// Removes an expression kind's globally unique identifier (<see cref="Guid"/>) from the
        /// group <see cref="ExpressionKind"/>.
        /// </summary>
        /// <param name="a">The <see cref="ExpressionKind"/> which contains 
        /// the <see cref="Guid"/>, <paramref name="b"/>, to have it removed.</param>
        /// <param name="b">The <see cref="Guid"/> of the custom <see cref="ExpressionKind"/>
        /// to remove</param>
        /// <returns></returns>
        public static ExpressionKind operator -(ExpressionKind a, Guid b)
        {
            if (a.custom == null || !a.custom.Contains(b))
                throw new ArgumentException("a");
            Guid[] series = new Guid[a.custom.Length - 1];
            if (series.Length == 0)
                return new ExpressionKind(a.binaryOps, a.expansionsRequired, a.invocations, a.primitives, a.referenceKinds, a.specialFuncs, a.symbols, a.unaryOps);
            /* *
             * Iterate and copy the series over to the new set;
             * if on the removed member, don't increment j.
             * */
            for (int i = 0, j = 0; i < a.custom.Length; i++)
                if (a.custom[i] != b)
                    series[j++] = a.custom[i];
            return new ExpressionKind(a.binaryOps, a.expansionsRequired, a.invocations, a.primitives, a.referenceKinds, a.specialFuncs, a.symbols, a.unaryOps, series);
        }
        /* *
         * Performs a merge set where only the elements
         * that overlap are included.
         * */
        internal static Guid[] MergeSetsAnd(Guid[] a, Guid[] b)
        {
            if (a == null || b == null)
                return null;
            int fullCount = 0;
            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < b.Length; j++)
                    if (a[i] == b[j])
                    {
                        fullCount++;
                        break;
                    }
            if (fullCount == 0)
                return null;
            Guid[] result = new Guid[fullCount];
            for (int i = 0, j = 0; i < a.Length; i++)
                for (int k = 0; k < b.Length; k++)
                    if (a[i] == b[k])
                    {
                        result[j++] = a[i];
                        break;
                    }
            return result;
        }

        /* *
         * Performs a merge set where only the elements that don't
         * overlap are included.
         * */
        internal static Guid[] MergeSetsXOr(Guid[] a, Guid[] b)
        {
            if (a == null)
                return b;
            else if (b == null)
                return a;
            int fullCount = b.Length;
            /* *
             * Iterate through the first set; each iteration through
             * if an element in b exists in a, reduce the count.
             * If the element exists in one, but not the other, increase 
             * the count, since the merge works by adding only the elements
             * that don't overlap.
             * */
            for (int i = 0; i < a.Length; i++)
            {
                bool contained = false;
                for (int j = 0; j < b.Length; j++)
                {
                    if (a[i] == b[j])
                    {
                        fullCount--;
                        contained = true;
                        break;
                    }
                }
                if (!contained)
                    fullCount++;
            }
            Guid[] result = new Guid[fullCount];
            int u = 0;
            /* *
             * Scan through both sets, inserting only the elements
             * which don't overlap.
             * */
            for (int i = 0; i < b.Length; i++)
            {
                bool contained=false;
                for (int k = 0; k < a.Length; k++)
                {
                    if (a[k] == b[i])
                    {
                        contained = true;
                        break;
                    }
                }
                if (!contained)
                    result[u++] = b[i];
            }
            for (int i = 0; i < a.Length; i++)
            {
                bool contained = false;
                for (int k = 0; k < b.Length; k++)
                    if (a[i] == b[k])
                    {
                        contained = true;
                        break;
                    }
                if (!contained)
                    result[u++] = a[i];
            }
            return result;
        }

        /* *
         * Performs a merge set where all distinct elements from both
         * are included.
         * */
        internal static Guid[] MergeSetsOr(Guid[] a, Guid[] b)
        {
            if (a == null)
                return b;
            else if (b == null)
                return a;
            int fullCount = a.Length + b.Length;
            /* *
             * Fairly straight forward, iterate through, subtract
             * one for every duplicate.
             * */
            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < b.Length; j++)
                    if (a[i] == b[j])
                    {
                        fullCount--;
                        break;
                    }
            if (fullCount == 0)
                return null;
            Guid[] result = new Guid[fullCount];
            //Add all of a's elements.
            for (int i = 0; i < a.Length; i++)
                result[i] = a[i];
            /* *
             * Add all but duplicates
             * */
            for (int i = 0, j = 0; i < b.Length; i++)
            {
                bool contained = false;
                for (int k = 0; k < a.Length; k++)
                    if (a[k] == b[i])
                    {
                        contained = true;
                        break;
                    }
                if (contained)
                    continue;
                result[a.Length + j++] = b[i];
            }
            return result;
        }

        /// <summary>
        /// Returns a merged set of the two <see cref="ExpressionKind"/> instances.
        /// </summary>
        /// <param name="a">The left-side of the or-operation.</param>
        /// <param name="b">The right-side of the or-operation.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the flags and custom 
        /// expression guids of both <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static ExpressionKind operator |(ExpressionKind a, ExpressionKind b)
        {
            return new ExpressionKind(a.binaryOps | b.binaryOps, a.expansionsRequired | b.expansionsRequired, a.invocations | b.invocations, a.primitives | b.primitives, a.referenceKinds | b.referenceKinds, a.specialFuncs | b.specialFuncs, a.symbols | b.symbols, a.unaryOps | b.unaryOps, MergeSetsOr(a.custom, b.custom));
        }

        /// <summary>
        /// Returns an exclusively merged set of the two <see cref="ExpressionKind"/> instances.
        /// </summary>
        /// <param name="a">The left-side of the exclusive or-operation.</param>
        /// <param name="b">The right-side of the exclusive or-operation.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the exclusive flags and 
        /// custom expression guids of both <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static ExpressionKind operator ^(ExpressionKind a, ExpressionKind b)
        {
            return new ExpressionKind(a.binaryOps ^ b.binaryOps, a.expansionsRequired ^ b.expansionsRequired, a.invocations ^ b.invocations, a.primitives ^ b.primitives, a.referenceKinds ^ b.referenceKinds, a.specialFuncs ^ b.specialFuncs, a.symbols ^ b.symbols, a.unaryOps ^ b.unaryOps, MergeSetsXOr(a.custom, b.custom));
        }

        /// <summary>
        /// Returns an merged set of the two <see cref="ExpressionKind"/> instances where
        /// their values overlap.
        /// </summary>
        /// <param name="a">The left-side of the and-operation.</param>
        /// <param name="b">The right-side of the and-operation.</param>
        /// <returns>A new <see cref="ExpressionKind"/> with the overlapping flags
        /// and custom expression guids of both <paramref name="a"/> and 
        /// <paramref name="b"/>.</returns>
        public static ExpressionKind operator &(ExpressionKind a, ExpressionKind b)
        {
            return new ExpressionKind(a.binaryOps & b.binaryOps, a.expansionsRequired & b.expansionsRequired, a.invocations & b.invocations, a.primitives & b.primitives, a.referenceKinds & b.referenceKinds, a.specialFuncs & b.specialFuncs, a.symbols & b.symbols, a.unaryOps & b.unaryOps, MergeSetsAnd(a.custom, b.custom));
        }

        /// <summary>
        /// Returns whether the <see cref="ExpressionKind"/> values <paramref name="a"/>
        /// and <paramref name="b"/> match.
        /// </summary>
        /// <param name="a">The left-side of the equals operation.</param>
        /// <param name="b">The right-side of the equals operation.</param>
        /// <returns>true if <paramref name="a"/> equals <paramref name="b"/>; false, otherwise.</returns>
        public static bool operator ==(ExpressionKind a, ExpressionKind b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Returns whether the <see cref="ExpressionKind"/> values <paramref name="a"/>
        /// and <paramref name="b"/> match.
        /// </summary>
        /// <param name="a">The left-side of the equals operation.</param>
        /// <param name="b">The right-side of the equals operation.</param>
        /// <returns>true if <paramref name="a"/> does not equal <paramref name="b"/>; false, otherwise.</returns>
        public static bool operator !=(ExpressionKind a, ExpressionKind b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> instance of the 
        /// <see cref="Guid"/> values which the <see cref="ExpressionKind"/> 
        /// relates to.
        /// </summary>
        /// <remarks>Custom <see cref="Guid"/> values are used to identify custom 
        /// expression kinds transformer and translator implementations support.</remarks>
        public IEnumerable<Guid> CustomExpressionKinds
        {
            get
            {
                if (this.custom == null)
                    yield break;
                for (int i = 0; i < this.custom.Length; i++)
                    yield return this.custom[i];
                yield break;
            }
        }

        /// <summary>
        /// Provides relevant information to what areas of the <see cref="ExpressionKind"/>
        /// are active.
        /// </summary>
        [Flags]
        public enum ActiveSectorFlags
        {
            /// <summary>
            /// No sectors of the expression kind are initialized;
            /// this is part of the default state, the ExpressionKind 
            /// represents no expression type.
            /// </summary>
            None                                = 0x0000000000000000,
            /// <summary>
            /// The expression kind contains information about innately supported
            /// binary operation expressions.
            /// </summary>
            BinaryOperationExpression           = 0x0000000000000001,
            /// <summary>
            /// The expression kind contains information about innately supported
            /// expansion required expressions.
            /// </summary>
            ExpansionRequiredExpression         = 0x0000000000000002,
            /// <summary>
            /// The expression kind contains information about innately 
            /// supported invocation expressions.
            /// </summary>
            InvocationExpression                = 0x0000000000000004,
            /// <summary>
            /// The expression kind contains information about innately 
            /// supported primitive expressions.
            /// </summary>
            PrimitiveExpression                 = 0x0000000000000008,
            /// <summary>
            /// The expression kind contains information about innately 
            /// supported reference expressions.
            /// </summary>
            ReferenceExpression                 = 0x0000000000000010,
            /// <summary>
            /// The expression kind contains information about innately 
            /// supported special function expressions.
            /// </summary>
            SpecialFunctionExpression           = 0x0000000000000020,
            /// <summary>
            /// The expression kind contains information about innately 
            /// supported symbol expressions.
            /// </summary>
            SymbolExpression                    = 0x0000000000000040,
            /// <summary>
            /// The expression kind contains information about innately
            /// supported unary operation expressions.
            /// </summary>
            UnaryOperationExpression            = 0x0000000000000080,
            /// <summary>
            /// The expression kind contains custom expression kind 
            /// information.
            /// </summary>
            Custom                              = 0x0000000000000100,
            /// <summary>
            /// The expression kind contains information in all sectors.
            /// </summary>
            All                                 = 0x00000000000001FF,
        }
        /// <summary>
        /// The kinds of binary operations possible.
        /// </summary>
        [Flags]
        public enum BinaryOperationSector :
            long
        {
            None                                = 0x0000000000000000,
            /// <summary>
            /// Indicates that an expression is a binary operation expression and
            /// that it is a simple forward to an expression of lower-precedence.
            /// </summary>
            BinaryForwardTerm                   = 0x0000000000000001,
            /// <summary>
            /// The expression is an assignment operation.
            /// </summary>
            AssignExpression                    = 0x0000000000000002,
            /// <summary>
            /// The expression is an assignment operation where the 
            /// source value is multiplied by the target value and
            /// stored back to the source.
            /// </summary>
            AssignMultiplyOperation             = 0x0000000000000004,
            /// <summary>
            /// The expression is an assignment operation where the 
            /// source value is divided by the target value and
            /// stored back to the source.
            /// </summary>
            AssignDivideOperation               = 0x0000000000000008,
            /// <summary>
            /// The expression is an assignment operation where the 
            /// source value is divided by the target value and
            /// the remainder is stored back to the source.
            /// </summary>
            AssignModulusOperation              = 0x0000000000000010,
            /// <summary>
            /// The expression is an assignment operation where the 
            /// target value is added to the source value and
            /// stored into the source.
            /// </summary>
            AssignAddOperation                  = 0x0000000000000020,
            /// <summary>
            /// The expression is an assignment operation where the 
            /// target value is subtracted from the source value and
            /// stored into the source.
            /// </summary>
            AssignSubtractOperation             = 0x0000000000000040,
            /// <summary>
            /// The expression is an assignment operation where the
            /// source value is left shifted by the target number of bits
            /// and stored in the source.
            /// </summary>
            AssignLeftShiftOperation            = 0x0000000000000080,
            /// <summary>
            /// The expression is an assignment operation where the
            /// source value is right shifted by the target number of bits
            /// and stored in the source.
            /// </summary>
            AssignRightShiftOperation           = 0x0000000000000100,
            /// <summary>
            /// The expression is an assignment operation where the
            /// bits from the source and target value, on overlap,
            /// are stored in the source.
            /// </summary>
            AssignBitwiseAndOperation           = 0x0000000000000200,
            /// <summary>
            /// The expression is an assignment operation where the
            /// bits from the source and target value,  are stored 
            /// in the source.
            /// </summary>
            AssignBitwiseOrOperation            = 0x0000000000000400,
            /// <summary>
            /// The expression is an assignment operation where the
            /// exclusive bits from the source and target value
            /// are stored in the source.
            /// </summary>
            AssignBitwiseExclusiveOrOperation   = 0x0000000000000800,
            /// <summary>
            /// The expression is a logical or operation.
            /// </summary>
            LogicalOrOperation                  = 0x0000000000001000,
            /// <summary>
            /// The expression is a logical and operation.
            /// </summary>
            LogicalAndOperation                 = 0x0000000000002000,
            /// <summary>
            /// The expression is a bitwise or operation.
            /// </summary>
            BitwiseOrOperation                  = 0x0000000000004000,
            /// <summary>
            /// The expression is a bitwise exclusive or operation.
            /// </summary>
            BitwiseExclusiveOrOperation         = 0x0000000000008000,
            /// <summary>
            /// The expression is a bitwise and operation.
            /// </summary>
            BitwiseAndOperation                 = 0x0000000000010000,
            /// <summary>
            /// The expression is a inequality check operation.
            /// </summary>
            InequalityOperation                 = 0x0000000000020000,
            /// <summary>
            /// The expression is an equality operation which checks
            /// two operands to one another.
            /// </summary>
            EqualityOperation                   = 0x0000000000040000,
            /// <summary>
            /// The expression is a less than operation which checks
            /// two operands to one another.
            /// </summary>
            LessThanOperation                   = 0x0000000000080000,
            /// <summary>
            /// The expression is a less than or equal to operation.
            /// </summary>
            LessThanOrEqualToOperation          = 0x0000000000100000,
            /// <summary>
            /// The expression is a greater than operation.
            /// </summary>
            GreaterThanOperation                = 0x0000000000200000,
            /// <summary>
            /// The expression is a greater than or equal to operation.
            /// </summary>
            GreaterThanOrEqualToOperation       = 0x0000000000400000,
            /// <summary>
            /// The expression is a type-check operation yielding a 
            /// boolean value of true if the type of the enclosing 
            /// expression matches the type specified; false, otherwise.
            /// </summary>
            TypeCheckOperation                  = 0x0000000000800000,
            /// <summary>
            /// The expression is a type-cast operation, where failure
            /// yields a null value.
            /// </summary>
            TypeCastOrNull                      = 0x0000000001000000,
            /// <summary>
            /// The expression is a shift left operation.
            /// </summary>
            ShiftLeftOperation                  = 0x0000000002000000,
            /// <summary>
            /// The expression is a shift right operation.
            /// </summary>
            ShiftRightOperation                 = 0x0000000004000000,
            /// <summary>
            /// The expression is an addition operation.
            /// </summary>
            AddOperation                        = 0x0000000008000000,
            /// <summary>
            /// The expression is a subtraction operation.
            /// </summary>
            SubtractOperation                   = 0x0000000010000000,
            /// <summary>
            /// The expression is an multiplication operation.
            /// </summary>
            MultiplyOperation                   = 0x0000000020000000,
            /// <summary>
            /// The expression is a division operation.
            /// </summary>
            StrictDivisionOperation             = 0x0000000040000000,
            /// <summary>
            /// The expression is a modulus operation.
            /// </summary>
            ModulusOperation                    = 0x0000000080000000,
            /// <summary>
            /// The expression is a flexible division operation which casts the operands
            /// to a 64-bit floating point value as necessary.
            /// </summary>
            FlexibleDivisionOperation           = 0x0000000100000000,
            /// <summary>
            /// The division ignores the native type of the operands and performs 
            /// an integer-based division.
            /// </summary>
            IntegerDivisionOperation            = 0x0000000200000000,
            /// <summary>
            /// Represents all binary operator expressions.
            /// </summary>
            All                                 = 0x00000003FFFFFFFF,
        }
        /// <summary>
        /// The kinds of unary operations possible.
        /// </summary>
        [Flags]
        public enum UnaryOperationSector
        {
            None                                = 0x0000000000000000,
            /// <summary>
            /// The unary operation is not present and thus
            /// acts as a forward to the unary operator's term.
            /// </summary>
            UnaryForwardTerm                    = 0x0000000000000001,
            /// <summary>
            /// The unary operation pre-increments the value specified and returns
            /// that value to the next containing expression.
            /// </summary>
            UnaryPreincrement                   = 0x0000000000000002,
            /// <summary>
            /// The unary operation caches the original target value and increments
            /// the target, returning the cached value.
            /// </summary>
            UnaryPostincrement                  = 0x0000000000000004,
            /// <summary>
            /// The unary operation pre-decrements the value specified and returns
            /// that value to the next containing expression.
            /// </summary>
            UnaryPredecrement                   = 0x0000000000000008,
            /// <summary>
            /// The unary operation caches the original target value and decrements
            /// the target, returning the cached value.
            /// </summary>
            UnaryPostdecrement                  = 0x0000000000000010,
            /// <summary>
            /// The unary operation inverts the boolean target value or invokes
            /// its unary logical not operator overload.
            /// </summary>
            /// <remarks>The underlying associated overload name is op_LogicalNot.</remarks>
            UnaryBooleanInversion               = 0x0000000000000020,
            /// <summary>
            /// The unary operation returns the inverted bits of the
            /// target numeric value.
            /// </summary>
            UnaryBitwiseInversion               = 0x0000000000000040,
            /// <summary>
            /// The expression is a unary sign inversion operation.
            /// </summary>
            UnarySignInversionOperation         = 0x0000000000000080,
            /// <summary>
            /// Represents all unary operator expressions.
            /// </summary>
            All                                 = 0x00000000000000FF,
        }
        /// <summary>
        /// The kinds of references possible.
        /// </summary>
        [Flags]
        public enum ReferenceSector
        {
            None = 0x0000000000000000,
            /// <summary>
            /// The expression is a reference to a local variable.
            /// </summary>
            LocalReference = 0x0000000000000001,
            /// <summary>
            /// The expression is a reference to an event.
            /// </summary>
            EventReference = 0x0000000000000002,
            /// <summary>
            /// The expression is a type-reference.
            /// </summary>
            TypeReference = 0x0000000000000004,
            /// <summary>
            /// The expression is a method reference expression.
            /// </summary>
            MethodReference = 0x0000000000000008,
            /// <summary>
            /// The expressioin is a property reference.
            /// </summary>
            PropertyReference = 0x0000000000000010,
            /// <summary>
            /// Indicates that an expression is a this reference expression
            /// and method references by default are virtual, if applicable.
            /// </summary>
            ThisReference = 0x0000000000000020,
            /// <summary>
            /// Indicates that an expression is a base reference expression
            /// and method references by default are never virtual and 
            /// refer to the base-type of the type provided.
            /// </summary>
            BaseReference = 0x0000000000000040,
            /// <summary>
            /// Indicates that an expression is a self reference expression
            /// and method references by default are never virtual.
            /// </summary>
            /// <remarks>'self' for 'selfish' as in it prioritizes 
            /// its own members over the those from the inheritance 
            /// hierarchy disregarding higher order overrides.</remarks>
            SelfReference = 0x0000000000000080,
            /// <summary>
            /// The expression is a field reference.
            /// </summary>
            FieldReference = 0x0000000000000100,
            /// <summary>
            /// The expression is an indexer reference.
            /// </summary>
            IndexerReference = 0x0000000000000200,
            /// <summary>
            /// The expression refers to the current type of the active
            /// instance.
            /// </summary>
            CurrentTypeReference = 0x0000000000000400,
            /// <summary>
            /// The expression wraps a sub-expression and denotes the
            /// named parameter 
            /// </summary>
            NamedParameterReference = 0x0000000000000800,
            /// <summary>
            /// The expression references a method parameter.
            /// </summary>
            ParameterReference = 0x0000000000001000,
            /// <summary>
            /// The expression references a specific constructor.
            /// </summary>
            ConstructorReference = 0x0000000000002000,
            /// <summary>
            /// The expression represents a switch case
            /// label which acts as a forward to the constant
            /// expression the label represents.
            /// </summary>
            SwitchCaseLabel =      0x0000000000004000,
            /// <summary>
            /// Represents every kind of reference.
            /// </summary>
            All = 0x0000000000007FFF,
        }
        /// <summary>
        /// The kinds of primitive value inserts possible.
        /// </summary>
        [Flags]
        public enum PrimitiveInsertSector
        {
            /// <summary>
            /// No primitive value is represented by the expression kind.
            /// </summary>
            None                                = 0x0000000000000000,
            /// <summary>
            /// The expression is a primitive insert containing an <see cref="SByte"/>
            /// value.
            /// </summary>
            PrimitiveSByteInsert                = 0x0000000000000001,
            /// <summary>
            /// The expression is a primitive insert containing a 
            /// <see cref="Byte"/> value.
            /// </summary>
            PrimitiveByteInsert                 = 0x0000000000000002,
            /// <summary>
            /// The expression is a primitive insert containing a 
            /// <see cref="Boolean"/> value.
            /// </summary>
            PrimitiveBooleanInsert              = 0x0000000000000004,
            /// <summary>
            /// The expression is a primitive insert containing a 
            /// <see cref="Char"/> value.
            /// </summary>
            PrimitiveCharInsert                 = 0x0000000000000008,
            /// <summary>
            /// The expression is a primitive insert containing a <see cref="UInt16"/>
            /// value.
            /// </summary>
            PrimitiveUInt16Insert               = 0x0000000000000010,
            /// <summary>
            /// The expression is a primitive insert containing an <see cref="Int16"/>
            /// value.
            /// </summary>
            PrimitiveInt16Insert                = 0x0000000000000020,
            /// <summary>
            /// The expression is a primitive insert containing a <see cref="UInt32"/>
            /// value.
            /// </summary>
            PrimitiveUInt32Insert               = 0x0000000000000040,
            /// <summary>
            /// The expression is a primitive insert containing an <see cref="Int32"/>
            /// value.
            /// </summary>
            PrimitiveInt32Insert                = 0x0000000000000080,
            /// <summary>
            /// The expression is a primitive insert containing a <see cref="UInt64"/>
            /// value.
            /// </summary>
            PrimitiveUInt64Insert               = 0x0000000000000100,
            /// <summary>
            /// The expression is a primitive insert containing an <see cref="Int64"/>
            /// value.
            /// </summary>
            PrimitiveInt64Insert                = 0x0000000000000200,
            /// <summary>
            /// The expression is a primitive insert containing a <see cref="Single"/>
            /// value.
            /// </summary>
            PrimitiveSingleInsert               = 0x0000000000000400,
            /// <summary>
            /// The expression is a primitive insert containing a <see cref="Double"/>
            /// value.
            /// </summary>
            PrimitiveDoubleInsert               = 0x0000000000000800,
            /// <summary>
            /// The expression is a primitive insert containing a <see cref="Decimal"/>
            /// value.
            /// </summary>
            PrimitiveDecimalInsert              = 0x0000000000001000,
            /// <summary>
            /// The expression is a primitive insert containing a <see cref="String"/>
            /// value.
            /// </summary>
            PrimitiveStringInsert               = 0x0000000000002000,
            /// <summary>
            /// The expression is a primitive insert containing a null
            /// value.
            /// </summary>
            PrimitiveNullInsert                 = 0x0000000000004000,
            /// <summary>
            /// Represents all expressions which are primitive value 
            /// insertions.
            /// </summary>
            All                                 = 0x0000000000007FFF,
        }

        /// <summary>
        /// The kinds of special language functions possible.
        /// </summary>
        [Flags]
        public enum SpecialFunctionSector
        {
            None                                = 0x0000000000000000,
            /// <summary>
            /// The expression casts another expression to a given
            /// type.
            /// </summary>
            TypeCast                            = 0x0000000000000001,
            /// <summary>
            /// The expression casts another expression to a given type
            /// based upon which kind of type the expression is.
            /// </summary>
            VariadicTypeCast                    = 0x0000000000000002,
            /// <summary>
            /// The expression is a 'checked' expression, where overflows throw an error.
            /// </summary>
            /// <remarks>CIL implementation: conv.ovf.*, add.ovf and so on.</remarks>
            CheckedExpression                   = 0x0000000000000004,
            /// <summary>
            /// The expression is an 'unchecked' expression, where overflows are ignored.
            /// </summary>
            UncheckedExpression                 = 0x0000000000000008,
            /// <summary>
            /// An expression which references a specific type wherein the <see cref="RuntimeTypeHandle"/>
            /// is pushed onto the stack.
            /// </summary>
            /// <remarks><para>C&#9839;: typeof(type)</para>
            /// <para>VB: GetType(type)</para></remarks>
            TypeOfExpression                    = 0x0000000000000010,
            /// <summary>
            /// Represents all expressions which are a special langauge
            /// function.
            /// </summary>
            All                                 = 0x000000000000001F,
        }
        /// <summary>
        /// The kinds of invocations possible.
        /// </summary>
        [Flags]
        public enum InvocationSector
        {
            None                                = 0x0000000000000000,
            /// <summary>
            /// The expression is the act of firing/raising an event.
            /// </summary>
            EventFire                           = 0x0000000000000001,
            /// <summary>
            /// The expression is a method call.
            /// </summary>
            MethodCall                          = 0x0000000000000002,
            /// <summary>
            /// The expression is a call to a multicast delegate.
            /// </summary>
            MultiCastDelegateCall               = 0x0000000000000004,
            /// <summary>
            /// The expression invokes a constructor member, generally
            /// returning an instance to an object.
            /// </summary>
            ConstructorInvoke                   = 0x0000000000000008,
            /// <summary>
            /// Represents all expressions which involve invocation
            /// targets.
            /// </summary>
            All                                 = 0x000000000000000F,
        }

        /// <summary>
        /// The kinds of expressions which require further expansion
        /// or code generation to compile.
        /// </summary>
        [Flags]
        public enum ExpansionRequiredSector
        {
            None                                = 0x0000000000000000,
            /// <summary>
            /// The expression is a lambda expression.
            /// </summary>
            /// <remarks>
            ///     C&#9839;: (Identifier | '(' Identifier (',' Identifier)* ')' | '(' TypedIdentifier (',' TypedIdentifier)* ')') "=>"
            ///         (Expression | StatementBlock)
            ///     VB: "Function" '(' TypedIdentifier (',' TypedIdentifier) ')' Expression
            /// </remarks>
            LambdaExpression                    = 0x0000000000000001,
            /// <summary>
            /// The expression is a ternary conditional operation.
            /// </summary>
            ConditionalOperation                = 0x0000000000000002,
            /// <summary>
            /// An expression which is merely a forward from a conditional expression.
            /// </summary>
            ConditionalForwardTerm              = 0x0000000000000004,
            /// <summary>
            /// An series of expressions evaluated in verbatim order.
            /// </summary>
            CommaExpression                     = 0x0000000000000008,
            /// <summary>
            /// An expression which is a language integrated query used to
            /// manipulate and build new sequenes.
            /// </summary>
            LinqExpression                      = 0x0000000000000010,
            /// <summary>
            /// An expression which modifies a wrapped expression prior
            /// to being sent to the recipient of the wrapped expression.
            /// </summary>
            WorkspaceExpression                 = 0x0000000000000020,
            /// <summary>
            /// An expression which creates an array.
            /// </summary>
            /// <remarks>In the case of a rewrite, native numeric data-types
            /// are enumerated and consolidated into a byte-array and cast into a
            /// field for loading by the assembly.</remarks>
            CreateArray                         = 0x0000000000000040,
            /// <summary>
            /// An expression which awaits the result of an asynchronous task.
            /// </summary>
            AwaitExpression                     = 0x0000000000000080,
            /// <summary>
            /// Represents all expressions which require precompiler
            /// expansion.
            /// </summary>
            All                                 = 0x00000000000000FF,
        }

        /// <summary>
        /// Provides a series of values related to the innately
        /// supported series of expression kinds.
        /// </summary>
        /// <remarks>Set two of two.</remarks>
        [Flags]
        [CLSCompliant(false)]
        public enum SymbolSector
        {
            None                                = 0x0000000000000000,
            /// <summary>
            /// The expression is a fusion of two expressions.
            /// </summary>
            /// <remarks>Typically used to denote an expression to be fused with
            /// another expression.</remarks>
            ExpressionFusion                    = 0x0000000000000001,
            /// <summary>
            /// The expression is a fusion of another expression and a series
            /// of expressions encased by parenthesis, and individually
            /// delimited by commas.
            /// </summary>
            /// <remarks>Typically an invocation type expression where the target
            /// is the left and the parameters are on the right.</remarks>
            ExpressionToCommaFusion             = 0x0000000000000002,
            /// <summary>
            /// The expression is a fusion of an expression and a series of type
            /// references, delimited by a comma.
            /// </summary>
            /// <remarks>Typically used to denote the fusion of an expression
            /// which needs a series of type-parameters.</remarks>
            ExpressionToTypeCollectionFusion    = 0x0000000000000004,
            /// <summary>
            /// Indicates that an expression requires its intent to be
            /// inferred.
            /// </summary>
            /// <remarks>Symbol expressions are not enough information alone
            /// to infer use.  Must be conjoined with a call, set, access or
            /// reference-based expression kind.</remarks>
            SymbolExpression                    = 0x0000000000000008,
            /// <summary>
            /// Indicates that an expression is wrapped in parenthesis ('(' and ')').
            /// </summary>
            ParenthesizedExpression             = 0x0000000000000010,
            /// <summary>
            /// Represents all symbol sector elements.
            /// </summary>
            All                                 = 0x000000000000001F,
        }

    }
}
