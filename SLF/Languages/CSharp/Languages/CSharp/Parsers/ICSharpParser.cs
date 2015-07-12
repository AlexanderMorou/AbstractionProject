using AllenCopeland.Abstraction.Slf.Cst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Parsers
{
    /// <summary>
    /// Defines properties and methods for working with a parser
    /// which parses C&#9839; source files.
    /// </summary>
    public interface ICSharpParser :
        ILanguageParser<ICSharpCompilationUnit>
    {
    }
}
