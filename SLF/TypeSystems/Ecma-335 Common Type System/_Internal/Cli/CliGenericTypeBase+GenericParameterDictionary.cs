using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliGenericTypeBase<TIdentifier, TType>
    {
        private class GenericParameterDictionary :
            CliMetadataDrivenDictionary<IGenericParameterUniqueIdentifier, ICliMetadataGenericParameterTableRow, IGenericTypeParameter<TIdentifier, TType>>,
            IGenericParameterDictionary<IGenericTypeParameter<TIdentifier, TType>, TType>,
            IGenericParameterDictionary
        {
            private CliGenericTypeBase<TIdentifier, TType> owner;

            internal GenericParameterDictionary(CliGenericTypeBase<TIdentifier, TType> owner)
                : base(owner.MetadataEntry.TypeParameters)
            {
                this.owner = owner;
            }

            protected override IGenericTypeParameter<TIdentifier, TType> CreateElementFrom(int index, ICliMetadataGenericParameterTableRow metadata)
            {
                throw new NotImplementedException();
            }


            #region IGenericParameterDictionary<IGenericTypeParameter<TIdentifier,TType>,TType> Members

            public TType Parent
            {
                get { return (TType)(object)this.owner; }
            }

            #endregion

            protected override IGenericParameterUniqueIdentifier GetIdentifierFrom(int index, ICliMetadataGenericParameterTableRow metadata)
            {
                return AstIdentifier.GetGenericParameterIdentifier(index, true);
            }
        }
    }
}