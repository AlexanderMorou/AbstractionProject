using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Events;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
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
        IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignature, TParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TParent :
            IMethodSignatureParent<TSignature, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which owns the 
        /// <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</param>
        protected IntermediateMethodSignatureMemberBase(TIntermediateParent parent)
            : base(parent)
        {

        }

        public override void Visit(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
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
        IntermediateSignatureMemberBase<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
        IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TParent :
            ISignatureParent<TSignature, TSignatureParameter, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>
    {
        private GenericParameterCollection genericParameters;
        private IDictionary<ITypeCollectionBase, TSignature> genericCache;
        private IType returnType;
        private TypeParameterDictionary typeParameters;
        private MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes signatureTypes;
        /// <summary>
        /// Creates a new <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which
        /// owns the <see cref="IntermediateMethodSignatureMemberBase{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.</param>
        protected IntermediateMethodSignatureMemberBase(TIntermediateParent parent)
            : base(parent)
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
        protected IntermediateMethodSignatureMemberBase(string name, TIntermediateParent parent)
            : base(name, parent)
        {
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

        public bool IsGenericMethod
        {
            get
            {
                if (this.typeParameters == null)
                    return false;
                return this.TypeParameters.Count > 0;
            }
        }

        public bool IsGenericMethodDefinition
        {
            get
            {
                return this.IsGenericMethod;
            }
        }

        IType IMethodSignatureMember.ReturnType
        {
            get { return this.ReturnType; }
        }

        IMethodSignatureMember IMethodSignatureMember.MakeGenericMethod(ITypeCollection genericReplacements)
        {
            return this.MakeGenericMethod(genericReplacements);
        }

        IMethodSignatureMember IMethodSignatureMember.GetGenericDefinition()
        {
            return this.GetGenericDefinition();
        }

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

        public TSignature MakeGenericMethod(ITypeCollection genericReplacements)
        {
            if (!this.IsGenericMethod)
                throw new InvalidOperationException("not a generic method");
            TSignature k = null;
            IGenericType genericParent = null;
            if (this.Parent is IGenericType && (genericParent = ((IGenericType)(this.Parent))).IsGenericType &&
                genericParent.IsGenericTypeDefinition)
                throw new InvalidOperationException("Cannot obtain a closed generic method whose containing type is an open generic definition.");
            if (this.ContainsGenericMethod(genericReplacements, ref k))
                return k;
            /* *
             * _IGenericMethodRegistrar handles cache.
             * */
            var tK = this.OnMakeGenericMethod(genericReplacements);
            CLIGateway.VerifyTypeParameters<TSignatureParameter, TSignature, TParent>(this, genericReplacements);
            return tK;
        }

        private bool ContainsGenericMethod(ITypeCollectionBase typeParameters, ref TSignature r)
        {
            if (this.genericCache == null)
                return false;
            var fd = this.genericCache.Keys.FirstOrDefault(itc => itc.SequenceEqual(typeParameters));
            if (fd == null)
                return false;
            r = this.genericCache[fd];
            return true;
        }

        protected abstract TSignature OnMakeGenericMethod(ITypeCollection genericReplacements);

        public TSignature GetGenericDefinition()
        {
            return null;
        }

        #endregion

        public TypeParameterDictionary TypeParameters
        {
            get
            {
                if (this.typeParameters == null)
                    this.typeParameters = this.InitializeTypeParameters();
                return this.typeParameters;
            }
        }

        protected virtual TypeParameterDictionary InitializeTypeParameters()
        {
            return new TypeParameterDictionary(this);
        }

        public IType ReturnType
        {
            get
            {
                return this.returnType;
            }
            set
            {
                this.returnType = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (this.genericCache != null)
                    {
                        Parallel.ForEach(this.genericCache.Values.ToArray(), p =>
                            p.Dispose());
                        this.genericCache.Clear();
                        this.genericCache = null;
                    }
                    if (this.typeParameters != null)
                        this.typeParameters.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public override string UniqueIdentifier
        {
            get
            {
                return string.Format("{0} {1}{2}{3}", this.ReturnType.BuildTypeName(true), base.Name, this.UniqueIdentifier_TypeParameters(), base.UniqueIdentifier_Parameters());
            }
        }

        private string UniqueIdentifier_TypeParameters()
        {
            if (this.typeParameters == null || this.typeParameters.Count == 0)
                return string.Empty;
            else
            {
                return string.Format("`{0}", this.typeParameters.Count);
                //StringBuilder sb = new StringBuilder();
                //sb.Append("<");
                //bool first = true;
                //foreach (var typeParameter in this.TypeParameters.Values)
                //{
                //    if (first)
                //        first = false;
                //    else
                //        sb.Append(", ");
                //    sb.Append(typeParameter.UniqueIdentifier);
                //}
                //sb.Append(">");
                //return sb.ToString();
            }
        }

        protected virtual void OnTypeParameterAdded(IIntermediateMethodSignatureGenericTypeParameterMember arg1)
        {
            if (this._TypeParameterAdded != null)
                this._TypeParameterAdded(this, new EventArgsR1<IIntermediateGenericParameter>(arg1));
            if (this.TypeParameterAdded != null)
                this.TypeParameterAdded(this, new EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>(arg1));
        }

        protected virtual void OnTypeParameterRemoved(IIntermediateMethodSignatureGenericTypeParameterMember arg1)
        {
            if (this._TypeParameterRemoved != null)
                this._TypeParameterRemoved(this, new EventArgsR1<IIntermediateGenericParameter>(arg1));
            if (this.TypeParameterRemoved != null)
                this.TypeParameterRemoved(this, new EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>(arg1));
        }


        #region IIntermediateGenericParameterParent<IMethodSignatureGenericTypeParameterMember,IIntermediateMethodSignatureGenericTypeParameterMember,IMethodSignatureMember,IIntermediateMethodSignatureMember> Members

        public event EventHandler<EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>> TypeParameterAdded;

        public event EventHandler<EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>> TypeParameterRemoved;

        #endregion

        #region IIntermediateGenericType Members
        private event EventHandler<EventArgsR1<IIntermediateGenericParameter>> _TypeParameterAdded;
        private event EventHandler<EventArgsR1<IIntermediateGenericParameter>> _TypeParameterRemoved;
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

            if (!this.IsGenericMethod)
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
    }
}
