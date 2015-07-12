using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class LocalDeclarationsStatement :
        StatementBase, 
        ILocalDeclarationsStatement
    {
        internal LocalDeclarationsStatement(IEnumerable<ILocalMember> declaredLocals, IStatementParent parent)
            : base(parent)
        {
            this.DeclaredLocals = new ControlledCollection<ILocalMember>(declaredLocals.ToList());
        }

        #region ILocalDeclarationsStatement Members

        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/> of <see cref="ILocalMember"/> elements declared by the 
        /// <see cref="LocalDeclarationsStatement"/>.
        /// </summary>
        public IControlledCollection<ILocalMember> DeclaredLocals { get; private set; }

        #endregion

        public override void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        public override TResult Visit<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            if (this.DeclaredLocals == null || this.DeclaredLocals.Count == 0)
                return string.Empty;
            return string.Join(", ", this.DeclaredLocals);
        }
    }
}
