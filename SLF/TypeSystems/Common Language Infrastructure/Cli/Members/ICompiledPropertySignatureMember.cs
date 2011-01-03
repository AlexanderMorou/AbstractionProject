using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Cli.Members
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with a compiled property member.
    /// </summary>
    public interface ICompiledPropertySignatureMember :
        IPropertySignatureMember,
        ICompiledMember
    {
        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> associated
        /// to the <see cref="ICompiledPropertySignatureMember"/>.
        /// </summary>
        new PropertyInfo MemberInfo { get; }
        T GetValue<T>(object target);
        T GetValue<T>();
        object GetValue(object target);
        object GetValue();
        void SetValue(object target, object value);
        void SetValue(object value);
    }
}
