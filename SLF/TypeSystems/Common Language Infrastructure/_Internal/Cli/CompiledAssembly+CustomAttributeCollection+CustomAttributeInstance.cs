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
    partial class CompiledAssembly
    {
        partial class CustomAttributeCollection
        {
            private class CustomAttributeInstance :
                ICustomAttributeInstance
            {
                private CompiledAssembly assembly;
                private Attribute attribute;
                public CustomAttributeInstance(Attribute attribute, CompiledAssembly assembly)
                {
                    this.assembly = assembly;
                    this.attribute = attribute;
                }

                #region ICustomAttributeInstance Members

                public IType Type
                {
                    get { return this.attribute.GetType().GetTypeReference(); }
                }

                public Attribute WrappedAttribute
                {
                    get { return this.attribute; }
                }

                public ICustomAttributedDeclaration DeclarationPoint
                {
                    get { return this.assembly; }
                }

                #endregion

                #region IDisposable Members

                public void Dispose()
                {
                    this.assembly = null;
                    this.attribute = null;
                }

                #endregion
            }
        }
    }
}
