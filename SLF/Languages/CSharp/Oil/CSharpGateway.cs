using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public static class CSharpGateway
    {
        private static readonly ICSharpLanguage language = InitializeCSharpLanguage();

        private static ICSharpLanguage InitializeCSharpLanguage()
        {
            return new CSharpLanguage();
        }

        public static ICSharpLanguage Language
        {
            get
            {
                return language;
            }
        }
    }
}
