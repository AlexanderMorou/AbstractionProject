using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateDelegateType
    {
        /// <summary>
        /// Provides a parameter member for a delegate type.
        /// </summary>
        protected internal sealed class ParameterMember :
            IntermediateParameterMemberBase<IDelegateType, IIntermediateDelegateType, IDelegateTypeParameterMember, IIntermediateDelegateTypeParameterMember>,
            IIntermediateDelegateTypeParameterMember
        {
            internal ParameterMember(IntermediateDelegateType parent)
                : base(parent)
            {

            }

        }
    }
}
