using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Lazy<IEnumerable<Tuple<IType, object>>> parameters;
        private Lazy<IEnumerable<Tuple<IType, string, object>>> namedParameters;

        public CliMetadatum(ICliMetadataCustomAttributeTableRow metadataEntry, _ICliManager identityManager, IMetadataEntity declarationPoint)
        {
            this.declarationPoint = declarationPoint;
            this.metadataEntry = metadataEntry;
            this.identityManager = identityManager;
            this.parameters = new Lazy<IEnumerable<Tuple<IType, object>>>(() => this.GetParameters().SinglePass(), true);
            this.namedParameters = new Lazy<IEnumerable<Tuple<IType, string, object>>>(() => this.GetNamedParameters().SinglePass(), true); ;
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
                    var tdTable = metadataEntry.MetadataRoot.TableStream.TypeDefinitionTable;
                    for (int i = 1, c = tdTable.Count; i <= c; i++)
                    {
                        if (i == c)
                        {
                            typeDef = tdTable[i];
                            break;
                        }
                        else
                        {
                            var current = tdTable[i];
                            var next = tdTable[i + 1];
                            if (methodDefinition.Index >= current.MethodStartIndex && methodDefinition.Index < next.MethodStartIndex)
                            {
                                typeDef = current;
                                break;
                            }
                        }
                    }
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
                return this.parameters.Value;
            }
        }

        internal IType ActiveType
        {
            get
            {
                if (this.DeclarationPoint is IType)
                {
                    return (IType)this.DeclarationPoint;
                }
                else if (this.DeclarationPoint is IMember)
                {
                    var member = (IMember)this.DeclarationPoint;
                    if (member.Parent is IType)
                        return (IType)member.Parent;
                }
                return null;
            }
        }

        private IEnumerable<Tuple<IType, object>> GetParameters()
        {
            var fixedParameterValues = this.MetadataBlob.FixedParameters.ToArray();
            var fixedParameters = this.metadataEntry.GetParameterCollection();
            for (int i = 0; i < fixedParameterValues.Length; i++)
            {
                var fixedParameterValue = fixedParameterValues[i];
                var fixedParameter = fixedParameters[i];
                yield return Tuple.Create(identityManager.ObtainTypeReference(fixedParameter.ParameterType, this.ActiveType, null), fixedParameterValue.Value);
            }
        }

        public IEnumerable<Tuple<IType, string, object>> NamedParameters
        {
            get {
                return this.namedParameters.Value;
            }
        }

        private IEnumerable<Tuple<IType, string, object>> GetNamedParameters()
        {
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
                            else if (t == typeof(string))
                                yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.String), namedParameter.Name, namedParameter.Value);
                            else if (t == typeof(bool))
                                yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.Boolean), namedParameter.Name, namedParameter.Value);
                            else if (t == typeof(object))
                                yield return Tuple.Create(identityManager.ObtainTypeReference(RuntimeCoreType.RootType), namedParameter.Name, namedParameter.Value);
                            else if (t.IsGenericType)
                            {
                                var tupleValue = (Tuple<IType, object>)namedParameter.Value;
                                yield return Tuple.Create(tupleValue.Item1, namedParameter.Name, tupleValue.Item2);
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
                            else if (t == typeof(string))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.String);
                            else if (t == typeof(bool))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Boolean);
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
                            else if (t == typeof(IType))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Type);
                            else if (t.IsGenericType)
                            {
                                rootType = (IType)t.GetProperty("Item1").GetMethod.Invoke(namedParameter.Value, null);
                                var value = t.GetProperty("Item2").GetMethod.Invoke(namedParameter.Value, null);
                                t = value.GetType();
                                while (t.IsArray)
                                {
                                    t = t.GetElementType();
                                    rootType = rootType.MakeArray();
                                }
                                yield return Tuple.Create(rootType, namedParameter.Name, value);
                                continue;
                            }
                            for (int i = 0; i < arrayLevel; i++)
                                rootType = rootType.MakeArray();
                            yield return Tuple.Create(rootType, namedParameter.Name, namedParameter.Value);
                        }
                        break;
                    default:
                        break;
                }
        }

        public override string ToString()
        {
            if (this.MetadataBlob.FixedParameters.Count == 0)
                if (this.MetadataBlob.NamedParameters.Count == 0)
                    return string.Format("{0}()", Type);
                else
                    return string.Format("{0}({1})", Type, NamedParametersString);
            else if (this.MetadataBlob.NamedParameters.Count == 0)
                return string.Format("{0}({1})", Type, ParametersString);
            return string.Format("{0}({1}, {2})", Type, ParametersString, NamedParametersString);
        }

        public void Dispose()
        {
            this.metadataEntry = null;
            this.declarationPoint = null;
        }
        internal string ParametersString
        {
            get
            {
                StringBuilder result = new StringBuilder();
                bool first = true;
                foreach (var parameter in this.Parameters)
                {
                    if (first)
                        first = false;
                    else
                        result.Append(", ");
                    result.AppendFormat("({0}){1}", parameter.Item1, parameter.Item2);
                }
                return result.ToString();
            }
        }
        internal string NamedParametersString
        {
            get
            {
                StringBuilder result = new StringBuilder();
                bool first = true;
                foreach (var parameter in this.NamedParameters)
                {
                    if (first)
                        first = false;
                    else
                        result.Append(", ");
                    result.AppendFormat("{0}=({1}){2}", parameter.Item2, parameter.Item1, parameter.Item3);
                }
                return result.ToString();
            }
        }
    }
}
