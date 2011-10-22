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

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class CustomAttributeDefinitionCollection :
        ControlledStateCollection<ICustomAttributeDefinition>,
        ICustomAttributeDefinitionCollection
    {

        private ICustomAttributeDefinitionCollectionSeries parent;
        public CustomAttributeDefinitionCollection(ICustomAttributeDefinitionCollectionSeries parent)
        {
            this.parent = parent;
        }

        #region ICustomAttributeDefinitionCollection Members

        public ICustomAttributeDefinitionCollectionSeries Parent
        {
            get { return this.parent; ; }
        }

        /// <summary>
        /// Adds a <see cref="ICustomAttributeDefinition"/> with the
        /// <paramref name="values"/> provided.
        /// </summary>
        /// <param name="values">A series of varied type values and possible names.</param>
        /// <returns>A <see cref="ICustomAttributeDefinition"/> instance with the
        /// <paramref name="values"/> provided.</returns>
        /// <exception cref="System.ArgumentException">thrown when the <paramref name="values"/> points to a compiled attribute type
        /// which has no public constructor that matches the values given, or a property referenced in the named value series did not exist.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="values"/>' <see cref="CustomAttributeDefinition.ParameterValueCollection.AttributeType"/> is null.</exception>
        public ICustomAttributeDefinition Add(CustomAttributeDefinition.ParameterValueCollection values)
        {
            CustomAttributeDefinition definition = new CustomAttributeDefinition(this.parent.Parent, values);
            this.baseList.Add(definition);
            return definition;
        }

        /// <summary>
        /// Returns whether the <see cref="CustomAttributeDefinitionCollection"/> contains the
        /// an attribute of the <paramref name="attributeType"/> provided.
        /// </summary>
        /// <param name="attributeType">The <see cref="IType"/> of the attribute
        /// to search for.</param>
        /// <returns>true if a definition of an attribute is of or derived from
        /// the <paramref name="attributeType"/> provided.</returns>
        public bool Contains(IType attributeType)
        {
            foreach (var item in this)
            {
                if (item.Type == attributeType)
                    return true;
                else if (attributeType.IsAssignableFrom(item.Type))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the <paramref name="customAttribute"/> defined within the
        /// <see cref="CustomAttributeDefinitionCollection"/>.
        /// </summary>
        /// <param name="customAttribute">The <see cref="ICustomAttributeDefinition"/>
        /// to remove.</param>
        public void Remove(ICustomAttributeDefinition customAttribute)
        {
            if (!this.baseList.Contains(customAttribute))
                throw new ArgumentException("customAttribute");
            this.baseList.Remove(customAttribute);
        }

        #endregion

        internal void Add(ICustomAttributeDefinition definition)
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

    }
}
