﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    public partial class LinqRangeVariable :
        IntermediateDeclarationBase<IGeneralMemberUniqueIdentifier>,
        ILinqRangeVariable
    {
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;
        private Reference _reference;
        public LinqRangeVariable(ILinqClause parent, string name)
        {
            this.Parent = parent;
            this.Name = name;
        }

        #region ILinqRangeVariable Members

        public ILinqClause Parent { get; private set; }

        /// <summary>
        /// Obtains a <see cref="ILinqRangeVariableReference"/> which
        /// refers back to the <see cref="LinqRangeVariable"/>
        /// as an expression.
        /// </summary>
        /// <returns>A <see cref="ILinqRangeVariableReference"/>
        /// associated to the current <see cref="LinqRangeVariable"/>.</returns>
        public ILinqRangeVariableReference GetReference()
        {
            if (this._reference == null)
                this._reference = new Reference(this);
            return this._reference;
        }

        #endregion 

        #region IIntermediateMember Members

        IIntermediateMemberParent IIntermediateMember.Parent
        {
            get { return this.Parent; }
        }

        public void Visit(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion

        #region IMember Members

        IMemberParent IMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion


        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get 
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = AstIdentifier.Member(this.Name);
                return this.uniqueIdentifier;
            }
        }
    }
}