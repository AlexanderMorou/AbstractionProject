using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf.Cli.Members
{
   public interface ICompiledIndexerMember :
       ICompiledMember,
       IIndexerMember
    {
        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> associated to the <see cref="ICompiledIndexerMember"/>.
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
