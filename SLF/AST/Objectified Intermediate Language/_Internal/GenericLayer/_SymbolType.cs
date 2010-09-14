using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _SymbolType :
        _GenericTypeBase<ISymbolType>,
        ISymbolType,
        IExpression
    {
        public _SymbolType(ISymbolType original, ITypeCollectionBase genericParameters)
            : base(original, genericParameters)
        {
        }
        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Other; }
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            return LockedFullMembersBase.Empty;
        }

        #region IExpression Members

        ExpressionKind IExpression.Type
        {
            get { return ExpressionKinds.TypeReference; }
        }

        #endregion

        #region IExpression Members


        public void Visit(IExpressionVisitor visitor)
        {
            //ToDo: Fix.
            throw new NotSupportedException();
        }

        #endregion
    }
}
