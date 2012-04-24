using System.Runtime.InteropServices;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
        None = 0x0,
        /// <summary>
        /// Defines a class which requires an inheritor to
        /// derive from it and implement elements marked as
        /// abstract; classes which are abstract cannot be
        /// instantiated until the inheritors fill in all 
        /// necessary functionality.
        /// </summary>
        Abstract = 0x21,
        /// <summary>
        /// Defines a class whose static components are in scope
        /// when the class itself is within scope.
        /// </summary>
        Hidden = 0x2,
        /// <summary>
        /// Defines a <see cref="Static"/>
        /// class which cannot have type-parameters
        /// or be a nested type.
        /// </summary>
        /// <remarks><para>Classes marked with this are tagged
        /// with the <see cref="StandardModuleAttribute"/>.</para>
        /// <para>Classes marked with this cannot be generics
        /// nor can they be nested.</para></remarks>
        Module = 0xe4,
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
        HiddenModule = Hidden | Module,
        /// <summary>
        /// Defines a class which has been sealed, no further
        /// inheritance can branch from this point.
        /// </summary>
        Sealed = 0x48,
        /// <summary>
        /// Defines a static class which can have type-parameters,
        /// can not be derived from or be instantiated, and is thusly
        /// <see cref="Abstract"/> and <see cref="Sealed"/>.
        /// </summary>
        Static = Abstract | Sealed,
        /// <summary>
        /// Defines a static class which can not have type-parameters,
        /// can not be derived from, or be instantiated, where its members
        /// are in scope when the class itself is within scope; it is
        /// thusly <see cref="Hidden"/>, <see cref="Sealed"/> and
        /// <see cref="Abstract"/>.
        /// </summary>
        HiddenStatic = Hidden | Static,
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
        TypeExtensionSource = 0x90,
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
        /// Returns the type from which the current <see cref="IClassType"/>
        /// derives.
        /// </summary>
        new IClassType BaseType { get; }
    }
}
