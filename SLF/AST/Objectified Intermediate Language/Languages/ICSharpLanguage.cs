using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ICSharpLanguage :
        IVersionedHighLevelLanguage<CSharpLanguageVersion, ICSharpCompilationUnit>
    {
        new ICSharpProvider GetProvider();
        new ICSharpProvider GetProvider(CSharpLanguageVersion version);
    }
}
