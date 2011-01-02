using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a series of 
    /// <see cref="ICustomAttributeDefinitionParameter"/>s.
    /// </summary>
    public interface ICustomAttributeDefinitionParameterCollection :
        IControlledStateCollection<ICustomAttributeDefinitionParameter>,
        IDisposable
    {
        /// <summary>
        /// Occurs when the a nameless parameter changes value or a member is added/removed.
        /// </summary>
        event EventHandler NamelessParametersChanged;
        /// <summary>
        /// Occurs when a named parameter is renamed.
        /// </summary>
        /// <remarks><para>Dynamic attribute wrapper uses this to keep its wrapped instance up to date.</para>
        /// <para>When a parameter is renamed it needs recreated to ensure the data is an accurate reflection
        /// of the attribute emitted.</para></remarks>
        event EventHandler<EventArgsR1<ICustomAttributeDefinitionNamedParameter>> NamedParameterChangedName;
        /// <summary>
        /// Occurs when a named parameter changed its value.
        /// </summary>
        /// <remarks><para>Dynamic attribute wrapper uses this to keep its wrapped instance up to date.</para>
        /// <para>When a named parameter's value changes, the value on the wrapper can simply be updated.</para></remarks>
        event EventHandler<EventArgsR1<ICustomAttributeDefinitionNamedParameter>> NamedParameterChangedValue;
        /// <summary>
        /// Returns the <see cref="ICustomAttributeDefinition"/> which contains the <see cref="ICustomAttributeDefinitionParameterCollection"/>
        /// </summary>
        ICustomAttributeDefinition Parent { get; }
        /// <summary>
        /// Adds a <see cref="Boolean"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Boolean"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Boolean"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<bool> Add(bool value);
        /// <summary>
        /// Adds a <see cref="String"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="String"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<string> Add(string value);
        /// <summary>
        /// Adds a <see cref="Type"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Type"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Type"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<Type> Add(Type value);
        /// <summary>
        /// Adds a <see cref="Byte"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Byte"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Byte"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<byte> Add(byte value);
        /// <summary>
        /// Adds a <see cref="SByte"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="SByte"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="SByte"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<sbyte> Add(sbyte value);
        /// <summary>
        /// Adds a <see cref="UInt16"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt16"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="UInt16"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<ushort> Add(ushort value);
        /// <summary>
        /// Adds a <see cref="Int16"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int16"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Int16"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<short> Add(short value);
        /// <summary>
        /// Adds a <see cref="Int32"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Int32"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<int> Add(int value);
        /// <summary>
        /// Adds a <see cref="UInt32"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt32"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="UInt32"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<uint> Add(uint value);
        /// <summary>
        /// Adds a <see cref="Int64"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int64"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Int64"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<long> Add(long value);
        /// <summary>
        /// Adds a <see cref="UInt64"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt64"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="UInt64"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<ulong> Add(ulong value);
        /// <summary>
        /// Adds a <see cref="Single"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Single"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Single"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<float> Add(float value);
        /// <summary>
        /// Adds a <see cref="Double"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Double"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Double"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<double> Add(double value);
        /// <summary>
        /// Adds a <see cref="Decimal"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Decimal"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Decimal"/> parameter.</returns>
        ICustomAttributeDefinitionParameter<decimal> Add(decimal value);

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Boolean"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Boolean"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Boolean"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<bool> Add(string name, bool value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="String"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="String"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="String"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<string> Add(string name, string value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Type"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Type"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Type"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<Type> Add(string name, Type value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Byte"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Byte"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Byte"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<byte> Add(string name, byte value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="SByte"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="SByte"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="SByte"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<sbyte> Add(string name, sbyte value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Int16"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Int16"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Int16"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<short> Add(string name, short value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="UInt16"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="UInt16"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="UInt16"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<ushort> Add(string name, ushort value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Int32"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Int32"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Int32"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<int> Add(string name, int value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="UInt32"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="UInt32"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="UInt32"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<uint> Add(string name, uint value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Int64"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Int64"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Int64"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<long> Add(string name, long value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="UInt64"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="UInt64"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="UInt64"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<ulong> Add(string name, ulong value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Single"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Single"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Single"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<float> Add(string name, float value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Double"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Double"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Double"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<double> Add(string name, double value);
        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Decimal"/> <paramref name="value"/> to the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Decimal"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Decimal"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        ICustomAttributeDefinitionNamedParameter<decimal> Add(string name, decimal value);

        /// <summary>
        /// Inserts a series of <see cref="ICustomAttributeDefinitionParameter"/> instances
        /// based upon the <paramref name="values"/> provided.
        /// </summary>
        /// <param name="values">A <see cref="CustomAttributeDefinition.ParameterValueCollection"/>
        /// instance which has had similarly structured values added to it.</param>
        /// <returns>The series of <see cref="ICustomAttributeDefinitionParameter"/> as they were added.</returns>
        ICustomAttributeDefinitionParameter[] AddSeries(CustomAttributeDefinition.ParameterValueCollection values);

        /// <summary>
        /// Clears the parameters contained within the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <remarks>Raises <see cref="NamelessParamtersChanged"/> if there were
        /// nameless parameters.</remarks>
        void Clear();
        /// <summary>
        /// Removes a nameless parameter at the <paramref name="index"/> specified.
        /// </summary>
        /// <param name="index">The index of the nameless parameter.</param>
        /// <remarks>If the nameless parameters and named parameters are interleaved, the named
        /// elements are not counted during this process.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="index"/> is below zero
        /// -or- beyond the scope of the unnamed parameters.</exception>
        void RemoveAt(int index);
        /// <summary>
        /// Removes a named parameter with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the named parameter to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when a parameter of the <paramref name="name"/>
        /// provided could not be found.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        void Remove(string name);
    }
}
