﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal static class SignatureParser
    {
        internal static T Parse<T>(SignatureKinds signatureKind, uint heapIndex, CliMetadataRoot metadataRoot)
            where T :
                ICliMetadataSignature
        {

            MemoryStream ms = new MemoryStream(metadataRoot.BlobHeap[heapIndex]);
            EndianAwareBinaryReader heapReader = new EndianAwareBinaryReader(ms, Endianness.LittleEndian, false);
            try
            {
                switch (signatureKind)
                {
                    case SignatureKinds.MethodDefSig:
                        return (T) ParseMethodDefSig(heapReader, metadataRoot);
                    case SignatureKinds.MethodRefSig:
                        return (T) ParseMethodRefSig(heapReader, metadataRoot);
                    case SignatureKinds.FieldSig:
                        return (T) ParseFieldSig(heapReader, metadataRoot);
                    case SignatureKinds.PropertySig:
                        return (T) ParsePropertySig(heapReader, metadataRoot);
                    case SignatureKinds.StandaloneSignature:
                        return (T) ParseStandaloneSig(heapReader, metadataRoot);
                    case SignatureKinds.CustomModifier:
                        return (T) ParseCustomModifier(heapReader, metadataRoot);
                    case SignatureKinds.Param:
                        return (T) ParseParam(heapReader, metadataRoot);
                    case SignatureKinds.Type:
                        return (T) ParseType(heapReader, metadataRoot);
                    case SignatureKinds.ArrayShape:
                        return (T) ParseArrayShape(heapReader, metadataRoot);
                    case SignatureKinds.TypeSpec:
                        return (T) ParseTypeSpec(heapReader, metadataRoot);
                    case SignatureKinds.MethodSpec:
                        return (T) ParseMethodSpec(heapReader, metadataRoot);
                    default:
                        throw new ArgumentOutOfRangeException("signatureKind");
                }
            }
            finally
            {
                heapReader.Close();
                heapReader.Dispose();
                ms.Close();
                ms.Dispose();
            }
        }

        internal static ICliMetadataStandaloneSignature ParseStandaloneSig(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            var prolog = (SignatureKinds) (reader.PeekChar() & 0xFF);
            if (prolog == SignatureKinds.StandaloneLocalVarSig)
                return ParseLocalVarSig(reader, metadataRoot);
            else if (prolog == SignatureKinds.FieldSig)
                //return ParseLocalVarSig(reader, metadataRoot);
                return ParseFieldSig(reader, metadataRoot);
            else
                return ParseStandaloneMethodSig(reader, metadataRoot);
        }

        internal static ICliMetadataMethodSignature ParseMethodSignature(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot, bool canHaveRefContext = true)
        {
            const CliMetadataMethodSigFlags legalFlags = CliMetadataMethodSigFlags.HasThis | CliMetadataMethodSigFlags.ExplicitThis;
            const CliMetadataMethodSigConventions legalConventions = CliMetadataMethodSigConventions.Default | CliMetadataMethodSigConventions.VariableArguments | CliMetadataMethodSigConventions.Generic | CliMetadataMethodSigConventions.StdCall | CliMetadataMethodSigConventions.FastCall | CliMetadataMethodSigConventions.Cdecl;
            const int legalFirst = (int) legalFlags | (int) legalConventions;
            byte firstByte = reader.ReadByte();
            if ((firstByte & legalFirst) == 0 && firstByte != 0)
                throw new BadImageFormatException("Unknown calling convention encountered.");
            var callingConvention = ((CliMetadataMethodSigConventions) firstByte) & legalConventions;
            var flags = ((CliMetadataMethodSigFlags) firstByte) & legalFlags;

            int paramCount;
            int genericParamCount = 0;
            if ((callingConvention & CliMetadataMethodSigConventions.Generic) == CliMetadataMethodSigConventions.Generic)
                genericParamCount = CliMetadataRoot.ReadCompressedUnsignedInt(reader);
            paramCount = CliMetadataRoot.ReadCompressedUnsignedInt(reader);
            ICliMetadataReturnTypeSignature returnType = ParseReturnTypeSignature(reader, metadataRoot);
            bool sentinelEncountered = false;
            if (canHaveRefContext)
            {
                ICliMetadataVarArgParamSignature[] parameters = new ICliMetadataVarArgParamSignature[paramCount];
                for (int i = 0; i < parameters.Length; i++)
                {
                    byte nextByte = (byte) (reader.PeekChar() & 0xFF);
                    if (nextByte == (byte) CliMetadataMethodSigFlags.Sentinel)
                    {
                        if (!sentinelEncountered)
                        {
                            flags |= CliMetadataMethodSigFlags.Sentinel;
                            sentinelEncountered = true;
                            reader.ReadByte();
                        }
                    }
                    parameters[i] = (ICliMetadataVarArgParamSignature) ParseParam(reader, metadataRoot, true, sentinelEncountered);
                }
                return new CliMetadataMethodRefSignature(callingConvention, flags, returnType, parameters);
            }
            else
            {
                ICliMetadataParamSignature[] parameters = new ICliMetadataParamSignature[paramCount];
                for (int i = 0; i < parameters.Length; i++)
                    parameters[i] = ParseParam(reader, metadataRoot);
                return new CliMetadataMethodDefSignature(callingConvention, flags, returnType, parameters);
            }
        }

        internal static ICliMetadataMethodDefSignature ParseMethodDefSig(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            return (ICliMetadataMethodDefSignature) ParseMethodSignature(reader, metadataRoot, false);
        }

        internal static ICliMetadataMethodRefSignature ParseMethodRefSig(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            return (ICliMetadataMethodRefSignature) ParseMethodSignature(reader, metadataRoot);
        }

        internal static ICliMetadataStandAloneMethodSignature ParseStandaloneMethodSig(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            var firstByte = reader.ReadByte();
            var convention = ((CliMetadataMethodSigConventions) firstByte) & CliMetadataMethodSigConventions.Mask;
            var flags = ((CliMetadataMethodSigFlags) firstByte) & CliMetadataMethodSigFlags.Mask;
            if (flags == CliMetadataMethodSigFlags.SentinelLowBit)
                flags = CliMetadataMethodSigFlags.None;
            int paramCount = CliMetadataRoot.ReadCompressedUnsignedInt(reader);
            var returnType = ParseReturnTypeSignature(reader, metadataRoot);
            bool sentinelAllowed = convention == CliMetadataMethodSigConventions.VariableArguments || convention == CliMetadataMethodSigConventions.Cdecl;
            bool sentinelEncountered = false;
            ICliMetadataVarArgParamSignature[] parameters = new ICliMetadataVarArgParamSignature[paramCount];

            for (int i = 0; i < paramCount; i++)
            {
                byte peek = (byte) (reader.PeekChar() & 0xFF);
                if (peek == (byte) CliMetadataMethodSigFlags.Sentinel)
                {
                    if (sentinelAllowed && !sentinelEncountered)
                    {
                        flags |= CliMetadataMethodSigFlags.Sentinel;
                        sentinelEncountered = true;
                        reader.ReadByte();
                    }
                }
                parameters[i] = (ICliMetadataVarArgParamSignature) ParseParam(reader, metadataRoot, true, sentinelEncountered);
            }
            return new CliMetadataStandAloneMethodSignature(convention, flags, returnType, parameters);
        }

        internal static ICliMetadataReturnTypeSignature ParseReturnTypeSignature(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            List<ICliMetadataCustomModifierSignature> customModifiers = new List<ICliMetadataCustomModifierSignature>();
        parseNextCustomModifier:
            NativeTypes firstByte = (NativeTypes) (reader.PeekChar() & 0xFF);
            switch (firstByte)
            {
                case NativeTypes.RequiredModifier:
                case NativeTypes.OptionalModifier:
                    customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
                    goto parseNextCustomModifier;
                case NativeTypes.ByRef:
                    return new CliMetadataReturnTypeSignature(ParseType(reader, metadataRoot), customModifiers);
                case NativeTypes.TypedByReference:
                    return new CliMetadataReturnTypeSignature(new CliMetadataNativeTypeSignature(firstByte), customModifiers);
                case NativeTypes.Pointer:
                case NativeTypes.Type:
                case NativeTypes.Boolean:
                case NativeTypes.Char:
                case NativeTypes.SByte:
                case NativeTypes.Byte:
                case NativeTypes.Int16:
                case NativeTypes.UInt16:
                case NativeTypes.Int32:
                case NativeTypes.UInt32:
                case NativeTypes.Int64:
                case NativeTypes.UInt64:
                case NativeTypes.Single:
                case NativeTypes.Double:
                case NativeTypes.String:
                case NativeTypes.ValueType:
                case NativeTypes.Class:
                case NativeTypes.GenericTypeParameter:
                case NativeTypes.Array:
                case NativeTypes.GenericClosure:
                case NativeTypes.NativeInteger:
                case NativeTypes.NativeUnsignedInteger:
                case NativeTypes.FunctionPointer:
                case NativeTypes.Object:
                case NativeTypes.VectorArray:
                case NativeTypes.Void:
                case NativeTypes.MethodGenericParameter:
                    return new CliMetadataReturnTypeSignature(ParseType(reader, metadataRoot, true), customModifiers);
                default:
                    throw new BadImageFormatException("Invalid return type signature.");
            }
        }

        internal static ICliMetadataFieldSignature ParseFieldSig(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot, bool pinnedContext = false)
        {
            var prolog = (SignatureKinds) reader.ReadByte();
            if ((prolog & SignatureKinds.FieldSig) != SignatureKinds.FieldSig)
                throw new InvalidOperationException();
            List<ICliMetadataCustomModifierSignature> customModifiers = new List<ICliMetadataCustomModifierSignature>();
        parseNextModifier:
            NativeTypes nextChar = (NativeTypes) (reader.PeekChar() & 0xFF);
            switch (nextChar)
            {
                case NativeTypes.Pinned:
                    reader.ReadByte();
                    return new CliMetadataFieldSignature(ParseType(reader, metadataRoot), customModifiers, true);
                case NativeTypes.Boolean:
                case NativeTypes.Char:
                case NativeTypes.SByte:
                case NativeTypes.Byte:
                case NativeTypes.Int16:
                case NativeTypes.UInt16:
                case NativeTypes.Int32:
                case NativeTypes.UInt32:
                case NativeTypes.Int64:
                case NativeTypes.UInt64:
                case NativeTypes.Single:
                case NativeTypes.ByRef:
                case NativeTypes.Double:
                case NativeTypes.String:
                case NativeTypes.Pointer:
                case NativeTypes.ValueType:
                case NativeTypes.Class:
                case NativeTypes.GenericTypeParameter:
                case NativeTypes.Array:
                case NativeTypes.GenericClosure:
                case NativeTypes.TypedByReference:
                case NativeTypes.NativeInteger:
                case NativeTypes.NativeUnsignedInteger:
                case NativeTypes.FunctionPointer:
                case NativeTypes.Object:
                case NativeTypes.VectorArray:
                case NativeTypes.MethodGenericParameter:
                    return new CliMetadataFieldSignature(ParseType(reader, metadataRoot), customModifiers);
                case NativeTypes.RequiredModifier:
                case NativeTypes.OptionalModifier:
                    customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
                    goto parseNextModifier;
            }
            throw new BadImageFormatException("Signature not properly constructed.");
        }

        internal static ICliMetadataPropertySignature ParsePropertySig(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            NativeTypes firstChar = (NativeTypes) (reader.ReadByte() & 0xFF);
            if (firstChar != (NativeTypes) SignatureKinds.PropertySig &&
                firstChar != ((NativeTypes) SignatureKinds.PropertySig | (NativeTypes) CliMetadataMethodSigFlags.HasThis))
                throw new BadImageFormatException("Expected property or hasthis with property.");
            bool hasThis = (firstChar & (NativeTypes) CliMetadataMethodSigFlags.HasThis) == (NativeTypes) CliMetadataMethodSigFlags.HasThis;
            int paramCount = CliMetadataRoot.ReadCompressedUnsignedInt(reader);
            IList<ICliMetadataCustomModifierSignature> customModifiers = new List<ICliMetadataCustomModifierSignature>();
            ICliMetadataTypeSignature propertyType = null;
        nextChar:
            NativeTypes nextChar = (NativeTypes) (reader.PeekChar() & 0xFF);
            switch (nextChar)
            {
                case NativeTypes.OptionalModifier:
                case NativeTypes.RequiredModifier:
                    customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
                    goto nextChar;
                case NativeTypes.Boolean:
                case NativeTypes.Char:
                case NativeTypes.SByte:
                case NativeTypes.Byte:
                case NativeTypes.Int16:
                case NativeTypes.UInt16:
                case NativeTypes.Int32:
                case NativeTypes.UInt32:
                case NativeTypes.Int64:
                case NativeTypes.UInt64:
                case NativeTypes.Single:
                case NativeTypes.Double:
                case NativeTypes.String:
                case NativeTypes.NativeInteger:
                case NativeTypes.NativeUnsignedInteger:
                case NativeTypes.Object:
                case NativeTypes.Type:
                case NativeTypes.GenericClosure:
                case NativeTypes.Pointer:
                case NativeTypes.ValueType:
                case NativeTypes.Class:
                case NativeTypes.FunctionPointer:
                case NativeTypes.MethodGenericParameter:
                case NativeTypes.GenericTypeParameter:
                case NativeTypes.Array:
                case NativeTypes.VectorArray:
                case NativeTypes.ByRef:
                    propertyType = ParseType(reader, metadataRoot);
                    break;
                default:
                    throw new BadImageFormatException("Expected: TypeSignature");
            }
            ICliMetadataParamSignature[] parameters = new ICliMetadataParamSignature[paramCount];
            for (int i = 0; i < paramCount; i++)
                parameters[i] = ParseParam(reader, metadataRoot);
            return new CliMetadataPropertySignature(hasThis, parameters, customModifiers, propertyType);
        }

        internal static ICliMetadataLocalVarSignature ParseLocalVarSig(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            var prolog = (SignatureKinds) reader.ReadByte();
            if (prolog != SignatureKinds.StandaloneLocalVarSig)
                throw new BadImageFormatException("LocalVariableSig contains invalid leading byte");
            int count = CliMetadataRoot.ReadCompressedUnsignedInt(reader);
            ICliMetadataLocalVarEntrySignature[] localVariables = new ICliMetadataLocalVarEntrySignature[count];

            for (int i = 0; i < count; i++)
            {
                bool currentPinned = false;
                ICliMetadataTypeSignature currentType = null;
                List<ICliMetadataCustomModifierSignature> customModifiers = new List<ICliMetadataCustomModifierSignature>();
            parseNext:
                NativeTypes peekChar = (NativeTypes) (reader.PeekChar() & 0xFF);
                switch (peekChar)
                {
                    case NativeTypes.TypedByReference:
                        goto typedByReferenceEntry;
                    case NativeTypes.ByRef:
                    case NativeTypes.Boolean:
                    case NativeTypes.Char:
                    case NativeTypes.SByte:
                    case NativeTypes.Byte:
                    case NativeTypes.Int16:
                    case NativeTypes.UInt16:
                    case NativeTypes.Int32:
                    case NativeTypes.UInt32:
                    case NativeTypes.Int64:
                    case NativeTypes.UInt64:
                    case NativeTypes.Single:
                    case NativeTypes.Double:
                    case NativeTypes.String:
                    case NativeTypes.Type:
                    case NativeTypes.ValueType:
                    case NativeTypes.Class:
                    case NativeTypes.GenericTypeParameter:
                    case NativeTypes.Array:
                    case NativeTypes.GenericClosure:
                    case NativeTypes.NativeInteger:
                    case NativeTypes.NativeUnsignedInteger:
                    case NativeTypes.FunctionPointer:
                    case NativeTypes.Object:
                    case NativeTypes.VectorArray:
                    case NativeTypes.MethodGenericParameter:
                        currentType = ParseType(reader, metadataRoot);
                        break;
                    case NativeTypes.RequiredModifier:
                    case NativeTypes.OptionalModifier:
                        customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
                        goto parseNext;
                    case NativeTypes.Pinned:
                        currentPinned = true;
                        reader.ReadByte();
                        goto parseNext;
                    case NativeTypes.Pointer:
                    default:
                        break;
                }
                localVariables[i] = new CliMetadataLocalVarFullEntrySignature(currentType, customModifiers, currentPinned);
                continue;
            typedByReferenceEntry:
                localVariables[i] = new CliMetadataLocalVarEntrySignature(CliMetadataLocalVarEntryKind.TypedReference);
            }
            return new CliMetadataLocalVarSignature(localVariables);
        }

        internal static ICliMetadataCustomModifierSignature ParseCustomModifier(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            NativeTypes requiredOrOptional = (NativeTypes) reader.ReadByte();
            switch (requiredOrOptional)
            {
                case NativeTypes.RequiredModifier:
                    return new CliMetadataCustomModifierSignature(true, ParseTypeRefOrDefOrSpecEncoded(reader, metadataRoot));
                case NativeTypes.OptionalModifier:
                    return new CliMetadataCustomModifierSignature(false, ParseTypeRefOrDefOrSpecEncoded(reader, metadataRoot));
                default:
                    throw new BadImageFormatException("Custom modifiers must be CMOD_OPT OR CMOD_REQD");
            }
        }

        internal static ICliMetadataParamSignature ParseParam(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot, bool varArgConvention = false, bool isVarArg = false)
        {
            List<ICliMetadataCustomModifierSignature> customModifiers = new List<ICliMetadataCustomModifierSignature>();
        parseNextCustomModifier:
            NativeTypes firstByte = (NativeTypes) (reader.PeekChar() & 0xFF);
            switch (firstByte)
            {
                case NativeTypes.RequiredModifier:
                case NativeTypes.OptionalModifier:
                    customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
                    goto parseNextCustomModifier;
                case NativeTypes.ByRef:
                    if (varArgConvention)
                        return new CliMetadataVarArgParamSignature(ParseType(reader, metadataRoot), isVarArg, customModifiers);
                    else
                        return new CliMetadataParamSignature(ParseType(reader, metadataRoot), customModifiers);
                case NativeTypes.TypedByReference:
                case NativeTypes.Type:
                    reader.ReadByte();
                    if (varArgConvention)
                        return new CliMetadataVarArgParamSignature(new CliMetadataNativeTypeSignature(firstByte), isVarArg, customModifiers);
                    else
                        return new CliMetadataParamSignature(new CliMetadataNativeTypeSignature(firstByte), customModifiers);
                case NativeTypes.Boolean:
                case NativeTypes.Char:
                case NativeTypes.SByte:
                case NativeTypes.Byte:
                case NativeTypes.Int16:
                case NativeTypes.UInt16:
                case NativeTypes.Int32:
                case NativeTypes.UInt32:
                case NativeTypes.Int64:
                case NativeTypes.UInt64:
                case NativeTypes.Single:
                case NativeTypes.Double:
                case NativeTypes.String:
                case NativeTypes.ValueType:
                case NativeTypes.Class:
                case NativeTypes.GenericTypeParameter:
                case NativeTypes.Array:
                case NativeTypes.GenericClosure:
                case NativeTypes.NativeInteger:
                case NativeTypes.NativeUnsignedInteger:
                case NativeTypes.FunctionPointer:
                case NativeTypes.Object:
                case NativeTypes.VectorArray:
                case NativeTypes.Pointer:
                case NativeTypes.MethodGenericParameter:
                    if (varArgConvention)
                        return new CliMetadataVarArgParamSignature(ParseType(reader, metadataRoot), isVarArg, customModifiers);
                    else
                        return new CliMetadataParamSignature(ParseType(reader, metadataRoot), customModifiers);
                default:
                    throw new BadImageFormatException("Invalid param type signature.");
            }
        }

        internal static ICliMetadataTypeSignature ParseType(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot, bool voidContext = false)
        {
            NativeTypes firstValue = (NativeTypes) reader.ReadByte();
            switch (firstValue)
            {
                case NativeTypes.Void:
                    if (voidContext)
                        return new CliMetadataNativeTypeSignature(NativeTypes.Void);
                    break;
                case NativeTypes.Pointer:
                    return new CliMetadataElementTypeAndModifiersSignature(TypeElementClassification.Pointer, customModifiers: ParseCustomModifierSignatures(reader, metadataRoot), elementType: ParseType(reader, metadataRoot, true));
                case NativeTypes.ByRef:
                    return new CliMetadataElementTypeAndModifiersSignature(TypeElementClassification.Reference, customModifiers: ParseCustomModifierSignatures(reader, metadataRoot), elementType: ParseType(reader, metadataRoot, true));
                case NativeTypes.Boolean:
                case NativeTypes.Char:
                case NativeTypes.SByte:
                case NativeTypes.Byte:
                case NativeTypes.Int16:
                case NativeTypes.UInt16:
                case NativeTypes.Int32:
                case NativeTypes.UInt32:
                case NativeTypes.Int64:
                case NativeTypes.UInt64:
                case NativeTypes.Single:
                case NativeTypes.Double:
                case NativeTypes.String:
                case NativeTypes.NativeInteger:
                case NativeTypes.NativeUnsignedInteger:
                case NativeTypes.Object:
                case NativeTypes.Type:
                    return new CliMetadataNativeTypeSignature(firstValue);
                case NativeTypes.GenericClosure:
                    NativeTypes valOrClass = (NativeTypes) reader.ReadByte();
                    bool isClass;
                    switch (valOrClass)
                    {
                        case NativeTypes.ValueType:
                            isClass = false;
                            break;
                        case NativeTypes.Class:
                            isClass = true;
                            break;
                        default:
                            throw new BadImageFormatException("Generic Signature Expects VALUETYPE or CLASS to follow GENERICINST");
                    }
                    var typeRefOrDefOrSpec = ParseTypeRefOrDefOrSpecEncoded(reader, metadataRoot);
                    var genParams = CliMetadataRoot.ReadCompressedUnsignedInt(reader);
                    ICliMetadataTypeSignature[] genericParams = new ICliMetadataTypeSignature[genParams];
                    for (int i = 0; i < genericParams.Length; i++)
                        genericParams[i] = ParseType(reader, metadataRoot);
                    return new CliMetadataGenericTypeInstanceSignature(isClass, typeRefOrDefOrSpec, genericParams);
                case NativeTypes.ValueType:
                    return new CliMetadataValueOrClassTypeSignature(false, ParseTypeRefOrDefOrSpecEncoded(reader, metadataRoot));
                case NativeTypes.Class:
                    return new CliMetadataValueOrClassTypeSignature(true, ParseTypeRefOrDefOrSpecEncoded(reader, metadataRoot));
                case NativeTypes.FunctionPointer:
                    return new CliMetadataFunctionPointerTypeSignature(ParseMethodSignature(reader, metadataRoot));
                case NativeTypes.MethodGenericParameter:
                    return new CliMetadataGenericParameterSignature(CliMetadataGenericParameterParent.Method, (uint) CliMetadataRoot.ReadCompressedUnsignedInt(reader));
                case NativeTypes.GenericTypeParameter:
                    return new CliMetadataGenericParameterSignature(CliMetadataGenericParameterParent.Type, (uint) CliMetadataRoot.ReadCompressedUnsignedInt(reader));
                case NativeTypes.Array:
                    var arrayType = ParseType(reader, metadataRoot);
                    var arrayStructure = ParseArrayShape(reader, metadataRoot);
                    return new CliMetadataArrayTypeSignature(arrayType, arrayStructure);
                case NativeTypes.VectorArray:
                    return new CliMetadataVectorArrayTypeSignature(ParseCustomModifierSignatures(reader, metadataRoot), ParseType(reader, metadataRoot));
                default:
                    throw new BadImageFormatException("Unexpected type kind.");
            }
            throw new NotImplementedException();
        }

        internal static List<ICliMetadataCustomModifierSignature> ParseCustomModifierSignatures(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            List<ICliMetadataCustomModifierSignature> customModifiers = null;
            NativeTypes peekChar;
            if ((peekChar = (NativeTypes) (reader.PeekChar() & 0xFF)) == NativeTypes.OptionalModifier ||
                peekChar == NativeTypes.RequiredModifier)
            {
                customModifiers = new List<ICliMetadataCustomModifierSignature>();
                customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
            }

            while ((peekChar = (NativeTypes) (reader.PeekChar() & 0xFF)) == NativeTypes.OptionalModifier ||
                peekChar == NativeTypes.RequiredModifier)
                customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
            return customModifiers;
        }

        internal static ITypeDefOrRefRow ParseTypeRefOrDefOrSpecEncoded(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            var encodedIndex = CliMetadataRoot.ReadCompressedUnsignedInt(reader);
            CliMetadataTypeDefOrRefTag tableSelector = (CliMetadataTypeDefOrRefTag) (encodedIndex & ((1 << (int) CliMetadataTypeDefOrRefTag.ShiftSize) - 1));
            encodedIndex >>= (int) CliMetadataTypeDefOrRefTag.ShiftSize;
            switch (tableSelector)
            {
                case CliMetadataTypeDefOrRefTag.TypeDefinition:
                    return metadataRoot.TableStream.TypeDefinitionTable[encodedIndex];
                case CliMetadataTypeDefOrRefTag.TypeReference:
                    return metadataRoot.TableStream.TypeRefTable[encodedIndex];
                case CliMetadataTypeDefOrRefTag.TypeSpecification:
                    return metadataRoot.TableStream.TypeSpecificationTable[encodedIndex];
            }
            throw new BadImageFormatException("Wrong table type encoded.");
        }

        internal static ICliMetadataArrayShapeSignature ParseArrayShape(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            uint rank = (uint) CliMetadataRoot.ReadCompressedUnsignedInt(reader);
            uint numSizes = (uint) CliMetadataRoot.ReadCompressedUnsignedInt(reader);
            uint[] sizes = new uint[numSizes];
            for (int i = 0; i < sizes.Length; i++)
                sizes[i] = (uint) CliMetadataRoot.ReadCompressedUnsignedInt(reader);
            uint numLoBounds = (uint) CliMetadataRoot.ReadCompressedUnsignedInt(reader);
            uint[] lowerBounds = new uint[numLoBounds];
            for (int i = 0; i < lowerBounds.Length; i++)
                lowerBounds[i] = (uint) CliMetadataRoot.ReadCompressedSignedInt(reader);
            return new CliMetadataArrayShapeSignature(rank, sizes, lowerBounds);
        }

        internal static ICliMetadataTypeSpecSignature ParseTypeSpec(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            var firstByte = (NativeTypes)(reader.PeekChar() & 0xFF);
            switch (firstByte)
            {
                case NativeTypes.Array:
                case NativeTypes.GenericClosure:
                case NativeTypes.FunctionPointer:
                case NativeTypes.Pointer:
                case NativeTypes.MethodGenericParameter:
                case NativeTypes.GenericTypeParameter:
                case NativeTypes.VectorArray:
                case NativeTypes.EnumSignal:
                    return ParseType(reader, metadataRoot, false);
                    break;
            }
            throw new BadImageFormatException("Unknown type specification format.");
        }

        internal static ICliMetadataMethodSpecSignature ParseMethodSpec(EndianAwareBinaryReader reader, CliMetadataRoot metadataRoot)
        {
            throw new NotImplementedException();
        }
    }
}