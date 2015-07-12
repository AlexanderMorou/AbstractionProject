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
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private ParameterCoercionDirection direction;
        private IMetadataDefinitionCollection metadata;

        private IMetadataCollection metadataBack;
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;
        internal IIntermediateAssembly assembly;


        /// <summary>
        /// Creates a new <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/>
        /// which contains the <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/></param>
        /// <param name="assembly">
        /// The <see cref="IIntermediateAssembly"/> which contains the
        /// entity which contains parameters.</param>
        public IntermediateParameterMemberBase(TIntermediateParent parent, IIntermediateAssembly assembly)
            : base(parent)
        {
            this.assembly = assembly;
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
                lock (this.SyncObject)
                    return this.parameterType;
            }
            set
            {
                IType originalType;
                lock (this.SyncObject)
                {
                    if (value == this.parameterType)
                        return;
                    originalType = this.parameterType;
                    this.parameterType = value;
                }
                this.OnParameterTypeChanged(originalType, value);
            }
        }

        /// <summary>
        /// Returns/sets the direction the parameter is coerced.
        /// </summary>
        public virtual ParameterCoercionDirection Direction
        {
            get
            {
                lock (this.SyncObject)
                    return this.direction;
            }
            set
            {
                lock (this.SyncObject)
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
                lock (this.SyncObject)
                {
                    if (this.metadata == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.metadata = this.InitializeCustomAttributes();
                    return this.metadata;
                }
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
            return new MetadataDefinitionCollection(this, this.assembly);
        }

        protected override void ClearIdentifier()
        {
            lock (this.SyncObject)
                if (this.uniqueIdentifier != null)
                    this.uniqueIdentifier = null;
        }


        #region IMetadataEntity Members

        IMetadataCollection IMetadataEntity.Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadataBack == null)
                        this.metadataBack = ((MetadataDefinitionCollection)(this.Metadata)).GetWrapper();
                    return this.metadataBack;
                }
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
                lock (this.SyncObject)
                {
                    if (this.uniqueIdentifier == null)
                        this.uniqueIdentifier = TypeSystemIdentifiers.GetMemberIdentifier(this.Name);
                    return this.uniqueIdentifier;
                }
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

            EventHandler<EventArgsR1R2<IType, IType>> parameterTypeChanged = this.ParameterTypeChanged;
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

        public override TResult Visit<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

    }

}
