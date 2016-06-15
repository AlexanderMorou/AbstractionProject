using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public abstract class VreXmlNamespaceParentVersion<TEnvironment, TVersion, TIdentityManager> :
        VreXmlTypeParentVersion<TEnvironment, TVersion, TIdentityManager>,
        IVreNamespaceParentVersion<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        private IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager> _rootParent;
        private IVreNamespaceDictionaryVersion<TEnvironment, TVersion, TIdentityManager> _namespaces;

        public VreXmlNamespaceParentVersion(TVersion version, IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager> rootParent)
            : base(version)
        {
            this._rootParent = rootParent;
        }

        public IVreNamespaceDictionaryVersion<TEnvironment, TVersion, TIdentityManager> Namespaces
        {
            get
            {
                //return this._namespaces ?? (this._namespaces = new VreXmlNamespaceDictionaryVersion<TEnvironment, TVersion, TIdentityManager>(this, ));
                throw new NotImplementedException();
            }
        }

        public override void Dispose()
        {
            this._namespaces = null;
            base.Dispose();
        }
    }
}
