﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Globalization;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IAssemblyUniqueIdentifier :
        IDeclarationUniqueIdentifier<IAssemblyUniqueIdentifier>,
        IGeneralDeclarationUniqueIdentifier
    {
        /// <summary>
        /// Returns the <see cref="Version"/> of the assembly.
        /// </summary>
        Version Version { get; }
        /// <summary>
        /// Returns the <see cref="ICultureIdentifier"/> which denotes the culture
        /// of the assembly.
        /// </summary>
        ICultureIdentifier Culture { get; }
        /// <summary>
        /// Returns the <see cref="Byte"/> array of the last 8 digits
        /// of the SHA-1 hash of the public key under which the assembly
        /// is assigned.
        /// </summary>
        byte[] PublicKeyToken { get; }
    }
}