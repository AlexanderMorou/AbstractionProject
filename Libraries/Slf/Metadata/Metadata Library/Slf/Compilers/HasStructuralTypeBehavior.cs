using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Metadata;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Provides an attribute for associating a type-parameter to an
    /// interface for inference of type parameter constructors, methods, properties
    /// indexers, and events.
    /// </summary>
    [AttributeUsage(AttributeTargets.GenericParameter)]
    public class HasStructuralTypeBehavior :
        Attribute
    {
        /// <summary>
        /// Data member for <see cref="StructuralTypeInfo"/>.
        /// </summary>
        private Type structuralTypeInfo;
        /// <summary>
        /// Data member for <see cref="BridgeGuid"/>.
        /// </summary>
        private Guid bridgeGuid;
        /// <summary>
        /// Creates a new <see cref="HasStructuralTypeBehavior"/>
        /// instance with the <paramref name="structuralTypeInfo"/> and 
        /// <paramref name="bridgeGuid"/> provided.
        /// </summary>
        /// <param name="structuralTypeInfo">The <see cref="Type"/> relative
        /// to the local of the generic that defines the signatures of the
        /// members to of the structural type.</param>
        /// <param name="bridgeGuid">The <see cref="string"/> which denotes the
        /// unique guid of the type parameter's structural typing functionality.</param>
        /// <remarks>bridgeGuid is used to denote a simple way for a compiler to 
        /// reference the type-parameter in question, since you can't inject the type
        /// of the type-parameter itself.</remarks>
        public HasStructuralTypeBehavior(Type structuralTypeInfo, string bridgeGuid)
        {
            this.structuralTypeInfo = structuralTypeInfo;
            Guid.TryParse(bridgeGuid, out this.bridgeGuid);
        }

        public bool IsGenericLocalTypeValid
        {
            get
            {
                if (structuralTypeInfo == null)
                    return false;
                /* *
                 * Verify that the type is properly structured...
                 * */
                Type t = structuralTypeInfo;
                if (t.IsGenericType)
                {
                    if (!(t.IsGenericTypeDefinition))
                        t = t.GetGenericTypeDefinition();
                    Type tDecl = t.DeclaringType;
                    if (!tDecl.IsGenericTypeDefinition)
                        return false;
                    if (tDecl.GetGenericArguments().Length != t.GetGenericArguments().Length)
                        return false;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Returns the <see cref="Type"/> of the local type defined in the 
        /// generic type that has new signature information
        /// defined on a type-parameter.
        /// </summary>
        public Type StructuralTypeInfo
        {
            get
            {
                return this.structuralTypeInfo;
            }
        }

        /// <summary>
        /// Returns the <see cref="Guid"/> associated to the bridge.
        /// </summary>
        public Guid BridgeGuid { get { return this.bridgeGuid; } }

        /// <summary>
        /// Returns whether the <see cref="BridgeGuid"/> is valid.
        /// </summary>
        public bool BridgeGuidValid { get { return !this.BridgeGuid.Equals(Guid.Empty); } }

    }
}
