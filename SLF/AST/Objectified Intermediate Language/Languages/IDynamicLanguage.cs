﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a language
    /// that is written to work on the dynamic language runtime.
    /// </summary>
    /// <remarks>Languages written on top of the DLR may have a
    /// sligntly different ruleset than the Slf.</remarks>
    /// <typeparam name="TRootNode">The type concrete node in the syntax tree
    /// of the language which denotes the root entry for a given parse.</typeparam>
    public interface IDynamicLanguage<TRootNode> :
        IHighLevelLanguage<TRootNode>
        where TRootNode :
            IConcreteNode
    {
    }
}