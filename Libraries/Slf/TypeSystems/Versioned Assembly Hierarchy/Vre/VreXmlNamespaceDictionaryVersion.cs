using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public class VreXmlNamespaceDictionaryVersion<TEnvironment, TVersion, TIdentityManager> :
        ControlledDictionary<string, IVreNamespaceVersion<TEnvironment, TVersion, TIdentityManager>>,
        IVreNamespaceDictionaryVersion<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        private IVreNamespaceParentVersion<TEnvironment, TVersion, TIdentityManager> _parent;

        public VreXmlNamespaceDictionaryVersion(IVreNamespaceParentVersion<TEnvironment, TVersion, TIdentityManager> parent, IEnumerable<KeyValuePair<string, IVreNamespaceVersion<TEnvironment, TVersion, TIdentityManager>>> elements)
            : base(elements)
        {
            this._parent = parent;
        }

        public IVreNamespaceVersion<TEnvironment, TVersion, TIdentityManager> GetNamespace(string dottedName)
        {
            string[] parts = dottedName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            IVreNamespaceDictionaryVersion<TEnvironment, TVersion, TIdentityManager> current = this;
            IVreNamespaceVersion<TEnvironment, TVersion, TIdentityManager> currentNamespace = null;
            foreach (var part in parts)
            {
                currentNamespace = current[part];
                current = currentNamespace.Namespaces;
            }
            return currentNamespace;
        }
    }
}
