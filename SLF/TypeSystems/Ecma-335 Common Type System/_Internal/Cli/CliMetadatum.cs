using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliMetadatum :
        IMetadatum
    {
        private ICliMetadataCustomAttributeTableRow metadataEntry;
        private ICliMetadataCustomAttribute metadataBlob;
        private IMetadataEntity declarationPoint;
        private _ICliManager identityManager;
        private IType sourceType;

        public CliMetadatum(ICliMetadataCustomAttributeTableRow metadataEntry, _ICliManager identityManager, IMetadataEntity declarationPoint)
        {
            this.declarationPoint = declarationPoint;
            this.metadataEntry = metadataEntry;
            this.identityManager = identityManager;
        }
        public IType Type
        {
            get
            {
                if (this.sourceType == null)
                    this.sourceType = OnGetType();
                return this.sourceType;
            }
        }

        private IType OnGetType()
        {
            if (this.metadataEntry == null)
                throw new InvalidOperationException();
            var metadataEntryCtor = metadataEntry.Ctor;
            if (metadataEntryCtor == null)
                throw new InvalidOperationException();
            ICliMetadataTypeDefinitionTableRow typeDef = null;
            switch (metadataEntryCtor.CustomAttributeTypeEncoding)
            {
                case CliMetadataCustomAttributeTypeTag.MethodDefinition:
                    ICliMetadataMethodDefinitionTableRow methodDefinition = (ICliMetadataMethodDefinitionTableRow)metadataEntryCtor;
                    break;
                case CliMetadataCustomAttributeTypeTag.MemberReference:
                    ICliMetadataMemberReferenceTableRow reference = (ICliMetadataMemberReferenceTableRow)metadataEntryCtor;
                    switch (reference.ClassSource)
                    {
                        case CliMetadataMemberRefParentTag.TypeDefinition:
                            typeDef = (ICliMetadataTypeDefinitionTableRow)reference.Class;
                            break;
                        case CliMetadataMemberRefParentTag.TypeReference:
                            typeDef = identityManager.ResolveScope((ICliMetadataTypeRefTableRow)reference.Class);
                            break;
                        case CliMetadataMemberRefParentTag.TypeSpecification:
                        case CliMetadataMemberRefParentTag.ModuleReference:
                        case CliMetadataMemberRefParentTag.MethodDefinition:
                        default:
                            throw new BadImageFormatException();
                    }
                    break;
            }

            if (typeDef == null)
                throw new BadImageFormatException();
            return identityManager.ObtainTypeReference(typeDef);
        }

        public IMetadataEntity DeclarationPoint
        {
            get
            {
                return this.declarationPoint;
            }
        }

        public ICliMetadataCustomAttribute MetadataBlob
        {
            get {
                if (this.metadataBlob == null)
                    this.metadataBlob = this.metadataEntry.GetValue(this.identityManager);
                return this.metadataBlob;
            }
        }

        public IEnumerable<Tuple<IType, object>> Parameters
        {
            get {
                foreach (var fixedParameter in this.MetadataBlob.FixedParameters)
                    switch (fixedParameter.ValueType)
                    {
                        case CustomAttributeParameterValueType.String:
                            yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.String), fixedParameter.Value);
                            break;
                        case CustomAttributeParameterValueType.BoxedNativeType:
                            yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.RootType), fixedParameter.Value);
                            break;
                        case CustomAttributeParameterValueType.EnumValue:
                            {
                                yield return Tuple.Create((IType)fixedParameter.Value.GetType().GetProperty("Item1").GetGetMethod().Invoke(fixedParameter.Value, null), fixedParameter.Value);
                                break;
                            }
                        case CustomAttributeParameterValueType.NativeType:
                            {
                                Type t = fixedParameter.Value.GetType();
                                if (t == typeof(int))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Int32), fixedParameter.Value);
                                else if (t == typeof(uint))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.UInt32), fixedParameter.Value);
                                else if (t == typeof(short))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Int16), fixedParameter.Value);
                                else if (t == typeof(ushort))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.UInt16), fixedParameter.Value);
                                else if (t == typeof(byte))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Byte), fixedParameter.Value);
                                else if (t == typeof(sbyte))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.SByte), fixedParameter.Value);
                                else if (t == typeof(long))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Int64), fixedParameter.Value);
                                else if (t == typeof(ulong))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.UInt64), fixedParameter.Value);
                                else if (t == typeof(float))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Single), fixedParameter.Value);
                                else if (t == typeof(double))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Double), fixedParameter.Value);
                                else if (t == typeof(object))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.RootType), fixedParameter.Value);
                                else if (t.IsGenericType)
                                {
                                    yield return Tuple.Create((IType)t.GetProperty("Item1").GetMethod.Invoke(fixedParameter.Value, null), t.GetProperty("Item2").GetMethod.Invoke(fixedParameter.Value, null));
                                }
                            }
                            break;
                        case CustomAttributeParameterValueType.Type:
                            yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Type), fixedParameter.Value);
                            break;
                        case CustomAttributeParameterValueType.VectorArray:
                            {
                                Type t = fixedParameter.Value.GetType();
                                /* *
                                 * The actual type of the value can't be used because
                                 * they could be looking at a different version of the
                                 * runtime.
                                 * */
                                int arrayLevel = 0;
                                while (t.IsArray)
                                {
                                    t = t.GetElementType();
                                    arrayLevel++;
                                }
                                IType rootType = null;
                                if (t == typeof(int))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Int32);
                                else if (t == typeof(uint))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt32);
                                else if (t == typeof(short))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Int16);
                                else if (t == typeof(ushort))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt16);
                                else if (t == typeof(byte))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Byte);
                                else if (t == typeof(sbyte))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.SByte);
                                else if (t == typeof(long))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Int64);
                                else if (t == typeof(ulong))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt64);
                                else if (t == typeof(float))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Single);
                                else if (t == typeof(double))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Double);
                                else if (t == typeof(object))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.RootType);
                                else if (t.IsGenericType)
                                    rootType = (IType)t.GetProperty("Item1").GetGetMethod().Invoke(fixedParameter.Value, null);

                                for (int i = 0; i < arrayLevel; i++)
                                    rootType = rootType.MakeArray();
                                yield return Tuple.Create(rootType, fixedParameter.Value);
                            }
                            break;
                        default:
                            break;
                    }
            }
        }

        public IEnumerable<Tuple<IType, string, object>> NamedParameters
        {
            get {
                foreach (var namedParameter in this.MetadataBlob.NamedParameters)
                    switch (namedParameter.ValueType)
                    {
                        case CustomAttributeParameterValueType.String:
                            yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.String), namedParameter.Name, namedParameter.Value);
                            break;
                        case CustomAttributeParameterValueType.BoxedNativeType:
                            yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.RootType), namedParameter.Name, namedParameter.Value);
                            break;
                        case CustomAttributeParameterValueType.EnumValue:
                            {
                                yield return Tuple.Create((IType)namedParameter.Value.GetType().GetProperty("Item1").GetGetMethod().Invoke(namedParameter.Value, null), namedParameter.Name, namedParameter.Value);
                                break;
                            }
                        case CustomAttributeParameterValueType.NativeType:
                            {
                                Type t = namedParameter.Value.GetType();
                                if (t == typeof(int))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Int32), namedParameter.Name, namedParameter.Value);
                                else if (t == typeof(uint))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.UInt32), namedParameter.Name, namedParameter.Value);
                                else if (t == typeof(short))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Int16), namedParameter.Name, namedParameter.Value);
                                else if (t == typeof(ushort))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.UInt16), namedParameter.Name, namedParameter.Value);
                                else if (t == typeof(byte))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Byte), namedParameter.Name, namedParameter.Value);
                                else if (t == typeof(sbyte))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.SByte), namedParameter.Name, namedParameter.Value);
                                else if (t == typeof(long))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Int64), namedParameter.Name, namedParameter.Value);
                                else if (t == typeof(ulong))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.UInt64), namedParameter.Name, namedParameter.Value);
                                else if (t == typeof(float))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Single), namedParameter.Name, namedParameter.Value);
                                else if (t == typeof(double))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Double), namedParameter.Name, namedParameter.Value);
                                else if (t == typeof(object))
                                    yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.RootType), namedParameter.Name, namedParameter.Value);
                                else if (t.IsGenericType)
                                {
                                    var item1 = (IType)t.GetProperty("Item1").GetGetMethod().Invoke(namedParameter.Value, null);
                                    yield return Tuple.Create(item1, namedParameter.Name, namedParameter.Value);
                                }
                            }
                            break;
                        case CustomAttributeParameterValueType.Type:
                            yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Type), namedParameter.Name, namedParameter.Value);
                            break;
                        case CustomAttributeParameterValueType.VectorArray:
                            {
                                Type t = namedParameter.Value.GetType();
                                int arrayLevel = 0;
                                while (t.IsArray)
                                {
                                    t = t.GetElementType();
                                    arrayLevel++;
                                }
                                IType rootType = null;
                                if (t == typeof(int))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Int32);
                                else if (t == typeof(uint))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt32);
                                else if (t == typeof(short))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Int16);
                                else if (t == typeof(ushort))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt16);
                                else if (t == typeof(byte))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Byte);
                                else if (t == typeof(sbyte))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.SByte);
                                else if (t == typeof(long))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Int64);
                                else if (t == typeof(ulong))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt64);
                                else if (t == typeof(float))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Single);
                                else if (t == typeof(double))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Double);
                                else if (t == typeof(object))
                                    rootType = identityManager.ObtainTypeReference(RuntimeCoreType.RootType);
                                else if (t.IsGenericType)
                                    rootType = (IType)t.GetProperty("Item1").GetGetMethod().Invoke(namedParameter.Value, null);
                                for (int i = 0; i < arrayLevel; i++)
                                    rootType = rootType.MakeArray();
                                yield return Tuple.Create(rootType, namedParameter.Name, namedParameter.Value);
                            }
                            break;
                        default:
                            break;
                    }
            }
        }

        public void Dispose()
        {
            this.metadataEntry = null;
            this.declarationPoint = null;
        }
    }
}
