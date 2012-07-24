using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Globalization;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a unique identifier
    /// which represents an assembly.
    /// </summary>
    public interface IAssemblyUniqueIdentifier :
        IGeneralDeclarationUniqueIdentifier,
        IEquatable<IAssemblyUniqueIdentifier>
    {
        /// <summary>
        /// Returns the <see cref="IVersion"/> of the assembly.
        /// </summary>
        IVersion Version { get; }
        /// <summary>
        /// Returns the <see cref="ICultureIdentifier"/> which denotes the culture
        /// of the assembly.
        /// </summary>
        ICultureIdentifier Culture { get; }
        /// <summary>
        /// Returns the <see cref="Byte"/> array of the last 8 digits
        /// of the SHA-1 hash of the public key under which the assembly
        /// is assigned.
        /// </summary>
        byte[] PublicKeyToken { get; }
        /// <summary>
        /// Creates a new <see cref="IGeneralGenericTypeUniqueIdentifier"/>
        /// relative to the assembly represented by the current
        /// <see cref="IAssemblyUniqueIdentifier"/>.
        /// </summary>
        /// <param name="namespace">The <see cref="String"/> value which represents the name of the namespace
        /// the type resides within.</param>
        /// <param name="name">The <see cref="String"/> value which represents
        /// the name of the type.</param>
        /// <param name="typeParameters">The <see cref="Int32"/> value which represents
        /// the number of type parameters within the type.</param>
        /// <returns></returns>
        IGeneralGenericTypeUniqueIdentifier GetTypeIdentifier(string @namespace, string name, int typeParameters);
        /// <summary>
        /// Creates a new <see cref="IGeneralGenericTypeUniqueIdentifier"/>
        /// relative to the assembly represented by the current
        /// <see cref="IAssemblyUniqueIdentifier"/>.
        /// </summary>
        /// <param name="namespace">The <see cref="IGeneralDeclarationUniqueIdentifier"/> of the namespace
        /// the type resides within.</param>
        /// <param name="name">The <see cref="String"/> value which represents
        /// the name of the type.</param>
        /// <param name="typeParameters">The <see cref="Int32"/> value which represents
        /// the number of type parameters within the type.</param>
        /// <returns></returns>
        IGeneralGenericTypeUniqueIdentifier GetTypeIdentifier(IGeneralDeclarationUniqueIdentifier @namespace, string name, int typeParameters);
        /// <summary>
        /// Obtains a unique identifier for a type with the <paramref name="name"/>
        /// provided.
        /// </summary>
        /// <param name="namespace">The <see cref="String"/> value which represents the name of the namespace
        /// the type resides within.</param>
        /// <param name="name">The display name of the type which differentiates
        /// it from its siblings.</param>
        /// <returns>A <see cref="IGeneralTypeUniqueIdentifier"/>
        /// which represents the type.</returns>
        IGeneralTypeUniqueIdentifier GetTypeIdentifier(string @namespace, string name);
        /// <summary>
        /// Obtains a unique identifier for a type with the <paramref name="name"/>
        /// provided.
        /// </summary>
        /// <param name="namespace">The <see cref="IGeneralDeclarationUniqueIdentifier"/> of the namespace
        /// the type resides within.</param>
        /// <param name="name">The display name of the type which differentiates
        /// it from its siblings.</param>
        /// <returns>A <see cref="IGeneralTypeUniqueIdentifier"/>
        /// which represents the type.</returns>
        IGeneralTypeUniqueIdentifier GetTypeIdentifier(IGeneralDeclarationUniqueIdentifier @namespace, string name);
    }
}
