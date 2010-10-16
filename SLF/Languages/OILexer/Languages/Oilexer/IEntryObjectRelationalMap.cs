using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public interface IEntryObjectRelationalMap
    {
        IReadOnlyCollection<IScannableEntry> Implements { get; }
        /// <summary>
        /// Obtains an enumerable state machine which yields the sequences of 
        /// variations of which the <see cref="IEntryObjectRelationalMap"/>'s
        /// <see cref="Entry"/> can be.
        /// </summary>
        IEnumerable<IEnumerable<IScannableEntry>> Variations { get; }
        /// <summary>
        /// The <see cref="IScannableEntry"/> which is represented by the <see cref="IEntryObjectRelationalMap"/>.
        /// </summary>
        IScannableEntry Entry { get; }
    }
}
