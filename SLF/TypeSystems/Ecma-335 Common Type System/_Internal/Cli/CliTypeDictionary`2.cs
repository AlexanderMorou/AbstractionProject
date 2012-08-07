using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Diagnostics;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    [DebuggerDisplay("{Count} {PluralElementKind,nq}")]
    internal partial class CliTypeDictionary<TIdentifier, TType> :
        CliMetadataDrivenDictionary<TIdentifier, int, TType>,
        IGroupedTypeDictionary<TIdentifier, TType>,
        ISubordinateDictionary
        where TIdentifier :
            class,
            ITypeUniqueIdentifier,
            IGeneralTypeUniqueIdentifier
        where TType :
            class,
            IType<TIdentifier, TType>
    {
        private _ICliTypeParent parent;
        private CliFullTypeDictionary master;
        //private int[] filteredTypes;
        private TIdentifier[] filteredIdentifiers;
        private TypeKind filterKind;
        public CliTypeDictionary(_ICliTypeParent parent, CliFullTypeDictionary master, TypeKind filterKind)
        {
            this.master = master;
            var filteredSet = master.ObtainSubset<TIdentifier, TType>(filterKind).SplitSet();
            this.filteredIdentifiers = filteredSet.Item2;
            base.Initialize(filteredSet.Item1);
            this.parent = parent;
            this.filterKind = filterKind;
        }

        protected override TType CreateElementFrom(int index, int metadata)
        {
            return (TType)this.master.Values[metadata].Entry;
        }

        private string PluralElementKind
        {
            get
            {
                switch (filterKind)
                {
                    case TypeKind.Class:
                        return "classes";
                    case TypeKind.Delegate:
                        return "delegates";
                    case TypeKind.Enumeration:
                        return "enumerations";
                    case TypeKind.Interface:
                        return "interfaces";
                    case TypeKind.Struct:
                        return "data structures";
                    default:
                        throw new InvalidOperationException();
                        break;
                }
            }
        }

        #region ISubordinateDictionary<TIdentifier,IGeneralTypeUniqueIdentifier,TType,IType> Members

        public IMasterDictionary<IGeneralTypeUniqueIdentifier, IType> Master
        {
            get { return this.master; }
        }

        #endregion

        #region ISubordinateDictionary Members

        IMasterDictionary ISubordinateDictionary.Master
        {
            get { return (IMasterDictionary) this.Master; }
        }

        #endregion

        protected override TIdentifier GetIdentifierFrom(int index, int metadata)
        {
            return this.filteredIdentifiers[index];
        }


        public ITypeParent Parent
        {
            get { return this.parent; }
        }

    }
}
