using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
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
    partial class _EnumTypeBase
    {
        private class FieldMemberDictionary :
            _GroupedMembersBase<IEnumType, IGeneralMemberUniqueIdentifier, IEnumFieldMember, IFieldMemberDictionary<IEnumFieldMember, IEnumType>>,
            IFieldMemberDictionary<IEnumFieldMember, IEnumType>,
            IFieldMemberDictionary
        {
            internal FieldMemberDictionary(_FullMembersBase master, IFieldMemberDictionary<IEnumFieldMember, IEnumType> original, _EnumTypeBase parent)
                : base(master, original, parent)
            {

            }
            private new _EnumTypeBase Parent
            {
                get
                {
                    return (_EnumTypeBase)base.Parent;
                }
            }

            protected override IEnumFieldMember ObtainWrapper(IEnumFieldMember item)
            {
                return new _FieldMember(item, this.Parent);
            }

            private class _FieldMember :
                _FieldMemberBase<IEnumFieldMember, IEnumType>,
                IEnumFieldMember
            {
                internal _FieldMember(IEnumFieldMember original, _EnumTypeBase parent)
                    : base(original, parent)
                {

                }

                public override IGeneralMemberUniqueIdentifier UniqueIdentifier
                {
                    get { return AstIdentifier.GetMemberIdentifier(this.Name); }
                }

                #region IInstanceMember Members

                public InstanceMemberFlags InstanceFlags
                {
                    get { return Original.InstanceFlags; }
                }

                public bool IsStatic
                {
                    get { return Original.IsStatic; }
                }

                public bool IsHideBySignature
                {
                    get { return Original.IsHideBySignature; }
                }

                #endregion
            }

            #region IFieldMemberDictionary Members

            IFieldParent IFieldMemberDictionary.Parent
            {
                get { return this.Parent; }
            }

            #endregion

        }

    }
}
