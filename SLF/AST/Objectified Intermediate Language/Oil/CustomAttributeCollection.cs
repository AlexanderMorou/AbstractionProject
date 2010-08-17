using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.TypeSystems.Abstract;
/*----------------------------------------\
| Copyright © 2009 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base custom attributes collection for enabling 
    /// <see cref="ICustomAttributedDeclaration"/> instances to hold
    /// their attribute series.
    /// </summary>
    public class CustomAttributeCollection :
        ControlledStateCollection<ICustomAttributeDefinitionCollection>,
        ICustomAttributeDefinitionCollectionSeries
    {
        public CustomAttributeCollection()
        {
        }

        #region ICustomAttributeDefinitionCollectionSeries Members

        public ICustomAttributeDefinitionCollection Add(params ICustomAttributeDefinition[] attributes)
        {
            throw new NotImplementedException();
        }

        public IIntermediateCustomAttributedDeclaration Parent
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

    }
}
