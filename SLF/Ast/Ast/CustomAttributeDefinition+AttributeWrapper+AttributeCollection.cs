using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class MetadatumDefinition
    {
        partial class AttributeWrapper
        {
            private class _AttributeCollection :
                AttributeCollection
            {
                private MetadatumDefinition attr;
                public _AttributeCollection(MetadatumDefinition attr, AttributeWrapper[] attributes)
                    : base(attributes)
                {
                    this.attr = attr;
                }
            }
        }
    }
}
