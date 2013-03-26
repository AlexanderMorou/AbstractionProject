using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract class CliGenericParentType<TIdentifier, TType> :
        CliGenericTypeBase<TIdentifier, TType>,
        __ICliTypeParent
        where TIdentifier :
            IGenericTypeUniqueIdentifier
        where TType :
            IGenericType<TIdentifier, TType>
    {
        /// <summary>
        /// Data member for <see cref="Classes"/>.
        /// </summary>
        private CliClassTypeDictionary classes;
        /// <summary>
        /// Data member for <see cref="Delegates"/>.
        /// </summary>
        private CliDelegateTypeDictionary delegates;
        /// <summary>
        /// Data member for <see cref="Enums"/>
        /// </summary>
        private CliEnumTypeDictionary enums;
        /// <summary>
        /// Data member for <see cref="Interfaces"/>
        /// </summary>
        private CliInterfaceTypeDictionary interfaces;
        /// <summary>
        /// Data member for <see cref="Structs"/>.
        /// </summary>
        private CliStructTypeDictionary structs;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private CliFullTypeDictionary types;

        /// <summary>
        /// Creates a new <see cref="CliGenericParentType{TIdentifier, TType}"/> with the
        /// <paramref name="owner"/> and <paramref name="metadataEntry"/> provided.
        /// </summary>
        /// <param name="owner">The <see cref="CliAssembly"/> which contains the 
        /// <see cref="CliGenericParentType{TIdentifier, TType}"/>.</param>
        /// <param name="metadataEntry">The <see cref="ICliMetadataTypeDefinitionTableRow"/> from
        /// which the information within the <see cref="CliGenericParentType{TIdentifier, TType}"/>
        /// is derived.</param>
        internal CliGenericParentType(CliAssembly owner, ICliMetadataTypeDefinitionTableRow metadataEntry)
            : base(owner, metadataEntry)
        {

        }

        public IClassTypeDictionary Classes
        {
            get
            {
                return this.classes ?? (this.classes = new CliClassTypeDictionary(this, (CliFullTypeDictionary)this.Types));
            }
        }

        public IDelegateTypeDictionary Delegates
        {
            get
            {
                return this.delegates ?? (this.delegates = new CliDelegateTypeDictionary(this, (CliFullTypeDictionary)this.Types));
            }
        }

        public IEnumTypeDictionary Enums
        {
            get
            {
                return this.enums ?? (this.enums = new CliEnumTypeDictionary(this, (CliFullTypeDictionary)this.Types));
            }
        }

        public IInterfaceTypeDictionary Interfaces
        {
            get
            {
                return this.interfaces ?? (this.interfaces = new CliInterfaceTypeDictionary(this, (CliFullTypeDictionary)this.Types));
            }
        }

        public IStructTypeDictionary Structs
        {
            get
            {
                return this.structs ?? (this.structs = new CliStructTypeDictionary(this, (CliFullTypeDictionary)this.Types));
            }
        }

        public IFullTypeDictionary Types
        {
            get
            {
                return this.types ?? (this.types = new CliFullTypeDictionary(this.MetadataEntry.NestedClasses, this));
            }
        }


        public new _ICliManager IdentityManager
        {
            get
            {
                return (_ICliManager)base.IdentityManager;
            }
        }

        public new _ICliAssembly Assembly
        {
            get
            {
                return (_ICliAssembly)base.Assembly;
            }
        }

        public IControlledCollection<ICliMetadataTypeDefinitionTableRow> _Types
        {
            get
            {
                return this.MetadataEntry.NestedClasses;
            }
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name)
        {
            foreach (var type in this._Types)
                if (type.Namespace == @namespace &&
                    type.Name == name)
                    return type;
            return null;
        }

        protected override IEnumerable<IDeclaration> OnGetDeclarations()
        {
            return (from d in this.Types.Values
                    select (IDeclaration)d.Entry).Concat((from d in this.Members.Values
                                                          select (IDeclaration)d.Entry));

        }

        public ICliMetadataTypeDefinitionTableRow FindType(IGeneralTypeUniqueIdentifier uniqueIdentifier)
        {
            var myID = this.UniqueIdentifier;
            var depth = ((IGeneralTypeUniqueIdentifier)this.UniqueIdentifier).GetDepth();
            /* *
             * If the hierarchy of the unique identifier is properly formed, then
             * the current type's identifier should be contained within it.
             * */
            var hierarchy = uniqueIdentifier.GetNestingHierarchy().Skip(depth - 1);
            var first = hierarchy.FirstOrDefault();
            if (first == null ||
               !myID.Equals(first))
                return null;
            ICliMetadataTypeDefinitionTableRow typeDefinition = this.MetadataEntry;
            var remainingHierarchy = new Stack<IGeneralTypeUniqueIdentifier>(hierarchy.Skip(1).Reverse());
            while (remainingHierarchy.Count > 0)
            {
                var current = remainingHierarchy.Pop();
                bool found = false;
                foreach (var nestedType in typeDefinition.NestedClasses)
                    if (nestedType.Name == current.Name &&
                        current.Namespace == null && string.IsNullOrEmpty(nestedType.Namespace) ||
                        (current.Namespace != null && nestedType.Namespace == current.Namespace.Name))
                    {
                        typeDefinition = nestedType;
                        found = true;
                        break;
                    }
                if (!found)
                    return null;
            }
            return typeDefinition;
        }
    }
}
