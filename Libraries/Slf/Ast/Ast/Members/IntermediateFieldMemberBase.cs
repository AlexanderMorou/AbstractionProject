using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private IType fieldType;
        private IMetadataDefinitionCollection metadata;
        private IMetadataCollection metadataBack;


        protected IntermediateFieldMemberBase(string name, TIntermediateFieldParent parent)
            : base(parent)
        {
            base.AssignName(name);
        }

        #region IIntermediateFieldMember Members

        public virtual IType FieldType
        {
            get
            {
                return this.fieldType;
            }
            set
            {
                this.fieldType = value;
            }
        }

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

        public override void Accept(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get 
            {
                lock (this.SyncObject)
                {
                    if (uniqueIdentifier == null)
                    {
                        var service = this.Assembly.GetUniqueIdentifierService();
                        this.uniqueIdentifier = service.HandlesMemberIdentifier
                                                ? service.GetIdentifier(this)
                                                : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(this);
                    }
                    return this.uniqueIdentifier;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (this.uniqueIdentifier != null)
                    this.uniqueIdentifier = null;
                this.fieldType = null;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }


        protected override void ClearIdentifier()
        {
            lock (this.SyncObject)
                this.uniqueIdentifier = null;
        }

        IMetadataCollection IMetadataEntity.Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadataBack != null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                        else
                            this.metadataBack = ((MetadataDefinitionCollection)(this.Metadata)).GetWrapper();
                    return this.metadataBack;
                }
            }
        }

        protected abstract IIntermediateIdentityManager IdentityManager { get; }

        public IMetadataDefinitionCollection Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadata == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.metadata = new MetadataDefinitionCollection(this, this.Assembly);
                    return this.metadata;
                }
            }
        }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which the 
        /// <see cref="IntermediateFieldMemberBase{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/>
        /// is contained.
        /// </summary>
        protected abstract IIntermediateAssembly Assembly { get; }

        public bool IsDefined(IType metadatumType)
        {
            return this.Metadata.Contains(metadatumType);
        }

        /// <summary>
        /// Returns the readonly status of the field.
        /// </summary>
        /// <returns>true, if the field is writeable only when the parent is being initialized; false, otherwise.</returns>
        protected abstract bool OnGetReadonly();
        /// <summary>
        /// Returns the constant status of the field.
        /// </summary>
        /// <returns>true, if the field is a literal value which will be calculated at compile time.</returns>
        protected abstract bool OnGetConstant();

        #region IFieldMember Members

        public FieldMemberAttributes Attributes
        {
            get { return (this.ReadOnly ? FieldMemberAttributes.ReadOnly : FieldMemberAttributes.None) | (this.Constant ? FieldMemberAttributes.Constant : FieldMemberAttributes.None); }
        }

        /// <summary>
        /// Returns whether the <see cref="IntermediateFieldMemberBase{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/> is read-only.
        /// </summary>
        /// <remarks>Read-only fields can only be initialized during the 
        /// constructor phase of a type or instance.</remarks>
        public bool ReadOnly
        {
            get { return this.OnGetReadonly(); }
        }

        /// <summary>
        /// Returns whether the <see cref="IntermediateFieldMemberBase{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/> is a constant value.
        /// </summary>
        /// <remarks>Constant values are evaluated at compile-time and folded into
        /// a single value of the appropriate data-type.</remarks>
        public bool Constant { get { return this.OnGetConstant(); } }

        #endregion
    }
}
