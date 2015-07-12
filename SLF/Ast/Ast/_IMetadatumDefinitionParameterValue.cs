using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    internal interface _IMetadatumDefinitionParameterValue :
        IMetadatumDefinitionParameterValue
    {
        IMetadatumDefinitionParameter AddSelf(_IMetadatumDefinitionParameterCollection target);
    }
    public interface IMetadatumDefinitionParameterValue
    {
        object Value { get; }
    }
}
