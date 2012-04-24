using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliNamespaceDeclaration :
        INamespaceDeclaration
    {
        private CliAssembly owningAssembly;
        CliNamespaceKeyedTreeNode namespaceInfo;
        private INamespaceParent parent;
        private INamespaceDictionary namespaces;
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
                string fullSpace = this.owningAssembly.MetadataRoot.StringsHeap[this.namespaceInfo.Value];
                if (this.namespaceInfo.IsSubspace)
                    return fullSpace.Substring(0, this.namespaceInfo.SubspaceStart + this.namespaceInfo.SubspaceLength);
                else
                    return fullSpace;
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
            get {
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
            get { throw new NotImplementedException(); }
        }

        public IDelegateTypeDictionary Delegates
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumTypeDictionary Enums
        {
            get { throw new NotImplementedException(); }
        }

        public IInterfaceTypeDictionary Interfaces
        {
            get { throw new NotImplementedException(); }
        }

        public IStructTypeDictionary Structs
        {
            get { throw new NotImplementedException(); }
        }

        public IFullTypeDictionary Types
        {
            get { throw new NotImplementedException(); }
        }

        //#endregion

        //#region IDeclaration<IGeneralDeclarationUniqueIdentifier> Members

        public IGeneralDeclarationUniqueIdentifier UniqueIdentifier
        {
            get { return AstIdentifier.GetDeclarationIdentifier(this.Name); }
        }

        //#endregion

        //#region IDeclaration Members

        public event EventHandler Disposed;

        public string Name
        {
            get
            {
                string fullSpace = this.owningAssembly.MetadataRoot.StringsHeap[this.namespaceInfo.Value];
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
    }
}
