using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    /// <summary>Provides a base implementation for working with an intermediate generic interface defined on top of the Common Language Infrastructure</summary>
    /// <typeparam name="TInstanceIntermediateType">The type of intermediate instance to create partials for if segmented across multiple files.</typeparam>
    internal abstract partial class IntermediateCliInterfaceType<TInstanceIntermediateType> :
        IntermediateInterfaceType<TInstanceIntermediateType>,
        IIntermediateCliType
        where TInstanceIntermediateType :
            IntermediateCliInterfaceType<TInstanceIntermediateType>
    {
        internal protected IntermediateCliInterfaceType(TInstanceIntermediateType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }
        internal protected IntermediateCliInterfaceType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        public new IIntermediateCliAssembly Assembly
        {
            get { return (IIntermediateCliAssembly)base.Assembly; }
        }

        public ICliMetadataTypeDefinitionMutableTableRow MetadataEntry
        {
            get { return null; }
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

    /// <summary>Provides a base implementation for working with an intermediate interface defined on top of the Common Language Infrastructure</summary>
    internal class IntermediateCliInterfaceType :
        IntermediateCliInterfaceType<IntermediateCliInterfaceType>
    {
        internal protected IntermediateCliInterfaceType(IntermediateCliInterfaceType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        internal protected IntermediateCliInterfaceType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        protected override IntermediateCliInterfaceType GetNewPartial(IntermediateCliInterfaceType root, IIntermediateTypeParent parent)
        {
            return new IntermediateCliInterfaceType(root, parent);
        }
    }
}
