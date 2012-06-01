using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IIdentityManager<TTypeIdentity, TAssemblyIdentity, TAssembly> :
        ITypeIdentityManager<TTypeIdentity>,
        IAssemblyIdentityManager<TAssemblyIdentity, TAssembly>
        where TAssembly :
            IAssembly
    {

    }

    /// <summary>
    /// Defines properties and methods for obtaining a model specific representation
    /// of a <see cref="IType"/> relative to the <typeparamref name="TAssemblyIdentity"/>
    /// instances from the containing model.
    /// </summary>
    /// <typeparam name="TAssemblyIdentity">The  type used in the containing model
    /// to represent <typeparamref name="TAssembly"/> instances.</typeparam>
    /// <typeparam name="TAssembly">The type of <see cref="TAssembly"/>
    /// to retrieve.</typeparam>
    public interface IAssemblyIdentityManager<TAssemblyIdentity, TAssembly> :
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IAssembly"/> from the <paramref name="assemblyIdentity"/> 
        /// from the underlying model in which it exists.
        /// </summary>
        /// <param name="assemblyIdentity">The <typeparamref name="TAssemblyIdentity"/>
        /// which represents the assembly's identity in the base model on which
        /// the <see cref="IAssembly"/> is defined.</param>
        /// <returns>An <see cref="IAssembly"/> instance relative to the
        /// <paramref name="assemblyIdentity"/> provided.</returns>
        TAssembly ObtainAssemblyReference(TAssemblyIdentity assemblyIdentity);
    }

    /// <summary>
    /// Defines properties and methods for obtaining a model specific representation
    /// of a <see cref="IType"/> relative to <typeparamref name="TTypeIdentity"/>
    /// instances from the containing model.
    /// </summary>
    /// <typeparam name="TTypeIdentity">The identity relative to the model
    /// in which the <see cref="IType"/> instances are created from.</typeparam>
    /// <typeparam name="TType">The kind of <see cref="IType"/> which is derived
    /// from the model.</typeparam>
    public interface ITypeIdentityManager<TTypeIdentity> :
        ITypeIdentityManager
    {
        /// <summary>
        /// Returns the <see cref="IType"/> from the <paramref name="typeIdentity"/>
        /// from the underlying model in which it exists.
        /// </summary>
        /// <param name="typeIdentity">The <typeparamref name="TTypeIdentity"/> that represents the
        /// type's identity in the base model on which the <see cref="IType"/>
        /// is defined.</param>
        /// <returns>A <see cref="IType"/> instance relative to the
        /// <paramref name="typeIdentity"/>.</returns>
        IType ObtainTypeReference(TTypeIdentity typeIdentity);
    }

    public interface ITypeIdentityManager :
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IType"/> from the <paramref name="coreType"/>
        /// from the underlying model in which it exists.
        /// </summary>
        /// <param name="coreType">The <see cref="RuntimeCoreType"/> that represents a
        /// core type in the base model on which the <see cref="IType"/>
        /// is defined.</param>
        /// <returns>An <see cref="IType"/> instance relative to the
        /// <paramref name="coreType"/> provided.</returns>
        IType ObtainTypeReference(RuntimeCoreType coreType);
        /// <summary>
        /// Returns the <see cref="IType"/> from the
        /// <see cref="typeIdentity"/> from the underlying model in
        /// which it exists.
        /// </summary>
        /// <param name="typeIdentity">The <see cref="Object"/>
        /// which represents the type's primitive identity in the base model on which the 
        /// <see cref="IType"/> is defined.</param>
        /// <returns>An <see cref="IType"/> instance relative to the
        /// <paramref name="typeIdentity"/> provided.</returns>
        IType ObtainTypeReference(object typeIdentity);
        /// <summary>
        /// Returns whether the <see cref="IType"/> from the
        /// as a metadatum entity is inheritable.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/>,
        /// which represents a metadatum to be applied to a member,
        /// to discern the inheritability of.</param>
        /// <returns>true if the <paramref name="metadatumType"/>
        /// is inheritable; false, otherwise.</returns>
        bool IsMetadatumInheritable(IType metadatumType);
        /// <summary>
        /// Returns the <see cref="IStandardRuntimeEnvironmentInfo"/> necessary to 
        /// identify the target runtime.
        /// </summary>
        IStandardRuntimeEnvironmentInfo RuntimeEnvironment { get; }

    }
}
