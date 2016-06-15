using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    internal abstract partial class IntermediateCliClassType<TInstanceIntermediateType> :
        IntermediateClassType<TInstanceIntermediateType>,
        IIntermediateCliType
        where TInstanceIntermediateType :
            IntermediateCliClassType<TInstanceIntermediateType>
    {
        private ICliMetadataTypeDefinitionMutableTableRow mutableTableRow;
        internal protected IntermediateCliClassType(TInstanceIntermediateType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        internal protected IntermediateCliClassType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }
        internal protected IntermediateCliClassType(IIntermediateTypeParent parent)
            : base(parent)
        {
        }

        public new IIntermediateCliAssembly Assembly
        {
            get { return (IIntermediateCliAssembly)base.Assembly; }
        }

        public ICliMetadataTypeDefinitionMutableTableRow MetadataEntry
        {
            get { return this.mutableTableRow ?? (this.mutableTableRow = this.InitializeMetadataEntry()); ; }
        }

        private ICliMetadataTypeDefinitionMutableTableRow InitializeMetadataEntry()
        {
            return null;
        }

        ICliAssembly ICliType.Assembly
        {
            get { return this.Assembly; }
        }

        ICliMetadataTypeDefinitionTableRow ICliType.MetadataEntry
        {
            get { return this.MetadataEntry; }
        }


        ICliMetadataTableRow ICliDeclaration.MetadataEntry
        {
            get { return this.MetadataEntry; }
        }
    }

    internal class IntermediateCliClassType :
        IntermediateCliClassType<IntermediateCliClassType>
    {
        internal protected IntermediateCliClassType(IntermediateCliClassType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        internal protected IntermediateCliClassType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        internal protected IntermediateCliClassType(IIntermediateTypeParent parent)
            : base(parent)
        {
        }

        protected override IntermediateCliClassType GetNewPartial(IntermediateCliClassType root, IIntermediateTypeParent parent)
        {
            return new IntermediateCliClassType(root, parent);
        }
    }
}
