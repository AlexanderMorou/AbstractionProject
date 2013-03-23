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
        private class TypeParameterDictionary :
            CliMetadataDrivenDictionary<IGenericParameterUniqueIdentifier, ICliMetadataGenericParameterTableRow, IGenericTypeParameter<TIdentifier, TType>>,
            IGenericParameterDictionary<IGenericTypeParameter<TIdentifier, TType>, TType>,
            IGenericParameterDictionary
        {
            private CliGenericTypeBase<TIdentifier, TType> owner;

            /// <summary>
            /// Creates a new <see cref="GenericParameterDicitonary"/> with the <paramref name="owner"/> provided.
            /// </summary>
            /// <param name="owner">The <see cref="CliGenericTypeBase{TIdentifier, TType}"/> which 
            /// contains the generic parameters.</param>
            internal TypeParameterDictionary(CliGenericTypeBase<TIdentifier, TType> owner)
            {
                if (owner.MetadataEntry.TypeParameters == null)
                    this.Initialize(new ICliMetadataGenericParameterTableRow[0]);
                else
                    this.Initialize(owner.MetadataEntry.TypeParameters);
                this.owner = owner;
            }

            protected override IGenericTypeParameter<TIdentifier, TType> CreateElementFrom(int index, ICliMetadataGenericParameterTableRow metadata)
            {
                return new GenericParameter(this.owner, metadata, index);
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