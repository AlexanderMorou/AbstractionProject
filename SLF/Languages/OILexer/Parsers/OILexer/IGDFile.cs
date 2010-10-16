using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Parsers.Oilexer
{
    /// <summary>
    /// Defines properties and methods for working with a grammar description file.
    /// </summary>
    public interface IGDFile :
        ICollection<IEntry>,
        IConcreteNode
    {
        /// <summary>
        /// Returns the <see cref="IReadOnlyCollection"/> of files that the <see cref="IGDFile"/>
        /// was created from.
        /// </summary>
        IReadOnlyCollection<string> Files { get; }
        /// <summary>
        /// Returns the <see cref="IGDFileOptions"/> which determines the options related to the 
        /// resulted generation process.
        /// </summary>
        IGDFileOptions Options { get; }

        IList<string> Includes { get; }

        IReadOnlyCollection<IGDRegion> Regions { get; }
    }
}
