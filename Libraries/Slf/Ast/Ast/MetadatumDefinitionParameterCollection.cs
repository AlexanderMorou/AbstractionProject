using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class MetadatumDefinitionParameterCollection :
        ControlledCollection<IMetadatumDefinitionParameter>,
        _IMetadatumDefinitionParameterCollection
    {
        private int namelessParamCount = 0;

        private HashSet<string> namedParameterNames = new HashSet<string>();
        /// <summary>
        /// Data member holding onto the boolean type relative to the active scope.
        /// </summary>
        private  IType booleanType;
        private  IType charType;
        private  IType stringType;
        private  IType typeType;
        private  IType byteType;
        private  IType sbyteType;
        private  IType uint16Type;
        private  IType int16Type;
        private  IType int32Type;
        private  IType uint32Type;
        private  IType int64Type;
        private  IType uint64Type;
        private  IType singleType;
        private  IType doubleType;
        private  IType decimalType;
        private  IType itypeType;
        /// <summary>
        /// Returns the <see cref="IMetadatumDefinition"/> which contains the <see cref="MetadatumDefinitionParameterCollection"/>
        /// </summary>
        public MetadatumDefinition Parent { get; private set; }

        IMetadatumDefinition IMetadataDefinitionParameterCollection.Parent
        {
            get
            {
                return this.Parent;
            }
        }

        private IIntermediateAssembly OwningAssembly
        {
            get
            {
                return this.Parent.OwningAssembly;
            }
        }

        /// <summary>
        /// Creates a new <see cref="MetadatumDefinitionParameterCollection"/> with
        /// the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="MetadatumDefinition"/> which contains
        /// the <see cref="MetadatumDefinitionParameterCollection"/></param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parent"/>
        /// is null.</exception>
        internal MetadatumDefinitionParameterCollection(MetadatumDefinition parent)
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
        private IMetadatumDefinitionParameter<T> AddInternal<T>(T value, IType valueType)
        {
            MetadatumDefinitionParameter<T> parameter = new MetadatumDefinitionParameter<T>(value, this, valueType);
            base.baseList.Add(parameter);
            namelessParamCount++;
            return parameter;
        }

        private IMetadatumDefinitionExpressionParameter AddInternal(IExpression value)
        {
            var parameter = new MetadatumDefinitionExpressionParameter(value, this);
            base.baseList.Add(parameter);
            namelessParamCount++;
            return parameter;
        }

        private IMetadatumDefinitionNamedParameter<T> AddInternal<T>(string name, T value, IType valueType)
        {
            if (namedParameterNames.Contains(name))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.name, ExceptionMessageId.DuplicateKeyExists);
            MetadatumDefinitionNamedParameter<T> parameter = new MetadatumDefinitionNamedParameter<T>(name, value, this, valueType);
            base.baseList.Add(parameter);
            this.namedParameterNames.Add(name);
            return parameter;
        }
        #region IMetadataDefinitionParameterCollection Members

        public IMetadatumDefinitionParameter[] AddSeries(MetadatumDefinitionParameterValueCollection values)
        {
            return values.AddInternal(this);
        }

        /// <summary>
        /// Adds a <see cref="Boolean"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Boolean"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="Boolean"/> parameter.</returns>
        public IMetadatumDefinitionParameter<bool> Add(bool value)
        {
            return AddInternal(value, this.booleanType ?? (this.booleanType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Boolean)));
        }

        /// <summary>
        /// Adds a <see cref="String"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="String"/> parameter.</returns>
        public IMetadatumDefinitionParameter<string> Add(string value)
        {
            return AddInternal(value, this.stringType ?? (this.stringType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.String)));
        }

        /// <summary>
        /// Adds a <see cref="Type"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Type"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="Type"/> parameter.</returns>
        public IMetadatumDefinitionParameter<Type> Add(Type value)
        {
            return AddInternal(value, this.typeType ?? (this.typeType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Type)));
        }

        /// <summary>
        /// Adds a <see cref="Byte"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Byte"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="Byte"/> parameter.</returns>
        public IMetadatumDefinitionParameter<byte> Add(byte value)
        {
            return AddInternal(value, this.byteType ?? (this.byteType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Byte)));
        }

        /// <summary>
        /// Adds a <see cref="SByte"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="SByte"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="SByte"/> parameter.</returns>
        public IMetadatumDefinitionParameter<sbyte> Add(sbyte value)
        {
            return AddInternal(value, this.sbyteType ?? (this.sbyteType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.SByte)));
        }

        /// <summary>
        /// Adds a <see cref="UInt16"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt16"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="UInt16"/> parameter.</returns>
        public IMetadatumDefinitionParameter<ushort> Add(ushort value)
        {
            return AddInternal(value, this.uint16Type ?? (this.uint16Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt16)));
        }

        /// <summary>
        /// Adds a <see cref="Int16"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int16"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="Int16"/> parameter.</returns>
        public IMetadatumDefinitionParameter<short> Add(short value)
        {
            return AddInternal(value, this.int16Type ?? (this.int16Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int16)));
        }

        /// <summary>
        /// Adds a <see cref="Int32"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="Int32"/> parameter.</returns>
        public IMetadatumDefinitionParameter<int> Add(int value)
        {
            return AddInternal(value, this.int32Type ?? (this.int32Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int32)));
        }

        /// <summary>
        /// Adds a <see cref="UInt32"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt32"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="UInt32"/> parameter.</returns>
        public IMetadatumDefinitionParameter<uint> Add(uint value)
        {
            return AddInternal(value, this.uint32Type ?? (this.uint32Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt32)));
        }

        /// <summary>
        /// Adds a <see cref="Int64"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int64"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="Int64"/> parameter.</returns>
        public IMetadatumDefinitionParameter<long> Add(long value)
        {
            return AddInternal(value, this.int64Type ?? (this.int64Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int64)));
        }

        /// <summary>
        /// Adds a <see cref="UInt64"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="UInt64"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="UInt64"/> parameter.</returns>
        public IMetadatumDefinitionParameter<ulong> Add(ulong value)
        {
            return AddInternal(value, this.uint64Type ?? (this.uint64Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt64)));
        }

        /// <summary>
        /// Adds a <see cref="Single"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Single"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="Single"/> parameter.</returns>
        public IMetadatumDefinitionParameter<float> Add(float value)
        {
            return AddInternal(value, this.singleType ?? (this.singleType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Single)));
        }

        /// <summary>
        /// Adds a <see cref="Double"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Double"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="Double"/> parameter.</returns>
        public IMetadatumDefinitionParameter<double> Add(double value)
        {
            return AddInternal(value, this.doubleType ?? (this.doubleType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Double)));
        }

        /// <summary>
        /// Adds a <see cref="Decimal"/> <paramref name="value"/> parameter to the <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Decimal"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionParameter{T}"/> typed
        /// as a <see cref="Decimal"/> parameter.</returns>
        public IMetadatumDefinitionParameter<decimal> Add(decimal value)
        {
            return AddInternal(value, this.decimalType ?? (this.decimalType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Decimal)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Boolean"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Boolean"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Boolean"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        public IMetadatumDefinitionNamedParameter<bool> Add(string name, bool value)
        {
            return AddInternal(name, value, this.booleanType ?? (this.booleanType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Boolean)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="String"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="String"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="String"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        public IMetadatumDefinitionNamedParameter<string> Add(string name, string value)
        {
            return AddInternal(name, value, this.stringType ?? (this.stringType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.String)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Type"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Type"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Type"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        public IMetadatumDefinitionNamedParameter<Type> Add(string name, Type value)
        {
            return AddInternal(name, value, this.typeType ?? (this.typeType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Type)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Byte"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Byte"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Byte"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        public IMetadatumDefinitionNamedParameter<byte> Add(string name, byte value)
        {
            return AddInternal(name, value, this.byteType ?? (this.byteType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Byte)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="SByte"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="SByte"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="SByte"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        [CLSCompliant(false)]
        public IMetadatumDefinitionNamedParameter<sbyte> Add(string name, sbyte value)
        {
            return AddInternal(name, value, this.sbyteType ?? (this.sbyteType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.SByte)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Int16"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Int16"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Int16"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        public IMetadatumDefinitionNamedParameter<short> Add(string name, short value)
        {
            return AddInternal(name, value, this.int16Type ?? (this.int16Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int16)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="UInt16"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="UInt16"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="UInt16"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        [CLSCompliant(false)]
        public IMetadatumDefinitionNamedParameter<ushort> Add(string name, ushort value)
        {
            return AddInternal(name, value, this.uint16Type ?? (this.uint16Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt16)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Int32"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Int32"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Int32"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        public IMetadatumDefinitionNamedParameter<int> Add(string name, int value)
        {
            return AddInternal(name, value, this.int32Type ?? (this.int32Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int32)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="UInt32"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="UInt32"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="UInt32"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        [CLSCompliant(false)]
        public IMetadatumDefinitionNamedParameter<uint> Add(string name, uint value)
        {
            return AddInternal(name, value, this.uint32Type ?? (this.uint32Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt32)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Int64"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Int64"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Int64"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        public IMetadatumDefinitionNamedParameter<long> Add(string name, long value)
        {
            return AddInternal(name, value, this.int64Type ?? (this.int64Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int64)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="UInt64"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="UInt64"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="UInt64"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        [CLSCompliant(false)]
        public IMetadatumDefinitionNamedParameter<ulong> Add(string name, ulong value)
        {
            return AddInternal(name, value, this.uint64Type ?? (this.uint64Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt64)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Single"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Single"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Single"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        public IMetadatumDefinitionNamedParameter<float> Add(string name, float value)
        {
            return AddInternal(name, value, this.singleType ?? (this.singleType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Single)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Double"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Double"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Double"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        public IMetadatumDefinitionNamedParameter<double> Add(string name, double value)
        {
            return AddInternal(name, value, this.doubleType ?? (this.doubleType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Double)));
        }

        /// <summary>
        /// Adds a <paramref name="name"/>d <see cref="Decimal"/> <paramref name="value"/> to the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
        /// </summary>
        /// <param name="name">The name of the new value to add.</param>
        /// <param name="value">The <see cref="Decimal"/> value to add.</param>
        /// <returns>A <see cref="IMetadatumDefinitionNamedParameter{T}"/> with the given
        /// <paramref name="name"/>, and typed as a <see cref="Decimal"/> parameter.</returns>
        /// <exception cref="System.ArgumentException">thrown when a parameter by the 
        /// <paramref name="name"/> provided already exists.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        public IMetadatumDefinitionNamedParameter<decimal> Add(string name, decimal value)
        {
            return AddInternal(name, value, this.decimalType ?? (this.decimalType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Decimal)));
        }

        #endregion

        #region _IMetadatumDefinitionParameterCollection Members

        IMetadatumDefinitionParameter _IMetadatumDefinitionParameterCollection.AddInternal<T>(T value)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    return this.AddInternal(value, this.booleanType ?? (this.booleanType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Boolean, this.OwningAssembly)));
                case TypeCode.Byte:
                    return this.AddInternal(value, this.byteType ?? (this.byteType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Byte, this.OwningAssembly)));
                case TypeCode.Char:
                    return this.AddInternal(value, this.charType ?? (this.charType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Char, this.OwningAssembly)));
                //case TypeCode.DateTime:
                //    return this.AddInternal(value, this.DateType ?? (this.DateType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Date, this.OwningAssembly)));
                case TypeCode.Decimal:
                    return this.AddInternal(value, this.decimalType ?? (this.decimalType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Decimal, this.OwningAssembly)));
                case TypeCode.Double:
                    return this.AddInternal(value, this.doubleType ?? (this.doubleType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Double, this.OwningAssembly)));
                case TypeCode.Int16:
                    return this.AddInternal(value, this.int16Type ?? (this.int16Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int16, this.OwningAssembly)));
                case TypeCode.Int32:
                    return this.AddInternal(value, this.int32Type ?? (this.int32Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int32, this.OwningAssembly)));
                case TypeCode.Int64:
                    return this.AddInternal(value, this.int64Type ?? (this.int64Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int64, this.OwningAssembly)));
                case TypeCode.SByte:
                    return this.AddInternal(value, this.sbyteType ?? (this.sbyteType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.SByte, this.OwningAssembly)));
                case TypeCode.Single:
                    return this.AddInternal(value, this.singleType ?? (this.singleType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Single, this.OwningAssembly)));
                case TypeCode.String:
                    return this.AddInternal(value, this.stringType ?? (this.stringType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.String, this.OwningAssembly)));
                case TypeCode.UInt16:
                    return this.AddInternal(value, this.uint16Type ?? (this.uint16Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt16, this.OwningAssembly)));
                case TypeCode.UInt32:
                    return this.AddInternal(value, this.uint32Type ?? (this.uint32Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt32, this.OwningAssembly)));
                case TypeCode.UInt64:
                    return this.AddInternal(value, this.uint64Type ?? (this.uint64Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt64, this.OwningAssembly)));
                case TypeCode.Object:
                    if (typeof(T) == typeof(IExpression))
                        return this.AddInternal((IExpression)value);
                    else if (typeof(T) == typeof(IType))
                        return this.AddInternal(value, this.itypeType ?? (this.itypeType = this.IdentityManager.ObtainTypeReference(typeof(IType))));
                    break;
            }
            throw new InvalidOperationException("Type code not supported.");
        }

        IMetadatumDefinitionParameter _IMetadatumDefinitionParameterCollection.AddInternal<T>(string name, T value)
        {

            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    return this.AddInternal(name, value, this.booleanType ?? (this.booleanType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Boolean, this.OwningAssembly)));
                case TypeCode.Byte:
                    return this.AddInternal(name, value, this.byteType ?? (this.byteType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Byte, this.OwningAssembly)));
                case TypeCode.Char:
                    return this.AddInternal(name, value, this.charType ?? (this.charType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Char, this.OwningAssembly)));
                //case TypeCode.DateTime:
                //    return this.AddInternal(name, value, this.DateType ?? (this.DateType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Date, this.OwningAssembly)));
                case TypeCode.Decimal:
                    return this.AddInternal(name, value, this.decimalType ?? (this.decimalType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Decimal, this.OwningAssembly)));
                case TypeCode.Double:
                    return this.AddInternal(name, value, this.doubleType ?? (this.doubleType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Double, this.OwningAssembly)));
                case TypeCode.Int16:
                    return this.AddInternal(name, value, this.int16Type ?? (this.int16Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int16, this.OwningAssembly)));
                case TypeCode.Int32:
                    return this.AddInternal(name, value, this.int32Type ?? (this.int32Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int32, this.OwningAssembly)));
                case TypeCode.Int64:
                    return this.AddInternal(name, value, this.int64Type ?? (this.int64Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int64, this.OwningAssembly)));
                case TypeCode.SByte:
                    return this.AddInternal(name, value, this.sbyteType ?? (this.sbyteType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.SByte, this.OwningAssembly)));
                case TypeCode.Single:
                    return this.AddInternal(name, value, this.singleType ?? (this.singleType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.Single, this.OwningAssembly)));
                case TypeCode.String:
                    return this.AddInternal(name, value, this.stringType ?? (this.stringType = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.String, this.OwningAssembly)));
                case TypeCode.UInt16:
                    return this.AddInternal(name, value, this.uint16Type ?? (this.uint16Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt16, this.OwningAssembly)));
                case TypeCode.UInt32:
                    return this.AddInternal(name, value, this.uint32Type ?? (this.uint32Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt32, this.OwningAssembly)));
                case TypeCode.UInt64:
                    return this.AddInternal(name, value, this.uint64Type ?? (this.uint64Type = this.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt64, this.OwningAssembly)));
            }
            throw new InvalidOperationException("Type code not supported.");
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

        #region IMetadataDefinitionParameterCollection Members

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
        public event EventHandler<EventArgsR1<IMetadatumDefinitionNamedParameter>> NamedParameterChangedName;

        /// <summary>
        /// Occurs when a named parameter changed its value.
        /// </summary>
        /// <remarks><para>Dynamic attribute wrapper uses this to keep its wrapped instance up to date.</para>
        /// <para>When a named parameter's value changes, the value on the wrapper can simply be updated.</para></remarks>
        public event EventHandler<EventArgsR1<IMetadatumDefinitionNamedParameter>> NamedParameterChangedValue;

        /// <summary>
        /// Clears the parameters contained within the
        /// <see cref="IMetadataDefinitionParameterCollection"/>.
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
                throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
            int _index = 0;
            foreach (var item in this)
                if (!(item is IMetadatumDefinitionNamedParameter))
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
            if (name == string.Empty)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.name, ExceptionMessageId.ArgumentCannotBeEmpty, ThrowHelper.GetArgumentName(ArgumentWithException.name));
            if (!this.namedParameterNames.Contains(name))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.name, ExceptionMessageId.DuplicateKeyExists);
            foreach (var item in this)
                if (item is IMetadatumDefinitionNamedParameter && ((IMetadatumDefinitionNamedParameter) (item)).Name == name)
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

        internal void OnItemRename<T>(MetadatumDefinitionNamedParameter<T> item, string oldName, string newName)
        {
            if (this.namedParameterNames.Contains(oldName))
            {
                this.OnNamedParameterChangedName(item);
            }
            else //No record of it.
                throw new ArgumentException("item");
        }

        internal void OnItemValueChanged<T>(MetadatumDefinitionParameter<T> item)
        {
            if (item is IMetadatumDefinitionNamedParameter)
                this.OnNamedParameterChangedValue((IMetadatumDefinitionNamedParameter) item);
            else
                this.OnNamelessParametersChanged(EventArgs.Empty);
        }

        private void OnNamedParameterChangedValue(IMetadatumDefinitionNamedParameter item)
        {
            var namedParameterChangedValue = this.NamedParameterChangedValue;
            if (namedParameterChangedValue != null)
                namedParameterChangedValue(this, new EventArgsR1<IMetadatumDefinitionNamedParameter>(item));
        }

        private void OnNamedParameterChangedName(IMetadatumDefinitionNamedParameter item)
        {
            var namedParameterChangedName = this.NamedParameterChangedName;

            if (namedParameterChangedName != null)
                namedParameterChangedName(this, new EventArgsR1<IMetadatumDefinitionNamedParameter>(item));
        }

        private IIntermediateIdentityManager IdentityManager { get { return this.OwningAssembly.IdentityManager; } }
    }
}
