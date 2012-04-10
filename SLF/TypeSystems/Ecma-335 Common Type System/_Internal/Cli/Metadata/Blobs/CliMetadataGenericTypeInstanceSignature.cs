using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataGenericTypeInstanceSignature :
        CliMetadataValueOrClassTypeSignature,
        ICliMetadataGenericTypeInstanceSignature
    {
        public CliMetadataGenericTypeInstanceSignature(bool isClass, ITypeDefOrRefRow genericType, IEnumerable<ICliMetadataTypeSignature> genericParameters)
            : base(isClass, genericType)
        {
            this.GenericParameters = new ReadOnlyCollection<ICliMetadataTypeSignature>(genericParameters.ToList());
        }

        #region ICliMetadataGenericTypeInstanceSignature Members

        public IReadOnlyCollection<ICliMetadataTypeSignature> GenericParameters { get; private set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}<{1}>", base.ToString(), string.Join<ICliMetadataTypeSignature>(", ", GenericParameters));
        }
    }
}
