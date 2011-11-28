using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    /// <summary>
    /// The kind of identity represented by the aggregated
    /// identity.
    /// </summary>
    public enum AggregateIdentityKind
    {
        /// <summary>
        /// The identity represents the union of multiple assemblies
        /// at the root namespace level.
        /// </summary>
        RootNamespace,
        /// <summary>
        /// The identity represents the union of multiple assemblies
        /// on some namespace.
        /// </summary>
        Namespace,
        /// <summary>
        /// The identity represents the ambiguity between a type
        /// and a namespace.
        /// </summary>
        NamespaceTypeAmbiguity,
        /// <summary>
        /// The identity represents a specific type within a union of multiple
        /// assemblies that does not clash with the name of another type.
        /// </summary>
        Type,
        /// <summary>
        /// The identity represents a stand-alone type with a series of 
        /// generic variations.
        /// </summary>
        TypeGenericSet,
        /// <summary>
        /// The identity represents a specific, perhaps generic, type
        /// which clashes with a secondary identity from another assembly.
        /// </summary>
        TypeAmbiguity,
        /// <summary>
        /// The identity represents a series of types and methods typically
        /// observed when global methods clash with types from another 
        /// assembly.
        /// </summary>
        TypeMethodSet,
        /// <summary>
        /// The identity represents a stand-alone method with a series
        /// of generic variations.
        /// </summary>
        MethodGenericSet,
        /// <summary>
        /// The identity represents a series of methods under a common name.
        /// </summary>
        MethodSet,
        /// <summary>
        /// The identity represents a property under a given name.
        /// </summary>
        Property,
        /// <summary>
        /// The identity represents a series of indexers under
        /// a common name.
        /// </summary>
        IndexerSet,
        /// <summary>
        /// The identity represents a field under a given name.
        /// </summary>
        Field,
        /// <summary>
        /// The identity represents a series of members which 
        /// are ambiguous within the current context.
        /// </summary>
        MemberAmbiguity,
    }
}
