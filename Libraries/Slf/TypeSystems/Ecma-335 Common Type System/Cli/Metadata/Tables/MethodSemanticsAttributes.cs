﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    /// <summary>
    /// Determines the kind of specialized meaning that
    /// is associated to the method.
    /// </summary>
    public enum MethodSemanticsAttributes
    {
        None = 0x0,
        Setter = 0x01,
        Getter = 0x02,
        Other = 0x04,
        AddOn = 0x08,
        RemoveOn = 0x10,
        Fire = 0x20,
    }
}
