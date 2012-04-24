using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;
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
                : base(this.owner.Metadata.TypeParameters.Count)
            {
                this.owner = owner;
            }

            protected override ICliMetadataGenericParameterTableRow GetMetadataAt(int index)
            {
                return this.owner.Metadata.TypeParameters[index];
            }

            protected override IGenericTypeParameter<TIdentifier, TType> CreateElementFrom(ICliMetadataGenericParameterTableRow metadata)
            {
                throw new NotImplementedException();
            }


            #region IGenericParameterDictionary<IGenericTypeParameter<TIdentifier,TType>,TType> Members

            public TType Parent
            {
                get { return (TType)(object)this.owner; }
            }

            #endregion


        }
    }
}