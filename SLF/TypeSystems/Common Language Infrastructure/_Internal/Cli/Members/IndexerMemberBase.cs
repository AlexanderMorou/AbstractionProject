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
    internal abstract partial class IndexerMemberBase<TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent> :
        SignatureMemberBase<IGeneralSignatureMemberUniqueIdentifier, TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>, TIndexerParent>,
        IIndexerMember
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
        where TIndexerMethod :
            TMethod,
            IPropertyMethodMember
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        private IType propertyType;
        /// <summary>
        /// Data member for <see cref="GetMethod"/>.
        /// </summary>
        private TIndexerMethod getMethod;
        /// <summary>
        /// Data member for <see cref="SetMethod"/>.
        /// </summary>
        private TIndexerMethod setMethod;
        private IModifiersAndAttributesMetadata metadata;

        /// <summary>
        /// Creates a new <see cref="IndexerMemberBase{TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIndexerParent"/> which
        /// contains the <see cref="IndexerMemberBase{TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent}"/></param>
        public IndexerMemberBase(TIndexerParent parent)
            : base(parent)
        {
        }

        #region IPropertySignatureMember Members

        public IType PropertyType
        {
            get
            {
                if (this.CanCachePropertyType)
                {
                    if (this.propertyType == null)
                        propertyType = this.OnGetPropertyType();
                    return this.propertyType;
                }
                else
                    return this.OnGetPropertyType();
            }
        }

        public bool CanRead
        {
            get { return this.GetMethod != null; }
        }

        public bool CanWrite
        {
            get { return this.SetMethod != null; }
        }

        public TIndexerMethod GetMethod
        {
            get
            {
                if (this.getMethod == null)
                    this.getMethod = this.OnGetMethod(PropertyMethodType.GetMethod);
                return this.getMethod;
            }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        IPropertyMethodMember IPropertyMember.GetMethod
        {
            get { return this.GetMethod; }
        }
        IPropertyMethodMember IIndexerMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        public TIndexerMethod SetMethod
        {
            get
            {
                if (this.setMethod == null)
                    this.setMethod = this.OnGetMethod(PropertyMethodType.SetMethod);
                return this.setMethod;
            }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        IPropertyMethodMember IPropertyMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        IPropertyMethodMember IIndexerMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        #endregion

        /// <summary>
        /// Obtains the <typeparamref name="TIndexerMethod"/> for the <paramref name="methodType"/> provided.
        /// </summary>
        /// <param name="methodType">The type of property method to obtain the property wrapper 
        /// method for.</param>
        /// <returns>An instance of <typeparamref name="TIndexerMethod"/>
        /// if successful.</returns>
        protected abstract TIndexerMethod OnGetMethod(PropertyMethodType methodType);

        /// <summary>
        /// Obtains the <see cref="IType"/> that the
        /// <see cref="IndexerMemberBase{TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent}"/> 
        /// is defined as.
        /// </summary>
        /// <returns>An <see cref="IType"/> that relates to the
        /// type associated to the 
        /// <see cref="IndexerMemberBase{TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent}"/>.</returns>
        protected abstract IType OnGetPropertyType();

        /// <summary>
        /// Returns whether the <see cref="IndexerMemberBase{TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent}"/>
        /// can cache the <see cref="PropertyType"/>.
        /// </summary>
        protected abstract bool CanCachePropertyType { get; }

        #region IScopedDeclaration Members

        /// <summary>
        /// Returns the access level of the <see cref="MethodMemberBase{TMethod, TMethodParent}"/>.
        /// </summary>
        public AccessLevelModifiers AccessLevel
        {
            get { return this.AccessLevelImpl; }
        }

        protected abstract AccessLevelModifiers AccessLevelImpl { get; }

        #endregion

        #region IExtendedInstanceMember Members

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> is
        /// static.
        /// </summary>
        public abstract bool IsStatic { get; }

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> is
        /// virtual (can be overridden).
        /// </summary>
        public abstract bool IsVirtual { get; }

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// hides the original definition completely.
        /// </summary>
        public abstract bool IsHideBySignature { get; }

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// finalizes the member removing the overrideable 
        /// status.
        /// </summary>
        public abstract bool IsFinal { get; }

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> 
        /// is an override of a virtual member.
        /// </summary>
        public abstract bool IsOverride { get; }

        /// <summary>
        /// Returns whether the
        /// <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// is abstract (must be implemented, or is
        /// not yet implemented).
        /// </summary>
        public abstract bool IsAbstract { get; }

        /// <summary>
        /// Returns the <see cref="ExtendedInstanceMemberFlags"/> that determine how the
        /// <see cref="IExtendedInstanceMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        public ExtendedInstanceMemberFlags InstanceFlags
        {
            get
            {
                ExtendedInstanceMemberFlags imfs = ExtendedInstanceMemberFlags.None;
                if (this.IsStatic)
                    imfs |= ExtendedInstanceMemberFlags.Static;
                if (this.IsVirtual)
                    imfs |= ExtendedInstanceMemberFlags.Virtual;
                if (this.IsOverride)
                    imfs |= ExtendedInstanceMemberFlags.Override;
                if (this.IsFinal)
                    imfs |= ExtendedInstanceMemberFlags.Final;
                if (this.IsHideBySignature)
                    imfs |= ExtendedInstanceMemberFlags.HideBySignature;
                if (this.IsAbstract)
                    imfs |= ExtendedInstanceMemberFlags.Abstract;
                return imfs;
            }
        }

        #endregion

        #region IInstanceMember Members

        InstanceMemberFlags IInstanceMember.InstanceFlags
        {
            get { return (InstanceMemberFlags)this.InstanceFlags; }
        }

        #endregion

        public IModifiersAndAttributesMetadata Metadata
        {
            get
            {
                if (this.metadata == null)
                    this.metadata = this.InitializeMetadata();
                return this.metadata;
            }
        }
        protected abstract IModifiersAndAttributesMetadata InitializeMetadata();
    }
}
