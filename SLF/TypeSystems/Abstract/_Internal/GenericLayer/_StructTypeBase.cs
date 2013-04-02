using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal partial class _StructTypeBase :
        _GenericInstantiableTypeBase<IStructCtorMember, IStructEventMember, IStructFieldMember, IStructIndexerMember, IStructMethodMember, IStructPropertyMember, IStructType>,
        IStructType
    {
        internal _StructTypeBase(IStructType original, IControlledTypeCollection genericParameters)
            : base(original,genericParameters)
        {
        }

        #region IStructType Members

        public new IStructInterfaceMapping GetInterfaceMap(IInterfaceType type)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override IInterfaceMemberMapping<IStructMethodMember, IStructPropertyMember, IStructEventMember, IStructIndexerMember, IStructType> OnGetInterfaceMapping(IInterfaceType type)
        {
            return this.GetInterfaceMap(type);
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Struct; }
        }

        protected override IConstructorMemberDictionary<IStructCtorMember, IStructType> InitializeConstructors()
        {
            return new _ConstructorsBase(this._Members, this.Original.Constructors, this);
        }

        protected override IMethodMemberDictionary<IStructMethodMember, IStructType> InitializeMethods()
        {
            return new _MethodsBase(this._Members, this.Original.Methods, this);
        }

        protected override IPropertyMemberDictionary<IStructPropertyMember, IStructType> InitializeProperties()
        {
            return new _PropertiesBase(this._Members, this.Original.Properties, this);
        }

        protected override IFieldMemberDictionary<IStructFieldMember, IStructType> InitializeFields()
        {
            return new _FieldMembersBase(this._Members, this.Original.Fields, this);
        }

        protected override IStructCtorMember InitializeTypeInitializer(IStructCtorMember original)
        {
            return new _ConstructorsBase._Constructor(original, this);
        }

        protected override IEventMemberDictionary<IStructEventMember, IStructType> InitializeEvents()
        {
            return new _EventsBase(this._Members, this.Original.Events, this);
        }

        protected override IIndexerMemberDictionary<IStructIndexerMember, IStructType> InitializeIndexers()
        {
            return new _IndexersBase(this._Members, this.Original.Indexers, this);
        }
    }
}
