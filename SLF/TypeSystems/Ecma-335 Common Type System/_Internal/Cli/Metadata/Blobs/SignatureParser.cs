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
            var assem = identityManager.GetRelativeAssembly(metadataRoot);
            long startIndex = reader.BaseStream.Position;
            var prolog = reader.ReadUInt16();
            if (prolog != 0x0001)
                throw new BadImageFormatException("Bad custom attribute format.");
            ICliMetadataCustomAttributeParameter[] parameters = null;
            long rIndex = reader.BaseStream.Position;
            var caParams = target.GetParameterCollection();
            parameters = new ICliMetadataCustomAttributeParameter[caParams.Count];
            reader.BaseStream.Position = rIndex;
            for (int i = 0; i < parameters.Length; i++)
            {
                var currentSigParam = caParams[i];
                parameters[i] = ParseCustomAttributeParameter(reader, metadataRoot, identityManager, currentSigParam.ParameterType, startIndex, dataLength, assem);
            }
            if (reader.BaseStream.Position >= startIndex + dataLength)
                return new CliMetadataCustomAttribute(parameters, null);
            int numNamed = reader.ReadUInt16();
            var namedParameters = new ICliMetadataCustomAttributeNamedParameter[numNamed];
            for (int i = 0; i < numNamed; i++)
                namedParameters[i] = ParseCustomAttributeNamedParameter(reader, metadataRoot, identityManager, assem);
            return new CliMetadataCustomAttribute(parameters, namedParameters);
        }

        internal static ICliMetadataCustomAttributeParameter ParseCustomAttributeParameter(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, _ICliManager identityManager, ICliMetadataTypeSignature type, long start, uint dataLength, ICliAssembly assembly)
        {
            switch (type.TypeSignatureKind)
            {
                case CliMetadataTypeSignatureKind.NativeType:
                    var nativeType = (ICliMetadataNativeTypeSignature)type;
                    switch (nativeType.TypeKind)
                    {
                        case CliMetadataNativeTypes.Object:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.BoxedNativeType, ParseCustomAttributeBoxedType(reader, metadataRoot, identityManager, false, assembly));
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
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.NativeType, ParseCustomAttributeNativeValue(reader, nativeType.TypeKind, metadataRoot, identityManager, assembly));
                        case CliMetadataNativeTypes.Type:
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.Type, DeserializeType(reader, identityManager, identityManager.GetRelativeAssembly(metadataRoot)));
                        default:
                            throw new BadImageFormatException("Bad custom attribute format.");
                    }
                case CliMetadataTypeSignatureKind.ValueOrClassType:
                    {
                        ICliMetadataValueOrClassTypeSignature targetSig = (ICliMetadataValueOrClassTypeSignature)type;
                        if (targetSig.IsValueType)
                        {
                            var pos = reader.BaseStream.Position;
                            ICliMetadataTypeDefOrRefRow rowData = targetSig.Target;
                            var targetType = identityManager.ResolveScope(rowData);
                            if (!CliCommon.IsEnum(identityManager, targetType))
                                throw new BadImageFormatException("Bad custom attribute format.");
                            var enumType = targetType.GetEnumBaseType();
                            reader.BaseStream.Position = pos;
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.EnumValue, ParseCustomAttributeNativeValue(reader, GetEnumNativeType(enumType), metadataRoot, identityManager, assembly, false));
                        }
                        else if (identityManager.ObtainTypeReference(targetSig.Target) == identityManager.ObtainTypeReference(RuntimeCoreType.Type, assembly))
                            return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.Type, DeserializeType(reader, identityManager, assembly));
                        throw new BadImageFormatException("Bad custom attribute format.");
                    }
                case CliMetadataTypeSignatureKind.VectorArrayType:
                    {
                        int arrayLevel = 0;
                        ICliMetadataTypeSignature currentType = type;
                        while (currentType.TypeSignatureKind == CliMetadataTypeSignatureKind.VectorArrayType)
                        {
                            arrayLevel++;
                            currentType = ((ICliMetadataVectorArrayTypeSignature)(currentType)).ElementType;
                        }
                        CliMetadataNativeTypes nativeTypeKind = CliMetadataNativeTypes.ListEnd;
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
                                nativeTypeKind = GetEnumNativeType(targetType.GetEnumBaseType());
                                reader.BaseStream.Position = pos;
                            }
                            else if (identityManager.ObtainTypeReference(targetSig.Target) == identityManager.ObtainTypeReference(RuntimeCoreType.Type))
                                nativeTypeKind = CliMetadataNativeTypes.Type;
                            else
                                throw new BadImageFormatException("Bad custom attribute format.");

                        }
                        return new CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType.VectorArray, ParseCustomAttributeArrayValue(reader, metadataRoot, identityManager, nativeTypeKind, arrayLevel, assembly));
                    }
                default:
                    throw new BadImageFormatException("Bad custom attribute format.");
            }
        }

        private static CliMetadataNativeTypes GetEnumNativeType(EnumerationBaseType enumBaseType)
        {
            switch (enumBaseType)
            {
                case EnumerationBaseType.SByte:
                    return CliMetadataNativeTypes.SByte;
                case EnumerationBaseType.Byte:
                    return CliMetadataNativeTypes.Byte;
                case EnumerationBaseType.Int16:
                    return CliMetadataNativeTypes.Int16;
                case EnumerationBaseType.UInt16:
                    return CliMetadataNativeTypes.UInt16;
                case EnumerationBaseType.Default:
                case EnumerationBaseType.Int32:
                    return CliMetadataNativeTypes.Int32;
                case EnumerationBaseType.UInt32:
                    return CliMetadataNativeTypes.UInt32;
                case EnumerationBaseType.Int64:
                    return CliMetadataNativeTypes.Int64;
                case EnumerationBaseType.UInt64:
                    return CliMetadataNativeTypes.UInt64;
                case EnumerationBaseType.NativeInteger:
#if x86
                    return CliMetadataNativeTypes.Int32;
#elif x64
                    return CliMetadataNativeTypes.Int64;
#endif
                case EnumerationBaseType.NativeUnsignedInteger:
#if x86
                    return CliMetadataNativeTypes.UInt32;
#elif x64
                    return CliMetadataNativeTypes.UInt64;
#endif
                default:
                    throw new ArgumentOutOfRangeException("enumBaseType");
            }
        }

        /// <summary>
        /// Parses an array from the stream of the <paramref name="reader"/>
        /// provided, using the <paramref name="identityManager"/> to resolve the
        /// serialized types.
        /// </summary>
        /// <param name="reader">The <see cref="EndianAwareBinaryReader"/> to use to 
        /// parse the array value.</param>
        /// <param name="metadataRoot">The <see cref="CliMetadataFixedRoot"/>
        /// which contains the necessary information about the active assembly in the event
        /// that deserialized types have no assembly present in the string.</param>
        /// <param name="identityManager">The <see cref="_ICliManager"/> used to 
        /// resolve the identity of types within the stream.</param>
        /// <param name="nativeType">The <see cref="CliMetadataNativeTypes"/>
        /// which denotes the type of the elements within the array to parse.</param>
        /// <param name="arrayLevel">The nesting level of the possibly jagged array.</param>
        /// <returns>An <see cref="Object"/> representing the array that was serialized into
        /// the stream of <paramref name="reader"/></returns>
        private static object ParseCustomAttributeArrayValue(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, _ICliManager identityManager, CliMetadataNativeTypes nativeType, int arrayLevel, ICliAssembly assembly)
        {
            var numCases = reader.ReadInt32();
            Type arrayElementType;
            switch (nativeType)
            {
                case CliMetadataNativeTypes.Boolean:
                    arrayElementType = typeof(bool);
                    break;
                case CliMetadataNativeTypes.Char:
                    arrayElementType = typeof(char);
                    break;
                case CliMetadataNativeTypes.SByte:
                    arrayElementType = typeof(sbyte);
                    break;
                case CliMetadataNativeTypes.Byte:
                    arrayElementType = typeof(byte);
                    break;
                case CliMetadataNativeTypes.Int16:
                    arrayElementType = typeof(short);
                    break;
                case CliMetadataNativeTypes.UInt16:
                    arrayElementType = typeof(ushort);
                    break;
                case CliMetadataNativeTypes.Int32:
                    arrayElementType = typeof(int);
                    break;
                case CliMetadataNativeTypes.UInt32:
                    arrayElementType = typeof(uint);
                    break;
                case CliMetadataNativeTypes.Int64:
                    arrayElementType = typeof(long);
                    break;
                case CliMetadataNativeTypes.UInt64:
                    arrayElementType = typeof(ulong);
                    break;
                case CliMetadataNativeTypes.Single:
                    arrayElementType = typeof(float);
                    break;
                case CliMetadataNativeTypes.Double:
                    arrayElementType = typeof(double);
                    break;
                case CliMetadataNativeTypes.Type:
                    arrayElementType = typeof(IType);
                    break;
                case CliMetadataNativeTypes.String:
                    arrayElementType = typeof(String);
                    break;
                case CliMetadataNativeTypes.Object:
                    arrayElementType = typeof(object);
                    break;
                default:
                    throw new BadImageFormatException("Bad custom attribute format.");
            }
            for (int depth = 0; depth < arrayLevel - 1; depth++)
                arrayElementType = arrayElementType.MakeArrayType();
            var currentArray = Array.CreateInstance(arrayElementType, numCases);
            for (int i = 0; i < numCases; i++)
            {
                if (arrayLevel == 1)
                    currentArray.SetValue(ParseCustomAttributeNativeValue(reader, nativeType, metadataRoot, identityManager, assembly), i);
                else
                    currentArray.SetValue(ParseCustomAttributeArrayValue(reader, metadataRoot, identityManager, nativeType, arrayLevel - 1, assembly), i);
            }
            return currentArray;
        }

        /// <summary>
        /// Deserializes a native type from the stream of the <paramref name="reader"/>
        /// provided.
        /// </summary>
        /// <param name="reader">The <see cref="EndianAwareBinaryReader"/> to use to 
        /// parse the native type.</param>
        /// <param name="nativeType">The <see cref="CliMetadataNativeTypes"/>
        /// used to denote the data type to parse from the stream of <paramref name="reader"/>.</param>
        /// <param name="metadataRoot">The <see cref="CliMetadataFixedRoot"/>
        /// which contains the necessary information about the active assembly in the event
        /// that deserialized types have no assembly present in the string.</param>
        /// <param name="identityManager">The <see cref="_ICliManager"/> used to 
        /// resolve the identity of types within the stream.</param>
        /// <param name="encapsulateEnum">Whether to encapsulate the type of enumeration types
        /// within a tuple to denote the source type of the enumeration.</param>
        /// <param name="beforeParseValue">An action necessary to be utilized just before 
        /// parsing the value, in case information comes before the serialized value.</param>
        /// <returns>An <see cref="Object"/> representing an instance of the <paramref name="nativeType"/>
        /// provided.</returns>
        private static object ParseCustomAttributeNativeValue(EndianAwareBinaryReader reader, CliMetadataNativeTypes nativeType, CliMetadataFixedRoot metadataRoot, _ICliManager identityManager, ICliAssembly assembly, bool encapsulateEnum = true, Action beforeParseValue = null)
        {
            switch (nativeType)
            {
                case CliMetadataNativeTypes.Boolean:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadByte() == 1;
                case CliMetadataNativeTypes.Char:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadChar();
                case CliMetadataNativeTypes.SByte:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadSByte();
                case CliMetadataNativeTypes.Byte:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadByte();
                case CliMetadataNativeTypes.Int16:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadInt16();
                case CliMetadataNativeTypes.UInt16:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadUInt16();
                case CliMetadataNativeTypes.Int32:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadInt32();
                case CliMetadataNativeTypes.UInt32:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadUInt32();
                case CliMetadataNativeTypes.Int64:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadInt64();
                case CliMetadataNativeTypes.UInt64:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadUInt64();
                case CliMetadataNativeTypes.Single:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadSingle();
                case CliMetadataNativeTypes.Double:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return reader.ReadDouble();
                case CliMetadataNativeTypes.String:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return ReadSerializedString(reader);
                case CliMetadataNativeTypes.Type:
                    if (beforeParseValue != null)
                        beforeParseValue();
                    return DeserializeType(reader, identityManager, assembly);
                case CliMetadataNativeTypes.Object:
                case CliMetadataNativeTypes.Boxed:
                    return ParseCustomAttributeBoxedType(reader, metadataRoot, identityManager, encapsulateEnum, assembly, beforeParseValue);
                case CliMetadataNativeTypes.VectorArray:
                    {
                        byte current = (byte)nativeType;
                        bool isEnum = false;
                        int arrayLevel = 0;
                        while (current == (byte)CliMetadataNativeTypes.VectorArray)
                        {
                            arrayLevel++;
                            current = reader.ReadByte();
                        }
                        ICliMetadataTypeSignature arrayBaseType = null;
                        IEnumType targetEnum = null;
                        if (current == (byte)CliMetadataNativeTypes.Boxed)
                            current = (byte)CliMetadataNativeTypes.Object;
                        var nativeTypeKind = ((CliMetadataNativeTypes)(current));
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
                                break;
                            case CliMetadataNativeTypes.EnumSignal:
                                var enumType = DeserializeType(reader, identityManager, assembly);
                                isEnum = true;
                                targetEnum = (IEnumType)enumType;
                                var position = reader.BaseStream.Position;
                                nativeTypeKind = GetEnumNativeType(targetEnum.ValueType);
                                reader.BaseStream.Position = position;
                                break;
                            default:
                                break;
                        }
                        if (beforeParseValue != null)
                            beforeParseValue();
                        var result = ParseCustomAttributeArrayValue(reader, metadataRoot, identityManager, nativeTypeKind, arrayLevel, assembly);
                        if (isEnum && encapsulateEnum)
                            return Tuple.Create((IType)targetEnum, result);
                        else
                            return result;
                    }
                case CliMetadataNativeTypes.EnumSignal:
                    {
                        var enumType = DeserializeType(reader, identityManager, assembly);
                        IEnumType targetEnum = (IEnumType)enumType;
                        var position = reader.BaseStream.Position;
                        var valueType = targetEnum.ValueType;
                        reader.BaseStream.Position = position;
                        if (beforeParseValue != null)
                            beforeParseValue();
                        var value = ParseCustomAttributeNativeValue(reader, GetEnumNativeType(valueType), metadataRoot, identityManager, assembly);
                        if (encapsulateEnum)
                            return Tuple.Create(targetEnum, value);
                        else
                            return value;
                    }
                default:
                    throw new BadImageFormatException("Bad custom attribute format.");

            }
                throw new BadImageFormatException("Bad custom attribute format.");
        }

        /// <summary>
        /// Deserializes a type from the stream of the <paramref name="reader"/> provided,
        /// using the <paramref name="identityManager"/> to resolve the identity, and
        /// the <paramref name="relAssem"/> to denote the active assembly for serialized
        /// types which omit the assembly identity.
        /// </summary>
        /// <param name="reader">The <see cref="EndianAwareBinaryReader"/> to use to 
        /// deserialize the type.</param>
        /// <param name="identityManager">The <see cref="_ICliManager"/> used to 
        /// resolve the identity of types within the stream.</param>
        /// <param name="relAssem">The <see cref="_ICliAssembly"/> which denotes the
        /// active assembly for serialized types which omit the assembly identity.</param>
        /// <returns></returns>
        private static IType DeserializeType(EndianAwareBinaryReader reader, _ICliManager identityManager, ICliAssembly relAssem)
        {
            var relAssemId = relAssem.UniqueIdentifier;
            var serializedTypeName = ReadSerializedString(reader);
            if (serializedTypeName == null)
                return null;
            var parser = new TypeIdentityParser(serializedTypeName, new TIAssemblyIdentityRule(relAssemId.Name, new TIVersionRule(relAssemId.Version.Major, relAssemId.Version.Minor, relAssemId.Version.Build, relAssemId.Version.Revision), relAssemId.Culture, relAssemId.PublicKeyToken));
            var typeIdentity = parser.ParseQualifiedTypeName();
            var pos = reader.BaseStream.Position;
            var decodedType = typeIdentity.DecodeParsedType(identityManager, relAssem);
            reader.BaseStream.Position = pos;
            return decodedType;
        }

        /// <summary>
        /// Parses a named parameter from a custom attribute as defined by
        /// the ECMA-335 specification.
        /// </summary>
        /// <param name="reader">The <see cref="EndianAwareBinaryReader"/> to use to 
        /// parse the named parameter from.</param>
        /// <param name="metadataRoot">The <see cref="CliMetadataFixedRoot"/>
        /// which contains the necessary information about the active assembly in the event
        /// that deserialized types have no assembly present in the string.</param>
        /// <param name="identityManager">The <see cref="_ICliManager"/> used to 
        /// resolve the identity of types within the stream.</param>
        /// <returns>An instance implementing <see cref="ICliMetadataCustomAttributeNamedParameter"/>
        /// which represents the deserialized named parameter.</returns>
        internal static ICliMetadataCustomAttributeNamedParameter ParseCustomAttributeNamedParameter(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, _ICliManager identityManager, ICliAssembly assembly)
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
            string name = null;
            CustomAttributeParameterValueType parameterType;

            value = ParseCustomAttributeNativeValue(reader, propType, metadataRoot, identityManager, assembly, true, () => name = ReadSerializedString(reader));
            switch (propType)
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
                    parameterType = CustomAttributeParameterValueType.NativeType;
                    break;
                case CliMetadataNativeTypes.String:
                    parameterType = CustomAttributeParameterValueType.String;
                    break;
                case CliMetadataNativeTypes.Object:
                case CliMetadataNativeTypes.Boxed:
                    parameterType = CustomAttributeParameterValueType.BoxedNativeType;
                    break;
                case CliMetadataNativeTypes.VectorArray:
                    parameterType = CustomAttributeParameterValueType.VectorArray;
                    break;
                case CliMetadataNativeTypes.Type:
                    parameterType = CustomAttributeParameterValueType.Type;
                    break;
                case CliMetadataNativeTypes.EnumSignal:
                    parameterType = CustomAttributeParameterValueType.EnumValue;
                    break;
                default:
                    throw new BadImageFormatException("Bad custom attribute format.");
            }

            return new CliMetadataCustomAttributeNamedParameter(name, target, parameterType, value);
        }

        private static object ParseCustomAttributeBoxedType(EndianAwareBinaryReader reader, CliMetadataFixedRoot metadataRoot, _ICliManager identityManager, bool encapsulateEnum, ICliAssembly assembly, Action beforeParseValue = null)
        {
            if (beforeParseValue != null)
                beforeParseValue();
            CliMetadataNativeTypes elementKind = (CliMetadataNativeTypes)reader.ReadByte();
            return ParseCustomAttributeNativeValue(reader, elementKind, metadataRoot, identityManager, assembly, encapsulateEnum);
        }

        /// <summary>
        /// Parses a SerString from the ECMA-335 specification.
        /// </summary>
        /// <param name="reader">The <see cref="EndianAwareBinaryReader"/>
        /// to read the data from.</param>
        /// <returns>A <see cref="String"/> deserialized from the stream of the
        /// <paramref name="reader"/>.</returns>
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
