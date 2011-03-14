using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateEnumType
    {
        partial class FieldMember
        {
            internal class ConstantValue<TValue> :
                PrimitiveExpression<TValue>,
                IIntermediateEnumFieldConstantValue<TValue>
            {
                internal ConstantValue(TValue value)
                    : base(value)
                {
                }

                #region IIntermediateEnumFieldValue Members

                public EnumValueType ValueType
                {
                    get { return EnumValueType.Constant; }
                }

                #endregion

            }

            internal class ExpressionValue :
                IIntermediateEnumFieldExpressionValue
            {
                internal ExpressionValue(IExpression value)
                {
                    this.Value = value;
                }

                #region IIntermediateEnumFieldExpressionValue Members

                public IExpression Value { get; set; }

                #endregion

                #region IIntermediateEnumFieldValue Members

                public EnumValueType ValueType
                {
                    get { return EnumValueType.Mixed; }
                }

                #endregion
                public override string ToString()
                {
                    if (this.Value == null)
                        return string.Empty;
                    return this.Value.ToString();
                }
            }
        }
    }
}
