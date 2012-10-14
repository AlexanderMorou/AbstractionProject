using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class MetadataDefinition :
        ControlledCollection<IMetadatumDefinition>,
        IMetadataDefinition
    {

        private IMetadataDefinitionCollection parent;
        public MetadataDefinition(IMetadataDefinitionCollection parent)
        {
            this.parent = parent;
        }

        #region IMetadataDefinition Members

        public IMetadataDefinitionCollection Parent
        {
            get { return this.parent; ; }
        }

        /// <summary>
        /// Adds a <see cref="IMetadatumDefinition"/> with the
        /// <paramref name="values"/> provided.
        /// </summary>
        /// <param name="values">A series of varied type values and possible names.</param>
        /// <returns>A <see cref="IMetadatumDefinition"/> instance with the
        /// <paramref name="values"/> provided.</returns>
        /// <exception cref="System.ArgumentException">thrown when the <paramref name="values"/> points to a compiled attribute type
        /// which has no public constructor that matches the values given, or a property referenced in the named value series did not exist.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="values"/>' <see cref="MetadatumDefinitionParameterValueCollection.AttributeType"/> is null.</exception>
        public IMetadatumDefinition Add(MetadatumDefinitionParameterValueCollection values)
        {
            MetadatumDefinition definition = new MetadatumDefinition(this.parent.Parent, values, this.parent.IdentityManager);
            this.baseList.Add(definition);
            return definition;
        }

        /// <summary>
        /// Returns whether the <see cref="MetadataDefinition"/> contains the
        /// an attribute of the <paramref name="metadatumType"/> provided.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/> of the attribute
        /// to search for.</param>
        /// <returns>true if a definition of an attribute is of or derived from
        /// the <paramref name="metadatumType"/> provided.</returns>
        public bool Contains(IType metadatumType)
        {
            foreach (var item in this)
            {
                if (item.Type == metadatumType)
                    return true;
                else if (metadatumType.IsAssignableFrom(item.Type))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the <paramref name="customAttribute"/> defined within the
        /// <see cref="MetadataDefinition"/>.
        /// </summary>
        /// <param name="customAttribute">The <see cref="IMetadatumDefinition"/>
        /// to remove.</param>
        public void Remove(IMetadatumDefinition customAttribute)
        {
            if (!this.baseList.Contains(customAttribute))
                throw new ArgumentException("customAttribute");
            this.baseList.Remove(customAttribute);
        }

        #endregion

        internal void Add(IMetadatumDefinition definition)
        {
            this.baseList.Add(definition);
        }

        #region IDisposable Members

        public void Dispose()
        {
            foreach (var item in this)
                item.Dispose();
            this.baseList.Clear();
            this.baseList = null;
        }

        #endregion
        public override string ToString()
        {

            StringBuilder builder = new StringBuilder();
            bool first = true;
            foreach (var item in this)
            {
                if (first)
                    first = false;
                else
                    builder.Append(", ");
                builder.Append(item);
            }
            return builder.ToString();
        }

    }
}
