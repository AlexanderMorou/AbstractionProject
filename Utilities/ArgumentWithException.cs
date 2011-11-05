using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction
{
    /// <summary>
    /// The argument which caused the <see cref="ArgumentException"/>
    /// or <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    internal enum ArgumentWithException
    {
        /// <summary>
        /// Parameter 'argument'.
        /// </summary>
        argument,
        /// <summary>
        /// Parameter 'array'.
        /// </summary>
        array,
        /// <summary>
        /// Parameter 'collections'.
        /// </summary>
        collections,
        /// <summary>
        /// Parameter 'ctor'.
        /// </summary>
        ctor,
        /// <summary>
        /// Parameter 'data'.
        /// </summary>
        data,
        /// <summary>
        /// Parameter 'decl'.
        /// </summary>
        decl,
        /// <summary>
        /// Parameter 'deviant'.
        /// </summary>
        deviant,
        /// <summary>
        /// Parameter 'direction'.
        /// </summary>
        direction,
        /// <summary>
        /// Parameter 'e'.
        /// </summary>
        e,
        /// <summary>
        /// Parameter 'elementType'.
        /// </summary>
        elementType,
        /// <summary>
        /// Parameter 'genericLocalType'.
        /// </summary>
        genericLocalType,
        /// <summary>
        /// Parameter 'genericParameters'.
        /// </summary>
        genericParameters,
        /// <summary>
        /// Parameter 'genericType'.
        /// </summary>
        genericType,
        /// <summary>
        /// Parameter 'globalMemberType'.
        /// </summary>
        globalMemberType,
        /// <summary>
        /// Parameter 'identifier'.
        /// </summary>
        identifier,
        /// <summary>
        /// Parameter 'index'.
        /// </summary>
        index,
        /// <summary>
        /// Parameter 'item'.
        /// </summary>
        item,
        /// <summary>
        /// Parameter 'items'.
        /// </summary>
        items,
        /// <summary>
        /// Parameter 'key'.
        /// </summary>
        key,
        /// <summary>
        /// Parameter 'length'.
        /// </summary>
        length,
        /// <summary>
        /// Parameter 'message'.
        /// </summary>
        message,
        /// <summary>
        /// Parameter 'methodReplacements'.
        /// </summary>
        methodReplacements,
        /// <summary>
        /// Parameter 'name'.
        /// </summary>
        name,
        /// <summary>
        /// Parameter 'nameAndDelegateType'.
        /// </summary>
        nameAndDelegateType,
        /// <summary>
        /// Parameter 'nameAndType'.
        /// </summary>
        nameAndType,
        /// <summary>
        /// Parameter 'namespace'.
        /// </summary>
        @namespace,
        /// <summary>
        /// Parameter 'owner'.
        /// </summary>
        owner,
        /// <summary>
        /// Parameter 'parameter'.
        /// </summary>
        parameter,
        /// <summary>
        /// Parameter 'rank'.
        /// </summary>
        rank,
        /// <summary>
        /// Parameter 'requirement'.
        /// </summary>
        requirement,
        /// <summary>
        /// Parameter 'searchCriteria'.
        /// </summary>
        searchCriteria,
        /// <summary>
        /// Parameter 'series'.
        /// </summary>
        series,
        /// <summary>
        /// Parameter 'signature'.
        /// </summary>
        signature,
        /// <summary>
        /// Parameter 'sizes'.
        /// </summary>
        sizes,
        /// <summary>
        /// Parameter 'source'.
        /// </summary>
        source,
        /// <summary>
        /// Parameter 'sourceElement'.
        /// </summary>
        sourceElement,
        /// <summary>
        /// Parameter 'subordinate'.
        /// </summary>
        subordinate,
        /// <summary>
        /// Parameter 'target'.
        /// </summary>
        target,
        /// <summary>
        /// Type-parameter 'TAssembly'.
        /// </summary>
        TAssembly,
        /// <summary>
        /// Parameter 'type'.
        /// </summary>
        type,
        /// <summary>
        /// Parameter 'typedName'.
        /// </summary>
        typedName,
        /// <summary>
        /// Parameter 'typeParameters'.
        /// </summary>
        typeParameters,
        /// <summary>
        /// Parameter 'typeReplacements'.
        /// </summary>
        typeReplacements,
        /// <summary>
        /// Parameter 'typeSymbol'.
        /// </summary>
        typeSymbol,
        /// <summary>
        /// Parameter 'underlyingSystemType'.
        /// </summary>
        underlyingSystemType,
        /// <summary>
        /// Parameter 'value'.
        /// </summary>
        value,
        /// <summary>
        /// Parameter 'word'.
        /// </summary>
        word,
    }

    /// <summary>
    /// The words used by argument exceptions.
    /// </summary>
    internal enum ArgumentExceptionWord
    {
        /// <summary>
        /// Word 'array'.
        /// </summary>
        array,
        /// <summary>
        /// Words 'by reference type'.
        /// </summary>
        by_reference_type,
        /// <summary>
        /// Word 'explicit'.
        /// </summary>
        @explicit,
        /// <summary>
        /// Word 'from'.
        /// </summary>
        from,
        /// <summary>
        /// Word 'implicit'.
        /// </summary>
        @implicit,
        /// <summary>
        /// Word 'to'.
        /// </summary>
        to,
    }
    /// <summary>
    /// The <see cref="ArgumentException"/> message.
    /// </summary>
    internal enum ArgumentExceptionMessage
    {
        /// <summary>
        /// The argument '{0}' can't be <see cref="String.Empty"/>.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        ArgumentCannotBeEmpty,
        /// <summary>
        /// Both operands of the binary operation are present; thus, an
        /// operation of 'Term' is invalid in this context.
        /// </summary>
        CannotTermBinaryOperation,
        /// <summary>
        /// No {0} coercion {1} target type.
        /// </summary>
        /// <remarks>Requires two (2) replacements.</remarks>
        CoercionDoesNotExist,
        /// <summary>
        /// Type is not a(n) {0}.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        CompiledType_NotProperKind,
        ConditionMustBeBreakable,
        DataCannotBeEmpty,
        /// <summary>
        /// The provided constructor contains a different number of
        /// parameters than the Delegate type.
        /// </summary>
        DelegateTypeParameterMismatch,
        DetachedExpressionNotValidFor,
        /// <summary>
        /// An item with the key provided already exists.
        /// </summary>
        DuplicateKeyExists,
        /// <summary>
        /// The element type on '{0}' must be '{1}'.
        /// </summary>
        /// <remarks>Requires two (2) replacements.</remarks>
        ElementTypeMustBeGivenKind,
        /// <summary>
        /// There are '{0}' parameters within the generic closure, but
        /// {1} were provided.
        /// </summary>
        /// <remarks>Requires two (2) replacements.</remarks>
        GenericClosureReplacementCount,
        GenericParameterRequiresName,
        /// <summary>
        /// {0} not large enough to hold elements.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        InsufficientSpaceForCopy,
        InvalidTCheckDerivation,
        JumpTargetMustBeLabel,
        ManifestModuleTarget,
        /// <summary>
        /// A member of {0} was null.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        MemberOfSeriesNull,
        NamedGenericParameterExists,
        NamespacePathAlreadyPresent,
        /// <summary>
        /// Nullable types must be normal types, array, by-reference,
        /// nullable, and pointer types are not allowed.
        /// </summary>
        NonNullableTypeProvided,
        NumericControllerMismatch,
        /// <summary>
        /// The character '{0}' was not found within the base entities
        /// for the current numeric controller.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        NumericValueParseError,
        OnlyNullableOnTerm,
        /// <summary>
        /// Operand relative to associativity is 'null'; thus, operation
        /// of 'Term' is required in this context.
        /// </summary>
        OperationMustBeTerm,
        ParentMustBeEqual,
        Part_CannotBeRoot,
        Part_MustReferenceRoot,
        PathCannotBeDotsOnly,
        Primitive_Invalid,
        Primitive_NonStringObject,
        ProvidedExpressionCannotBe,
        /// <summary>
        /// The rank '{0}' provided is invalid, it must be one (1) or greater.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        RankMustBeOneOrGreater,
        RelationalInvalidOnExpression,
        Remove_ValueNotFound,
        RemoveFailed_CustomAttributeNotFound,
        /// <summary>
        /// Data length inappropriate for conversion into a unicode string.
        /// </summary>
        SourceStreamInvalidLength,
        /// <summary>
        /// String {0} doesn't match the lengths provided.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        SourceStringInvalid,
        /// <summary>
        /// The provided subordinate does not match the subordinate
        /// contained; thus, the change action cannot proceed.
        /// </summary>
        SubordinateCannotChange,
        /// <summary>
        /// The provided subordinate does not exist within the current
        /// context.
        /// </summary>
        SubordinateDoesNotExist,
        /// <summary>
        /// The provided subordinate dictionary is not valid within 
        /// current context.
        /// </summary>
        SubordinateInvalid,
        /// <summary>
        /// The provided subordinate does not match the subordinate contained.
        /// </summary>
        SubordinateMismatch,
        /// <summary>
        /// The subordinate associated to the value provided is null,
        /// and thus cannot be entered.
        /// </summary>
        SubordinateNull,
        /// <summary>
        /// The subordinate sub-type already exists within the current
        /// context.
        /// </summary>
        SubordinateSubTypeDuplicate,
        /// <summary>
        /// The provided transitionary condition collides with an existing
        /// condition.  On a deterministic system this is invalid.
        /// </summary>
        TransitionKeyCollision,
        TypeAlreadyGenericClosure,
        TypedName_ImplicitConversion,
        /// <summary>
        /// The reference source of the TypedName within {0} is invalid.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        TypedName_Invalid,
        /// <summary>
        /// Invalid reference in a member of {0}.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        TypedName_InvalidElement,
        /// <summary>
        /// Invalid reference, {0} must be a {1}.
        /// </summary>
        /// <remarks>Requires two (2) replacements.</remarks>
        TypedName_ReferenceKind,
        /// <summary>
        /// Type is a {0} which is not valid on a(n) {1} type.
        /// </summary>
        TypeInvalidElementType,
        /// <summary>
        /// The type provided is a reference type, only value types
        /// (structs) or generic parameters, with the value type 
        /// constraint (struct), are allowed.
        /// </summary>
        TypeMustNotBeAReferenceType,
        /// <summary>
        /// Invalid target container type, must be generated by a compiler.
        /// </summary>
        TypeMustBeCompilerGenerated,
        /// <summary>
        /// Provided type must be the child of a generic.
        /// </summary>
        TypeMustBeGenericChild,
        /// <summary>
        /// Provided type must be a generic parameter.
        /// </summary>
        TypeMustBeGenericParameter,
        /// <summary>
        /// Invalid target container type, must be a static (abstract
        /// sealed) class.
        /// </summary>
        TypeMustBeStaticClass,
        /// <summary>
        /// Type not generic.
        /// </summary>
        TypeNotGeneric,
        /// <summary>
        /// The compiled type '{0}' cannot be wrapped into a '{1}'.
        /// </summary>
        /// <remarks>Requires two (2) replacements.</remarks>
        TypeNotGivenKind,
        /// <summary>
        /// Type provided represents an informational type, and must
        /// contain only the generic arguments of declaring generic
        /// type.
        /// </summary>
        TypeParameterInfoError,
        TypesAssemblyIsFixed,
        /// <summary>
        /// Unknown argument identifier provided.
        /// </summary>
        UnknownArgument,
        /// <summary>
        /// Unknown argument message identifier provided.
        /// </summary>
        UnknownArgumentMessage,
        /// <summary>
        /// Unknown argument word provided.
        /// </summary>
        UnknownArgumentWord,
        /// <summary>
        /// {0}, of type '{1}', is of the wrong type; expected: '{2}'.
        /// </summary>
        /// <remarks>Requires three (3) replacements.</remarks>
        ValueIsWrongType,
        /// <summary>
        /// '{0}' does not implement '{1}'
        /// </summary>
        /// <remarks>Requires two (2) replacements.</remarks>
        InterfaceNotImplemented,
    }
}
