using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
            return this.DeclaredLocal.ToString();
        }
    }
}
