using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
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
        private Lazy<IEnumerable<MetadatumTypedParameter>> parameters;
        private Lazy<IEnumerable<MetadatumNamedParameter>> namedParameters;

        public CliMetadatum(ICliMetadataCustomAttributeTableRow metadataEntry, _ICliManager identityManager, IMetadataEntity declarationPoint)
        {
            this.declarationPoint = declarationPoint;
            this.metadataEntry = metadataEntry;
            this.identityManager = identityManager;
            this.parameters = new Lazy<IEnumerable<MetadatumTypedParameter>>(() => this.GetParameters().SinglePass(), true);
            this.namedParameters = new Lazy<IEnumerable<MetadatumNamedParameter>>(() => this.GetNamedParameters().SinglePass(), true); ;
            
        }

        public CliMetadatum(ICliMetadataCustomAttributeTableRow metadataEntry, _ICliAssembly owner)
        {
            this.declarationPoint = owner;
            this.metadataEntry = metadataEntry;
            this.identityManager = owner.IdentityManager;
            this.parameters = new Lazy<IEnumerable<MetadatumTypedParameter>>(() => this.GetParameters().SinglePass(), true);
            this.namedParameters = new Lazy<IEnumerable<MetadatumNamedParameter>>(() => this.GetNamedParameters().SinglePass(), true); ;
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
                    uint typeIndex = tdTable.GetTypeFromMethodIndex(methodDefinition.Index);
                    typeDef = tdTable[(int)typeIndex];
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

        public IEnumerable<MetadatumTypedParameter> Parameters
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

        private IEnumerable<MetadatumTypedParameter> GetParameters()
        {
            var fixedParameterValues = this.MetadataBlob.FixedParameters.ToArray();
            var fixedParameters = this.metadataEntry.GetParameterCollection();
            for (int i = 0; i < fixedParameterValues.Length; i++)
            {
                var fixedParameterValue = fixedParameterValues[i];
                var fixedParameter = fixedParameters[i];
                yield return new MetadatumTypedParameter { ParameterType = identityManager.ObtainTypeReference(fixedParameter.ParameterType, this.ActiveType, null, this.declarationPoint as IAssembly), Value = fixedParameterValue.Value };
            }
        }

        public IEnumerable<MetadatumNamedParameter> NamedParameters
        {
            get {
                return this.namedParameters.Value;
            }
        }

        private IEnumerable<MetadatumNamedParameter> GetNamedParameters()
        {
            foreach (var namedParameter in this.MetadataBlob.NamedParameters)
                switch (namedParameter.ValueType)
                {
                    case CustomAttributeParameterValueType.String:
                        yield return new MetadatumNamedParameter{ ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.String, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };//, namedParameter.Name, namedParameter.Value);
                        break;
                    case CustomAttributeParameterValueType.BoxedNativeType:
                        yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.RootType, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                        break;
                    case CustomAttributeParameterValueType.EnumValue:
                        yield return  new MetadatumNamedParameter{ ParameterType = (IType)namedParameter.Value.GetType().GetProperty("Item1").GetGetMethod().Invoke(namedParameter.Value, null), MemberName = namedParameter.Name, Value = namedParameter.Value };
                        break;
                    case CustomAttributeParameterValueType.NativeType:
                        {
                            Type t = namedParameter.Value.GetType();
                            if (t == typeof(int))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.Int32, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(uint))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt32, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(short))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.Int16, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(ushort))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt16, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(byte))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.Byte, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(sbyte))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.SByte, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(long))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.Int64, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(ulong))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt64, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(float))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.Single, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(double))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.Double, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(string))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.String, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(bool))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.Boolean, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t == typeof(object))
                                yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.RootType, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
                            else if (t.IsGenericType)
                            {
                                var tupleValue = (Tuple<IType, object>)namedParameter.Value;
                                yield return new MetadatumNamedParameter { ParameterType = tupleValue.Item1, MemberName = namedParameter.Name, Value = tupleValue.Item2 };
                            }
                        }
                        break;
                    case CustomAttributeParameterValueType.Type:
                        yield return new MetadatumNamedParameter { ParameterType = identityManager.ObtainTypeReference(RuntimeCoreType.Type, this.ActiveAssembly), MemberName = namedParameter.Name, Value = namedParameter.Value };
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
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Int32, this.ActiveAssembly);
                            else if (t == typeof(uint))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt32, this.ActiveAssembly);
                            else if (t == typeof(string))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.String, this.ActiveAssembly);
                            else if (t == typeof(bool))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Boolean, this.ActiveAssembly);
                            else if (t == typeof(short))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Int16, this.ActiveAssembly);
                            else if (t == typeof(ushort))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt16, this.ActiveAssembly);
                            else if (t == typeof(byte))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Byte, this.ActiveAssembly);
                            else if (t == typeof(sbyte))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.SByte, this.ActiveAssembly);
                            else if (t == typeof(long))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Int64, this.ActiveAssembly);
                            else if (t == typeof(ulong))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt64, this.ActiveAssembly);
                            else if (t == typeof(float))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Single, this.ActiveAssembly);
                            else if (t == typeof(double))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Double, this.ActiveAssembly);
                            else if (t == typeof(object))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.RootType, this.ActiveAssembly);
                            else if (t == typeof(IType))
                                rootType = identityManager.ObtainTypeReference(RuntimeCoreType.Type, this.ActiveAssembly);
                            else if (t.IsGenericType)
                            {
                                rootType = (IType)t.GetProperty("Item1").GetGetMethod(false).Invoke(namedParameter.Value, null);
                                var value = t.GetProperty("Item2").GetGetMethod(false).Invoke(namedParameter.Value, null);
                                t = value.GetType();
                                while (t.IsArray)
                                {
                                    t = t.GetElementType();
                                    rootType = rootType.MakeArray();
                                }
                                yield return new MetadatumNamedParameter { ParameterType = rootType, MemberName = namedParameter.Name, Value = value };
                                continue;
                            }
                            for (int i = 0; i < arrayLevel; i++)
                                rootType = rootType.MakeArray();
                            yield return new MetadatumNamedParameter { ParameterType = rootType, MemberName = namedParameter.Name, Value = namedParameter.Value };
                        }
                        break;
                    default:
                        break;
                }
        }

        public IAssembly ActiveAssembly
        {
            get
            {
                return this.ActiveType == null ? this.declarationPoint as IAssembly : this.ActiveType.Assembly;
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
                    result.AppendFormat("({0}){1}", parameter.ParameterType, parameter.Value);
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
                    result.AppendFormat("{0}=({1}){2}", parameter.MemberName, parameter.ParameterType, parameter.Value);
                }
                return result.ToString();
            }
        }
    }
}
