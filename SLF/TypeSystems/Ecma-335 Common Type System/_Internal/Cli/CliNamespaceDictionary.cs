using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliNamespaceDictionary :
        CliMetadataDrivenDictionary<IGeneralDeclarationUniqueIdentifier, uint, INamespaceDeclaration>,
        INamespaceDictionary
    {
        private CliAssembly owner;
        private CliNamespaceKeyedTree info;
        private INamespaceParent parent;

        internal CliNamespaceDictionary(CliAssembly owner, INamespaceParent parent, CliNamespaceKeyedTree info)
            : base(info.Count)
        {
            this.owner = owner;
            this.info = info;
            this.parent = parent;
        }

        #region INamespaceDictionary Members

        public INamespaceParent Parent
        {
            get { return this.parent; }
        }

        public bool PathExists(string path)
        {
            throw new NotImplementedException();
        }

        public INamespaceDeclaration this[string path]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        protected override uint GetMetadataAt(int index)
        {
            return this.info.Keys[index];
        }

        protected override INamespaceDeclaration CreateElementFrom(uint metadata)
        {
            return new CliNamespaceDeclaration(this.owner, this.parent, this.info[metadata]);
        }
    }
}
