using System;
using System.Diagnostics.CodeAnalysis;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{

    /// <summary>
    /// Defines properties and methods for working with a declaration that
    /// is limited by a scope.
    /// </summary>
    public interface IScopedDeclaration :
        IDeclaration
    {
        /// <summary>
        /// Returns the access level of the <see cref="IScopedDeclaration"/>.
        /// </summary>
        AccessLevelModifiers AccessLevel { get; }
    }

    /// <summary>
    /// The accessability of an <see cref="IScopedDeclaration"/>.
    /// </summary>
    /// <remarks>
    /// <para><see cref="AccessLevelModifiers.InternalProtected"/> 
    /// (<seealso cref="Type.IsNestedFamANDAssem"/>):</para>
    /// <para>Not available in C&#9839; or Visual Basic; therefore,
    /// <see cref="ProtectedInternal"/> (<seealso cref="Type.IsNestedFamORAssem"/>)
    /// will be translated as an alternative and a warning thrown.
    /// </para>
    /// <para>
    /// <see cref="AccessLevelModifiers.Private"/>:</para>
    /// <para>Local scope is only available at a type-level. 
    /// Namespaces cannot use private scope.</para>
    /// <para>Local scope refers to the scope that defined the item.
    /// If it's a nested type then all member scopes higher than this
    /// level are limited to the same scope.</para>
    /// <para><see cref="AccessLevelModifiers.Public"/>:
    /// <see cref="Public"/> does not mean it's accessable to 
    /// everything, because if it's a member declared in a private
    /// class only that class (and the local scope it was defined in)
    /// has access to it.</para>
    /// <para>Not all combinations are allowed, for more information
    /// as to why see <a href="http://www.codeproject.com/KB/recipes/spc_bits.aspx">SPC Bits</a>.</para>
    /// </remarks>
    /* *
     * Added suppression for CA2217:DoNotMarkEnumsWithFlags.
     * This enumeration is intended for flag usage; however, the
     * combinations are restricted.  The specifics of the 
     * restrictions are listed in each element's documentation 
     * comments.
     * */
    [SuppressMessage("Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags")]
    [FlagsAttribute]
    public enum AccessLevelModifiers
    {
        /// <summary>
        /// Declaration is accessable to the inheritance family at or below the current inheritance 
        /// threshold only in the defining assembly (<seealso cref="Type.IsNestedFamANDAssem"/>).
        /// </summary>
        /// <remarks><para>Not available in C&#9839; or Visual Basic; therefore, 
        /// <see cref="ProtectedInternal"/> will be translated as an alternative.</para>
        /// <para>A combination of <see cref="AssemblyOr"/> and <see cref="Protected"/>.</para></remarks>
        InternalProtected = AssemblyAnd | Protected,

        /// <summary>
        /// Declaration is accessable to the assembly that defined it.
        /// </summary>
        /// <remarks>A combination of <see cref="AssemblyAnd"/> and <see cref="Public"/>.</remarks>
        Internal = AssemblyAnd | Public,

        /* 000000000111000001 */
        /// <summary>
        /// Can be accessed by the assembly, and is further restrained by
        /// the following possible scopes: <see cref="Protected"/> or
        /// <see cref="Public"/>.
        /// </summary>
        AssemblyAnd = 0x1c1,

        /* 000000111001000010 */
        /// <summary>
        /// Can be accessed by the assembly or the other valid scope: 
        /// <see cref="Protected"/>.
        /// </summary>
        AssemblyOr = 0xe42,

        /* 000111001010000100 */
        /// <summary>
        /// Declaration is accessable to the current local scope.
        /// </summary>
        /// <remarks><para>Local scope is only available at a type-level. 
        /// Namespaces cannot use private scope.</para>
        /// <para>Local scope refers to the scope that defined the item.  If it's a nested type 
        /// then all member scopes higher than this level are limited to the same scope.</para>
        /// <para>Cannot be combined with any other flag.</para>
        /// </remarks>
        /// <example>
        /// <code language="C#">
        /// class Test
        /// {
        ///     private class NestTest
        ///     {
        ///         //The public constructor is limited to the local scope of Test because 
        ///         //of the private modifier on NestTest.
        ///         public NestTest()
        ///         {
        ///             //...
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        Private = 0x7284,

        /* 011001010100001000 */
        /// <summary>
        /// Special scope indicating assembly-only, different
        /// from <see cref="Internal"/> in that InternalsVisibleToAttribute
        /// is ineffective.
        /// </summary>
        /// <remarks>Cannot be combined with any other flag.</remarks>
        PrivateScope = 0x19508,

        /* 101010100000010000 */
        /// <summary>
        /// Declaration is accessable to everything within scope.
        /// </summary>
        /// <remarks><para><see cref="Public"/> does not mean it's accessable to everything, because if it's 
        /// a member declared in a private class only that class (and the local scope it was defined in) 
        /// has access to it.</para>
        /// <para>If combined with <see cref="AssemblyAnd"/>, the member is visible to 
        /// the assembly, and nothing else.
        /// </para>
        /// <para>Cannot be combined with anything but <see cref="AssemblyAnd"/>.</para></remarks>
        Public = 0x2a810,

        /* 110100000000100000 */
        /// <summary>
        /// Declaration is accessable to the inheritance family at or below 
        /// the current inheritance threshold.
        /// </summary>
        /// <remarks>Cannot be combined with anything but <see cref="AssemblyAnd"/> 
        /// or <see cref="AssemblyOr"/>.</remarks>
        Protected = 0x34020,

        /// <summary>
        /// Declaration is accessable to the inheritance family at or below the current inheritance threshold 
        /// as well as the defining assembly (<seealso cref="Type.IsNestedFamORAssem"/>).
        /// </summary>
        /// <remarks><para>A combination of <see cref="AssemblyOr"/> and <see cref="Protected"/>.</para></remarks>
        ProtectedInternal = AssemblyOr | Protected,
    }
}
