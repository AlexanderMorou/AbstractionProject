using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a singleton field automatic value definition.
    /// </summary>
    public sealed class IntermediateEnumFieldAutomaticValue :
        IIntermediateEnumFieldValue
    {
        public static readonly IntermediateEnumFieldAutomaticValue AutomaticValue = new IntermediateEnumFieldAutomaticValue();
        private IntermediateEnumFieldAutomaticValue()
        {
        }

        #region IIntermediateEnumFieldValue Members

        public EnumValueType ValueType
        {
            get { return EnumValueType.Automatic; }
        }

        #endregion
    }
}
