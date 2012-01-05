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
    internal abstract partial class IndexerSignatureMemberBase<TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent> :
        SignatureMemberBase<IGeneralSignatureMemberUniqueIdentifier, TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, TIndexerParent>,
        IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
        where TIndexerMethod :
            TMethod,
            IPropertySignatureMethodMember
        where TMethod :
            IMethodSignatureMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodSignatureParent<TMethod, TMethodParent>
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
        /// Creates a new <see cref="IndexerSignatureMemberBase{TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIndexerParent"/> which
        /// contains the <see cref="IndexerSignatureMemberBase{TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent}"/></param>
        public IndexerSignatureMemberBase(TIndexerParent parent)
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
        /// <see cref="IndexerSignatureMemberBase{TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent}"/> 
        /// is defined as.
        /// </summary>
        /// <returns>An <see cref="IType"/> that relates to the
        /// type associated to the 
        /// <see cref="IndexerSignatureMemberBase{TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent}"/>.</returns>
        protected abstract IType OnGetPropertyType();

        /// <summary>
        /// Returns whether the <see cref="IndexerSignatureMemberBase{TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent}"/>
        /// can cache the <see cref="PropertyType"/>.
        /// </summary>
        protected abstract bool CanCachePropertyType { get; }

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