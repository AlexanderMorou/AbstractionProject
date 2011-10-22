using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Defines properties and methods for working with a compiled method member.
    /// </summary>
    public interface ICompiledMethodMember :
        IMethodMember,
        ICompiledMember
    {
        /// <summary>
        /// Returns the <see cref="MethodInfo"/> associated to the <see cref="ICompiledMethodMember"/>.
        /// </summary>
        new MethodInfo MemberInfo { get; }
        /// <summary>
        /// Invokes the <see cref="MemberInfo"/> associated to the <see cref="ICompiledMethodMember"/>
        /// with the <paramref name="target"/> and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="Object"/> on which to invoke the <see cref="MemberInfo"/>.</param>
        /// <param name="parameters">The sequence of <see cref="Object"/> instances which denotes
        /// the information to pass to the <see cref="MemberInfo"/>.</param>
        /// <returns>A <see cref="Object"/> instance based upon the return of the call.</returns>
        /// <remarks>Convenience function.</remarks>
        object Invoke(object target, params object[] parameters);
        /// <summary>
        /// Invokes the <see cref="MemberInfo"/> associated to the <see cref="ICompiledMethodMember"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">The sequence of <see cref="Object"/> instances which denotes
        /// the information to pass to the <see cref="MemberInfo"/>.</param>
        /// <returns>A <see cref="Object"/> instance based upon the return of the call.</returns>
        /// <remarks>Convenience function.</remarks>
        object Invoke(params object[] parameters);
        /// <summary>
        /// Invokes the <see cref="MemberInfo"/> associated to the <see cref="ICompiledMethodMember"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <typeparam name="T">The type of value the <see cref="MemberInfo"/> returns.</typeparam>
        /// <param name="parameters">The sequence of <see cref="Object"/> instances which denotes
        /// the information to pass to the <see cref="MemberInfo"/>.</param>
        /// <returns>A <typeparamref name="T"/> instance based upon the return of the call.</returns>
        /// <remarks>Convenience function.</remarks>
        T Invoke<T>(params object[] parameters);
        /// <summary>
        /// Invokes the <see cref="MemberInfo"/> associated to the <see cref="ICompiledMethodMember"/>
        /// with the <paramref name="target"/> and <paramref name="parameters"/> provided.
        /// </summary>
        /// <typeparam name="T">The type of value the <see cref="MemberInfo"/> returns.</typeparam>
        /// <param name="target">The <see cref="Object"/> on which to invoke the <see cref="MemberInfo"/>.</param>
        /// <param name="parameters">The sequence of <see cref="Object"/> instances which denotes
        /// the information to pass to the <see cref="MemberInfo"/>.</param>
        /// <returns>A <see cref="Object"/> instance based upon the return of the call.</returns>
        /// <remarks>Convenience function.</remarks>
        T Invoke<T>(object target, params object[] parameters);
    }
}
