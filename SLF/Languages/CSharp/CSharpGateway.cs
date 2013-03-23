using AllenCopeland.Abstraction.Slf.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    public static class CSharpGateway
    {
        public static ICSharpLanguage GetCSharpLanguage(this IMicrosoftLanguageVendor vendor)
        {
            return CSharpLanguage.Singleton;
        }

    }
}
