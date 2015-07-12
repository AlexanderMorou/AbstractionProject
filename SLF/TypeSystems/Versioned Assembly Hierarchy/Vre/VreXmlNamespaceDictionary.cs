using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public class VreXmlNamespaceDictionary<TEnvironment, TVersion, TIdentityManager> :
        ControlledDictionary<string, IVreNamespace<TEnvironment, TVersion, TIdentityManager>>,
        IVreNamespaceDictionary<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {

        internal VreXmlNamespaceDictionary(Dictionary<string, IVreNamespace<TEnvironment, TVersion, TIdentityManager>> elements)
            : base(elements)
        {
        }

        public IVreNamespace<TEnvironment, TVersion, TIdentityManager> GetNamespace(string dottedName)
        {
            string[] parts = dottedName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            IVreNamespaceDictionary<TEnvironment, TVersion, TIdentityManager> current = this;
            IVreNamespace<TEnvironment, TVersion, TIdentityManager> currentNamespace = null;
            foreach (var part in parts)
            {
                currentNamespace = current[part];
                current = currentNamespace.Namespaces;
            }
            return currentNamespace;
        }
    }
}
