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

        public static ArgumentOutOfRangeException ObtainArgumentOutOfRangeException(ArgumentWithException argument)
        {
            return new ArgumentOutOfRangeException(GetArgumentName(argument));
        }

        public static ArgumentOutOfRangeException ObtainArgumentOutOfRangeException(ArgumentWithException argument, ArgumentExceptionMessage message)
        {
            return ObtainArgumentOutOfRangeException(argument, message, emptyReplacements);
        }
        public static ArgumentOutOfRangeException ObtainArgumentOutOfRangeException(ArgumentWithException argument, ArgumentExceptionMessage message, params string[] replacements)
        {
            return new ArgumentOutOfRangeException(GetArgumentName(argument), GetArgumentMessage(message, replacements ?? emptyReplacements));
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ArgumentExceptionMessage message)
        {
            return ObtainArgumentException(argument, message, emptyReplacements);
        }
        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ArgumentExceptionMessage message, string replacement)
        {
            return ObtainArgumentException(argument, message, new[] { replacement });
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ArgumentExceptionMessage message, string replacement1, string replacement2)
        {
            return ObtainArgumentException(argument, message, new[] { replacement1, replacement2 });
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ArgumentExceptionMessage message, string replacement1, string replacement2, string replacement3)
        {
            return ObtainArgumentException(argument, message, new[] { replacement1, replacement2, replacement3 });
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ArgumentExceptionMessage message, params string[] replacements)
        {
            return new ArgumentException(GetArgumentMessage(message, replacements ?? emptyReplacements), GetArgumentName(argument));
        }

        public static string GetArgumentExceptionWord(ArgumentExceptionWord word)
        {
            switch (word)
            {
                case ArgumentExceptionWord.array:
                    return "array";
                case ArgumentExceptionWord.by_reference_type:
                    return "by reference type";
                case ArgumentExceptionWord.@explicit:
                    return "explicit";
                case ArgumentExceptionWord.from:
                    return "from";
                case ArgumentExceptionWord.@implicit:
                    return "implicit";
                case ArgumentExceptionWord.to:
                    return "to";
                default:
                    throw ObtainArgumentOutOfRangeException(ArgumentWithException.word, ArgumentExceptionMessage.UnknownArgumentWord);
            }
        }

        private static string GetArgumentMessage(ArgumentExceptionMessage message, params string[] replacements)
        {
            switch (message)
            {
                case ArgumentExceptionMessage.ArgumentCannotBeEmpty:
                    return string.Format(Resources.AE_ArgumentCannotBeEmpty, replacements);
                case ArgumentExceptionMessage.CannotTermBinaryOperation:
                    return string.Format(Resources.AE_CannotTermBinaryOperation, replacements);
                case ArgumentExceptionMessage.CoercionDoesNotExist:
                    return string.Format(Resources.AE_CoercionDoesNotExist, replacements);
                case ArgumentExceptionMessage.CompiledType_NotProperKind:
                    return string.Format(Resources.AE_CompiledType_NotProperKind, replacements);
                case ArgumentExceptionMessage.ConditionMustBeBreakable:
                    return string.Format(Resources.AE_ConditionMustBeBreakable, replacements);
                case ArgumentExceptionMessage.DataCannotBeEmpty:
                    return string.Format(Resources.AE_DataCannotBeEmpty, replacements);
                case ArgumentExceptionMessage.DelegateTypeParameterMismatch:
                    return string.Format(Resources.AE_DelegateTypeParameterMismatch, replacements);
                case ArgumentExceptionMessage.DetachedExpressionNotValidFor:
                    return string.Format(Resources.AE_DetachedExpressionNotValidFor, replacements);
                case ArgumentExceptionMessage.DuplicateKeyExists:
                    return string.Format(Resources.AE_DuplicateKeyExists, replacements);
                case ArgumentExceptionMessage.ElementTypeMustBeGivenKind:
                    return string.Format(Resources.AE_ElementTypeMustBeGivenKind, replacements);
                case ArgumentExceptionMessage.GenericClosureReplacementCount:
                    return string.Format(Resources.AE_GenericClosureReplacementCount, replacements);
                case ArgumentExceptionMessage.GenericParameterRequiresName:
                    return string.Format(Resources.AE_GenericParameterRequiresName, replacements);
                case ArgumentExceptionMessage.InsufficientSpaceForCopy:
                    return string.Format(Resources.AE_InsufficientSpaceForCopy, replacements);
                case ArgumentExceptionMessage.InterfaceNotImplemented:
                    return string.Format(Resources.AE_InterfaceNotImplemented, replacements);
                case ArgumentExceptionMessage.InvalidTCheckDerivation:
                    return string.Format(Resources.AE_InvalidTCheckDerivation, replacements);
                case ArgumentExceptionMessage.JumpTargetMustBeLabel:
                    return string.Format(Resources.AE_JumpTargetMustBeLabel, replacements);
                case ArgumentExceptionMessage.ManifestModuleTarget:
                    return string.Format(Resources.AE_ManifestModuleTarget, replacements);
                case ArgumentExceptionMessage.MemberOfSeriesNull:
                    return string.Format(Resources.AE_MemberOfSeriesNull, replacements);
                case ArgumentExceptionMessage.NamedGenericParameterExists:
                    return string.Format(Resources.AE_NamedGenericParameterExists, replacements);
                case ArgumentExceptionMessage.NamespacePathAlreadyPresent:
                    return string.Format(Resources.AE_NamespacePathAlreadyPresent, replacements);
                case ArgumentExceptionMessage.NonNullableTypeProvided:
                    return string.Format(Resources.AE_NonNullableTypeProvided, replacements);
                case ArgumentExceptionMessage.NumericControllerMismatch:
                    return string.Format(Resources.AE_NumericControllerMismatch, replacements);
                case ArgumentExceptionMessage.NumericValueParseError:
                    return string.Format(Resources.AE_NumericValueParseError, replacements);
                case ArgumentExceptionMessage.OnlyNullableOnTerm:
                    return string.Format(Resources.AE_OnlyNullableOnTerm, replacements);
                case ArgumentExceptionMessage.OperationMustBeTerm:
                    return string.Format(Resources.AE_OperationMustBeTerm, replacements);
                case ArgumentExceptionMessage.ParentMustBeEqual:
                    return string.Format(Resources.AE_ParentMustBeEqual, replacements);
                case ArgumentExceptionMessage.Part_CannotBeRoot:
                    return string.Format(Resources.AE_Part_CannotBeRoot, replacements);
                case ArgumentExceptionMessage.Part_MustReferenceRoot:
                    return string.Format(Resources.AE_Part_MustReferenceRoot, replacements);
                case ArgumentExceptionMessage.PathCannotBeDotsOnly:
                    return string.Format(Resources.AE_PathCannotBeDotsOnly, replacements);
                case ArgumentExceptionMessage.Primitive_Invalid:
                    return string.Format(Resources.AE_Primitive_Invalid, replacements);
                case ArgumentExceptionMessage.Primitive_NonStringObject:
                    return string.Format(Resources.AE_Primitive_NonStringObject, replacements);
                case ArgumentExceptionMessage.ProvidedExpressionCannotBe:
                    return string.Format(Resources.AE_ProvidedExpressionCannotBe, replacements);
                case ArgumentExceptionMessage.RankMustBeOneOrGreater:
                    return string.Format(Resources.AE_RankMustBeOneOrGreater, replacements);
                case ArgumentExceptionMessage.RelationalInvalidOnExpression:
                    return string.Format(Resources.AE_RelationalInvalidOnExpression, replacements);
                case ArgumentExceptionMessage.Remove_ValueNotFound:
                    return string.Format(Resources.AE_Remove_ValueNotFound, replacements);
                case ArgumentExceptionMessage.RemoveFailed_CustomAttributeNotFound:
                    return string.Format(Resources.AE_RemoveFailed_CustomAttributeNotFound, replacements);
                case ArgumentExceptionMessage.SourceStringInvalid:
                    return string.Format(Resources.AE_SourceStringInvalid, replacements);
                case ArgumentExceptionMessage.SubordinateCannotChange:
                    return string.Format(Resources.AE_SubordinateCannotChange, replacements);
                case ArgumentExceptionMessage.SubordinateDoesNotExist:
                    return string.Format(Resources.AE_SubordinateDoesNotExist, replacements);
                case ArgumentExceptionMessage.SubordinateInvalid:
                    return string.Format(Resources.AE_SubordinateInvalid, replacements);
                case ArgumentExceptionMessage.SubordinateMismatch:
                    return string.Format(Resources.AE_SubordinateMismatch, replacements);
                case ArgumentExceptionMessage.SubordinateNull:
                    return string.Format(Resources.AE_SubordinateNull, replacements);
                case ArgumentExceptionMessage.SubordinateSubTypeDuplicate:
                    return string.Format(Resources.AE_SubordinateSubTypeDuplicate, replacements);
                case ArgumentExceptionMessage.SourceStreamInvalidLength:
                    return string.Format(Resources.AE_SourceStreamInvalidLength, replacements);
                case ArgumentExceptionMessage.TransitionKeyCollision:
                    return string.Format(Resources.AE_TransitionKeyCollision, replacements);
                case ArgumentExceptionMessage.TypeAlreadyGenericClosure:
                    return string.Format(Resources.AE_TypeAlreadyGenericClosure, replacements);
                case ArgumentExceptionMessage.TypedName_ImplicitConversion:
                    return string.Format(Resources.AE_TypedName_ImplicitConversion, replacements);
                case ArgumentExceptionMessage.TypedName_Invalid:
                    return string.Format(Resources.AE_TypedName_Invalid, replacements);
                case ArgumentExceptionMessage.TypedName_InvalidElement:
                    return string.Format(Resources.AE_TypedName_InvalidElement, replacements);
                case ArgumentExceptionMessage.TypedName_ReferenceKind:
                    return string.Format(Resources.AE_TypedName_ReferenceKind, replacements);
                case ArgumentExceptionMessage.TypeInvalidElementType:
                    return string.Format(Resources.AE_TypeInvalidElementType, replacements);
                case ArgumentExceptionMessage.TypeMustBeCompilerGenerated:
                    return string.Format(Resources.AE_TypeMustBeCompilerGenerated, replacements);
                case ArgumentExceptionMessage.TypeMustNotBeAReferenceType:
                    return string.Format(Resources.AE_TypeMustNotBeAReferenceType, replacements);
                case ArgumentExceptionMessage.TypeMustBeGenericParameter:
                    return string.Format(Resources.AE_TypeMustBeGenericParameter, replacements);
                case ArgumentExceptionMessage.TypeMustBeStaticClass :
                    return string.Format(Resources.AE_TypeMustBeStaticClass, replacements);
                case ArgumentExceptionMessage.TypeMustBeGenericChild:
                    return string.Format(Resources.AE_TypeMustBeGenericChild, replacements);
                case ArgumentExceptionMessage.TypeParameterInfoError:
                    return string.Format(Resources.AE_TypeParameterInfoError, replacements);
                case ArgumentExceptionMessage.TypeNotGeneric:
                    return string.Format(Resources.AE_TypeNotGeneric, replacements);
                case ArgumentExceptionMessage.TypeNotGivenKind:
                    return string.Format(Resources.AE_TypeNotGivenKind, replacements);
                case ArgumentExceptionMessage.TypesAssemblyIsFixed:
                    return string.Format(Resources.AE_TypesAssemblyIsFixed, replacements);
                case ArgumentExceptionMessage.UnknownArgument:
                    return string.Format(Resources.AE_UnknownArgument, replacements);
                case ArgumentExceptionMessage.UnknownArgumentMessage:
                    return string.Format(Resources.AE_UnknownArgumentMessage, replacements);
                case ArgumentExceptionMessage.UnknownArgumentWord:
                    return string.Format(Resources.AE_UnknownArgumentWord, replacements);
                case ArgumentExceptionMessage.ValueIsWrongType:
                    return string.Format(Resources.AE_ValueIsWrongType, replacements);
                default:
                    throw ObtainArgumentException(ArgumentWithException.message, ArgumentExceptionMessage.UnknownArgumentMessage);
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
                case ArgumentWithException.owner:
                    return "owner";
                case ArgumentWithException.parameter:
                    return "parameter";
                case ArgumentWithException.rank:
                    return "rank";
                case ArgumentWithException.requirement:
                    return "requirement";
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
                case ArgumentWithException.value:
                    return "value";
                case ArgumentWithException.word:
                    return "word";
                default:
                    throw ObtainArgumentException(ArgumentWithException.argument, ArgumentExceptionMessage.UnknownArgument);
            }
        }
    }
}
