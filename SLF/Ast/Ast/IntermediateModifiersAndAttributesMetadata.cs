using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class IntermediateModifiersAndAttributesMetadata :
        IIntermediateModifiersAndAttributesMetadata
    {
        private CustomAttributeDefinitionCollectionSeries attributes;
        private ICustomAttributeCollection _attributes;
        private ITypeCollection requiredModifiers;
        private ITypeCollection optionalModifiers;

        #region IIntermediateModifiersAndAttributesMetadata Members

        public ITypeCollection RequiredModifiers
        {
            get {
                if (this.requiredModifiers == null)
                    this.requiredModifiers = new TypeCollection();
                return this.requiredModifiers;
            }
        }

        public ITypeCollection OptionalModifiers
        {
            get {
                if (this.optionalModifiers == null)
                    this.optionalModifiers = new TypeCollection();
                return this.optionalModifiers; }
        }

        #endregion

        #region IIntermediateCustomAttributedEntity Members

        public ICustomAttributeDefinitionCollectionSeries CustomAttributes
        {
            get
            {
                if (this.attributes == null)
                    this.attributes = new CustomAttributeDefinitionCollectionSeries(this);
                return this.attributes;
            }
        }

        #endregion

        #region ICustomAttributedEntity Members

        ICustomAttributeCollection ICustomAttributedEntity.CustomAttributes
        {
            get
            {
                if (this._attributes == null)
                    this._attributes = ((CustomAttributeDefinitionCollectionSeries)(this.CustomAttributes)).GetWrapper();
                return this._attributes;
            }
        }

        public bool IsDefined(IType attributeType)
        {
            return this.StandardIsDefined(attributeType);
        }

        #endregion

        #region IModifiersAndAttributesMetadata Members

        IEnumerable<IType> IModifiersAndAttributesMetadata.RequiredModifiers
        {
            get { return this.RequiredModifiers; }
        }

        IEnumerable<IType> IModifiersAndAttributesMetadata.OptionalModifiers
        {
            get { return this.OptionalModifiers; }
        }

        #endregion
    }
}
