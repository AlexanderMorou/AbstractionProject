using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    /// <summary>Provides a base implementation for working with an intermediate generic struct defined on top of the Common Language Infrastructure.</summary>
    /// <typeparam name="TInstanceIntermediateType">The type of intermediate instance to create partials for if segmented across multiple files.</typeparam>
    internal abstract partial class IntermediateCliStructType<TInstanceIntermediateType> :
        IntermediateStructType<TInstanceIntermediateType>,
        IIntermediateCliType
        where TInstanceIntermediateType :
            IntermediateCliStructType<TInstanceIntermediateType>
    {
        internal protected IntermediateCliStructType(TInstanceIntermediateType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        internal protected IntermediateCliStructType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }
        internal protected IntermediateCliStructType(IIntermediateTypeParent parent)
            : base(parent)
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

    /// <summary>Provides a base implementation for working with an intermediate struct defined on top of the Common Language Infrastructure.</summary>
    internal class IntermediateCliStructType :
        IntermediateCliStructType<IntermediateCliStructType>
    {

        internal protected IntermediateCliStructType(IntermediateCliStructType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        internal protected IntermediateCliStructType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
            
        }

        internal protected IntermediateCliStructType(IIntermediateTypeParent parent)
            : base(parent)
        {
        }

        protected override IntermediateCliStructType GetNewPartial(IntermediateCliStructType root, IIntermediateTypeParent parent)
        {
            return new IntermediateCliStructType(root, parent);
        }
    }
}
