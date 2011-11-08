using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Properties;

namespace AllenCopeland.Abstraction
{
    internal static class ThrowHelper
    {
        private static readonly string[] emptyReplacements = new string[0];

        public static NotSupportedException ObtainNotSupportedException(ExceptionMessageId message, string replacement)
        {
            return new NotSupportedException(GetExceptionMessage(message, new[] { replacement }));
        }

        public static NotSupportedException ObtainNotSupportedException(ExceptionMessageId message, string replacement1, string replacement2)
        {
            return new NotSupportedException(GetExceptionMessage(message, new[] { replacement1, replacement2 }));
        }

        public static NotSupportedException ObtainNotSupportedException(ExceptionMessageId message, string replacement1, string replacement2, string replacement3)
        {
            return new NotSupportedException(GetExceptionMessage(message, new[] { replacement1, replacement2, replacement3 }));
        }

        public static NotSupportedException ObtainNotSupportedException(ExceptionMessageId message, params string[] replacements)
        {
            return new NotSupportedException(GetExceptionMessage(message, replacements ?? emptyReplacements));
        }

        public static ArgumentOutOfRangeException ObtainArgumentOutOfRangeException(ArgumentWithException argument)
        {
            return new ArgumentOutOfRangeException(GetArgumentName(argument));
        }

        public static ArgumentOutOfRangeException ObtainArgumentOutOfRangeException(ArgumentWithException argument, ExceptionMessageId message)
        {
            return ObtainArgumentOutOfRangeException(argument, message, emptyReplacements);
        }
        public static ArgumentOutOfRangeException ObtainArgumentOutOfRangeException(ArgumentWithException argument, ExceptionMessageId message, params string[] replacements)
        {
            return new ArgumentOutOfRangeException(GetArgumentName(argument), GetExceptionMessage(message, replacements ?? emptyReplacements));
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ExceptionMessageId message)
        {
            return ObtainArgumentException(argument, message, emptyReplacements);
        }
        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ExceptionMessageId message, string replacement)
        {
            return ObtainArgumentException(argument, message, new[] { replacement });
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ExceptionMessageId message, string replacement1, string replacement2)
        {
            return ObtainArgumentException(argument, message, new[] { replacement1, replacement2 });
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ExceptionMessageId message, string replacement1, string replacement2, string replacement3)
        {
            return ObtainArgumentException(argument, message, new[] { replacement1, replacement2, replacement3 });
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ExceptionMessageId message, params string[] replacements)
        {
            return new ArgumentException(GetExceptionMessage(message, replacements ?? emptyReplacements), GetArgumentName(argument));
        }

        public static string GetArgumentExceptionWord(ExceptionWordId word)
        {
            switch (word)
            {
                case ExceptionWordId.array:
                    return "array";
                case ExceptionWordId.assembly:
                    return "assembly";
                case ExceptionWordId.by_reference_type:
                    return "by reference type";
                case ExceptionWordId.@class:
                    return "class";
                case ExceptionWordId.cs_bitwise_and:
                    return "C\u266F bitwise and";
                case ExceptionWordId.cs_bitwise_or:
                    return "C\u266F bitwise or";
                case ExceptionWordId.cs_bitwise_xor:
                    return "C\u266F bitwise exclusive or";
                case ExceptionWordId.cs_conditional:
                    return "C\u266F conditional";
                case ExceptionWordId.cs_logical_and:
                    return "C\u266F logical and";
                case ExceptionWordId.cs_logical_or:
                    return "C\u266F logical or";
                case ExceptionWordId.@delegate:
                    return "delegate";
                case ExceptionWordId.@enum:
                    return "enum";
                case ExceptionWordId.@explicit:
                    return "explicit";
                case ExceptionWordId.from:
                    return "from";
                case ExceptionWordId.@implicit:
                    return "implicit";
                case ExceptionWordId.@interface:
                    return "interface";
                case ExceptionWordId.@namespace:
                    return "namespace";
                case ExceptionWordId.nullable:
                    return "nullable";
                case ExceptionWordId.pointer:
                    return "pointer";
                case ExceptionWordId.Source:
                    return "Source";
                case ExceptionWordId.@struct:
                    return "struct";
                case ExceptionWordId.to:
                    return "to";
                default:
                    throw ObtainArgumentOutOfRangeException(ArgumentWithException.word, ExceptionMessageId.UnknownArgumentWord);
            }
        }

        private static string GetExceptionMessage(ExceptionMessageId message, params string[] replacements)
        {
            switch (message)
            {
                case ExceptionMessageId.ArgumentCannotBeEmpty:
                    return string.Format(Resources.AE_ArgumentCannotBeEmpty, replacements);
                case ExceptionMessageId.CannotTermBinaryOperation:
                    return string.Format(Resources.AE_CannotTermBinaryOperation, replacements);
                case ExceptionMessageId.CoercionDoesNotExist:
                    return string.Format(Resources.AE_CoercionDoesNotExist, replacements);
                case ExceptionMessageId.CompiledType_NotProperKind:
                    return string.Format(Resources.AE_CompiledType_NotProperKind, replacements);
                case ExceptionMessageId.ConditionMustBeBreakable:
                    return string.Format(Resources.AE_ConditionMustBeBreakable, replacements);
                case ExceptionMessageId.DataCannotBeEmpty:
                    return string.Format(Resources.AE_DataCannotBeEmpty, replacements);
                case ExceptionMessageId.DelegateTypeParameterMismatch:
                    return string.Format(Resources.AE_DelegateTypeParameterMismatch, replacements);
                case ExceptionMessageId.DetachedExpressionNotValidFor:
                    return string.Format(Resources.AE_DetachedExpressionNotValidFor, replacements);
                case ExceptionMessageId.DuplicateKeyExists:
                    return string.Format(Resources.AE_DuplicateKeyExists, replacements);
                case ExceptionMessageId.ElementTypeMustBeGivenKind:
                    return string.Format(Resources.AE_ElementTypeMustBeGivenKind, replacements);
                case ExceptionMessageId.GenericClosureReplacementCount:
                    return string.Format(Resources.AE_GenericClosureReplacementCount, replacements);
                case ExceptionMessageId.GenericParameterRequiresName:
                    return string.Format(Resources.AE_GenericParameterRequiresName, replacements);
                case ExceptionMessageId.InsufficientSpaceForCopy:
                    return string.Format(Resources.AE_InsufficientSpaceForCopy, replacements);
                case ExceptionMessageId.InterfaceNotImplemented:
                    return string.Format(Resources.AE_InterfaceNotImplemented, replacements);
                case ExceptionMessageId.InvalidTCheckDerivation:
                    return string.Format(Resources.AE_InvalidTCheckDerivation, replacements);
                case ExceptionMessageId.JumpTargetMustBeLabel:
                    return string.Format(Resources.AE_JumpTargetMustBeLabel, replacements);
                case ExceptionMessageId.ManifestModuleTarget:
                    return string.Format(Resources.AE_ManifestModuleTarget, replacements);
                case ExceptionMessageId.MemberOfSeriesNull:
                    return string.Format(Resources.AE_MemberOfSeriesNull, replacements);
                case ExceptionMessageId.NamedGenericParameterExists:
                    return string.Format(Resources.AE_NamedGenericParameterExists, replacements);
                case ExceptionMessageId.NamespacePathAlreadyPresent:
                    return string.Format(Resources.AE_NamespacePathAlreadyPresent, replacements);
                case ExceptionMessageId.NonNullableTypeProvided:
                    return string.Format(Resources.AE_NonNullableTypeProvided, replacements);
                case ExceptionMessageId.NumericControllerMismatch:
                    return string.Format(Resources.AE_NumericControllerMismatch, replacements);
                case ExceptionMessageId.NumericValueParseError:
                    return string.Format(Resources.AE_NumericValueParseError, replacements);
                case ExceptionMessageId.OnlyNullableOnTerm:
                    return string.Format(Resources.AE_OnlyNullableOnTerm, replacements);
                case ExceptionMessageId.OperationMustBeTerm:
                    return string.Format(Resources.AE_OperationMustBeTerm, replacements);
                case ExceptionMessageId.ParentMustBeEqual:
                    return string.Format(Resources.AE_ParentMustBeEqual, replacements);
                case ExceptionMessageId.Part_CannotBeRoot:
                    return string.Format(Resources.AE_Part_CannotBeRoot, replacements);
                case ExceptionMessageId.Part_MustReferenceRoot:
                    return string.Format(Resources.AE_Part_MustReferenceRoot, replacements);
                case ExceptionMessageId.Part_RootOfASeparateSeries:
                    return string.Format(Resources.AE_Part_RootOfASeparateSeries, replacements);
                case ExceptionMessageId.PathCannotBeDotsOnly:
                    return string.Format(Resources.AE_PathCannotBeDotsOnly, replacements);
                case ExceptionMessageId.Primitive_Invalid:
                    return string.Format(Resources.AE_Primitive_Invalid, replacements);
                case ExceptionMessageId.Primitive_NonStringObject:
                    return string.Format(Resources.AE_Primitive_NonStringObject, replacements);
                case ExceptionMessageId.ProvidedExpressionCannotBe:
                    return string.Format(Resources.AE_ProvidedExpressionCannotBe, replacements);
                case ExceptionMessageId.RankMustBeOneOrGreater:
                    return string.Format(Resources.AE_RankMustBeOneOrGreater, replacements);
                case ExceptionMessageId.RelationalInvalidOnExpression:
                    return string.Format(Resources.AE_RelationalInvalidOnExpression, replacements);
                case ExceptionMessageId.Remove_ValueNotFound:
                    return string.Format(Resources.AE_Remove_ValueNotFound, replacements);
                case ExceptionMessageId.RemoveFailed_CustomAttributeNotFound:
                    return string.Format(Resources.AE_RemoveFailed_CustomAttributeNotFound, replacements);
                case ExceptionMessageId.SourceStringInvalid:
                    return string.Format(Resources.AE_SourceStringInvalid, replacements);
                case ExceptionMessageId.SubordinateCannotChange:
                    return string.Format(Resources.AE_SubordinateCannotChange, replacements);
                case ExceptionMessageId.SubordinateDoesNotExist:
                    return string.Format(Resources.AE_SubordinateDoesNotExist, replacements);
                case ExceptionMessageId.SubordinateInvalid:
                    return string.Format(Resources.AE_SubordinateInvalid, replacements);
                case ExceptionMessageId.SubordinateMismatch:
                    return string.Format(Resources.AE_SubordinateMismatch, replacements);
                case ExceptionMessageId.SubordinateNull:
                    return string.Format(Resources.AE_SubordinateNull, replacements);
                case ExceptionMessageId.SubordinateSubTypeDuplicate:
                    return string.Format(Resources.AE_SubordinateSubTypeDuplicate, replacements);
                case ExceptionMessageId.SourceStreamInvalidLength:
                    return string.Format(Resources.AE_SourceStreamInvalidLength, replacements);
                case ExceptionMessageId.TransitionKeyCollision:
                    return string.Format(Resources.AE_TransitionKeyCollision, replacements);
                case ExceptionMessageId.TypeAlreadyGenericClosure:
                    return string.Format(Resources.AE_TypeAlreadyGenericClosure, replacements);
                case ExceptionMessageId.TypedName_ImplicitConversion:
                    return string.Format(Resources.AE_TypedName_ImplicitConversion, replacements);
                case ExceptionMessageId.TypedName_Invalid:
                    return string.Format(Resources.AE_TypedName_Invalid, replacements);
                case ExceptionMessageId.TypedName_InvalidElement:
                    return string.Format(Resources.AE_TypedName_InvalidElement, replacements);
                case ExceptionMessageId.TypedName_ReferenceKind:
                    return string.Format(Resources.AE_TypedName_ReferenceKind, replacements);
                case ExceptionMessageId.TypeInvalidElementType:
                    return string.Format(Resources.AE_TypeInvalidElementType, replacements);
                case ExceptionMessageId.TypeMustBeCompilerGenerated:
                    return string.Format(Resources.AE_TypeMustBeCompilerGenerated, replacements);
                case ExceptionMessageId.TypeMustNotBeAReferenceType:
                    return string.Format(Resources.AE_TypeMustNotBeAReferenceType, replacements);
                case ExceptionMessageId.TypeMustBeGenericParameter:
                    return string.Format(Resources.AE_TypeMustBeGenericParameter, replacements);
                case ExceptionMessageId.TypeMustBeStaticClass :
                    return string.Format(Resources.AE_TypeMustBeStaticClass, replacements);
                case ExceptionMessageId.TypeMustBeGenericChild:
                    return string.Format(Resources.AE_TypeMustBeGenericChild, replacements);
                case ExceptionMessageId.TypeParameterInfoError:
                    return string.Format(Resources.AE_TypeParameterInfoError, replacements);
                case ExceptionMessageId.TypeNotGeneric:
                    return string.Format(Resources.AE_TypeNotGeneric, replacements);
                case ExceptionMessageId.TypeNotGivenKind:
                    return string.Format(Resources.AE_TypeNotGivenKind, replacements);
                case ExceptionMessageId.TypeRelationalCheckRequiresType:
                    return string.Format(Resources.AE_TypeRelationalCheckRequiresType, replacements);
                case ExceptionMessageId.TypeRelationalTypeCannotBeNull:
                    return string.Format(Resources.AE_TypeRelationalTypeCannotBeNull, replacements);
                case ExceptionMessageId.TypeRelationalOrNullCastMustBeReference:
                    return string.Format(Resources.AE_TypeRelationalOrNullCastMustBeReference, replacements);
                case ExceptionMessageId.TypesAssemblyIsFixed:
                    return string.Format(Resources.AE_TypesAssemblyIsFixed, replacements);
                case ExceptionMessageId.UnknownArgument:
                    return string.Format(Resources.AE_UnknownArgument, replacements);
                case ExceptionMessageId.UnknownArgumentMessage:
                    return string.Format(Resources.AE_UnknownArgumentMessage, replacements);
                case ExceptionMessageId.UnknownArgumentWord:
                    return string.Format(Resources.AE_UnknownArgumentWord, replacements);
                case ExceptionMessageId.ValueIsWrongType:
                    return string.Format(Resources.AE_ValueIsWrongType, replacements);
                default:
                    throw ObtainArgumentException(ArgumentWithException.message, ExceptionMessageId.UnknownArgumentMessage);
            }
        }

        /// <summary>
        /// Obtains the <see cref="String"/> name associated to the 
        /// <paramref name="argument"/> provided.
        /// </summary>
        /// <param name="argument">The <see cref="ArgumentWithException"/> enumeration value
        /// which determines which name to retrieve.</param>
        /// <returns>A <see cref="String"/> associated to the 
        /// name of the <paramref name="argument"/> provided.</returns>
        internal static string GetArgumentName(ArgumentWithException argument)
        {
            switch (argument)
            {
                case ArgumentWithException.argument:
                    return "argument";
                case ArgumentWithException.array:
                    return "array";
                case ArgumentWithException.collections:
                    return "collections";
                case ArgumentWithException.ctor:
                    return "ctor";
                case ArgumentWithException.data:
                    return "data";
                case  ArgumentWithException.decl:
                    return "decl";
                case  ArgumentWithException.deviant:
                    return "deviant";
                case ArgumentWithException.direction:
                    return "direction";
                case ArgumentWithException.e:
                    return "e";
                case ArgumentWithException.elementType:
                    return "elementType";
                case ArgumentWithException.genericLocalType:
                    return "genericLocalType";
                case ArgumentWithException.genericParameters:
                    return "genericParameters";
                case ArgumentWithException.genericType:
                    return "genericType";
                case ArgumentWithException.globalMemberType:
                    return "globalMemberType";
                case ArgumentWithException.identifier:
                    return "identifier";
                case ArgumentWithException.index:
                    return "index";
                case ArgumentWithException.item:
                    return "item";
                case ArgumentWithException.items:
                    return "items";
                case ArgumentWithException.key:
                    return "key";
                case ArgumentWithException.leftSide:
                    return "leftSide";
                case ArgumentWithException.length:
                    return "length";
                case ArgumentWithException.message:
                    return "message";
                case ArgumentWithException.methodReplacements:
                    return "methodReplacements";
                case ArgumentWithException.name:
                    return "name";
                case ArgumentWithException.nameAndDelegateType:
                    return "nameAndDelegateType";
                case ArgumentWithException.nameAndType:
                    return "nameAndType";
                case ArgumentWithException.@namespace:
                    return "namespace";
                case ArgumentWithException.operation:
                    return "operation";
                case ArgumentWithException.owner:
                    return "owner";
                case ArgumentWithException.parameter:
                    return "parameter";
                case ArgumentWithException.part:
                    return "part";
                case ArgumentWithException.path:
                    return "path";
                case ArgumentWithException.rank:
                    return "rank";
                case ArgumentWithException.requirement:
                    return "requirement";
                case ArgumentWithException.rightSide:
                    return "rightSide";
                case ArgumentWithException.searchCriteria:
                    return "searchCriteria";
                case ArgumentWithException.series:
                    return "series";
                case ArgumentWithException.signature:
                    return "signature";
                case ArgumentWithException.sizes:
                    return "sizes";
                case ArgumentWithException.source:
                    return "source";
                case ArgumentWithException.sourceElement:
                    return "sourceElement";
                case ArgumentWithException.subordinate:
                    return "subordinate";
                case ArgumentWithException.target:
                    return "target";
                case ArgumentWithException.TAssembly:
                    return "TAssembly";
                case ArgumentWithException.type:
                    return "type";
                case ArgumentWithException.typedName:
                    return "typedName";
                case ArgumentWithException.typeParameters:
                    return "typeParameters";
                case ArgumentWithException.typeReplacements:
                    return "typeReplacements";
                case ArgumentWithException.typeSymbol:
                    return "typeSymbol";
                case ArgumentWithException.underlyingSystemType:
                    return "underlyingSystemType";
                case ArgumentWithException.uniqueId:
                    return "uniqueId";
                case ArgumentWithException.value:
                    return "value";
                case ArgumentWithException.word:
                    return "word";
                default:
                    throw ObtainArgumentException(ArgumentWithException.argument, ExceptionMessageId.UnknownArgument);
            }
        }
    }
}
