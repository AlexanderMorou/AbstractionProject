using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    /// <summary>
    /// Provides a root implementation of <see cref="IParameterMember{TParent}"/>.
    /// </summary>
    /// <typeparam name="TParent">The type of parent that contains the <see cref="ParameterMemberBase{TParent}"/> instances
    /// in the current implementation.</typeparam>
    internal abstract class ParameterMemberBase<TParent> :
        MemberBase<IGeneralMemberUniqueIdentifier, TParent>,
        IParameterMember<TParent>
        where TParent :
            IParameterParent
    {
        private ICustomAttributeCollection customAttributes;
        /// <summary>
        /// Creates a new <see cref="ParameterMemberBase{TParent}"/> with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The parent of the <see cref="ParameterMemberBase{TParent}"/>.</param>
        protected ParameterMemberBase(TParent parent)
            : base(parent)
        {
        }

        #region IParameterMember Members

        IParameterParent IParameterMember.Parent
        {
            get { return this.Parent; }
        }

        public IType ParameterType { get { return this.ParameterTypeImpl; } }

        protected abstract IType ParameterTypeImpl { get; }

        public abstract ParameterDirection Direction { get; }

        #endregion

        #region ICustomAttributedDeclaration Members

        public ICustomAttributeCollection CustomAttributes
        {
            get
            {
                if (this.customAttributes == null)
                    this.customAttributes = this.InitializeCustomAttributes();
                return this.customAttributes;
            }
        }

        public bool IsDefined(IType attributeType)
        {
            return this.CustomAttributes.Contains(attributeType);
        }

        #endregion

        protected abstract ICustomAttributeCollection InitializeCustomAttributes();

    }
}
