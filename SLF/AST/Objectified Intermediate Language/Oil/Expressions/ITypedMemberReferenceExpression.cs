using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface ITypedMemberReferenceExpression :
        IMemberReferenceExpression
    {
        /// <summary>
        /// Returns the <see cref="IType"/> associated to the member.
        /// </summary>
        IType MemberType { get; }
        /// <summary>
        /// Returns the <see cref="IMember"/> associated to the
        /// typed member reference.
        /// </summary>
        IMember Member { get; }
    }
}
