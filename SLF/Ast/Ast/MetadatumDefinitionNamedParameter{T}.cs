using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base class for custom attribute property assignments.
    /// </summary>
    /// <typeparam name="T">The type of value the property is.</typeparam>
    public class MetadatumDefinitionNamedParameter<T> :
        MetadatumDefinitionParameter<T>,
        IMetadatumDefinitionNamedParameter<T>
    {
        private string name;
        /// <summary>
        /// Creates a new <see cref="MetadatumDefinitionNamedParameter{T}"/>
        /// instance with the <paramref name="name"/> and <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="name">The name of the <see cref="MetadatumDefinitionNamedParameter{T}"/>
        /// relative to the property that is set on the custom attribute.</param>
        /// <param name="value">The <typeparamref name="T"/> instance
        /// which the <see cref="MetadatumDefinitionNamedParameter{T}"/> is typed as.</param>
        /// <param name="owner">The <see cref="MetadatumDefinitionParameterCollection"/>
        /// which contains the <see cref="MetadatumDefinitionNamedParameter{T}"/>.</param>
        /// <param name="valueType">The <see cref="IType"/>
        /// which represents the type of <paramref name="value"/>
        /// within the current typing model.</param>
        internal MetadatumDefinitionNamedParameter(string name, T value, MetadatumDefinitionParameterCollection owner, IType valueType) :
            base(value, owner, valueType)
        {
            this.name = name;
        }

        #region IMetadatumDefinitionNamedParameter<T> Members

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

        public override string ToString()
        {
            return string.Format("{0} = {1}", this.Name, this.Value);
        }
    }
}
