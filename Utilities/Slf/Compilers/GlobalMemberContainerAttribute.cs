using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Represents an attribute that connects a module with its
    /// global methods/fields container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Module, AllowMultiple = false)]
    public class GlobalMemberContainerAttribute :
        Attribute
    {
        /// <summary>
        /// Creates a new <see cref="GlobalFieldContainerAttribute"/>
        /// which denotes the <paramref name="globalMemberType"/>
        /// which contains the members that have global visibility.
        /// </summary>
        /// <param name="globalMemberType">The <see cref="Type"/>
        /// which contains the members with global visibility.</param>
        public GlobalMemberContainerAttribute(Type globalMemberType)
        {
            if (!globalMemberType.IsDefined(typeof(CompilerGeneratedAttribute), false))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.globalMemberType, ArgumentExceptionMessage.TypeMustBeCompilerGenerated);
            if (!(globalMemberType.IsClass && ((globalMemberType.Attributes & (TypeAttributes.Abstract | TypeAttributes.Sealed)) != (TypeAttributes)0)))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.globalMemberType, ArgumentExceptionMessage.TypeMustBeStaticClass);
            this.GlobalMemberType = globalMemberType;
        }

        /// <summary>
        /// Returns the <see cref="Type"/> which denotes the container
        /// type which specifies the global members for a given module.
        /// </summary>
        public Type GlobalMemberType { get; private set; }
    }
}
