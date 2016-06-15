using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
//using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private string _languageSpecificQualifier;
        private MalleableExpressionCollection<IDecorationExpression> _decorations;
        public LinqRangeVariable(ILinqRangeVariableParent parent, string name)
        {
            this.Parent = parent;
            this.AssignName(name);
        }

        #region ILinqRangeVariable Members

        public ILinqRangeVariableParent Parent { get; private set; }

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

        public void Accept(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        public TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
        #endregion

        #region IMember Members

        IMemberParent IMember.Parent
        {
            get { return this.Parent; }
        }

        IMemberUniqueIdentifier IMember.UniqueIdentifier
        {
            get { return this.UniqueIdentifier; }
        }

        #endregion

        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get 
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = TypeSystemIdentifiers.GetMemberIdentifier(this.Name);
                return this.uniqueIdentifier;
            }
        }

        protected override void ClearIdentifier()
        {
            lock (base.SyncObject) 
                this.uniqueIdentifier = null;
        }
        public string SummaryText { get; set; }
        public string RemarksText { get; set; }

        public string UserSpecificQualifier
        {
            get { return this._languageSpecificQualifier; }
            set 
            {
                this._languageSpecificQualifier = value;
                this.OnIdentifierChanged(this.UniqueIdentifier, DeclarationChangeCause.Name);
            }
        }

        public bool HasDecorations { get { return this._decorations != null && this._decorations.Count > 0; } }

        public IMalleableExpressionCollection<IDecorationExpression> Decorations
        {
            get
            {
                return this._decorations ?? (this._decorations = new MalleableExpressionCollection<IDecorationExpression>());
            }
        }

    }
}
