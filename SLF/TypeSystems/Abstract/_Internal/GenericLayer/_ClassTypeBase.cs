using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal partial class _ClassTypeBase :
        _GenericInstantiableTypeBase<IClassCtorMember, IClassEventMember, IClassFieldMember, IClassIndexerMember, IClassMethodMember, IClassPropertyMember, IClassType>,
        IClassType
    {
        internal _ClassTypeBase(IClassType original, IControlledTypeCollection genericParameters)
            : base(original,genericParameters)
        {
        }

        #region IClassType Members

        public new IClassInterfaceMapping GetInterfaceMap(IInterfaceType type)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override IInterfaceMemberMapping<IClassMethodMember, IClassPropertyMember, IClassEventMember, IClassIndexerMember, IClassType> OnGetInterfaceMapping(IInterfaceType type)
        {
            return this.GetInterfaceMap(type);
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Class; }
        }

        protected override IConstructorMemberDictionary<IClassCtorMember, IClassType> InitializeConstructors()
        {
            return new _ConstructorsBase(this._Members, this.Original.Constructors, this);
        }

        protected override IMethodMemberDictionary<IClassMethodMember, IClassType> InitializeMethods()
        {
            return new _MethodsBase(this._Members, this.Original.Methods, this);
        }

        protected override IPropertyMemberDictionary<IClassPropertyMember, IClassType> InitializeProperties()
        {
            return new _PropertiesBase(this._Members, this.Original.Properties, this);
        }

        protected override IFieldMemberDictionary<IClassFieldMember, IClassType> InitializeFields()
        {
            return new _FieldMembersBase(this._Members, this.Original.Fields, this);
        }

        protected override IIndexerMemberDictionary<IClassIndexerMember, IClassType> InitializeIndexers()
        {
            return new _IndexersBase(this._Members, this.Original.Indexers, this);
        }

        protected override IEventMemberDictionary<IClassEventMember, IClassType> InitializeEvents()
        {
            return new _EventsBase(this._Members, this.Original.Events, this);
        }

        protected override IClassCtorMember InitializeTypeInitializer(IClassCtorMember original)
        {
            return new _ConstructorsBase._Constructor(original, this);
        }

        #region IClassType Members

        public bool IsDefined(IType metadatumType, bool inherit)
        {
            return this.Original.IsDefined(metadatumType, inherit);
        }


        public SpecialClassModifier SpecialModifier
        {
            get { return this.Original.SpecialModifier; }
        }

        #endregion

        public new IClassType BaseType
        {
            get
            {
                return ((IClassType)(base.BaseType));
            }
        }

    }
}
