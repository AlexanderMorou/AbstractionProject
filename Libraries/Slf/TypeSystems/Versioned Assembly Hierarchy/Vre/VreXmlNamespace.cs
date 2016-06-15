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
    public class VreXmlNamespace<TEnvironment, TVersion, TIdentityManager> :
        VreXmlNamespaceParent<TEnvironment, TVersion, TIdentityManager>,
        IVreNamespace<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        private IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager> parent;
        internal VreXmlNamespace(XmlNode node, XmlNamespaceTree nsTreeNode, TEnvironment environment, IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager> parent)
            : base(node, nsTreeNode, environment)
        {
            this.parent = parent;
        }

        public string Name
        {
            get {
                int lastDot = this.FullName.LastIndexOf('.');
                if (lastDot == -1)
                    return this.FullName;
                return this.FullName.Substring(lastDot + 1);
            }
        }

        public string FullName
        {
            get
            {
                return this.NamespaceNode.Name;
            }
        }
        public override string ToString()
        {
            return this.Name;
        }


        public IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager> Parent
        {
            get { return this.parent; }
        }
    }
}
