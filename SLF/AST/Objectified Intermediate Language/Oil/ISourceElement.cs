﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with an
    /// element defined within source.
    /// </summary>
    public interface ISourceElement
    {
        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes
        /// the start point of the <see cref="ISourceElement"/>.
        /// </summary>
        LineColumnPair Start { get; set; }
        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes the
        /// end point of the <see cref="ISourceElement"/>.
        /// </summary>
        LineColumnPair End { get; set; }
    }
}