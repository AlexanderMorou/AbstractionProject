using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class CustomAttributeDefinition
    {
        public struct ParameterValueCollection :
            IEnumerable<ICustomAttributeDefinitionParameterValue>
        {
            private List<_ICustomAttributeDefinitionParameterValue> values;
            private List<_ICustomAttributeDefinitionParameterValue> namedValues;
            public ParameterValueCollection(IType attributeType)
                : this()
            {
                this.AttributeType = attributeType;
            }

            internal ICustomAttributeDefinitionParameter[] AddInternal(_ICustomAttributeDefinitionParameterCollection target)
            {
                int count = (this.values != null ? this.values.Count : 0) + (this.namedValues != null ? this.namedValues.Count : 0);
                ICustomAttributeDefinitionParameter[] result = new ICustomAttributeDefinitionParameter[count];
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
            /// Returns/sets the <see cref="AttributeType"/> associated to the <see cref="CustomAttributeDefinition.ParameterValueCollection"/>.
            /// </summary>
            public IType AttributeType { get; set; }

            private CustomAttributeDefinitionParameterValue<T> AddInternal<T>(T value)
            {
                if (this.values == null)
                    this.values = new List<_ICustomAttributeDefinitionParameterValue>();
                CustomAttributeDefinitionParameterValue<T> parameter = new CustomAttributeDefinitionParameterValue<T>(value);
                this.values.Add(parameter);
                return parameter;
            }
            private CustomAttributeDefinitionNamedParameterValue<T> AddInternal<T>(string name, T value)
            {
                if (this.namedValues == null)
                    this.namedValues = new List<_ICustomAttributeDefinitionParameterValue>();
                CustomAttributeDefinitionNamedParameterValue<T> parameter = new CustomAttributeDefinitionNamedParameterValue<T>(name, value);
                this.namedValues.Add(parameter);
                return parameter;
            }

            /// <summary>
            /// Adds a <see cref="Boolean"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="Boolean"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="Boolean"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<bool> Add(bool value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="String"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="String"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="String"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<string> Add(string value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="Type"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="Type"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="Type"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<Type> Add(Type value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="Byte"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="Byte"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="Byte"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<byte> Add(byte value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="SByte"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="SByte"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="SByte"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<sbyte> Add(sbyte value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="UInt16"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="UInt16"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="UInt16"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<ushort> Add(ushort value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="Int16"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="Int16"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="Int16"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<short> Add(short value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="Int32"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="Int32"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="Int32"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<int> Add(int value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="UInt32"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="UInt32"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="UInt32"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<uint> Add(uint value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="Int64"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="Int64"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="Int64"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<long> Add(long value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="UInt64"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="UInt64"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="UInt64"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<ulong> Add(ulong value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="Single"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="Single"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="Single"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<float> Add(float value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="Double"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="Double"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="Double"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<double> Add(double value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <see cref="Decimal"/> <paramref name="value"/> parameter to the <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="value">The <see cref="Decimal"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionParameterValue{T}"/> typed
            /// as a <see cref="Decimal"/> parameter.</returns>
            public CustomAttributeDefinitionParameterValue<decimal> Add(decimal value)
            {
                return AddInternal(value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="Boolean"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="Boolean"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="Boolean"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<bool> Add(string name, bool value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="String"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="String"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="String"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<string> Add(string name, string value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="Type"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="Type"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="Type"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<Type> Add(string name, Type value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="Byte"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="Byte"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="Byte"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<byte> Add(string name, byte value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="SByte"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="SByte"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="SByte"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<sbyte> Add(string name, sbyte value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="Int16"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="Int16"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="Int16"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<short> Add(string name, short value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="UInt16"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="UInt16"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="UInt16"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<ushort> Add(string name, ushort value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="Int32"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="Int32"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="Int32"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<int> Add(string name, int value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="UInt32"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="UInt32"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="UInt32"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<uint> Add(string name, uint value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="Int64"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="Int64"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="Int64"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<long> Add(string name, long value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="UInt64"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="UInt64"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="UInt64"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<ulong> Add(string name, ulong value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="Single"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="Single"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="Single"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<float> Add(string name, float value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="Double"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="Double"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="Double"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<double> Add(string name, double value)
            {
                return this.AddInternal(name, value);
            }

            /// <summary>
            /// Adds a <paramref name="name"/>d <see cref="Decimal"/> <paramref name="value"/> to the
            /// <see cref="ParameterValueCollection"/>.
            /// </summary>
            /// <param name="name">The name of the new value to add.</param>
            /// <param name="value">The <see cref="Decimal"/> value to add.</param>
            /// <returns>A <see cref="CustomAttributeDefinitionNamedParameterValue{T}"/> with the given
            /// <paramref name="name"/>, and typed as a <see cref="Decimal"/> parameter.</returns>
            public CustomAttributeDefinitionNamedParameterValue<decimal> Add(string name, decimal value)
            {
                return this.AddInternal(name, value);
            }

            #region IEnumerable<ICustomAttributeDefinitionParameterValue> Members

            public IEnumerator<ICustomAttributeDefinitionParameterValue> GetEnumerator()
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
}
