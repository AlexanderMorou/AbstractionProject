using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides a base class for parameters.
    /// </summary>
    public class IntermediateParameterMemberBase<TParent, TIntermediateParent, TParameter, TIntermediateParameter> :
        IntermediateMemberBase<IGeneralMemberUniqueIdentifier, TParent, TIntermediateParent>,
        IIntermediateParameterMember<TParent, TIntermediateParent>
        where TParent :
            IParameterParent<TParent, TParameter>
        where TIntermediateParent :
            TParent,
            IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>
        where TParameter :
            IParameterMember<TParent>
        where TIntermediateParameter :
            TParameter,
            IIntermediateParameterMember<TParent, TIntermediateParent>
    {
        private IType parameterType;
        private ParameterDirection direction;
        private IMetadataDefinitionCollection metadata;

        private IMetadataCollection metadataBack;
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;
        internal ITypeIdentityManager identityManager;


        /// <summary>
        /// Creates a new <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/>
        /// which contains the <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/></param>
        /// <param name="identityManager">The <see cref="ITypeIdentityManager"/>
        /// which is responsible for maintaining type identity within the current type
        /// model.</param>
        public IntermediateParameterMemberBase(TIntermediateParent parent, ITypeIdentityManager identityManager)
            : base(parent)
        {
            this.identityManager = identityManager;
        }

        #region IIntermediateParameterMember Members

        IIntermediateParameterParent IIntermediateParameterMember.Parent
        {
            get { return base.Parent; }
        }

        /// <summary>
        /// Returns/sets the type that the <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>
        /// is defined as.
        /// </summary>
        public virtual IType ParameterType
        {
            get
            {
                return this.parameterType;
            }
            set
            {
                if (value == parameterType)
                    return;
                var originalType = this.parameterType;
                this.parameterType = value;
                this.OnParameterTypeChanged(originalType, value);
            }
        }

        /// <summary>
        /// Returns/sets the direction the parameter is coerced.
        /// </summary>
        public virtual ParameterDirection Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
            }
        }

        #endregion

        #region IParameterMember Members

        IParameterParent IParameterMember.Parent
        {
            get { return base.Parent; }
        }

        #endregion

        #region IIntermediateMetadataEntity Members

        public IMetadataDefinitionCollection Metadata
        {
            get
            {
                if (this.metadata == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.metadata = this.InitializeCustomAttributes();
                return this.metadata;
            }
        }

        #endregion

        /// <summary>
        /// Initializes the <see cref="MetadataDefinitionCollection"/> which
        /// denotes the groups of attributes defined on
        /// the <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.
        /// </summary>
        /// <returns>A new <see cref="MetadataDefinitionCollection"/>
        /// instance which refers to the parameters defined on the 
        /// <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</returns>
        protected virtual MetadataDefinitionCollection InitializeCustomAttributes()
        {
            return new MetadataDefinitionCollection(this, this.identityManager);
        }

        protected override void OnIdentifierChanged(IGeneralMemberUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            if (this.uniqueIdentifier != null)
                this.uniqueIdentifier = null;
            base.OnIdentifierChanged(oldIdentifier, cause);
        }

        #region IMetadataEntity Members

        IMetadataCollection IMetadataEntity.Metadata
        {
            get
            {
                if (this.metadataBack == null)
                    this.metadataBack = ((MetadataDefinitionCollection)(this.Metadata)).GetWrapper();
                return this.metadataBack;
            }
        }

        public bool IsDefined(IType attributeType)
        {
            return this.StandardIsDefined(attributeType);
        }

        #endregion

        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = AstIdentifier.GetMemberIdentifier(this.Name);
                return this.uniqueIdentifier;
            }
        }

        #region IIntermediateParameterMember Members

        public event EventHandler<EventArgsR1R2<IType, IType>> ParameterTypeChanged;

        protected virtual void OnParameterTypeChanged(IType originalType, IType newType)
        {
            if (originalType == null || originalType == newType)
                return;
            if (newType == null)
                throw new ArgumentNullException("newType");
            var parameterTypeChanged = this.ParameterTypeChanged;
            if (parameterTypeChanged != null)
                parameterTypeChanged(this, new EventArgsR1R2<IType, IType>(originalType, newType));
        }

        IParameterReferenceExpression IIntermediateParameterMember.GetReference()
        {
            return this.GetReference();
        }

        /// <summary>
        /// Obtains a <see cref="IParameterReferenceExpression{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/> for the
        /// current <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.
        /// </summary>
        /// <returns>A <see cref="IParameterReferenceExpression{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/> for the
        /// current <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</returns>
        public IParameterReferenceExpression<TParent, TIntermediateParent, TParameter, TIntermediateParameter> GetReference()
        {
            return new ParameterReferenceExpression<TParent, TIntermediateParent, TParameter, TIntermediateParameter>(((TIntermediateParameter)(object)(this)));
        }

        #endregion

        public override void Visit(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

    }

}
