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
    public interface ICompiledIndexerSignatureMember :
        ICompiledMember,
        IIndexerSignatureMember
    {
        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> associated to the <see cref="ICompiledIndexerSignatureMember"/>.
        /// </summary>
        new PropertyInfo MemberInfo { get; }
        T GetValue<T>(object target, params object[] indices);
        T GetValue<T>(params object[] indices);
        object GetValue(object target, params object[] indices);
        object GetValue(params object[] indices);
        void SetValue(object target, object[] indices, object value);
        void SetValue(object[] indices, object value);
    }
}
