using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base class for custom attribute property assignments.
    /// </summary>
    /// <typeparam name="T">The type of value the property is.</typeparam>
    public class CustomAttributeDefinitionNamedParameter<T> :
        CustomAttributeDefinitionParameter<T>,
        ICustomAttributeDefinitionNamedParameter<T>
    {
        private string name;
        /// <summary>
        /// Creates a new <see cref="CustomAttributeDefinitionNamedParameter{T}"/>
        /// instance with the <paramref name="name"/> and <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="name">The name of the <see cref="CustomAttributeDefinitionNamedParameter{T}"/>
        /// relative to the property that is set on the custom attribute.</param>
        /// <param name="value">The <typeparamref name="T"/> instance
        /// which the <see cref="CustomAttributeDefinitionNamedParameter{T}"/> is typed as.</param>
        internal CustomAttributeDefinitionNamedParameter(string name, T value, CustomAttributeDefinitionParameterCollection owner) :
            base(value, owner)
        {
            this.name = name;
        }

        #region ICustomAttributeDefinitionNamedParameter<T> Members

        /// <summary>
        /// Returns/sets the name of the parameter
        /// defined on the custom attribute.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name == value)
                    return;
                string oldName = this.name;
                this.name = value;
                if (this.Owner == null)
                    return;
                this.Owner.OnItemRename(this, oldName, value);
            }
        }

        #endregion

        public override void Dispose()
        {
            base.Dispose();
            this.Name = null;
        }
    }
}
