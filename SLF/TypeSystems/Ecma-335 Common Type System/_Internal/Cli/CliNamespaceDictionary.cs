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
            : base(info.Keys)
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
                        topNamespaceDict = topNamespaceDict[TypeSystemIdentifiers.GetDeclarationIdentifier(pathBuilder.ToString())].Namespaces;
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
                        return topNamespaceDict[TypeSystemIdentifiers.GetDeclarationIdentifier(pathBuilder.ToString())];
                    }
                    else
                        return null;
                }
            }
        }

        #endregion

        public override INamespaceDeclaration this[IGeneralDeclarationUniqueIdentifier key]
        {
            get
            {
                INamespaceDeclaration result;
                if (base.TryGetValue(key, out result))
                    return result;
                else
                    return this[key.Name];
            }
        }

        protected override INamespaceDeclaration CreateElementFrom(int index, uint metadata)
        {
            return new CliNamespaceDeclaration(this.owner, this.parent, this.info[metadata]);
        }

        protected override IGeneralDeclarationUniqueIdentifier GetIdentifierFrom(int index, uint metadata)
        {
            var namespaceInfo = this.info[metadata];
            string fullSpace = namespaceInfo.StringsSection[namespaceInfo.Value];
            if (namespaceInfo.SubspaceLength != 0)
                fullSpace = fullSpace.Substring(0, namespaceInfo.SubspaceStart + namespaceInfo.SubspaceLength);
            return TypeSystemIdentifiers.GetDeclarationIdentifier(fullSpace);
        }
    }
}
