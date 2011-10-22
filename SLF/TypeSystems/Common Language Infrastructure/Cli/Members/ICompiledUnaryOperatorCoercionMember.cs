using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Cli.Members
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// compiled unary operator coercion member.
    /// </summary>
    public interface ICompiledUnaryOperatorCoercionMember :
        IUnaryOperatorCoercionMember,
        ICompiledMember
    {
        /// <summary>
        /// Returns the <see cref="System.Reflection.MethodInfo"/> associated to the <see cref="ICompiledUnaryOperatorCoercionMember"/>.
        /// </summary>
        new MethodInfo MemberInfo { get; }

    }
}
