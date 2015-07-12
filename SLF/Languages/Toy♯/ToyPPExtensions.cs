using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.ToySharp
{
    public static class ToySharpExtensions
    {
        public static IToySharpLanguage GetToySharpLanguage(this IAllenCopelandLanguageVendor vendor)
        {
            return ToySharpLanguage.Singleton;
        }
    }
}
