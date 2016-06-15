using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public class VreXmlElement :
      VreXmlNode
    {
        public VreXmlElement(XmlElement element) :
            base(element) { }

        protected VreXmlElement() : base() { }

        public new XmlElement XmlNode { get { return (XmlElement)base.XmlNode; } }
    }
}
