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
    /// <summary>
    /// The requirement set forth on the <see cref="ITypeCoercionMember"/> which
    /// stipulates whether the conversion is implicit or explicit.
    /// </summary>
    public enum TypeConversionRequirement
    {
        Unknown,
        /// <summary>
        /// The conversion from/to the containing type of the <see cref="ITypeCoercionMember"/>
        /// must be specified through a cast.
        /// </summary>
        Explicit,
        /// <summary>
        /// The conversion from/to the containing type of the <see cref="ITypeCoercionMember"/>
        /// is inferred by use.
        /// </summary>
        Implicit
    }
    /// <summary>
    /// The direction in which the containing type of the 
    /// <see cref="ITypeCoercionMember"/> is coerced.
    /// </summary>
    public enum TypeConversionDirection
    {
        /// <summary>
        /// The conversion is to the containing type from
        /// the <see cref="ITypeCoercionMember.CoercionType"/> 
        /// specified.
        /// </summary>
        ToContainingType,
        /// <summary>
        /// The conversion is from the containing type to
        /// the <see cref="ITypeCoercionMember.CoercionType"/> 
        /// specified.
        /// </summary>
        FromContainingType
    }
    /// <summary>
    /// Defines properties and methods for working with a type-coercion member.
    /// </summary>
    public interface ITypeCoercionMember :
        ICoercionMember
    {
        /// <summary>
        /// Returns whether the conversion overload is implicit or explicit.
        /// </summary>
        TypeConversionRequirement Requirement { get; }
        /// <summary>
        /// Returns whether the conversion overload is from the containing type or 
        /// to the containing type.
        /// </summary>
        TypeConversionDirection Direction { get; }
        /// <summary>
        /// Returns the type which is coerced by the overload.
        /// </summary>
        IType CoercionType { get; }
    }
}
