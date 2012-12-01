using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliMemberBase<TIdentifier, TParent, TMetadata> :
        MemberBase<TIdentifier, TParent>
        where TIdentifier :
            IMemberUniqueIdentifier
        where TParent :
            IMemberParent
        where TMetadata :
            ICliMetadataTableRow
    {
        private TMetadata metadataEntry;
        public CliMemberBase(TParent parent, TMetadata metadataEntry)
            : base(parent)
        {
            this.metadataEntry = metadataEntry;
        }

        /// <summary>
        /// Returns the <typeparamref name="TMetadata"/> from which the current
        /// <see cref="CliMemberBase{TIdentifier, TParent, TMetadata}"/> was
        /// derived.
        /// </summary>
        protected TMetadata MetadataEntry { get { return this.metadataEntry; } }

    }
}
