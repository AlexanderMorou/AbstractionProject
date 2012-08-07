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
        private MetadataDefinitionCollection attributes;
        private IMetadataCollection _attributes;
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

        #region IIntermediateMetadataEntity Members

        public IMetadataDefinitionCollection CustomAttributes
        {
            get
            {
                if (this.attributes == null)
                    this.attributes = new MetadataDefinitionCollection(this);
                return this.attributes;
            }
        }

        #endregion

        #region IMetadataEntity Members

        IMetadataCollection IMetadataEntity.Metadata
        {
            get
            {
                if (this._attributes == null)
                    this._attributes = ((MetadataDefinitionCollection)(this.CustomAttributes)).GetWrapper();
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
