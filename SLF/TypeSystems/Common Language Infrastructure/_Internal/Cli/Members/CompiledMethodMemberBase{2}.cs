using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.CompilerServices;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CompiledMethodMemberBase<TMethod, TMethodParent> :
        MethodMemberBase<TMethod, TMethodParent>,
        ICompiledMethodMember,
        _IGenericMethodRegistrar
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        private IDictionary<ITypeCollectionBase, IMethodMember> genericCache;
        private bool lastIsParams;
        int flagA;
        private IParameterMemberDictionary<TMethod, IMethodParameterMember<TMethod, TMethodParent>> parameters;
        /// <summary>
        /// Data member for <see cref="MemberInfo"/>.
        /// </summary>
        private MethodInfo memberInfo;
        /// <summary>
        /// Data member used to store the unique identifier of the <see cref="CompiledMethodMemberBase{TMethod, TMethodParent}"/>.
        /// </summary>
        private string uniqueIdentifier;

        /// <summary>
        /// Creates a new <see cref="CompiledMethodMemberBase{TMethod, TMethodParent}"/> with the
        /// <paramref name="parent"/> and <paramref name="memberInfo"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TMethodParent"/> to which the current
        /// <see cref="CompiledMethodMemberBase{TMethod, TMethodParent}"/> belongs.</param>
        /// <param name="memberInfo">The <see cref="MethodInfo"/> that is used to source the 
        /// <see cref="CompiledMethodMemberBase{TMethod, TMethodParent}"/>'s data.</param>
        public CompiledMethodMemberBase(MethodInfo memberInfo, TMethodParent parent)
            : base(parent)
        {
            this.flagA = 0;
            this.uniqueIdentifier = memberInfo.GetUniqueIdentifier();
            this.memberInfo = memberInfo;
            this.lastIsParams = memberInfo.LastParameterIsParams();
        }

        public override string UniqueIdentifier
        {
            get
            {
                if (flagA == 0)
                {
                    string uniqueIdentifier = this.uniqueIdentifier;
                    flagA = 1;
                    this.uniqueIdentifier = null;
                    return uniqueIdentifier;
                }
                else if (flagA == 1)
                {
                    flagA = 2;
                    this.uniqueIdentifier = this.GetUniqueIdentifier();
                }
                return this.uniqueIdentifier;
            }
        }

        #region ICompiledMethodMember Members

        /// <summary>
        /// Returns the <see cref="MethodInfo"/> associated to the <see cref="CompiledMethodMemberBase{TMethod, TMethodParent}"/>.
        /// </summary>
        public MethodInfo MemberInfo
        {
            get { return this.memberInfo; }
        }

        #endregion

        #region IScopedDeclaration Members

        /// <summary>
        /// Returns the access level of the <see cref="CompiledMethodMemberBase{TMethod, TMethodParent}"/>.
        /// </summary>
        protected override AccessLevelModifiers AccessLevelImpl
        {
            get { return this.MemberInfo.GetAccessModifiers(); }
        }

        #endregion

        #region ICompiledMember Members

        MemberInfo ICompiledMember.MemberInfo
        {
            get { return this.MemberInfo; }
        }

        #endregion

        /// <summary>
        /// Obtains the <see cref="DeclarationBase.Name"/> for the <see cref="CompiledMethodMemberBase{TMethod, TMethodParent}"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that contains the name of the <see cref="CompiledMethodMemberBase{TMethod, TMethodParent}"/>.</returns>
        protected override string OnGetName()
        {
            return this.MemberInfo.Name;
        }


        /// <summary>
        /// Initializes the <see cref="IParameterMemberDictionary{TSignature, TSignatureParameter}"/> used
        /// to store the <see cref="IMethodParameterMember{TMethod, TMethodParent}"/> instances.
        /// </summary>
        /// <returns>A new <see cref="IParameterMemberDictionary{TSignature, TSignatureParameter}"/> instance.</returns>
        protected override IParameterMemberDictionary<TMethod, IMethodParameterMember<TMethod, TMethodParent>> InitializeParameters()
        {
            return new ParametersDictionary(this, this.MemberInfo.GetParameters().OnAll(paramInfo => (IMethodParameterMember<TMethod, TMethodParent>)new ParameterMember(paramInfo, this)));
        }

        /// <summary>
        /// Initialies the <see cref="MethodSignatureMemberBase{TSignatureParameter, TGenericParameter, TSignature, TSignatureParent}.TypeParameters"/> property.
        /// </summary>
        /// <returns>A new <see cref="IGenericParameterDictionary{TParameter, TParent}"/> instance.</returns>
        protected override IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> InitializeTypeParameters()
        {
            return new LockedGenericParameters<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>(((TMethod)((object)(this))), this.MemberInfo.GetGenericArguments().OnAll(gParamType =>
                (IMethodSignatureGenericTypeParameterMember)new GenericParameterMember(((TMethod)((object)(this))), gParamType)));
        }


        /// <summary>
        /// Returns whether the <see cref="CompiledMethodMemberBase{TMethod, TMethodParent}"/> is a generic method.
        /// </summary>
        public override bool IsGenericMethod
        {
            get {
                return this.MemberInfo.IsGenericMethod;
            }
        }

        protected override bool CanCacheReturn
        {
            get { return true; }
        }


        protected override IType OnGetReturnType()
        {
            IType returnType = this.MemberInfo.ReturnType.GetTypeReference();
            if (returnType.ElementClassification == TypeElementClassification.Array && 
                this.MemberInfo.ReturnTypeCustomAttributes.IsDefined(typeof(LowerBoundTargetAttribute), true))
            {
                var attr = this.MemberInfo.ReturnTypeCustomAttributes.GetCustomAttributes(typeof(LowerBoundTargetAttribute), true).Cast<LowerBoundTargetAttribute>().First();
                if (attr.IsNonstandardArray)
                    returnType = returnType.ElementType.MakeArray(attr.Bounds);
            }
            return returnType;
        }

        protected override ILockedTypeCollection OnGetGenericParameters()
        {
            return this.TypeParameters.Values.ToLockedCollection();
        }

        protected override bool CanCacheGenericParameters
        {
            get { return true; }
        }

        internal void AddImplementationTarget(IMethodSignatureMember member)
        {

        }

        protected override bool LastIsParamsImpl
        {
            get { return this.lastIsParams; }
        }

        #region _IGenericMethodRegistrar Members

        public void RegisterGenericMethod(IMethodMember targetSignature, ITypeCollectionBase typeParameters)
        {
            if (this.genericCache == null)
                this.genericCache = new Dictionary<ITypeCollectionBase, IMethodMember>();
            IMethodMember required = null;
            if (this.ContainsGenericMethod(typeParameters, ref required))
                return;
            genericCache.Add(typeParameters, targetSignature);
        }

        public void UnregisterGenericMethod(ITypeCollectionBase typeParameters)
        {
            if (this.genericCache == null)
                return;
            ITypeCollectionBase match = null;
            foreach (var itc in this.genericCache.Keys)
                if (itc.SequenceEqual(typeParameters))
                {
                    match = itc;
                    break;
                }
            //Nothing matched.
            if (match == null)
                return;
            genericCache.Remove(match);
            if (match is ILockedTypeCollection)
                ((ILockedTypeCollection)(match)).Dispose();
            else if (match is ITypeCollection)
                try
                {
                    ((ITypeCollection)(match)).Clear();
                }
                /* *
                 * Even being a type collection, it doesn't
                 * support modification, the proper response
                 * is not supported...?
                 * */
                catch (NotSupportedException)
                {
                }
        }

        #endregion

        private bool ContainsGenericMethod(ITypeCollectionBase typeParameters, ref IMethodMember r)
        {
            if (this.genericCache == null)
                return false;
            var fd = this.genericCache.Keys.FirstOrDefault(itc => itc.SequenceEqual(typeParameters));
            if (fd == null)
                return false;
            r = this.genericCache[fd];
            return true;
        }

        public override sealed TMethod MakeGenericMethod(ITypeCollection genericReplacements)
        {
            if (!this.IsGenericMethod)
                throw new InvalidOperationException("not a generic method");
            IMethodMember k = null;
            IGenericType genericParent = null;
            if (this.Parent is IGenericType && (genericParent = ((IGenericType)(this.Parent))).IsGenericType &&
                genericParent.IsGenericTypeDefinition)
                throw new InvalidOperationException("Cannot obtain a closed generic method whose containing type is an open generic definition.");
            if (this.ContainsGenericMethod(genericReplacements, ref k))
                return ((TMethod)(k));
            /* *
             * _IGenericMethodRegistrar handles cache.
             * */
            var tK = this.OnMakeGenericMethod(genericReplacements);
            CLIGateway.VerifyTypeParameters<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>(this, genericReplacements);
            return tK;
        }

        protected abstract TMethod OnMakeGenericMethod(ITypeCollectionBase genericReplacements);

        public override void Dispose()
        {
            try
            {
                if (this.genericCache != null)
                {
                    for (int i = 0; i < this.genericCache.Count; i++)
                    {
                        var first = this.genericCache.Keys.ElementAt(0);
                        this.genericCache[first].Dispose();
                        this.genericCache.Remove(first);
                    }
                }
            }
            finally
            {
                base.Dispose();
            }
        }


        protected sealed override TMethod OnGetGenericDefinition()
        {
            return default(TMethod);
        }
    }
}
