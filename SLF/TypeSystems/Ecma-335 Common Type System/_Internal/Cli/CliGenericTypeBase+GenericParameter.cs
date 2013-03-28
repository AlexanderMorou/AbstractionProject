using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Properties;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
//2.2046226218487755 -- kg -> pounds

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliGenericTypeBase<TIdentifier, TType>
    {
        private class GenericParameter :
            CliGenericParameterMember<IGenericTypeParameter<TIdentifier, TType>, TType>,
            IGenericTypeParameter<TIdentifier, TType>
        {
            internal GenericParameter(CliGenericTypeBase<TIdentifier, TType> owner, ICliMetadataGenericParameterTableRow metadataEntry, int index)
                : base((TType)(object)owner, metadataEntry, index)
            {
            }
            protected override IAssembly OnGetAssembly()
            {
                return this.Parent.Assembly;
            }

            protected override IGenericParameterUniqueIdentifier OnGetUniqueIdentifier()
            {
                return TypeSystemIdentifiers.GetGenericParameterIdentifier(this.Position, true);
            }

            IGenericType IGenericTypeParameter.Parent { get { return this.Parent; } }

            protected override ITypeIdentityManager OnGetManager()
            {
                return this.Parent.IdentityManager;
            }
        }
    }
}