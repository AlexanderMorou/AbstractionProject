using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.TypeSystems.Intermediate
{
    public class ImmutableTypeBase<TTypeIdentifier, TType> :
        TypeBase<TTypeIdentifier, TType>,
        IImmutableType
        where TTypeIdentifier :
            ITypeUniqueIdentifier
        where TType :
            class,
            IType<TTypeIdentifier, TType>
    {
        private IImmutableType declaringType;
        private AccessLevelModifiers accessModifiers;
        private IImmutableAssembly assembly;
        private IType[] implementedInterfaces;
        private ILockedTypeCollection _implementedInterfaces;
        protected override IType OnGetDeclaringType()
        {
            return this.declaringType;
        }

        protected override bool CanCacheImplementsList
        {
            get { return true; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            if (this._implementedInterfaces == null)
                this._implementedInterfaces = new LockedTypeCollection(this.implementedInterfaces);
            return this._implementedInterfaces;
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            throw new NotImplementedException();
        }

        protected override INamespaceDeclaration OnGetNamespace()
        {
            throw new NotImplementedException();
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return this.accessModifiers;
        }

        protected override IAssembly OnGetAssembly()
        {
            return this.Assembly;
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            throw new NotImplementedException();
        }

        protected override string OnGetNamespaceName()
        {
            throw new NotImplementedException();
        }

        protected override IType BaseTypeImpl
        {
            get { throw new NotImplementedException(); }
        }

        protected override ICustomAttributeCollection InitializeCustomAttributes()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { throw new NotImplementedException(); }
        }

        protected override string OnGetName()
        {
            throw new NotImplementedException();
        }
    }
}
