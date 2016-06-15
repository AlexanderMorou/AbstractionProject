using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliNamespaceDeclaration :
        INamespaceDeclaration,
        _ICliTopLevelTypeParent
    {
        private object syncObject = new object();
        private CliAssembly owningAssembly;
        CliNamespaceKeyedTreeNode namespaceInfo;
        private INamespaceParent parent;
        private INamespaceDictionary namespaces;
        private CliFullTypeDictionary types;
        private CliClassTypeDictionary classes;
        private CliDelegateTypeDictionary delegates;
        private CliEnumTypeDictionary enumerations;
        private CliInterfaceTypeDictionary interfaces;
        private CliStructTypeDictionary structs;

        public CliNamespaceDeclaration(CliAssembly owningAssembly, INamespaceParent parent, CliNamespaceKeyedTreeNode namespaceInfo)
        {
            this.owningAssembly = owningAssembly;
            this.namespaceInfo = namespaceInfo;
            this.parent = parent;
        }

        //#region INamespaceDeclaration Members

        public IAssembly Assembly
        {
            get { return this.owningAssembly; }
        }

        public string FullName
        {
            get
            {
                string fullSpace = this.namespaceInfo.StringsSection[this.namespaceInfo.Value];
                if (namespaceInfo.SubspaceLength == 0)
                    return fullSpace;
                else
                    return fullSpace.Substring(0, this.namespaceInfo.SubspaceStart + this.namespaceInfo.SubspaceLength);
            }
        }

        public INamespaceParent Parent
        {
            get { return this.parent; }
        }

        //#endregion

        //#region INamespaceParent Members

        public IFullMemberDictionary Members
        {
            get { throw new NotImplementedException(); }
        }

        public INamespaceDictionary Namespaces
        {
            get
            {
                if (namespaces == null)
                    this.namespaces = new CliNamespaceDictionary(this.owningAssembly, this, this.namespaceInfo);
                return this.namespaces;
            }
        }

        //#endregion

        //#region IFieldParent<ITopLevelFieldMember,INamespaceParent> Members

        public IFieldMemberDictionary<ITopLevelFieldMember, INamespaceParent> Fields
        {
            get { throw new NotImplementedException(); }
        }

        //#endregion

        //#region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return (IFieldMemberDictionary)this.Fields; }
        }

        //#endregion

        //#region IMethodParent<ITopLevelMethodMember,INamespaceParent> Members

        public IMethodMemberDictionary<ITopLevelMethodMember, INamespaceParent> Methods
        {
            get { throw new NotImplementedException(); }
        }

        //#endregion

        //#region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { return (IMethodMemberDictionary)this.Methods; }
        }

        //#endregion

        //#region ITypeParent Members

        public IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { throw new NotImplementedException(); }
        }

        public IClassTypeDictionary Classes
        {
            get
            {
                lock (this.syncObject)
                {
                    CheckClasses();
                    return this.classes;
                }
            }
        }

        public IDelegateTypeDictionary Delegates
        {
            get
            {
                lock (this.syncObject)
                {
                    CheckDelegates();
                    return this.delegates;
                }
            }
        }

        public IEnumTypeDictionary Enums
        {
            get
            {
                lock (this.syncObject)
                {
                    CheckEnumerations();
                    return this.enumerations;
                }
            }
        }

        public IInterfaceTypeDictionary Interfaces
        {
            get
            {
                lock (this.syncObject)
                {
                    CheckInterfaces();
                    return this.interfaces;
                }
            }
        }

        public IStructTypeDictionary Structs
        {
            get
            {
                lock (this.syncObject)
                {
                    CheckStructs();
                    return this.structs;
                }
            }
        }

        public IFullTypeDictionary Types
        {
            get
            {
                lock (this.syncObject)
                {
                    CheckTypes();
                    return this.types;
                }
            }
        }

        private void CheckTypes()
        {
            lock (this.syncObject)
                if (this.types == null)
                    this.types = InitializeTypes();
        }

        //#endregion

        //#region IDeclaration<IGeneralDeclarationUniqueIdentifier> Members

        public IGeneralDeclarationUniqueIdentifier UniqueIdentifier
        {
            get { return TypeSystemIdentifiers.GetDeclarationIdentifier(this.FullName); }
        }

        //#endregion

        //#region IDeclaration Members

        public event EventHandler Disposed;

        public string Name
        {
            get
            {
                lock (this.syncObject)
                {
                    string fullSpace = this.namespaceInfo.StringsSection[this.namespaceInfo.Value];
                    return fullSpace.Substring(this.namespaceInfo.SubspaceStart, this.namespaceInfo.SubspaceLength);
                }
            }
        }

        //#endregion

        //#region IDisposable Members

        public void Dispose()
        {
            lock (this.syncObject)
            {
                this.owningAssembly = null;
                this.namespaceInfo = null;
            }
        }

        //#endregion

        #region __ICliTypeParent Members

        public _ICliManager IdentityManager
        {
            get { return (_ICliManager)((_ICliAssembly)this.Assembly).IdentityManager; }
        }

        _ICliAssembly __ICliTypeParent.Assembly
        {
            get { return (_ICliAssembly)this.Assembly; }
        }

        public IControlledCollection<ICliMetadataTypeDefinitionTableRow> _Types
        {
            get
            {
                lock (this.syncObject)
                    return this.namespaceInfo._Types;
            }
        }

        #endregion

        #region ICliTypeParent Members

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name)
        {
            return CliCommon.FindTypeImplementation(this.IdentityManager, this.Assembly, @namespace, name, this.namespaceInfo);
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name, string moduleName)
        {
            return CliCommon.FindTypeImplementation(this.IdentityManager, this.Assembly, @namespace, name, moduleName, this.namespaceInfo, this.Assembly.Modules);
        }

        public ICliMetadataTypeDefinitionTableRow FindType(IGeneralTypeUniqueIdentifier uniqueIdentifier)
        {
            return CliCommon.FindTypeImplementation(this.IdentityManager, this.Assembly, uniqueIdentifier, this.namespaceInfo);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("namespace {0}", this.UniqueIdentifier);
        }

        private void CheckClasses()
        {
            lock (this.syncObject)
                if (this.classes == null)
                    this.classes = InitializeClasses();
        }

        private void CheckDelegates()
        {
            lock (this.syncObject)
            {
                if (this.delegates == null)
                    this.delegates = InitializeDelegates();
            }
        }

        private void CheckEnumerations()
        {
            lock (this.syncObject)
                if (this.enumerations == null)
                    this.enumerations = InitializeEnumerations();
        }

        private void CheckInterfaces()
        {
            lock (this.syncObject)
                if (this.interfaces == null)
                    this.interfaces = InitializeInterfaces();
        }

        private void CheckStructs()
        {
            lock (this.syncObject)
                if (this.structs == null)
                    this.structs = InitializeStructs();
        }

        private CliClassTypeDictionary InitializeClasses()
        {
            return new CliClassTypeDictionary(this, (CliFullTypeDictionary)this.Types);
        }

        private CliDelegateTypeDictionary InitializeDelegates()
        {
            return new CliDelegateTypeDictionary(this, (CliFullTypeDictionary)this.Types);
        }

        private CliEnumTypeDictionary InitializeEnumerations()
        {
            return new CliEnumTypeDictionary(this, (CliFullTypeDictionary)this.Types);
        }

        private CliInterfaceTypeDictionary InitializeInterfaces()
        {
            return new CliInterfaceTypeDictionary(this, (CliFullTypeDictionary)this.Types);
        }

        private CliStructTypeDictionary InitializeStructs()
        {
            return new CliStructTypeDictionary(this, (CliFullTypeDictionary)this.Types);
        }

        private CliFullTypeDictionary InitializeTypes()
        {
            return new CliFullTypeDictionary(this.namespaceInfo._Types, this);
        }

    }
}
