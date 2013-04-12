using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal static class SignatureParser
    {
        internal static ICliMetadataStandAloneSignature ParseStandaloneSig(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            var prolog = (SignatureKinds)(reader.PeekByte() & 0xFF);
            if (prolog == SignatureKinds.StandaloneLocalVarSig)
                return ParseLocalVarSig(reader, metadataRoot);
            else if (prolog == SignatureKinds.FieldSig)
                //return ParseLocalVarSig(reader, metadataRoot);
                return ParseFieldSig(reader, metadataRoot);
            else
                return ParseStandaloneMethodSig(reader, metadataRoot);
        }

        internal static ICliMetadataMethodSignature ParseMethodSignature(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, bool canHaveRefContext = true)
        {

            const CliMetadataMethodSigFlags legalFlags = CliMetadataMethodSigFlags.HasThis | CliMetadataMethodSigFlags.ExplicitThis;
            const CliMetadataMethodSigConventions legalConventions =
                  CliMetadataMethodSigConventions.Default |
                  CliMetadataMethodSigConventions.VariableArguments |
                  CliMetadataMethodSigConventions.Generic |
                  CliMetadataMethodSigConventions.StdCall |
                  CliMetadataMethodSigConventions.Cdecl;
            const int legalFirst = (int)legalFlags | (int)legalConventions;
            byte firstByte = reader.ReadByte();
            if ((firstByte & legalFirst) == 0 && firstByte != 0)
                throw new BadImageFormatException("Unknown calling convention encountered.");
            var callingConvention = ((CliMetadataMethodSigConventions)firstByte) & legalConventions;
            var flags = ((CliMetadataMethodSigFlags)firstByte) & legalFlags;


            int paramCount;
            int genericParamCount = 0;
            if ((callingConvention & CliMetadataMethodSigConventions.Generic) == CliMetadataMethodSigConventions.Generic)
                genericParamCount = CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            paramCount = CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            ICliMetadataReturnTypeSignature returnType = ParseReturnTypeSignature(reader, metadataRoot);
            bool sentinelEncountered = false;
            if (canHaveRefContext)
            {
                ICliMetadataVarArgParamSignature[] parameters = new ICliMetadataVarArgParamSignature[paramCount];
                for (int i = 0; i < parameters.Length; i++)
                {
                    byte nextByte = (byte)(reader.PeekByte() & 0xFF);
                    if (nextByte == (byte)CliMetadataMethodSigFlags.Sentinel)
                    {
                        if (!sentinelEncountered)
                        {
                            flags |= CliMetadataMethodSigFlags.Sentinel;
                            sentinelEncountered = true;
                            reader.ReadByte();
                        }
                    }
                    parameters[i] = (ICliMetadataVarArgParamSignature)ParseParam(reader, metadataRoot, true, sentinelEncountered);
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

        internal static ICliMetadataMethodDefSignature ParseMethodDefSig(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            return (ICliMetadataMethodDefSignature)ParseMethodSignature(reader, metadataRoot, false);
        }

        internal static ICliMetadataMethodRefSignature ParseMethodRefSig(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            return (ICliMetadataMethodRefSignature)ParseMethodSignature(reader, metadataRoot);
        }

        internal static ICliMetadataStandAloneCommonMethodSignature ParseStandaloneMethodSig(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            var firstByte = reader.ReadByte();
            var convention = ((CliMetadataMethodSigConventions)firstByte) & CliMetadataMethodSigConventions.Mask;
            var flags = ((CliMetadataMethodSigFlags)firstByte) & (CliMetadataMethodSigFlags.Mask ^ CliMetadataMethodSigFlags.SentinelLowBit);
            int paramCount = CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            var returnType = ParseReturnTypeSignature(reader, metadataRoot);
            bool sentinelAllowed = convention == CliMetadataMethodSigConventions.VariableArguments || convention == CliMetadataMethodSigConventions.Cdecl;
            bool sentinelEncountered = false;
            ICliMetadataVarArgParamSignature[] parameters = new ICliMetadataVarArgParamSignature[paramCount];

            for (int i = 0; i < paramCount; i++)
            {
                byte peek = (byte)(reader.PeekByte() & 0xFF);
                if (peek == (byte)CliMetadataMethodSigFlags.Sentinel)
                {
                    if (sentinelAllowed && !sentinelEncountered)
                    {
                        flags |= CliMetadataMethodSigFlags.Sentinel;
                        sentinelEncountered = true;
                        reader.ReadByte();
                    }
                }
                parameters[i] = (ICliMetadataVarArgParamSignature)ParseParam(reader, metadataRoot, true, sentinelEncountered);
            }
            return new CliMetadataStandAloneMethodSignature(convention, flags, returnType, parameters);
        }

        internal static ICliMetadataReturnTypeSignature ParseReturnTypeSignature(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            List<ICliMetadataCustomModifierSignature> customModifiers = null;
        parseNextCustomModifier:
            CliMetadataNativeTypes firstByte = (CliMetadataNativeTypes)(reader.PeekByte() & 0xFF);
            switch (firstByte)
            {
                case CliMetadataNativeTypes.RequiredModifier:
                case CliMetadataNativeTypes.OptionalModifier:
                    if (customModifiers == null)
                        customModifiers = new List<ICliMetadataCustomModifierSignature>();
                    customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
                    goto parseNextCustomModifier;
                case CliMetadataNativeTypes.ByRef:
                    if (customModifiers == null)
                        return new CliMetadataReturnTypeSignature(ParseType(reader, metadataRoot), null);
                    else
                        return new CliMetadataReturnTypeSignature(ParseType(reader, metadataRoot), customModifiers.ToArray());
                case CliMetadataNativeTypes.TypedByReference:
                    if (customModifiers == null)
                        return new CliMetadataReturnTypeSignature(new CliMetadataNativeTypeSignature(firstByte), null);
                    else
                        return new CliMetadataReturnTypeSignature(new CliMetadataNativeTypeSignature(firstByte), customModifiers.ToArray());
                case CliMetadataNativeTypes.Pointer:
                case CliMetadataNativeTypes.Type:
                case CliMetadataNativeTypes.Boolean:
                case CliMetadataNativeTypes.Char:
                case CliMetadataNativeTypes.SByte:
                case CliMetadataNativeTypes.Byte:
                case CliMetadataNativeTypes.Int16:
                case CliMetadataNativeTypes.UInt16:
                case CliMetadataNativeTypes.Int32:
                case CliMetadataNativeTypes.UInt32:
                case CliMetadataNativeTypes.Int64:
                case CliMetadataNativeTypes.UInt64:
                case CliMetadataNativeTypes.Single:
                case CliMetadataNativeTypes.Double:
                case CliMetadataNativeTypes.String:
                case CliMetadataNativeTypes.ValueType:
                case CliMetadataNativeTypes.Class:
                case CliMetadataNativeTypes.GenericTypeParameter:
                case CliMetadataNativeTypes.Array:
                case CliMetadataNativeTypes.GenericClosure:
                case CliMetadataNativeTypes.NativeInteger:
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                case CliMetadataNativeTypes.FunctionPointer:
                case CliMetadataNativeTypes.Object:
                case CliMetadataNativeTypes.VectorArray:
                case CliMetadataNativeTypes.Void:
                case CliMetadataNativeTypes.MethodGenericParameter:
                    if (customModifiers == null)
                        return new CliMetadataReturnTypeSignature(ParseType(reader, metadataRoot, true), null);
                    else
                        return new CliMetadataReturnTypeSignature(ParseType(reader, metadataRoot, true), customModifiers.ToArray());
                default:
                    throw new BadImageFormatException("Invalid return type signature.");
            }
        }

        internal static ICliMetadataFieldSignature ParseFieldSig(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, bool pinnedContext = false)
        {
            var prolog = (SignatureKinds)reader.ReadByte();
            if ((prolog & SignatureKinds.FieldSig) != SignatureKinds.FieldSig)
                throw new InvalidOperationException();
            List<ICliMetadataCustomModifierSignature> customModifiers = null;
        parseNextModifier:
            CliMetadataNativeTypes nextChar = (CliMetadataNativeTypes)(reader.PeekByte() & 0xFF);
            switch (nextChar)
            {
                case CliMetadataNativeTypes.Pinned:
                    reader.ReadByte();
                    if (customModifiers == null)
                        return new CliMetadataFieldSignature(ParseType(reader, metadataRoot), null, true);
                    else
                        return new CliMetadataFieldSignature(ParseType(reader, metadataRoot), customModifiers.ToArray(), true);
                case CliMetadataNativeTypes.Boolean:
                case CliMetadataNativeTypes.Char:
                case CliMetadataNativeTypes.SByte:
                case CliMetadataNativeTypes.Byte:
                case CliMetadataNativeTypes.Int16:
                case CliMetadataNativeTypes.UInt16:
                case CliMetadataNativeTypes.Int32:
                case CliMetadataNativeTypes.UInt32:
                case CliMetadataNativeTypes.Int64:
                case CliMetadataNativeTypes.UInt64:
                case CliMetadataNativeTypes.Single:
                case CliMetadataNativeTypes.ByRef:
                case CliMetadataNativeTypes.Double:
                case CliMetadataNativeTypes.String:
                case CliMetadataNativeTypes.Pointer:
                case CliMetadataNativeTypes.ValueType:
                case CliMetadataNativeTypes.Class:
                case CliMetadataNativeTypes.GenericTypeParameter:
                case CliMetadataNativeTypes.Array:
                case CliMetadataNativeTypes.GenericClosure:
                case CliMetadataNativeTypes.TypedByReference:
                case CliMetadataNativeTypes.NativeInteger:
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                case CliMetadataNativeTypes.FunctionPointer:
                case CliMetadataNativeTypes.Object:
                case CliMetadataNativeTypes.VectorArray:
                case CliMetadataNativeTypes.MethodGenericParameter:
                    if (customModifiers == null)
                        return new CliMetadataFieldSignature(ParseType(reader, metadataRoot), null);
                    else
                        return new CliMetadataFieldSignature(ParseType(reader, metadataRoot), customModifiers.ToArray());
                case CliMetadataNativeTypes.RequiredModifier:
                case CliMetadataNativeTypes.OptionalModifier:
                    if (customModifiers == null)
                        customModifiers = new List<ICliMetadataCustomModifierSignature>();
                    customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
                    goto parseNextModifier;
            }
            throw new BadImageFormatException("Signature not properly constructed.");
        }

        internal static ICliMetadataPropertySignature ParsePropertySig(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            CliMetadataNativeTypes firstChar = (CliMetadataNativeTypes)(reader.ReadByte() & 0xFF);
            if (firstChar != (CliMetadataNativeTypes)SignatureKinds.PropertySig &&
                firstChar != ((CliMetadataNativeTypes)SignatureKinds.PropertySig | (CliMetadataNativeTypes)CliMetadataMethodSigFlags.HasThis))
                throw new BadImageFormatException("Expected property or hasthis with property.");
            bool hasThis = (firstChar & (CliMetadataNativeTypes)CliMetadataMethodSigFlags.HasThis) == (CliMetadataNativeTypes)CliMetadataMethodSigFlags.HasThis;
            int paramCount = CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            IList<ICliMetadataCustomModifierSignature> customModifiers = null;
            ICliMetadataTypeSignature propertyType = null;
        nextChar:
            CliMetadataNativeTypes nextChar = (CliMetadataNativeTypes)(reader.PeekByte() & 0xFF);
            switch (nextChar)
            {
                case CliMetadataNativeTypes.OptionalModifier:
                case CliMetadataNativeTypes.RequiredModifier:
                    if (customModifiers == null)
                        customModifiers = new List<ICliMetadataCustomModifierSignature>();
                    customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
                    goto nextChar;
                case CliMetadataNativeTypes.Boolean:
                case CliMetadataNativeTypes.Char:
                case CliMetadataNativeTypes.SByte:
                case CliMetadataNativeTypes.Byte:
                case CliMetadataNativeTypes.Int16:
                case CliMetadataNativeTypes.UInt16:
                case CliMetadataNativeTypes.Int32:
                case CliMetadataNativeTypes.UInt32:
                case CliMetadataNativeTypes.Int64:
                case CliMetadataNativeTypes.UInt64:
                case CliMetadataNativeTypes.Single:
                case CliMetadataNativeTypes.Double:
                case CliMetadataNativeTypes.String:
                case CliMetadataNativeTypes.NativeInteger:
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                case CliMetadataNativeTypes.Object:
                case CliMetadataNativeTypes.Type:
                case CliMetadataNativeTypes.GenericClosure:
                case CliMetadataNativeTypes.Pointer:
                case CliMetadataNativeTypes.ValueType:
                case CliMetadataNativeTypes.Class:
                case CliMetadataNativeTypes.FunctionPointer:
                case CliMetadataNativeTypes.MethodGenericParameter:
                case CliMetadataNativeTypes.GenericTypeParameter:
                case CliMetadataNativeTypes.Array:
                case CliMetadataNativeTypes.VectorArray:
                case CliMetadataNativeTypes.ByRef:
                    propertyType = ParseType(reader, metadataRoot);
                    break;
                default:
                    throw new BadImageFormatException("Expected: TypeSignature");
            }
            ICliMetadataParamSignature[] parameters = new ICliMetadataParamSignature[paramCount];
            for (int i = 0; i < paramCount; i++)
                parameters[i] = ParseParam(reader, metadataRoot);
            if (customModifiers == null)
                return new CliMetadataPropertySignature(hasThis, parameters, null, propertyType);
            else
                return new CliMetadataPropertySignature(hasThis, parameters, customModifiers.ToArray(), propertyType);
        }

        internal static ICliMetadataLocalVarSignature ParseLocalVarSig(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            var prolog = (SignatureKinds)reader.ReadByte();
            if (prolog != SignatureKinds.StandaloneLocalVarSig)
                throw new BadImageFormatException("LocalVariableSig contains invalid leading byte");
            int count = CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            ICliMetadataLocalVarEntrySignature[] localVariables = new ICliMetadataLocalVarEntrySignature[count];

            for (int i = 0; i < count; i++)
            {
                bool currentPinned = false;
                ICliMetadataTypeSignature currentType = null;
                List<ICliMetadataCustomModifierSignature> customModifiers = null;
            parseNext:
                CliMetadataNativeTypes peekChar = (CliMetadataNativeTypes)(reader.PeekByte() & 0xFF);
                switch (peekChar)
                {
                    case CliMetadataNativeTypes.TypedByReference:
                        goto typedByReferenceEntry;
                    case CliMetadataNativeTypes.ByRef:
                    case CliMetadataNativeTypes.Boolean:
                    case CliMetadataNativeTypes.Char:
                    case CliMetadataNativeTypes.SByte:
                    case CliMetadataNativeTypes.Byte:
                    case CliMetadataNativeTypes.Int16:
                    case CliMetadataNativeTypes.UInt16:
                    case CliMetadataNativeTypes.Int32:
                    case CliMetadataNativeTypes.UInt32:
                    case CliMetadataNativeTypes.Int64:
                    case CliMetadataNativeTypes.UInt64:
                    case CliMetadataNativeTypes.Single:
                    case CliMetadataNativeTypes.Double:
                    case CliMetadataNativeTypes.String:
                    case CliMetadataNativeTypes.Type:
                    case CliMetadataNativeTypes.ValueType:
                    case CliMetadataNativeTypes.Class:
                    case CliMetadataNativeTypes.GenericTypeParameter:
                    case CliMetadataNativeTypes.Array:
                    case CliMetadataNativeTypes.GenericClosure:
                    case CliMetadataNativeTypes.NativeInteger:
                    case CliMetadataNativeTypes.NativeUnsignedInteger:
                    case CliMetadataNativeTypes.FunctionPointer:
                    case CliMetadataNativeTypes.Object:
                    case CliMetadataNativeTypes.VectorArray:
                    case CliMetadataNativeTypes.MethodGenericParameter:
                        currentType = ParseType(reader, metadataRoot);
                        break;
                    case CliMetadataNativeTypes.RequiredModifier:
                    case CliMetadataNativeTypes.OptionalModifier:
                        if (customModifiers == null)
                            customModifiers = new List<ICliMetadataCustomModifierSignature>();
                        customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
                        goto parseNext;
                    case CliMetadataNativeTypes.Pinned:
                        currentPinned = true;
                        reader.ReadByte();
                        goto parseNext;
                    case CliMetadataNativeTypes.Pointer:
                    default:
                        break;
                }
                if (customModifiers == null)
                    localVariables[i] = new CliMetadataLocalVarFullEntrySignature(currentType, null, currentPinned);
                else
                    localVariables[i] = new CliMetadataLocalVarFullEntrySignature(currentType, customModifiers.ToArray(), currentPinned);
                continue;
            typedByReferenceEntry:
                localVariables[i] = new CliMetadataLocalVarEntrySignature(CliMetadataLocalVarEntryKind.TypedReference);
            }
            return new CliMetadataLocalVarSignature(localVariables);
        }

        internal static ICliMetadataCustomModifierSignature ParseCustomModifier(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            CliMetadataNativeTypes requiredOrOptional = (CliMetadataNativeTypes)reader.ReadByte();
            switch (requiredOrOptional)
            {
                case CliMetadataNativeTypes.RequiredModifier:
                    return new CliMetadataCustomModifierSignature(true, ParseTypeRefOrDefOrSpecEncoded(reader, metadataRoot));
                case CliMetadataNativeTypes.OptionalModifier:
                    return new CliMetadataCustomModifierSignature(false, ParseTypeRefOrDefOrSpecEncoded(reader, metadataRoot));
                default:
                    throw new BadImageFormatException("Custom modifiers must be CMOD_OPT OR CMOD_REQD");
            }
        }

        internal static ICliMetadataParamSignature ParseParam(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, bool varArgConvention = false, bool isVarArg = false)
        {
            List<ICliMetadataCustomModifierSignature> customModifiers = null;
        parseNextCustomModifier:
            CliMetadataNativeTypes firstByte = (CliMetadataNativeTypes)(reader.PeekByte() & 0xFF);
            switch (firstByte)
            {
                case CliMetadataNativeTypes.RequiredModifier:
                case CliMetadataNativeTypes.OptionalModifier:
                    if (customModifiers == null)
                        customModifiers = new List<ICliMetadataCustomModifierSignature>();
                    customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
                    goto parseNextCustomModifier;
                case CliMetadataNativeTypes.ByRef:
                    if (customModifiers == null)
                        if (varArgConvention)
                            return new CliMetadataVarArgParamSignature(ParseType(reader, metadataRoot), isVarArg, null);
                        else
                            return new CliMetadataParamSignature(ParseType(reader, metadataRoot), null);
                    else
                        if (varArgConvention)
                            return new CliMetadataVarArgParamSignature(ParseType(reader, metadataRoot), isVarArg, customModifiers.ToArray());
                        else
                            return new CliMetadataParamSignature(ParseType(reader, metadataRoot), customModifiers.ToArray());
                case CliMetadataNativeTypes.TypedByReference:
                case CliMetadataNativeTypes.Type:
                    reader.ReadByte();
                    if (customModifiers == null)
                        if (varArgConvention)
                            return new CliMetadataVarArgParamSignature(new CliMetadataNativeTypeSignature(firstByte), isVarArg, null);
                        else
                            return new CliMetadataParamSignature(new CliMetadataNativeTypeSignature(firstByte), null);
                    else
                        if (varArgConvention)
                            return new CliMetadataVarArgParamSignature(new CliMetadataNativeTypeSignature(firstByte), isVarArg, customModifiers.ToArray());
                        else
                            return new CliMetadataParamSignature(new CliMetadataNativeTypeSignature(firstByte), customModifiers.ToArray());
                case CliMetadataNativeTypes.Boolean:
                case CliMetadataNativeTypes.Char:
                case CliMetadataNativeTypes.SByte:
                case CliMetadataNativeTypes.Byte:
                case CliMetadataNativeTypes.Int16:
                case CliMetadataNativeTypes.UInt16:
                case CliMetadataNativeTypes.Int32:
                case CliMetadataNativeTypes.UInt32:
                case CliMetadataNativeTypes.Int64:
                case CliMetadataNativeTypes.UInt64:
                case CliMetadataNativeTypes.Single:
                case CliMetadataNativeTypes.Double:
                case CliMetadataNativeTypes.String:
                case CliMetadataNativeTypes.ValueType:
                case CliMetadataNativeTypes.Class:
                case CliMetadataNativeTypes.GenericTypeParameter:
                case CliMetadataNativeTypes.Array:
                case CliMetadataNativeTypes.GenericClosure:
                case CliMetadataNativeTypes.NativeInteger:
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                case CliMetadataNativeTypes.FunctionPointer:
                case CliMetadataNativeTypes.Object:
                case CliMetadataNativeTypes.VectorArray:
                case CliMetadataNativeTypes.Pointer:
                case CliMetadataNativeTypes.MethodGenericParameter:
                    if (customModifiers == null)
                        if (varArgConvention)
                            return new CliMetadataVarArgParamSignature(ParseType(reader, metadataRoot), isVarArg, null);
                        else
                            return new CliMetadataParamSignature(ParseType(reader, metadataRoot), null);
                    else
                        if (varArgConvention)
                            return new CliMetadataVarArgParamSignature(ParseType(reader, metadataRoot), isVarArg, customModifiers.ToArray());
                        else
                            return new CliMetadataParamSignature(ParseType(reader, metadataRoot), customModifiers.ToArray());
                default:
                    throw new BadImageFormatException("Invalid param type signature.");
            }
        }

        internal static ICliMetadataTypeSignature ParseType(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, bool voidContext = false, bool allowModifiers = false)
        {
            ICliMetadataCustomModifierSignature[] cmods = null;
        postCustomMods:
            CliMetadataNativeTypes firstValue = (CliMetadataNativeTypes)reader.ReadByte();
            switch (firstValue)
            {
                case CliMetadataNativeTypes.Void:
                    if (voidContext)
                        return new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Void);
                    goto default;
                case CliMetadataNativeTypes.Pointer:
                    if (allowModifiers && cmods == null)
                        return new CliMetadataElementTypeAndModifiersSignature(TypeElementClassification.Pointer, customModifiers: cmods.Concat(ParseCustomModifierSignatures(reader, metadataRoot)).ToArray(), elementType: ParseType(reader, metadataRoot, true));
                    return new CliMetadataElementTypeAndModifiersSignature(TypeElementClassification.Pointer, customModifiers: ParseCustomModifierSignatures(reader, metadataRoot), elementType: ParseType(reader, metadataRoot, true));
                case CliMetadataNativeTypes.ByRef:
                    if (allowModifiers && cmods == null)
                        return new CliMetadataElementTypeAndModifiersSignature(TypeElementClassification.Reference, customModifiers: cmods.Concat(ParseCustomModifierSignatures(reader, metadataRoot)).ToArray(), elementType: ParseType(reader, metadataRoot, true));
                    return new CliMetadataElementTypeAndModifiersSignature(TypeElementClassification.Reference, customModifiers: ParseCustomModifierSignatures(reader, metadataRoot), elementType: ParseType(reader, metadataRoot, true));
                case CliMetadataNativeTypes.Boolean:
                case CliMetadataNativeTypes.Char:
                case CliMetadataNativeTypes.SByte:
                case CliMetadataNativeTypes.Byte:
                case CliMetadataNativeTypes.Int16:
                case CliMetadataNativeTypes.UInt16:
                case CliMetadataNativeTypes.Int32:
                case CliMetadataNativeTypes.UInt32:
                case CliMetadataNativeTypes.Int64:
                case CliMetadataNativeTypes.UInt64:
                case CliMetadataNativeTypes.Single:
                case CliMetadataNativeTypes.Double:
                case CliMetadataNativeTypes.String:
                case CliMetadataNativeTypes.NativeInteger:
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                case CliMetadataNativeTypes.Object:
                case CliMetadataNativeTypes.Type:
                    if (allowModifiers && cmods != null)
                        return new CliMetadataElementTypeAndModifiersSignature(TypeElementClassification.ModifiedType, new CliMetadataNativeTypeSignature(firstValue), cmods);
                    return new CliMetadataNativeTypeSignature(firstValue);
                case CliMetadataNativeTypes.GenericClosure:
                    CliMetadataNativeTypes valOrClass = (CliMetadataNativeTypes)reader.ReadByte();
                    bool isClass;
                    switch (valOrClass)
                    {
                        case CliMetadataNativeTypes.ValueType:
                            isClass = false;
                            break;
                        case CliMetadataNativeTypes.Class:
                            isClass = true;
                            break;
                        default:
                            throw new BadImageFormatException("Generic Signature Expects VALUETYPE or CLASS to follow GENERICINST");
                    }
                    var typeRefOrDefOrSpec = ParseTypeRefOrDefOrSpecEncoded(reader, metadataRoot);
                    byte r;
                    var genParams = CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader, out r);
                    ICliMetadataTypeSignature[] genericParams = new ICliMetadataTypeSignature[genParams];
                    for (int i = 0; i < genericParams.Length; i++)
                        genericParams[i] = ParseType(reader, metadataRoot, allowModifiers: true);
                    if (allowModifiers && cmods != null)
                        return new CliMetadataElementTypeAndModifiersSignature(TypeElementClassification.ModifiedType, new CliMetadataGenericInstanceTypeSignature(isClass, typeRefOrDefOrSpec, genericParams), cmods);
                    return new CliMetadataGenericInstanceTypeSignature(isClass, typeRefOrDefOrSpec, genericParams);
                case CliMetadataNativeTypes.ValueType:
                    return new CliMetadataValueOrClassTypeSignature(false, ParseTypeRefOrDefOrSpecEncoded(reader, metadataRoot));
                case CliMetadataNativeTypes.Class:
                    return new CliMetadataValueOrClassTypeSignature(true, ParseTypeRefOrDefOrSpecEncoded(reader, metadataRoot));
                case CliMetadataNativeTypes.FunctionPointer:
                    return new CliMetadataFunctionPointerTypeSignature(ParseMethodSignature(reader, metadataRoot));
                case CliMetadataNativeTypes.MethodGenericParameter:
                    if (allowModifiers && cmods != null)
                        return new CliMetadataElementTypeAndModifiersSignature(TypeElementClassification.ModifiedType, new CliMetadataGenericParameterSignature(CliMetadataGenericParameterParent.Method, (uint)CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader)), cmods);
                    else
                        return new CliMetadataGenericParameterSignature(CliMetadataGenericParameterParent.Method, (uint)CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader));
                case CliMetadataNativeTypes.GenericTypeParameter:
                    if (allowModifiers && cmods != null)
                        return new CliMetadataElementTypeAndModifiersSignature(TypeElementClassification.ModifiedType, new CliMetadataGenericParameterSignature(CliMetadataGenericParameterParent.Type, (uint)CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader)), cmods);
                    else
                        return new CliMetadataGenericParameterSignature(CliMetadataGenericParameterParent.Type, (uint)CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader));
                case CliMetadataNativeTypes.OptionalModifier:
                case CliMetadataNativeTypes.RequiredModifier:
                    if (!allowModifiers)
                        goto default;
                    reader.BaseStream.Position--;
                    cmods = ParseCustomModifierSignatures(reader, metadataRoot);
                    goto postCustomMods;
                case CliMetadataNativeTypes.Array:
                    var arrayType = ParseType(reader, metadataRoot);
                    var arrayStructure = ParseArrayShape(reader, metadataRoot);
                    return new CliMetadataArrayTypeSignature(arrayType, arrayStructure);
                case CliMetadataNativeTypes.VectorArray:
                    return new CliMetadataVectorArrayTypeSignature(ParseCustomModifierSignatures(reader, metadataRoot), ParseType(reader, metadataRoot));
                default:
                    throw new BadImageFormatException("Unexpected type kind.");
            }
            throw new NotImplementedException();
        }

        internal static ICliMetadataCustomModifierSignature[] ParseCustomModifierSignatures(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            List<ICliMetadataCustomModifierSignature> customModifiers = null;
            CliMetadataNativeTypes peekChar;
            if ((peekChar = (CliMetadataNativeTypes)(reader.PeekByte() & 0xFF)) == CliMetadataNativeTypes.OptionalModifier ||
                peekChar == CliMetadataNativeTypes.RequiredModifier)
            {
                customModifiers = new List<ICliMetadataCustomModifierSignature>();
                customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
            }

            while ((peekChar = (CliMetadataNativeTypes)(reader.PeekByte() & 0xFF)) == CliMetadataNativeTypes.OptionalModifier ||
                peekChar == CliMetadataNativeTypes.RequiredModifier)
                customModifiers.Add(ParseCustomModifier(reader, metadataRoot));
            if (customModifiers == null)
                return null;
            else
                return customModifiers.ToArray();
        }

        internal static ICliMetadataTypeDefOrRefRow ParseTypeRefOrDefOrSpecEncoded(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            var encodedIndex = CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            CliMetadataTypeDefOrRefTag tableSelector = (CliMetadataTypeDefOrRefTag)(encodedIndex & ((1 << (int)CliMetadataTypeDefOrRefTag.ShiftSize) - 1));
            encodedIndex >>= (int)CliMetadataTypeDefOrRefTag.ShiftSize;
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

        internal static ICliMetadataArrayShapeSignature ParseArrayShape(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            uint rank = (uint)CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            uint numSizes = (uint)CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            uint[] sizes = new uint[numSizes];
            for (int i = 0; i < sizes.Length; i++)
                sizes[i] = (uint)CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            uint numLoBounds = (uint)CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            int[] lowerBounds = new int[numLoBounds];
            for (int i = 0; i < lowerBounds.Length; i++)
                lowerBounds[i] = CliMetadataFixedRoot.ReadCompressedSignedInt(reader);
            return new CliMetadataArrayShapeSignature(rank, sizes, lowerBounds);
        }

        internal static ICliMetadataTypeSpecSignature ParseTypeSpec(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            var firstByte = (CliMetadataNativeTypes)(reader.PeekByte() & 0xFF);
            switch (firstByte)
            {

                case CliMetadataNativeTypes.Array:
                case CliMetadataNativeTypes.GenericClosure:
                case CliMetadataNativeTypes.FunctionPointer:
                case CliMetadataNativeTypes.Pointer:
                case CliMetadataNativeTypes.MethodGenericParameter:
                case CliMetadataNativeTypes.GenericTypeParameter:
                case CliMetadataNativeTypes.VectorArray:
                case CliMetadataNativeTypes.EnumSignal:
                    return ParseType(reader, metadataRoot, false);
                case CliMetadataNativeTypes.OptionalModifier:
                case CliMetadataNativeTypes.RequiredModifier:
                    return ParseType(reader, metadataRoot, false, true);
                case CliMetadataNativeTypes.Boolean:
                case CliMetadataNativeTypes.Char:
                case CliMetadataNativeTypes.SByte:
                case CliMetadataNativeTypes.Byte:
                case CliMetadataNativeTypes.Int16:
                case CliMetadataNativeTypes.UInt16:
                case CliMetadataNativeTypes.Int32:
                case CliMetadataNativeTypes.UInt32:
                case CliMetadataNativeTypes.Int64:
                case CliMetadataNativeTypes.UInt64:
                case CliMetadataNativeTypes.Single:
                case CliMetadataNativeTypes.Double:
                case CliMetadataNativeTypes.String:
                case CliMetadataNativeTypes.NativeInteger:
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                case CliMetadataNativeTypes.Object:
                case CliMetadataNativeTypes.Type:
                    return ParseType(reader, metadataRoot);
            }
            throw new BadImageFormatException("Unknown type specification format.");
        }

        internal static ICliMetadataMethodSpecSignature ParseMethodSpec(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            const byte GenericInstProlog = 0x0A;
            byte methodSpecLeadIn = reader.ReadByte();
            if (methodSpecLeadIn != GenericInstProlog)
                throw new BadImageFormatException();
            int genericParamCnt = CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            ICliMetadataTypeSignature[] genericParameters = new ICliMetadataTypeSignature[genericParamCnt];
            for (int i = 0; i < genericParamCnt; i++)
                genericParameters[i] = ParseType(reader, metadataRoot, allowModifiers: true);
            return new CliMetadataMethodSpecSignature(genericParameters);
        }

        internal static ICliMetadataSignature ParseMemberRefSig(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot)
        {
            const CliMetadataMethodSigFlags legalFlags = CliMetadataMethodSigFlags.HasThis | CliMetadataMethodSigFlags.ExplicitThis;
            const CliMetadataMethodSigConventions legalConventions =
                  CliMetadataMethodSigConventions.Default |
                  CliMetadataMethodSigConventions.VariableArguments |
                  CliMetadataMethodSigConventions.Generic |
                  CliMetadataMethodSigConventions.StdCall |
                  CliMetadataMethodSigConventions.Cdecl;
            byte firstChar = (byte)(reader.PeekByte() & 0xFF);
            if (firstChar != 0 &&
                ((firstChar & ((byte)legalFlags)) == 0) &&
                ((firstChar & ((byte)legalConventions)) == 0) &&
                firstChar != (byte)SignatureKinds.FieldSig)
                throw new BadImageFormatException("Expected Default, VarArg, or Generic calling convention, or a field sig prolog.");
            switch (firstChar)
            {
                case (byte)CliMetadataMethodSigFlags.HasThis:
                case (byte)CliMetadataMethodSigFlags.ExplicitThis:
                case (byte)(CliMetadataMethodSigFlags.HasThis | CliMetadataMethodSigFlags.ExplicitThis):
                case (byte)CliMetadataMethodSigConventions.Default:
                case (byte)CliMetadataMethodSigConventions.VariableArguments:
                case (byte)CliMetadataMethodSigConventions.Generic:
                    return ParseMethodSignature(reader, metadataRoot);
                case (byte)SignatureKinds.FieldSig:
                    return ParseFieldSig(reader, metadataRoot);
            }
            throw new BadImageFormatException("Expected Default, VarArg, or Generic calling convention, or a field sig prolog.");
        }

        internal static ICliMetadataCustomAttribute ParseCustomAttribute(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, _ICliManager identityManager, ICliMetadataCustomAttributeTableRow target, uint dataLength)
        {
            long startIndex = reader.BaseStream.Position;
            var prolog = reader.ReadUInt16();
            if (prolog != 0x0001)
                throw new BadImageFormatException("Bad custom attribute format.");
            ICliMetadataCustomAttributeParameter[] parameters = null;
            long rIndex = reader.BaseStream.Position;
            IControlledCollection<ICliMetadataParamSignature> caParams = null;
            switch (target.Ctor.CustomAttributeTypeEncoding)
            {
                case CliMetadataCustomAttributeTypeTag.MethodDefinition:
                    {
                        var mRef = (ICliMetadataMethodDefinitionTableRow)target.Ctor;
                        /* *
                         * Use the signature, instead of the method's properties, due
                         * to the whole metadata association to the parameter at sequence 0.
                         * */
                        caParams = mRef.Signature.Parameters;
                    }
                    break;
                case CliMetadataCustomAttributeTypeTag.MemberReference:
                    {
                        var mRef = (ICliMetadataMemberReferenceTableRow)target.Ctor;
                        switch (mRef.Signature.SignatureKind)
                        {
                            case SignatureKinds.MethodDefSig:
                            case SignatureKinds.MethodRefSig:
                            case SignatureKinds.StandaloneMethodSig:
                                ICliMetadataMethodSignature signature = (ICliMetadataMethodSignature)mRef.Signature;
                                parameters = new ICliMetadataCustomAttributeParameter[signature.Parameters.Count];
                                caParams = signature.Parameters;
                                break;
                            default:
                                throw new BadImageFormatException("Bad custom attribute format.");
                        }
                    }
                    break;
                default:
                    throw new BadImageFormatException("Bad custom attribute format.");
            }
            parameters = new ICliMetadataCustomAttributeParameter[caParams.Count];
            reader.BaseStream.Position = rIndex;
            for (int i = 0; i < parameters.Length; i++)
            {
                var currentSigParam = caParams[i];
                parameters[i] = ParseCustomAttributeParameter(reader, metadataRoot, identityManager, currentSigParam.ParameterType, startIndex, dataLength);
            }
            if (reader.BaseStream.Position >= startIndex + dataLength)
                return new CliMetadataCustomAttribute(parameters, null);
            int numNamed = reader.ReadUInt16();
            var namedParameters = new ICliMetadataCustomAttributeNamedParameter[numNamed];
            for (int i = 0; i < numNamed; i++)
                namedParameters[i] = ParseCustomAttributeNamedParameter(reader, metadataRoot, identityManager, startIndex, dataLength);
            return new CliMetadataCustomAttribute(parameters, namedParameters);

        }



        internal static ICliMetadataCustomAttributeParameter ParseCustomAttributeParameter(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, _ICliManager identityManager, ICliMetadataTypeSignature type, long start, uint dataLength)
        {
            switch (type.TypeSignatureKind)
            {
                case CliMetadataTypeSignatureKind.NativeType:
                    var nativeType = (ICliMetadataNativeTypeSignature)type;
                    switch (nativeType.TypeKind)
                    {
                        case CliMetadataNativeTypes.Boolean:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadByte() == 1);
                        case CliMetadataNativeTypes.Char:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadChar());
                        case CliMetadataNativeTypes.SByte:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadSByte());
                        case CliMetadataNativeTypes.Byte:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadByte());
                        case CliMetadataNativeTypes.Int16:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadInt16());
                        case CliMetadataNativeTypes.UInt16:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadUInt16());
                        case CliMetadataNativeTypes.Int32:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadInt32());
                        case CliMetadataNativeTypes.UInt32:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadUInt32());
                        case CliMetadataNativeTypes.Int64:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadInt64());
                        case CliMetadataNativeTypes.UInt64:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadUInt64());
                        case CliMetadataNativeTypes.Single:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadSingle());
                        case CliMetadataNativeTypes.Double:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, reader.ReadDouble());
                        case CliMetadataNativeTypes.String:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, ReadSerializedString(reader));
                        case CliMetadataNativeTypes.Type:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.Type, DeserializeType(reader, identityManager, identityManager.GetRelativeAssembly(metadataRoot)));
                        default:
                            throw new BadImageFormatException("Bad custom attribute format.");
                    }
                    break;
                case CliMetadataTypeSignatureKind.ValueOrClassType:
                    ICliMetadataValueOrClassTypeSignature targetSig = (ICliMetadataValueOrClassTypeSignature)type;
                    if (targetSig.IsValueType)
                    {
                        var pos = reader.BaseStream.Position;
                        ICliMetadataTypeDefOrRefRow rowData = targetSig.Target;
                        var targetType = identityManager.ResolveScope(rowData);
                        if (!CliCommon.IsEnum(identityManager, targetType))
                            throw new BadImageFormatException("Bad custom attribute format.");
                        var enumType = targetType.GetEnumBaseType();
                        IEnumType targetEnum = (IEnumType)identityManager.ObtainTypeReference(targetType);
                        reader.BaseStream.Position = pos;
                        switch (enumType)
                        {
                            case EnumerationBaseType.Default:
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadInt32()));
                            case EnumerationBaseType.SByte:
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadSByte()));
                            case EnumerationBaseType.Byte:
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadByte()));
                            case EnumerationBaseType.Int16:
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadInt16()));
                            case EnumerationBaseType.UInt16:
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadUInt16()));
                            case EnumerationBaseType.Int32:
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadInt32()));
                            case EnumerationBaseType.UInt32:
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadUInt32()));
                            case EnumerationBaseType.Int64:
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadInt64()));
                            case EnumerationBaseType.UInt64:
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadUInt64()));
                            case EnumerationBaseType.NativeInteger:
                            case EnumerationBaseType.NativeUnsignedInteger:
#if x86
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadUInt32()));
#elif x64
                                return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, Tuple.Create(targetEnum, reader.ReadUInt64()));
#endif
                            default:
                                throw new BadImageFormatException("Bad custom attribute format.");
                        }
                    }
                    else if (identityManager.ObtainTypeReference(targetSig.Target) == identityManager.ObtainTypeReference(RuntimeCoreType.Type))
                    {
                        /* *
                         * Parsing a type from a custom attribute.
                         * */
                        var relAssem = identityManager.GetRelativeAssembly(targetSig.Target.MetadataRoot);
                        var relAssemId = relAssem.UniqueIdentifier;
                        var parser = new TypeIdentityParser(ReadSerializedString(reader), new TIAssemblyIdentityRule(relAssemId.Name, new TIVersionRule(relAssemId.Version.Major, relAssemId.Version.Minor, relAssemId.Version.Build, relAssemId.Version.Revision), relAssemId.Culture, relAssemId.PublicKeyToken));
                        var typeIdentity = parser.ParseQualifiedTypeName();
                        return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.Type, typeIdentity.DecodeParsedType(identityManager));
                    }
                    throw new BadImageFormatException("Bad custom attribute format.");
                case CliMetadataTypeSignatureKind.VectorArrayType:
                    int arrayLevel = 0;
                    ICliMetadataTypeSignature currentType = type;
                    while (currentType.TypeSignatureKind == CliMetadataTypeSignatureKind.VectorArrayType)
                    {
                        arrayLevel++;
                        currentType = ((ICliMetadataVectorArrayTypeSignature)(currentType)).ElementType;
                    }
                    return ParseCustomAttributeArrayType(reader, metadataRoot, identityManager, currentType, arrayLevel, start, dataLength);
                default:
                    throw new BadImageFormatException("Bad custom attribute format.");
            }
            throw new NotImplementedException();
        }

        private static ICliMetadataCustomAttributeParameter ParseCustomAttributeArrayType(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, _ICliManager identityManager, ICliMetadataTypeSignature currentType, int arrayLevel, long start, uint dataLength)
        {
            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.VectorArray, ParseCustomAttributeArrayValue(reader, metadataRoot, identityManager, currentType, arrayLevel, start, dataLength, true));
        }

        private static object ParseCustomAttributeArrayValue(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, _ICliManager identityManager, ICliMetadataTypeSignature currentType, int arrayLevel, long start, uint dataLength, bool topLevel)
        {
            var numCases = reader.ReadInt32();
            Type t = null;
            ICliMetadataNativeTypeSignature nativeType = null;
            _ICliAssembly relAssem = null;
            IEnumType targetEnum = null;
            bool isEnum = false;
            if (currentType.TypeSignatureKind == CliMetadataTypeSignatureKind.ValueOrClassType)
            {
                ICliMetadataValueOrClassTypeSignature targetSig = (ICliMetadataValueOrClassTypeSignature)currentType;
                if (targetSig.IsValueType)
                {
                    var pos = reader.BaseStream.Position;
                    ICliMetadataTypeDefOrRefRow rowData = targetSig.Target;
                    var targetType = identityManager.ResolveScope(rowData);
                    if (!CliCommon.IsEnum(identityManager, targetType))
                        throw new BadImageFormatException("Bad custom attribute format.");
                    var enumType = targetType.GetEnumBaseType();
                    targetEnum = (IEnumType)identityManager.ObtainTypeReference(targetType);
                    reader.BaseStream.Position = pos;
                    switch (enumType)
                    {
                        case EnumerationBaseType.Default:
                        case EnumerationBaseType.Int32:
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Int32);
                            t = typeof(int);
                            break;
                        case EnumerationBaseType.SByte:
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.SByte);
                            t = typeof(sbyte);
                            break;
                        case EnumerationBaseType.Byte:
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Byte);
                            t = typeof(byte);
                            break;
                        case EnumerationBaseType.Int16:
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Int16);
                            t = typeof(short);
                            break;
                        case EnumerationBaseType.UInt16:
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.UInt16);
                            t = typeof(ushort);
                            break;
                        case EnumerationBaseType.UInt32:
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.UInt32);
                            t = typeof(uint);
                            break;
                        case EnumerationBaseType.Int64:
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Int64);
                            t = typeof(long);
                            break;
                        case EnumerationBaseType.UInt64:
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.UInt64);
                            t = typeof(ulong);
                            break;
                        case EnumerationBaseType.NativeInteger:
#if x86
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Int32);
                            t = typeof(int);
                            break;
#elif x64
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Int64);
                            t = typeof(long);
                            break;
#endif
                        case EnumerationBaseType.NativeUnsignedInteger:
#if x86
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.UInt32);
                            t = typeof(uint);
                            break;
#elif x64
                            nativeType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.UInt64);
                            t = typeof(ulong);
                            break;
#endif
                        default:
                            throw new BadImageFormatException("Bad custom attribute format.");
                    }
                    isEnum = true;
                    goto parseArray;
                }
                else if (identityManager.ObtainTypeReference(targetSig.Target) == identityManager.ObtainTypeReference(RuntimeCoreType.Type))
                {
                    relAssem = identityManager.GetRelativeAssembly(targetSig.Target.MetadataRoot);
                    /* *
                     * Parsing a type from a custom attribute.
                     * */
                    t = typeof(IType);
                    goto parseArray;
                }
                throw new BadImageFormatException("Bad custom attribute format.");
            }
            else if (currentType.TypeSignatureKind != CliMetadataTypeSignatureKind.NativeType)
                throw new BadImageFormatException("Bad custom attribute format.");
            nativeType = (ICliMetadataNativeTypeSignature)currentType;
            switch (nativeType.TypeKind)
            {
                case CliMetadataNativeTypes.Boolean:
                    t = typeof(bool);
                    break;
                case CliMetadataNativeTypes.Char:
                    t = typeof(char);
                    break;
                case CliMetadataNativeTypes.SByte:
                    t = typeof(sbyte);
                    break;
                case CliMetadataNativeTypes.Byte:
                    t = typeof(byte);
                    break;
                case CliMetadataNativeTypes.Int16:
                    t = typeof(short);
                    break;
                case CliMetadataNativeTypes.UInt16:
                    t = typeof(ushort);
                    break;
                case CliMetadataNativeTypes.Int32:
                    t = typeof(int);
                    break;
                case CliMetadataNativeTypes.UInt32:
                    t = typeof(uint);
                    break;
                case CliMetadataNativeTypes.Int64:
                    t = typeof(long);
                    break;
                case CliMetadataNativeTypes.UInt64:
                    t = typeof(ulong);
                    break;
                case CliMetadataNativeTypes.Single:
                    t = typeof(float);
                    break;
                case CliMetadataNativeTypes.Double:
                    t = typeof(double);
                    break;
                case CliMetadataNativeTypes.Type:
                    t = typeof(IType);
                    break;
                case CliMetadataNativeTypes.String:
                    t = typeof(String);
                    break;
                case CliMetadataNativeTypes.Object:
                    t = typeof(object);
                    break;
                default:
                    throw new BadImageFormatException("Bad custom attribute format.");
            }
        parseArray:
            for (int depth = 0; depth < arrayLevel - 1; depth++)
                t = t.MakeArrayType();
            var currentArray = Array.CreateInstance(t, numCases);
            for (int i = 0; i < numCases; i++)
            {
                if (arrayLevel == 1)
                {
                    if (nativeType != null)
                    {
                        switch (nativeType.TypeKind)
                        {
                            case CliMetadataNativeTypes.Boolean:
                                currentArray.SetValue(reader.ReadByte() == 1, i);
                                break;
                            case CliMetadataNativeTypes.Char:
                                currentArray.SetValue(reader.ReadChar(), i);
                                break;
                            case CliMetadataNativeTypes.SByte:
                                currentArray.SetValue(reader.ReadSByte(), i);
                                break;
                            case CliMetadataNativeTypes.Byte:
                                currentArray.SetValue(reader.ReadByte(), i);
                                break;
                            case CliMetadataNativeTypes.Int16:
                                currentArray.SetValue(reader.ReadInt16(), i);
                                break;
                            case CliMetadataNativeTypes.UInt16:
                                currentArray.SetValue(reader.ReadUInt16(), i);
                                break;
                            case CliMetadataNativeTypes.Int32:
                                currentArray.SetValue(reader.ReadInt32(), i);
                                break;
                            case CliMetadataNativeTypes.UInt32:
                                currentArray.SetValue(reader.ReadUInt32(), i);
                                break;
                            case CliMetadataNativeTypes.Int64:
                                currentArray.SetValue(reader.ReadInt64(), i);
                                break;
                            case CliMetadataNativeTypes.UInt64:
                                currentArray.SetValue(reader.ReadUInt64(), i);
                                break;
                            case CliMetadataNativeTypes.Single:
                                currentArray.SetValue(reader.ReadSingle(), i);
                                break;
                            case CliMetadataNativeTypes.Double:
                                currentArray.SetValue(reader.ReadDouble(), i);
                                break;
                            case CliMetadataNativeTypes.String:
                                currentArray.SetValue(ReadSerializedString(reader), i);
                                break;
                            case CliMetadataNativeTypes.Type:
                                currentArray.SetValue(DeserializeType(reader, identityManager, identityManager.GetRelativeAssembly(metadataRoot)), i);
                                break;
                            case CliMetadataNativeTypes.Object:
                                CliMetadataNativeTypes elementKind = (CliMetadataNativeTypes)reader.ReadByte();
                                switch (elementKind)
                                {
                                    case CliMetadataNativeTypes.Boolean:
                                        currentArray.SetValue(reader.ReadByte() == 1, i);
                                        break;
                                    case CliMetadataNativeTypes.Char:
                                        currentArray.SetValue(reader.ReadChar(), i);
                                        break;
                                    case CliMetadataNativeTypes.SByte:
                                        currentArray.SetValue(reader.ReadSByte(), i);
                                        break;
                                    case CliMetadataNativeTypes.Byte:
                                        currentArray.SetValue(reader.ReadByte(), i);
                                        break;
                                    case CliMetadataNativeTypes.Int16:
                                        currentArray.SetValue(reader.ReadInt16(), i);
                                        break;
                                    case CliMetadataNativeTypes.UInt16:
                                        currentArray.SetValue(reader.ReadUInt16(), i);
                                        break;
                                    case CliMetadataNativeTypes.Int32:
                                        currentArray.SetValue(reader.ReadInt32(), i);
                                        break;
                                    case CliMetadataNativeTypes.UInt32:
                                        currentArray.SetValue(reader.ReadUInt32(), i);
                                        break;
                                    case CliMetadataNativeTypes.Int64:
                                        currentArray.SetValue(reader.ReadInt64(), i);
                                        break;
                                    case CliMetadataNativeTypes.UInt64:
                                        currentArray.SetValue(reader.ReadUInt64(), i);
                                        break;
                                    case CliMetadataNativeTypes.Single:
                                        currentArray.SetValue(reader.ReadSingle(), i);
                                        break;
                                    case CliMetadataNativeTypes.Double:
                                        currentArray.SetValue(reader.ReadDouble(), i);
                                        break;
                                    case CliMetadataNativeTypes.String:
                                        currentArray.SetValue(ReadSerializedString(reader), i);
                                        break;
                                    case CliMetadataNativeTypes.Type:
                                        throw new NotImplementedException();
                                        break;
                                    case CliMetadataNativeTypes.EnumSignal:
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        var decodedType = DeserializeType(reader, identityManager, relAssem);
                        currentArray.SetValue(decodedType, i);
                    }
                }
                else
                    currentArray.SetValue(ParseCustomAttributeArrayValue(reader, metadataRoot, identityManager, currentType, arrayLevel - 1, start, dataLength, false), i);
            }
            if (topLevel && isEnum)
                return Tuple.Create((IType)targetEnum, currentArray);
            return currentArray;
        }

        private static IType DeserializeType(EndianAwareBinaryReader reader, _ICliManager identityManager, _ICliAssembly relAssem)
        {
            var relAssemId = relAssem.UniqueIdentifier;
            var serializedTypeName = ReadSerializedString(reader);
            if (serializedTypeName == null)
                return null;
            var parser = new TypeIdentityParser(serializedTypeName, new TIAssemblyIdentityRule(relAssemId.Name, new TIVersionRule(relAssemId.Version.Major, relAssemId.Version.Minor, relAssemId.Version.Build, relAssemId.Version.Revision), relAssemId.Culture, relAssemId.PublicKeyToken));
            var typeIdentity = parser.ParseQualifiedTypeName();
            var pos = reader.BaseStream.Position;
            var decodedType = typeIdentity.DecodeParsedType(identityManager);
            reader.BaseStream.Position = pos;
            return decodedType;
        }
        internal static ICliMetadataCustomAttributeNamedParameter ParseCustomAttributeNamedParameter(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, _ICliManager identityManager, long start, uint dataLength)
        {
            var targetElement = reader.ReadByte();
            NamedParameterTargetType target = NamedParameterTargetType.NotSet;
            if (targetElement == (int)CliMetadataNativeTypes.PropertySignal)
                target = NamedParameterTargetType.Property;
            else if (targetElement == (int)CliMetadataNativeTypes.FieldSignal)
                target = NamedParameterTargetType.Field;
            else
                throw new BadImageFormatException("Bad custom attribute format.");
            var propType = (CliMetadataNativeTypes)reader.ReadByte();
            object value = null;
            string name;
            CustomAttributeParameterValueType parameterType;
            switch (propType)
            {
                case CliMetadataNativeTypes.Boolean:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadByte() == 1;
                    break;
                case CliMetadataNativeTypes.Char:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadChar();
                    break;
                case CliMetadataNativeTypes.SByte:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadSByte();
                    break;
                case CliMetadataNativeTypes.Byte:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadByte();
                    break;
                case CliMetadataNativeTypes.Int16:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadInt16();
                    break;
                case CliMetadataNativeTypes.UInt16:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadUInt16();
                    break;
                case CliMetadataNativeTypes.Int32:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadInt32();
                    break;
                case CliMetadataNativeTypes.UInt32:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadUInt32();
                    break;
                case CliMetadataNativeTypes.Int64:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadInt64();
                    break;
                case CliMetadataNativeTypes.UInt64:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadUInt64();
                    break;
                case CliMetadataNativeTypes.Single:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadSingle();
                    break;
                case CliMetadataNativeTypes.Double:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    value = reader.ReadDouble();
                    break;
                case CliMetadataNativeTypes.String:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.String;
                    value = ReadSerializedString(reader);
                    break;
                case CliMetadataNativeTypes.Object:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.BoxedNativeType;
                    break;
                case CliMetadataNativeTypes.VectorArray:
                    byte current = (byte)propType;
                    int arrayLevel = 0;
                    while (current == (byte)CliMetadataNativeTypes.VectorArray)
                    {
                        arrayLevel++;
                        current = reader.ReadByte();
                    }
                    ICliMetadataTypeSignature arrayBaseType = null;
                    switch (((CliMetadataNativeTypes)(current)))
                    {
                        case CliMetadataNativeTypes.Boolean:
                        case CliMetadataNativeTypes.Char:
                        case CliMetadataNativeTypes.SByte:
                        case CliMetadataNativeTypes.Byte:
                        case CliMetadataNativeTypes.Int16:
                        case CliMetadataNativeTypes.UInt16:
                        case CliMetadataNativeTypes.Int32:
                        case CliMetadataNativeTypes.UInt32:
                        case CliMetadataNativeTypes.Int64:
                        case CliMetadataNativeTypes.UInt64:
                        case CliMetadataNativeTypes.Single:
                        case CliMetadataNativeTypes.Double:
                        case CliMetadataNativeTypes.String:
                        case CliMetadataNativeTypes.Object:
                        case CliMetadataNativeTypes.Type:
                            arrayBaseType = new CliMetadataNativeTypeSignature((CliMetadataNativeTypes)current);
                            break;
                        case CliMetadataNativeTypes.EnumSignal:
                            var enumType = DeserializeType(reader, identityManager, identityManager.GetRelativeAssembly(metadataRoot));
                            IEnumType targetEnum = (IEnumType)enumType;
                            switch (targetEnum.ValueType)
                            {
                                case EnumerationBaseType.Default:
                                case EnumerationBaseType.Int32:
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Int32);
                                    break;
                                case EnumerationBaseType.SByte:
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.SByte);
                                    break;
                                case EnumerationBaseType.Byte:
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Byte);
                                    break;
                                case EnumerationBaseType.Int16:
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Int16);
                                    break;
                                case EnumerationBaseType.UInt16:
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.UInt16);
                                    break;
                                case EnumerationBaseType.UInt32:
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.UInt32);
                                    break;
                                case EnumerationBaseType.Int64:
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Int64);
                                    break;
                                case EnumerationBaseType.UInt64:
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.UInt64);
                                    break;
                                case EnumerationBaseType.NativeInteger:
#if x86
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Int32);
                                    break;
#elif x64
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Int64);
                                    break;
#endif
                                case EnumerationBaseType.NativeUnsignedInteger:
#if x86
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.UInt32);
                                    break;
#elif x64
                                    arrayBaseType = new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.UInt64);
                                    break;
#endif
                                default:
                                    throw new BadImageFormatException("Bad custom attribute format.");
                            }
                            break;
                        default:
                            break;
                    }
                    parameterType = CustomAttributeParameterValueType.VectorArray;
                    name = ReadSerializedString(reader);
                    value = ParseCustomAttributeArrayValue(reader, metadataRoot, identityManager, arrayBaseType, arrayLevel, start, dataLength, true);
                    break;
                case CliMetadataNativeTypes.Type:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.Type;
                    break;
                case CliMetadataNativeTypes.EnumSignal:
                    name = ReadSerializedString(reader);
                    parameterType = CustomAttributeParameterValueType.EnumValue;
                    break;
                default:
                    throw new BadImageFormatException("Bad custom attribute format.");
            }

            return new CliMetadataCustomAttributeNamedParameter(name, target, parameterType, value);
        }

        internal static string ReadSerializedString(EndianAwareBinaryReader reader)
        {
            if (reader.PeekByte() == 0xFF)
            {
                reader.ReadByte();
                return null;
            }
            var numBytes = CliMetadataFixedRoot.ReadCompressedUnsignedInt(reader);
            var stringBytes = reader.ReadBytes(numBytes);
            return CliMetadataStringsHeaderAndHeap.ConvertUTF8ByteArray(stringBytes);
        }
    }
}
