using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    [Serializable]
    public class PropertySetBodyMember :
        PropertyBodyMember,
        IPropertySetBodyMember
    {
        public PropertySetBodyMember(IPropertyMember parentTarget)
            : base(PropertyBodyMemberPart.SetPart, parentTarget)
        {
        }

        #region IPropertySetBodyMember Members

        public IPropertySetValueReferenceExpression ValueLocal
        {
            get
            {
                return PropertySetValueReferenceExpression.Reference;
            }
        }

        #endregion

    }
}
