using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliMemberBase<TIdentifier, TParent, TMetadata> :
        MemberBase<TIdentifier, TParent>,
        ICliMetadataMember
        where TIdentifier :
            IMemberUniqueIdentifier
        where TParent :
            IMemberParent
        where TMetadata :
            ICliMetadataTableRow
    {
        /// <summary>
        /// Data member for <see cref="MetadataEntry"/>.
        /// </summary>
        private TMetadata metadataEntry;

        /// <summary>
        /// Creates a new <see cref="CliMemberBase{TIdentifier, TParent, TMetadata}"/>
        /// with the <paramref name="parent"/> and <paramref name="metadataEntry"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TParent"/> which contains the
        /// <see cref="CliMemberBase{TIdentifier, TParent, TMetadata}"/>.</param>
        /// <param name="metadataEntry">The <typeparamref name="TMetadata"/> which
        /// contains the information about the member the <see cref="CliMemberBase{TIdentifier, TParent, TMetadata}"/>
        /// represents.</param>
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
        /// <value><see cref="metadataEntry"/></value>
        protected TMetadata MetadataEntry { get { return this.metadataEntry; } }

        ICliMetadataTableRow ICliDeclaration.MetadataEntry
        {
            get { return this.MetadataEntry; }
        }

    }
}
