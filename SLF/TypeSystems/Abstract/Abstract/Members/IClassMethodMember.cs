using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    [Flags]
    public enum ClassMethodMemberFlags :
        long
    {
        /// <summary>
        /// No flags are set relative to the instance
        /// class method member modifier flags.
        /// </summary>
        None = ExtendedMethodAttributes.None,
        /// <summary>
        /// Member is a static member.
        /// </summary>
        /* 000000 00001110 00000001 */
        Static = ExtendedMethodAttributes.Static,
        /* 00111001000010 */
        /// <summary>
        /// Method is a virtual (overridable) Method.
        /// </summary>
        /* 000000 01110010 00000010 */
        /* *
         * virtual methods declare 'virtual' and 'newslot'.
         * */
        Virtual = ExtendedMethodAttributes.Virtual,
        /// <summary>
        /// Member is an abstract member.
        /// </summary>
        /* 000001 10010100 00000100 */
        Abstract = ExtendedMethodAttributes.Abstract,
        /// <summary>
        /// Method is an overridden Method.
        /// </summary>
        /* 000010 10101000 00001000 */
        /* *
         * overridden members declare 'virtual' and no
         * 'newslot'.
         * */
        Override = ExtendedMethodAttributes.Override,
        /// <summary>
        /// Method hides base's definition
        /// by signature.
        /// </summary>
        /* 0000 00100000 00000000 00000000 00010000 */
        /* *
         * Hides the previous definition by signature.
         * Default value for instance/static members;
         * neither virtual nor newslot attributes are used
         * by default.
         * */
        HideBySignature = ExtendedMethodAttributes.HideBySignature,
        /* 010100 00000000 00100000 */
        /// <summary>
        /// Member hides base's definition by name.
        /// </summary>
        HideByName = ExtendedMethodAttributes.HideByName,
        /// <summary>
        /// Method is final (removes the ability for 
        /// inheritors to override).
        /// </summary>
        /* 100000 00000000 01000000 */
        /* *
         * As the name implies, final specifies the final 
         * attribute along with virtual, to indicate
         * that it's a sealed override.
         * */
        Final = ExtendedMethodAttributes.Final,
        /// <summary>
        /// The Method is asynchronous in nature.
        /// </summary>
        /* 000000 00000001 00000000 */
        Async = ExtendedMethodAttributes.Async,
        ///<summary>
        /// The Method is an extension method coercing
        /// the member lookup of the type specified in the
        /// first parameter of the method.
        ///</summary>
        /* 00000001 11000100 10000100 00000000 10000000 */
        Extension = 0x1C4840080,
    }
    /// <summary>
    /// Defines properties and methods for working with a method of a <see cref="IClassType"/>.
    /// </summary>
    public interface IClassMethodMember :
        IMethodMember<IClassMethodMember, IClassType>,
        IExtendedMethodMember
    {
        /// <summary>
        /// Returns the base definition of a virtual method that is an override
        /// of the original on the type in which it was first declared.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="IClassMethodMember"/>
        /// is not an overridden member.</exception>
        IClassMethodMember BaseDefinition { get; }
        /// <summary>
        /// Returns the previous definition of a virtual method that is an override
        /// of the original on the next highest point in the hierarchy in which it was either
        /// declared or overridden.
        /// </summary>
        IClassMethodMember PreviousDefinition { get; }
        /// <summary>
        /// Returns whether the current <see cref="IClassMethodMember"/>
        /// is an extension method.
        /// </summary>
        bool IsExtensionMethod { get; }
        /// <summary>
        /// Returns the <see cref="ClassMethodMemberFlags"/> that determine how the
        /// <see cref="IClassMethodMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        new ClassMethodMemberFlags Attributes { get; }
    }
}
