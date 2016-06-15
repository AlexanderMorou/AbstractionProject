using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Xml;
namespace AllenCopeland.Abstraction.Slf._Internal.Vre
{
    internal class XmlNamespaceTree :
        KeyedTree<string, XmlNode, XmlNamespaceTree>,
        IKeyedTreeNode<string, XmlNode, XmlNamespaceTree>
    {
        public static XmlNamespaceTree GenerateTree(XmlNode environmentNode, XmlNamespaceManager manager)
        {
            XmlNamespaceTree result = new XmlNamespaceTree();
            var pluralRoot = environmentNode.SelectSingleNode("./vre:Namespaces", manager);
            if (pluralRoot == null)
                return result;
            var nodes = pluralRoot.SelectNodes("./vre:Namespace", manager);
            foreach (XmlElement element in nodes.Cast<XmlElement>())
            {
                XmlNamespaceTree currentNode = result;
                string name = element.GetAttribute("Name");
                string[] nameParts = name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder currentName = new StringBuilder();
                bool first = true;
                for (int nameIndex = 0; nameIndex < nameParts.Length; nameIndex++)
                {
                    if (first)
                        first = false;
                    else
                        currentName.Append(".");
                    string namePart = nameParts[nameIndex];
                    currentName.Append(namePart);
                    XmlNamespaceTree target;
                    if (!currentNode.TryGetValue(namePart, out target))
                        target = currentNode.Add(namePart, (nameIndex == nameParts.Length - 1) ? element : null);
                    else if (nameIndex == (nameParts.Length - 1) && target.Value == null)
                        target.Value = element;
                    currentNode = target;
                    target.Name = currentName.ToString();
                }
            }
            return result;
        }

        //public static XmlNamespaceTree CreateFilteredTreeForLibrary(string library, XmlNamespaceTree original)
        //{
            
        //}

        public XmlNode Value { get; set; }

        public string Name { get; set; }
    }
}
