﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil.Properties;
using AllenCopeland.Abstraction.Slf.Compilers;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class DynamicType :
        TypeBase<DynamicType>
    {
        internal static DynamicType SingleTon = new DynamicType();

        private DynamicType() { }

        protected override bool Equals(DynamicType other)
        {
            return object.ReferenceEquals(this, other);
        }

        protected override IType OnGetDeclaringType()
        {
            return null;
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Dynamic; }
        }

        protected override bool CanCacheImplementsList
        {
            get { return true; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            return LockedFullMembersBase.Empty;
        }

        protected override INamespaceDeclaration OnGetNameSpace()
        {
            return null;
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return AccessLevelModifiers.PrivateScope;
        }

        protected override IAssembly OnGetAssembly()
        {
            return null;
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
            return null;
        }

        protected override IType OnMakeNullable()
        {
            throw new NotSupportedException(CSharpCompilerMessages.FormatCS0453("dynamic", "T", "Nullable<T>"));
        }

        public override bool IsGenericConstruct
        {
            get { return false; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return false;
        }

        protected override string OnGetNamespaceName()
        {
            return null;
        }

        protected override IType BaseTypeImpl
        {
            get { return null; }
        }

        protected override ICustomAttributeCollection InitializeCustomAttributes()
        {
            return null;
        }

        public override IEnumerable<string> AggregateIdentifiers
        {
            get { return TypeBase.EmptyIdentifiers; }
        }

        protected override string OnGetName()
        {
            return "dynamic";
        }
    }
}