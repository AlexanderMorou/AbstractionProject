﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal class AllenCopelandLanguageVendor :
        LanguageVendorBase,
        IAllenCopelandLanguageVendor
    {
        public static readonly IAllenCopelandLanguageVendor Singleton = new AllenCopelandLanguageVendor();
        private AllenCopelandLanguageVendor()
            : base("Allen C. Copeland Jr.", new Guid(0x6A9AFE0B, 0x5C4A, 0x4FE8, 0x9F, 0x2B, 0x91, 0x43, 0x4C, 0xE1, 0x29, 0xEB))
        {

        }
    }
}