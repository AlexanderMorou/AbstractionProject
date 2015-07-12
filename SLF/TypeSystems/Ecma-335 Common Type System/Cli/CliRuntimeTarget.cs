using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliRuntimeTarget :
        ICliRuntimeTarget
    {
        private CliFrameworkVersion version;

        public CliRuntimeTarget(CliFrameworkVersion version, bool useCoreLibrary = true)
        {
            this.version = version;
        }

        public IAssemblyUniqueIdentifier CoreLibraryIdentifier
        {
            get
            {
                return CliGateway.GetCoreLibraryIdentifier(this.version);
            }
        }

        #region IStandardRuntimeEnvironmentInfo Members

        public IGeneralTypeUniqueIdentifier GetCoreIdentifier(RuntimeCoreType coreType)
        {
            string typeName;
            switch (coreType)
            {
                case RuntimeCoreType.Array:
                    typeName = "Array";
                    break;
                case RuntimeCoreType.Boolean:
                    typeName = "Boolean";
                    break;
                case RuntimeCoreType.Decimal:
                    typeName = "Decimal";
                    break;
                case RuntimeCoreType.Single:
                    typeName = "Single";
                    break;
                case RuntimeCoreType.Double:
                    typeName = "Double";
                    break;
                case RuntimeCoreType.SByte:
                    typeName = "SByte";
                    break;
                case RuntimeCoreType.Byte:
                    typeName = "Byte";
                    break;
                case RuntimeCoreType.Char:
                    typeName = "Char";
                    break;
                case RuntimeCoreType.RootEnum:
                    typeName = "Enum";
                    break;
                case RuntimeCoreType.Int16:
                    typeName = "Int16";
                    break;
                case RuntimeCoreType.UInt16:
                    typeName = "UInt16";
                    break;
                case RuntimeCoreType.Int32:
                    typeName = "Int32";
                    break;
                case RuntimeCoreType.UInt32:
                    typeName = "UInt32";
                    break;
                case RuntimeCoreType.Int64:
                    typeName = "Int64";
                    break;
                case RuntimeCoreType.UInt64:
                    typeName = "UInt64";
                    break;
                case RuntimeCoreType.RootType:
                    typeName = "Object";
                    break;
                case RuntimeCoreType.String:
                    typeName = "String";
                    break;
                case RuntimeCoreType.RootStruct:
                    typeName = "ValueType";
                    break;
                case RuntimeCoreType.VoidType:
                    typeName = "VoidType";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("coreType");
                    break;
            }
            if (UseCoreLibrary)
                return CoreLibraryIdentifier.GetTypeIdentifier("System", typeName, 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", typeName, 0);                
        }

        public IEnumerable<IType> GetCoreTypeInterfaces(RuntimeCoreType coreType)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region ICliRuntimeTarget Members

        public CliFrameworkVersion Version
        {
            get { return this.version; }
        }

        #endregion

        #region ICliRuntimeTarget Members

        public bool UseCoreLibrary { get; private set; }

        #endregion
    }
}
