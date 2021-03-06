﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public interface IBoundMemberReference 
    {
        /// <summary>
        /// Returns the <see cref="IType"/> associated to the member.
        /// </summary>
#if DEBUG
        [VisitorImplementationIgnoreProperty]
#endif
        IType MemberType { get; }
        /// <summary>
        /// Returns the <see cref="IMember"/> associated to the
        /// typed member reference.
        /// </summary>
        IMember Member { get; }
    }
}
