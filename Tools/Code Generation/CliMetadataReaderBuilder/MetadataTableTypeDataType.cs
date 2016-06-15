using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace CliMetadataReader
{
    public class MetadataTableTypeDataType :
        IMetadataTableFieldDataType
    {
        private static readonly IType[] primitives = new IType[0];
        private static readonly Dictionary<IType, int> primitiveSizes;
        static MetadataTableTypeDataType()
        {
            var cliManager = IntermediateCliGateway.GlobalManager;
            var primitiveSizeLookup = new Dictionary<IGeneralTypeUniqueIdentifier, int>()
                {
                    { cliManager.RuntimeEnvironment.Boolean,    sizeof(bool)},
                    { cliManager.RuntimeEnvironment.Byte,       sizeof(byte)},
                    { cliManager.RuntimeEnvironment.Char,       sizeof(char)},
                    { cliManager.RuntimeEnvironment.Decimal,    sizeof(decimal)},
                    { cliManager.RuntimeEnvironment.Double,     sizeof(double)},
                    { cliManager.RuntimeEnvironment.Int16,      sizeof(short)},
                    { cliManager.RuntimeEnvironment.Int32,      sizeof(int)}, 
                    { cliManager.RuntimeEnvironment.Int64,      sizeof(long)},
                    { cliManager.RuntimeEnvironment.SByte,      sizeof(sbyte)},
                    { cliManager.RuntimeEnvironment.Single,     sizeof(float)},
                    { cliManager.RuntimeEnvironment.UInt16,     sizeof(ushort)},
                    { cliManager.RuntimeEnvironment.UInt32,     sizeof(uint)},
                    { cliManager.RuntimeEnvironment.UInt64,     sizeof(ulong) }
                };
            var primitives = 
                new[] 
                { 
                    cliManager.RuntimeEnvironment.Boolean,
                    cliManager.RuntimeEnvironment.Byte,
                    cliManager.RuntimeEnvironment.Char,
                    cliManager.RuntimeEnvironment.Decimal,
                    cliManager.RuntimeEnvironment.Double,
                    cliManager.RuntimeEnvironment.Int16,
                    cliManager.RuntimeEnvironment.Int32,
                    cliManager.RuntimeEnvironment.Int64,
                    cliManager.RuntimeEnvironment.SByte,
                    cliManager.RuntimeEnvironment.Single,
                    cliManager.RuntimeEnvironment.UInt16,
                    cliManager.RuntimeEnvironment.UInt32,
                    cliManager.RuntimeEnvironment.UInt64,
                }.Select(k => cliManager.ObtainTypeReference(k)).ToArray();
            MetadataTableTypeDataType.primitives = primitives;
            MetadataTableTypeDataType.primitiveSizes =
                primitives.ToDictionary(k => k, v => primitiveSizeLookup[v.UniqueIdentifier]);
        }


        public IType DataType { get; private set; }

        public bool SelfSufficientType { get; private set; }

        public MetadataTableTypeDataType(IType dataType)
        {
            this.DataType = dataType;
        }

        public MetadataTableTypeDataType(IType dataType, bool selfSufficient)
        {
            this.DataType = dataType;
            this.SelfSufficientType = selfSufficient;
        }

        public MetadataTableTypeDataType(IType dataType, IType castType)
        {
            this.DataType = dataType;
            this.CastType = castType;
        }

        public IType CastType { get; private set; }

        public override string ToString()
        {
            if (this.CastType != null)
                return string.Format("{1} as {0}", this.CastType, this.DataType);
            else
                return this.DataType.ToString();
        }


        #region IMetadataTableFieldDataType Members

        public FieldDataKind DataKind
        {
            get
            {
                if (this.SelfSufficientType)
                    return FieldDataKind.SelfsufficientDataType;
                else if (this.CastType != null)
                    return FieldDataKind.CastDataType;
                else
                    return FieldDataKind.DataType;
            }
        }

        #endregion

        private static int GetTypeSize(IType type)
        {
            if (type is IEnumType)
            {
                IEnumType targetType = (IEnumType)type;
                switch (targetType.ValueType)
                {
                    case EnumerationBaseType.SByte:
                        return GetTypeSize(IntermediateCliGateway.GlobalManager.ObtainTypeReference(RuntimeCoreType.SByte));
                    case EnumerationBaseType.Byte:
                        return GetTypeSize(IntermediateCliGateway.GlobalManager.ObtainTypeReference(RuntimeCoreType.Byte));
                    case EnumerationBaseType.Int16:
                        return GetTypeSize(IntermediateCliGateway.GlobalManager.ObtainTypeReference(RuntimeCoreType.Int16));
                    case EnumerationBaseType.UInt16:
                        return GetTypeSize(IntermediateCliGateway.GlobalManager.ObtainTypeReference(RuntimeCoreType.UInt16));
                    case EnumerationBaseType.Int32:
                        return GetTypeSize(IntermediateCliGateway.GlobalManager.ObtainTypeReference(RuntimeCoreType.Int32));
                    case EnumerationBaseType.UInt32:
                        return GetTypeSize(IntermediateCliGateway.GlobalManager.ObtainTypeReference(RuntimeCoreType.UInt32));
                    case EnumerationBaseType.Int64:
                        return GetTypeSize(IntermediateCliGateway.GlobalManager.ObtainTypeReference(RuntimeCoreType.Int64));
                    case EnumerationBaseType.UInt64:
                        return GetTypeSize(IntermediateCliGateway.GlobalManager.ObtainTypeReference(RuntimeCoreType.UInt64));
                    case EnumerationBaseType.NativeInteger:
                        return Marshal.SizeOf(typeof(IntPtr));
                    case EnumerationBaseType.NativeUnsignedInteger:
                        return Marshal.SizeOf(typeof(UIntPtr));
                    case EnumerationBaseType.Default:
                    default:
                        goto case EnumerationBaseType.Int32;
                }
            }
            else if (primitiveSizes.ContainsKey(type))
            {
                return primitiveSizes[type];
            }
            else if (type is ICliType)
            {
                var typeName = type.FullName;
                var assemblyName = type.Assembly.ToString();
                var assem = Assembly.Load(new AssemblyName(assemblyName));
                var targetType = assem.GetType(typeName);
                return Marshal.SizeOf(targetType);
            }
            throw new ArgumentOutOfRangeException("type");
        }

        public int GetSize()
        {

            if (this.CastType != null)
                return GetTypeSize(this.CastType);
            else
                return GetTypeSize(this.DataType);
                //return Marshal.SizeOf(((IExternTypeReference)this.DataType).TypeInstance.Type);
        }
    }
}
