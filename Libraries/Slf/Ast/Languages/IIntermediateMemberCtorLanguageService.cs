using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a service which creates
    /// intermediate members of the given type (<typeparamref name="TMember"/>.)
    /// </summary>
    /// <typeparam name="TMember">The type of intermediate member in the
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateMemberCtorLanguageService<TMember> :
        ILanguageService
        where TMember :
            IIntermediateMember
    {
        /// <summary>
        /// Creates a new <typeparamref name="TMember"/> instance.
        /// </summary>
        /// <returns>A <typeparamref name="TMember"/> instance.</returns>
        TMember New();
    }
    /// <summary>
    /// Defines properties and methods for working with a service which creates
    /// named intermediate members of the given type (<typeparamref name="TMember"/>.)
    /// </summary>
    /// <typeparam name="TMember">The type of named intermediate member in the
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateNamedMemberCtorLanguageService<TMember> :
        IIntermediateMemberCtorLanguageService<TMember>
        where TMember :
            IIntermediateMember
    {
        /// <summary>
        /// Creates a new <typeparamref name="TMember"/> instance
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value denoting
        /// the name of the <typeparamref name="TMember"/> created.</param>
        /// <returns>A <typeparamref name="TMember"/> instance with
        /// the <paramref name="name"/> provided.</returns>
        TMember New(string name);
    }
}
