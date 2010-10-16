using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public interface IGDFileObjectRelationalMap :
        IControlledStateDictionary<IScannableEntry, IEntryObjectRelationalMap>
    {
        /// <summary>
        /// Returns the <see cref="IGDFile"/> from which the object relational 
        /// map is derived.
        /// </summary>
        IGDFile Source { get; }
    }
}
