using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class CustomAttributeDefinitionParameterCollection :
        ControlledStateCollection<ICustomAttributeDefinitionParameter>,
        _ICustomAttributeDefinitionParameterCollection
    {
        private int namelessParamCount = 0;
        private HashSet<string> namedParameterNames = new HashSet<string>();
        /// <summary>
        /// Returns the <see cref="ICustomAttributeDefinition"/> which contains the <see cref="CustomAttributeDefinitionParameterCollection"/>
        /// </summary>
        public ICustomAttributeDefinition Parent { get; private set; }
        /// <summary>
        /// Creates a new <see cref="CustomAttributeDefinitionParameterCollection"/> with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <see cref="ICustomAttributeDefinition"/> which contains the <see cref="CustomAttributeDefinitionParameterCollection"/></param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parent"/>
        /// is null.</exception>
        internal CustomAttributeDefinitionParameterCollection(ICustomAttributeDefinition parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            this.Parent = parent;
        }

        /* *
         * Only certain types are allowed in a constructor for an attribute.
         * *
         * ToDo: Add parameter array support.
         * */
        private ICustomAttributeDefinitionParameter<T> AddInternal<T>(T value)
        {
            CustomAttributeDefinitionParameter<T> parameter = new CustomAttributeDefinitionParameter<T>(value, this);
            base.baseList.Add(parameter);
            namelessParamCount++;
            return parameter;
        }
        private ICustomAttributeDefinitionNamedParameter<T> AddInternal<T>(string name, T value)
        {
            if (namedParameterNames.Contains(name))
                throw new ArgumentException("name");
            CustomAttributeDefinitionNamedParameter<T> parameter = new CustomAttributeDefinitionNamedParameter<T>(name, value, this);
            base.baseList.Add(parameter);
            this.namedParameterNames.Add(name);
            return parameter;
        }
        #region ICustomAttributeDefinitionParameterCollection Members

        public ICustomAttributeDefinitionParameter[] AddSeries(CustomAttributeDefinition.ParameterValueCollection values)
        {
            return values.AddInternal(this);
        }

        /// <summary>
        /// Adds a <see cref="Boolean"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Boolean"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Boolean"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<bool> Add(bool value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="String"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="String"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<string> Add(string value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Type"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Type"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Type"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<Type> Add(Type value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Byte"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Byte"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Byte"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<byte> Add(byte value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="SByte"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="SByte"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="SByte"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<sbyte> Add(sbyte value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="UInt16"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt16"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="UInt16"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<ushort> Add(ushort value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Int16"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int16"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Int16"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<short> Add(short value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Int32"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Int32"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<int> Add(int value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="UInt32"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt32"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="UInt32"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<uint> Add(uint value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Int64"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int64"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Int64"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<long> Add(long value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="UInt64"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt64"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="UInt64"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<ulong> Add(ulong value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Single"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Single"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<float> Add(float value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Double"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Double"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Double"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<double> Add(double value)
        {
            return AddInternal(value);
        }

        /// <summary>
        /// Adds a <see cref="Decimal"/> <paramref name="value"/> parameter to the <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Decimal"/> value to add.</param>
        /// <returns>A <see cref="ICustomAttributeDefinitionParameter{T}"/> typed
        /// as a <see cref="Decimal"/> parameter.</returns>
        public ICustomAttributeDefinitionParameter<decimal> Add(decimal value)
        {
            return AddInternal(value);
        }

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
        public ICustomAttributeDefinitionNamedParameter<bool> Add(string name, bool value)
        {
            return this.AddInternal(name, value);
        }

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
        public ICustomAttributeDefinitionNamedParameter<string> Add(string name, string value)
        {
            return this.AddInternal(name, value);
        }

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
        public ICustomAttributeDefinitionNamedParameter<Type> Add(string name, Type value)
        {
            return this.AddInternal(name, value);
        }

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
        public ICustomAttributeDefinitionNamedParameter<byte> Add(string name, byte value)
        {
            return this.AddInternal(name, value);
        }

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
        [CLSCompliant(false)]
        public ICustomAttributeDefinitionNamedParameter<sbyte> Add(string name, sbyte value)
        {
            return this.AddInternal(name, value);
        }

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
        public ICustomAttributeDefinitionNamedParameter<short> Add(string name, short value)
        {
            return this.AddInternal(name, value);
        }

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
        [CLSCompliant(false)]
        public ICustomAttributeDefinitionNamedParameter<ushort> Add(string name, ushort value)
        {
            return this.AddInternal(name, value);
        }

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
        public ICustomAttributeDefinitionNamedParameter<int> Add(string name, int value)
        {
            return this.AddInternal(name, value);
        }

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
        [CLSCompliant(false)]
        public ICustomAttributeDefinitionNamedParameter<uint> Add(string name, uint value)
        {
            return this.AddInternal(name, value);
        }

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
        public ICustomAttributeDefinitionNamedParameter<long> Add(string name, long value)
        {
            return this.AddInternal(name, value);
        }

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
        [CLSCompliant(false)]
        public ICustomAttributeDefinitionNamedParameter<ulong> Add(string name, ulong value)
        {
            return this.AddInternal(name, value);
        }

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
        public ICustomAttributeDefinitionNamedParameter<float> Add(string name, float value)
        {
            return this.AddInternal(name, value);
        }

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
        public ICustomAttributeDefinitionNamedParameter<double> Add(string name, double value)
        {
            return this.AddInternal(name, value);
        }

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
        public ICustomAttributeDefinitionNamedParameter<decimal> Add(string name, decimal value)
        {
            return this.AddInternal(name, value);
        }

        #endregion

        #region _ICustomAttributeDefinitionParameterCollection Members

        ICustomAttributeDefinitionParameter _ICustomAttributeDefinitionParameterCollection.AddInternal<T>(T value)
        {
            return this.AddInternal(value);
        }

        ICustomAttributeDefinitionParameter _ICustomAttributeDefinitionParameterCollection.AddInternal<T>(string name, T value)
        {
            return this.AddInternal(name, value);
        }


        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                var items = this.ToArray();
                for (int i = items.Length - 1; i >= 0; i--)
                    items[i].Dispose();
                if (NamelessParametersChanged != null)
                    this.NamelessParametersChanged = null;
                if (this.NamedParameterChangedValue != null)
                    this.NamedParameterChangedValue = null;
                if (this.NamedParameterChangedName != null)
                    this.NamedParameterChangedName = null;
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region ICustomAttributeDefinitionParameterCollection Members

        /// <summary>
        /// Occurs when the a nameless parameter changes value or a member is added/removed.
        /// </summary>
        public event EventHandler NamelessParametersChanged;

        /// <summary>
        /// Occurs when a named parameter is renamed.
        /// </summary>
        /// <remarks><para>Dynamic attribute wrapper uses this to keep its wrapped instance up to date.</para>
        /// <para>When a parameter is renamed it needs recreated to ensure the data is an accurate reflection
        /// of the attribute emitted.</para></remarks>
        public event EventHandler<EventArgsR1<ICustomAttributeDefinitionNamedParameter>> NamedParameterChangedName;

        /// <summary>
        /// Occurs when a named parameter changed its value.
        /// </summary>
        /// <remarks><para>Dynamic attribute wrapper uses this to keep its wrapped instance up to date.</para>
        /// <para>When a named parameter's value changes, the value on the wrapper can simply be updated.</para></remarks>
        public event EventHandler<EventArgsR1<ICustomAttributeDefinitionNamedParameter>> NamedParameterChangedValue;

        /// <summary>
        /// Clears the parameters contained within the
        /// <see cref="ICustomAttributeDefinitionParameterCollection"/>.
        /// </summary>
        /// <remarks>Raises <see cref="NamelessParametersChanged"/> if there were
        /// nameless parameters.</remarks>
        public void Clear()
        {
            if (this.namelessParamCount > 0)
            {
                this.OnNamelessParametersChanged(EventArgs.Empty);
                this.namelessParamCount = 0;
            }
            foreach (var item in this)
                item.Dispose();

        }

        protected virtual void OnNamelessParametersChanged(EventArgs eventArgs)
        {
            var namelessParametersChanged = this.NamelessParametersChanged;
            if (namelessParametersChanged != null)
                namelessParametersChanged(this, eventArgs);
        }

        /// <summary>
        /// Removes a nameless parameter at the <paramref name="index"/> specified.
        /// </summary>
        /// <param name="index">The index of the nameless parameter.</param>
        /// <remarks>If the nameless parameters and named parameters are interleaved, the named
        /// elements are not counted during this process.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="index"/> is below zero
        /// -or- beyond the scope of the unnamed parameters.</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.namelessParamCount)
                throw new ArgumentOutOfRangeException("index");
            int _index = 0;
            foreach (var item in this)
                if (!(item is ICustomAttributeDefinitionNamedParameter))
                    if (_index == index)
                    {
                        base.baseList.Remove(item);
                        break;
                    }
                    else
                        _index++;
        }

        /// <summary>
        /// Removes a named parameter with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the named parameter to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when a parameter of the <paramref name="name"/>
        /// provided could not be found.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">thrown when the collection is desynchronized with its
        /// internal collection; occurs when its been modified outside of this instance's scope.</exception>
        public void Remove(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (!this.namedParameterNames.Contains(name))
                throw new ArgumentException("name");
            foreach (var item in this)
                if (item is ICustomAttributeDefinitionNamedParameter && ((ICustomAttributeDefinitionNamedParameter)(item)).Name == name)
                {
                    this.namedParameterNames.Remove(name);
                    base.baseList.Remove(item);
                    return;
                }
            //If this occurs, someone's modified the list, the name is there in our cache
            //but it's not in the list.
            throw new InvalidOperationException();
        }

        #endregion

        internal void OnItemRename<T>(CustomAttributeDefinitionNamedParameter<T> item, string oldName, string newName)
        {
            if (this.namedParameterNames.Contains(oldName))
            {
                this.OnNamedParameterChangedName(item);
            }
            else //No record of it.
                throw new ArgumentException("item");
        }

        internal void OnItemValueChanged<T>(CustomAttributeDefinitionParameter<T> item)
        {
            if (item is ICustomAttributeDefinitionNamedParameter)
                this.OnNamedParameterChangedValue((ICustomAttributeDefinitionNamedParameter)item);
            else
                this.OnNamelessParametersChanged(EventArgs.Empty);
        }

        private void OnNamedParameterChangedValue(ICustomAttributeDefinitionNamedParameter item)
        {
            var namedParameterChangedValue = this.NamedParameterChangedValue;
            if (namedParameterChangedValue != null)
                namedParameterChangedValue(this, new EventArgsR1<ICustomAttributeDefinitionNamedParameter>(item));
        }

        private void OnNamedParameterChangedName(ICustomAttributeDefinitionNamedParameter item)
        {
            var namedParameterChangedName = this.NamedParameterChangedName;

            if (namedParameterChangedName != null)
                namedParameterChangedName(this, new EventArgsR1<ICustomAttributeDefinitionNamedParameter>(item));
        }
    }
}
