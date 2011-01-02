using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Dynamic;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class CustomAttributeDefinition
    {
        private partial class AttributeWrapper :
            Attribute
        {
            public AttributeWrapper()
            {
            }

            //#region IDynamicMetaObjectProvider Members

            //public DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
            //{
            //    throw new NotImplementedException();
            //}

            //#endregion
        }
    }
}
