﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides a base class for intermediate field members.
    /// </summary>
    /// <typeparam name="TField">The type of field in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateField">The type of field in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TFieldParent">The type which owns the fields
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateFieldParent">The type which owns the fields
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateFieldMemberBase<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> :
        IntermediateMemberBase<IGeneralMemberUniqueIdentifier, TFieldParent, TIntermediateFieldParent>,
        IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
        where TField :
            IFieldMember<TField, TFieldParent>
        where TIntermediateField :
            TField,
            IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
        where TIntermediateFieldParent :
            TFieldParent,
            IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
    {
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;

        protected IntermediateFieldMemberBase(string name, TIntermediateFieldParent parent)
            : base(parent)
        {
            base.AssignName(name);
        }

        #region IIntermediateFieldMember Members

        public virtual IType FieldType { get; set; }

        IFieldReferenceExpression IIntermediateFieldMember.GetReference(IMemberParentReferenceExpression source)
        {
            return ((TField)(object)this).GetFieldReference<TField, TFieldParent>(source);
        }

        #endregion

        #region IIntermediateFieldMember<TField,TIntermediateField,TFieldParent,TIntermediateFieldParent> Members

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/>
        /// used to initialize the 
        /// <see cref="IntermediateFieldMemberBase{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/>.
        /// </summary>
        public IExpression InitializationExpression { get; set; }

        #endregion

        public override void Visit(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get {
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