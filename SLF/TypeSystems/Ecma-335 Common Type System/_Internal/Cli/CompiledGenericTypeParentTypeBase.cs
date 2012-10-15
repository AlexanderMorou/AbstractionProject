using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract partial class CompiledGenericTypeParentTypeBase<TIdentifier, TType> :
        CliGenericTypeBase<TIdentifier, TType>,
        IGenericType<TIdentifier, TType>,
        __ICliTypeParent
        where TIdentifier :
            IGenericTypeUniqueIdentifier
        where TType :
            IGenericType<TIdentifier, TType>
    {
        private CliFullTypeDictionary types;
        private CliClassTypeDictionary classes;
        private CliDelegateTypeDictionary delegates;
        private CliEnumTypeDictionary enumerations;
        private CliInterfaceTypeDictionary interfaces;
        private CliStructTypeDictionary structs;

        public CompiledGenericTypeParentTypeBase(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadataEntry)
            : base(assembly, metadataEntry)
        {
        }

        public IClassTypeDictionary Classes
        {
            get
            {
                if (this.classes == null)
                    this.classes = new CliClassTypeDictionary(this, (CliFullTypeDictionary) this.Types);
                return this.classes;
            }
        }

        public IDelegateTypeDictionary Delegates
        {
            get
            {
                if (this.delegates == null)
                    this.delegates = new CliDelegateTypeDictionary(this, (CliFullTypeDictionary) this.Types);
                return this.delegates;
            }
        }

        public IEnumTypeDictionary Enums
        {
            get
            {
                if (this.enumerations == null)
                    this.enumerations = new CliEnumTypeDictionary(this, (CliFullTypeDictionary) this.Types);
                return this.enumerations;
            }
        }

        public IInterfaceTypeDictionary Interfaces
        {
            get
            {
                if (this.interfaces == null)
                    this.interfaces = new CliInterfaceTypeDictionary(this, (CliFullTypeDictionary) this.Types);
                return this.interfaces;
            }
        }

        public IStructTypeDictionary Structs
        {
            get
            {
                if (this.structs == null)
                    this.structs = new CliStructTypeDictionary(this, (CliFullTypeDictionary) this.Types);
                return this.structs;
            }
        }

        public IFullTypeDictionary Types
        {
            get
            {
                if (this.types == null)
                    this.types = new CliFullTypeDictionary(this.MetadataEntry.NestedClasses, this);
                return this.types;
            }
        }

        public new _ICliManager IdentityManager
        {
            get { return (_ICliManager) base.IdentityManager; }
        }

        public new _ICliAssembly Assembly
        {
            get { return (_ICliAssembly) base.Assembly; }
        }

        public IControlledCollection<ICliMetadataTypeDefinitionTableRow> _Types
        {
            get { return this.MetadataEntry.NestedClasses; }
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name)
        {
            foreach (var nestedClass in this.MetadataEntry.NestedClasses)
                if (nestedClass.Name == name &&
                    (@namespace == null && nestedClass.Namespace.IsEmptyOrNull() ||
                    nestedClass.Namespace == @namespace))
                    return nestedClass;
            return null;
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name, string moduleName)
        {

            IModule checkModule;
            if (!this.Assembly.Modules.TryGetValue(AstIdentifier.GetDeclarationIdentifier(moduleName), out checkModule))
                return null;
            var module = (ICliModule) checkModule;
            foreach (var nestedClass in this.MetadataEntry.NestedClasses)
                if (nestedClass.Name == name &&
                    (@namespace == null && nestedClass.Namespace.IsEmptyOrNull() ||
                     nestedClass.Namespace == @namespace) && 
                    nestedClass.MetadataRoot == module.Metadata.MetadataRoot)
                    return nestedClass;

            return null;
        }

        public ICliMetadataTypeDefinitionTableRow FindType(IGeneralTypeUniqueIdentifier uniqueIdentifier)
        {
            foreach (var nestedClass in this.MetadataEntry.NestedClasses)
                if (nestedClass.Name == uniqueIdentifier.Name &&
                    (
                        (
                            (uniqueIdentifier.Namespace == null || uniqueIdentifier.Namespace.Name == string.Empty) && 
                            nestedClass.Namespace.IsEmptyOrNull()
                        ) ||
                        uniqueIdentifier.Namespace != null && nestedClass.Namespace == uniqueIdentifier.Namespace.Name)
                    )
                    return nestedClass;
            return null;
        }
    }
}
