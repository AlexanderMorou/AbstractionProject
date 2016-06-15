using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    /// <summary>Provides a base implementation for working with an intermediate enum defined on top of the Common Language Infrastructure</summary>
    internal class IntermediateCliEnumType :
        IntermediateEnumType,
        IIntermediateCliType
    {
        internal protected IntermediateCliEnumType(string name, IIntermediateTypeParent parent)
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
}
