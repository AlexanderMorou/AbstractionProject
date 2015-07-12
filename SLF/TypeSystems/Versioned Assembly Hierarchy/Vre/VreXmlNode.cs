using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AllenCopeland.Abstraction.Slf.Vre
{
  /// <summary>
  /// Provides a simple outline to an XML derived Versioned Runtime Environment construct.
  /// </summary>
  public abstract class VreXmlNode :
      IDisposable
  {
    private XmlNode xmlNode;
    /// <summary>
    /// Creates a new <see cref="VreXmlNode"/> with the <paramref name="node"/> provided.
    /// </summary>
    /// <param name="node"></param>
    public VreXmlNode(XmlNode node) { this.xmlNode = node; }

    protected VreXmlNode() { }

    /// <summary>
    /// Denotes the <see cref="XmlNode"/> on which the <see cref="VreXmlNode"/> is built.
    /// </summary>
    public XmlNode XmlNode { get { return this.xmlNode; } }

    /// <summary>
    /// Disposes the <see cref="VreXmlNode"/>.
    /// </summary>
    /// <remarks>Releases the <see cref="XmlNode"/>.</remarks>
    public virtual void Dispose()
    {
        this.xmlNode = null;
    }
  }
}
