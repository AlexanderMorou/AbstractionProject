using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Statements;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public partial class TypedLocalMember :
        LocalMember,
        ITypedLocalMember
    {
        /// <summary>
        /// Creates a new <see cref="TypedLocalMember"/>
        /// with the <paramref name="name"/>, <paramref name="parent"/> and <paramref name="localType"/>
        /// provided.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="localType"></param>
        public TypedLocalMember(string name, IBlockStatementParent parent, IType localType)
            : base(name, parent, LocalTypingKind.Explicit)
        {
            this.LocalType = localType;
        }
        #region ITypedLocalMember Members

        public IType LocalType { get; set; }

        #endregion

        public override Expressions.ILocalReferenceExpression GetReference()
        {
            return new ReferenceExpression(this);
        }
    }
}
