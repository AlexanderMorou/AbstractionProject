using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// documentation comment for a member may contain
    /// a series of parameters, has a return value,
    /// and a possible sequence of exceptions to describe.
    /// </summary>
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
