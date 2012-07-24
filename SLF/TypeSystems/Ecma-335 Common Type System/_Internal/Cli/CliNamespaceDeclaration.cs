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
        _ICliTypeParent
    {
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owningAssembly"></param>
        /// <param name="parent"></param>
        /// <param name="namespaceInfo"></param>
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
            get { throw new NotImplementedException(); }
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
            get { throw new NotImplementedException(); }
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
                if (this.classes == null)
                    this.classes = new CliClassTypeDictionary(this, (CliFullTypeDictionary)this.Types);
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
            get {
                if (this.types == null)
                    this.types = new CliFullTypeDictionary(this.namespaceInfo.NamespaceTypes, this);
                return this.types;
            }
        }

        //#endregion

        //#region IDeclaration<IGeneralDeclarationUniqueIdentifier> Members

        public IGeneralDeclarationUniqueIdentifier UniqueIdentifier
        {
            get { return AstIdentifier.GetDeclarationIdentifier(this.FullName); }
        }

        //#endregion

        //#region IDeclaration Members

        public event EventHandler Disposed;

        public string Name
        {
            get
            {
                string fullSpace = this.namespaceInfo.StringsSection[this.namespaceInfo.Value];
                return fullSpace.Substring(this.namespaceInfo.SubspaceStart, this.namespaceInfo.SubspaceLength);
            }
        }

        //#endregion

        //#region IDisposable Members

        public void Dispose()
        {
            this.owningAssembly = null;
            this.namespaceInfo = null;
        }

        //#endregion

        #region _ICliTypeParent Members

        public _ICliManager Manager
        {
            get { return (_ICliManager)((_ICliAssembly)this.Assembly).IdentityManager;}
        }

        _ICliAssembly _ICliTypeParent.Assembly
        {
            get { return (_ICliAssembly)this.Assembly; }
        }

        public IReadOnlyCollection<ICliMetadataTypeDefinitionTableRow> _Types
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ICliTypeParent Members

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name)
        {
            return CliCommon.FindTypeImplementation(@namespace, name, this.namespaceInfo);
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name, string moduleName)
        {
            return CliCommon.FindTypeImplementation(@namespace, name, moduleName, this.namespaceInfo, this.Assembly.Modules);
        }

        public ICliMetadataTypeDefinitionTableRow FindType(IGeneralTypeUniqueIdentifier uniqueIdentifier)
        {
            return CliCommon.FindTypeImplementation(uniqueIdentifier, this.namespaceInfo);
        }

        #endregion
    }
}
