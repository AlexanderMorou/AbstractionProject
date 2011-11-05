using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Provides a base implementation of <see cref="ICompiledDelegateType"/>.
    /// </summary>
    internal sealed partial class CompiledDelegateType :
        CompiledGenericTypeBase<IDelegateUniqueIdentifier, IDelegateType>,
        ICompiledDelegateType
    {
        private bool lastIsParams;
        private IDelegateTypeParameterDictionary parameters;
        /// <summary>
        /// Data member for <see cref="ReturnType"/>.
        /// </summary>
        private IType returnType;
        /// <summary>
        /// Creates a new <see cref="CompiledDelegateType"/> with the 
        /// <paramref name="underlyingSystemType"/> provided.
        /// </summary>
        /// <param name="underlyingSystemType">The <see cref="System.Type"/> from which the current
        /// <see cref="CompiledDelegateType"/> is based.</param>
        internal CompiledDelegateType(Type underlyingSystemType)
            : base(underlyingSystemType)
        {
            if (!(underlyingSystemType.IsSubclassOf(typeof(Delegate)) && underlyingSystemType != typeof(MulticastDelegate)))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.underlyingSystemType, ArgumentExceptionMessage.CompiledType_NotProperKind, "delegate");
        }
        #region IDelegateType Members

        public IDelegateTypeParameterDictionary Parameters
        {
            get
            {
                ParamsAndReturnCheck();
                return this.parameters;
            }
        }

        #endregion
        private void ParamsAndReturnCheck()
        {
            if (this.parameters == null)
                this.parameters = this.InitializeParametersAndReturn();
        }

        private IDelegateTypeParameterDictionary InitializeParametersAndReturn()
        {
            MethodInfo invokeMethod = this.UnderlyingSystemType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
            ParameterInfo[] parameters = invokeMethod.GetParameters();
            this.lastIsParams = invokeMethod.LastParameterIsParams();
            IDelegateTypeParameterMember[] idtpma = new IDelegateTypeParameterMember[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                idtpma[i] = new ParameterMember(parameters[i], this);
            returnType = invokeMethod.ReturnType.GetTypeReference();
            return new ParametersDictionary(this, idtpma);
        }

        #region IParameterParent<IDelegateType,IDelegateTypeParameterMember> Members

        IParameterMemberDictionary<IDelegateType, IDelegateTypeParameterMember> IParameterParent<IDelegateType, IDelegateTypeParameterMember>.Parameters
        {
            get { return (IParameterMemberDictionary<IDelegateType, IDelegateTypeParameterMember>)this.Parameters; }
        }

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return (IParameterMemberDictionary)this.Parameters; }
        }

        #endregion

        /// <summary>
        /// Creates a new generic <see cref="IDelegateType"/> from the current generic type definition
        /// </summary>
        /// <param name="typeParameters">The <see cref="ITypeCollection"/> of <see cref="IType"/> instances
        /// to replace the generic parameters contained within the current <see cref="CompiledDelegateType"/>.
        /// </param>
        /// <returns>A new <see cref="IDelegateType"/> as a closed generic type.</returns>
        protected override IDelegateType OnMakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return new _DelegateTypeBase(this, typeParameters);
        }

        #region IDelegateType Members

        public IType ReturnType
        {
            get {
                ParamsAndReturnCheck();
                return returnType;
            }
        }

        #endregion

        protected override IFullMemberDictionary OnGetMembers()
        {
            return null;
        }

        #region IParameterParent Members

        public bool LastIsParams
        {
            get {
                ParamsAndReturnCheck();
                return this.lastIsParams; }
        }

        #endregion


        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Delegate; }
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return EmptyIdentifiers; }
        }
    }
}
