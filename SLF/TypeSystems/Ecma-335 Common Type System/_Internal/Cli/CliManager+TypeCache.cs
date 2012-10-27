using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliManager
    {
        private class TypeCache
        {
            private CliManager manager;
            private MultikeyedDictionary<IType, TypeElementClassification, IType> classifiedTypeCache;
            private Dictionary<IType, TypeArrayCache> arrayCache = new Dictionary<IType,TypeArrayCache>();
            private Dictionary<IType, TypeModifiedCache> modifiedCache = new Dictionary<IType,TypeModifiedCache>();
            internal IDictionary<ICliMetadataTypeDefinitionTableRow, TypeKind> typeKindCache = new Dictionary<ICliMetadataTypeDefinitionTableRow, TypeKind>();
            internal IDictionary<ICliMetadataTypeDefinitionTableRow, IType> metadataTypeCache = new Dictionary<ICliMetadataTypeDefinitionTableRow, IType>();
            internal TypeCache(CliManager manager)
            {
                this.manager = manager;
            }

            public TypeArrayCache GetArrayCache(IType target)
            {
                if (target is _IType)
                    return (target as _IType).ArrayCache;
                TypeArrayCache result;
                lock (arrayCache)
                    if (!arrayCache.TryGetValue(target, out result))
                        arrayCache.Add(target, result = new TypeArrayCache(target, r => new ArrayType(target, r, this.manager), (lowerBounds, lengths) => new ArrayType(target, this.manager, lowerBounds, lengths)));
                return result;
            }

            public TypeModifiedCache GetModifiedTypeCache(IType target)
            {
                if (target is _IType)
                    return (target as _IType).ModifiedTypeCache;
                TypeModifiedCache result;
                lock (modifiedCache)
                    if (!modifiedCache.TryGetValue(target, out result))
                        modifiedCache.Add(target, result = new TypeModifiedCache());
                return result;
            }

            public IType MakeClassificationType(IType elementType, TypeElementClassification classification)
            {
                IType result;
                if (!classifiedTypeCache.TryGetValue(elementType, classification, out result))
                    switch (classification)
                    {
                        case TypeElementClassification.Nullable:
                            classifiedTypeCache.Add(elementType, classification, result = ((IGenericType)this.manager.ObtainTypeReference(this.manager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.NullableType))).MakeGenericClosure(elementType));
                            break;
                        case TypeElementClassification.Pointer:
                            classifiedTypeCache.Add(elementType, classification, result = new PointerType(elementType, this.manager));
                            break;
                        case TypeElementClassification.Reference:
                            classifiedTypeCache.Add(elementType, classification, result = new ByRefType(elementType, this.manager));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("classification");
                    }
                return result;
            }

            internal IType ObtainTypeReference(ICliMetadataTypeDefinitionTableRow typeIdentity)
            {
                if (CliCommon.IsSpecialModule(typeIdentity))
                    return null;
                IType result;
                lock (this.metadataTypeCache)
                {
                    if (!metadataTypeCache.TryGetValue(typeIdentity, out result))
                    {
                        var refAssem = this.manager.GetRelativeAssembly(typeIdentity.MetadataRoot);
                        if (CliCommon.IsBaseObject(this.manager, typeIdentity))
                            result = new M_T<IGeneralGenericTypeUniqueIdentifier, IClassType>(TypeKind.Class, typeIdentity, this.manager, refAssem);
                        else if ((typeIdentity.TypeAttributes & TypeAttributes.Interface) == TypeAttributes.Interface &&
                         (typeIdentity.TypeAttributes & TypeAttributes.Sealed) != TypeAttributes.Sealed)
                            result = new CliInterfaceType(refAssem, typeIdentity);
                        //result = new M_T<IGeneralGenericTypeUniqueIdentifier, IInterfaceType>(TypeKind.Interface, typeIdentity, this.manager, refAssem);
                        else if (CliCommon.IsBaseObject(this.manager, typeIdentity.Extends))
                            result = new M_T<IGeneralGenericTypeUniqueIdentifier, IClassType>(TypeKind.Class, typeIdentity, this.manager, refAssem);
                        else if (CliCommon.IsEnum(this.manager, typeIdentity))
                            result = new M_T<IGeneralTypeUniqueIdentifier>(TypeKind.Enumeration, typeIdentity, this.manager);
                        else if (CliCommon.IsValueType(this.manager, typeIdentity))
                            result = new M_T<IGeneralGenericTypeUniqueIdentifier, IStructType>(TypeKind.Struct, typeIdentity, this.manager, refAssem);
                        else if (CliCommon.IsDelegate(this.manager, typeIdentity))
                            result = new M_T<IGeneralGenericTypeUniqueIdentifier, IDelegateType>(TypeKind.Delegate, typeIdentity, this.manager, refAssem);
                        else
                            result = new M_T<IGeneralGenericTypeUniqueIdentifier, IClassType>(TypeKind.Class, typeIdentity, this.manager, refAssem);
                        this.metadataTypeCache.Add(typeIdentity, result);
                    }
                }
                return result;
            }
        }
    }
}
