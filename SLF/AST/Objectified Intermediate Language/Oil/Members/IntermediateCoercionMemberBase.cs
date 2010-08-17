using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public class IntermediateCoercionMemberBase<TCoercion, TIntermediateCoercion, TType, TIntermediateType> :
        IntermediateMemberBase<TType, TIntermediateType>,
        IIntermediateCoercionMember<TCoercion, TIntermediateCoercion, TType, TIntermediateType>
        where TCoercion :
            ICoercionMember<TCoercion, TType>
        where TIntermediateCoercion :
            TCoercion,
            IIntermediateCoercionMember<TCoercion, TIntermediateCoercion, TType, TIntermediateType>
        where TType :
            ICoercibleType<TCoercion, TType>
        where TIntermediateType :
            TType,
            IIntermediateCoercibleType<TCoercion, TIntermediateCoercion, TType, TIntermediateType>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateType"/> which contains the 
        /// <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>.</param>
        public IntermediateCoercionMemberBase(TIntermediateType parent)
            : base(parent)
        {
        }
        #region IIntermediateCoercionMember Members

        IIntermediateCoercibleType IIntermediateCoercionMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IIntermediateScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get
            {
                return AccessLevelModifiers.Public;
            }
            set
            {
                throw new NotSupportedException("Altering the access level of expression coercion members is not supported.");
            }
        }

        #endregion

        #region ICoercionMember Members

        ICoercibleType ICoercionMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion
    }
}
