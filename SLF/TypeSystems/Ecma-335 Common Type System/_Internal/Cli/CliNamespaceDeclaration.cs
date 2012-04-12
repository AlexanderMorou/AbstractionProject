using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliNamespaceDeclaration :
        INamespaceDeclaration
    {
        private CliAssembly owningAssembly;
        private uint namespaceIndex;
        private int partCount;

        //#region INamespaceDeclaration Members

        public IAssembly Assembly
        {
            get { return this.owningAssembly; }
        }

        public string FullName
        {
            get { throw new NotImplementedException(); }
        }

        public INamespaceParent Parent
        {
            get { throw new NotImplementedException(); }
        }

        //#endregion

        //#region INamespaceParent Members

        public IFullMemberDictionary Members
        {
            get { throw new NotImplementedException(); }
        }

        public INamespaceDictionary Namespaces
        {
            get { throw new NotImplementedException(); }
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
            get { throw new NotImplementedException(); }
        }

        //#endregion

        //#region IDeclaration Members

        public event EventHandler Disposed;

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        //#endregion

        //#region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //#endregion
    }
}
