using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;

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
            get
            {
                string ns = path;

                int lastIndex = 0;
                CliNamespaceKeyedTree topLevel = this.info;
                INamespaceDictionary topNamespaceDict = this;
                StringBuilder pathBuilder = new StringBuilder();
                bool first = true;
            nextPart:
                int next = ns.IndexOf('.', lastIndex);
                if (first)
                    first = false;
                else
                    pathBuilder.Append('.');
                if (next != -1)
                {
                    string current = ns.Substring(lastIndex, next - lastIndex);
                    pathBuilder.Append(current);
                    uint currentHash = (uint) current.GetHashCode();
                    if (topLevel.ContainsKey(currentHash))
                    {

                        topLevel = topLevel[currentHash];
                        topNamespaceDict = topNamespaceDict[AstIdentifier.GetDeclarationIdentifier(pathBuilder.ToString())].Namespaces;
                    }
                    else
                        return null;
                    lastIndex = next + 1;
                    goto nextPart;
                }
                else
                {
                    string current = ns.Substring(lastIndex);
                    pathBuilder.Append(current);
                    uint currentHash = (uint) current.GetHashCode();
                    if (topLevel.ContainsKey(currentHash))
                    {
                        topLevel = topLevel[currentHash];
                        return topNamespaceDict[AstIdentifier.GetDeclarationIdentifier(pathBuilder.ToString())];
                    }
                    else
                        return null;
                }
            }
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
