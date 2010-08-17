using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using System.Runtime.Serialization;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;

 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base class for delegate types.
    /// </summary>
    public partial class IntermediateDelegateType :
        IntermediateGenericTypeBase<IDelegateType, IIntermediateDelegateType>,
        IIntermediateDelegateType
    {
        private IntermediateFullMemberDictionary members;
        private ParameterDictionary parameters;
        private IType returnType;
        private static ILockedTypeCollection implementedInterfaces;

        internal protected IntermediateDelegateType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
            this.returnType = IntermediateGateway.CommonlyUsedTypeReferences.Void;
        }

        private static ILockedTypeCollection _ImplementedInterfaces
        {
            get
            {
                if (implementedInterfaces == null)
                    implementedInterfaces = new LockedTypeCollection(new IType[] { typeof(ICloneable).GetTypeReference(), typeof(ISerializable).GetTypeReference() });
                return implementedInterfaces;
            }
        }

        public ParameterDictionary Parameters
        {
            get
            {
                this.CheckParameters();
                return this.parameters;
            }
        }

        #region IIntermediateDelegateType Members

        /// <summary>
        /// Returns the <see cref="IType"/> that the <see cref="IDelegateType"/> returns.
        /// </summary>
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

        IIntermediateDelegateTypeParameterDictionary IIntermediateDelegateType.Parameters
        {
            get
            {
                return this.Parameters;
            }
        }

        #endregion

        #region IIntermediateParameterParent<IDelegateType,IIntermediateDelegateType,IDelegateTypeParameterMember,IIntermediateDelegateTypeParameterMember> Members

        IIntermediateParameterMemberDictionary<IDelegateType, IIntermediateDelegateType, IDelegateTypeParameterMember, IIntermediateDelegateTypeParameterMember> IIntermediateParameterParent<IDelegateType,IIntermediateDelegateType,IDelegateTypeParameterMember,IIntermediateDelegateTypeParameterMember>.Parameters
        {
            get
            {
                return this.Parameters;
            }
        }

        #endregion

        #region IIntermediateParameterParent Members

        IIntermediateParameterMemberDictionary IIntermediateParameterParent.Parameters
        {
            get {
                return this.Parameters;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateDelegateType"/>'s last parameter
        /// is a parameter array.
        /// </summary>
        public bool LastIsParams
        {
            get
            {
                if (this.parameters == null || 
                    this.parameters.Count == 0)
                    return false;
                return this.Parameters.Values[this.Parameters.Count - 1].IsDefined(IntermediateGateway.CommonlyUsedTypeReferences.ParameterArrayAttribute);
            }
            set
            {
                if (value == this.LastIsParams)
                    return;
                this.CheckParameters();
                var lastParameter = this.Parameters.Values[this.Parameters.Count - 1];
                if (value)
                    lastParameter.CustomAttributes.Add(new CustomAttributeDefinition.ParameterValueCollection(IntermediateGateway.CommonlyUsedTypeReferences.ParameterArrayAttribute));
                else
                {
                    var customAttrDef = lastParameter.CustomAttributes[IntermediateGateway.CommonlyUsedTypeReferences.ParameterArrayAttribute];
                    lastParameter.CustomAttributes.Remove(customAttrDef);
                }
            }
        }

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return this.Parameters; }
        }

        #endregion

        #region IParameterParent<IDelegateType,IDelegateTypeParameterMember> Members

        IParameterMemberDictionary<IDelegateType, IDelegateTypeParameterMember> IParameterParent<IDelegateType, IDelegateTypeParameterMember>.Parameters
        {
            get { return this.Parameters; }
        }

        #endregion

        #region IDelegateType Members

        IType IDelegateType.ReturnType
        {
            get { return this.ReturnType; }
        }

        IDelegateTypeParameterDictionary IDelegateType.Parameters
        {
            get { return this.Parameters; }
        }

        #endregion

        private void CheckParameters()
        {
            if (this.parameters == null)
                this.parameters = new ParameterDictionary(this);
        }


        protected override IDelegateType OnMakeGenericType(ITypeCollectionBase typeParameters)
        {
            return new _DelegateTypeBase(this, typeParameters);
        }

        protected override IIntermediateFullMemberDictionary OnGetIntermediateMembers()
        {
            return this._Members;
        }

        protected override bool Equals(IDelegateType other)
        {
            return object.ReferenceEquals(other, this);
        }

        protected override TypeKind TypeImpl
        {
            get {
                return TypeKind.Delegate;
            }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return _ImplementedInterfaces;
        }

        public override bool IsGenericType
        {
            get {
                if (!base.TypeParametersInitialized)
                    return false;
                return this.TypeParameters.Count > 0; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            ICompiledType compiledType = other as ICompiledType;
            if (compiledType != null &&
                (
                    compiledType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.MulticastDelegate) |
                    compiledType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Delegate) |
                    compiledType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Object)
                ))
                return true;
            return false;
        }

        protected override IType BaseTypeImpl
        {
            get { return typeof(MulticastDelegate).GetTypeReference(); }
        }

        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                Check_Members();
                return this.members;
            }
        }

        private void Check_Members()
        {
            if (this.members == null)
                this.members = new IntermediateFullMemberDictionary();
        }

        protected override void Disposed(bool dispose)
        {
            base.Disposed(dispose);
        }
    }
}
