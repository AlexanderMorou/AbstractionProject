using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class CustomAttributeDefinition
    {
        partial class AttributeWrapper
        {
            private Dictionary<Type, _AttributeCollection> AttributeCache;
            private class _AttributeCollection :
                AttributeCollection
            {
                private CustomAttributeDefinition attr;
                public _AttributeCollection(CustomAttributeDefinition attr, AttributeWrapper[] attributes)
                    : base(attributes)
                {
                    this.attr = attr;
                }
            }
        }
    }
}
