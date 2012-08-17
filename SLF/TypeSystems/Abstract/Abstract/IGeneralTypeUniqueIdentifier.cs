using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// general type's unique identifier which is
    /// either a nested type or a top-level type, but not
    /// a generic parameter.
    /// </summary>
    public interface IGeneralTypeUniqueIdentifier :
        ITypeUniqueIdentifier,
        IGeneralDeclarationUniqueIdentifier,
        IEquatable<IGeneralTypeUniqueIdentifier>
    {
        /// <summary>
        /// Returns the <see cref="IAssemblyUniqueIdentifier"/>
        /// which identifies the assembly from which the type is derived.
        /// </summary>
        IAssemblyUniqueIdentifier Assembly { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralDeclarationUniqueIdentifier"/> which
        /// designates the namespace from which the <see cref="IType"/> the 
        /// <see cref="IGeneralTypeUniqueIdentifier"/> is derived.
        /// </summary>
        IGeneralDeclarationUniqueIdentifier Namespace { get; }
        /// <summary>
        /// Returns a <see cref="ITypeUniqueIdentifier"/> which represents a nested
        /// type of the current <see cref="ITypeUniqueIdentifier"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the nested type.</param>
        /// <returns>A <see cref="ITypeUniqueIdentifier"/>
        /// which represents the nested type.</returns>
        IGeneralTypeUniqueIdentifier GetNestedIdentifier(string name);
        /// <summary>
        /// Returns a <see cref="IGeneralGenericTypeUniqueIdentifier"/> which represents a nested
        /// type of the current <see cref="IGeneralGenericTypeUniqueIdentifier"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the nested type.</param>
        /// <param name="typeParameterCount">A <see cref="Int32"/> value which
        /// determines the number of type-parameters the nested type contains.
        /// </param>
        /// <returns>A <see cref="IGeneralGenericTypeUniqueIdentifier"/>
        /// which represents the nested type.</returns>
        IGeneralGenericTypeUniqueIdentifier GetNestedIdentifier(string name, int typeParameterCount);
        /// <summary>
        /// Returns a <see cref="ITypeUniqueIdentifier"/> which represents a nested
        /// type of the current <see cref="ITypeUniqueIdentifier"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the nested type.</param>
        /// <param name="namespace">The <see cref="IGeneralDeclarationUniqueIdentifier"/> which denotes
        /// the additional namespace information about the nested type.</param>
        /// <returns>A <see cref="ITypeUniqueIdentifier"/>
        /// which represents the nested type.</returns>
        IGeneralTypeUniqueIdentifier GetNestedIdentifier(string name, IGeneralDeclarationUniqueIdentifier @namespace);
        /// <summary>
        /// Returns a <see cref="IGeneralGenericTypeUniqueIdentifier"/> which represents a nested
        /// type of the current <see cref="IGeneralGenericTypeUniqueIdentifier"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the nested type.</param>
        /// <param name="typeParameterCount">A <see cref="Int32"/> value which
        /// determines the number of type-parameters the nested type contains.
        /// </param>
        /// <param name="namespace">The <see cref="IGeneralDeclarationUniqueIdentifier"/> which denotes
        /// the additional namespace information about the nested type.</param>
        /// <returns>A <see cref="IGeneralGenericTypeUniqueIdentifier"/>
        /// which represents the nested type.</returns>
        IGeneralGenericTypeUniqueIdentifier GetNestedIdentifier(string name, int typeParameterCount, IGeneralDeclarationUniqueIdentifier @namespace);
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> which denotes the
        /// owning type of the current type identifier.
        /// </summary>
        IGeneralTypeUniqueIdentifier ParentIdentifier { get; }
        /// <summary>
        /// Returns a <see cref="String"/> which represents the full name
        /// of the type.
        /// </summary>
        /// <remarks>Does not include assembly information.</remarks>
        string FullName { get; }
    }
}
