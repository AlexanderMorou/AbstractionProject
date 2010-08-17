using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
/*----------------------------------------\
| Copyright © 2009 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledCustomAttributeInstance :
        ICustomAttributeInstance
    {
        public CompiledCustomAttributeInstance(Attribute attribute, ICustomAttributedDeclaration declarationPoint)
        {
            this.DeclarationPoint = declarationPoint;
            this.WrappedAttribute = attribute;
        }

        #region ICustomAttributeInstance Members

        public IType Type
        {
            get
            {
                if (this.WrappedAttribute == null)
                    throw new InvalidOperationException();
                return this.WrappedAttribute.GetType().GetTypeReference();
            }
        }

        public Attribute WrappedAttribute { get; private set; }

        public ICustomAttributedDeclaration DeclarationPoint { get; private set; }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
