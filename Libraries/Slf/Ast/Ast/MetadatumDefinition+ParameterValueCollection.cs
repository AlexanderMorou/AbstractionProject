using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class MetadatumDefinitionParameterValueCollection :
        IEnumerable<IMetadatumDefinitionParameterValue>
    {
        private List<_IMetadatumDefinitionParameterValue> values;
        private List<_IMetadatumDefinitionParameterValue> namedValues;
        /// <summary>
        /// Creates a new <see cref="MetadatumDefinitionParameterValueCollection"/>
        /// instance.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/> which represents
        /// the metadatum the parameters relate to.</param>
        public MetadatumDefinitionParameterValueCollection(IType metadatumType)
        {
            this.MetadatumType = metadatumType;
        }

        internal IMetadatumDefinitionParameter[] AddInternal(_IMetadatumDefinitionParameterCollection target)
        {
            int count = (this.values != null ? this.values.Count : 0) + (this.namedValues != null ? this.namedValues.Count : 0);
            IMetadatumDefinitionParameter[] result = new IMetadatumDefinitionParameter[count];
            int i = 0;
            if (this.values != null)
                foreach (var item in values)
                    result[i++] = item.AddSelf(target);
            if (this.namedValues != null)
                foreach (var item in namedValues)
                    result[i++] = item.AddSelf(target);
            return result;
        }

        /// <summary>
        /// Returns/sets the <see cref="MetadatumType"/> associated to the 
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        public IType MetadatumType { get; set; }

        private MetadatumDefinitionParameterValue<T> AddInternal<T>(T value)
        {
            if (this.values == null)
                this.values = new List<_IMetadatumDefinitionParameterValue>();
            MetadatumDefinitionParameterValue<T> parameter = new MetadatumDefinitionParameterValue<T>(value);
            this.values.Add(parameter);
            return parameter;
        }

        private MetadatumDefinitionNamedParameterValue<T> AddInternal<T>(string name, T value)
        {
            if (this.namedValues == null)
                this.namedValues = new List<_IMetadatumDefinitionParameterValue>();
            MetadatumDefinitionNamedParameterValue<T> parameter = new MetadatumDefinitionNamedParameterValue<T>(name, value);
            this.namedValues.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Adds a <see cref="Boolean"/> <paramref name="value"/> parameter to 
        /// the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Boolean"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="Boolean"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<bool> Add(bool value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="IExpression"/> <paramref name="value"/> parameter to 
        /// the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="IExpression"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a constant <see cref="IExpression"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<IExpression> Add(IExpression value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="String"/> <paramref name="value"/> parameter to
        /// the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="String"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<string> Add(string value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Type"/> <paramref name="value"/> parameter to
        /// the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Type"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="Type"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<IType> Add(IType value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Byte"/> <paramref name="value"/> parameter to
        /// the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Byte"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="Byte"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<byte> Add(byte value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="SByte"/> <paramref name="value"/> parameter
        /// to the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="SByte"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="SByte"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<sbyte> Add(sbyte value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="UInt16"/> <paramref name="value"/> parameter
        /// to the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt16"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="UInt16"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<ushort> Add(ushort value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Int16"/> <paramref name="value"/> parameter
        /// to the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int16"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="Int16"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<short> Add(short value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Int32"/> <paramref name="value"/> parameter
        /// to the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="Int32"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<int> Add(int value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="UInt32"/> <paramref name="value"/> parameter
        /// to the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt32"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="UInt32"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<uint> Add(uint value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Int64"/> <paramref name="value"/> parameter
        /// to the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int64"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="Int64"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<long> Add(long value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="UInt64"/> <paramref name="value"/> parameter
        /// to the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt64"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="UInt64"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<ulong> Add(ulong value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> <paramref name="value"/> parameter
        /// to the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Single"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="Single"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<float> Add(float value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Double"/> <paramref name="value"/> parameter
        /// to the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Double"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="Double"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<double> Add(double value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Decimal"/> <paramref name="value"/> parameter
        /// to the <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Decimal"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionParameterValue{T}"/> typed
        /// as a <see cref="Decimal"/> parameter.</returns>
        public MetadatumDefinitionParameterValue<decimal> Add(decimal value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Boolean"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Boolean"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Boolean"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<bool> Add(string name, bool value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="String"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="String"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="String"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<string> Add(string name, string value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Type"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Type"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Type"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<IType> Add(string name, IType value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Byte"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Byte"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Byte"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<byte> Add(string name, byte value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="SByte"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="SByte"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="SByte"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<sbyte> Add(string name, sbyte value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Int16"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Int16"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Int16"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<short> Add(string name, short value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="UInt16"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="UInt16"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="UInt16"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<ushort> Add(string name, ushort value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Int32"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Int32"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Int32"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<int> Add(string name, int value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="UInt32"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="UInt32"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="UInt32"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<uint> Add(string name, uint value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Int64"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Int64"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Int64"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<long> Add(string name, long value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="UInt64"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="UInt64"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="UInt64"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<ulong> Add(string name, ulong value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Single"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Single"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Single"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<float> Add(string name, float value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Double"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Double"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Double"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<double> Add(string name, double value)
        {
            return this.AddInternal(name, value);
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Decimal"/> <paramref name="value"/> to the
        /// <see cref="MetadatumDefinitionParameterValueCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Decimal"/> value to add.</param>
        /// <returns>A <see cref="MetadatumDefinitionNamedParameterValue{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Decimal"/> parameter.</returns>
        public MetadatumDefinitionNamedParameterValue<decimal> Add(string name, decimal value)
        {
            return this.AddInternal(name, value);
        }

        //public IMetadatumDefinitionParameterValue Add(IEnumFieldMember value)
        //{
        //    switch (value.Parent.ValueType)
        //    {
        //        case EnumerationBaseType.SByte:
                    
        //            break;
        //        case EnumerationBaseType.Byte:
        //            break;
        //        case EnumerationBaseType.Int16:
        //            break;
        //        case EnumerationBaseType.UInt16:
        //            break;
        //        case EnumerationBaseType.Int32:
        //            break;
        //        case EnumerationBaseType.UInt32:
        //            break;
        //        case EnumerationBaseType.Int64:
        //            break;
        //        case EnumerationBaseType.UInt64:
        //            break;
        //        default:
        //            break;
        //    }
        //}

        #region IEnumerable<IMetadatumDefinitionParameterValue> Members

        public IEnumerator<IMetadatumDefinitionParameterValue> GetEnumerator()
        {
            if (this.values != null)
                foreach (var item in this.values)
                    yield return item;
            if (this.namedValues != null)
                foreach (var item in this.namedValues)
                    yield return item;
            yield break;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}