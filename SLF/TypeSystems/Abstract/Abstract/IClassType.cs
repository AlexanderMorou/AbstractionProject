using AllenCopeland.Abstraction.Slf.Abstract.Members;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// The type of special modifier applied to the 
    /// <see cref="IClassType"/>.
    /// </summary>
    public enum SpecialClassModifier
    {
        /// <summary>
        /// No special modifier is placed upon the type;
        /// it is a standard instantiable type.
        /// </summary>
        None,
        /// <summary>
        /// Defines a <see cref="Static"/>
        /// class which cannot have type-parameters
        /// or be a nested type.
        /// </summary>
        /// <remarks><para>Classes marked with this are tagged
        /// with the <see cref="StandardModuleAttribute"/>.</para>
        /// <para>Classes marked with this cannot be generics
        /// nor can they be nested.</para></remarks>
        Module,
        /// <summary>
        /// Defines a <see cref="Static"/> class which cannot
        /// have type-parameters or be a nested type.  Further
        /// scopes which import the containing namespace
        /// will see the members of this module in the active
        /// static scope.
        /// </summary>
        /// <remarks><para>Classes marked with this are tagged
        /// with the <see cref="StandardModuleAttribute"/> and
        /// the <see cref="HideModuleNameAttribute"/>.</para>
        /// <para>Classes marked with this cannot be generics
        /// nor can they be nested.</para></remarks>
        HiddenModule,
        /// <summary>
        /// Defines a static class
        /// which can have type-parameters, but can not be
        /// instantiated.
        /// </summary>
        Static,
        /// <summary>
        /// Defines a <see cref="Module"/>-based class which
        /// can not have type-parameters; however, it can
        /// contain extension methods.
        /// </summary>
        /// <remarks>
        /// <para>Classes marked with this may be tagged
        /// with the <see cref="StandardModuleAttribute"/>
        /// based upon the language implementation.</para>
        /// <para>Classes marked with this cannot be generics
        /// nor can they be nested.</para>
        /// </remarks>
        TypeExtensionSource
    }
    
    /// <summary>
    /// Defines properties and methods for working with a class type.
    /// </summary>
    public interface IClassType :
        IGenericType<IGeneralGenericTypeUniqueIdentifier, IClassType>,
        IInstantiableType
            <IClassCtorMember, IClassEventMember, IClassFieldMember, 
             IClassIndexerMember, IClassMethodMember, IClassPropertyMember,
             IGeneralGenericTypeUniqueIdentifier, IClassType>,
        IReferenceType
    {
        /// <summary>
        /// Obtains an <see cref="IClassInterfaceMapping"/> 
        /// related to the <paramref name="type"/> provided.
        /// </summary>
        /// <param name="type">The <see cref="IInterfaceType"/> 
        /// to obtain the map of.</param>
        /// <returns>A <see cref="IClassInterfaceMapping"/> relative
        /// to the properties and methods implemented
        /// by the <see cref="IClassType"/> with regards
        /// to <paramref name="type"/>.</returns>
        new IClassInterfaceMapping GetInterfaceMap(IInterfaceType type);
        /// <summary>
        /// Returns the <see cref="SpecialClassModifier"/>
        /// applied to the <see cref="IClassType"/>.
        /// </summary>
        SpecialClassModifier SpecialModifier { get; }
        /// <summary>
        /// Returns whether the <see cref="IClassType"/> has the 
        /// attribute defined.
        /// </summary>
        /// <param name="attributeType">The type of attribute to check
        /// the existence of.</param>
        /// <param name="inherit">Whether to check the inherited 
        /// attributes of the <see cref="IClassType"/>.</param>
        /// <returns>true if the <paramref name="attributeType"/>
        /// is present on the current <see cref="IClassType"/> or one 
        /// of its bases; false, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// thrown when <paramref name="attributeType"/> is null.
        /// </exception>
        bool IsDefined(IType attributeType, bool inherit);
        /// <summary>
        /// Returns the type from which the current <see cref="IClassType"/>
        /// derives.
        /// </summary>
        new IClassType BaseType { get; }
    }
}
