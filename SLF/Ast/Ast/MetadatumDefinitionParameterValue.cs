using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a quick instantiable custom attribute definition
    /// value structure.
    /// </summary>
    /// <typeparam name="T">The type of parameter.</typeparam>
    public struct MetadatumDefinitionParameterValue<T> :
        _IMetadatumDefinitionParameterValue
    {
        /// <summary>
        /// Returns/sets the <typeparamref name="T"/> value.
        /// </summary>
        T Value { get; set; }
        internal MetadatumDefinitionParameterValue(T value)
            : this()
        {
            this.Value = value;
        }

        #region _IMetadatumDefinitionParameterValue Members

        IMetadatumDefinitionParameter _IMetadatumDefinitionParameterValue.AddSelf(_IMetadatumDefinitionParameterCollection target)
        {
            return target.AddInternal(Value);
        }

        #endregion

        #region IMetadatumDefinitionParameterValue Members

        object IMetadatumDefinitionParameterValue.Value
        {
            get { return this.Value; }
        }

        #endregion

        public override string ToString()
        {
            return this.Value.ToString();
        }

    }
}
