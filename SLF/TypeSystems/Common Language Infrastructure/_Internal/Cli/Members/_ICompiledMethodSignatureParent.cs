using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal interface _ICompiledMethodSignatureParent
    {
        /// <summary>
        /// Returns a <see cref="MethodInfo"/> array
        /// which contains the methods within the
        /// <see cref="_ICompiledMethodSignatureParent"/>.
        /// </summary>
        MethodInfo[] MethodInfos { get; }
        /// <summary>
        /// Obtains the <see cref="_ICompiledMethodSignatureMember"/>
        /// for the <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="MethodBase"/> to retrieve
        /// the <see cref="_ICompiledMethodSignatureMember"/> for.</param>
        /// <returns>A <see cref="_ICompiledMethodSignatureMember"/>
        /// for the <paramref name="info"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">thrown
        /// when the <paramref name="info"/> is not contained within the
        /// <see cref="_ICompiledMethodSignatureParent"/></exception>
        _ICompiledMethodSignatureMember GetSignatureFor(MethodBase info);
    }
}
