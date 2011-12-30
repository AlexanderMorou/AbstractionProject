using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    /// <summary>
    /// Provides a means to create a type-parameter which denotes the
    /// replacement form of the constraints from the original used to
    /// verify the type-parameters passed on a generic type instance.
    /// </summary>
    internal class GenericVerificationParameter :
        TypeBase<IGenericParameterUniqueIdentifier>,
        IGenericTestCaseParameter
    {
        private ITypeCollection newConstraints;
        private ILockedTypeCollection lockedConstraints;
        /// <summary>
        /// Data member linking back to the original.
        /// </summary>
        private IGenericParameter original;
        internal GenericVerificationParameter(ITypeCollection newConstraints, IGenericParameter original)
        {
            this.original = original;
            this.newConstraints = newConstraints;
        }

        #region IGenericParameter Members

        public GenericTypeParameterSpecialConstraint SpecialConstraint
        {
            get { return original.SpecialConstraint; }
        }

        public int Position
        {
            get { return original.Position; }
        }

        public ILockedTypeCollection Constraints
        {
            get {
                this.CheckLockedConstraints();
                return this.lockedConstraints; }
        }

        public IGenericParamParent Parent
        {
            get { return original.Parent; }
        }

        public IGenericParameterConstructorMemberDictionary Constructors
        {
            get
            {
                /* *
                 * Presently due to limitations in building IL
                 * to instantiate an object, type-parameters
                 * aren't allowed within them, thereby eliminating
                 * the need to replace the type-parameters within
                 * the generic parameter constructors.
                 * */
                return original.Constructors;
            }
        }

        #endregion

        protected override IType OnGetDeclaringType()
        {
            return null;
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Class; }
        }

        protected override bool CanCacheImplementsList
        {
            get { return true; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            CheckLockedConstraints();
            return this.lockedConstraints;
        }

        private void CheckLockedConstraints()
        {
            if (this.lockedConstraints == null)
                this.lockedConstraints = this.newConstraints.ToLockedCollection();
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            return LockedFullMembersBase.Empty;
        }

        protected override INamespaceDeclaration OnGetNamespace()
        {
            return null;
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return original.AccessLevel;
        }

        protected override IAssembly OnGetAssembly()
        {
            return original.Assembly;
        }

        protected override IType BaseTypeImpl
        {
            get { return null; }
        }

        protected override string OnGetName()
        {
            return original.Name;
        }

        #region IGenericTestCaseParameter Members

        public IGenericParameter Original
        {
            get { return this.original; }
        }

        #endregion

        public override bool IsGenericConstruct
        {
            get { return false; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return false;
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
            if (this.SpecialConstraint == GenericTypeParameterSpecialConstraint.Struct)
                return new NullableType(this);
            else
                throw new InvalidOperationException();
        }
        public override void Dispose()
        {
            if (this.original != null)
                this.original = null;
            if (this.newConstraints != null)
            {
                newConstraints.Clear();
                this.newConstraints = null;
            }
            if (this.lockedConstraints != null)
            {
                this.lockedConstraints.Dispose();
                this.lockedConstraints = null;
            }
            base.Dispose();
        }

        #region IGenericParameter Members


        public IGenericParameterEventMemberDictionary Events
        {
            get { throw new NotImplementedException(); }
        }

        public IGenericParameterIndexerMemberDictionary Indexers
        {
            get { throw new NotImplementedException(); }
        }

        public IGenericParameterMethodMemberDictionary Methods
        {
            get { throw new NotImplementedException(); }
        }

        public IGenericParameterPropertyMemberDictionary Properties
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMethodSignatureParent Members

        IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IEventSignatureParent Members

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IIndexerSignatureParent Members

        IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IPropertySignatureParent Members

        IPropertySignatureMemberDictionary IPropertySignatureParent.Properties
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ICreatableParent Members

        IConstructorMemberDictionary ICreatableParent.Constructors
        {
            get { return (IConstructorMemberDictionary)this.Constructors; }
        }

        public IConstructorMember TypeInitializer
        {
            get { return original.TypeInitializer; }
        }

        #endregion

        protected override IGenericParameterUniqueIdentifier OnGetUniqueIdentifier()
        {
            return this.original.UniqueIdentifier;
        }

        protected override ICustomAttributeCollection InitializeCustomAttributes()
        {
            return this.Original.CustomAttributes;
        }

        public GenericParameterVariance Variance
        {
            get
            {
                return this.Original.Variance;
            }
        }

        protected override string OnGetNamespaceName()
        {
            return null;
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return EmptyIdentifiers; }
        }

        protected override bool IsAttributeInheritable(IType attribute)
        {
            if (attribute is ICompiledType)
            {
                var cType = attribute as ICompiledType;
                return CliAssist.GetAttributeUsage(cType.UnderlyingSystemType).AllowMultiple;
            }
            else
                return CliAssist.GetAttributeUsage(attribute).AllowMultiple;
        }
    }
}
