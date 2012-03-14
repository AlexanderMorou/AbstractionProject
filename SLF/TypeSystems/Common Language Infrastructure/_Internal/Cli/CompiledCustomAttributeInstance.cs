using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledCustomAttributeInstance :
        IMetadatum
    {
        private ICliManager manager;
        public CompiledCustomAttributeInstance(Attribute attribute, IMetadataEntity declarationPoint, ICliManager manager)
        {
            this.DeclarationPoint = declarationPoint;
            this.WrappedAttribute = attribute;
        }

        #region IMetadatum Members

        public IType Type
        {
            get
            {
                if (this.WrappedAttribute == null)
                    throw new InvalidOperationException();
                return manager.ObtainTypeReference(this.WrappedAttribute.GetType());
            }
        }

        public Attribute WrappedAttribute { get; private set; }

        public IMetadataEntity DeclarationPoint { get; private set; }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
