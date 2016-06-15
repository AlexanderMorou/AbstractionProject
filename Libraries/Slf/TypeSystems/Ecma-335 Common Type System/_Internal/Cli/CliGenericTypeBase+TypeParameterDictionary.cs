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
                {
                    if (owner.Parent is IGenericType)
                    {
                        var parentGeneric = (IGenericType)owner.Parent;
                        if (parentGeneric.IsGenericConstruct)
                            this.Initialize(owner.MetadataEntry.TypeParameters.Skip(parentGeneric.GenericParameters.Count).ToArray());
                        else
                            this.Initialize(owner.MetadataEntry.TypeParameters);
                    }
                    else
                        this.Initialize(owner.MetadataEntry.TypeParameters);
                }
                this.owner = owner;
            }

            protected override IGenericTypeParameter<TIdentifier, TType> CreateElementFrom(int index, ICliMetadataGenericParameterTableRow metadata)
            {
                /* ACC - November 20, 2015: Due to the means at which type-parameters are handled, the limited set of type-parameters we 
                 * select out for the sake of the model, we need to use the metadata's number versus the index, as it's the accurate
                 * reflection of what we're aiming for. */
                return new GenericParameter(this.owner, metadata, metadata.Number);
            }

            #region IGenericParameterDictionary<IGenericTypeParameter<TIdentifier,TType>,TType> Members

            public TType Parent
            {
                get { return (TType)(object)this.owner; }
            }

            #endregion

            protected override IGenericParameterUniqueIdentifier GetIdentifierFrom(int index, ICliMetadataGenericParameterTableRow metadata)
            {
                return TypeSystemIdentifiers.GetGenericParameterIdentifier(index, true);
            }
        }
    }
}