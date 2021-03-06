﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataGenericInstanceTypeSignature :
        CliMetadataValueOrClassTypeSignature,
        ICliMetadataGenericInstanceTypeSignature
    {
        public CliMetadataGenericInstanceTypeSignature(bool isClass, ICliMetadataTypeDefOrRefRow genericType, ICliMetadataTypeSignature[] genericParameters)
            : base(isClass, genericType)
        {
            if (genericParameters == null || genericParameters.Length == 0)
                this.GenericParameters = ArrayReadOnlyCollection<ICliMetadataTypeSignature>.Empty;
            else
                this.GenericParameters = new ArrayReadOnlyCollection<ICliMetadataTypeSignature>(genericParameters);
        }

        //#region ICliMetadataGenericInstanceTypeSignature Members

        public IControlledCollection<ICliMetadataTypeSignature> GenericParameters { get; private set; }

        //#endregion

        public override string ToString()
        {
            return string.Format("{0}<{1}>", base.ToString(), string.Join<ICliMetadataTypeSignature>(", ", GenericParameters));
        }

        #region ICliMetadataTypeSignature Members

        public CliMetadataTypeSignatureKind TypeSignatureKind
        {
            get { return CliMetadataTypeSignatureKind.GenericInstType; }
        }

        #endregion

        #region ICliMetadataSignature Members

        public SignatureKinds SignatureKind
        {
            get { return SignatureKinds.GenericTypeInstance; }
        }

        #endregion
    }
}
