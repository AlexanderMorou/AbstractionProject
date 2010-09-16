using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;

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

        public override string ToString()
        {
            if (this.DeclaredLocal == null)
                return string.Empty;
            switch (DeclaredLocal.TypingMethod)
            {
                case LocalTypingKind.Dynamic:
                    return string.Format("dynamic {0};", this.DeclaredLocal.Name);
                case LocalTypingKind.Implicit:
                    return string.Format("var {0};", this.DeclaredLocal.Name);
                case LocalTypingKind.Explicit:
                    if (this.DeclaredLocal is ITypedLocalMember)
                        return string.Format("{0} {1};", ((ITypedLocalMember)(this.DeclaredLocal)).LocalType.BuildTypeName(), this.DeclaredLocal.Name);
                    else
                        return string.Format("? {0};", this.DeclaredLocal.Name);
                default:
                    return string.Empty;
            }
        }
    }
}
