﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    public partial class LocalMember :
        IntermediateMemberBase<IGeneralMemberUniqueIdentifier, IBlockStatementParent, IBlockStatementParent>,
        ILocalMember
    {
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;
        private IBoundLocalReferenceExpression singletonLocal = null;
        private ILocalDeclarationStatement singletonDeclaration = null;
        public LocalMember(string name, IBlockStatementParent parent, LocalTypingKind typingMethod)
            : base(name, parent)
        {
            this.TypingMethod = typingMethod;
            this.AutoDeclare = true;
        }

        public IExpression InitializationExpression { get; set; }

        #region ILocalMember Members

        public virtual IBoundLocalReferenceExpression GetReference()
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
            if (this.InitializationExpression != null)
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
            }
            else
                switch (this.TypingMethod)
                {
                    case LocalTypingKind.Dynamic:
                        return string.Format("dynamic {0}", this.Name == null ? "?" : this.Name);
                    case LocalTypingKind.Implicit:
                        return string.Format("var {0}", this.Name == null ? "?" : this.Name);
                    case LocalTypingKind.Explicit:
                        return string.Format("? {0}", this.Name == null ? "?" : this.Name);
                }
            return this.Name;
        }

        public override void Visit(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        /* *
         * On implicitly typed members, the inferred type will be associated through this field.
         * *
         * It can't be inferred on the fly because context information at the time of request
         * might not be sufficient.
         * *
         * Examples include symbol types being present within the initialization expression.
         * */
        internal IType InferredType { get; set; }

        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = AstIdentifier.Member(this.Name);
                return this.uniqueIdentifier;
            }
        }

        protected override void OnIdentifierChanged(IGeneralMemberUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            if (this.uniqueIdentifier != null)
                this.uniqueIdentifier = null;
            base.OnIdentifierChanged(oldIdentifier, cause);
        }
    }
}