﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic.My;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Oil.VisualBasic
{
    /// <summary>
    /// Defines properties and methods for working with an assembly
    /// from the Visual Basic.NET language.
    /// </summary>
    /// <remarks>Relative to Start in Visual Basic Language 
    /// Specification Version 10.0</remarks>
    public interface IVisualBasicAssembly :
        IVersionedHighLevelIntermediateAssembly<IVisualBasicLanguage, IVisualBasicStart, IVisualBasicProvider, VisualBasicVersion>
    {
        /// <summary>
        /// Returns the <see cref="IMyNamespaceDeclaration"/> which
        /// relates to the special My namespace within Visual Basic.
        /// </summary>
        IMyNamespaceDeclaration MyNamespace { get; }
    }
}