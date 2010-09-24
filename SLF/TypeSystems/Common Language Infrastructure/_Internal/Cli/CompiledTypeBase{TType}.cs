using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
//Average number of combinations in my passwords: 47,672,401,706,823,533,450,263,330,816 (62 ^ 16)
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract class CompiledTypeBase<TType> :
        TypeBase<TType>,
        ICompiledType
        where TType :
            class,
            IType<TType>
    {
        #region CompiledTypeBase{TType} Data members
        private INamespaceDeclaration nameSpace;
        /// <summary>
        /// Data member for <see cref="_Members"/>.
        /// </summary>
        private LockedFullMembersBase _members;
        /// <summary>
        /// Data member for <see cref="DeclaringType"/>.
        /// </summary>
        private IType declaringType = null;
        /// <summary>
        /// Data member for <see cref="UnderlyingSystemType"/>.
        /// </summary>
        private Type underlyingSystemType;
        #endregion

        /// <summary>
        /// Creates a new <see cref="CompiledTypeBase{TType}"/> with the 
        /// <paramref name="underlyingSystemType"/> provided.
        /// </summary>
        /// <param name="underlyingSystemType">The <see cref="System.Type"/> from which the current
        /// <see cref="CompiledTypeBase{TType}"/> is based.</param>
        internal CompiledTypeBase(Type underlyingSystemType)
            : base()
        {
            this.underlyingSystemType = underlyingSystemType;
        }

        /// <summary>
        /// Returns the <see cref="IType"/> in which the current <see cref="IType"/> is declared.
        /// </summary>
        protected override IType OnGetDeclaringType()
        {
            if (this.UnderlyingSystemType.DeclaringType == null)
                return null;
            else
                this.declaringType = this.UnderlyingSystemType.DeclaringType.GetTypeReference();
            return this.declaringType;
        }

        public override sealed bool IsGenericType
        {
            get
            {
                return this.UnderlyingSystemType.IsGenericType;
            }
        }

        #region ICompiledType Members

        /// <summary>
        /// Returns the <see cref="System.Type"/> which the <see cref="CompiledTypeBase{TType}"/> refers to.
        /// </summary>
        public Type UnderlyingSystemType
        {
            get
            {
                return this.underlyingSystemType;
            }
        }

        #endregion

        protected override bool Equals(TType other)
        {
            if (other.GetType() == this.GetType())
                return this.UnderlyingSystemType != ((CompiledTypeBase<TType>)(object)(other)).UnderlyingSystemType;
            else
                return false;
        }

        /// <summary>
        /// Obtains the name of the <see cref="CompiledTypeBase{TType}"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> containing the name of the <see cref="CompiledTypeBase{TType}"/>.</returns>
        protected override string OnGetName()
        {
            if (this.UnderlyingSystemType.IsGenericType)
            {
                string n = this.UnderlyingSystemType.Name;
                if (n.Contains("`"))
                    n = n.Substring(0, n.LastIndexOf('`'));
                return n;
            }
            return this.UnderlyingSystemType.Name;
        }

        protected override INamespaceDeclaration OnGetNameSpace()
        {
            if (nameSpace == null)
            {
                var namespaceName = this.UnderlyingSystemType.Namespace;
                this.nameSpace = this.Assembly.Namespaces[namespaceName];
            }
            return nameSpace;
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            if (this.UnderlyingSystemType.IsPublic ||
                this.UnderlyingSystemType.IsNestedPublic)
                return AccessLevelModifiers.Public;
            else if (this.UnderlyingSystemType.IsNestedAssembly ||
                     this.UnderlyingSystemType.IsNotPublic)
                return AccessLevelModifiers.Internal;
            else if (this.UnderlyingSystemType.IsNestedPrivate)
                return AccessLevelModifiers.Private;
            else if (this.UnderlyingSystemType.IsNestedFamily)
                return AccessLevelModifiers.Protected;
            else if (this.UnderlyingSystemType.IsNestedFamORAssem)
                return AccessLevelModifiers.ProtectedInternal;
            else if (this.UnderlyingSystemType.IsNestedFamANDAssem)
                //Special case, not available in C# or VB.
                return AccessLevelModifiers.InternalProtected;
            return AccessLevelModifiers.PrivateScope;
        }

        /// <summary>
        /// Returns the base type of the current <see cref="CompiledTypeBase{TType}"/>.
        /// </summary>
        protected override IType BaseTypeImpl
        {
            get
            {
                if (this.UnderlyingSystemType.BaseType == null)
                    return null;
                return this.UnderlyingSystemType.BaseType.GetTypeReference();
            }
        }

        /// <summary>
        /// Returns a collection of <see cref="IType"/> instances that are implemented by the current
        /// <see cref="CompiledTypeBase{TType}"/>.
        /// </summary>
        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return this.UnderlyingSystemType.GetInterfaces().ToLockedCollection();
        }
        protected override bool CanCacheImplementsList
        {
            get { return true; }
        }

        protected override void Dispose(bool dispose)
        {
            this.RemoveFromCache();
            this.underlyingSystemType = null;
            this.declaringType = null;
            if (this._members != null)
            {
                this._members._Clear();
                this._members = null;
            }
            base.Dispose(dispose);
        }


        protected override IAssembly OnGetAssembly()
        {
            if (this.ElementClassification == TypeElementClassification.None)
            {
                return this.UnderlyingSystemType.Assembly.GetAssemblyReference();
            }
            else
                return this.ElementType.Assembly;
        }

        /// <summary>
        /// Instantiates the <see cref="Members"/> property.
        /// </summary>
        /// <returns>A new <see cref="IFullMemberDictionary"/> 
        /// instance that contains the members of 
        /// the <see cref="CompiledTypeBase{TType}"/>.</returns>
        protected override IFullMemberDictionary OnGetMembers()
        {
            return _Members;
        }

        internal LockedFullMembersBase _Members
        {
            get
            {
                if (this._members == null)
                    this._members = new LockedFullMembersBase();
                return this._members;
            }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return other.Equals(typeof(object).GetTypeReference());
        }

        protected override IArrayType OnMakeArray(int rank)
        {
            return new ArrayType(this, rank);
        }

        protected override IArrayType OnMakeArray(params int[] lowerBounds)
        {
            return new ArrayType(this, lowerBounds);
        }

        protected override IType OnMakeByReference()
        {
            return new ByRefType(this);
        }

        protected override IType OnMakePointer()
        {
            return new PointerType(this);
        }

        protected override IType OnMakeNullable()
        {
            return new NullableType(this);
        }

        /// <summary>
        /// Initializes the <see cref="CustomAttributes"/> for the current
        /// <see cref="CompiledTypeBase{TType}"/>.
        /// </summary>
        /// <returns>A <see cref="ICustomAttributeCollection"/> of
        /// attributes relative to the current compiled type's
        /// <see cref="UnderlyingSystemType"/>.</returns>
        protected override ICustomAttributeCollection InitializeCustomAttributes()
        {
            return new CompiledTypeCustomAttributeCollection(this);
        }

        protected override string OnGetNamespaceName()
        {
            return this.UnderlyingSystemType.Namespace;
        }
    }
}
