using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _EnumTypeBase
    {
        private class FieldMemberDictionary :
            _GroupedMembersBase<IEnumType, IEnumFieldMember, IFieldMemberDictionary<IEnumFieldMember, IEnumType>>,
            IFieldMemberDictionary<IEnumFieldMember, IEnumType>,
            IFieldMemberDictionary
        {
            internal FieldMemberDictionary(_FullMembersBase master, IFieldMemberDictionary<IEnumFieldMember, IEnumType> original, _EnumTypeBase parent)
                : base(master, original, parent)
            {

            }
            private _EnumTypeBase Parent
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

                public override string UniqueIdentifier
                {
                    get { return this.Name; }
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
