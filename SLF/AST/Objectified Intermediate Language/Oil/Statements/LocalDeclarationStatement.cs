using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class LocalDeclarationStatement :
        StatementBase, 
        ILocalDeclarationStatement
    {
        internal LocalDeclarationStatement(ILocalMember declaredLocal, IStatementParent parent)
            : base(parent)
        {
            this.DeclaredLocal = declaredLocal;
        }

        #region ILocalDeclarationStatement Members

        /// <summary>
        /// Returns the <see cref="ILocalMember"/> declared by the 
        /// <see cref="LocalDeclarationStatement"/>.
        /// </summary>
        public ILocalMember DeclaredLocal { get; private set; }

        #endregion

        public override void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }
    }
}
