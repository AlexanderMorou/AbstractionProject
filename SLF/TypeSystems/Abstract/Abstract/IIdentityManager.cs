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

    public interface IGenericConstructIdentityManager<TGenericConstruct, TGenericParameter>
        where TGenericConstruct :
                IGenericParamParent<TGenericParameter, TGenericConstruct>
        where TGenericParameter :
                IGenericParameter<TGenericParameter, TGenericConstruct>
    {
        TGenericConstruct MakeGenericClosure(TGenericConstruct source, ITypeCollectionBase closureArguments);
    }

    public interface ITypeIdentityManager :
        IGenericConstructIdentityManager<IClassType, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IClassType>>,
        IGenericConstructIdentityManager<IDelegateType, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IDelegateType>>,
        IGenericConstructIdentityManager<IInterfaceType, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IInterfaceType>>,
        IGenericConstructIdentityManager<IStructType, IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IStructType>>,
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
        /// Returns whether the <see cref="IType"/> is a metadatum type.
        /// </summary>
        /// <param name="possibleMetadatumType">The <see cref="IType"/> which represents
        /// the potential metadatum type.</param>
        /// <returns>true, if the <paramref name="possibleMetadatumType"/>
        /// can be used as metadata; false, otherwise.</returns>
        bool IsMetadatumType(IType possibleMetadatumType);
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
        /// <summary>
        /// Creates a new <see cref="IType"/> with the <paramref name="elementType"/>
        /// relative to the <paramref name="classification"/> provided.
        /// </summary>
        /// <param name="elementType">The <see cref="IType"/> which needs a special <paramref name="classification"/></param>
        /// <param name="classification">The <see cref="TypeElementClassification"/> which denotes
        /// how to marshal the resulted <paramref name="elementType"/> classification.</param>
        /// <returns>A <see cref="IType"/> which represents the special <paramref name="classification"/>
        /// of <paramref name="elementType"/>.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="classification"/> is not one of:
        /// <list type="number"><item><term>Nullable</term></item>
        /// <item><term>Pointer</term></item>
        /// <item><term>Reference</term></item></list></exception>
        IType MakeClassificationType(IType elementType, TypeElementClassification classification);
        /// <summary>
        /// Creates a new <see cref="IArrayType"/> with the <paramref name="elementType"/>
        /// provided.
        /// </summary>
        /// <param name="elementType">The <see cref="IType"/> to create
        /// a single-dimensional array from.</param>
        /// <returns>A <see cref="IArrayType"/> which represents the <paramref name="elementType"/>
        /// as an array with one dimension, with no lower-bound or length specified.</returns>
        IArrayType MakeArray(IType elementType);
        /// <summary>
        /// Creates a new <see cref="IArrayType"/> with the <paramref name="elementType"/>
        /// and <paramref name="rank"/>.
        /// </summary>
        /// <param name="elementType">The <see cref="IType"/> to create a multi-dimensional
        /// array from.</param>
        /// <param name="rank">The <see cref="Int32"/> value which denotes the nubmer of dimensions in the
        /// multi-dimensional array.</param>
        /// <returns>A <see cref="IArrayType"/> which represents the <paramref name="elementType"/>
        /// as an array with <paramref name="rank"/> dimensions, each with 
        /// no lower bound or length specified.</returns>
        IArrayType MakeArray(IType elementType, int rank);
        /// <summary>
        /// Creates a new <see cref="IArrayType"/> with the <paramref name="elementType"/>,
        /// <paramref name="lowerBounds"/>, and <paramref name="lengths"/> provided.
        /// </summary>
        /// <param name="elementType">The <see cref="IType"/> to create an n-dimensional array from.</param>
        /// <param name="lowerBounds">The <see cref="Int32"/> series which represents the 
        /// lower bounds of the result <see cref="IArrayType"/> dimensions.</param>
        /// <param name="lengths">The <see cref="Int32"/> series which represents the 
        /// lengths of the result <see cref="IArrayType"/> dimensions.</param>
        /// <returns>A new <see cref="IArrayType"/> which represents the <paramref name="elementType"/>
        /// as an array with its <paramref name="lowerBounds"/> and <paramref name="lengths"/> provided.</returns>
        /// <remarks><para><paramref name="lowerBounds"/> and <paramref name="lengths"/> need not contain the same number
        /// of elements.</para><para>The number of dimensions within the result <see cref="IArrayType"/> will
        /// be the greater of the number of elements within <paramref name="lowerBounds"/> and
        /// <paramref name="lengths"/>.</para>
        /// <para>If <paramref name="lowerBounds"/> and <paramref name="lengths"/> are both null
        /// then the resultant array is a single-dimensional array with no lower bounds and
        /// length specified.</para></remarks>
        IArrayType MakeArray(IType elementType, int[] lowerBounds = null, uint[] lengths = null);
        /// <summary>
        /// Creates a new <see cref="IModifiedType"/> with the <paramref name="modifications"/>
        /// provided.
        /// </summary>
        /// <param name="elementType">The <see cref="IType"/> to create a modified variation of.</param>
        /// <param name="modifications">The <see cref="TypeModification"/> series which represents
        /// the optional and required nature of the type modifiers.</param>
        /// <returns>An <see cref="IModifiedType"/> which represents the modified <paramref name="elementType"/>
        /// as per the <paramref name="modifications"/> provided.</returns>
        IModifiedType MakeModifiedType(IType elementType, params TypeModification[] modifications);
        /// <summary>
        /// Returns the <see cref="RuntimeCoreType"/> associated to the
        /// <paramref name="type"/> provided.
        /// </summary>
        /// <param name="type">The <see cref="IType"/> to obtain the <see cref="RuntimeCoreType"/>
        /// of.</param>
        /// <returns></returns>
        RuntimeCoreType ObtainCoreType(IType type);

    }
}
