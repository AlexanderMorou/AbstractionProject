﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// condition statement which alters the flow of the 
    /// execution based upon a boolean condition within a
    /// breakable section of code.
    /// </summary>
    public interface IBreakableConditionBlockStatement :
        IBreakableConditionContinuationStatement,
        IConditionBlockStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IBreakableConditionContinuationStatement"/> which continues the
        /// code flow control conditioning.
        /// </summary>
        new IBreakableConditionContinuationStatement Next { get; set; }
        /// <summary>
        /// Returns the <see cref="IBreakableBlockStatement"/> which contains the 
        /// <see cref="IBreakableConditionBlockStatement"/>.
        /// </summary>
        new IBreakableBlockStatement Parent { get; }
    }
}