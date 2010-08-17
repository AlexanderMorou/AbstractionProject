using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base class for parameters.
    /// </summary>
    public class IntermediateParameterMemberBase<TParent, TIntermediateParent, TParameter, TIntermediateParameter> :
        IntermediateMemberBase<TParent, TIntermediateParent>,
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
        private ICustomAttributeDefinitionCollectionSeries customAttributes;
        private ICustomAttributeCollection customAttributesBack;
        /// <summary>
        /// Creates a new <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/>
        /// which contains the <see cref="IntermediateParameterMemberBase"/></param>
        public IntermediateParameterMemberBase(TIntermediateParent parent)
            : base(parent)
        {
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
        public IType ParameterType
        {
            get
            {
                return this.parameterType;
            }
            set
            {
                this.parameterType = value;
            }
        }

        /// <summary>
        /// Returns/sets the direction the parameter is coerced.
        /// </summary>
        public ParameterDirection Direction
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

        #region IIntermediateCustomAttributedDeclaration Members

        public ICustomAttributeDefinitionCollectionSeries CustomAttributes
        {
            get
            {
                if (this.customAttributes == null)
                    this.customAttributes = this.InitializeCustomAttributes();
                return this.customAttributes;
            }
        }

        #endregion

        protected ICustomAttributeDefinitionCollectionSeries InitializeCustomAttributes()
        {
            return new CustomAttributeDefinitionCollectionSeries(this);
        }

        #region ICustomAttributedDeclaration Members

        ICustomAttributeCollection ICustomAttributedDeclaration.CustomAttributes
        {
            get {
                return ((CustomAttributeDefinitionCollectionSeries)(this.CustomAttributes)).GetWrapper();
            }
        }

        public bool IsDefined(IType attributeType)
        {
            return this.CustomAttributes.Contains(attributeType);
        }

        #endregion

        public override string UniqueIdentifier
        {
            get
            {
                return string.Format("{0} {1}", this.ParameterType.BuildTypeName(true, true), base.UniqueIdentifier);
            }
        }
    }

}
