using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public interface ILocalDeclarationStatement :
        IStatement
    {
        /// <summary>
        /// Returns the <see cref="ILocalMember"/> declared by the 
        /// <see cref="ILocalDeclarationStatement"/>.
        /// </summary>
        ILocalMember DeclaredLocal { get; }
    }
}
