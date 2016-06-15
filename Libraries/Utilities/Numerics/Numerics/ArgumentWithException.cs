using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Numerics
{
    /// <summary>
    /// The argument which caused the exception.
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
        /// Parameter 'structuralTypeInfo'.
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
        /// Parameter 'leftSide'.
        /// </summary>
        leftSide,
        /// <summary>
        /// Parameter 'length'.
        /// </summary>
        length,
        /// <summary>
        /// Parameter 'member'.
        /// </summary>
        member,
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
        /// Parameter 'operation'.
        /// </summary>
        operation,
        /// <summary>
        /// Parameter 'owner'.
        /// </summary>
        owner,
        /// <summary>
        /// Parameter 'parameter'.
        /// </summary>
        parameter,
        /// <summary>
        /// Parameter 'part'.
        /// </summary>
        part,
        /// <summary>
        /// Parameter 'rank'.
        /// </summary>
        rank,
        /// <summary>
        /// Parameter 'requirement'.
        /// </summary>
        requirement,
        /// <summary>
        /// Parameter 'rightSide'.
        /// </summary>
        rightSide,
        /// <summary>
        /// Parameter 'searchCriteria'.
        /// </summary>
        searchCriteria,
        /// <summary>
        /// Parameter 'series'.
        /// </summary>
        series,
        /// <summary>
        /// Parameter 'service'.
        /// </summary>
        service,
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
        /// Parameter 'uniqueId'.
        /// </summary>
        uniqueId,
        /// <summary>
        /// Parameter 'value'.
        /// </summary>
        value,
        /// <summary>
        /// Parameter 'word'.
        /// </summary>
        word,
        /// <summary>
        /// Parameter 'path'.
        /// </summary>
        path,
    }

    /// <summary>
    /// The words used by argument exceptions.
    /// </summary>
    internal enum ExceptionWordId
    {
        /// <summary>
        /// Word 'array'.
        /// </summary>
        array,
        /// <summary>
        /// Word 'assembly'.
        /// </summary>
        assembly,
        /// <summary>
        /// Words 'by reference type'.
        /// </summary>
        by_reference_type,
        /// <summary>
        /// Word 'class'.
        /// </summary>
        @class,
        /// <summary>
        /// Words 'C&#9839; bitwise and'.
        /// </summary>
        cs_bitwise_and,
        /// <summary>
        /// Words 'C&#9839; bitwise or'.
        /// </summary>
        cs_bitwise_or,
        /// <summary>
        /// Words 'C&#9839; bitwise exclusive or'.
        /// </summary>
        cs_bitwise_xor,
        /// <summary>
        /// Words 'C&#9839; conditional'.
        /// </summary>
        cs_conditional,
        /// <summary>
        /// Words 'C&#9839; logical or'.
        /// </summary>
        cs_logical_and,
        /// <summary>
        /// Words 'C&#9839; logical or'.
        /// </summary>
        cs_logical_or,
        /// <summary>
        /// Word 'delegate'.
        /// </summary>
        @delegate,
        /// <summary>
        /// Word 'enum'.
        /// </summary>
        @enum,
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
        /// Word 'interface'.
        /// </summary>
        @interface,
        /// <summary>
        /// Word 'namespace'.
        /// </summary>
        @namespace,
        /// <summary>
        /// Word 'nullable'.
        /// </summary>
        nullable,
        /// <summary>
        /// Word 'pointer'.
        /// </summary>
        pointer,
        /// <summary>
        /// Word 'Source'.
        /// </summary>
        Source,
        /// <summary>
        /// Word 'struct'.
        /// </summary>
        @struct,
        /// <summary>
        /// Word 'to'.
        /// </summary>
        to,
    }
    /// <summary>
    /// The <see cref="ArgumentException"/> message.
    /// </summary>
    internal enum ExceptionMessageId
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
        /// <summary>
        /// Target manifest module must belong to the current assembly.
        /// </summary>
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
        /// <summary>
        /// Term is used when {0} is null.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        OnlyNullableOnTerm,
        /// <summary>
        /// Operand relative to associativity is 'null'; thus, operation
        /// of 'Term' is required in this context.
        /// </summary>
        OperationMustBeTerm,
        ParentMustBeEqual,
        /// <summary>
        /// Root {0} instance of the series is implicitly a part of the series, adding it to the {0} part collection is unnecessary.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        Part_CannotBeRoot,
        /// <summary>
        /// Adding a part to the series of a(n) {0} requires that the new {0} part reference the root {0} of the series.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        Part_MustReferenceRoot,
        /// <summary>
        /// The {0} part provided is the root of another {0} and cannot be added to this sequence.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        Part_RootOfASeparateSeries,
        PathCannotBeDotsOnly,
        /// <summary>
        /// Cannot have primitive values of any other type than listed in the PrimitiveType enum.
        /// </summary>
        Primitive_Invalid,
        /// <summary>
        /// Cannot have object primitives of anything other than string.
        /// </summary>
        Primitive_NonStringObject,
        /// <summary>
        /// The provided expression '{0}' cannot be affixed into a(n)
        /// {1} expression.
        /// </summary>
        ProvidedExpressionCannotBe,
        /// <summary>
        /// The rank '{0}' provided is invalid, it must be one (1) or greater.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        RankMustBeOneOrGreater,
        /// <summary>
        /// Cannot relationally check a type against another expression.
        /// </summary>
        RelationalInvalidOnExpression,
        /// <summary>
        /// Cannot remove value in parameter '{0}', member not found.
        /// </summary>
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
        /// <summary>
        /// The provided type '{0}' is already a generic closure, retrieve the generic definition prior to making a new closure.
        /// </summary>
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
        /// Type not generic.
        /// </summary>
        TypeNotGeneric,
        /// <summary>
        /// The compiled type '{0}' cannot be wrapped into a '{1}'.
        /// </summary>
        /// <remarks>Requires two (2) replacements.</remarks>
        TypeNotGivenKind,
        /// <summary>
        /// Type-relational checks require a type on the {0}.
        /// </summary>
        /// <remarks>Requires one (1) replacement.</remarks>
        TypeRelationalCheckRequiresType,
        /// <summary>
        /// Type-relational check invalid.  Target type on the type reference expression cannot be null.
        /// </summary>
        TypeRelationalTypeCannotBeNull,
        /// <summary>
        /// Type-Cast (or null) expression requires a reference type to work.
        /// </summary>
        TypeRelationalOrNullCastMustBeReference,
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
