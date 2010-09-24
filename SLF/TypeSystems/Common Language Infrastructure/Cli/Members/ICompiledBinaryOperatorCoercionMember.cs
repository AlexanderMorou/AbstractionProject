using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Cli.Members
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// compiled binary operator coercion member.
    /// </summary>
    public interface ICompiledBinaryOperatorCoercionMember :
        IBinaryOperatorCoercionMember,
        ICompiledMember
    {
        /// <summary>
        /// Returns the <see cref="System.Reflection.MethodInfo"/> associated to the <see cref="ICompiledBinaryOperatorCoercionMember"/>.
        /// </summary>
        new MethodInfo MemberInfo { get; }
    }
}
