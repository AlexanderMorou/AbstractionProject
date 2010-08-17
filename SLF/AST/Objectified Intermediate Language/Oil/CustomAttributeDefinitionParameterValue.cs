using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a quick instantiable custom attribute definition
    /// value structure.
    /// </summary>
    /// <typeparam name="T">The type of parameter.</typeparam>
    public struct CustomAttributeDefinitionParameterValue<T> :
        _ICustomAttributeDefinitionParameterValue
    {
        /// <summary>
        /// Returns/sets the <typeparamref name="T"/> value.
        /// </summary>
        T Value { get; set; }
        internal CustomAttributeDefinitionParameterValue(T value)
            : this()
        {
            this.Value = value;
        }

        #region _ICustomAttributeDefinitionParameterValue Members

        ICustomAttributeDefinitionParameter _ICustomAttributeDefinitionParameterValue.AddSelf(_ICustomAttributeDefinitionParameterCollection target)
        {
            return target.AddInternal(Value);
        }

        #endregion

        #region ICustomAttributeDefinitionParameterValue Members

        object ICustomAttributeDefinitionParameterValue.Value
        {
            get { return this.Value; }
        }

        #endregion

    }
}
