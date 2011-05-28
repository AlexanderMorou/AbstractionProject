using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    public interface ISignatureMemberDocComment :
        ICodeMemberDocComment
    {
        /// <summary>
        /// Returns the <see cref="IDocCommentGroup{TItem, TSection}"/>
        /// associated to the parameters of the 
        /// <see cref="ISignatureMemberDocComment"/>.
        /// </summary>
        IDocCommentNamedGroup<IParameterMember, IDocCommentParameterSection> Parameters { get; }
        /// <summary>
        /// Returns the <see cref="IDocCommentSection"/>
        /// which denotes information about the return
        /// value of the method.
        /// </summary>
        IDocCommentSection Returns { get; }
    }
}
