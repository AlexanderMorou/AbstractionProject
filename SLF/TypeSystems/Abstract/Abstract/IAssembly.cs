﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /* *
     * If you wonder why I did all of this on types, assemblies, modules and so on
     * it's because I didn't want to derive from the System bases.  Primarily
     * due to a few structural differences between how I preferred to work
     * and how the bases work.  Additionally, an 'IntermediateAssembly'
     * precedes an actual assembly, therefore deriving from the system's 
     * Assembly would confuse this fact.  Even though I used similar names,
     * I chose interfaces as a common ground to ensure that it's clear
     * that the only similarity between intermediate and compiled
     * assembly is in name and underlying functionality, not that
     * they are identical.
     * */
    /// <summary>
    /// Defines properties and methods for working with an assembly.
    /// </summary>
    public interface IAssembly :
        INamespaceParent,
        ICustomAttributedDeclaration,
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IAssemblyInformation"/> about the current <see cref="IAssembly"/>
        /// instance.
        /// </summary>
        IAssemblyInformation AssemblyInformation { get; }
        /// <summary>
        /// Returns the <see cref="IModule"/> which exposes
        /// the manifest data for the current <see cref="IAssembly"/>.
        /// </summary>
        IModule ManifestModule { get; }

    }
}