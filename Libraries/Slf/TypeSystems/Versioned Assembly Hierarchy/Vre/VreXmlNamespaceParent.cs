using AllenCopeland.Abstraction.Slf._Internal.Vre;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public class VreXmlNamespaceParent<TEnvironment, TVersion, TIdentityManager> :
        VreXmlTypeParent<TEnvironment, TVersion, TIdentityManager>,
        IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        private IVreNamespaceDictionary<TEnvironment, TVersion, TIdentityManager> namespaces;
        private XmlNamespaceTree nsTreeNode;

        internal VreXmlNamespaceParent(XmlNode node, XmlNamespaceTree nsTreeNode)
            : base(node ?? nsTreeNode.Value)
        {
            this.nsTreeNode = nsTreeNode;
        }

        internal VreXmlNamespaceParent(XmlNode node, XmlNamespaceTree nsTreeNode, TEnvironment environment)
            : base(node ?? nsTreeNode.Value, environment)
        {
            this.nsTreeNode = nsTreeNode;
        }
        public IVreNamespaceDictionary<TEnvironment, TVersion, TIdentityManager> Namespaces
        {
            get 
            {
                return this.namespaces ?? (this.namespaces = this.InitializeNamespaces());
            }
        }

        private IVreNamespaceDictionary<TEnvironment, TVersion, TIdentityManager> InitializeNamespaces()
        {
            Dictionary<string, IVreNamespace<TEnvironment, TVersion, TIdentityManager>> result = new Dictionary<string, IVreNamespace<TEnvironment, TVersion, TIdentityManager>>();
            foreach (var nsKey in this.nsTreeNode.Keys)
            {
                var treeNode = this.nsTreeNode[nsKey];
                result.Add(nsKey, new VreXmlNamespace<TEnvironment, TVersion, TIdentityManager>(treeNode.Value, treeNode, this.Environment, this));
            }
            return new VreXmlNamespaceDictionary<TEnvironment, TVersion, TIdentityManager>(result);
        }

        internal XmlNamespaceTree NamespaceNode { get { return this.nsTreeNode; } }
    }
}
