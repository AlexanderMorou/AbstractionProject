﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a top-level method
    /// defined at a top level (outside of a class.)
    /// </summary>
    public interface ITopLevelMethodMember :
        IMethodMember<ITopLevelMethodMember, INamespaceParent>
    {
        /// <summary>
        /// Returns the <see cref="IModule"/> which declares the 
        /// <see cref="ITopLevelMethodMember"/>.
        /// </summary>
        IModule DeclaringModule { get; }
        /// <summary>
        /// Returns the <see cref="String"/> value relative to the full
        /// name of the method.
        /// </summary>
        string FullName { get; }
    }
}