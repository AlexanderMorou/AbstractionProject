using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICliDeclaration :
        IDeclaration
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataTableRow"/> from which the
        /// <see cref="ICliDeclaration"/> is derived.
        /// </summary>
        ICliMetadataTableRow Metadata { get; }
    }

    public interface ICliDeclaration<TIdentifier, TMetadata> :
        IDeclaration<TIdentifier>,
        ICliDeclaration
        where TIdentifier :
            IDeclarationUniqueIdentifier
        where TMetadata :
            ICliMetadataTableRow
    {
        /// <summary>
        /// Returns the <typeparamref name="ICliMetadataTableRow"/> from which the
        /// <see cref="ICliDeclaration{TIdentifier, TMetadata}"/> is derived.
        /// </summary>
        new TMetadata Metadata { get; }
    }
}
