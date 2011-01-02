using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
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
        /// Provides a parameter member dictionary for a delegate type.
        /// </summary>
        public sealed class ParameterDictionary :
            IntermediateParameterMemberDictionary<IDelegateType, IIntermediateDelegateType, IDelegateTypeParameterMember, IIntermediateDelegateTypeParameterMember>,
            IIntermediateDelegateTypeParameterDictionary
        {
            internal ParameterDictionary(IntermediateDelegateType parent)
                : base(parent)
            {
            }

            protected override IIntermediateDelegateTypeParameterMember GetNewParameter(string name, IType parameterType, ParameterDirection direction)
            {
                var result = new ParameterMember(this.Parent);
                result.ParameterType = parameterType;
                result.Direction = direction;
                result.Name = name;
                return result;
            }

            private new IntermediateDelegateType Parent
            {
                get
                {
                    return (IntermediateDelegateType)base.Parent;
                }
            }
        }
    }
}
