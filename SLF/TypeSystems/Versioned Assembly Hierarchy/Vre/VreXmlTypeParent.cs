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
    public class VreXmlTypeParent<TEnvironment, TVersion, TIdentityManager> :
        VreXmlNode,
        IVreTypeParent<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        private TEnvironment _environment;
        private IControlledDictionary<string, IVreType<TEnvironment, TVersion, TIdentityManager>> _types;
        public VreXmlTypeParent(XmlNode node)
            : base(node)
        {

        }

        public VreXmlTypeParent(XmlNode node, TEnvironment environment)
            : base(node)
        {
            this._environment = environment;
        }

        public TEnvironment Environment { get { return OnGetEnvironment(); } }

        protected virtual TEnvironment OnGetEnvironment()
        {
            return this._environment;
        }

        public IControlledDictionary<string, IVreType<TEnvironment, TVersion, TIdentityManager>> Types
        {
            get { return this._types ?? (this._types = this.InitializeTypes()); }
        }

        private IControlledDictionary<string, IVreType<TEnvironment, TVersion, TIdentityManager>> InitializeTypes()
        {
            if (this.XmlNode != null)
            {
                return new ControlledDictionary<string, IVreType<TEnvironment, TVersion, TIdentityManager>>(XmlExtensions.ParseDictionary<Dictionary<string, IVreType<TEnvironment, TVersion, TIdentityManager>>, string, IVreType<TEnvironment, TVersion, TIdentityManager>, XmlElement>(
                    this.XmlNode,
                    this.Environment.XmlNamespaceManager,
                    "Types",
                    "Type",
                    d => new VreXmlType<TEnvironment, TVersion, TIdentityManager>(this.Environment, d.Extra, this),
                    e => e.Extra.Item2.Name));
            }
            else
                return new ControlledDictionary<string, IVreType<TEnvironment, TVersion, TIdentityManager>>(new KeyValuePair<string, IVreType<TEnvironment, TVersion, TIdentityManager>>[0]);
        }
    }
}
