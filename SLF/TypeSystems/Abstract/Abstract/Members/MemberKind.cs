using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// The kind of member the item is.
    /// </summary>
    public enum MemberKind :
        byte
    {
        /// <summary>
        /// The member is a method.
        /// </summary>
        Method,
        /// <summary>
        /// The member is a method parameter.
        /// </summary>
        MethodParameter,
        /// <summary>
        /// The member is a method's type-parameter.
        /// </summary>
        MethodTypeParameter,
        /// <summary>
        /// The member is a methods type-parameter's constructor.
        /// </summary>
        MethodTypeParameterConstructor,
        /// <summary>
        /// The member is a methods type parameters constructor's parameter.
        /// </summary>
        MethodTypeParameterConstructorParameter,
        /// <summary>
        /// The member is a method signature.
        /// </summary>
        MethodSignature,
        /// <summary>
        /// The member is a method signature parameter.
        /// </summary>
        MethodSignatureParameter,
        /// <summary>
        /// The member is a method signature type parameter.
        /// </summary>
        MethodSignatureTypeParameter,
        /// <summary>
        /// The member is a method signature type parameter constructor.
        /// </summary>
        MethodSignatureTypeParameterConstructor,
        /// <summary>
        /// The member is a method signature type parameter constructor parameter.
        /// </summary>
        MethodSignatureTypeParameterConstructorParameter,
        /// <summary>
        /// The member is an instance field.
        /// </summary>
        Field,
        /// <summary>
        /// The member is a field of an enumerator.
        /// </summary>
        EnumField,
        /// <summary>
        /// The member is a field of a module.
        /// </summary>
        ModuleField,
        /// <summary>
        /// The member is an event.
        /// </summary>
        Event,
        /// <summary>
        /// The member is an event parameter.
        /// </summary>
        EventParameter,
        /// <summary>
        /// The member is an event signature.
        /// </summary>
        EventSignature,
        /// <summary>
        /// The member is an event signature parameter.
        /// </summary>
        EventSignatureParameter,
        /// <summary>
        /// The member is an indexer.
        /// </summary>
        Indexer,
        /// <summary>
        /// The member is an indexer parameter.
        /// </summary>
        IndexerParameter,
        /// <summary>
        /// The member is an indexer signature.
        /// </summary>
        IndexerSignature,
        /// <summary>
        /// The member is an indexer signature parameter.
        /// </summary>
        IndexerSignatureParameter,
        /// <summary>
        /// The member is a property.
        /// </summary>
        Property,
        /// <summary>
        /// The member is a property signature.
        /// </summary>
        PropertySignature,
        /// <summary>
        /// The member provides type-coercion.
        /// </summary>
        TypeCoercionMember,
        /// <summary>
        /// The member provides expression coercion through binary
        /// overloads.
        /// </summary>
        BinaryExpressionCoercionMember,
        /// <summary>
        /// The member provides expression coercion through unary
        /// overloads.
        /// </summary>
        UnaryExpressionCoercionMember,
    }
}
