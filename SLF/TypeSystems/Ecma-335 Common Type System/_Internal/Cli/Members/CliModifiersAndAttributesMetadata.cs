using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliModifiersAndAttributesMetadata :
        IModifiersAndAttributesMetadata
    {
        private IReadOnlyCollection<ICliMetadataCustomAttributeTableRow> attributes;
        private IReadOnlyCollection<ICliMetadataTypeSignature> requiredModifiers;
        private IReadOnlyCollection<ICliMetadataTypeSignature> optionalModifiers;

        #region IModifiersAndAttributesMetadata Members

        public IEnumerable<IType> RequiredModifiers
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IType> OptionalModifiers
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMetadataEntity Members

        public IMetadataCollection CustomAttributes
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsDefined(IType metadatumType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
