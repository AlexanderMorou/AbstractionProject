﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataFieldSignature :
        ICliMetadataFieldSignature
    {
        internal CliMetadataFieldSignature(ICliMetadataTypeSignature type, IEnumerable<ICliMetadataCustomModifierSignature> modifiers, bool isPinned = false)
        {
            this.CustomModifiers = new ReadOnlyCollection<ICliMetadataCustomModifierSignature>(modifiers.ToArray());
            this.Type = type;
            this.IsPinned = isPinned;
        }

        #region ICliMetadataFieldSignature Members

        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeSignature"/> associated
        /// to the signature.
        /// </summary>
        public ICliMetadataTypeSignature Type { get; private set; }

        /// <summary>
        /// Returns the <see cref="IReadOnlyCollection{T}"/> which denotes
        /// the custom modifiers applied to the <see cref="Type"/>.
        /// </summary>
        public IReadOnlyCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        public bool IsPinned { get; private set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", string.Join<ICliMetadataCustomModifierSignature>(" ", CustomModifiers), CustomModifiers.Count == 0 ? string.Empty : " ", Type);
        }
    }
}