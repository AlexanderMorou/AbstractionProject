using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private ICustomAttributeDefinitionCollectionSeries customAttributes;
        private ICustomAttributeCollection customAttributesBack;
        /// <summary>
        /// Creates a new <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/>
        /// which contains the <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/></param>
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
                if (value is IArrayType)
                {
                    var arrayValue = value as IArrayType;
                    var lowerBoundsTargetType = typeof(LowerBoundTargetAttribute).GetTypeReference();
                    if (!arrayValue.IsVectorArray)
                    {
                        if (this.CustomAttributes.Contains(lowerBoundsTargetType))
                        {
                            var customAttribute = this.CustomAttributes[lowerBoundsTargetType];
                            customAttribute.Parameters.Clear();
                            foreach (var element in arrayValue.LowerBounds)
                                customAttribute.Parameters.Add(element);
                        }
                        else
                        {
                            var customAttribute = new CustomAttributeDefinition.ParameterValueCollection(lowerBoundsTargetType);
                            foreach (var element in arrayValue.LowerBounds)
                                customAttribute.Add(element);
                            this.CustomAttributes.Add(customAttribute);
                        }
                    }
                    else if (this.CustomAttributes.Contains(lowerBoundsTargetType))
                        this.CustomAttributes.Remove(this.CustomAttributes[lowerBoundsTargetType]);
                }
                else
                {
                    var lowerBoundsTargetType = typeof(LowerBoundTargetAttribute).GetTypeReference();
                    if (this.CustomAttributes.Contains(lowerBoundsTargetType))
                        this.CustomAttributes.Remove(this.CustomAttributes[lowerBoundsTargetType]);
                }
                this.parameterType = value;
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

        /// <summary>
        /// Initializes the <see cref="CustomAttributeDefinitionCollectionSeries"/> which
        /// denotes the groups of attributes defined on
        /// the <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.
        /// </summary>
        /// <returns>A new <see cref="CustomAttributeDefinitionCollectionSeries"/>
        /// instance which refers to the parameters defined on the 
        /// <see cref="IntermediateParameterMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</returns>
        protected virtual CustomAttributeDefinitionCollectionSeries InitializeCustomAttributes()
        {
            return new CustomAttributeDefinitionCollectionSeries(this);
        }

        #region ICustomAttributedDeclaration Members

        ICustomAttributeCollection ICustomAttributedDeclaration.CustomAttributes
        {
            get
            {
                if (this.customAttributesBack == null)
                    this.customAttributesBack = ((CustomAttributeDefinitionCollectionSeries)(this.CustomAttributes)).GetWrapper();
                return this.customAttributesBack;
            }
        }

        public bool IsDefined(IType attributeType)
        {
            return this.CustomAttributes.Contains(attributeType);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} {1}", this.ParameterType.BuildTypeName(true, true), base.UniqueIdentifier);;
        }

        #region IIntermediateParameterMember Members

        IParameterReferenceExpression IIntermediateParameterMember.GetReference()
        {
            return this.GetReference();
        }

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
