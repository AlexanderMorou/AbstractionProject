using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public partial class LocalMember :
        IntermediateMemberBase<IBlockStatementParent, IBlockStatementParent>,
        ILocalMember
    {
        private ILocalReferenceExpression singletonLocal = null;
        private ILocalDeclarationStatement singletonDeclaration = null;
        public LocalMember(string name, IBlockStatementParent parent, LocalTypingKind typingMethod)
            : base(name, parent)
        {
            this.TypingMethod = typingMethod;
            this.AutoDeclare = true;
        }

        public IExpression InitializationExpression { get; set; }

        #region ILocalMember Members

        public virtual ILocalReferenceExpression GetReference()
        {
            if (singletonLocal == null)
                singletonLocal = new ReferenceExpression(this);
            return this.singletonLocal;
        }

        /// <summary>
        /// Returns/sets whether the <see cref="LocalMember"/> should 
        /// be automatically declared.
        /// </summary>
        public bool AutoDeclare { get; set; }

        public LocalTypingKind TypingMethod { get; private set; }

        public ILocalDeclarationStatement GetDeclarationStatement()
        {
            if (this.singletonDeclaration == null)
                this.singletonDeclaration = new LocalDeclarationStatement(this, this.Parent);
            return this.singletonDeclaration;
        }

        #endregion

        public override string ToString()
        {
            switch (this.TypingMethod)
            {
                case LocalTypingKind.Dynamic:
                    return string.Format("dynamic {0} = {1}", this.Name == null ? "?" : this.Name, this.InitializationExpression);
                case LocalTypingKind.Implicit:
                    return string.Format("var {0} = {1}", this.Name == null ? "?" : this.Name, this.InitializationExpression);
                case LocalTypingKind.Explicit:
                    return string.Format("? {0} = {1}", this.Name == null ? "?" : this.Name, this.InitializationExpression);
            }
            return this.Name;
        }

        public override void Visit(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
