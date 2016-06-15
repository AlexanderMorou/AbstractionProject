using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides a base implementation for a method signature member which focuses solely on being a 
    /// signature member; that is: no code.
    /// </summary>
    /// <typeparam name="TSignature">The <see cref="IMethodSignatureMember{TSignature, TSignatureParent}"/>
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The <see cref="IIntermediateMethodSignatureMember{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TParent">The <see cref="IMethodSignatureParent{TSignature, TParent}"/> in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The <see cref="IIntermediateMethodSignatureParent{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract partial class IntermediateMethodSignatureMemberBase<TSignature, TIntermediateSignature, TParent, TIntermediateParent> :
        IntermediateMethodSignatureMemberBase<IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
        IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
        _IGenericMethodSignatureRegistrar
        where TSignature :
            class,
            IMethodSignatureMember<TSignature, TParent>
        where TIntermediateSignature :
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignature
        where TParent :
            IMethodSignatureParent<TSignature, TParent>
        where TIntermediateParent :
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TParent
    {
        private GenericMethodSignatureCache<IMethodSignatureParameterMember<TSignature, TParent>, TSignature, TParent> genericMethodCache;

        internal bool _AreParametersInitialized { get { return base.AreParametersInitialized; } }

        /// <summary>
        /// Creates a new <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which owns the 
        /// <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</param>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/>
        /// which contains the <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// and contains context relative to disambiguating type identities.</param>
        protected IntermediateMethodSignatureMemberBase(TIntermediateParent parent, IIntermediateAssembly assembly)
            : base(parent, assembly)
        {
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// with the <paramref name="name"/>, <paramref name="parent"/> and <paramref name="assembly"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value which denotes
        /// the name of the <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which owns the 
        /// <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</param>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/>
        /// which contains the <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// and contains context relative to disambiguating type identities.</param>
        protected IntermediateMethodSignatureMemberBase(string name, TIntermediateParent parent, IIntermediateAssembly assembly)
            : base(name, parent, assembly)
        {
        }

        public override void Accept(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        private void CheckGenericMethodCache()
        {
            lock (this.SyncObject)
                if (this.genericMethodCache == null)
                    this.genericMethodCache = new GenericMethodSignatureCache<IMethodSignatureParameterMember<TSignature, TParent>, TSignature, TParent>();
        }

        #region _IGenericMethodSignatureRegistrar Members

        public void RegisterGenericChild(IMethodSignatureParent parent, IMethodSignatureMember genericChild)
        {
            this.CheckGenericMethodCache();
            this.genericMethodCache.RegisterGenericChild(parent, genericChild);
        }

        public void UnregisterGenericChild(IMethodSignatureParent parent)
        {
            this.CheckGenericMethodCache();
            this.genericMethodCache.UnregisterGenericChild(parent);
        }

        public void RegisterGenericMethod(IMethodSignatureMember targetSignature, IControlledTypeCollection typeParameters)
        {
            this.CheckGenericMethodCache();
            this.genericMethodCache.RegisterGenericMethod(targetSignature, typeParameters);
        }

        public void UnregisterGenericMethod(IControlledTypeCollection typeParameters)
        {
            this.CheckGenericMethodCache();
            this.genericMethodCache.UnregisterGenericMethod(typeParameters);
        }

        #endregion

        internal override void OnRearranged(GenericParameterMovedEventArgs e)
        {
            this.CheckGenericMethodCache();
            this.genericMethodCache.PositionalShift(e.From, e.To);
        }

        protected override bool ContainsGenericMethod(IControlledTypeCollection typeParameters, ref TSignature r)
        {
            this.CheckGenericMethodCache();
            return this.genericMethodCache.ContainsGenericMethod(typeParameters, ref r);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                lock (this.SyncObject)
                {
                    if (this.genericMethodCache != null)
                        this.genericMethodCache.Dispose();
                    this.genericMethodCache = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
    }

    /// <summary>
    /// Provides a base intermediate method signature member whose code dependency is undecided.
    /// </summary>
    /// <typeparam name="TSignatureParameter">The type of the parameter for the <typeparamref name="TSignature"/>
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParameter">The type of parameter for the
    /// <typeparamref name="TIntermediateSignature"/> in the intermediate abstract syntax
    /// tree.</typeparam>
    /// <typeparam name="TSignature">The type of method signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The type of method signature in the intermediate abstract
    /// syntax tree.</typeparam>
    /// <typeparam name="TParent">The type which owns the <typeparamref name="TSignature"/> instances
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type which owns the <typeparamref name="TIntermediateSignature"/>
    /// instances in the intermediate abstract syntax tree.</typeparam>
    public abstract partial class IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> :
        IntermediateSignatureMemberBase<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
        IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
        _IIntermediateMethodSignatureMember
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignatureParameter
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignature
        where TParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
        where TIntermediateParent :
            IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
            TParent
    {
        /// <summary>
        /// Data member for <see cref="UniqueIdentifier"/>.
        /// </summary>
        private IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier;
        /// <summary>
        /// Data member for <see cref="GenericParameters"/>.
        /// </summary>
        private GenericParameterCollection genericParameters;
        /// <summary>
        /// Data member for <see cref="ReturnType"/>.
        /// </summary>
        private IType returnType;
        private IMetadataDefinitionCollection returnTypeMetadata;
        private IMetadataCollection _returnTypeMetadata;
        /// <summary>
        /// Data member for <see cref="TypeParameters"/>.
        /// </summary>
        private TypeParameterDictionary typeParameters;
        private MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes signatureTypes;
        private IMetadataDefinitionCollection metadata;
        private IMetadataCollection _metadata;
        /// <summary>
        /// Creates a new <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which
        /// owns the <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</param>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/>
        /// which contains the <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// and contains context relative to disambiguating type identities.</param>
        protected IntermediateMethodSignatureMemberBase(TIntermediateParent parent, IIntermediateAssembly assembly)
            : base(parent, assembly)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the unique name of the 
        /// <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which
        /// owns the <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</param>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/>
        /// which contains the <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// and contains context relative to disambiguating type identities.</param>
        protected IntermediateMethodSignatureMemberBase(string name, TIntermediateParent parent, IIntermediateAssembly assembly)
            : base(name, parent, assembly)
        {
        }


        internal IGeneralGenericSignatureMemberUniqueIdentifier _UniqueIdentifier
        {
            get
            {
                return this.uniqueIdentifier;
            }
        }

        #region IIntermediateGenericParameterParent<IMethodSignatureGenericTypeParameterMember,IIntermediateMethodSignatureGenericTypeParameterMember,IMethodSignatureMember,IIntermediateMethodSignatureMember> Members

        IIntermediateGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IIntermediateMethodSignatureGenericTypeParameterMember, IMethodSignatureMember, IIntermediateMethodSignatureMember> IIntermediateGenericParameterParent<IMethodSignatureGenericTypeParameterMember, IIntermediateMethodSignatureGenericTypeParameterMember, IMethodSignatureMember, IIntermediateMethodSignatureMember>.TypeParameters
        {
            get { return this.TypeParameters; }
        }

        #endregion

        #region IGenericParamParent<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember> Members

        IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>.TypeParameters
        {
            get { return this.TypeParameters; }
        }

        #endregion

        #region IGenericParamParent Members

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { return this.TypeParameters; }
        }

        #endregion

        #region IIntermediateGenericParameterParent Members

        IIntermediateGenericParameterDictionary IIntermediateGenericParameterParent.TypeParameters
        {
            get { return this.TypeParameters; }
        }

        #endregion

        #region IMethodSignatureMember Members

        /// <summary>
        /// Returns whether the current <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// is a generic construct.
        /// </summary>
        public bool IsGenericConstruct
        {
            get
            {
                if (this.typeParameters == null)
                    return false;
                return this.TypeParameters.Count > 0;
            }
        }

        /// <summary>
        /// Returns whether the current <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// is the generic definition of a generic series.
        /// </summary>
        public bool IsGenericDefinition
        {
            get
            {
                return this.IsGenericConstruct;
            }
        }

        IType IMethodSignatureMember.ReturnType
        {
            get { return this.ReturnType; }
        }

        IMethodSignatureMember IMethodSignatureMember.GetGenericDefinition()
        {
            return this.GetGenericDefinition();
        }

        /// <summary>
        /// Returns the <see cref="ILockedTypeCollection"/> which
        /// contains the type-parameters associated to the
        /// current <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.
        /// </summary>
        public ILockedTypeCollection GenericParameters
        {
            get
            {
                if (this.genericParameters == null)
                    this.genericParameters = new GenericParameterCollection(this);
                return this.genericParameters;
            }
        }

        #endregion

        #region IMethodSignatureMember<TSignatureParameter,TSignature,TParent> Members

        /// <summary>
        /// Obtains a variant of the current 
        /// <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// with the current generic type-parameters 
        /// replaced with the elements within 
        /// <paramref name="genericReplacements"/>.
        /// </summary>
        /// <param name="genericReplacements">
        /// The <see cref="IType"/> series to replace the 
        /// original generic parameters with.</param>
        /// <returns>A <typeparamref name="TSignature"/> 
        /// as a variant of the current <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// with the current generic type-parameters 
        /// replaced with the elements within 
        /// <paramref name="genericReplacements"/>.
        /// </returns>
        public TSignature MakeGenericClosure(IControlledTypeCollection genericReplacements)
        {
            if (!this.IsGenericConstruct)
                throw new InvalidOperationException("not a generic method");
            TSignature k = null;
            IGenericType genericParent = null;
            if (this.Parent is IGenericType && (genericParent = ((IGenericType)(this.Parent))).IsGenericConstruct &&
                genericParent.IsGenericDefinition)
                throw new InvalidOperationException("Cannot obtain a closed generic method whose containing type is an open generic definition.");
            if (this.ContainsGenericMethod(genericReplacements, ref k))
                return k;
            /* *
             * _IGenericMethodRegistrar and _IGenericMethodSignatureRegistrar handle cache.
             * */
            var tK = this.OnMakeGenericMethod(genericReplacements);
            //CliCommon.VerifyTypeParameters<TSignatureParameter, TSignature, TParent>(this, genericReplacements);
            return tK;
        }

        /// <summary>
        /// Returns a <typeparamref name="TSignature"/> instance that is the 
        /// closed generic form of the current <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/> 
        /// collection used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TSignature"/> instance with 
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// <seealso cref="IsGenericConstruct"/> 
        /// of the current instance is false.</exception>
        public TSignature MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters.ToLockedCollection());
        }

        protected abstract bool ContainsGenericMethod(IControlledTypeCollection typeParameters, ref TSignature r);

        /// <summary>
        /// Returns the original generic form of the current
        /// <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/> 
        /// generic variant.
        /// </summary>
        /// <returns>A <typeparamref name="TSignature"/> 
        /// which denotes the original generic variant
        /// of the current <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// is not a generic closure of the current generic method.</exception>
        protected abstract TSignature OnMakeGenericMethod(IControlledTypeCollection genericReplacements);

        /// <summary>
        /// Returns the original generic form of the current
        /// <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/> 
        /// generic variant.
        /// </summary>
        /// <returns>A <typeparamref name="TSignature"/> 
        /// which denotes the original generic variant of the current
        /// <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</returns>
        /// <exception cref="System.InvalidOperationException">thrown always.</exception>
        public TSignature GetGenericDefinition()
        {
            throw new InvalidOperationException();
        }

        #endregion

        /// <summary>
        /// Returns the <see cref="TypeParameterDictionary"/> containing the 
        /// type-parameters for the current <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.
        /// </summary>
        public TypeParameterDictionary TypeParameters
        {
            get
            {
                if (this.typeParameters == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                    else
                        this.typeParameters = this.InitializeTypeParameters();
                return this.typeParameters;
            }
        }

        /// <summary>
        /// Initializes the type-parameters of the <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.
        /// </summary>
        /// <returns>A <see cref="TypeParameterDictionary"/> which contains the type-parameters
        /// for the <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</returns>
        protected virtual TypeParameterDictionary InitializeTypeParameters()
        {
            return new TypeParameterDictionary(this);
        }


        /// <summary>
        /// Returns/sets the <see cref="IType"/> which is returned from the <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// on invocation.
        /// </summary>
        public IType ReturnType
        {
            get
            {
                return OnGetReturnType();
            }
            set
            {
                OnSetReturnType(value);
            }
        }

        protected virtual void OnSetReturnType(IType value)
        {
            this.returnType = value;
        }

        protected virtual IType OnGetReturnType()
        {
            return this.returnType;
        }

        /// <summary>
        /// Disposes the <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// </summary>
        /// <param name="disposing">whether to dispose the managed 
        /// resources as well as the unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (this.typeParameters != null)
                        this.typeParameters.Dispose();
                    this.returnType = null;
                    this.returnTypeMetadata = null;
                    this.uniqueIdentifier = null;
                    if (this.metadata != null)
                    {
                        this.metadata.Dispose();
                        this.metadata = null;
                    }
                    if (this._metadata != null)
                    {
                        this._metadata.Dispose();
                        this._metadata = null;
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Returns whether the type-parameters of the <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// have been initialized or not.
        /// </summary>
        protected bool AreTypeParametersInitialized
        {
            get
            {
                return this.typeParameters != null;
            }
        }
        
        public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (uniqueIdentifier == null)
                    {
                        var service = this.Assembly.GetUniqueIdentifierService();
                        var iim = this as IIntermediateMethodMember;
                        if (iim != null)
                            this.uniqueIdentifier = service.HandlesMethodMemberIdentifier
                                                    ? service.GetIdentifier(iim)
                                                    : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(iim);
                        else
                            this.uniqueIdentifier = service.HandlesMethodSignatureMemberIdentifier
                                                    ? service.GetIdentifier(this)
                                                    : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(this);
                    }
                    return this.uniqueIdentifier;
                }
            }
        }

        internal void _OnIdentifierChanged(IGeneralGenericSignatureMemberUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            this.OnIdentifierChanged(oldIdentifier, cause);
        }

        protected virtual void OnTypeParameterAdded(IIntermediateMethodSignatureGenericTypeParameterMember arg1)
        {
            var _typeParameterAdded = this._TypeParameterAdded;
            if (_typeParameterAdded != null)
                _typeParameterAdded(this, new EventArgsR1<IIntermediateGenericParameter>(arg1));
            var typeParameterAdded = this.TypeParameterAdded;
            if (typeParameterAdded != null)
                typeParameterAdded(this, new EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>(arg1));
            if (this.uniqueIdentifier != null)
                this.OnIdentifierChanged(this.UniqueIdentifier, DeclarationChangeCause.IdentityCardinality);
        }

        protected virtual void OnTypeParameterRemoved(IIntermediateMethodSignatureGenericTypeParameterMember arg1)
        {
            var _typeParameterRemoved = this._TypeParameterRemoved;
            if (_typeParameterRemoved != null)
                _typeParameterRemoved(this, new EventArgsR1<IIntermediateGenericParameter>(arg1));
            var typeParameterRemoved = this.TypeParameterRemoved;
            if (typeParameterRemoved != null)
                typeParameterRemoved(this, new EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>(arg1));
            if (this.uniqueIdentifier != null)
                this.OnIdentifierChanged(this.UniqueIdentifier, DeclarationChangeCause.IdentityCardinality);
        }

        #region IIntermediateGenericParameterParent<IMethodSignatureGenericTypeParameterMember,IIntermediateMethodSignatureGenericTypeParameterMember,IMethodSignatureMember,IIntermediateMethodSignatureMember> Members

        public event EventHandler<EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>> TypeParameterAdded;

        public event EventHandler<EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>> TypeParameterRemoved;

        #endregion

        #region IIntermediateGenericType Members
        private event EventHandler<EventArgsR1<IIntermediateGenericParameter>> _TypeParameterAdded;
        private event EventHandler<EventArgsR1<IIntermediateGenericParameter>> _TypeParameterRemoved;
        private string languageSpecificQualifier;
        event EventHandler<EventArgsR1<IIntermediateGenericParameter>> IIntermediateGenericParameterParent.TypeParameterAdded
        {
            add { _TypeParameterAdded += value; }
            remove { _TypeParameterAdded -= value; }
        }

        event EventHandler<EventArgsR1<IIntermediateGenericParameter>> IIntermediateGenericParameterParent.TypeParameterRemoved
        {
            add { _TypeParameterRemoved += value; }
            remove { _TypeParameterRemoved -= value; }
        }

        #endregion

        private MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes GetSignatureTypes()
        {
            if (signatureTypes == null)
                if (this.IsDisposed)
                    throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                else
                    this.signatureTypes = new MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes((TIntermediateSignature)(object)this);
            return this.signatureTypes;
        }

        #region IIntermediateMethodSignatureMember<TSignatureParameter,TIntermediateSignatureParameter,TSignature,TIntermediateSignature,TParent,TIntermediateParent> Members

        public IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetReference(IMemberParentReferenceExpression source)
        {
            if (this is IIntermediateInstanceMember)
            {
                if (source == null)
                    source = new AutoContextMemberSource((IIntermediateInstanceMember)this);
            }
            else if (source == null)
                throw new ArgumentNullException("source");
            return new MethodReferenceStub<TSignatureParameter, TSignature, TParent>(source, (TSignature)(object)this, this.GetSignatureTypes).GetPointer();
        }

        public IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetReference(IMemberParentReferenceExpression source, IEnumerable<IType> typeParameters)
        {
            return this.GetReference(source, typeParameters.ToArray());
        }

        public IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetReference(IMemberParentReferenceExpression source, params IType[] typeParameters)
        {

            if (!this.IsGenericConstruct)
                throw new InvalidOperationException("Not valid on a non-generic method.");
            if (this is IIntermediateInstanceMember)
            {
                if (source == null)
                    source = new AutoContextMemberSource((IIntermediateInstanceMember)this);
            }
            else if (source == null)
                throw new ArgumentNullException("source");
            if (typeParameters.Length != this.TypeParameters.Count)
                throw new ArgumentException("typeParameters");
            return new MethodReferenceStub<TSignatureParameter, TSignature, TParent>(source, (TIntermediateSignature)(object)this, typeParameters.ToCollection(), this.GetSignatureTypes).GetPointer();
        }

        #endregion

        #region IIntermediateMethodSignatureMember Members

        IMethodPointerReferenceExpression IIntermediateMethodSignatureMember.GetReference(IMemberParentReferenceExpression source)
        {
            return GetReference(source);
        }

        IMethodPointerReferenceExpression IIntermediateMethodSignatureMember.GetReference(IMemberParentReferenceExpression source, IEnumerable<IType> typeParameters)
        {
            return GetReference(source, typeParameters);
        }

        IMethodPointerReferenceExpression IIntermediateMethodSignatureMember.GetReference(IMemberParentReferenceExpression source, params IType[] typeParameters)
        {
            return GetReference(source, typeParameters);
        }

        #endregion

        #region IGenericParamParent<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember> Members


        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember>.MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember>.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters.ToLockedCollection());
        }

        #endregion

        #region IGenericParamParent Members


        IGenericParamParent IGenericParamParent.MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters.ToLockedCollection());
        }

        public bool ContainsGenericParameters
        {
            get { return this.ContainsGenericParameters(); }
        }

        #endregion

    
        #region IIntermediateMethodSignatureMember Members

        public IMetadataDefinitionCollection ReturnTypeMetadata
        {
            get
            {
                if (this.returnTypeMetadata == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                    else
                        this.returnTypeMetadata = new MetadataDefinitionCollection(this, this.Assembly);
                return this.returnTypeMetadata;
            }
        }

        #endregion

        #region IMethodSignatureMember Members


        IMetadataCollection IMethodSignatureMember.ReturnTypeMetadata
        {
            get {
                if (this._returnTypeMetadata == null)
                    this._returnTypeMetadata = ((MetadataDefinitionCollection)this.ReturnTypeMetadata).GetWrapper();
                return this._returnTypeMetadata;
            }
        }

        #endregion

        IMetadataCollection IMetadataEntity.Metadata
        {
            get
            {
                if (this._metadata != null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                    else
                        this._metadata = ((MetadataDefinitionCollection)(this.Metadata)).GetWrapper();
                return this._metadata;
            }
        }

        public IMetadataDefinitionCollection Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadata == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                        else
                            this.metadata = new MetadataDefinitionCollection(this, this.Assembly);
                    return this.metadata;
                }
            }
        }

        public bool IsDefined(IType attributeType)
        {
            return this.StandardIsDefined(attributeType);
        }

        protected override void ClearIdentifier()
        {
            lock (this.SyncObject)
                this.uniqueIdentifier = null;
        }

        internal abstract void OnRearranged(GenericParameterMovedEventArgs e);
        public string ReturnsText { get; set; }


        bool _IIntermediateMethodSignatureMember.AreTypeParametersInitialized
        {
            get { return AreTypeParametersInitialized; }
        }

        bool _IIntermediateMethodSignatureMember.AreParametersInitialized
        {
            get { return AreParametersInitialized; }
        }

        public bool HasTypeParameters { get { return this.typeParameters != null && this.typeParameters.Count > 0; } }
    }

    internal interface _IIntermediateMethodSignatureMember
    {
        bool AreTypeParametersInitialized { get; }
        bool AreParametersInitialized { get; }
        bool IsDisposed { get; }
    }
}
