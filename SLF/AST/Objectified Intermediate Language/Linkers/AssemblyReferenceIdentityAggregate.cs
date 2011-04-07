using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public class AssemblyReferenceIdentityAggregate :
        AggregateIdentityNode,
        IAssemblyReferenceIdentityAggregate
    {
        #region IAssemblyReferenceIdentityAggregate Members

        public IEnumerable<string> Aliases
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IAssemblyReference> References
        {
            get { throw new NotImplementedException(); }
        }

        public IAssemblyReferenceCollection IdentitySource
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        public override string Name
        {
            get { return null; }
        }

        public override AggregateIdentityKind Kind
        {
            get { return AggregateIdentityKind.RootNamespace; }
        }
    }
}
